using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WebGrappler.Converts
{
    class CountVisiableConvert : IValueConverter

    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                if ((string)value == "0") return Visibility.Hidden;
                else return Visibility.Visible;
            }
            else if(value is int)
            {
                if ((int)value <= 0) return Visibility.Hidden;
                else return Visibility.Visible;
            }
            else if(value is double)
            {
                if ((double)value <= 0) return Visibility.Hidden;
                else return Visibility.Visible;
            }
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
