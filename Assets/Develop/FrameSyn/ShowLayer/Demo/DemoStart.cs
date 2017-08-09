using UnityEngine;
using FrameSyn;
using System.Collections;

public class DemoStart : MonoBehaviour
{
    public static DemoStart instance;
    
    public Transform moveTarget;

	// Use this for initialization
	IEnumerator Start()
    {
        instance = this;
        MainGame.mLuaMgr = gameObject.AddComponent<LuaManager>();
        MainGame.mFrameSynMgr = gameObject.AddComponent<FrameSynMgr>();

        yield return null;

        MainGame.mLuaMgr.CallFunction("SetDemo1", Settings.USER2);
    }

    void OnDestroy()
    {
        ClientSocket.Dispose();
        instance = null;
    }
}