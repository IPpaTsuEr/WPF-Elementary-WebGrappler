using CefSharp;
using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WebGrappler.Commands;
using WebGrappler.Handler;
using WebGrappler.Models;
using WebGrappler.Services;

namespace WebGrappler.ViewModels
{
    public class WindowView:NotifyBase
    {
        
        //用户浏览
        public ObservableCollection<ChrominViewModel> PageList { get; set; } = new ObservableCollection<ChrominViewModel>();
        //任务解析
        public ObservableCollection<ChrominViewModel> TaskList { get; set; } = new ObservableCollection<ChrominViewModel>();
        
        //下载 解析 队列
        public ObservableCollection<TaskViewModel> TaskViewList { get; set; } = new ObservableCollection<TaskViewModel>();
        public ObservableCollection<DownloadItemViewModel> DownloadList { get; set; } = new ObservableCollection<DownloadItemViewModel>();
        
        //预载入
        private ChrominViewModel _PreLoad ;

        public ChrominViewModel PreLoad
        {
            get { return _PreLoad; }
            set
            {
                if (value != _PreLoad)
                {
                    _PreLoad = value;
                    Notify("PreLoad");
                }
            }
        }


        public ICommand WebControl { get; set; }
        public ICommand DownloadControl { get; set; }
        public ICommand SearchControl { get; set; }
        //public ICommand OpenurlControl { get; set; }
        public ICommand TaskControl { get; set; }
        public ICommand MenuControl { get; set; }

        CustomLifeSpanHandler LifeSpanHandler { get; set; } = new CustomLifeSpanHandler();
        CustomDownloadHandler DownloadHandler { get; set; } = new CustomDownloadHandler();

        BackgroundWorker BK;
        BrowserHelper<CustomChrominWebBrowser> BH;

        string BaseSavePath = "./Download/";

        CefSettings cf = new CefSettings();

        private void InitCef()
        {
            cf.Locale = "zh-CN";
            cf.AcceptLanguageList = "zh-CN";
            cf.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36";
            cf.CefCommandLineArgs.Add("disable-gpu", "0");
            cf.CefCommandLineArgs["disable-gpu-compositing"] = "0";
            cf.CefCommandLineArgs.Add("enable-webgl", "1");  //开启WEBGL
            cf.CefCommandLineArgs.Add("ignore-gpu-blacklist", "1");  //忽略显卡黑名单
            cf.CefCommandLineArgs.Add("allow-file-access-from-files", "1");  //本地调试WEBGL

            cf.CefCommandLineArgs.Add("--ignore-urlfetcher-cert-requests", "1");
            cf.CefCommandLineArgs.Add("--ignore-certificate-errors", "1");
            //禁止启用同源策略安全限制，禁止后不会出现跨域问题
            cf.CefCommandLineArgs.Add("--disable-web-security", "0");
            cf.CefCommandLineArgs.Add("enable-media-stream", "1");
            cf.PersistSessionCookies = true;
            //flash
            cf.CefCommandLineArgs.Add("ppapi-flash-path", @"Plugins\pepflashplayer.dll");
            cf.CefCommandLineArgs["enable-system-flash"] = "1";
            cf.CefCommandLineArgs.Add("ppapi-flash-version", "25.0.0.209");

            //cf.CefCommandLineArgs.Add("disable-application-cache", "1"); AppDomain.CurrentDomain.BaseDirectory +
            if (!Directory.Exists("./RootCache/WebCache")) Directory.CreateDirectory("./RootCache/WebCache");
            cf.RootCachePath = "./RootCache";
            cf.CachePath = "./RootCache/WebCache";

            CefSharp.Cef.Initialize(cf);

            //browser.GetBrowser().GetHost().SendMouseWheelEvent()

            //KeyEvent k = new KeyEvent();
            //k.WindowsKeyCode = 0x0D;
            //k.FocusOnEditableField = true;
            //k.IsSystemKey = false;
            //k.Type = KeyEventType.Char;
            //Browser.GetBrowser().GetHost().SendKeyEvent(k);
        }
        private void EndCef()
        {
            CefSharp.Cef.Shutdown();
        }

