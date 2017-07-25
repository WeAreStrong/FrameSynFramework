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
    }
}