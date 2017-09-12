using UnityEngine;
using TrueSync;

[RequireComponent(typeof(TSRigidBody))]
public class BallController : MonoBehaviour
{
    public bool m_UseTorque = true;
    public FP m_MovePower = 5;
    public FP m_JumpPower = 2; // The force added to the ball when it jumps.

    private const float k_GroundRayLength = 1; // The length of the ray to check if the ball is grounded.

    public TSVector move;
    // the world-relative desired move direction, calculated from the camForward and user input.

    private Transform cam; // A reference to the main camera in the scenes transform
    private TSVector camForward; // The current forward direction of the camera
    //private bool mJump; // whether the jump button is currently pressed

    private TSRigidBody mBall;

    private void Awake()
    {
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
        mBall = gameObject.GetComponent<TSRigidBody>();
	}

    public void OnSyncUpdate(FP h, FP v, bool jump, bool pressT)
    {
        // calculate move direction
        if (cam != null)
        {
            // calculate camera relative direction to move:
            camForward = TSVector.Scale(cam.forward.ToTSVector(), new TSVector(1, 0, 1)).normalized;
            move = (v * camForward + h * cam.right.ToTSVector()).normalized;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            move = (v * TSVector.forward + h * TSVector.right).normalized;
        }

        // If using torque to rotate the ball...
        if (m_UseTorque)
        {
            // ... add torque around the axis defined by the move direction.
            mBall.AddTorque(new TSVector(move.z, 0, -move.x) * m_MovePower);
        }
        else
        {
            // Otherwise add force in the move direction.
            mBall.AddForce(move * m_MovePower);
        }

        // If on the ground and jump is pressed...
        if (Physics.Raycast(mBall.tsTransform.position.ToVector(), -Vector3.up, k_GroundRayLength) && jump)
        {
            // ... add force in upwards.
            mBall.AddForce(TSVector.up * m_JumpPower, ForceMode.Impulse);
        }
        jump = false;

        if (pressT)
        {
            mBall.AddForce(camForward * 100);
        }
    }
}