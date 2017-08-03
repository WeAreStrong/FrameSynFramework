using UnityEngine;
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

    void OnDestroy()
    {
        ClientSocket.Dispose();
        instance = null;
    }
}