        public WindowView()
        {
            InitCef();

            DownloadHandler.OnUpdated += WindowView_OnUpdated;

            WebControl = new CustomCommand(CommandJudger, CommandExcter);

            TaskControl = new CustomCommand(
                o=> {
                    //return true;
                    var _cmds = (object[])o;
                    var cmd = (string)_cmds[0];
                    var target = (TaskViewModel)_cmds[1];
                    switch (cmd)
                    {
                        case "_Pause":
                           return  !target.IsPaused && !target.IsPauseing;
                        case "_Stop":
                            return !target.IsCompleted && !target.IsErrored;
                        case "_Resume":
                            return target.IsPaused ;
                        case "_CopyUrl":
                            return !string.IsNullOrWhiteSpace(target.Url);
                        case "_Drop":
                            return true;
                        case "_Remove":
                            return target.IsCompleted || target.IsStopped;
                        case "_Retry":
                            return target.IsStopped || target.IsErrored;
                        case "_Open":
                            return !string.IsNullOrWhiteSpace(target.Path);
                        default:
                            return true;
                    }
                }
               ,p=> {
                var _cmds = (object[])p;
                var cmd = (string)_cmds[0];
                var target = (TaskViewModel)_cmds[1];
                switch (cmd)
                {
                    case "_Pause":
                        target.IsPauseing = true;
                        break;
                    case "_Stop":
                        target.IsStopping = true;
                        target.IsPaused = false;
                        break;
                    case "_Resume":
                        target.IsResuming = true;
                        target.Chrome.Info = target.Chrome.PopTask();
                        target.Chrome.Address = target.Chrome.Info.Action;
                        target.IsResuming = false;
                        target.IsPaused = false;
                        break;

                    case "_CopyUrl":
                        if(!string.IsNullOrWhiteSpace(target.Url))Clipboard.SetText(target.Url);
                        break;
                    case "_Drop":
                        if(MessageBox.Show(string.Format("注意，此操作无法恢复！是否确认移除任务集[{0}]产生的所有数据?",target.Title),"移除警告",MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                        {
                            target.IsStopping = true;
                            App.Current.Dispatcher.BeginInvoke(new Action(() => {
                                //等待任务终止
                                while (target.IsStopped == false) { Thread.Sleep(300); }
                                TaskViewList.Remove(target);
                                Directory.Delete(target.Path, true);
                            }));
                            
                        }
                        break;
                    case "_Remove":
                         
                        TaskViewList.Remove(target);
                        break;
                       case "_Retry":

                           break;
                    case "_Open":
                        System.Diagnostics.Process.Start(Path.GetFullPath(target.Path));
                        break;
                    default:
                        break;
                }
            });

            SearchControl = new CustomCommand(
                o=> {
                    if (o is string && !string.IsNullOrWhiteSpace((string)o)) return true;
                    return false;
                }, 
                p=> {
                    var _key = p as string;
                    _key = "https://www.baidu.com/s?wd=" + _key;
                    OpenUrl(_key);
                });
            //OpenurlControl = new CustomCommand(
            //    o =>{
            //        if (o is string && !string.IsNullOrWhiteSpace((string)o)) return true;
            //        return false;
            //     },
            //    p =>
            //    {
            //        OpenUrl(p as string);
            //    });

            DownloadControl = new CustomCommand(
                o=> {
                    var cmds = o as object[];
                    var target = (DownloadItemViewModel)cmds[1];
                    if (target == null) return false;
                    switch (cmds[0] as string)
                    {

                        case "_Cancel":
                            return !target.IsComplete && !target.IsCancelled && !target.IsValid;
                        case "_Pause":
                            return target.IsValid&& !target.IsPaused && !target.IsComplete && !target.IsCancelled;
                        case "_Resume":
                            return target.IsPaused;
                        case "_Open":
                            return !string.IsNullOrWhiteSpace(target.FullPath); 
                        case "_Excute":
                            return !string.IsNullOrWhiteSpace(target.FullPath);
                        case "_CopyUrl":
                            return !string.IsNullOrWhiteSpace(target.Url);
                        case "_Drop":
                            return true;
                        case "_Retry":
                            return !target.IsValid || target.IsCancelled;
                        case "_Remove":
                            return target.IsComplete || target.IsCancelled;
                        default:
                            return false;
                    }
                },
                p=> {
                    var cmds = p as object[];
                    var target = (DownloadItemViewModel)cmds[1];
                    switch (cmds[0] as string)
                    {

                        case "_Cancel":
                            //target.NeedCancel = true;
                            target.customDownloadHandler.Cancel(target.ID);
                            break;
                        case "_Pause":
                            //target.NeedPause = true;
                            target.customDownloadHandler.Pause(target.ID);
                            break;
                        case "_Resume":
                            //target.NeedResume = true;
                            target.customDownloadHandler.Resume(target.ID);
                            break;
                        case "_Open":
                            System.Diagnostics.Process.Start(Path.GetDirectoryName(target.FullPath));
                            break;
                        case "_Excute":
                            System.Diagnostics.Process.Start(target.FullPath);
                            break;
                        case "_CopyUrl":
                            Clipboard.SetText(target.Url);
                            break;
                        case "_Drop":
                            if (MessageBox.Show(string.Format("是否确认删除文件[{0}]并移除记录?此操作无法恢复！",target.FullPath), "移除警告", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                            {
                                DownloadList.Remove(target);
                                if (!target.IsComplete) target.NeedCancel = true ;
                                else  File.Delete(target.FullPath);
                            }
                            break;
                        case "_Retry":

                            break;
                        case "_Remove":
                            DownloadList.Remove(target);
                            break;
                        default:
                            break;
                    }
                });

            MenuControl = new CustomCommand(
                o=> {
                    if(o is string)
                    {
                        switch (o as string)
                        {
                            case "_OpenDownload":
                                return Directory.Exists("./Download/");
                            case "_OpenLog":
                                return Directory.Exists("./Log/");
                            case "_OpenAbout":
                                return true;
                            default:
                                break;
                        }
                    }
                    return false; },
                p=> {
                    if(p is string)
                    {
                        switch (p as string)
                        {
                            case "_OpenRules":
                                var path = Path.GetFullPath("./Rules.txt");
                                if (!File.Exists(path))
                                {
                                    File.WriteAllText(path, "@请添加解析规则,重启应用程序后生效;");
                                    
                                }
                                System.Diagnostics.Process.Start(path);
                                break;
                            case "_OpenDownload":
                                System.Diagnostics.Process.Start(Path.GetFullPath("./Download/"));
                                break;
                            case "_OpenLog":
                                System.Diagnostics.Process.Start(Path.GetFullPath("./Log/"));
                                break;
                            case "_OpenAbout":
                                MessageBox.Show("什么都没有～");
                                break;
                            default:
                                break;
                        }
                    }
                });

            CmdHelper.Load("./Rules.txt");

            BK = new BackgroundWorker();
            BH = new BrowserHelper<CustomChrominWebBrowser>();
            BH.SetPoolSize(3);
            
            LifeSpanHandler.OpenTargUrl += OpenTargetUrl;

            BK.WorkerReportsProgress = false;
            BK.WorkerSupportsCancellation = true;
            BK.DoWork += BK_DoWork;
            BK.RunWorkerCompleted += BK_RunWorkerCompleted;

            //DownloadList.Add(new DownloadItemViewModel() { FullPath = @"E:\Windows\Pictures\笑哭.jpg", FileName = "笑哭.jpg", TotalBytes = 4096, ReceivedBytes = 1024, CurrentSpeed = 1099, EndTime = DateTime.Now, IsComplete = true });
            //DownloadList.Add(new DownloadItemViewModel() { FullPath = @"E:\Windows\Pictures\笑哭.jpg", FileName = "笑哭.jpg", TotalBytes = 4096, ReceivedBytes = 1024, CurrentSpeed = 1099, EndTime = DateTime.Now, IsPaused = true });
            //DownloadList.Add(new DownloadItemViewModel() { FullPath = @"E:\Windows\Pictures\笑哭.jpg", FileName = "笑哭.jpg", TotalBytes = 4096, ReceivedBytes = 1024, CurrentSpeed = 1099, EndTime = DateTime.Now, IsValid = false });
            //DownloadList.Add(new DownloadItemViewModel() { FullPath = @"E:\Windows\Pictures\笑哭.jpg", FileName = "笑哭.jpg", TotalBytes = 4096, ReceivedBytes = 1024, CurrentSpeed = 1099, EndTime = DateTime.Now, IsCancelled = true });
            //DownloadList.Add(new DownloadItemViewModel() { FullPath = @"E:\Windows\Pictures\笑哭.jpg", FileName = "笑哭.jpg", TotalBytes = 4096, ReceivedBytes = 1024, CurrentSpeed = 1099, EndTime = DateTime.Now, IsInProgress = true });

            //TaskViewList.Add(new TaskViewModel() { ID = 9990099, Title = "艾玛A网站", TotalCount = 67, ErrorCount = 10, SuccessCount = 48, IsCompleted = true });
            //TaskViewList.Add(new TaskViewModel() { ID = 9994799, Title = "艾玛B网站", TotalCount = 67, ErrorCount = 10, SuccessCount = 48, IsErrored = true });
            //TaskViewList.Add(new TaskViewModel() { ID = 9994599, Title = "艾玛C网站", TotalCount = 67, ErrorCount = 10, SuccessCount = 48, IsPaused = true });
            //TaskViewList.Add(new TaskViewModel() { ID = 9994399, Title = "艾玛D网站", TotalCount = 67, ErrorCount = 10, SuccessCount = 48, IsPaused = false });
            //TaskViewList.Add(new TaskViewModel() { ID = 9994199, Title = "艾玛E网站", TotalCount = 67, ErrorCount = 10, SuccessCount = 48, IsStopped = true });

        }

        public void OnClosing()
        {
            BH.Dispose();
            EndCef();
        }

        private void Browser_StatusMessage(object sender, StatusMessageEventArgs e)
        {
            Console.WriteLine("StatusMessage {0}", e.Value);
        }

        #region Command

        public bool CommandJudger(object pra)
        {
            
            if(pra is object[])
            {
                object[] cmds = pra as object[];
                string cmd = cmds[0] as string;
                ChrominViewModel cvm = cmds[1] as ChrominViewModel;
                //Console.WriteLine("Canexcute=====>" + cmd);
                if (cvm == null || cvm.WebBrowser == null) {
                    
                    if (cmd == "_OpenUrl")if(!string.IsNullOrWhiteSpace((string)cmds[2])) return true;
                    if (cmd == "_NewTask")if (!string.IsNullOrWhiteSpace((string)cmds[2])) return true;
                    return false;
                }
                switch (cmd)
                {
                    case "_Back":
                        return cvm.WebBrowser.CanGoBack;
                    case "_Fresh":
                        return cvm.WebBrowser.Address!=null;
                    case "_Next":
                        return cvm.WebBrowser.CanGoForward;
                    case "_Close":
                        return true;
                    case "_OpenUrl":
                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            CommandManager.InvalidateRequerySuggested();
                        }));
                        if ((cvm == null || cvm.WebBrowser.Address == null))
                        {
                            if ((cmds[2] == null || (string)cmds[2] == "")) return false;
                            return true;
                        }
                        else return cvm.WebBrowser.Address != cmds[2] as string;
                    case "_NewTask":

                        return CmdHelper.CanAnalysis(cmds[2] as string);
                    default:
                        break;
                }
            }
            return pra == null ? false : true;
        }

        public void CommandExcter(object pra)
        {

            if (pra is object[])
            {
                object[] cmds = pra as object[];
                string cmd = cmds[0] as string;
                ChrominViewModel cvm = cmds[1] as ChrominViewModel;

                if (cvm == null || cvm.WebBrowser == null)
                {
                    if(cmd != "_OpenUrl" && cmd != "_NewTask")  return;
                }
                switch (cmd)
                {
                    case "_Back":
                        cvm.WebBrowser.GetBrowser().GoBack();
                        break;
                    case "_Fresh":
                        cvm.WebBrowser.GetBrowser().Reload();
                        break;
                    case "_Next":
                        cvm.WebBrowser.GetBrowser().GoForward();
                        break;
                    case "_Close":
                        cvm.WebBrowser.GetBrowser().CloseBrowser(false);
                        PageList.Remove(cvm);
                        break;
                    case "_OpenUrl":
                        OpenUrl((string)cmds[2]);
                        break;
                    case "_NewTask":
                        OpenTask((string)cmds[2]);
                        break;
                    default:
                        break;
                }
            }

        }

        public void OpenUrl(string url)
        {
            CustomChrominWebBrowser browser = new CustomChrominWebBrowser(url);
            browser.LifeSpanHandler = LifeSpanHandler;
            
            var page = new ChrominViewModel(browser);
            browser.RequestHandler = new CustomRequestHandler();
            browser.LoadHandler = new CustomLoadHandler(page);
            browser.MenuHandler = new CustomMenuHandler();
            browser.DownloadHandler = DownloadHandler;
            //browser.StatusMessage += Browser_StatusMessage;

            //browser.CommandBindings.Add(new CommandBinding(CustomDownloadHandler.DownloadUpdated,new ExecutedRoutedEventHandler(DownloadUpdated)));//,new CanExecuteRoutedEventHandler(CanDownloadUpdated)
            page.IsSelected = true;
            PreLoad = page;
            if (PageList.Count > 0)
            {
                PageList.Insert(GetCurentPageIndex()+1, page);
            }
            else PageList.Add(page);
            
        }

        public int GetCurentPageIndex()
        {
            if (PageList.Count == 0) return 0;
            int index = 0;
            foreach (var item in PageList)
            {
                if (item.IsSelected)
                {
                    return index;
                }
                index++;
            }
            return 0;
        }
        
        //private void CanDownloadUpdated(object sender,CanExecuteRoutedEventArgs e)
        //{
        //    e.CanExecute = true;
        //}   
        
        //private void DownloadUpdated(object sender,ExecutedRoutedEventArgs e)
        //{
        //    var _P = e.Parameter as Tuple<DownloadItem, IDownloadItemCallback>;

        //    var _target = GetTargetDownloadItemView(_P.Item1.Id);

        //    if (_target == null)
        //    {
        //        var _new = new DownloadItemViewModel();
        //        //_new.CallBack = _P.Item2;
        //        DownloadList.Add(_new);
        //        //_new.UpDate(_P.Item1);
        //        _target = _new;
        //        //初始时暂停，防止在后台向临时文件夹下载文件挤占C盘空间
        //        //_target.CallBack.Pause();
        //        _target.IsPaused = true;
        //    }
        //    else
        //    {
        //        //_target.UpDate(_P.Item1);
        //    }


        //    Console.WriteLine(_P.Item1.CurrentSpeed);
        //    if (_P.Item1.IsComplete) Console.WriteLine("Finished "+_P.Item1.FullPath);
        //}


        #endregion



        #region Handler
        //open url
        public void OpenTargetUrl(object sender, string url)
        {
            OpenUrl(url);
        }

        //download
        object DownloadListLocker = new object();

        private void WindowView_OnUpdated(object sender, object e)
        {
            var _source = ( DownloadItemViewModel )e;
            lock (DownloadListLocker)
            {
                var _target = GetTargetDownloadItemView(_source.ID);
                if(_target == null)
                {
                    App.Current.Dispatcher.Invoke(() => { DownloadList.Insert(0,_source); });
                    _target = _source;
                    //Console.WriteLine("Thread Id {1} insert {0}",_target.ID,Thread.CurrentThread.ManagedThreadId);
                }
                _target.UpDate(_source);

                if (_target.IsComplete)
                {
                    _target.customDownloadHandler.Completed(_target.ID);
                }
            }
        }

        public DownloadItemViewModel GetTargetDownloadItemView(int id)
        {

            foreach (var item in DownloadList)
            {
                if (item.ID == id)
                {
                    return item;
                }
            }
            return null;
        }
        
        //task
        private void Browser_StateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e)
        {
            if (e.IsLoading == false)
            {
                new Thread(
                   () =>
                   {
                       var w = sender as CustomChrominWebBrowser;
                       var infoTask = w.Info;
                       //获取对应View
                       var _TaskView = GetTaskViewModel(w);
                       if (_TaskView.Chrome == null) _TaskView.Chrome = w;

                       if (infoTask != null)
                       {
                           LogHelper.Debug("开始执行任务 ：" + infoTask.Action);
                           //设置显示标题
                           App.Current.Dispatcher.BeginInvoke(new Action(() =>
                           {
                               _TaskView.Url = w.Address;
                               _TaskView.Title = w.Title;
                           }));
                           //获取本地Tasksize
                           //if (_TaskView.TotalCount < w.TaskSize()) _TaskView.TotalCount = w.TaskSize();

                           //JQ环境检查与注入
                           var _jq = JSHelper.EnableJQ(e.Browser.MainFrame);
                           _jq.Wait();
                           if (!_jq.Result) JSHelper.InjuctJQ(e.Browser.MainFrame);


                           string url = w.Info.Action;//即使页面发生跳转也按指定的task进行解析
                                                      //App.Current.Dispatcher.Invoke((Func<string>)(() => { return w.Address; }));

                           //进行解析
                           var _result = ExcuteAnalysis(w, e.Browser.MainFrame, CmdHelper.GetRule(url), infoTask, _TaskView);
                           _result.Wait();
                           Console.WriteLine("Task 执行完毕 出错值为" + _result.Result);
                           w.Info = null;

                           App.Current.Dispatcher.BeginInvoke(new Action(() =>
                           {
                               //只获取当前存储路径
                               _TaskView.Path = infoTask.WorkDirectory;

                               if (_result.Result > 0) _TaskView.ErrorCount += 1;
                               else _TaskView.SuccessCount += 1;
                           }));

                           
                       }
                       //控制响应
                       if (_TaskView.IsPauseing)
                       {
                           _TaskView.IsPaused = true;
                           _TaskView.IsPauseing = false;
                           LogHelper.Debug(w.ID + " 任务已被用户主动暂停");
                           return;
                       }

                       if (_TaskView.IsStopping)
                       {
                           _TaskView.IsStopped = true;
                           _TaskView.IsStopping = false;
                           LogHelper.Debug(w.ID +" 任务已被用户主动停止");
                           return;
                       }
                       

                       if (w.TaskSize() > 0)
                       {
                           //if (_TaskView.TotalCount < w.TaskSize()) _TaskView.TotalCount = w.TaskSize();
                           //获取下一个解析任务
                           w.Info = w.PopTask();
                           
                           //跳转网页地址
                           App.Current.Dispatcher.Invoke(
                               (Action)(() =>
                               {
                                   w.Address = w.Info.Action;
                               }));

                           LogHelper.Debug(w.ID + " 已分配下一个内部任务 " + w.Info.Action);
                       }
                       else
                       {
                           App.Current.Dispatcher.BeginInvoke(new Action(() =>{
                               _TaskView.IsCompleted = true;

                               
                           }));
                           //Console.WriteLine("Finished ===> {0}", _TaskView.IsCompleted);
                           
                          
                           //重置以回收
                           //w.Reset();
                           BH.Reuse(w);
                           RemoveTaskBrowserView(w);
                           UpdateTasekViewUI();
                           LogHelper.Debug(w.ID + " 的内部任务已执行完毕,并已重置后回收到对象池");

                           //若向主任务队列中加入了子任务，而主任务分配线程已结束，需要重新启动分配线程
                           if (TaskHelper.HasTask() && !BK.IsBusy) BK.RunWorkerAsync();
                       }
                   }
                ).Start();
            }

        }

