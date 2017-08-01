namespace FrameSyn.Network
{
    public class Network
    {
        public static void OnFrameStep(int frameId, int type, UnityEngine.Vector2[] clicks)
        {
            if (frameId == 0)
            {
                FrameSynMgr.instance.Begin();
            }

            FrameType fType = (FrameType)type;
            if (fType == FrameType.Fill)
            {
                FillFrame frame = MainGame.mFillFramePool.Get();
                MainGame.mFrameList.AddFrame(frameId, frame);
            }
            else if (fType == FrameType.Key)
            {
                KeyFrame keyFrame = MainGame.mKeyFramePool.Get();
                keyFrame.mOpaData = new OperationData_Click[clicks.Length];
                for (int i = 0; i < clicks.Length; ++i)
                {
                    OperationData_Click opaData = new OperationData_Click();
                    opaData.pos = clicks[i];
                    keyFrame.mOpaData[i] = opaData;
                }

                MainGame.mFrameList.AddFrame(frameId, keyFrame);
            }
            MainGame.mFrameList.LockFrame();
        }
    }
}