namespace FrameSyn
{
    using UnityEngine;

    public class CollectInput
    {
        public void Collect()
        {
            // Get the axis and jump input.
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            bool jump = Input.GetButton("Jump");
            bool pressT = Input.GetKey(KeyCode.T);
            //OnSyncUpdate(h, v, jump, pressT);
            if (h * v != 0 || jump || pressT)
            {
                MainGame.mLuaMgr.CallFunction("Demo2.Control", h, v, jump, pressT, RealTime.frameCount);
            }
        }
    }
}