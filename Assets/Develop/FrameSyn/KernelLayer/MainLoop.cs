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

            mPassedTime = frequency;
        }

        public void Update()
        {
            mPassedTime -= (int)(UnityEngine.Time.deltaTime * 1000);

            while (mPassedTime <= 0)
            {
                mPassedTime += frequency;
                mUpdates.Run();
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
            mUpdates.Clear();
            mLateUpdates.Clear();
        }
    }
}