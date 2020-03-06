using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WebGrappler.Converts
{
    class DownloadTimeConvert : IValueConverter

    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is double)
            {
                var _v = (double)value;

                if (_v <= 0) return " 未知";
                else if (_v <= 60) return string.Format(" 还需{0}秒", _v);
                else if (_v <= 60 * 60) return string.Format(" 还需{1}分{0}秒", _v / 60, _v % 60);
                else if (_v <= 60 * 60 * 24)
                {
                    var _d = _v % 3600;
                    return string.Format(" 还需{2}小时{1}分{0}秒", _d % 60, _d / 60, _v / 3600);
                }
                else return " 超过1天";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
