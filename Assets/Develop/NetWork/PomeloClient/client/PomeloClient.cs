﻿using SimpleJson;
using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;
using System.Collections.Generic;

namespace Pomelo.DotNetClient
{
    /// <summary>
    /// network state enum
    /// </summary>
    public enum NetWorkState
    {
        [Description("initial state")]
        CLOSED,

        [Description("connecting server")]
        CONNECTING,

        [Description("server connected")]
        CONNECTED,

        [Description("disconnected with server")]
        DISCONNECTED,

        [Description("connect timeout")]
        TIMEOUT,

        [Description("netwrok error")]
        ERROR,

		[Description("be kicked by server")]
		KICKED,
    }

    public class PomeloClient : IDisposable
    {
        public event Action<NetWorkState> NetWorkStateChangedEvent;

        public NetWorkState netWorkState = NetWorkState.CLOSED;   //current network state

        private EventManager eventManager;
        private Socket socket;
        private Protocol protocol;
        private bool disposed = false;
        private uint reqId = 1;

        private static int DEFAULT_RECONNECT_DELAY = 5000;
        private int DEFAULT_MAX_RECONNECT_ATTEMPTS = 5;
        private int reconnectAttempets = 0;
        private int reconnectionDelay = DEFAULT_RECONNECT_DELAY;
        private System.Timers.Timer reconnctTimer = null ;

        private ManualResetEvent timeoutEvent = new ManualResetEvent(false);
        private int timeoutMSec = 5000;    //connect timeout count in millisecond

        private IPEndPoint ie;
        private Action connectCallback = null;

        private MsgQueue msgQueue = new MsgQueue();//断网时的消息缓存队列

        public PomeloClient()
        {
        }

        public void initClient(string host, int port , Action callback = null)
        {
            eventManager = new EventManager();
            NetWorkChanged(NetWorkState.CONNECTING);

            IPAddress ipAddress = null;

            try
            {
                IPAddress[] addresses = Dns.GetHostEntry(host).AddressList;
                foreach (var item in addresses)
                {
                    if (item.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = item;
                        break;
                    }
                }
            }
            catch (Exception)
            {
                NetWorkChanged(NetWorkState.ERROR);
                return;
            }

            if (ipAddress == null)
            {
                throw new Exception("can not parse host : " + host);
            }

            NetWorkStateChangedEvent += (stat) =>
            {
                if ( stat == NetWorkState.ERROR || stat == NetWorkState.DISCONNECTED )
                {
                    protocol.close();
                    DisposeSocket();
                }
                else if( stat == NetWorkState.CONNECTED )
                {
                    if(reconnctTimer!=null)
                        reconnectReset();
                }
            };

            ie = new IPEndPoint(ipAddress, port);  
            beginConnect(callback);         
        }

        public void beginConnect(Action callback =null)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            timeoutEvent.Reset();
            socket.BeginConnect(ie, new AsyncCallback((result) =>
            {
                try
                {
                    this.socket.EndConnect(result);
                    this.protocol = new Protocol(this, this.socket);
                                             
                    if (callback != null)
                    {
                        callback();
                    }
                     NetWorkChanged(NetWorkState.CONNECTED);
                }
                catch (SocketException)
                {                
                    if (netWorkState != NetWorkState.TIMEOUT)
                    {
                        NetWorkChanged(NetWorkState.ERROR);
                    }
                    // Dispose();
                    tryReconnect();
                }
                finally
                {
                    timeoutEvent.Set();
                }
            }), this.socket);

            if (timeoutEvent.WaitOne(timeoutMSec, false))
            {
                if (netWorkState != NetWorkState.CONNECTED && netWorkState != NetWorkState.ERROR)
                {
                    tryReconnect();
                    NetWorkChanged(NetWorkState.TIMEOUT);
                    Dispose();
                }
            }
        }

        private void NetWorkChanged(NetWorkState state)
        {
            if(netWorkState==state){
                return;
            }
            netWorkState = state;

            if (NetWorkStateChangedEvent != null)
            {
                NetWorkStateChangedEvent(state);
            }
        }

        public void connect()
        {
            connect(null, null);
        }

        public void connect(JsonObject user)
        {
            connect(user, null);
        }

        public void connect(Action<JsonObject> handshakeCallback)
        {
            connect(null, handshakeCallback);
        }

