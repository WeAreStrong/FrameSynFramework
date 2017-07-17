namespace FrameSyn
{
    public class MainLoop
    {
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

        public MainLoop()
        {
            mUpdates = new WaitForCalls();
            mLateUpdates = new WaitForCalls();
        }

        public void Update()
        {
            mUpdates.Run();
        }

        public void LateUpdate()
        {
            mLateUpdates.Run();
        }
    }
}