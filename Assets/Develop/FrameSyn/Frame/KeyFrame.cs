namespace FrameSyn
{
    /// <summary>
    /// 关键帧
    /// </summary>
    public class KeyFrame : Frame
    {
        public KeyFrame()
        {

        }

        public override FrameType frameType
        {
            get { return FrameType.Key; }
        }

        public override void Process()
        {

        }
    }
}