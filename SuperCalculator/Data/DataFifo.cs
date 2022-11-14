using System.Collections.Generic;
using System.Threading;

namespace SuperCalculator.Data;

public class DataFifo
{
    private readonly List<double[]> _innerList = new List<double[]>();
    private object _syncRoot = new object();

    public void Put(double[] data)
    {
        lock (_syncRoot)
        {
            _innerList.Add(data); 
        }
    }

    public bool TryGet(out double[] data)
    {
        lock (_syncRoot)
        {
            if (_innerList.Count > 0)
            {
                // Some little artificial delay, which does not affect the FIFO functionality
                Thread.Sleep(500);

                data = _innerList[0];
                _innerList.RemoveAt(0);
                return true;
            }
            else
            {
                data = null;
                return false;
            } 
        }
    }
}
