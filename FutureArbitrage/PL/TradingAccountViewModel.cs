using FutureArbitrage.Contract;
using FutureArbitrage.CTP;
using FutureArbitrage.Frame;
using System.Collections.Generic;
using System.Text;

namespace FutureArbitrage.PL
{
    public class TradingAccountViewModel : BindableBase
    {
        private TradingAccountViewModel() { }

        private static readonly TradingAccountViewModel instance = new TradingAccountViewModel();
        public static TradingAccountViewModel Instance { get { return instance; } }

        private List<Property> tradingAccountInfo;
        public List<Property> TradingAccountInfo
        {
            get
            {
                return this.tradingAccountInfo;
            }
            set
            {
                this.tradingAccountInfo = value;
                this.NotifyPropertyChanged("TradingAccountInfo");
            }
        }

        public Command RefreshCommand
        {
            get
            {
                return new Command(this.DoRefresh);
            }
        }
        private void DoRefresh()
        {
            this.TradingAccountInfo = null;
            TradeAdapter.Instance.ReqTradingAccount();
        }

        public void Update(TradingAccount tradingAccount)
        {
            this.TradingAccountInfo = GenerateInfo(tradingAccount);
        }

        private static List<Property> GenerateInfo(TradingAccount tradingAccount)
        {
            return new List<Property>()
            {
                new Property("BrokerID",tradingAccount.BrokerID),
                new Property("AccountID",tradingAccount.AccountID),
                new Property("PreMortgage",tradingAccount.PreMortgage),
                new Property("PreCredit",tradingAccount.PreCredit),
                new Property("PreDeposit",tradingAccount.PreDeposit),
                new Property("PreBalance",tradingAccount.PreBalance),
                new Property("PreMargin",tradingAccount.PreMargin),
                new Property("InterestBase",tradingAccount.InterestBase),
                new Property("Interest",tradingAccount.Interest),
                new Property("Deposit",tradingAccount.Deposit),
                new Property("Withdraw",tradingAccount.Withdraw),
                new Property("FrozenMargin",tradingAccount.FrozenMargin),
                new Property("FrozenCash",tradingAccount.FrozenCash),
                new Property("FrozenCommission",tradingAccount.FrozenCommission),
                new Property("CurrMargin",tradingAccount.CurrMargin),
                new Property("CashIn",tradingAccount.CashIn),
                new Property("Commission",tradingAccount.Commission),
                new Property("CloseProfit",tradingAccount.CloseProfit),
                new Property("PositionProfit",tradingAccount.PositionProfit),
                new Property("Balance",tradingAccount.Balance),
                new Property("Available",tradingAccount.Available),
                new Property("WithdrawQuota",tradingAccount.WithdrawQuota),
                new Property("Reserve",tradingAccount.Reserve),
                new Property("TradingDay",tradingAccount.TradingDay),
                new Property("SettlementID",tradingAccount.SettlementID),
                new Property("Credit",tradingAccount.Credit),
                new Property("Mortgage",tradingAccount.Mortgage),
                new Property("ExchangeMargin",tradingAccount.ExchangeMargin),
                new Property("DeliveryMargin",tradingAccount.DeliveryMargin),
                new Property("ExchangeDeliveryMargin",tradingAccount.ExchangeDeliveryMargin),
                new Property("ReserveBalance",tradingAccount.ReserveBalance),
                new Property("CurrencyID",tradingAccount.CurrencyID),
                new Property("PreFundMortgageIn",tradingAccount.PreFundMortgageIn),
                new Property("PreFundMortgageOut",tradingAccount.PreFundMortgageOut),
                new Property("FundMortgageIn",tradingAccount.FundMortgageIn),
                new Property("FundMortgageOut",tradingAccount.FundMortgageOut),
                new Property("FundMortgageAvailable",tradingAccount.FundMortgageAvailable),
                new Property("MortgageableFund",tradingAccount.MortgageableFund),
                new Property("SpecProductMargin",tradingAccount.SpecProductMargin),
                new Property("SpecProductFrozenMargin",tradingAccount.SpecProductFrozenMargin),
                new Property("SpecProductCommission",tradingAccount.SpecProductCommission),
                new Property("SpecProductFrozenCommission",tradingAccount.SpecProductFrozenCommission),
                new Property("SpecProductPositionProfit",tradingAccount.SpecProductPositionProfit),
                new Property("SpecProductCloseProfit",tradingAccount.SpecProductCloseProfit),
                new Property("SpecProductPositionProfitByAlg",tradingAccount.SpecProductPositionProfitByAlg),
                new Property("SpecProductExchangeMargin",tradingAccount.SpecProductExchangeMargin)
            };
        }
    }
    public class Property
    {
        public Property(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }
        public string Name { get; set; }
        public object Value { get; set; }
    }
}