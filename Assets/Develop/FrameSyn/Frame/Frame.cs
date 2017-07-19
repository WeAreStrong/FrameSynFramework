namespace FrameSyn
{
    public enum FrameType
    {
        Fill,
        Key,
    }

    public class Frame : IRecycleable
    {
        protected int mFrameID;

        public int frameID
        {
            get { return mFrameID; }
            set { mFrameID = value; }
        }

        public virtual FrameType frameType
        {
            get { throw new System.NotImplementedException(); }
        }

        public virtual void Process() { }

        public virtual void OnRecycle() { }

        public virtual void OnReuse() { }
    }
}