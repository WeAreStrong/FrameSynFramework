using UnityEngine;
using FrameSyn;
using FrameSyn.Physics;

public class FrameSynMgr : MonoBehaviour
{
    private bool mBegin = false;

	// Use this for initialization
	void Start ()
    {
        MainGame.Init();

        MainGame.mLogicLoop = new MainLoop(Settings.KernelUpdateCycle);
        MainGame.mShowLoop = new MainLoop(Settings.ShowUpdateCycle);

        //------------ Add logic updates
        MainGame.mLogicLoop.updates.Add(MainGame.mFrameList.LogicUpdate);
        //------------ Add show updates
        MainGame.mShowLoop.updates.Add(RealTime.OnUpdate);
        MainGame.mShowLoop.updates.Add(MainGame.mFrameList.ShowUpdate);
        MainGame.mShowLoop.updates.Add(PhysicsEngine.OnUpdate);

        RealTime.Reset();
	}
	
	// Update is called once per frame
	void Update()
    {
        MainGame.mLuaMgr.OnUpdate();
        if (mBegin == false) return;

        if (RealTime.frameCount < MainGame.mFrameList.lockFrameID)
        {
            MainGame.mShowLoop.Update();
            /*int rate1 = MainGame.mFrameList.speedupRate;
            for (int i = 1; i <= rate1; ++i)
            {
                MainGame.mLogicLoop.Update();
            }

            for (int i = 1; i <= rate1; ++i)
            {
                MainGame.mShowLoop.Update();
            }*/
        }
	}

    void LateUpdate()
    {
        if (mBegin == false) return;

        MainGame.mLogicLoop.LateUpdate();
        MainGame.mShowLoop.LateUpdate();
    }

    [ContextMenu("Begin")]
    public void Begin()
    {
        mBegin = true;
    }

    [ContextMenu("End")]
    public void End()
    {
        mBegin = false;
    }
}