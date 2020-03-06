using CefSharp;
using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebGrappler.Services
{
    class JSHelper

    {

        public static string GetOriginal(string str)
        {
            if (str == null || str.Count() == 0) return "";
            if (str.First() == '"' && str.Last() == '"') return str.Substring(1, str.Count() - 2);
            return str;
        }

        public static string JQ = TaskHelper.LoadString("./Sources/JQ.txt");

        public static async Task<bool> EnableJQ(IFrame frame)
        {
            var t = await frame.EvaluateScriptAsync(
                @"(function(){
                    if(typeof jQuery == 'undefined')return false;
                    else return true;
                }());"
                ).ContinueWith(result=> {
                    if (result != null && result.Result.Success)
                    {
                        try
                        {
                            return (bool)result.Result.Result;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                    return false;
                });
            Console.WriteLine("Supoort JQ ="+t);
            return t;
        }

        public static void DisableAlter(IFrame frame)
        {
            DoFunc(frame, "window.alert = function() {return false;}");
        }

        public static void InjuctJQ(IFrame frame)
        {
            frame.ExecuteJavaScriptAsync(JQ);
            //frame.ExecuteJavaScriptAsync(@" (function(){ 
            //        var h = document.getElementsByTagName('head')[0];
            //        var s = document.createElement('script');
            //        s.src = '"+ JQuerySource + "';h.appendChild(s);}());");
        }

        public static void Remove(IFrame frame,string target)
        {
            DoFunc(frame,"$('"+target+"').remove();");
        }

        public static void Click(IFrame frame , string target)
        {
            frame.ExecuteJavaScriptAsync(@"(function(){$("+target+").click();}());");
        }

        public static async Task<List<string>> ClickWithFunc(IFrame frame, string target,string func,uint times,string beforLoop,string afterLoop,int delay=200)
        {
            List<string> result = new List<string>();
            object b;
            if(beforLoop!=null && beforLoop != "")
            {
                b = await DoFunc(frame, beforLoop);
                ConvertAndCollection(result,b);
            }
            for (int i = 0; i < times; i++)
            {
                Click(frame, target);
                if (delay > 0.0) Thread.Sleep(delay);
                if (func!=null && func!="") {
                    b = await DoFunc(frame, func);
                    ConvertAndCollection(result, b);
                }
            }
            if (afterLoop != null && afterLoop != "")
            {
               b= await DoFunc(frame, afterLoop);
                ConvertAndCollection(result, b);
            }
            return result;
        }

        private static void ConvertAndCollection(List<string> dock,object source)
        {
            if (source == null || dock == null) return;
            if(source is string) { dock.Add((string) source); }
            else if(source is List<string>) { dock.AddRange((List<string>)source); }
            else if(source is List<object>) {
                foreach (var item in (List<object>)source)
                {
                    dock.Add((string)item);
                }
            }else if (source is object) {  dock.Add((string)source); }

        }

        public static void KeyDown(IFrame frame,string target,int keyCode)
        {
            frame.ExecuteJavaScriptAsync(@"(function(){
                            var e = jQuery.Event('keydown');
                            e.keyCode = " + keyCode + @";
                            $('" + GetOriginal(target) + @"').trigger(e);}());");
            frame.ExecuteJavaScriptAsync(@"(function(){
                            var e = jQuery.Event('keyup');
                            e.keyCode = " + keyCode + @";
                            $('" + GetOriginal(target) + @"').trigger(e);}());");

        }

        public static void LoopKeyDown(IFrame frame, string target,int times,int delay, int keyCode = 34)
        {
            Thread.Sleep(delay * 2);
            for (int i = 0; i < times; i++)
            {
                frame.ExecuteJavaScriptAsync(@"(function(){
                    var e = jQuery.Event('keydown');
                    e.keyCode = " + keyCode + @";
                    $('" + GetOriginal(target) + @"').trigger(e);}());");
                Thread.Sleep(delay);
            }
        }

        public static List<string> MakeUrl(string rule,int start,int end)
        {
            List<string> result = new List<string>();
            for (int i = start; i <= end; i++)
            {
               result.Add(rule.Replace("#", i.ToString()));
            }
            return result;
        }

        public static async Task<object> DoFunc(IFrame fram,string func)
        {
            //Console.WriteLine("Excute Function {"+func+"}");
            func = GetOriginal(func);
            var t = await fram.EvaluateScriptAsync("(function(){" + func + "}());").ContinueWith(r => {
                if (r.Result.Success)
                {
                    LogHelper.Debug(string.Format("函数[{0}]执行成功，返回值为[{1}]",func,r.Result.Result));
                    return r.Result.Result;
                }
                else LogHelper.Debug(string.Format("函数[{0}]执行失败，{1}", func, r.Result.Message));
                return null;
            });
            return t;
        }

        public static void RollDown(IFrame frame,int YOffset, string target = null)
        {
            if(target!=null)
                frame.ExecuteJavaScriptAsync(@"(function(){$('"+target+"').scrollTo({top:" + YOffset + ",behavior:'smooth'});}());");
            else
                frame.ExecuteJavaScriptAsync(@"(function(){window.scrollTo({top:" + YOffset + ",behavior:'smooth'});}());");
        }

        public static async Task<object> GetBaseFunc(IFrame frame, string target, string functionString)
        {
            target = target.Replace("\"", ""); ;
            functionString = functionString.Replace("\"", "");
            var t = await frame.EvaluateScriptAsync("(function(){ return $('" + target + "')." + functionString + ";}());").ContinueWith(
                r =>
                {
                    var result = r.Result;
                    if (result != null && result.Success)
                    {
                        LogHelper.Debug(string.Format("函数 {0} 执行成功 结果为{1}"," function(){ return $('" + target + "')." + functionString + ";}",result.Result));
                        return result.Result;
                    }
                    LogHelper.Debug( "GetBaseFunc Error  " + r.Result.Message);
                    return null;
                });
            return t;

        }
        public static async Task<object> GetAttr(IFrame frame, string target,string str)
        {
            return await GetBaseFunc(frame,target, "attr('" + str + "')");

        }
        public static async Task<object> GetValue(IFrame frame, string target)
        {
            return await GetBaseFunc(frame, target, "val()");
            //var t = await frame.EvaluateScriptAsync(@"(function(){return $('" + target + "'.val();});").ContinueWith(
            //    r=> {
            //        var result = r.Result;
            //        if (result != null && result.Success)
            //        {
            //            return result.Result;
            //        }
            //        return null;
            //    });
            //return t;
        }
        public static async Task<string> GetText(IFrame frame, string target)
        {
            return (string)await GetBaseFunc(frame, target, "text()");
            //string result = await  frame.EvaluateScriptAsync(@"(function(){return $('" + target + "').text();}());").ContinueWith( r => {
            //    var t = r.Result;
            //    if (t != null && t.Success)
            //    {
            //        return (string)t.Result;
            //    }
            //    return null;
            //});
            //return result;
        }
        public static async Task<string> GetHtml(IFrame frame, string target)
        {
            return (string)await GetBaseFunc(frame, target, "html()");
            //var t = frame.EvaluateScriptAsync(@"(function(){return $('" + target + "').html();}());");
            //t.Wait();
            //var result = t.Result;
            //if (result != null && result.Success)
            //{
            //    return (string)result.Result;
            //}
            //return null;
        }
        public static async Task<int> GetArrayCount(IFrame frame, string target)
        {
            try
            {
                return Convert.ToInt32(await GetBaseFunc(frame, target, "size()"));
            }
            catch(Exception er) {
                Console.WriteLine("获取["+target+"]对象的个数时出错 ： "+er.Message);
                return -1;
            }

        }

        public static async Task<System.Collections.IList> GetArrayBase(IFrame frame, string target, string functionString)
        {
            target = target.Replace("\"","");
            functionString = functionString.Replace("\"","");
            
            var t = await frame.EvaluateScriptAsync(@"(function(){ var list = []; $('" + target + "').each(function(){ list.push($(this)." + functionString + ");}); return list;}());").ContinueWith(
                    ts =>
                    {
                        if (ts != null && ts.Result.Success)
                        {
                            LogHelper.Debug(string.Format("函数 {0} => {1} 执行成功 结果为{2}", functionString , target, ts.Result));
                            return (System.Collections.IList)ts.Result.Result;
                        }
                        else
                        {
                            Console.WriteLine("GetArrayBase Error : " + ts.Result.Message);

                        }
                        return null;
                    });
            return t;
        }
        public static async Task<System.Collections.IList> GetArraryAttr(IFrame frame,string target, string attr)
        {
           return await GetArrayBase(frame,target,"attr('"+attr+"')");
            //var t = await frame.EvaluateScriptAsync(@"(function(){
            //            var list = []; 
            //            $('"+ target +@"').each(function(){
            //                list.push($(this).attr('"+attr+@"'));
            //           });
            //            return list;}());").ContinueWith(
            //    ts=> {
            //    if (ts != null && ts.Result.Success)
            //    {
            //        return (System.Collections.IList)ts.Result.Result;
            //    }
            //    return null;
            //});
            //return t;
        }
        public static async Task<System.Collections.IList> GetArraryText(IFrame frame,string target)
        {
            return await GetArrayBase(frame, target, "text()");
            //var t = frame.EvaluateScriptAsync(@"(function(){
            //            var list = []; 
            //            $('"+ target +@"').each(function(){
            //                list.push($(this).text());
            //            });
            //            return list;}());");
            //t.Wait();
            //var result = t.Result;
            //if (result != null && result.Success)
            //{
            //    return (System.Collections.IList)result.Result;
            //}
            //return null;
        }
        public static async Task<System.Collections.IList> GetArraryValue(IFrame frame,string target)
        {
            return await GetArrayBase(frame, target, "val()");
            //var t = frame.EvaluateScriptAsync(@"(function(){
            //            var list = []; 
            //            $('"+ target +@"').each(function(){
            //                list.push($(this).val());
            //            });
            //            return list;}());");
            //t.Wait();
            //var result = t.Result;
            //if (result != null && result.Success)
            //{
            //    return (System.Collections.IList)result.Result;
            //}
            //return null;
        }
        public static async Task<System.Collections.IList> GetArraryHtml(IFrame frame,string target)
        {
            return await GetArrayBase(frame, target, "html()");
            //var t = frame.EvaluateScriptAsync(@"(function(){
            //            var list = []; 
            //            $('"+ target +@"').each(function(){
            //                list.push($(this).html());
            //            });
            //            return list;}());");
            //t.Wait();
            //var result = t.Result;
            //if (result != null && result.Success)
            //{
            //    return (System.Collections.IList)result.Result;
            //}
            //return null;
        }
    }
}
