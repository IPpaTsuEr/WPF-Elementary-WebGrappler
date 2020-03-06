using CefSharp;
using CefSharp.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WebGrappler.Models;
using WebGrappler.ViewModels;

namespace WebGrappler.Services
{
    class TaskHelper
    {
        private static object locker = new object();

        //public static LinkedList<Models.Task> TaskList = new LinkedList<Models.Task>();
        public static Dictionary<string, Models.Task> TaskMap = new Dictionary<string, Models.Task>();

        public static Models.Task PopTask()
        {
            lock (locker)
            {
                Models.Task t = null;
                //if (TaskList.Count > 0)
                //{
                    

                //    t = TaskList.First();
                //    TaskList.RemoveFirst();
                //}
                if (TaskMap.Count > 0)
                {
                    var _t = TaskMap.First();
                    t = _t.Value;
                    TaskMap.Remove(_t.Key);
                }
                return t;
            }
        }

        public static void PushTask(Models.Task task)
        {
            lock (locker)
            {
                if (!TaskMap.ContainsKey(task.Action))
                    TaskMap.Add(task.Action,task);
                else {
                    LogHelper.Debug(string.Format("队列中已存在任务{0}",task.Action));
                }
                // TaskList.AddLast(task);
            }

        }

        public static  bool Finished = false;

        public static bool HasTask()
        {
            lock (locker)
            {
                //if (TaskList.Count != 0) return true;
                //if (TaskList.First() != null) return false;
                if (TaskMap.Count != 0) return true;
                return false;
            }

        }

        public static void Clear()
        {
            TaskMap.Clear();
        }


        public static async Task<bool> Excute(CustomChrominWebBrowser w,Dictionary<string, NetSource> memorydata ,IFrame frame, Models.Task task,TaskViewModel taskViewModel = null)
        {
            var cmds = task.ExcuteSequence.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            //按解析规则顺序 依次执行对应函数
            object data = null;
            string title = null;
            try
            {
                foreach (var item in cmds)
                {
                    switch (item)
                    {
                        case "Title":
                            LogHelper.Debug(w.ID + " 解析Title " + task.Title);
                            title = (string) await Excute(frame, task.Title);
                            title = title.Trim();
                            break;
                        case "Data":
                            LogHelper.Debug(w.ID + " 解析Data " + task.Data);
                            data = await Excute(frame, (string)task.Data);
                            if (data is List<object>)
                            {
                                List<string> temp = new List<string>();
                                foreach (var sub in (List<object>)data)
                                {
                                    temp.Add((string)sub);
                                }
                                data = temp;
                            }
                            break;
                        case "ExDo":
                            await Excute(frame, (string)task.ExDo);
                            break;
                        default:
                            break;
                    }
                }
            }catch(Exception Excerr)
            {
                LogHelper.Debug(string.Format("解析任务[{0}]时出错，请检查解析规则是否正确。错误信息：{1}",task.Action ,Excerr.Message));
                return false;
            }
            
            //处理最终生成的数据
            int index = 0;

            switch (task.DataType)
            {
                case FocuseOnType.URL:

                    if(data is string)
                    {
                        //传递生成的子任务数量
                        if(taskViewModel!=null && task.AssignTo != FocuseToType.MAIN) taskViewModel.TotalCount += 1;
                        MakeSubTask(w, task,title, (string)data, task.Index);
                    }
                    else
                    {
                        index = 0;

                        if (task.ReverseData) {((List<string>)data).Reverse(); }

                        foreach (var sub in (IList)data)
                        {
                            var url = (string)sub;
                            MakeSubTask(w,task, title, url, index);
                            index++;
                        }
                        //传递生成的子任务数量
                        if (taskViewModel != null && task.AssignTo != FocuseToType.MAIN) taskViewModel.TotalCount += index;
                    }

                    break;
                case FocuseOnType.DATA:
                    if(data is string)
                    {
                        var url = (string)data;
                        if (memorydata.ContainsKey(url))
                        {
                            var md = memorydata[url];
                            SaveMemoryToFile(md,GetPath( task.WorkDirectory,title), url,task.AttachName);
                        }
                        else
                        {
                            MissSourceSaveToFile(GetPath(task.WorkDirectory, title), url, task.AttachName);
                        }
                    }
                    else
                    {
                        index = 0;
                        foreach (var item in (IList)data)
                        {
                            var url = (string)item;
                            if (memorydata.ContainsKey(url))
                            {
                                var md = memorydata[url];
                                SaveMemoryToFile(md, GetPath(task.WorkDirectory,string.Format("{0} {1}",task.Index,title)), url, task.AttachName, index.ToString());
                            }
                            else
                            {
                                MissSourceSaveToFile(GetPath(task.WorkDirectory, string.Format("{0} {1}", task.Index, title)), url, task.AttachName, index.ToString());
                                //LogHelper.Debug(w.ID + " 没有找到指定的资源 " + url);
                            }
                            index++;
                        }
                    }

                    break;
                case FocuseOnType.TEXT:
                    if(data is string)
                    {
                        SaveTextToFile((string)data, GetPath(task.WorkDirectory, title), task.AttachName);
                    }
                    else
                    {
                        index = 0;
                        foreach (var item in (IEnumerable)data)
                        {
                            var _data = item as string;
                            SaveTextToFile((string)_data, GetPath(task.WorkDirectory, string.Format("{0} {1}",task.Index,title)),task.AttachName , index.ToString());
                            index++;
                        }
                    }
                    
                    break;
                default:
                    break;
            }
            LogHelper.Debug(String.Concat( "任务 " , task.Action ," 执行完毕 "));
            return true;
        }

