using System.Diagnostics;
using System.Windows.Data;

namespace DressApp.Converters
{
    // Debug converter
    // TODO ... Cuando realizemos debug 
    public class DebugConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Debugger.Break();
            return value;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Debugger.Break();
            return value;
        }
    }
}
