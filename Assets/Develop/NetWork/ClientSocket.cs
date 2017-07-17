using Pomelo.DotNetClient;
using LuaInterface;
using SimpleJson;
using System;
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

	// Use this for initialization
	public static void InitClient(string host, int port, LuaFunction func) 
	{
		PDAction action = delegate()
		{
			if (func != null)
			{
				func.Call();
			}
		};
		pc.initClient(host, port, action);
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
		if (user != null)
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
		};
		pc.NetWorkStateChangedEvent += action;
	}

	public static void Request(string route, string msg)
	{	
		pc.notify(route,msg);
	}

	public static void On(string route, LuaFunction func)
	{
		Action<string> action = delegate(string s)
		{
			if (func != null)
			{
				func.Call(s);
			}
		};
		pc.on(route, action);
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

	private static string[] GetMsgKey(string msgKey)
	{	
		char[] delimiterChars = {'|'};
		string[] keys = msgKey.Split(delimiterChars);
		return keys ;
	}
}