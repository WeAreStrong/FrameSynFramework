using UnityEngine;
using LuaInterface;

public class DemoStart : MonoBehaviour
{
    public static DemoStart instance;

    public LuaManager luaMgr;

    public Transform moveTarget;

	// Use this for initialization
	void Start()
    {
        instance = this;
        luaMgr = gameObject.AddComponent<LuaManager>();
        Move.mTarget = moveTarget;
        Move.to = moveTarget.position;

        gameObject.AddComponent<FrameSynMgr>();
	}

    void Destroy()
    {
        instance = null;
    }
}