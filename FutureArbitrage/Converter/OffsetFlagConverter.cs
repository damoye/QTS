using FutureArbitrage.Contract;
using System;
using System.Globalization;
using System.Windows.Data;

namespace FutureArbitrage.Converter
{
    public class OffsetFlagConverter : IValueConverter
    {
        public object Convert(object value, Type targetType = null, object parameter = null, CultureInfo culture = null)
        {
            switch ((OffsetFlag)value)
            {
                case OffsetFlag.Open:
                    return "开";
                case OffsetFlag.CloseToday:
                    return "平今";
                case OffsetFlag.CloseYesterday:
                    return "平昨";
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
