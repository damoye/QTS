using FutureArbitrage.Contract;
using FutureArbitrage.PL;
using System;
using System.Globalization;
using System.Windows.Data;

namespace FutureArbitrage.Converter
{
    public class DirectionToSymbolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (DirectionType)value == ArbitrageViewModel.Instance.InstrumentModels[0].Direction ? "+" : "-";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}