using Host.PL.Frame;
using System.Collections.ObjectModel;

namespace Host.PL
{
    class ChartsViewModel : BindableBase
    {
        private ChartsViewModel() { }

        private static readonly ChartsViewModel instance = new ChartsViewModel();
        public static ChartsViewModel Instance { get { return instance; } }

        private ObservableCollection<CandleChartViewModel> documents = new ObservableCollection<CandleChartViewModel>();
        public ObservableCollection<CandleChartViewModel> Documents
        {
            get
            {
                return this.documents;
            }
            set
            {
                this.documents = value;
            }
        }

        private CandleChartViewModel activeDocument;
        public CandleChartViewModel ActiveDocument
        {
            get
            {
                return this.activeDocument;
            }
            set
            {
                this.activeDocument = value;
                this.NotifyPropertyChanged("ActiveDocument");
            }
        }
    }
}