        static string _Replace = new string(Path.GetInvalidPathChars())+ ":?*";

        private static string GetPath(string item1,string item2)
        {
            foreach (var item in _Replace)
            {
                item2 = item2.Replace(item.ToString()," ");
            }
            return Path.Combine(item1, item2.Replace("  ", ""));
        }

        public static void SaveMemoryToFile(NetSource ms,string WorkPath,string Url,string  Attachname ,string index = null)
        {
            if (ms.Data.Length > 0)
            {
               // if (!WorkPath.EndsWith("/")) WorkPath += "/";

                try
                {
                    if (!Directory.Exists(WorkPath))
                    Directory.CreateDirectory(WorkPath);

                    var ext = IMMEHelper.GetExtentionType(ms.IMME);
                    if (ext == null) ext = NetSource.GetTypeFromUrl(Url);

                    if (!string.IsNullOrWhiteSpace(Attachname))
                    {
                        File.WriteAllBytes(GetPath(WorkPath, (string.IsNullOrWhiteSpace(index) ? Attachname : Attachname + index) + ext), ms.Data.ToArray());
                    }
                    else
                    {
                        File.WriteAllBytes(GetPath(WorkPath, (string.IsNullOrWhiteSpace(index) ? DateTime.Now.Ticks.ToString() : index) + ext), ms.Data.ToArray());

                    }
                   // LogHelper.Debug(string.Format("写入文件[{0}/{1}{2}{3}]成功",WorkPath,Attachname,index,ext));
                }catch(Exception e)
                {
                    LogHelper.Debug(string.Format("写入文件[{0}/{1}{2}]失败，{3}", WorkPath, Attachname, index,e.Message));
                }
            }
            else
            {
                LogHelper.Debug(string.Format("指定数据长度为0，文件写入[{0}/{1}]失败,index={2},data={3}", WorkPath, Attachname,index,Url));
            }
        }

