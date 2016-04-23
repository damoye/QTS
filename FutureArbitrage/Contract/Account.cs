using FutureArbitrage.Frame;

namespace FutureArbitrage.Contract
{
    public class Account : BindableBase
    {
        private string brokerID;
        public string BrokerID
        {
            get
            {
                return this.brokerID;
            }
            set
            {
                this.brokerID = value;
                this.NotifyPropertyChanged("BrokerID");
            }
        }

        private string investorID;
        public string InvestorID
        {
            get
            {
                return this.investorID;
            }
            set
            {
                this.investorID = value;
                this.NotifyPropertyChanged("InvestorID");
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
                this.NotifyPropertyChanged("Password");
            }
        }

        private string mDAddress;
        public string MDAddress
        {
            get
            {
                return this.mDAddress;
            }
            set
            {
                this.mDAddress = value;
                this.NotifyPropertyChanged("MDAddress");
            }
        }

        private string tDaddress;
        public string TDAddress
        {
            get
            {
                return this.tDaddress;
            }
            set
            {
                this.tDaddress = value;
                this.NotifyPropertyChanged("TDAddress");
            }
        }

        public void Update(Account account)
        {
            this.BrokerID = account.BrokerID;
            this.InvestorID = account.InvestorID;
            this.Password = account.Password;
            this.MDAddress = account.MDAddress;
            this.TDAddress = account.TDAddress;
        }
    }
}
