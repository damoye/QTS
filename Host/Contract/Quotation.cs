using CTP;
using Host.Common;
using System;

namespace Host.Contract
{
    public class Quotation
    {
        public DateTime TradingDay { get; set; }
        public string InstrumentID { get; set; }
        public string ExchangeID { get; set; }
        public string ExchangeInstID { get; set; }
        public double LastPrice { get; set; }
        public double PreSettlementPrice { get; set; }
        public double PreClosePrice { get; set; }
        public double PreOpenInterest { get; set; }
        public double OpenPrice { get; set; }
        public double HighestPrice { get; set; }
        public double LowestPrice { get; set; }
        public int Volume { get; set; }
        public double Turnover { get; set; }
        public double OpenInterest { get; set; }
        public double ClosePrice { get; set; }
        public double SettlementPrice { get; set; }
        public double UpperLimitPrice { get; set; }
        public double LowerLimitPrice { get; set; }
        public double PreDelta { get; set; }
        public double CurrDelta { get; set; }
        public DateTime Time { get; set; }
        public double BidPrice1 { get; set; }
        public int BidVolume1 { get; set; }
        public double AskPrice1 { get; set; }
        public int AskVolume1 { get; set; }
        public double AveragePrice { get; set; }

        public Quotation() { }

        public Quotation(ThostFtdcDepthMarketDataField field)
        {
            this.TradingDay = DateTimeHelper.ConvertTradingDay(field.TradingDay);
            this.InstrumentID = field.InstrumentID;
            this.ExchangeID = field.ExchangeID;
            this.ExchangeInstID = field.ExchangeInstID;
            this.LastPrice = field.LastPrice;
            this.PreSettlementPrice = field.PreSettlementPrice;
            this.PreClosePrice = field.PreClosePrice;
            this.PreOpenInterest = field.PreOpenInterest;
            this.OpenPrice = field.OpenPrice;
            this.HighestPrice = field.HighestPrice;
            this.LowestPrice = field.LowestPrice;
            this.Volume = field.Volume;
            this.Turnover = field.Turnover;
            this.OpenInterest = field.OpenInterest;
            this.ClosePrice = field.ClosePrice;
            this.SettlementPrice = field.SettlementPrice;
            this.UpperLimitPrice = field.UpperLimitPrice;
            this.LowerLimitPrice = field.LowerLimitPrice;
            this.PreDelta = field.PreDelta;
            this.CurrDelta = field.CurrDelta;
            this.Time = GetTime(field.ActionDay, field.UpdateTime, field.UpdateMillisec);
            this.BidPrice1 = field.BidPrice1;
            this.BidVolume1 = field.BidVolume1;
            this.AskPrice1 = field.AskPrice1;
            this.AskVolume1 = field.AskVolume1;
            this.AveragePrice = field.AveragePrice;
        }

        public override string ToString()
        {
            return this.InstrumentID + ' ' + this.LastPrice;
        }

        private static DateTime GetTime(string actionDay, string updateTime, int updateMillisec)
        {
            int year, month, day;
            DateTimeHelper.ParseDay(actionDay, out year, out month, out day);
            string[] updatetimeArray = updateTime.Split(':');
            int hour = int.Parse(updatetimeArray[0]);
            int minite = int.Parse(updatetimeArray[1]);
            int second = int.Parse(updatetimeArray[2]);
            var time = new DateTime(year, month, day, hour, minite, second, updateMillisec);
            return time;
        }
    }
}
