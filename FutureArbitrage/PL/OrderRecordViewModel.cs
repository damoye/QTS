using FutureArbitrage.Contract;
using FutureArbitrage.Frame;
using System.Collections.ObjectModel;

namespace FutureArbitrage.PL
{
    public class OrderRecordViewModel
    {
        private OrderRecordViewModel()
        {
            this.Orders = new ObservableCollection<Order>();
        }

        private static readonly OrderRecordViewModel instance = new OrderRecordViewModel();
        public static OrderRecordViewModel Instance { get { return instance; } }

        public ObservableCollection<Order> Orders { get; private set; }

        public Command ClearCommand { get { return new Command(this.DoClear); } }
        private void DoClear()
        {
            this.Orders.Clear();
        }

        public void AddOrder(Order order)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                this.Orders.Insert(0, order);
            });
        }
    }
}