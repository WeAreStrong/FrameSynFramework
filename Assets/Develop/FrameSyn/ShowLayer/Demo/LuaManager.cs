using UnityEngine;
using LuaInterface;


public class LuaManager : MonoBehaviour
{
    private LuaState m_kLuaState;
    private LuaFunction mUpdateFunc;
    // private LuaLooper loop = null;

    // Use this for initialization
    void Awake()
    {
        m_kLuaState = new LuaState();
        this.OpenLibs();
        m_kLuaState.LuaSetTop(0);        
        LuaBinder.Bind(m_kLuaState);
    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        InitLuaPath();
        this.m_kLuaState.Start();    //启动LUAVM

        m_kLuaState.DoFile("Main");
        LuaFunction main = m_kLuaState.GetFunction("Main");
#if UNITY_EDITOR
        main.Call("58f7035db4613d0b01e93770");
#else
        main.Call("59393f425635a36acdb3d1b9");
#endif
        main.Dispose();
        main = null;

        mUpdateFunc = m_kLuaState.GetFunction("Update");
    }

    void Update()
    {
        mUpdateFunc.Call();
    }

    /// <summary>
    /// 初始化加载第三方库
    /// </summary>
    void OpenLibs()
    {
        m_kLuaState.OpenLibs(LuaDLL.luaopen_pb);
        m_kLuaState.OpenLibs(LuaDLL.luaopen_sproto_core);
        m_kLuaState.OpenLibs(LuaDLL.luaopen_protobuf_c);
        m_kLuaState.OpenLibs(LuaDLL.luaopen_lpeg);
        m_kLuaState.OpenLibs(LuaDLL.luaopen_bit);
        m_kLuaState.OpenLibs(LuaDLL.luaopen_socket_core);

        // this.OpenCJson();
    }

    /// <summary>
    /// 初始化Lua代码加载路径
    /// </summary>
    void InitLuaPath()
    {
        #if UNITY_EDITOR
        m_kLuaState.AddSearchPath(LuaConst.LuaDir);
        #endif 
        // if (AppConst.DebugMode)
        // {
        //     string rootPath = AppConst.FrameworkRoot;
        //     lua.AddSearchPath(rootPath + "/Lua");
        // }
        // else
        // {
        //     lua.AddSearchPath(Util.DataPath + "lua");
        // }
    }

    public object[] DoFile(string filename)
    {
        return m_kLuaState.DoFile(filename);
    }

    // Update is called once per frame
    public object[] CallFunction(string funcName, params object[] args)
    {
        LuaFunction func = m_kLuaState.GetFunction(funcName);
        if (func != null)
        {
            return func.Call(args);
        }
        return null;
    }

    public void LuaGC()
    {
        m_kLuaState.LuaGC(LuaGCOptions.LUA_GCCOLLECT);
    }

    public void Close()
    {
        // loop.Destroy();
        // loop = null;
        mUpdateFunc.Dispose();
        mUpdateFunc = null;

        m_kLuaState.Dispose();
        m_kLuaState = null;
    }
}