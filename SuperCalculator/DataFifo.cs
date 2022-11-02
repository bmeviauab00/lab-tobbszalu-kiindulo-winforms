using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SuperCalculator
{
    class DataFifo
    {
        private List<double[]> innerList = new List<double[]>();
      
        public void Put( double[] data )
        {
            innerList.Add( data );
        }

        public bool TryGet(out double[] data)
        {
            data = null;
            if ( innerList.Count > 0 )
            {
                // Egy kis mesterséges késleltetés, ami nem befolyásolhatja, 
                // hogy helyesen mûködik-e a FIFO.
                Thread.Sleep(500);
                data = innerList[ 0 ];
                innerList.RemoveAt( 0 );
                return true;
            }
            else return false;
        }
    }
}
