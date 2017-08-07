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
        private Frame mCurrentFrame = null;

        private Queue<Frame> mQueue = new Queue<Frame>();

        public void AddFrame(int frameID, Frame frame)
        {
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

        public void LogicUpdate()
        {
            if (mQueue.Count > 0)
            {
                mCurrentFrame = mQueue.Dequeue();
            }
            else
            {
                mCurrentFrame = null;
#if UNITY_EDITOR
                UnityEngine.Debug.Assert(false);
#endif
            }
        }

        public void ShowUpdate()
        {
            mCurrentFrame.Process();
        }

        public int speedupRate
        {
            get
            {
                int count = mQueue.Count;
                if (count >= Settings.MAX_SPEEDUP_RATE)
                {
                    return Settings.MAX_SPEEDUP_RATE;
                }
                else if (count == 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public void Clear()
        {
            mFrameIdx = 0;
            mLockFrameID = 0;
            mQueue.Clear();
        }
    }
}