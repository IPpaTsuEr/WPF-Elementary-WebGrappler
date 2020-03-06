using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WebGrappler.Services
{
    class IconHelper
    {
        private static Dictionary<string, BitmapImage> IconMap = new Dictionary<string, BitmapImage>();

        public static BitmapImage GetIcon(string filepath)
        {
            if (string.IsNullOrWhiteSpace(filepath)) return null;
            var _type = GetFileTye(filepath);
            if (IconMap.ContainsKey(_type)) return IconMap[_type];
            else
            {
                var _bm = Icon.ExtractAssociatedIcon(filepath).ToBitmap();
                MemoryStream ms = new MemoryStream();
                _bm.Save(ms,ImageFormat.Png);
                BitmapImage bim = new BitmapImage();
                bim.BeginInit();
                bim.CacheOption = BitmapCacheOption.OnLoad;
                bim.StreamSource = ms;
                bim.EndInit();
                bim.Freeze();
                IconMap.Add(_type, bim);
                return bim;
            }
            
        }

        public static string GetFileTye(string filepath)
        {
            var _index = filepath.LastIndexOf(".");
            if (_index < 0) return filepath;
            return filepath.Substring(_index);
        }
    }
}
