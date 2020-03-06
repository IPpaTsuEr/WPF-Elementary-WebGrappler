using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebGrappler.Services
{
    class IMMEHelper
    {
        private static Dictionary<string, string> TypeMap = new Dictionary<string, string>();
        static IMMEHelper()
        {
            TypeMap.Clear();

            if (File.Exists("./Sources/IMMEType.txt"))
            {
                var f = File.OpenText("./Sources/IMMEType.txt");
                var s = f.ReadToEnd();
                f.Close();
                var imme = s.Split(new []{ '\n'},StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in imme)
                {
                    var types = item.Split(new[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
                    if(!TypeMap.ContainsKey(types[1])) TypeMap.Add(types[1], types[0]);
                }
            }
            else
            {
                Console.WriteLine("IMME Type 文件不存在！");
            }
        }
        public static string GetExtentionType(string IMME)
        {
            if (IMME == null) return null;
            string type = null;
           if( TypeMap.TryGetValue(IMME, out type))return type;
            Console.WriteLine(" 没有此类型 "+IMME);
            return null;
        }

    }
}
