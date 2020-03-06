using CefSharp;
using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using WebGrappler.Services;

namespace WebGrappler.Handler
{
    class CustomLifeSpanHandler : ILifeSpanHandler
    {
        public event EventHandler<string> OpenTargUrl;

        public void RiaseEvent(string url)
        {
            OpenTargUrl(this, url);
        }

        public bool DoClose(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
           Console.WriteLine(" Life Handler DoClose : "+ browser.IsDisposed);
            if (!browser.IsDisposed)
            {
                browser.Dispose();
            }
            return false; 
        }

        public void OnAfterCreated(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
            Console.WriteLine(" Life Handler OnAfterCreated");
        }

        public void OnBeforeClose(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
            try
            {
                var web = chromiumWebBrowser as ChromiumWebBrowser;
                var requ = web.RequestHandler as CustomRequestHandler;
                if(requ!=null && requ.GetMemoryData()!=null)requ.ReleaseCustomData();
            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message);
            }
            Console.WriteLine(" Life Handler OnBeforeClose");

        }

        public bool OnBeforePopup(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            //Console.WriteLine("On Pop {0}" , targetUrl);

            if (targetUrl!= "about:blank#blocked")
            {
                //需要在UI线程操作
                App.Current.Dispatcher.Invoke((Action)(()=>{ RiaseEvent(targetUrl); }));
                //Console.WriteLine("On Pop Open A New Browser");
            }

            //返回true 将取消默认创建
            newBrowser = null;
            return true;
        }
    }
}
