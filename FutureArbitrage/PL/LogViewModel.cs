using FutureArbitrage.Contract;
using FutureArbitrage.Frame;
using System.Collections.ObjectModel;

namespace FutureArbitrage.PL
{
    public class LogViewModel
    {
        private LogViewModel()
        {
            this.Logs = new ObservableCollection<Log>();
        }

        private static readonly LogViewModel instance = new LogViewModel();
        public static LogViewModel Instance { get { return instance; } }

        public ObservableCollection<Log> Logs { get; set; }

        public Command ClearCommand
        {
            get
            {
                return new Command(this.DoClear);
            }
        }
        private void DoClear()
        {
            this.Logs.Clear();
        }

        public void AddLog(Log log)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                this.Logs.Insert(0, log);
            });
        }
    }
}
