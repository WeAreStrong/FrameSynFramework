using UnityEngine;
using FrameSyn; 
using System.Collections;

public class DemoStart2 : MonoBehaviour
{
    public static DemoStart2 instance;

    public LuaManager luaMgr;

    public GameObject followGO;

    public GameObject cam;

    public GameObject ball;

    // Use this for initialization
    IEnumerator Start()
    {
        instance = this;
        luaMgr = gameObject.AddComponent<LuaManager>();

        gameObject.AddComponent<FrameSynMgr>();
        ball.AddComponent<BallController>();

        Follow follow = followGO.AddComponent<Follow>();
        follow.mTarget = ball.transform;
        yield return null;

        luaMgr.DoFile("Room");
        luaMgr.CallFunction("Room.Ready");

        MainGame.mShowLoop.updates.Add(follow.OnUpdate);
    }

    void OnDestroy()
    {
        ClientSocket.Dispose();
        instance = null;
    }
}