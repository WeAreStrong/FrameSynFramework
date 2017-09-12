using UnityEngine;
using FrameSyn; 
using System.Collections;

public class DemoStart2 : MonoBehaviour
{
    public static DemoStart2 instance;
    
    public BallController ballController;

    public CollectInput input;

    public GameObject followGO;

    public GameObject cam;

    public GameObject ball;

    public GameObject battleView;

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

        //MainGame.mShowLoop.updates.Add(follow.OnUpdate);
        input = new CollectInput();

        MainGame.mShowLoop.updates.Add(input.Collect);
        MainGame.mLuaMgr.CallFunction("SetDemo2", Settings.USER1, battleView);
    }

    void OnDestroy()
    {
        ClientSocket.Dispose();
        instance = null;
    }
}