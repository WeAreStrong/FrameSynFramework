using UnityEngine;
using LuaInterface;

public class DemoStart2 : MonoBehaviour
{
    public static DemoStart2 instance;

    public LuaManager luaMgr;

    public GameObject ball;

    // Use this for initialization
    void Start()
    {
        instance = this;
        luaMgr = gameObject.AddComponent<LuaManager>();

        gameObject.AddComponent<FrameSynMgr>();

        ball.AddComponent<BallController>();
    }

    void Destroy()
    {
        instance = null;
    }
}