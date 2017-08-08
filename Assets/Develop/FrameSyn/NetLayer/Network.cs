namespace FrameSyn.Network
{
    public class Network
    {
        public static void OnFrameStep(int frameId, int type, int[] fids, UnityEngine.Vector2[] clicks)
        {
            if (frameId == 0)
            {
                DemoStart.instance.frameSynMgr.Begin();
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
                for (int i = 0; i < clicks.Length; ++i)
                {
                    OperationData_Click opaData = new OperationData_Click();
                    opaData.frameID = fids[i];
                    opaData.pos = clicks[i];
                    keyFrame.mOpaData.Add(opaData);
                }

                MainGame.mFrameList.AddFrame(frameId, keyFrame);
            }
            MainGame.mFrameList.LockFrame();
        }

        public static void OnFrameStep(int frameId, int type, int[] fids, float[] hs, float[] vs, bool[] jumps, bool[] pressTs)
        {
            if (frameId == 0)
            {
                DemoStart2.instance.frameSynMgr.Begin();
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
                for (int i = 0; i < fids.Length; ++i)
                {
                    ControllerData opaData = ControllerData.mPool.Get();
                    opaData.frameID = fids[i];
                    //opaData.h = hs[i];
                    //opaData.v = vs[i];
                    //opaData.jump = jumps[i];
                    opaData.pressT = pressTs[i];
                    keyFrame.mOpaData.Add(opaData);
                }

                MainGame.mFrameList.AddFrame(frameId, keyFrame);
            }
            MainGame.mFrameList.LockFrame();
        }
    }
}