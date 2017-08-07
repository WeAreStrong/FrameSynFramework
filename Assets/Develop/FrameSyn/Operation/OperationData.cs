namespace FrameSyn
{
    public abstract class OperationData
    {
        private int mFrameID = 0;

        public int frameID
        {
            get { return mFrameID; }
            set { mFrameID = value; }
        }

        public abstract void OnProcess();
    }
}