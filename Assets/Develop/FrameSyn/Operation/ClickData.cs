namespace FrameSyn
{
    public class OperationData_Click : OperationData
    {
        public int x = 0;
        public int y = 0;

        private static Move move = null;

        static OperationData_Click()
        {
            move = new Move();
            move.mTarget = DemoStart.instance.moveTarget;
            MainGame.mShowLoop.updates.Add(move.OnUpdate);
        }

        public override void OnProcess()
        {
            move.to = new UnityEngine.Vector3(x, y);
        }
    }
}