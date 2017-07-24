﻿using UnityEngine;
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
        //------------ Add show updates

        RealTime.Reset();
	}
	
	// Update is called once per frame
	void Update()
    {
        if (mBegin == false) return;

        for (int i = 1; i <= MainGame.mFrameList.speedupRate; ++i)
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

    public void Begin()
    {
        mBegin = true;
    }

    public void End()
    {
        mBegin = false;
    }
}