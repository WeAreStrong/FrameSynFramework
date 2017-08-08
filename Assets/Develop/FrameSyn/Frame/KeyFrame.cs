namespace FrameSyn
{
    using System.Collections.Generic;

    /// <summary>
    /// 关键帧
    /// </summary>
    public class KeyFrame : Frame
    {
        public List<OperationData> mOpaData = new List<OperationData>();

        public KeyFrame()
        {

        }

        public override FrameType frameType
        {
            get { return FrameType.Key; }
        }

        public override void Process()
        {
            for (int i = 0; i < mOpaData.Count;)
            {
                if (RealTime.frameCount == mOpaData[i].frameID + Settings.PeriodicShowUpdateTimes())
                {
                    mOpaData[i].OnProcess();
                    mOpaData.RemoveAt(i);
                }
                else
                {
                    ++i;
                }
            }

            if (mOpaData.Count == 0)
            {
                MainGame.mKeyFramePool.Recycle(this);
            }
        }

        public override void OnRecycle()
        {
            base.OnRecycle();
            mOpaData.Clear();
        }
    }
}