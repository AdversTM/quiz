using System;
using System.Globalization;
using System.Windows.Data;

namespace generator.util {
    [ValueConversion(typeof(int), typeof(string))]
    public class TimeConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return TextUtil.TimeToStr(value is long v ? v : 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}