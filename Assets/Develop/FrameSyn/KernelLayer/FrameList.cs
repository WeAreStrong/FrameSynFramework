namespace FrameSyn
{
    using System.Collections.Generic;

    public class FrameList
    {
        private int mLockFrameID = 0;

        public int lockFrameID
        {
            get { return mLockFrameID; }
        }

        private int mFrameIdx = 0;

        private Queue<Frame> mQueue = new Queue<Frame>();

        public void AddFrame(int frameID, FrameType frameType)
        {
            Frame frame = null;

            switch (frameType)
            {
                case FrameType.Key:
                    frame = MainGame.mKeyFramePool.Get();
                    break;
                case FrameType.Fill:
                    frame = MainGame.mFillFramePool.Get();
                    break;
                default:

                    break;
            }

            if (frame != null)
            {
                frame.frameID = frameID + mFrameIdx * Settings.ServerFrameStep;
                mQueue.Enqueue(frame);
            }
        }

        public void LockFrame()
        {
            ++mFrameIdx;
            mLockFrameID = mFrameIdx * Settings.ServerFrameStep;
        }
    }
}