using CTP;

namespace FutureArbitrage.Contract
{
    public class Quotation
    {
        public Quotation(ThostFtdcDepthMarketDataField field)
        {
            this.InstrumentID = field.InstrumentID;
            this.AskPrice = field.AskPrice1;
            this.BidPrice = field.BidPrice1;
            this.LastPrice = field.LastPrice;
        }

        public string InstrumentID { get; set; }
        public double AskPrice { get; set; }
        public double BidPrice { get; set; }
        public double LastPrice { get; set; }

        public double GetPrice(DirectionType direction)
        {
            double askBidPrice = direction == DirectionType.Buy ? this.AskPrice : this.BidPrice;
            return askBidPrice == double.MaxValue ? this.LastPrice : askBidPrice;
        }
    }
}