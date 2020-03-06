using CefSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WebGrappler.Models;
using WebGrappler.Services;
using WebGrappler.ViewModels;

namespace WebGrappler.Handler
{
    class CustomLoadHandler : ILoadHandler
    {
        public WeakReference<ChrominViewModel> refrence;
        private bool MainLoadEnd = false;
        public CustomLoadHandler(ChrominViewModel cvm)
        {
            refrence = new WeakReference<ChrominViewModel>(cvm);
        }
        public CustomLoadHandler()
        {
        }

        public event EventHandler<LoadingStateChangedEventArgs> OnStateChange;

        public void OnFrameLoadEnd(IWebBrowser chromiumWebBrowser, FrameLoadEndEventArgs frameLoadEndArgs)
        {
            //Console.WriteLine("Frame Load End : "+frameLoadEndArgs.HttpStatusCode+" with "+frameLoadEndArgs.Url);
            if (frameLoadEndArgs.Frame.IsMain == true)
            {
                MainLoadEnd = true;
                Console.WriteLine("Frame Load End With Code: " + frameLoadEndArgs.HttpStatusCode + " @ " + frameLoadEndArgs.Url);
                var w = chromiumWebBrowser as CustomChrominWebBrowser;
                
                if (w.Info == null)
                {
                    string url = App.Current.Dispatcher.Invoke((Func<string>)(() => { return w.Address; }));
                    LoadIcon(url);
                }
            }
            else
            {
                MainLoadEnd = false;
            }
        }

        public void OnFrameLoadStart(IWebBrowser chromiumWebBrowser, FrameLoadStartEventArgs frameLoadStartArgs)
        {

            //Console.WriteLine("Frame Load Start : " + frameLoadStartArgs.TransitionType +" With "+ frameLoadStartArgs.Url);
        }

        public void OnLoadError(IWebBrowser chromiumWebBrowser, LoadErrorEventArgs loadErrorArgs)
        {
            //Console.WriteLine("Frame Load : " + loadErrorArgs.ErrorCode + " With "+loadErrorArgs.FailedUrl +" Beacuse :"+loadErrorArgs.ErrorText);
        }

        public void OnLoadingStateChange(IWebBrowser chromiumWebBrowser, LoadingStateChangedEventArgs loadingStateChangedArgs)
        {
           if(OnStateChange != null && MainLoadEnd) OnStateChange(chromiumWebBrowser, loadingStateChangedArgs);
        }

        /// <summary>
        /// 载入网站Icon
        /// </summary>
        /// <param name="url"></param>
        public async void LoadIcon(string url)
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    var source = TaskHelper.GetDomain(url, true) + "/favicon.ico";
                    WebClient wb = new WebClient();
                    byte[] data = wb.DownloadData(source);
                    if(data==null || data.Count() < 10)return ;
                    App.Current.Dispatcher.BeginInvoke(new Action<MemoryStream>(od =>
                    {
                        try
                        {
                            var b = new BitmapImage();
                            b.BeginInit();
                            b.CacheOption = BitmapCacheOption.OnLoad;
                            b.StreamSource = od;
                            b.EndInit();
                            b.Freeze();
                            ChrominViewModel target=null;
                            if(refrence!=null)refrence.TryGetTarget(out target);
                            if(target!=null)target.Icon = b;
                        }catch(Exception er)
                        {
                            Console.WriteLine(" 创建faveicon 失败 : " + er.Message);
                        }

                    }), new MemoryStream(data));

                }
                catch (Exception) { Console.WriteLine(string.Format(" 获取网络资源[{0}/favicon.ico]失败 : ", url)); }


            });
        }

    }
}