        #endregion
        
        #region Task
        //新建解析任务
        public void OpenTask(string url)
        {
            if (url==null || url=="") return;
            Models.Task task = new Models.Task
            {
                Action = url,
                WorkDirectory = BaseSavePath
            };
            TaskHelper.PushTask(task);
            TaskHelper.Finished = false;
            if (!BK.IsBusy) BK.RunWorkerAsync();
        }
        //解析Task
        public async Task<int> ExcuteAnalysis(CustomChrominWebBrowser w, CefSharp.IFrame frame, LinkedList<Models.Task> Rules, Models.Task infoTask, TaskViewModel taskViewModel)
        {
            int index = 0;
            int ErrorCount = 0;
            foreach (var item in Rules)
            {
                object  symbol = true;
                if(item.MatchSymbol!=null)
                    symbol =Convert.ToBoolean( await TaskHelper.Excute(frame, (string)item.MatchSymbol));
                
                //获取页面缓存数据集
                Dictionary<string, NetSource> FileData = ((CustomRequestHandler)w.RequestHandler).GetMemoryData();
               
                if ((bool)symbol)
                {
                    index++;
                    LogHelper.Debug(string.Format("规则[{0}]匹配标记 返回True，将执行此规则", item.Action));
                    //转换任务Task到解析Task
                    item.ConvertToRunTask(infoTask);
                    try
                    {
                        var _result = await TaskHelper.Excute(w, FileData, frame, item, taskViewModel);
                        if (_result)
                            LogHelper.Debug(string.Format("{0}完成解析任务{1}", w.ID, item.ID));
                        else
                            LogHelper.Debug(string.Format("{0}在解析任务{1}时出现错误",w.ID,item.ID));
                    }
                    catch (Exception e)
                    {
                        ErrorCount++;
                        LogHelper.Debug(string.Format("{0}在解析任务{1}时出现错误 Error:{2}", w.ID, item.ID,e.Message));
                    }

                }
                else
                {
                    LogHelper.Debug(string.Format("规则[{0}]匹配标记 返回False，将跳过此规则",item.Action));
                }
            }
            //清除页面文件的自定义缓存数据
            ((CustomRequestHandler)w.RequestHandler).ReleaseCustomData();

           if (Rules.Count==0)
            {
                LogHelper.Debug(string.Format("{0} 没有匹配的规则用于解析[{1}]",w.ID,w.Info?.Action));
            }
            return ErrorCount;
        }

