namespace FrameSyn.Physics
{
    using UnityEngine;

    public class PhysicsEngine
    {
        public static void Init()
        {
            Physics.autoSimulation = false;
        }

        public static void OnUpdate()
        {
            Physics.Simulate(1f / Settings.ShowUpdateCycle);
        }

        public static void Release()
        {
            Physics.autoSimulation = false;
        }
    }
}