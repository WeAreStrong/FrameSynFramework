using UnityEngine;
using FrameSyn;

public class FrameSynMgr : MonoBehaviour
{
    private bool mBegin = false;

	// Use this for initialization
	void Start ()
    {
        MainGame.Init();

        MainGame.mLogicLoop = new MainLoop(Settings.KernelUpdateCycle);
        MainGame.mShowLoop = new MainLoop(Settings.ShowUpdateCycle);

        MainGame.mLogicLoop.updates.Add(RealTime.OnUpdate);
        //------------ Add logic updates
        MainGame.mLogicLoop.updates.Add(MainGame.mFrameList.Update);
        //------------ Add show updates

        RealTime.Reset();

        Begin();
	}
	
	// Update is called once per frame
	void Update()
    {
        if (mBegin == false) return;

        int rate = MainGame.mFrameList.speedupRate;
        for (int i = 1; i <= rate; ++i)
        {
            MainGame.mLogicLoop.Update();
        }
        
        MainGame.mShowLoop.Update();
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