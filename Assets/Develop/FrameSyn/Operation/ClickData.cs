namespace FrameSyn
{
    public class OperationData_Click : OperationData
    {
        public int x = 0;
        public int y = 0;

        public override void OnProcess()
        {
            Move.to = new UnityEngine.Vector3(x, y);
        }
    }
}