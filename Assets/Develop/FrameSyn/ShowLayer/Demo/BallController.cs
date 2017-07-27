using UnityEngine;
using FrameSyn;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    private Rigidbody mBall;

	// Use this for initialization
	void Start ()
    {
        mBall = gameObject.GetComponent<Rigidbody>();

        MainGame.mLogicLoop.updates.Add(OnUpdate);
        MainGame.mShowLoop.updates.Add(OnShowUpdate);
        
        Physics.autoSimulation = false;
	}

    void OnUpdate()
    {
        //DealInput
        if (Input.GetKey(KeyCode.A))
        {
            mBall.AddForce(mBall.transform.forward * 10, ForceMode.Impulse);
        }
    }

    void OnShowUpdate()
    {
        Physics.Simulate(1f / Settings.ShowUpdateCycle);
    }
}