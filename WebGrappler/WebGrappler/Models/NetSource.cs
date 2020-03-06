using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WebGrappler.Models
{
    class NetSource
    {
        public long BrowserID { get; set; }
        public string IMME { get; set; }
        public MemoryStream Data { get; set; }


        public static string GetTypeFromUrl(string url)
        {
            Regex rg = new Regex("(ht|f)tp(s?)\\:\\/\\/[0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*(:(0-9)*)*(\\/?)([a-zA-Z0-9\\-\\.\\?\\,\\'\\/\\\\+&amp;%\\$#_]*)?");
            var m = rg.Match(url);
            if (m.Success)
            {
                var c = m.Groups[7].Value;
                var mt = Regex.Match(c, "[^.]+(\\.[0-9A-Za-z]+)");
                if (mt.Success) return mt.Groups[1].Value;
            }
            return ".unknow";
        }


    }
}
