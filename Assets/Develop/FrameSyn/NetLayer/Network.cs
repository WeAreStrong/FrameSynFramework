namespace FrameSyn.Network
{
    public class Network
    {
        private void OnFrameStep(int frameId)
        {
            MainGame.mFrameList.AddFrame(frameId, FrameType.Key);

            MainGame.mFrameList.LockFrame();
        }
    }
}