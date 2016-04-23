using Host.BLL;
using Host.BLL.Interface;
using Host.Common;
using Host.Contract;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using Host.PL.Frame;
using System.Windows;
using System.Windows.Threading;

namespace Host.PL
{
    public class CandleChartViewModel : BindableBase, IQuotationReceiver
    {
        private string instrumentID;
        private CandleBuilder builder;
        private Dispatcher dispatcher = Dispatcher.CurrentDispatcher;

        public string Title { get; set; }

        private CandleType[] candleTypes = Enum.GetValues(typeof(CandleType)).OfType<CandleType>().ToArray();
        public CandleType[] CandleTypes
        {
            get
            {
                return this.candleTypes;
            }
            set
            {
                this.candleTypes = value;
            }
        }

        private ObservableCollection<Candle> candles;
        public ObservableCollection<Candle> Candles
        {
            get
            {
                return this.candles;
            }
            set
            {
                this.candles = value;
                this.NotifyPropertyChanged("Candles");
            }
        }

        public int Precision { get; private set; }

        private CandleType candleType = CandleType.Minute;
        public CandleType CandleType
        {
            get
            {
                return this.candleType;
            }
            set
            {
                this.candleType = value;
                this.NotifyPropertyChanged("CandleType");
                this.GetCandles();
            }
        }

        private DateTime from = DateTime.Today.AddDays(-7);
        public DateTime From
        {
            get
            {
                return this.from;
            }
            set
            {
                this.from = value;
                this.GetCandles();
            }
        }

        public Command CloseCommand
        {
            get
            {
                return new Command(this.DoClose);
            }
        }
        private void DoClose()
        {
            QuotationCenter.UnSubscribe(this.instrumentID, this);
            ChartsViewModel.Instance.Documents.Remove(this);
        }

        public CandleChartViewModel(string instrumentID)
        {
            this.Title = instrumentID;
            this.instrumentID = instrumentID;
            double priceTick = InstrumentListViewModel.Instance.Instruments.First(p => p.InstrumentID == instrumentID).PriceTick;
            this.Precision = GetPrecision(priceTick);
            this.GetCandles();
            QuotationCenter.Subscribe(instrumentID, this);
        }

        public void ProcessQuotation(Quotation quotation, int volume)
        {
            if (this.builder == null)
            {
                return;
            }
            bool isCreateOrUpdate = this.builder.ProcessQuotation(quotation, volume);
            this.dispatcher.Invoke(() =>
            {
                if (isCreateOrUpdate)
                {
                    this.Candles.Add(this.builder.LastData);
                }
                else
                {
                    this.Candles[this.Candles.Count - 1] = this.builder.LastData;
                }
            });
        }

        private async void GetCandles()
        {
            this.builder = null;
            var candles = await Task.Run(() => { return CandleCenter.GetCandles(this.instrumentID, this.CandleType, this.From); });
            this.Candles = new ObservableCollection<Candle>(candles);
            this.builder = new CandleBuilder(this.Candles.LastOrDefault(), this.CandleType);
        }

        private static int GetPrecision(double priceTick)
        {
            string str = priceTick.ToString();
            int pointIndex = str.IndexOf('.');
            if (pointIndex != -1)
            {
                return str.Length - (pointIndex + 1);
            }
            else
            {
                return 0;
            }
        }
    }
}