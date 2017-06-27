using System;
using System.Windows.Data;

namespace DressApp.Converters
{
    // Convierte los valores agregando el valor de los parametros
    class IncreasedValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double number;
            double.TryParse((string)parameter, out number);

            return (double.Parse(value.ToString()) + number);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
