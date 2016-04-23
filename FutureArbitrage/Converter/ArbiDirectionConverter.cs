using FutureArbitrage.Contract;
using System;
using System.Globalization;
using System.Windows.Data;

namespace FutureArbitrage.Converter
{
    public class ArbiDirectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((ArbiDirection)value == ArbiDirection.Buy1Sell2)
            {
                return "买一卖二";
            }
            else
            {
                return "卖一买二";
            }
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}