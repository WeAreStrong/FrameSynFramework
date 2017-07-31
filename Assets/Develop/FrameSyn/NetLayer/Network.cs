namespace FrameSyn.Network
{
    public class Network
    {
        public static void OnOperationClick(int frameId, int x, int y)
        {
            OperationData_Click opaData = new OperationData_Click();
            opaData.x = x;
            opaData.y = y;

            KeyFrame keyFrame = MainGame.mKeyFramePool.Get();
            keyFrame.mOpaData = opaData;
            MainGame.mFrameList.AddFrame(frameId, keyFrame);
            MainGame.mFrameList.LockFrame();
        }

        public static void OnFrameStep(int frameId)
        {
            FillFrame frame = MainGame.mFillFramePool.Get();
            MainGame.mFrameList.AddFrame(frameId, frame);
            MainGame.mFrameList.LockFrame();
        }
    }
}