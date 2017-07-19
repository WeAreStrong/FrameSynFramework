using UnityEngine;
using FrameSyn;

public class FrameSynMgr : MonoBehaviour
{
    private MainLoop mLogicLoop;
    private MainLoop mShowLoop;
    private bool mBegin = false;

	// Use this for initialization
	void Start ()
    {
        mLogicLoop = new MainLoop(Settings.KernelUpdateCycle);
        mShowLoop = new MainLoop(Settings.ShowUpdateCycle);

        RealTime.Reset();
	}
	
	// Update is called once per frame
	void Update()
    {
        if (mBegin == false) return;

        RealTime.OnUpdate();

        mLogicLoop.Update();
        mShowLoop.Update();
	}

    void LateUpdate()
    {
        if (mBegin == false) return;

        mLogicLoop.LateUpdate();
        mShowLoop.LateUpdate();
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