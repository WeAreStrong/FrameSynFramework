using UnityEngine;
using FrameSyn;

public class Follow : MonoBehaviour
{
    public Transform mTarget;

    public void OnUpdate()
    {
        transform.position = mTarget.position;
    }
}