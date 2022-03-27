using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;
using System.Globalization;

namespace RestClientLibrary.Screen
{
    [ValueConversion(typeof(bool), typeof(WindowState))]
    public class BoolToWindowStateConverter : IValueConverter
    {
        public Xceed.Wpf.Toolkit.WindowState TrueValue { get; set; }
        public Xceed.Wpf.Toolkit.WindowState FalseValue { get; set; }

        public BoolToWindowStateConverter()
        {
            // set defaults
            TrueValue = Xceed.Wpf.Toolkit.WindowState.Open;
            FalseValue = Xceed.Wpf.Toolkit.WindowState.Closed;
        }

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                return FalseValue;
            return (bool)value ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (Equals(value, TrueValue))
                return true;
            if (Equals(value, FalseValue))
                return false;
            return null;
        }
    }
}
