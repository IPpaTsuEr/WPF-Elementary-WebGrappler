using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WebGrappler.Models;
using WebGrappler.Services;
using WebGrappler.ViewModels;

namespace WebGrappler.Handler
{
    public class CustomDownloadHandler : IDownloadHandler
    {
        //public static RoutedCommand DownloadUpdated = new RoutedCommand();

        public event EventHandler<object> OnUpdated;

        private Dictionary<int, DownloadItemViewModel> DownloadMap = new Dictionary<int, DownloadItemViewModel>();
        private Dictionary<int, IDownloadItemCallback> ControlMap = new Dictionary<int, IDownloadItemCallback>();
 
        private DownloadItemViewModel GetItem(int id)
        {
            if (DownloadMap.ContainsKey(id))
            {
                return DownloadMap[id];
            }
            return null;
        }

        private IDownloadItemCallback GetControl(int id)
        {
           if(ControlMap.ContainsKey(id))
            {
                return ControlMap[id];
               
            }
            return null;
        }

        public void Resume(int id)
        {
            var _t = GetItem(id);
            if (_t != null)
            {
                _t.NeedResume = true;
                try { GetControl(_t.ID)?.Resume(); } catch (Exception e) { Console.WriteLine("Resume Error :{0}", e.Message); }
            }
        }

        public void Cancel(int id)
        {
            var _t = GetItem(id);
            if (_t != null)
            {
                _t.NeedCancel = true;
               
            }
        }

        public void Pause(int id)
        {
            var _t = GetItem(id);
            if (_t != null)
            {
                _t.NeedPause = true;
                
            }
        }

        public void Completed(int id)
        {
            if (DownloadMap.ContainsKey(id))
            {
                DownloadMap.Remove(id);
            }
            if (ControlMap.ContainsKey(id))
            {
                ControlMap[id].Dispose();
                ControlMap.Remove(id);
            }
        }

        public void OnBeforeDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
        {
            callback.Continue(@"C:\Users\" + System.Security.Principal.WindowsIdentity.GetCurrent().Name +
                              @"\Downloads\" + downloadItem.SuggestedFileName, true);
            
            LogHelper.Debug("Download File "+ downloadItem.FullPath);
            
        }

        public void OnDownloadUpdated(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {
            DownloadItemViewModel _item = null;

            //初始暂停
            if (string.IsNullOrWhiteSpace(downloadItem.FullPath))
            {
                callback.Pause();
            }
            else//初始化完毕
            {
                //插入记录
                if (!DownloadMap.ContainsKey(downloadItem.Id))
                {
                    _item = new DownloadItemViewModel();
                    _item.customDownloadHandler = this;
                    _item.UpDate(downloadItem);
                    DownloadMap.Add(downloadItem.Id, _item);
                    //Console.WriteLine("----insert {0}", _item.ID);
                }
                else
                {            //获取对应记录
                    _item = DownloadMap[downloadItem.Id];
                }
                //继续开始下载
                if (!_item.IsInitalized)
                {
                    _item.IsInitalized = true;
                    callback.Resume();
                }
            }


            if (_item != null && _item.IsInitalized) {

                _item.UpDate(downloadItem);
                //callback管理
                if (callback.IsDisposed) Console.WriteLine("Callback disposed");
                else
                {
                    if (ControlMap.ContainsKey(_item.ID))
                    {
                        ControlMap[_item.ID] = callback;
                    }
                    else
                    {
                        ControlMap.Add(_item.ID, callback);
                    }
                }

                //更新
                OnUpdated?.BeginInvoke(this, _item, (o) =>
                {
                    if (_item.NeedPause)
                    {
                        try { GetControl(_item.ID)?.Pause(); } catch (Exception e) { Console.WriteLine("Cancel Error :{0}", e.Message); }
                        _item.NeedPause = false;
                        _item.IsPaused = true;
                    }
                    if (_item.NeedCancel)
                    {
                        try { GetControl(_item.ID)?.Cancel(); } catch (Exception e) { Console.WriteLine("Cancel Error :{0}", e.Message); }
                        _item.NeedCancel = false;
                        _item.IsCancelled = true;
                    }
                    if (_item.NeedResume)
                    {
                        try { GetControl(_item.ID)?.Resume(); } catch (Exception e) { Console.WriteLine("Resume Error :{0}", e.Message); }
                        _item.NeedResume = false;
                        _item.IsPaused = false;
                    }

                    OnUpdated.Invoke(this, _item);

                }, null);
            }
        }
    }
}
