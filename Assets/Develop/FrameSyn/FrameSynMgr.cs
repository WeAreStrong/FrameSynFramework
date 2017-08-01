﻿using UnityEngine;
using FrameSyn;

public class FrameSynMgr : MonoBehaviour
{
    public static FrameSynMgr instance;

    private bool mBegin = false;

	// Use this for initialization
	void Start ()
    {
        instance = this;

        MainGame.Init();

        MainGame.mLogicLoop = new MainLoop(Settings.KernelUpdateCycle);
        MainGame.mShowLoop = new MainLoop(Settings.ShowUpdateCycle);

        //------------ Add logic updates
        MainGame.mLogicLoop.updates.Add(RealTime.OnUpdate);
        MainGame.mLogicLoop.updates.Add(MainGame.mFrameList.Update);
        //------------ Add show updates

        RealTime.Reset();
	}
	
	// Update is called once per frame
	void Update()
    {
        if (mBegin == false) return;

        int rate1 = MainGame.mFrameList.speedupRate;
        for (int i = 1; i <= rate1; ++i)
        {
            MainGame.mLogicLoop.Update();
        }

        if (RealTime.frameCount <= MainGame.mFrameList.lockFrameID)
        {
            MainGame.mShowLoop.Update();
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

    void OnDestroy()
    {
        instance = null;
    }
}