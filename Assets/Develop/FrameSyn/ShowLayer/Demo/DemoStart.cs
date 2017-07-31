using UnityEngine;
using LuaInterface;
using FrameSyn;
using System.Collections;

public class DemoStart : MonoBehaviour
{
    public static DemoStart instance;

    public LuaManager luaMgr;

    public Transform moveTarget;

	// Use this for initialization
	IEnumerator Start()
    {
        instance = this;
        luaMgr = gameObject.AddComponent<LuaManager>();

        gameObject.AddComponent<FrameSynMgr>();

        yield return null;

        luaMgr.DoFile("Room");
        luaMgr.CallFunction("Room.Ready");
	}

    void Destroy()
    {
        instance = null;
    }
}