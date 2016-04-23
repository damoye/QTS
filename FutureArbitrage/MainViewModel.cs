using FutureArbitrage.Frame;
using System.Threading;

namespace FutureArbitrage
{
    public class MainViewModel : BindableBase
    {
        private Timer timer;

        private MainViewModel()
        {
            this.timer = new Timer(this.ClearMessage);
        }

        private static readonly MainViewModel instance = new MainViewModel();
        public static MainViewModel Instance { get { return instance; } }

        private string message;
        public string Message
        {
            get
            {
                return this.message;
            }
            private set
            {
                this.message = value;
                this.NotifyPropertyChanged("Message");
            }
        }

        public void Alert(string message)
        {
            this.Message = message;
            this.timer.Change(3000, Timeout.Infinite);
        }

        private void ClearMessage(object state)
        {
            this.Message = string.Empty;
        }
    }
}