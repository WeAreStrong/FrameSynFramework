namespace FrameSyn
{
    /// <summary>
    /// 关键帧
    /// </summary>
    public class KeyFrame : Frame
    {
        public OperationData[] mOpaData;

        public KeyFrame()
        {

        }

        public override FrameType frameType
        {
            get { return FrameType.Key; }
        }

        public override void Process()
        {
            for (int i = 0; i < mOpaData.Length; ++i)
            {
                mOpaData[i].OnProcess();
            }

            MainGame.mKeyFramePool.Recycle(this);
        }

        public override void OnRecycle()
        {
            base.OnRecycle();
            mOpaData = null;
        }
    }
}