using System;

namespace FutureArbitrage.Util
{
    public static class DateTimeHelper
    {
        public static DateTime GenerateTime(string date, string time)
        {
            return DateTime.ParseExact(date + time, "yyyyMMddHH:mm:ss", null);
        }
    }
}