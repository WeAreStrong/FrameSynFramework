using System.Collections.Generic;

public class PacketSequence
{
    private Queue<byte[]> mWaitForSendBuffer = new Queue<byte[]>();

    public byte[] First
    {
        get
        {
            if (mWaitForSendBuffer.Count > 0)
            {
                return mWaitForSendBuffer.Dequeue();
            }
            else
            {
                return null;
            }
        }
    }
}