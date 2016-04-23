using System;

namespace FutureArbitrage.Contract
{
    public class Trade
    {
        public string InstrumentID { get; set; }
        public string UserID { get; set; }
        public DirectionType Direction { get; set; }
        public OffsetFlag OffsetFlag { get; set; }
        public double Price { get; set; }
        public int Volume { get; set; }
        public DateTime TradeTime { get; set; }
    }
}