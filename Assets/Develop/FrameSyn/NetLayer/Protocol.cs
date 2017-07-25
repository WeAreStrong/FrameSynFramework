namespace FrameSyn.Network
{
    using System.Collections;
    using System.Collections.Generic;

    public class Message : IRecycleable
    {
        private int mSequenceID;
        public int sequenceID
        {
            get { return mSequenceID; }
            set { mSequenceID = value; }
        }

        private byte[] mContent;
        public byte[] content
        {
            get { return mContent; }
            set { mContent = value; }
        }

        public void OnRecycle() { mContent = null; }
        public void OnReuse() { mSequenceID = 0; }
    }

    public class Protocol : IEnumerable
    {
        private Pool<Message> mMsgPool = new Pool<Message>();
        private Queue<Message> mWaitForSendBuffer = new Queue<Message>();
        private int mSequenceID = 0;

        public void ReadyForSend(byte[] content)
        {
            ++mSequenceID;

            Message msg = mMsgPool.Get();
            msg.sequenceID = mSequenceID;
            msg.content = content;

            mWaitForSendBuffer.Enqueue(msg);
        }

        public void RemoveBefore(int sequenceID)
        {
            if (mWaitForSendBuffer.Count > 0)
            {
                if (mWaitForSendBuffer.Peek().sequenceID > sequenceID)
                {
                    return;
                }
            }

            while (true)
            {
                Message msg = mWaitForSendBuffer.Peek();
                if (msg.sequenceID <= sequenceID)
                {
                    Message readyForRecycle = mWaitForSendBuffer.Dequeue();
                    mMsgPool.Recycle(readyForRecycle);
                }

                if (msg.sequenceID == sequenceID)
                {
                    break;
                }
            }
        }

        public IEnumerator GetEnumerator()
        {
            return mWaitForSendBuffer.GetEnumerator();
        }
    }
}