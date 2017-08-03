using Pomelo.DotNetClient;
using LuaInterface;
using SimpleJson;
using System;
using System.Text.RegularExpressions;
using PDAction = Pomelo.DotNetClient.Action;

public static class MyExtensions
{
	public static JsonObject ToJsonObject(this LuaTable luaTb ,string[] keys )
	{
		JsonObject jo = new JsonObject() ;
		foreach(string key in keys)
		{
			if (string.IsNullOrEmpty(key) == false)
			{
				jo.Add(key,luaTb[key]);
			}
		}
		return jo;
	}
}   

public class ClientSocket {

	static PomeloClient pc = null ;

	static ClientSocket()
	{
		pc = new PomeloClient();
	}

    public static NetWorkState getNetworkState() { return pc.netWorkState; }

	// Use this for initialization
	public static void InitClient(string host, int port ,LuaFunction func) 
	{
        UnityEngine.Debug.Log(string.Format("C#初始化服务器:{0}:{1}", host, port));
		PDAction action = delegate()
		{
			if(func != null)
			{
				func.Call();
			}
		};
		pc.initClient(host,port,action);
	}
	
	// connect server with user 
	public static bool Connect(System.Object user, string keys, LuaFunction func)
	{
		Action<JsonObject> action = delegate(JsonObject jo)
		{
			if (func != null)
			{
				func.Call(jo.ToString());
			}
		};
		if(user	!=null)
		{
			LuaTable lt = (LuaTable)user ;
			return pc.connect(lt.ToJsonObject(GetMsgKey(keys)), action);
		}
		return pc.connect(null, action);
	}

	//add callback for network state change
	public static void AddNetWorkStateChangeEvent(LuaFunction func)
	{
		Action<NetWorkState> action = delegate(NetWorkState networkState)
		{
			if (func != null)
			{
				func.Call((int)networkState);
			}

            UnityEngine.Debug.Log("网络状态切换:" + networkState);
		};
		pc.NetWorkStateChangedEvent += action ;
	}

	//request for something
	// public static void Request(string route, LuaTable msg, string keys)
	// {	
	// 	JsonObject jo = msg.ToJsonObject(GetMsgKey(keys));
	// 	pc.notify(route,jo);
	// }
	public static void Notify(string route, string msg)
	{	
        try
        {
		    pc.notify(route,msg);
        }catch(Exception e)
        {
            UnityEngine.Debug.LogException(e);
        }
	}

	public static void Request(string route, string msg , LuaFunction func)
	{
		Action<string> action = delegate(string s)
		{
			if (func != null)
			{
				func.Call(s);
			}
		};
		pc.request(route,msg,action);
	}

	public static void On(string route ,LuaFunction func)
	{
		Action<string> action = delegate(string s)
		{
			if (func != null)
			{
				func.Call(s);
			}
		};
		pc.on(route,action);
	}

	public static void CSharpOn(string route ,Action<string> func)
	{
		pc.on(route,func);
	}


	public static void TryReconnect(LuaFunction func)
	{
		PDAction action = delegate()
		{
			if (func != null)
			{
				func.Call();
			}
		};
		pc.tryReconnect(action);
	}

	public static void SendDelayRequest()
	{
		pc.sendDelayRequest();
	}

	private static string[] GetMsgKey(string msgKey)
	{	
		char[] delimiterChars = {'|'};
		string[] keys = msgKey.Split(delimiterChars);
		return keys ;
	}

    public static void Dispose()
    {
        pc.Dispose();
    }

}
