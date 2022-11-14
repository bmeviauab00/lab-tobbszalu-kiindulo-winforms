using System.Collections.Generic;
using System.Threading;

namespace SuperCalculator.Data;

public class DataFifo
{
    private readonly List<double[]> _innerList = new List<double[]>();
    private object _syncRoot = new object();
    private ManualResetEvent _hasData = new ManualResetEvent(false);

    public void Put(double[] data)
    {
        lock (_syncRoot)
        {
            _innerList.Add(data);
            _hasData.Set();
        }
    }

    public bool TryGet(out double[] data)
    {
        if (_hasData.WaitOne())
        {
            lock (_syncRoot)
            {
                if (_innerList.Count > 0)
                {
                    data = _innerList[0];
                    _innerList.RemoveAt(0);
                    if (_innerList.Count == 0)
                    {
                        _hasData.Reset();
                    }

                    return true;  
                }
                else
                {
                    data = null;
                    return false;
                }
            }
        }
        else
        {
            data = null;
            return false;
        } 
    }
}
