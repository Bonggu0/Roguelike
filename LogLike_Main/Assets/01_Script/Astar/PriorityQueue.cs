using System.Collections.Generic;


public class PriorityQueue<T>
{
    List<(T item, float priority)> data = new List<(T, float)>();


    public int Count => data.Count;


    public void Enqueue(T item, float priority)
    {
        data.Add((item, priority));
        int ci = data.Count - 1;
        while (ci > 0)
        {
            int pi = (ci - 1) / 2;
            if (data[ci].priority >= data[pi].priority) break;
            var tmp = data[ci]; data[ci] = data[pi]; data[pi] = tmp;
            ci = pi;
        }
    }


    public T Dequeue()
    {
        var ret = data[0].item;
        int li = data.Count - 1;
        data[0] = data[li];
        data.RemoveAt(li);
        li--;
        int pi = 0;
        while (true)
        {
            int ci = pi * 2 + 1;
            if (ci > li) break;
            int rc = ci + 1;
            if (rc <= li && data[rc].priority < data[ci].priority) ci = rc;
            if (data[pi].priority <= data[ci].priority) break;
            var tmp = data[pi]; data[pi] = data[ci]; data[ci] = tmp;
            pi = ci;
        }
        return ret;
    }
}