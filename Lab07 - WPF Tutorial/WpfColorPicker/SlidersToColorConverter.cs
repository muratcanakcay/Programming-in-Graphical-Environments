using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfColorPicker
{
    public class SlidersToColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var red = System.Convert.ToByte(values[0]);
            var green = System.Convert.ToByte(values[1]);
            var blue = System.Convert.ToByte(values[2]);

            return new SolidColorBrush(Color.FromArgb(255, red, green, blue));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}