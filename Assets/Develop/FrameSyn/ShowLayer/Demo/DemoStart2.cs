using UnityEngine;
using FrameSyn; 
using System.Collections;

public class DemoStart2 : MonoBehaviour
{
    public static DemoStart2 instance;

    public LuaManager luaMgr;

    public FrameSynMgr frameSynMgr;

    public BallController ballController;

    public GameObject followGO;

    public GameObject cam;

    public GameObject ball;

    // Use this for initialization
    IEnumerator Start()
    {
        instance = this;
        luaMgr = gameObject.AddComponent<LuaManager>();
        frameSynMgr = gameObject.AddComponent<FrameSynMgr>();
        ballController = ball.AddComponent<BallController>();

        Follow follow = followGO.AddComponent<Follow>();
        follow.mTarget = ball.transform;
        yield return null;

        MainGame.mShowLoop.updates.Add(follow.OnUpdate);

        luaMgr.CallFunction("SetDemo2", Settings.USER1);
    }

    void OnDestroy()
    {
        ClientSocket.Dispose();
        instance = null;
    }
}