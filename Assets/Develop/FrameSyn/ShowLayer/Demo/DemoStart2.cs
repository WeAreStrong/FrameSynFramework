using UnityEngine;
using FrameSyn; 
using System.Collections;

public class DemoStart2 : MonoBehaviour
{
    public static DemoStart2 instance;
    
    public BallController ballController;

    public GameObject followGO;

    public GameObject cam;

    public GameObject ball;

    // Use this for initialization
    IEnumerator Start()
    {
        instance = this;
        MainGame.mLuaMgr = gameObject.AddComponent<LuaManager>();
        MainGame.mFrameSynMgr = gameObject.AddComponent<FrameSynMgr>();
        ballController = ball.AddComponent<BallController>();

        Follow follow = followGO.AddComponent<Follow>();
        follow.mTarget = ball.transform;
        yield return null;

        MainGame.mShowLoop.updates.Add(follow.OnUpdate);

        MainGame.mLuaMgr.CallFunction("SetDemo2", Settings.USER1);
    }

    void OnDestroy()
    {
        ClientSocket.Dispose();
        instance = null;
    }
}