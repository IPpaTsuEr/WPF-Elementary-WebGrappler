using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebGrappler.Models;

namespace WebGrappler.Services
{
    class CmdHelper
    {
        static Dictionary<string, LinkedList<Models.Task>> Rule = new Dictionary<string, LinkedList<Models.Task>>();

        static Dictionary<string, Models.Task> IDList = new Dictionary<string, Models.Task>();

        public static void AddRule(Models.Task task)
        {

            if (Rule.TryGetValue(task.Action, out LinkedList<Models.Task> refrenc))
            {
                refrenc.AddLast(task);
            }
            else
            {
                refrenc = new LinkedList<Models.Task>();
                refrenc.AddLast(task);
                Rule.Add(task.Action, refrenc);
            }
            if (!String.IsNullOrWhiteSpace(task.ID))
            {
                if (IDList.ContainsKey(task.ID))
                {
                    LogHelper.Debug(String.Concat(task.ID,task.Action, "多个解析规则具有相同的Id，当重定向时只有第一个生效"));
                }
                else
                {
                    IDList.Add(task.ID, task);
                }
            }

        }

        public static LinkedList<Models.Task> GetRule(string str)
        {
            LinkedList<Models.Task> result = new LinkedList<Models.Task>();
            if (String.IsNullOrWhiteSpace(str)) return result;

            foreach (var item in Rule)
            {
                if (Regex.IsMatch(str,item.Key))
                {
                    foreach (var sub in item.Value)
                    {
                        result.AddLast(sub.Clone());
                    }
                }
            }
            return result;
        }

        public static bool CanAnalysis(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return false;

            foreach (var item in Rule)
            {
                if (Regex.IsMatch(url, item.Key)) return true;
            }
            return false;
        } 

        public static bool Load(string path)
        {
            if (File.Exists(path))
            {
                var f = File.OpenText(path);
                var str = f.ReadToEnd();
                f.Close();

                var reg = new Regex(@"{((?>[^{}]+|{(?<D>)|}(?<-D>))*(?(D)(?!)))}");
                var m = reg.Matches(str);

                Dictionary<string, Models.Task> _TempIDList = new Dictionary<string, Models.Task>();

                foreach (Match item in m)
                {
                    AnalysisCmdString(item.Groups[1].Value.Trim(), _TempIDList);
                }
                //处理有BaseOn标志的Task
                foreach (var sub in _TempIDList)
                {
                    if (IDList.TryGetValue(sub.Key, out Models.Task _baseTask))
                    {
                        _baseTask.BaseTo(sub.Value);
                        AddRule(sub.Value);
                    }
                    else
                    {
                        LogHelper.Debug(string.Concat("没有找到具有指定ID的规则：", sub.Key));
                    }
                }
                return true;
            }
            else
            {
                LogHelper.Debug(string.Concat("没有找到指定的规则文件:",path));
                return false;
            }
        }

        public static void AnalysisCmdString(string str, Dictionary<string, Models.Task> baseonList)
        {
            var reg = new Regex(@"([^=;]+)=(.+);");
            Models.Task task = new Models.Task();
            str = str.Replace("\n","").Replace("\t","");
            //str = GroupReplace(str, '\t', "", '"', 2);
            //str = GroupReplace(str, '\n', "", '"', 2);
            foreach (var item in GroupSplit(str))
            {
                var m = reg.Matches(item);
                foreach (Match sub in m)
                {
                    var func = sub.Groups[1].Value;
                    var para = sub.Groups[2].Value;
                    task.ExcuteSequence += func + ";";
                    switch (func)
                    {
                        case "Action":
                            task.Action = para;
                            break;
                        case "DataType":
                            task.DataType = (FocuseOnType)Enum.Parse(typeof(FocuseOnType), para.ToUpper());
                            break;
                        case "AssignTo":
                            task.AssignTo = (FocuseToType)Enum.Parse(typeof(FocuseToType),para.ToUpper());
                            break;
                        case "DataReverse":
                            try { task.ReverseData = Convert.ToBoolean(para); } catch (Exception) { task.ReverseData = false; }
                            break;
                        case "ExDo":
                            task.ExDo = para;
                            break;
                        case "Data":
                            task.Data = para;
                            break;
                        case "MatchSymbol":
                            task.MatchSymbol = para;
                            break;
                        case "Title":
                            task.Title = para;
                            break;
                        case "ID":
                            task.ID = para;
                            break;
                        case "BaseOn":
                            task.BaseOn = para;
                            break;
                        case "AttachName":
                            task.AttachName = para;
                            break;
                        default:
                            break;
                    }
                }

            }
            task.CMD = str;
            if (!string.IsNullOrWhiteSpace(task.BaseOn))
            {
                baseonList.Add(task.BaseOn, task);
            }
            else
            {
                AddRule(task);
            }

            
        }

        public static List<string> GroupSplit(string str,char GroupBegin = '(',char GroupEnd = ')',char FinishSymbol=';')
        {
            int MarkGroup = 0;
            List<string> result = new List<string>();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                var item = str[i];
                sb.Append(item);
                if (item == GroupBegin) { MarkGroup++; }
                else if (item == GroupEnd) { MarkGroup--; }
                else if (item == FinishSymbol) {
                    if (MarkGroup == 0)
                    {
                        result.Add(sb.ToString());
                        sb.Clear();
                    }
                }
            }
            return result;
        }

        public static List<string> GroupSplit(string str,char SplitChar,char GroupChar,int TimesSwitch)
        {
            int GroupSymboyCount = 0;
            List<string> result = new List<string>();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == GroupChar)
                {
                    GroupSymboyCount++;
                }
                if (str[i] == SplitChar)
                {
                    if (GroupSymboyCount % TimesSwitch == 0)
                    {
                        result.Add(sb.ToString());
                        sb.Clear();
                    }
                    else
                    {
                        sb.Append(str[i]);
                    }
                }
                else
                {
                    sb.Append(str[i]);
                }
            }
            if (sb.Length > 0) result.Add(sb.ToString());
            return result;
        }

        public static string GroupReplace(string str,char target,string placement, char GroupChar, int TimesSwitch)
        {
            int SwitchCount = 0;
            StringBuilder sb = new StringBuilder();
            foreach (var item in str)
            {
                if (item == GroupChar) SwitchCount++;
                if(item==target && SwitchCount % TimesSwitch == 0)
                {
                    if(placement!=null) sb.Append(placement);
                }
                else
                {
                    sb.Append(item);
                }
            }
            return sb.ToString();
        }

        public static string GroupReplace(string str,char target,string placement, char GroupBegin = '(', char GroupEnd = ')')
        {
            int SwitchCount = 0;
            StringBuilder sb = new StringBuilder();
            foreach (var item in str)
            {
                if (item == GroupBegin) SwitchCount++;
                else if (item == GroupEnd) SwitchCount--;

                if (item == target && SwitchCount == 0)
                {
                    if (placement != null) sb.Append(placement);
                }
                else
                {
                    sb.Append(item);
                }
            }
            return sb.ToString();
        }

        public static bool IsStringParameter(string str)
        {
            int count = 0;
            if (string.IsNullOrWhiteSpace(str)) LogHelper.Debug("规则执行参数为空，可能导致异常");
            if (str.StartsWith("\"") && str.EndsWith("\""))
            {
                foreach (var item in str)
                {
                    if (item == '"')
                    {
                        count++;
                        if (count > 2) return false;
                    }
                }
                return true;
            }
            return false;
        }

    }

}
