namespace FrameSyn
{
    public class Settings
    {
        public const string USER1 = "58f7035db4613d0b01e93770";

        public const string USER2 = "59393f425635a36acdb3d1b9";

        public const int KernelUpdateCycle = 5;     //Frame count per second

        public const int ShowUpdateCycle = 30;      //Frame count per second

        public static int PeriodicShowUpdateTimes()
        {
            return ShowUpdateCycle / KernelUpdateCycle;
        }

        public const int ServerFrameStep = 1;

        public const int MAX_SPEEDUP_RATE = 3;
    }
}