        /// <summary>
        /// 从TaskVIewList中移除已完成Task的对象
        /// </summary>
        /// <param name="ce"></param>
        public void RemoveTaskBrowserView(CustomChrominWebBrowser ce)
        {
            ChrominViewModel target=null;
            foreach (var item in TaskList)
            {
                if (item.WebBrowser == ce) { target = item; break; }
            }
            if (target != null)App.Current.Dispatcher.Invoke(()=> { TaskList.Remove(target); });

        }

        private void BK_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           LogHelper.Debug("对象池循分配环结束");

        }
        //Task分配循环
        private void BK_DoWork(object sender, DoWorkEventArgs e)
        {
            while (TaskHelper.HasTask())
            {
                
                    CustomChrominWebBrowser browser = null; //尝试从对象池中获取一个可用对象

                    while (browser == null)
                    {
                        //任务队列还有任务
                        browser = BH.Get();
                        Thread.Sleep(2000);
                        //LogHelper.Debug("继续 等待可用对象");

                    }
                    {
                        var _MainTask = TaskHelper.PopTask();
                        if (_MainTask == null) return;
                        //LogHelper.Debug(string.Format("取得任务[{0}],及可用对象", _MainTask.Action));

                        //LogHelper.Debug(string.Format("池=>{1}/{0} ", BH.GetPoolSize(), BH.GetObjectSize()));
                        browser.Info = _MainTask;
                        //获取到可用对象则进行解析任务

                        browser.RequestHandler = new CustomRequestHandler();
                        if (browser.LifeSpanHandler == null) browser.LifeSpanHandler = LifeSpanHandler;
                        browser.LoadHandler = new CustomLoadHandler();
                        ((CustomLoadHandler)browser.LoadHandler).OnStateChange += Browser_StateChanged;

                        browser.FrameLoadStart -= Browser_FrameLoadStart;
                        browser.FrameLoadStart += Browser_FrameLoadStart;

                        var page = new ChrominViewModel(browser);
                        //PreLoad = page;
                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            browser.Address = browser.Info.Action;
                            TaskList.Add(page);
                        }));
                    }
            }

        }

        private void Browser_FrameLoadStart(object sender, FrameLoadStartEventArgs e)
        {
            var w = sender as CustomChrominWebBrowser;
            if (w != null && w.ID!=0)
            {
                GetTaskViewModel(w);
            }
        }

        /// <summary>
        /// 获取指定对象关联的TaskViewModel
        /// </summary>
        /// <param name="ccwb"></param>
        /// <returns></returns>
        object TaskListLocker = new object();
        private TaskViewModel GetTaskViewModel(CustomChrominWebBrowser ccwb)
        {
            lock (TaskListLocker)
            {
                for (int i = 0; i < TaskViewList.Count; i++)
                {
                    if (TaskViewList[i].ID == ccwb.ID) return TaskViewList[i];
                }
                var tvm = new TaskViewModel() { ID = ccwb.ID,TotalCount=1,Title=ccwb.Info.Action };//初始时必然是 一个task已经在执行中，所以totalcount应为1
                
                App.Current.Dispatcher.Invoke(()=>{ TaskViewList.Insert(0, tvm); });
                return tvm;
            }
        }
        private void UpdateTasekViewUI()
        {
             App.Current.Dispatcher.BeginInvoke(new Action(() => {
                    lock (TaskListLocker)
                    {
                        var _up = new TaskViewModel();
                        TaskViewList.Add(_up);
                        TaskViewList.Remove(_up);
                    }
             }));
        }

        #endregion
    }
}
