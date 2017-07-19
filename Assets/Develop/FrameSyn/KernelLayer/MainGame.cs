namespace FrameSyn
{
    public class MainGame
    {
        public static Pool<KeyFrame> mKeyFramePool;
        public static Pool<FillFrame> mFillFramePool;

        public MainGame()
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
        }
    }
 }