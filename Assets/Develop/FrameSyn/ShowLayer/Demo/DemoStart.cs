using UnityEngine;
using FrameSyn;
using System.Collections;

public class DemoStart : MonoBehaviour
{
    public static DemoStart instance;

    public LuaManager luaMgr;

    public FrameSynMgr frameSynMgr;

    public Transform moveTarget;

	// Use this for initialization
	IEnumerator Start()
    {
        instance = this;
        luaMgr = gameObject.AddComponent<LuaManager>();
        frameSynMgr = gameObject.AddComponent<FrameSynMgr>();

        yield return null;

        luaMgr.CallFunction("SetDemo1", Settings.USER2);
    }

    void OnDestroy()
    {
        ClientSocket.Dispose();
        instance = null;
    }
}