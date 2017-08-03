using UnityEngine;
using FrameSyn; 
using System.Collections;

public class DemoStart2 : MonoBehaviour
{
    public static DemoStart2 instance;

    public LuaManager luaMgr;

    public FrameSynMgr frameSynMgr;

    public GameObject followGO;

    public GameObject cam;

    public GameObject ball;

    // Use this for initialization
    IEnumerator Start()
    {
        instance = this;
        luaMgr = gameObject.AddComponent<LuaManager>();
        frameSynMgr = gameObject.AddComponent<FrameSynMgr>();
        ball.AddComponent<BallController>();

        Follow follow = followGO.AddComponent<Follow>();
        follow.mTarget = ball.transform;
        yield return null;

        MainGame.mShowLoop.updates.Add(follow.OnUpdate);

        luaMgr.CallFunction("SetDemo2", "58f7035db4613d0b01e93770");    //59393f425635a36acdb3d1b9
    }

    void OnDestroy()
    {
        ClientSocket.Dispose();
        instance = null;
    }
}