        public bool connect(JsonObject user, Action<JsonObject> handshakeCallback)
        {
            try
            {
                protocol.start(user, handshakeCallback);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //private string emptyMsg = new string();
        //private JsonObject emptyMsg = new JsonObject();
        private string emptyMsg = "";
        public void request(string route, Action<string> action)
        {
            this.request(route, emptyMsg, action);
        }

        public void request(string route, string msg, Action<string> action)
        {
            if ( netWorkState != NetWorkState.CONNECTED )
            {
               msgQueue.Enqueue(route,msg,action);
            }
            else
            {
                this.eventManager.AddCallBack(reqId, action);
                protocol.send(route, reqId, msg);
                reqId++;
            }
        }

        public void notify(string route, string msg)
        {
            if ( netWorkState != NetWorkState.CONNECTED )
            {
               msgQueue.Enqueue(route,msg);
            }
            else
            {
                protocol.send(route, msg);
            }        
        }

        public void on(string eventName, Action<string> action)
        {
            eventManager.AddOnEvent(eventName, action);
        }

        internal void processMessage(Message msg)
        {
            if (msg.type == MessageType.MSG_RESPONSE)
            {
                //msg.data["__route"] = msg.route;
                //msg.data["__type"] = "resp";
                eventManager.InvokeCallBack(msg.id, msg.data);
            }
            else if (msg.type == MessageType.MSG_PUSH)
            {
                //msg.data["__route"] = msg.route;
                //msg.data["__type"] = "push";
                eventManager.InvokeOnEvent(msg.route, msg.data);
            }
        }

        public void disconnect(NetWorkState n)
        {
            if (n == NetWorkState.KICKED)
            {
                NetWorkChanged(NetWorkState.KICKED);
            }
        }
        public void disconnect()
        {
            if(netWorkState != NetWorkState.DISCONNECTED){
                NetWorkChanged(NetWorkState.DISCONNECTED);
            }
        }
        public void tryReconnect(Action cb = null)
        {
            if(reconnctTimer==null)
            {
                reconnctTimer = new System.Timers.Timer();
                ElapsedEventHandler action = delegate(object sender, ElapsedEventArgs e) 
                {
                    reconnect(sender,e,cb);
                };
                reconnctTimer.Elapsed += action;
                
            }
            if (reconnectAttempets >= DEFAULT_MAX_RECONNECT_ATTEMPTS)
            {
                Dispose();
            }
            else
            {   
				reconnctTimer.Interval = reconnectionDelay;		
                reconnctTimer.Enabled = true;
                reconnctTimer.AutoReset = false;
				reconnectionDelay *= 2;
				reconnectAttempets++;
            }

        }
        public void reconnect(object source, ElapsedEventArgs e,Action cb = null)
        {
            beginConnect(cb);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // The bulk of the clean-up code
        public virtual void Dispose(bool disposing)
        {
            if (this.disposed)
                return;

            if (disposing)
            {
                // free managed resources
                if (this.protocol != null)
                {
                    this.protocol.close();
                }

                if (this.eventManager != null)
                {
                    this.eventManager.Dispose();
                }
                if (reconnctTimer!=null)
                {
                    reconnctTimer.Enabled = false;
                    reconnctTimer.Dispose();
                    reconnctTimer =null;
                }

                try
                {
                    this.socket.Shutdown(SocketShutdown.Both);
                    this.socket.Close();
                    this.socket = null;
                }
                catch (Exception)
                {
                    //todo : 有待确定这里是否会出现异常，这里是参考之前官方github上pull request。emptyMsg
                }

                this.disposed = true;
            }
        }
        public virtual void reconnectReset()
        {
            reconnectionDelay = DEFAULT_RECONNECT_DELAY;
            reconnectAttempets = 0;
            reconnctTimer.Enabled = false;
        }

        public void sendDelayRequest()
        {
            while(true)
            {
                DelayItem item = msgQueue.Dequeue();
                if( item == null )
                {
                    break;
                }
                if( item.Callback == null )
                {
                    notify(item.Route,item.Msg);
                }
                else
                {
                    request(item.Route, item.Msg, item.Callback );
                }
            }
        }

        private void DisposeSocket()
        {
            if(socket!=null)
            {
                socket.Close();
                socket = null;
            }
        }
    }
}