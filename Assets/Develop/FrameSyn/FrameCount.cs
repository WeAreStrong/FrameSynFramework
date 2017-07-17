namespace FrameSyn
{
    public partial class RealTime
    {
        private static int mFrameCount = 0;

        public static int frameCount
        {
            get { return mFrameCount; }
        }

        public static void OnUpdate()
        {
            ++mFrameCount;
        }

        public static void Reset()
        {
            mFrameCount = 0;
        }
    }
}