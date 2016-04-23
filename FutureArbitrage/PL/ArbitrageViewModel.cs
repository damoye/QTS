using FutureArbitrage.BLL;
using FutureArbitrage.Contract;
using FutureArbitrage.CTP;
using FutureArbitrage.Frame;
using FutureArbitrage.Util;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace FutureArbitrage.PL
{
    public class ArbitrageViewModel : BindableBase
    {
        private const string ARBITRAGE_CONFIG_KEY = "ARBITRAGE_CONFIG_KEY";

        private ArbitrageViewModel()
        {
            this.OffsetFlagSource = new OffsetFlag[] { OffsetFlag.Open, OffsetFlag.CloseToday, OffsetFlag.CloseYesterday };
            this.DirectionSource = EnumHelper.GetValues<DirectionType>();
            this.SymbolSource = EnumHelper.GetValues<RelativeSymbol>();
        }

        private static readonly ArbitrageViewModel instance = new ArbitrageViewModel();
        public static ArbitrageViewModel Instance { get { return instance; } }

        public ObservableCollection<string> InstrumentIDSource { get; set; }
        public OffsetFlag[] OffsetFlagSource { get; private set; }
        public DirectionType[] DirectionSource { get; private set; }
        public RelativeSymbol[] SymbolSource { get; set; }

        public ObservableCollection<InstrumentModel> InstrumentModels { get; set; }

        private double spread;
        public double Spread
        {
            get
            {
                return this.spread;
            }
            set
            {
                this.spread = value;
                this.NotifyPropertyChanged("Spread");
            }
        }

        private RelativeSymbol symbol;
        public RelativeSymbol Symbol
        {
            get
            {
                return this.symbol;
            }
            set
            {
                this.symbol = value;
                this.NotifyPropertyChanged("Symbol");
            }
        }

        private double triggerPrice;
        public double TriggerPrice
        {
            get
            {
                return this.triggerPrice;
            }
            set
            {
                this.triggerPrice = value;
                this.NotifyPropertyChanged("TriggerPrice");
            }
        }

        public int Multiple { get; set; }

        private bool isWork;
        public bool IsWork
        {
            get
            {
                return this.isWork;
            }
            set
            {
                if (value && !this.VerifyLogin())
                {
                    return;
                }
                this.isWork = value;
                this.NotifyPropertyChanged("IsWork");
            }
        }

        public Command SingleOrderCommand
        {
            get
            {
                return new Command(this.DoSingleOrder);
            }
        }
        private void DoSingleOrder(object obj)
        {
            if (!this.VerifyLogin())
            {
                return;
            }
            var model = obj as InstrumentModel;
            string groupID = Guid.NewGuid().ToString();
            var order = this.GenerateOrder(model, groupID);
            TradeAdapter.Instance.InsertOrder(order);
            SpeechHelper.Speak("Order");
        }

        public Command RemoveCommand
        {
            get
            {
                return new Command(this.DoRemove);
            }
        }
        private void DoRemove(object obj)
        {
            var model = obj as InstrumentModel;
            this.InstrumentModels.Remove(model);
        }

        public Command AddCommand
        {
            get
            {
                return new Command(this.DoAdd);
            }
        }
        private void DoAdd()
        {
            var model = new InstrumentModel
            {
                Direction = DirectionType.Buy
            };
            this.InstrumentModels.Add(model);
        }

        public Command ApplySpreadToTriggerPriceCommand
        {
            get
            {
                return new Command(this.DoApplySpreadToTriggerPrice);
            }
        }
        private void DoApplySpreadToTriggerPrice()
        {
            this.TriggerPrice = this.Spread;
        }

        public Command ReverseCommand
        {
            get
            {
                return new Command(this.DoReverse);
            }
        }
        private void DoReverse()
        {
            foreach (var item in InstrumentModels)
            {
                item.OffsetFlag = item.OffsetFlag == OffsetFlag.Open ? OffsetFlag.CloseToday : OffsetFlag.Open;
                item.Direction = item.Direction == DirectionType.Buy ? DirectionType.Sell : DirectionType.Buy;
            }
            this.Symbol = this.Symbol == RelativeSymbol.GreaterOrEqual ? RelativeSymbol.LessOrEqual : RelativeSymbol.GreaterOrEqual;
        }

        public Command ArbiOrderCommand
        {
            get
            {
                return new Command(this.DoArbiOrder);
            }
        }
        private void DoArbiOrder()
        {
            if (!this.VerifyLogin())
            {
                return;
            }
            LogCenter.Log("套利单发送，价差：" + this.Spread);
            string groupID = Guid.NewGuid().ToString();
            foreach (var item in this.InstrumentModels)
            {
                var order = this.GenerateOrder(item, groupID);
                TradeAdapter.Instance.InsertOrder(order);
            }
            SpeechHelper.Speak("Order");
        }

        public void ProcessQuotation(Quotation quotation)
        {
            double newSpread = 0;
            foreach (var item in this.InstrumentModels)
            {
                if (item.InstrumentID == quotation.InstrumentID)
                {
                    item.Price = quotation.GetPrice(item.Direction);
                }
                if (item.Direction == this.InstrumentModels[0].Direction)
                {
                    newSpread += item.Volume * item.Price;
                }
                else
                {
                    newSpread -= item.Volume * item.Price;
                }
            }
            this.Spread = newSpread;
            if (!this.IsWork)
            {
                return;
            }
            if (this.Symbol == RelativeSymbol.GreaterOrEqual && this.Spread >= this.TriggerPrice ||
                this.Symbol == RelativeSymbol.LessOrEqual && this.Spread <= this.TriggerPrice)
            {
                this.IsWork = false;
                this.DoArbiOrder();
            }
        }

        public void Init()
        {
            var config = ConfigHelper.GetConfig<ArbitrageConfig>(ARBITRAGE_CONFIG_KEY);
            if (config == null)
            {
                this.InstrumentModels = new ObservableCollection<InstrumentModel>()
                {
                    new InstrumentModel() {Direction = DirectionType.Buy },
                    new InstrumentModel() {Direction = DirectionType.Sell }
                };
                this.Symbol = RelativeSymbol.LessOrEqual;
                this.Multiple = 1;
            }
            else
            {
                this.InstrumentModels = new ObservableCollection<InstrumentModel>(config.InstrumentConfigs);
                this.Symbol = config.Symbol;
                this.TriggerPrice = config.TriggerPrice;
                this.Multiple = config.Multiple;
            }
        }

        public void Save()
        {
            foreach (var item in this.InstrumentModels)
            {
                item.Price = 0;
            }
            var config = new ArbitrageConfig
            {
                InstrumentConfigs = this.InstrumentModels.ToArray(),
                Symbol = this.Symbol,
                TriggerPrice = this.TriggerPrice,
                Multiple = this.Multiple
            };
            ConfigHelper.SaveConfig(ARBITRAGE_CONFIG_KEY, config);
        }

        public void TryUnSubscribe(string instrumentID)
        {
            if (this.InstrumentModels.All(p => p.InstrumentID != instrumentID))
            {
                MdAdapter.UnSubscribe(instrumentID);
            }
        }

        private bool VerifyLogin()
        {
            if (!AccountCenter.Instance.IsTDLogin)
            {
                MainViewModel.Instance.Alert("账户未登陆");
                return false;
            }
            else
            {
                return true;
            }
        }

        private InputOrder GenerateOrder(InstrumentModel model, string groupID)
        {
            return new InputOrder(AccountCenter.Instance.Account.BrokerID,
                                  AccountCenter.Instance.Account.InvestorID,
                                  model.Direction,
                                  model.OffsetFlag,
                                  model.InstrumentID,
                                  model.Price,
                                  model.Volume * this.Multiple,
                                  groupID);
        }
    }
}