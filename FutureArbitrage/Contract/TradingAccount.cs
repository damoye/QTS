namespace FutureArbitrage.Contract
{
    public class TradingAccount
    {
        public string BrokerID { get; set; }
        public string AccountID { get; set; }
        public double PreMortgage { get; set; }
        public double PreCredit { get; set; }
        public double PreDeposit { get; set; }
        public double PreBalance { get; set; }
        public double PreMargin { get; set; }
        public double InterestBase { get; set; }
        public double Interest { get; set; }
        public double Deposit { get; set; }
        public double Withdraw { get; set; }
        public double FrozenMargin { get; set; }
        public double FrozenCash { get; set; }
        public double FrozenCommission { get; set; }
        public double CurrMargin { get; set; }
        public double CashIn { get; set; }
        public double Commission { get; set; }
        public double CloseProfit { get; set; }
        public double PositionProfit { get; set; }
        public double Balance { get; set; }
        public double Available { get; set; }
        public double WithdrawQuota { get; set; }
        public double Reserve { get; set; }
        public string TradingDay { get; set; }
        public int SettlementID { get; set; }
        public double Credit { get; set; }
        public double Mortgage { get; set; }
        public double ExchangeMargin { get; set; }
        public double DeliveryMargin { get; set; }
        public double ExchangeDeliveryMargin { get; set; }
        public double ReserveBalance { get; set; }
        public string CurrencyID { get; set; }
        public double PreFundMortgageIn { get; set; }
        public double PreFundMortgageOut { get; set; }
        public double FundMortgageIn { get; set; }
        public double FundMortgageOut { get; set; }
        public double FundMortgageAvailable { get; set; }
        public double MortgageableFund { get; set; }
        public double SpecProductMargin { get; set; }
        public double SpecProductFrozenMargin { get; set; }
        public double SpecProductCommission { get; set; }
        public double SpecProductFrozenCommission { get; set; }
        public double SpecProductPositionProfit { get; set; }
        public double SpecProductCloseProfit { get; set; }
        public double SpecProductPositionProfitByAlg { get; set; }
        public double SpecProductExchangeMargin { get; set; }
    }
}