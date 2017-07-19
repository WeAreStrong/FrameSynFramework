using System.Collections.Generic;
using FrameSyn;

public class Pool<T> where T : IRecycleable, new()
{
    private List<T> mUsing;
    private List<T> mCaches;

    public Pool()
    {
        mUsing = new List<T>();
        mCaches = new List<T>();
    }

    public T Get()
    {
        if (mCaches.Count > 0)
        {
            T cache = mCaches[0];
            mCaches.RemoveAt(0);
            mUsing.Add(cache);
            cache.OnReuse();

            return cache;
        }
        else
        {
            T cache = new T();
            mUsing.Add(cache);
            cache.OnReuse();
            return cache;
        }
    }

    public void AddRange(IEnumerable<T> caches)
    {
        IEnumerator<T> iter = caches.GetEnumerator();
        while (iter.MoveNext())
        {
            mCaches.Add(iter.Current);
        }

        iter.Reset();
        iter.Dispose();
    }

    public void Recycle(T t)
    {
        t.OnRecycle();
        mUsing.Remove(t);
        mCaches.Add(t);
    }
}