namespace FrameSyn
{
    using System;
    using System.Collections.Generic;

    public class WaitForCalls
    {
        private List<Action> mCallbacks = new List<Action>();
        private List<Action> mPrepareForRemove = new List<Action>();

        public void Add(Action action)
        {
            mCallbacks.Add(action);
        }

        public void Remove(Action action)
        {
            mCallbacks.Remove(action);
        }

        public void PrepareForRemove(Action action)
        {
            mPrepareForRemove.Add(action);
        }

        public void Run()
        {
            for (int i = 0; i < mCallbacks.Count; ++i)
            {
                mCallbacks[i]();
            }

            for (int i = 0; i < mPrepareForRemove.Count; ++i)
            {
                mCallbacks.Remove(mPrepareForRemove[i]);
            }
        }

        public void Clear()
        {
            mCallbacks.Clear();
            mPrepareForRemove.Clear();
        }
    }
}