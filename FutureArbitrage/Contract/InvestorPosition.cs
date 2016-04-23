namespace FutureArbitrage.Contract
{
    public class InvestorPosition
    {
        public string InstrumentID { get; set; }
        public string BrokerID { get; set; }
        public string InvestorID { get; set; }
        public PosiDirection PosiDirection { get; set; }
        public HedgeFlag HedgeFlag { get; set; }
        public PositionDateType PositionDate { get; set; }
        public int YdPosition { get; set; }
        public int Position { get; set; }
        public int LongFrozen { get; set; }
        public int ShortFrozen { get; set; }
        public double LongFrozenAmount { get; set; }
        public double ShortFrozenAmount { get; set; }
        public int OpenVolume { get; set; }
        public int CloseVolume { get; set; }
        public double OpenAmount { get; set; }
        public double CloseAmount { get; set; }
        public double PositionCost { get; set; }
        public double PreMargin { get; set; }
        public double UseMargin { get; set; }
        public double FrozenMargin { get; set; }
        public double FrozenCash { get; set; }
        public double FrozenCommission { get; set; }
        public double CashIn { get; set; }
        public double Commission { get; set; }
        public double CloseProfit { get; set; }
        public double PositionProfit { get; set; }
        public double PreSettlementPrice { get; set; }
        public double SettlementPrice { get; set; }
        public string TradingDay { get; set; }
        public int SettlementID { get; set; }
        public double OpenCost { get; set; }
        public double ExchangeMargin { get; set; }
        public int CombPosition { get; set; }
        public int CombLongFrozen { get; set; }
        public int CombShortFrozen { get; set; }
        public double CloseProfitByDate { get; set; }
        public double CloseProfitByTrade { get; set; }
        public int TodayPosition { get; set; }
        public double MarginRateByMoney { get; set; }
        public double MarginRateByVolume { get; set; }
        public int StrikeFrozen { get; set; }
        public double StrikeFrozenAmount { get; set; }
        public int AbandonFrozen { get; set; }
    }
}