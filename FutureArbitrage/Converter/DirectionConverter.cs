using FutureArbitrage.Contract;
using System;
using System.Globalization;
using System.Windows.Data;

namespace FutureArbitrage.Converter
{
    public class DirectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType = null, object parameter = null, CultureInfo culture = null)
        {
            return (DirectionType)value == DirectionType.Buy ? "买" : "卖";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}