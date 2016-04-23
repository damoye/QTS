using FutureArbitrage.Contract;
using FutureArbitrage.Frame;
using System.Collections.ObjectModel;

namespace FutureArbitrage.PL
{
    public class TradeRecordViewModel
    {
        private TradeRecordViewModel()
        {
            this.Trades = new ObservableCollection<Trade>();
        }

        private static readonly TradeRecordViewModel instance = new TradeRecordViewModel();
        public static TradeRecordViewModel Instance { get { return instance; } }

        public ObservableCollection<Trade> Trades { get; private set; }

        public Command ClearCommand { get { return new Command(this.DoClear); } }
        private void DoClear()
        {
            this.Trades.Clear();
        }

        public void AddTrade(Trade trade)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                this.Trades.Insert(0, trade);
            });
        }
    }
}