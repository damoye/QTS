using System;

namespace Host.Common
{
    public static class DateTimeHelper
    {
        public static DateTime ConvertTradingDay(string tradingDay)
        {
            int year, month, day;
            ParseDay(tradingDay, out year, out month, out day);
            return new DateTime(year, month, day);
        }

        public static void ParseDay(string date, out int year, out int month, out int day)
        {
            year = int.Parse(date.Substring(0, 4));
            month = int.Parse(date.Substring(4, 2));
            day = int.Parse(date.Substring(6, 2));
        }
    }
}
