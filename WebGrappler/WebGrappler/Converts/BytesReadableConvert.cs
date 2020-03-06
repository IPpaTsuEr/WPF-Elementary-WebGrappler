using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WebGrappler.Converts
{
    class BytesReadableConvert : IValueConverter
    {
        public string formatString = "{0:F1}";
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value!=null)
            {
                double _bytes;
                if (value is long) _bytes = (long)value;
                else if (value is double) _bytes = (double)value;
                else return "N/A";
                if (_bytes < 1024) return string.Concat(String.Format(formatString, _bytes),"Byte");
                else if(_bytes < 1024 * 1024)return string.Concat(String.Format(formatString,_bytes /1024), "KB");
                else if(_bytes < 1024 * 1024 * 1024) return string.Concat(String.Format(formatString, _bytes /(1024*1024)), "MB");
                else return string.Concat(String.Format(formatString, _bytes /(1024*1024 * 1024)), "GB");
            }
            return "N/A";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
