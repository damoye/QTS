using System;
using System.Text;

namespace Host.Contract
{
    public class Candle : ICloneable
    {
        public string InstrumentID { get; set; }
        public DateTime TradingDay { get; set; }
        public DateTime CandleTime { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public int Volume { get; set; }
        public double OpenInterest { get; set; }

        public Candle() { }

        public Candle(Quotation quotation, DateTime candleTime, int volume)
        {
            this.InstrumentID = quotation.InstrumentID;
            this.TradingDay = quotation.TradingDay;
            this.CandleTime = candleTime;
            this.Open = quotation.LastPrice;
            this.Close = quotation.LastPrice;
            this.High = quotation.LastPrice;
            this.Low = quotation.LastPrice;
            this.Volume = volume;
            this.OpenInterest = quotation.OpenInterest;
        }

        public void UpdateCandle(Quotation quotation, int volume)
        {
            this.Close = quotation.LastPrice;
            this.High = Math.Max(this.High, quotation.LastPrice);
            this.Low = Math.Min(this.Low, quotation.LastPrice);
            this.Volume += volume;
            this.OpenInterest = quotation.OpenInterest;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Open:");
            sb.AppendLine(this.Open.ToString());
            sb.Append("Close:");
            sb.AppendLine(this.Close.ToString());
            sb.Append("High:");
            sb.AppendLine(this.High.ToString());
            sb.Append("Low:");
            sb.AppendLine(this.Low.ToString());
            sb.Append("Volume:");
            sb.AppendLine(this.Volume.ToString());
            sb.Append("OpenInterest:");
            sb.AppendLine(this.OpenInterest.ToString());
            return sb.ToString();
        }
    }
}