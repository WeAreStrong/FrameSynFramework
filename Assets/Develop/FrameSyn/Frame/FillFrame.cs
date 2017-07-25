namespace FrameSyn
{
    /// <summary>
    /// 填充帧 在逻辑帧获取不到服务器同步消息时使用
    /// </summary>
    public class FillFrame : Frame
    {
        public FillFrame()
        {

        }

        public override FrameType frameType
        {
            get { return FrameType.Fill; }
        }

        public override void Process()
        {
            MainGame.mFillFramePool.Recycle(this);
        }
    }
}