namespace FrameSyn
{
    public class ControllerData : OperationData , IRecycleable
    {
        public static Pool<ControllerData> mPool = new Pool<ControllerData>();

        public float h;
        public float v;
        public bool jump;
        public bool pressT;

        public override void OnProcess()
        {
            MainGame.mShowLoop.updates.Add(OnUpdate);
        }

        void OnUpdate()
        {
            DemoStart2.instance.ballController.OnSyncUpdate(h, v, jump, pressT);
            MainGame.mShowLoop.updates.PrepareForRemove(OnUpdate);
            mPool.Recycle(this);
        }

        public void OnRecycle()
        {
            h = 0f;
            v = 0f;
            jump = false;
            pressT = false;
        }

        public void OnReuse()
        {

        }
    }
}