namespace FutureArbitrage.Contract
{
    public class InvestorPositionDetail
    {
        public string InstrumentID { get; set; }
        public string BrokerID { get; set; }
        public string InvestorID { get; set; }
        public HedgeFlag HedgeFlag { get; set; }
        public DirectionType Direction { get; set; }
        public string OpenDate { get; set; }
        public string TradeID { get; set; }
        public int Volume { get; set; }
        public double OpenPrice { get; set; }
        public string TradingDay { get; set; }
        public int SettlementID { get; set; }
        public TradeType TradeType { get; set; }
        public string CombInstrumentID { get; set; }
        public string ExchangeID { get; set; }
        public double CloseProfitByDate { get; set; }
        public double CloseProfitByTrade { get; set; }
        public double PositionProfitByDate { get; set; }
        public double PositionProfitByTrade { get; set; }
        public double Margin { get; set; }
        public double ExchMargin { get; set; }
        public double MarginRateByMoney { get; set; }
        public double MarginRateByVolume { get; set; }
        public double LastSettlementPrice { get; set; }
        public double SettlementPrice { get; set; }
        public int CloseVolume { get; set; }
        public double CloseAmount { get; set; }
    }
}
