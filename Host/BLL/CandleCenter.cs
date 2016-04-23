using Host.Common;
using Host.Contract;
using Host.DAL;
using System;
using System.Collections.Generic;

namespace Host.BLL
{
    public class CandleCenter
    {
        private static Dictionary<string, CandleBuilder> builders = new Dictionary<string, CandleBuilder>();

        public static void ProcessQuotation(Quotation quotation, int volume)
        {
            CandleBuilder builder;
            if (!builders.TryGetValue(quotation.InstrumentID, out builder))
            {
                builder = new CandleBuilder(CandleDAL.GetLast(quotation.InstrumentID), CandleType.Minute);
                builders.Add(quotation.InstrumentID, builder);
            }
            builder.ProcessQuotation(quotation, volume);
            DataSaver.AddCandle(builder.LastData);
        }

        public static List<Candle> GetCandles(string instrumentID, CandleType type, DateTime fromTradingDay)
        {
            List<Candle> minuteCandles = CandleDAL.Get(instrumentID, fromTradingDay);
            if (type == CandleType.Minute)
            {
                return minuteCandles;
            }
            else
            {
                return CandleHelper.ConvertCandleToCandle(minuteCandles, type);
            }
        }
    }
}