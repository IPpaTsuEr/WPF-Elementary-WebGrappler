using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WebGrappler.ViewModels;

namespace WebGrappler.Converts
{
    class TaskProgressConvert : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if ((int)values[2] == 0) return 12;
                var _r = ((int)values[0]+ (int)values[1]) / ((int)values[2]*1.0);
                if (_r == 0) return 12.0;
                return 360.0 * _r;
            }catch(Exception e)
            {
                return 12.0;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
