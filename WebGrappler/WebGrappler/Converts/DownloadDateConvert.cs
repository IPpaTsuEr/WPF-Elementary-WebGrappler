using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WebGrappler.Converts
{
    class DownloadDateConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var _d = (DateTime)value;
                if (_d.Day == DateTime.Now.Day) { return _d.ToShortTimeString(); }
                else return _d.ToShortDateString();
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
