using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CefSharp;
using WebGrappler.Models;
using WebGrappler.Services;
using WebGrappler.ViewModels;

namespace WebGrappler.Handler
{
    class CustomRequestHandler : CefSharp.IRequestHandler
    {
        CustomResourceRequestHandler crrh;

        public CustomRequestHandler()
        {
            crrh = new CustomResourceRequestHandler();
            Console.WriteLine("RequestHandler Created");
        }

        public Dictionary<string, NetSource> GetMemoryData()
        {
            return crrh.SourcesList;
        }
        public void ReleaseCustomData()
        {
            crrh.SourcesList.Clear();
        }

        public bool GetAuthCredentials(IWebBrowser chromiumWebBrowser, IBrowser browser, string originUrl, bool isProxy, string host, int port, string realm, string scheme, IAuthCallback callback)
        {
            throw new NotImplementedException();
        }

        public IResourceRequestHandler GetResourceRequestHandler(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool isNavigation, bool isDownload, string requestInitiator, ref bool disableDefaultHandling)
        {
            return crrh;
        }

        public bool OnBeforeBrowse(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool userGesture, bool isRedirect)
        {
            ///在一个页面打开之前调用，如果你返回false，页面会继续加载，如果返回true，就停止加载了。
            ///如果你发现是mypro协议，就返回true，让OnProtocolExecution函数继续处理，
            ///否则返回false，让浏览器继续加载。
            return false;
        }

        public bool OnCertificateError(IWebBrowser chromiumWebBrowser, IBrowser browser, CefErrorCode errorCode, string requestUrl, ISslInfo sslInfo, IRequestCallback callback)
        {
            throw new NotImplementedException();
        }

        public bool OnOpenUrlFromTab(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, string targetUrl, WindowOpenDisposition targetDisposition, bool userGesture)
        {
            throw new NotImplementedException();
        }

        public void OnPluginCrashed(IWebBrowser chromiumWebBrowser, IBrowser browser, string pluginPath)
        {
            throw new NotImplementedException();
        }

        public bool OnQuotaRequest(IWebBrowser chromiumWebBrowser, IBrowser browser, string originUrl, long newSize, IRequestCallback callback)
        {
            throw new NotImplementedException();
        }

        public void OnRenderProcessTerminated(IWebBrowser chromiumWebBrowser, IBrowser browser, CefTerminationStatus status)
        {
            LogHelper.Debug(string.Format("CefSharp 遇到错误 渲染进程异常结束,{0}",status));
        }

        public void OnRenderViewReady(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
            //Console.WriteLine("-------------Render View Ready-------------");
            return;
        }

        public bool OnSelectClientCertificate(IWebBrowser chromiumWebBrowser, IBrowser browser, bool isProxy, string host, int port, X509Certificate2Collection certificates, ISelectClientCertificateCallback callback)
        {
            throw new NotImplementedException();
        }
    }
}
