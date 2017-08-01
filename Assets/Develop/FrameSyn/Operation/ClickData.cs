namespace FrameSyn
{
    public class OperationData_Click : OperationData
    {
        public UnityEngine.Vector2 pos;

        private static Move move = null;

        static OperationData_Click()
        {
            move = new Move();
            move.mTarget = DemoStart.instance.moveTarget;
            MainGame.mShowLoop.updates.Add(move.OnUpdate);
        }

        public override void OnProcess()
        {
            move.to = pos;
        }
    }
}