        public static void MissSourceSaveToFile(string WorkPath, string Url, string Attachname, string index = null, string filetype=".jpg")
        {
            new Thread(()=> {
                WebClient wb = new WebClient();
                string ext = NetSource.GetTypeFromUrl(Url);
                if (ext == ".unknow") ext = filetype;

                try
                {
                    if (!Directory.Exists(WorkPath))
                        Directory.CreateDirectory(WorkPath);

                    if (!string.IsNullOrWhiteSpace(Attachname))
                    {
                        wb.DownloadFile(Url, GetPath(WorkPath, (string.IsNullOrWhiteSpace(index) ? Attachname : Attachname + index) + ext));
                    }
                    else
                    {
                        wb.DownloadFile(Url, GetPath(WorkPath, (string.IsNullOrWhiteSpace(index) ? DateTime.Now.Ticks.ToString() : index) + ext));
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Debug(string.Format(@"尝试获取遗失文件{4}时出错：写入文件[{0}\{1}{2}{5}]失败，{3}", WorkPath, Attachname, index, e.Message, Url,ext));
                }
            }).Start();
        }

        public static void MakeSubTask(CustomChrominWebBrowser w,Models.Task task,string title ,string url,int taskIndex)
        {
            if (url == null)
            {
                LogHelper.Debug(String.Concat(w.ID , " 产生了空的子url " , task.Action));
                return;
            }
            if (!url.StartsWith(@"http://") && !url.StartsWith(@"https://") )
            {
                string i = GetDomain(task.Action,true);
                url = i + url;
            }

            Models.Task subtask = new Models.Task
            {
                Action = url,
                Index = taskIndex,
                WorkDirectory = GetPath(task.WorkDirectory,title)
            };

            if (task.AssignTo == FocuseToType.MAIN)
            {
                PushTask(subtask);
                LogHelper.Debug("向主队列添加任务 "+ subtask.Action);
            }
            else
            {
                w.PushTask(subtask);
                LogHelper.Debug("向本地列添加任务 " + subtask.Action);

            }


        }

        public static void SaveTextToFile(string data,string WorkPath,string AttachName ,string index=null)
        {
            //if (!WorkPath.EndsWith("/")) WorkPath = string.Concat(WorkPath, "/");
            try
            {
                if (!Directory.Exists(WorkPath)) Directory.CreateDirectory(WorkPath);

                if (!string.IsNullOrWhiteSpace(AttachName))
                {
                    File.AppendAllText(GetPath(WorkPath, (string.IsNullOrWhiteSpace(index) ? AttachName : AttachName + index) + ".txt"), data);
                }
                else
                {
                    File.AppendAllText(GetPath(WorkPath, (string.IsNullOrWhiteSpace(index) ? DateTime.Now.Ticks.ToString() : index) + ".txt"), data);
                }
            }catch(Exception e)
            {
                LogHelper.Debug(String.Format("保存文件出错:{0}",e.Message));
            }
 

        }

        public static async Task<object> Excute(IFrame frame , string str)
        {
            if (CmdHelper.IsStringParameter(str)) return str;

            Regex re = new Regex(@"([^\(\)]+)\((.*)\)");//***(***)
            if (!re.IsMatch(str)) return str;

            var m = re.Matches(str);

            foreach (Match item in m)
            {
                var function = item.Groups[1].Value.Trim();
                function = function.Replace("\n", "").Replace("\t","");
                // CmdHelper.GroupReplace(function, '\t', "", '"', 2);  function = CmdHelper.GroupReplace(function, '\n', "", '"', 2);
                var parameter = item.Groups[2].Value;

                LogHelper.Debug("执行函数 "+function +" 参数为 "+parameter);

                var list = CmdHelper.GroupSplit(parameter, ',', '"', 2);//按，分割字符串，并忽略在闭合的""中的，

                switch (function)
                {
   
                    case "Dofunc":
                        //var para = GetOriginal();
                        return await JSHelper.DoFunc(frame, (string)await Excute(frame,list[0]));
                        
                    case "GetText":
                        return await JSHelper.GetText(frame,(string)( await Excute(frame,list[0])));
                       
                    case "GetHtml":
                        return await JSHelper.GetHtml(frame, (string)await Excute(frame, list[0]));
                    case "GetValue":
                        return await JSHelper.GetValue(frame, (string)await Excute(frame, list[0]));
                        
                    case "GetAttr":
                        return await JSHelper.GetAttr(frame, (string)await Excute(frame, list[0]), (string)await Excute(frame, list[1]));
                    case "GroupText":
                        return await JSHelper.GetArraryText(frame, (string)await Excute(frame, list[0]));
                        
                    case "GroupHtml":
                        return await JSHelper.GetArraryHtml(frame, (string)await Excute(frame, list[0]));
                        
                    case "GroupAttr":
                        return await JSHelper.GetArraryAttr(frame, (string)await Excute(frame, list[0]), (string)await Excute(frame, list[1]));
                        
                    case "GroupCount":
                        return await JSHelper.GetArrayCount(frame, (string)await Excute(frame, list[0]));
                        
                    case "GroupValue":
                        return await JSHelper.GetArraryValue(frame, (string)await Excute(frame, list[0]));
                    case "Click":
                        JSHelper.Click(frame, (string)await Excute(frame, list[0]));
                        break;

                    case "KeyDown":
                        JSHelper.KeyDown(frame, (string)await Excute(frame, list[0]), Convert.ToInt32(await Excute(frame, list[1])));
                        break;

                    case "DisableAlter":
                        JSHelper.DisableAlter(frame);
                        break;
                    case "Remove":
                        JSHelper.Remove(frame,(string) await Excute(frame,list[0]) );
                        break;
                    case "RollDown":
                        var yoffset = Convert.ToInt32(Excute(frame, list[0]));
                        if(list.Count==1)JSHelper.RollDown(frame, yoffset);
                        else JSHelper.RollDown(frame, yoffset,(string)await Excute(frame,list[1]));
                        break;
                    case "ClickWithFunc":
                        if (list.Count == 6)
                            return await JSHelper.ClickWithFunc(frame, 
                                list[0],
                                list[1],
                                Convert.ToUInt32(list[2]),
                                list[3],
                                list[4],
                                Convert.ToInt32(list[5])
                                );
                        else
                            return await JSHelper.ClickWithFunc(frame,
                                list[0],
                                list[1],
                                Convert.ToUInt32(list[2]),
                                list[3],
                                list[4]
                                );
                    case "MakeUrl":
                        return JSHelper.MakeUrl((string)await Excute(frame, list[0]), Convert.ToInt32(await Excute(frame, list[1])), Convert.ToInt32(await Excute(frame, list[2])));
                    case "LoopKeyDown":
                        try
                        {
                            if (list.Count == 4)
                              JSHelper.LoopKeyDown(frame, list[0], Convert.ToInt32(await Excute(frame, list[1])), Convert.ToInt32(list[2]), Convert.ToInt32(list[3]));
                            else
                              JSHelper.LoopKeyDown(frame, list[0], Convert.ToInt32(await Excute(frame, list[1])), Convert.ToInt32(list[2]));

                        }
                        catch (Exception rke)
                        {
                            Console.WriteLine(rke.Message);
                        }
                         break;
                    default:

                        break;
                }
            }
            return null;
        }

        static Regex UrlDomainRegex = new Regex("(http|https|ftp)(://)([^/]*)",RegexOptions.IgnoreCase);

        public static string GetDomain(string url,bool WithHeard = false)
        {
            var mc = UrlDomainRegex.Match(url);
            if (mc.Success == false) return "";
            if(WithHeard) return mc.Groups[1].Value+ mc.Groups[2].Value + mc.Groups[3].Value;
            return mc.Groups[3].Value;

        }
        
        public static string LoadString(string path)
        {
            var f = File.OpenText(path);
            var t = f.ReadToEnd();
            f.Close();
            return t;
        }
        
    }
}
