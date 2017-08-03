using UnityEngine;
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

        luaMgr.CallFunction("SetDemo1", "58f7035db4613d0b01e93770");    //59393f425635a36acdb3d1b9
    }

    void OnDestroy()
    {
        ClientSocket.Dispose();
        instance = null;
    }
}