using Host.BLL;
using Host.BLL.Interface;
using Host.Common;
using Host.Contract;
using Host.PL.DataModel;
using Host.PL.Frame;
using System.Collections.ObjectModel;
using System.Linq;

namespace Host.PL
{
    public class QuotationListViewModel : BindableBase, IQuotationReceiver
    {
        private QuotationListViewModel() { }

        private static readonly QuotationListViewModel instance = new QuotationListViewModel();
        public static QuotationListViewModel Instance { get { return instance; } }

        private ObservableCollection<QuotationModel> quotations = new ObservableCollection<QuotationModel>();
        public ObservableCollection<QuotationModel> Quotations
        {
            get
            {
                return this.quotations;
            }
            set
            {
                this.quotations = value;
            }
        }

        private QuotationModel selectedQuotation;
        public QuotationModel SelectedQuotation
        {
            get
            {
                return this.selectedQuotation;
            }
            set
            {
                this.selectedQuotation = value;
                this.NotifyPropertyChanged("SelectedQuotation");
            }
        }

        public Command RemoveCommand
        {
            get
            {
                return new Command(this.Remove);
            }
        }
        private void Remove(object obj)
        {
            var model = obj as QuotationModel;
            QuotationCenter.UnSubscribe(model.InstrumentID, this);
            this.Quotations.Remove(model);
        }

        public Command OpenCandleCommand
        {
            get
            {
                return new Command(this.OpenCandle);
            }
        }
        private void OpenCandle(object obj)
        {
            if (obj != null)
            {
                var candleViewModel = new CandleChartViewModel(((QuotationModel)obj).InstrumentID);
                ChartsViewModel.Instance.Documents.Add(candleViewModel);
                ChartsViewModel.Instance.ActiveDocument = candleViewModel;
                MainViewModel.Instance.ChartIsSelected = true;
            }
        }

        public void Init()
        {
            string[] instrumentIDs = ConfigHelper.GetConfig<string[]>(this.ToString());
            if (instrumentIDs != null)
            {
                foreach (var item in instrumentIDs)
                {
                    this.AddInstrument(item);
                }
            }
        }

        public void TryAdd(string instrumentID)
        {
            QuotationModel existedQuotation = this.Quotations.FirstOrDefault(p => p.InstrumentID == instrumentID);
            this.SelectedQuotation = existedQuotation ?? this.AddInstrument(instrumentID);
        }

        public void ProcessQuotation(Quotation quotation, int volume)
        {
            QuotationModel quotationModel = this.Quotations.First(p => p.InstrumentID == quotation.InstrumentID);
            quotationModel.Update(quotation);
        }

        public void Save()
        {
            string[] instrumentIDs = this.Quotations.Select(p => p.InstrumentID).ToArray();
            ConfigHelper.SaveConfig(this.ToString(), instrumentIDs);
        }

        private QuotationModel AddInstrument(string instrumentID)
        {
            Quotation quotation = QuotationCenter.GetQuotation(instrumentID);
            QuotationModel quotationModel;
            if (quotation == null)
            {
                quotationModel = new QuotationModel(instrumentID);
            }
            else
            {
                quotationModel = new QuotationModel(quotation);
            }
            this.Quotations.Add(quotationModel);
            QuotationCenter.Subscribe(instrumentID, this);
            return quotationModel;
        }
    }
}