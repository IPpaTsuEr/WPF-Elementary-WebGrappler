using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGrappler.Models;

namespace WebGrappler.ViewModels
{
    public class TaskViewModel:NotifyBase
    {
        private bool _IsPausing;

        public bool IsPauseing
        {
            get { return _IsPausing; }
            set
            {
                if (value != _IsPausing)
                {
                    _IsPausing = value;
                    Notify("IsPauseing");
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

 
        private bool _IsResuming;

        public bool IsResuming
        {
            get { return _IsResuming; }
            set
            {
                if (value != _IsResuming)
                {
                    _IsResuming = value;
                    Notify("IsResuming");
                }
            }
        }


        private bool _IsStopped = false;

        public bool IsStopped
        {
            get { return _IsStopped; }
            set
            {
                if (value != _IsStopped)
                {
                    _IsStopped = value;
                    Notify("IsStoped");
                }
            }
        }

        private bool _IsStopping;

        public bool IsStopping
        {
            get { return _IsStopping; }
            set
            {
                if (value != _IsStopping)
                {
                    _IsStopping = value;
                    Notify("IsStopping");
                }
            }
        }


        private bool _IsCompleted = false;

        public bool IsCompleted
        {
            get { return _IsCompleted; }
            set
            {
                if (value != _IsCompleted)
                {
                    _IsCompleted = value;
                    Notify("IsCompleted");
                }
            }
        }

        private bool _IsErrored = false;

        public bool IsErrored
        {
            get { return _IsErrored; }
            set
            {
                if (value != _IsErrored)
                {
                    _IsErrored = value;
                    Notify("IsErrored");
                }
            }
        }


        private int _TotalCount;

        public int TotalCount
        {
            get { return _TotalCount; }
            set
            {
                if (value != _TotalCount)
                {
                    _TotalCount = value;
                    Notify("TotalCount");
                }
            }
        }

        private int _SuccessCount;

        public int SuccessCount
        {
            get { return _SuccessCount; }
            set
            {
                if (value != _SuccessCount)
                {
                    _SuccessCount = value;
                    Notify("SuccessCount");
                }
            }
        }

        private int _ErrorCount;

        public int ErrorCount
        {
            get { return _ErrorCount; }
            set
            {
                if (value != _ErrorCount)
                {
                    _ErrorCount = value;
                    Notify("ErrorCount");
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

        private string _Path;

        public string Path
        {
            get { return _Path; }
            set
            {
                if (value != _Path)
                {
                    _Path = value;
                    Notify("Path");
                }
            }
        }

        private string _Title;

        public string Title
        {
            get { return _Title; }
            set
            {
                if (value != _Title)
                {
                    _Title = value;
                    Notify("Title");
                }
            }
        }


        private long _ID;

        public long ID
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


        public CustomChrominWebBrowser Chrome { get; set; }

        public void reset() { }
    }
}
