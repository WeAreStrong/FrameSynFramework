namespace FrameSyn
{
    public sealed class RealTime
    {
        private static int mFrameCount = 0;

        public static int frameCount
        {
            get { return mFrameCount; }
        }

        public static int timePassed
        {
            get { return mFrameCount * deltaTime; }
        }

        /// <summary>
        /// Millisecond
        /// </summary>
        public static int deltaTime
        {
            get { return 1000 / Settings.KernelUpdateCycle; }
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