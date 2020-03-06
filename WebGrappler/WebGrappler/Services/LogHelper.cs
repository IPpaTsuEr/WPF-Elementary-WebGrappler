using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WebGrappler.Services
{
    class LogHelper
    {
        public static StreamWriter Loger;

        static LogHelper()
        {
            if(!Directory.Exists("./Log"))Directory.CreateDirectory("./Log");
            Loger = File.AppendText("./Log/"+DateTime.Today.ToLongDateString()+".log");
            Loger.AutoFlush = true;
        }
        ~LogHelper()
        {
            if (Loger != null) { Loger.Flush(); Loger.Close(); }
        }


        private static void _Base(string infotype,string msg)
        {
            Loger.Write("{0} {1}:{2}{3}",DateTime.Now.ToLongTimeString(),infotype,msg,"\n");
        }
        public static void Warring(string msg)
        {
            _Base("Warring ", msg);
        }
        public static void Error(string msg)
        {
            _Base("Error ", msg);
        }
        public static void Infomation(string msg)
        {
            _Base("Infomation ", msg);
        }
        public static void Flush()
        {
            Loger.Flush();
           
        }


        public static void Debug(string msg ="", bool ToLog = true , [CallerMemberName] string Caller = null, [CallerLineNumber] int Line = 0, [CallerFilePath] string Path = null )
        {
            Console.WriteLine("{0} Debug: {1} {2} 在{3}的{4}行", DateTime.Now.ToLongTimeString(), Caller,msg, Path, Line);
            if (ToLog)
            {
                Loger.Write("\n{0} Debug: {1} {2} 在{3}的{4}行", DateTime.Now.ToLongTimeString(), Caller,msg, Path, Line);
            }
        }
    }
}
