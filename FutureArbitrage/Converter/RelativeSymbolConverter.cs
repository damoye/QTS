using FutureArbitrage.Contract;
using System;
using System.Globalization;
using System.Windows.Data;

namespace FutureArbitrage.Converter
{
    public class RelativeSymbolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((RelativeSymbol)value == RelativeSymbol.GreaterOrEqual)
            {
                return ">=";
            }
            else
            {
                return "<=";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}