using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;

namespace WebGrappler.Handler
{
    class CustomResponseFilter : CefSharp.IResponseFilter
    {
        public long DataLength{ get; set; }
        public MemoryStream Data { get; set; }

        public CustomResponseFilter(long dataLength,MemoryStream data)
        {
            DataLength = dataLength;
            Data = data;
        }
        public CustomResponseFilter()
        {
            Console.WriteLine("Response Filter Created");
        }
        public void Dispose()
        {
            return;
        }

        public FilterStatus Filter(Stream dataIn, out long dataInRead, Stream dataOut, out long dataOutWritten)
        {
            if (dataIn == null || dataIn.Length==0)
            {
                dataInRead = 0;
                dataOutWritten = dataInRead;
            }
            else
            {
                dataInRead = dataIn.Length;
                dataOutWritten = dataInRead;
                dataIn.CopyTo(dataOut);
                dataIn.Position = 0;
                dataIn.CopyTo(Data);
                if (DataLength < 0) return FilterStatus.Done;
                else if (DataLength > dataInRead) return FilterStatus.NeedMoreData;
            }
            
            return FilterStatus.Done;
        }

        public bool InitFilter()
        {
            return true;
        }
    }
}
