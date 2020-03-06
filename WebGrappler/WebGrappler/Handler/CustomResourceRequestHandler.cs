using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using CefSharp;
using WebGrappler.Models;
using WebGrappler.Services;
using WebGrappler.ViewModels;

namespace WebGrappler.Handler
{
    class CustomResourceRequestHandler : CefSharp.IResourceRequestHandler
    {
        public static List<ResourceType> AcceptType = new List<ResourceType>()
        {
            ResourceType.Favicon,
            ResourceType.Image,
            ResourceType.Media,
            ResourceType.Xhr,
            ResourceType.PluginResource
        };

        public Dictionary<string, NetSource> SourcesList;
        string CustomUserAgent;

        public CustomResourceRequestHandler()
        {
            CustomUserAgent = (new UserAgentHelper()).GetRandomUserAgent();
            LogHelper.Debug("取得随机UserAgent【" + CustomUserAgent + "】");
            Console.WriteLine("Resource Request Handler Created");
        }


        public ICookieAccessFilter GetCookieAccessFilter(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request)
        {
            return null;
        }

        public IResourceHandler GetResourceHandler(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request)
        {
            //return new CustomResourceHandler();
            return null;
        }

        public IResponseFilter GetResourceResponseFilter(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IResponse response)
        {
            //将指定类型数据在内存中保留
            var _chrom = chromiumWebBrowser as CustomChrominWebBrowser;
            if(_chrom.Info!=null || _chrom.TaskSize() > 0)
            {
                if (SourcesList == null) SourcesList = new Dictionary<string, NetSource>();
                if (AcceptType.Contains(request.ResourceType))
                {
                   
                    return GetData(request, response);
                }
            }
                //LogHelper.Debug("非Task页面，未启用网络资源内存驻留");
            return null;
        }

        private CustomResponseFilter GetData(IRequest request ,IResponse response)
        {
            var length = response.Headers["Content-Length"];
            long size = -1;
            try
            {
                size = Convert.ToInt64(length);
            }
            catch (Exception errormsg)
            {
                LogHelper.Debug("无法转换的文件长度 ： " + length + "    " + errormsg);
            }
            var imme = response.Headers["Content-Type"];
            if (imme != null && imme.IndexOf(";") > 0)
                imme = imme.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)[0];

            NetSource ms = null;
            if (!SourcesList.TryGetValue(request.Url, out ms))
            {
                ms = new NetSource
                {
                    Data = new MemoryStream(),
                    IMME = imme
                };
                SourcesList.Add(request.Url, ms);
            }
            else
            {
               //LogHelper.Debug("Url 已存在于缓存列表 " + request.Url);
            }

            return new CustomResponseFilter(size, ms.Data);
        }

        public CefReturnValue OnBeforeResourceLoad(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IRequestCallback callback)
        {
            var _chrom = chromiumWebBrowser as CustomChrominWebBrowser;

            if (_chrom.Info == null)
            {
                var _RH = request.Headers;
                _RH["User-Agent"] = CustomUserAgent;
                request.Headers = _RH;
                //callback.Cancel();
                if(request.Headers["User-Agent"] != CustomUserAgent)
                {
                    LogHelper.Debug("随机UserAgent设失败");
                }
                //
            }
            
            return CefReturnValue.Continue;
            
        }

        public bool OnProtocolExecution(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request)
        {
          
            return false;
            ///注意第三个参数，allow_os_execution，它默认值为false，不会调用注册表内注册的自定义协议，需要修改它为true。
            ///第二参数是来自<a> 标签的href属性的值，就是URL。
            ///如果你想自己在C++代码里处理，可以直接启动一个应用来处理这个URL，如果你想交给系统处理，只要修改allow_os_execution为true即可。

        }

        public void OnResourceLoadComplete(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IResponse response, UrlRequestStatus status, long receivedContentLength)
        {
            //if(_Reference != null && request.ResourceType== ResourceType.Favicon)
            //{
            //    if(SourcesList.TryGetValue(request.Url,out NetSource data))
            //    {
            //        try
            //        {
            //            var b = new BitmapImage();
            //            b.BeginInit();
            //            b.CacheOption = BitmapCacheOption.OnLoad;
            //            b.StreamSource = data.Data;
            //            b.EndInit();
            //            b.Freeze();
            //            _Reference.Icon = b;
            //            //ChrominViewModel target = null;
            //            //if (refrence != null) refrence.TryGetTarget(out target);
            //            //if (target != null) target.Icon = b;
            //        }
            //        catch (Exception er)
            //        {
            //            Console.WriteLine(" 创建faveicon 失败 : " + er.Message);
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine(" 创建faveicon 失败 : 没有找到favicon资源");
            //    }
            //}
            //if (request.Url.LastIndexOf(".jpg") > 0) Console.WriteLine("载入完毕----->{0}", request.Url);
            return;
        }

        public void OnResourceRedirect(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IResponse response, ref string newUrl)
        {
            //LogHelper.Debug("跳转到[" + newUrl +"]");
            //Console.WriteLine(response.Headers["Accept-Ranges"]);
            //Console.WriteLine(request.Headers["Accept-Ranges"]);
            return;
        }

        public bool OnResourceResponse(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IResponse response)
        {
            return true;
        }
    }
}
