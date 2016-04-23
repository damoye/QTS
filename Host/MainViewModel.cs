using Host.BLL;
using Host.BLL.CTP;
using Host.DAL;
using Host.PL;
using Host.PL.Frame;
using System.Threading.Tasks;

namespace Host
{
    public class MainViewModel : BindableBase
    {
        private MainViewModel()
        {
            this.Init();
        }

        private static readonly MainViewModel instance = new MainViewModel();
        public static MainViewModel Instance { get { return instance; } }

        private bool isBusy = true;
        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }
            set
            {
                this.isBusy = value;
                this.NotifyPropertyChanged("IsBusy");
            }
        }

        private bool instrumentIsSelected;
        public bool InstrumentIsSelected
        {
            get
            {
                return this.instrumentIsSelected;
            }
            set
            {
                this.instrumentIsSelected = value;
                this.NotifyPropertyChanged("InstrumentIsSelected");
            }
        }

        private bool quotationIsSelected;
        public bool QuotationIsSelected
        {
            get
            {
                return this.quotationIsSelected;
            }
            set
            {
                this.quotationIsSelected = value;
                this.NotifyPropertyChanged("QuotationIsSelected");
            }
        }

        private bool chartIsSelected;
        public bool ChartIsSelected
        {
            get
            {
                return this.chartIsSelected;
            }
            set
            {
                this.chartIsSelected = value;
                this.NotifyPropertyChanged("ChartIsSelected");
            }
        }

        public Command SwitchToInstrumentCommand
        {
            get
            {
                return new Command(() => this.InstrumentIsSelected = true);
            }
        }

        public Command SwitchToQuotationCommand
        {
            get
            {
                return new Command(() => this.QuotationIsSelected = true);
            }
        }

        public Command SwitchToChartCommand
        {
            get
            {
                return new Command(() => this.ChartIsSelected = true);
            }
        }

        private async void Init()
        {
            await DALUtil.Init();
            InstrumentListViewModel.Instance.Instruments = await Task.Run(() => { return InstrumentDAL.Get(); });
            await Task.Run(() => { QuotationCenter.Init(); });
            QuotationListViewModel.Instance.Init();
            TradeAdapter.Instance.Start();
            this.IsBusy = false;
        }
    }
}