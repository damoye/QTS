using Host.Contract;
using System;

namespace Host.Common
{
    public class CandleBuilder
    {
        private CandleType type;
        public Candle LastData { get; private set; }

        public CandleBuilder(Candle lastData, CandleType type)
        {
            this.LastData = lastData;
            this.type = type;
        }

        public bool ProcessQuotation(Quotation quotation, int volume)
        {
            DateTime newCandleTime = CandleHelper.GetCandleTime(quotation.Time, quotation.TradingDay, this.type);
            if (this.LastData == null || this.LastData.CandleTime != newCandleTime)
            {
                this.LastData = new Candle(quotation, newCandleTime, volume);
                return true;
            }
            else
            {
                this.LastData.UpdateCandle(quotation, volume);
                return false;
            }
        }
    }
}