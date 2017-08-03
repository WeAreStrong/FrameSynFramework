using UnityEngine;
using FrameSyn;

public class Move
{
    public Transform mTarget;
    public Vector3 to;

    public const int speed = 30;

    public void OnUpdate()
    {
        Vector3 dir = to - mTarget.position;
        if (dir.magnitude > 0.01)
        {
            dir.Normalize();
            mTarget.position += dir * RealTime.deltaTime * speed / 1000f;
        }
        else
        {
            mTarget.position = to;
        }
    }
}