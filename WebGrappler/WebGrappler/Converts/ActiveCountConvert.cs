using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WebGrappler.Services;
using WebGrappler.ViewModels;

namespace WebGrappler.Converts
{
    class ActiveCountConvert : IMultiValueConverter

    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!string.IsNullOrWhiteSpace(parameter as string))
            {
                int count = 0;
                switch (parameter as string)
                {
                    case "Task":
                        count = TaskHelper.TaskMap.Count;
                        if (value[0] is IEnumerable)
                        {
                            foreach (var item in (IEnumerable)value[0])
                            {
                                var _item = (TaskViewModel)item;
                                if (!_item.IsErrored && !_item.IsStopped && !_item.IsCompleted) {/* Console.WriteLine("Task Count ++ =>" + count);*/ count++; }
                            }
                        }
                        //Console.WriteLine("Task Count " + count);
                        return count.ToString();
                    case "Download":

                        if (value[0] is IEnumerable)
                        {
                            foreach (var item in (IEnumerable)value[0])
                            {
                                var _item = (DownloadItemViewModel)item;
                                if (!_item.IsCancelled && !_item.IsComplete) count++;
                            }
                        }
                        return count.ToString();
                    default:
                        return "0";
                }
            }
            return "0";

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
