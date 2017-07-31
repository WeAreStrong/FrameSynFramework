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

        yield return null;

        luaMgr.DoFile("Room");
        luaMgr.CallFunction("Room.Ready");

        gameObject.AddComponent<FrameSynMgr>();
	}

    void Destroy()
    {
        instance = null;
    }
}