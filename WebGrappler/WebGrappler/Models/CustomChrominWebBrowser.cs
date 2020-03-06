using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WebGrappler.Handler;
using WebGrappler.Interface;
using WebGrappler.Services;

namespace WebGrappler.Models
{
    public class CustomChrominWebBrowser:ChromiumWebBrowser,IReuseable
    {
        private Dictionary<string,Task> Tasks { get; set; }

        private Models.Task _Info;
        public Models.Task Info { get{ return _Info; } set {_Info = value; } }

        public long ID { get; set; }

        public CustomChrominWebBrowser() : base()
        {
            ID = DateTime.Now.Ticks;
           Tasks = new Dictionary<string, Task>();
        }
        public CustomChrominWebBrowser(string url) : base(url)
        {
            ID = DateTime.Now.Ticks;
            Tasks = new Dictionary<string, Task>();
        }

        #region 内部任务管理
        public Models.Task PopTask()
        {
            if (Tasks.Count > 0)
            {
                var ft = Tasks.First();
                Tasks.Remove(ft.Key);
                return ft.Value;
            }
            return null;
        }

        public void PushTask(Models.Task task)
        {
            if(!Tasks.ContainsKey(task.Action))
                Tasks.Add(task.Action,task);
            else
            {
                LogHelper.Debug(string.Format("任务对象{0}的内部任务队列已包含任务{1}",ID,task.Action));
            }
        }

        public int TaskSize()
        {
            if(Tasks!=null)
                return Tasks.Count;
            return 0;
        }
        #endregion

        #region 对象池重用接口实现

        private bool IsUsing = false;

        public bool CanReuse()
        {
            return !IsUsing;
        }

        public void Reset()
        {
            ID = 0;
            Info = null;
            if (Tasks != null) Tasks.Clear();
            if (RequestHandler != null)
            {
                ((CustomRequestHandler)RequestHandler).ReleaseCustomData();
                RequestHandler = null;
            }
            if (LoadHandler != null) LoadHandler = null;
            //ID = DateTime.Now.Ticks;
        }

        public void Release()
        {
            Reset();
            Tasks = null;
            App.Current.Dispatcher.BeginInvoke(
                new Action(()=> {
                    if(!IsDisposed)
                        GetBrowser()?.CloseBrowser(false);
                })
            );
        }

        public void ChangeReuseable(bool reuseable)
        {
            if (reuseable) IsUsing = false;
            else { IsUsing = true; ID = DateTime.Now.Ticks; }
        }
        
        #endregion
    }
}
