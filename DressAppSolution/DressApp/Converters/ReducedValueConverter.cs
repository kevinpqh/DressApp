using System;
using System.Windows.Data;

namespace DressApp.Converters
{
    /// Converts a given double value by substracting it by double parameter amount

    public class ReducedValueConverter : IValueConverter
    {
        // Valor

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (double.Parse(value.ToString()) - double.Parse(parameter.ToString()));
        }

        // Convertidr a valores.

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
