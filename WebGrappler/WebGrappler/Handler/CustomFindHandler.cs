using CefSharp;
using CefSharp.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGrappler.Handler
{
    class CustomFindHandler : IFindHandler
    {
        public void OnFindResult(IWebBrowser chromiumWebBrowser, IBrowser browser, int identifier, int count, Rect selectionRect, int activeMatchOrdinal, bool finalUpdate)
        {
            Console.WriteLine("OnFindResult");
        }
    }
}
