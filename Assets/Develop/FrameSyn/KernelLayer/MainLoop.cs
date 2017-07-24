namespace FrameSyn
{
    public class MainLoop
    {
        private int mUpdateCycle = 1;

        protected int frequency
        {
            get { return 1000 / mUpdateCycle; }
        }

        private int mPassedTime = 0;

        private int mFrameNr = 0;
        public int frameNr
        {
            get { return mFrameNr; }
        }

        private WaitForCalls mUpdates;
        private WaitForCalls mLateUpdates;

        public WaitForCalls updates
        {
            get { return mUpdates; }
        }

        public WaitForCalls lateUpdates
        {
            get { return mLateUpdates; }
        }

        public MainLoop(int updateCycle)
        {
            mUpdates = new WaitForCalls();
            mLateUpdates = new WaitForCalls();

            mUpdateCycle = updateCycle;
        }

        public void Update()
        {
            mPassedTime += (int)(UnityEngine.Time.deltaTime * 1000);

            while (mPassedTime >= frequency)
            {
                mPassedTime -= frequency;
                mUpdates.Run();

                ++mFrameNr;
            }
        }

        public void LateUpdate()
        {
            mLateUpdates.Run();
        }

        public void Clear()
        {
            mUpdateCycle = 1;
            mPassedTime = 0;
            mFrameNr = 0;
            mUpdates.Clear();
            mLateUpdates.Clear();
        }
    }
}