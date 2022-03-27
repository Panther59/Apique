using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;
using System.Globalization;

namespace DataLibrary
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class ToggleConverter : IValueConverter
    {

        public ToggleConverter()
        {
        }

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                return false;
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                return false;
            return !(bool)value;
        }
    }
}
