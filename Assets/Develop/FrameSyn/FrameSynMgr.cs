using UnityEngine;
using FrameSyn;

public class FrameSynMgr : MonoBehaviour
{
    private int mFramesWaitForRun = 0;

    private MainLoop mMainLoop;

	// Use this for initialization
	void Start ()
    {
        Application.targetFrameRate = 30;

        mMainLoop = new MainLoop();
        RealTime.Reset();
	}
	
	// Update is called once per frame
	void Update()
    {
        if (mFramesWaitForRun <= 0)
        {
            return;
        }
        --mFramesWaitForRun;

        RealTime.OnUpdate();

        mMainLoop.Update();
	}

    void LateUpdate()
    {
        mMainLoop.LateUpdate();
    }
}