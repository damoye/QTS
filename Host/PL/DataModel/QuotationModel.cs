using Host.Contract;
using Host.PL.Frame;
using System;

namespace Host.PL.DataModel
{
    public class QuotationModel : BindableBase
    {
        public string InstrumentID { get; set; }

        private double? lastPrice;
        public double? LastPrice
        {
            get
            {
                return this.lastPrice;
            }
            set
            {
                this.lastPrice = value;
                this.NotifyPropertyChanged("LastPrice");
            }
        }

        private double? preSettlementPrice;
        public double? PreSettlementPrice
        {
            get
            {
                return this.preSettlementPrice;
            }
            set
            {
                this.preSettlementPrice = value;
                this.NotifyPropertyChanged("PreSettlementPrice");
            }
        }

        private double? preClosePrice;
        public double? PreClosePrice
        {
            get
            {
                return this.preClosePrice;
            }
            set
            {
                this.preClosePrice = value;
                this.NotifyPropertyChanged("PreClosePrice");
            }
        }

        private double? preOpenInterest;
        public double? PreOpenInterest
        {
            get
            {
                return this.preOpenInterest;
            }
            set
            {
                this.preOpenInterest = value;
                this.NotifyPropertyChanged("PreOpenInterest");
            }
        }

        private double? openPrice;
        public double? OpenPrice
        {
            get
            {
                return this.openPrice;
            }
            set
            {
                this.openPrice = value;
                this.NotifyPropertyChanged("OpenPrice");
            }
        }

        private double? highestPrice;
        public double? HighestPrice
        {
            get
            {
                return this.highestPrice;
            }
            set
            {
                this.highestPrice = value;
                this.NotifyPropertyChanged("HighestPrice");
            }
        }

        private double? lowestPrice;
        public double? LowestPrice
        {
            get
            {
                return this.lowestPrice;
            }
            set
            {
                this.lowestPrice = value;
                this.NotifyPropertyChanged("LowestPrice");
            }
        }

        private int volume;
        public int Volume
        {
            get
            {
                return this.volume;
            }
            set
            {
                this.volume = value;
                this.NotifyPropertyChanged("Volume");
            }
        }

        private double? turnover;
        public double? Turnover
        {
            get
            {
                return this.turnover;
            }
            set
            {
                this.turnover = value;
                this.NotifyPropertyChanged("Turnover");
            }
        }

        private double? openInterest;
        public double? OpenInterest
        {
            get
            {
                return this.openInterest;
            }
            set
            {
                this.openInterest = value;
                this.NotifyPropertyChanged("OpenInterest");
            }
        }

        private double? closePrice;
        public double? ClosePrice
        {
            get
            {
                return this.closePrice;
            }
            set
            {
                this.closePrice = value;
                this.NotifyPropertyChanged("ClosePrice");
            }
        }

        private double? settlementPrice;
        public double? SettlementPrice
        {
            get
            {
                return this.settlementPrice;
            }
            set
            {
                this.settlementPrice = value;
                this.NotifyPropertyChanged("SettlementPrice");
            }
        }

        private double? upperLimitPrice;
        public double? UpperLimitPrice
        {
            get
            {
                return this.upperLimitPrice;
            }
            set
            {
                this.upperLimitPrice = value;
                this.NotifyPropertyChanged("UpperLimitPrice");
            }
        }

        private double? lowerLimitPrice;
        public double? LowerLimitPrice
        {
            get
            {
                return this.lowerLimitPrice;
            }
            set
            {
                this.lowerLimitPrice = value;
                this.NotifyPropertyChanged("LowerLimitPrice");
            }
        }

        private double? preDelta;
        public double? PreDelta
        {
            get
            {
                return this.preDelta;
            }
            set
            {
                this.preDelta = value;
                this.NotifyPropertyChanged("PreDelta");
            }
        }

        private double? currDelta;
        public double? CurrDelta
        {
            get
            {
                return this.currDelta;
            }
            set
            {
                this.currDelta = value;
                this.NotifyPropertyChanged("CurrDelta");
            }
        }

        private DateTime time;
        public DateTime Time
        {
            get
            {
                return this.time;
            }
            set
            {
                this.time = value;
                this.NotifyPropertyChanged("Time");
            }
        }

        private double? bidPrice1;
        public double? BidPrice1
        {
            get
            {
                return this.bidPrice1;
            }
            set
            {
                this.bidPrice1 = value;
                this.NotifyPropertyChanged("BidPrice1");
            }
        }

        private int bidVolume1;
        public int BidVolume1
        {
            get
            {
                return this.bidVolume1;
            }
            set
            {
                this.bidVolume1 = value;
                this.NotifyPropertyChanged("BidVolume1");
            }
        }

        private double? askPrice1;
        public double? AskPrice1
        {
            get
            {
                return this.askPrice1;
            }
            set
            {
                this.askPrice1 = value;
                this.NotifyPropertyChanged("AskPrice1");
            }
        }

        private int askVolume1;
        public int AskVolume1
        {
            get
            {
                return this.askVolume1;
            }
            set
            {
                this.askVolume1 = value;
                this.NotifyPropertyChanged("AskVolume1");
            }
        }

        private double? averagePrice;
        public double? AveragePrice
        {
            get
            {
                return this.averagePrice;
            }
            set
            {
                this.averagePrice = value;
                this.NotifyPropertyChanged("AveragePrice");
            }
        }

        public QuotationModel(Quotation quotation)
        {
            this.InstrumentID = quotation.InstrumentID;
            this.Update(quotation);
        }

        public QuotationModel(string instrumentID)
        {
            this.InstrumentID = instrumentID;
        }

        public void Update(Quotation quotation)
        {
            this.LastPrice = GetNullableDouble(quotation.LastPrice);
            this.PreSettlementPrice = GetNullableDouble(quotation.PreSettlementPrice);
            this.PreClosePrice = GetNullableDouble(quotation.PreClosePrice);
            this.PreOpenInterest = GetNullableDouble(quotation.PreOpenInterest);
            this.OpenPrice = GetNullableDouble(quotation.OpenPrice);
            this.HighestPrice = GetNullableDouble(quotation.HighestPrice);
            this.LowestPrice = GetNullableDouble(quotation.LowestPrice);
            this.Volume = quotation.Volume;
            this.Turnover = GetNullableDouble(quotation.Turnover);
            this.OpenInterest = GetNullableDouble(quotation.OpenInterest);
            this.ClosePrice = GetNullableDouble(quotation.ClosePrice);
            this.SettlementPrice = GetNullableDouble(quotation.SettlementPrice);
            this.UpperLimitPrice = GetNullableDouble(quotation.UpperLimitPrice);
            this.LowerLimitPrice = GetNullableDouble(quotation.LowerLimitPrice);
            this.PreDelta = GetNullableDouble(quotation.PreDelta);
            this.CurrDelta = GetNullableDouble(quotation.CurrDelta);
            this.Time = quotation.Time;
            this.BidPrice1 = GetNullableDouble(quotation.BidPrice1);
            this.BidVolume1 = quotation.BidVolume1;
            this.AskPrice1 = GetNullableDouble(quotation.AskPrice1);
            this.AskVolume1 = quotation.AskVolume1;
            this.AveragePrice = Math.Round(quotation.AveragePrice, 2);
        }

        private static double? GetNullableDouble(double source)
        {
            if (source == double.MaxValue)
            {
                return null;
            }
            else
            {
                return source;
            }
        }
    }
}