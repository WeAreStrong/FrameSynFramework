﻿using UnityEngine;
using FrameSyn;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    public static BallController instance;
    public bool m_UseTorque = true;
    public float m_MovePower = 5;
    public float m_JumpPower = 2; // The force added to the ball when it jumps.

    private const float k_GroundRayLength = 1f; // The length of the ray to check if the ball is grounded.

    public Vector3 move;
    // the world-relative desired move direction, calculated from the camForward and user input.

    private Transform cam; // A reference to the main camera in the scenes transform
    private Vector3 camForward; // The current forward direction of the camera
    //private bool mJump; // whether the jump button is currently pressed

    private Rigidbody mBall;

    private void Awake()
    {
        instance = this;
        // get the transform of the main camera
        if (Camera.main != null)
        {
            cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Ball needs a Camera tagged \"MainCamera\", for camera-relative controls.");
            // we use world-relative controls in this case, which may not be what the user wants, but hey, we warned them!
        }
    }

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
    }

    void OnShowUpdate()
    {
        // Get the axis and jump input.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool jump = Input.GetButton("Jump");
        bool pressT = Input.GetKey(KeyCode.T);
        //OnSyncUpdate(h, v, jump, pressT);
        DemoStart2.instance.luaMgr.CallFunction("Demo2.Control", h, v, jump, pressT, RealTime.frameCount);
    }

    public void OnSyncUpdate(float h, float v, bool jump, bool pressT)
    {
        // calculate move direction
        if (cam != null)
        {
            // calculate camera relative direction to move:
            camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
            move = (v * camForward + h * cam.right).normalized;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            move = (v * Vector3.forward + h * Vector3.right).normalized;
        }

        // If using torque to rotate the ball...
        if (m_UseTorque)
        {
            // ... add torque around the axis defined by the move direction.
            mBall.AddTorque(new Vector3(move.z, 0, -move.x) * m_MovePower);
        }
        else
        {
            // Otherwise add force in the move direction.
            mBall.AddForce(move * m_MovePower);
        }

        // If on the ground and jump is pressed...
        if (Physics.Raycast(transform.position, -Vector3.up, k_GroundRayLength) && jump)
        {
            // ... add force in upwards.
            mBall.AddForce(Vector3.up * m_JumpPower, ForceMode.Impulse);
        }
        jump = false;

        if (pressT)
        {
            mBall.AddForce(camForward * 100);
        }

        Physics.Simulate(1f / Settings.ShowUpdateCycle);
    }

    void OnDestroy()
    {
        instance = null;
    }
}