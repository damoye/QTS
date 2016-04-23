using Host.Contract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Host.Common
{
    public static class CandleHelper
    {
        public static DateTime GetCandleTime(DateTime time, DateTime tradingDay, CandleType type)
        {
            switch (type)
            {
                case CandleType.Minute:
                    return new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0);
                case CandleType.Hour:
                    return new DateTime(time.Year, time.Month, time.Day, time.Hour, 0, 0);
                case CandleType.Day:
                    return tradingDay;
                case CandleType.Week:
                    return tradingDay.AddDays(-(int)tradingDay.DayOfWeek);
                case CandleType.Month:
                    return new DateTime(tradingDay.Year, tradingDay.Month, 1);
            }
            return time;
        }

        public static List<Candle> ConvertCandleToCandle(List<Candle> candles, CandleType type)
        {
            var result = new List<Candle>();
            DateTime currentCandleTime = CandleHelper.GetCandleTime(candles[0].CandleTime, candles[0].TradingDay, type);
            var currentCandle = new List<Candle>() { candles[0] };
            for (int i = 1; i < candles.Count; i++)
            {
                Candle item = candles[i];
                DateTime newCandleTime = CandleHelper.GetCandleTime(item.CandleTime, item.TradingDay, type);
                if (newCandleTime != currentCandleTime)
                {
                    Candle newCandle = CombineCandle(currentCandle, currentCandleTime);
                    result.Add(newCandle);
                    currentCandleTime = newCandleTime;
                    currentCandle.Clear();
                    currentCandle.Add(item);
                }
                currentCandle.Add(item);
            }
            result.Add(CombineCandle(currentCandle, currentCandleTime));
            return result;
        }

        private static Candle CombineCandle(List<Candle> candles, DateTime candleTime)
        {
            Candle first = candles[0];
            Candle last = candles[candles.Count - 1];
            Candle result = new Candle()
            {
                InstrumentID = first.InstrumentID,
                TradingDay = first.TradingDay,
                CandleTime = candleTime,
                Open = first.Open,
                Close = last.Close,
                High = candles.Max(p => p.High),
                Low = candles.Min(p => p.Low),
                Volume = candles.Sum(p => p.Volume),
                OpenInterest = last.OpenInterest
            };
            return result;
        }
    }
}