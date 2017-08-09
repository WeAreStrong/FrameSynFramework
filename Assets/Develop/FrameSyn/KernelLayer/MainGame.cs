namespace FrameSyn
{
    public class MainGame
    {
        public static Pool<KeyFrame> mKeyFramePool;
        public static Pool<FillFrame> mFillFramePool;
        public static FrameList mFrameList;
        public static MainLoop mLogicLoop;
        public static MainLoop mShowLoop;
        public static LuaManager mLuaMgr;
        public static FrameSynMgr mFrameSynMgr;

        public static void Init()
        {
            mKeyFramePool = new Pool<KeyFrame>();
            mFillFramePool = new Pool<FillFrame>();

            KeyFrame[] keyFrames = new KeyFrame[10];
            for (int i = 0; i < keyFrames.Length; ++i)
            {
                keyFrames[i] = new KeyFrame();
            }
            mKeyFramePool.AddRange(keyFrames);

            FillFrame[] fillFrames = new FillFrame[10];
            for (int i = 0; i < fillFrames.Length; ++i)
            {
                fillFrames[i] = new FillFrame();
            }
            mFillFramePool.AddRange(fillFrames);

            mFrameList = new FrameList();
        }

        public static void Release()
        {
            mKeyFramePool.Release();
            mFillFramePool.Release();
            mFrameList.Clear();
            mLogicLoop.Clear();
            mShowLoop.Clear();
            mLuaMgr = null;
            mFrameSynMgr = null;
        }
    }
 }