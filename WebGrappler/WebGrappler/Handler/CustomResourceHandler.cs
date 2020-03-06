using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;
using CefSharp.Callback;

namespace WebGrappler.Handler
{
    class CustomResourceHandler : CefSharp.IResourceHandler
    {
        public CustomResourceHandler()
        {
            Console.WriteLine("Resource Handler Created");
        }

        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void GetResponseHeaders(IResponse response, out long responseLength, out string redirectUrl)
        {
            Console.WriteLine("Resource Response Code " + response.ErrorCode);
            throw new NotImplementedException();
        }

        public bool Open(IRequest request, out bool handleRequest, ICallback callback)
        {
            handleRequest = true;
            return true;
        }

        public bool ProcessRequest(IRequest request, ICallback callback)
        {
            throw new NotImplementedException();
        }

        public bool Read(Stream dataOut, out int bytesRead, IResourceReadCallback callback)
        {
            throw new NotImplementedException();
        }

        public bool ReadResponse(Stream dataOut, out int bytesRead, ICallback callback)
        {
            throw new NotImplementedException();
        }

        public bool Skip(long bytesToSkip, out long bytesSkipped, IResourceSkipCallback callback)
        {
            throw new NotImplementedException();
        }
    }
}
