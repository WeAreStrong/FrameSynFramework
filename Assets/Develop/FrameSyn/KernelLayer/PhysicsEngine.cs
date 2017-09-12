namespace FrameSyn.Physics
{
    using TrueSync;

    public class PhysicsEngine
    {
        public static void Init()
        {
            TrueSyncManager.Init();
        }

        public static void OnUpdate()
        {
            PhysicsManager.instance.UpdateStep();
        }

        public static void Release()
        {
            TrueSyncManager.CleanUp();
        }
    }
}