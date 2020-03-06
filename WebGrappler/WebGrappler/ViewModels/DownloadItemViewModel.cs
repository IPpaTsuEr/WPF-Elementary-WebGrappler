using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGrappler.Handler;
using WebGrappler.Services;

namespace WebGrappler.ViewModels
{
    public class DownloadItemViewModel:NotifyBase
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set
            {
                if (value != _ID)
                {
                    _ID = value;
                    Notify("ID");
                }
            }
        }
        private string _FullPath;

        public string FullPath
        {
            get { return _FullPath; }
            set
            {
                if (value != _FullPath)
                {
                    _FullPath = value;
                    Notify("FullPath");
                }
            }
        }
        private string _FileName;

        public string FileName
        {
            get { return _FileName; }
            set
            {
                if (value != _FileName)
                {
                    _FileName = value;
                    Notify("FileName");
                }
            }
        }

        private long _CurrentSpeed;

        public long CurrentSpeed
        {
            get { return _CurrentSpeed; }
            set
            {
                if (value != _CurrentSpeed)
                {
                    _CurrentSpeed = value;
                    Notify("CurrentSpeed");
                }
            }
        }
        private DateTime? _StartTime;

        public DateTime? StartTime
        {
            get { return _StartTime; }
            set
            {
                if (value != _StartTime)
                {
                    _StartTime = value;
                    Notify("StartTime");
                }
            }
        }
        private DateTime? _EndTime;

        public DateTime? EndTime
        {
            get { return _EndTime; }
            set
            {
                if (value != _EndTime)
                {
                    _EndTime = value;
                    Notify("EndTime");
                }
            }
        }

        private long _TotalBytes;

        public long TotalBytes
        {
            get { return _TotalBytes; }
            set
            {
                if (value != _TotalBytes)
                {
                    _TotalBytes = value;
                    Notify("TotalBytes");
                }
            }
        }
        private long _ReceivedBytes;

        public long ReceivedBytes
        {
            get { return _ReceivedBytes; }
            set
            {
                if (value != _ReceivedBytes)
                {
                    _ReceivedBytes = value;
                    Notify("ReceivedBytes");
                }
            }
        }

        private bool _IsCancelled = false;
            
        public bool IsCancelled
        {
            get { return _IsCancelled; }
            set
            {
                if (value != _IsCancelled)
                {
                    _IsCancelled = value;
                    Notify("IsCancelled");
                }
            }
        }
        private bool _IsValid = true;

        public bool IsValid
        {
            get { return _IsValid; }
            set
            {
                if (value != _IsValid)
                {
                    _IsValid = value;
                    Notify("IsValid");
                }
            }
        }
        private bool _IsComplete = false;

        public bool IsComplete
        {
            get { return _IsComplete; }
            set
            {
                if (value != _IsComplete)
                {
                    _IsComplete  = value;
                    Notify("IsComplete");
                }
            }
        }
        private bool _IsPaused = false; 

        public bool IsPaused
        {
            get { return _IsPaused; }
            set
            {
                if (value != _IsPaused)
                {
                    _IsPaused = value;
                    Notify("IsPaused");
                }
            }
        }
        private bool _IsInProgress;

        public bool IsInProgress
        {
            get { return _IsInProgress; }
            set
            {
                if (value != _IsInProgress)
                {
                    _IsInProgress = value;
                    Notify("IsInProcessed");
                }
            }
        }

        private string _Url;

        public string Url
        {
            get { return _Url; }
            set
            {
                if (value != _Url)
                {
                    _Url = value;
                    Notify("Url");
                }
            }
        }
        private string _OriginalUrl;

        public string OriginalUrl
        {
            get { return _OriginalUrl; }
            set
            {
                if (value != _OriginalUrl)
                {
                    _OriginalUrl = value;
                    Notify("OriginalUrl");
                }
            }
        }

        private double _AverageSpeed;

        public double AverageSpeed
        {
            get { return _AverageSpeed; }
            set
            {
                if (value != _AverageSpeed)
                {
                    _AverageSpeed = value;
                    Notify("AverageSpeed");
                }
            }
        }


        private bool _NeedPause;

        public bool NeedPause
        {
            get { return _NeedPause; }
            set
            {
                if (value != _NeedPause)
                {
                    _NeedPause = value;
                    Notify("NeedPause");
                }
            }
        }

        private bool _NeedResume;

        public bool NeedResume
        {
            get { return _NeedResume; }
            set
            {
                if (value != _NeedResume)
                {
                    _NeedResume = value;
                    Notify("NeedResume");
                }
            }
        }

        private bool _NeedCancel;

        public bool NeedCancel
        {
            get { return _NeedCancel; }
            set
            {
                if (value != _NeedCancel)
                {
                    _NeedCancel = value;
                    Notify("NeedCancel");
                }
            }
        }

        private bool _IsInitalized = false;

        public bool IsInitalized
        {
            get { return _IsInitalized; }
            set
            {
                if (value != _IsInitalized)
                {
                    _IsInitalized = value;
                    Notify("IsInitalized");
                }
            }
        }

        private double _NeedTime;

        public double NeedTime
        {
            get { return _NeedTime; }
            set
            {
                if (value != _NeedTime)
                {
                    _NeedTime = value;
                    Notify("NeedTime");
                }
            }
        }

        public CustomDownloadHandler customDownloadHandler { get; set; }
        public DownloadItemViewModel() { }

        public void UpDate(DownloadItem item)
        {
            //this.IsInitalized = item.IsInitalized;
            this.ID = item.Id;

            this.CurrentSpeed = item.CurrentSpeed;
            this.EndTime = item.EndTime;
            this.StartTime = item.StartTime;

            this.FileName = GetFileName(item.FullPath);
            this.FullPath = item.FullPath;

            this.IsCancelled = item.IsCancelled;
            this.IsComplete = item.IsComplete;
            this.IsValid = item.IsValid;

            this.ReceivedBytes = item.ReceivedBytes;
            this.TotalBytes = item.TotalBytes;

            this.IsInProgress = item.IsInProgress;

            this.Url = item.Url;
            this.OriginalUrl = item.OriginalUrl;


            if (this.CurrentSpeed == 0) this.NeedTime = -1;
            else this.NeedTime = (this.TotalBytes - this.ReceivedBytes) / this.CurrentSpeed;


            if (this.IsComplete)
                this.AverageSpeed = this.ReceivedBytes / (this.EndTime - this.StartTime).Value.TotalSeconds;
            else
                this.AverageSpeed = this.ReceivedBytes / (DateTime.Now - this.StartTime.Value).TotalSeconds;
        }

        public void UpDate(DownloadItemViewModel item)
        {
            //this.ID = item.ID;
            this.IsInitalized = item.IsInitalized;

            this.CurrentSpeed = item.CurrentSpeed;
            this.EndTime = item.EndTime;
            this.StartTime = item.StartTime;

            this.FileName = GetFileName(item.FullPath);
            this.FullPath = item.FullPath;

            this.IsCancelled = item.IsCancelled;
            this.IsComplete = item.IsComplete;
            this.IsValid = item.IsValid;
            this.IsPaused = item.IsPaused;

            this.ReceivedBytes = item.ReceivedBytes;
            this.TotalBytes = item.TotalBytes;

            this.IsInProgress = item.IsInProgress;

            this.Url = item.Url;
            this.OriginalUrl = item.OriginalUrl;

            if (this.CurrentSpeed == 0) this.NeedTime = -1;
            else this.NeedTime = (this.TotalBytes - this.ReceivedBytes) / this.CurrentSpeed;

            
            if (this.IsComplete)
                this.AverageSpeed = this.ReceivedBytes / (this.EndTime - this.StartTime).Value.TotalSeconds;
            else
                this.AverageSpeed = this.ReceivedBytes / (DateTime.Now - this.StartTime.Value).TotalSeconds;
        }

        public string GetFileName (string fullpath)
        {
            var _index = fullpath.LastIndexOf("\\");
            try { if (_index > 0) return fullpath.Substring(_index + 1); }
            catch (Exception e) { LogHelper.Debug(String.Concat("没有找到文件名 ",e.Message)); }
            return fullpath;
        }

    }
}
