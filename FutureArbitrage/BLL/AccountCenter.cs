using CTP;
using FutureArbitrage.Contract;
using FutureArbitrage.CTP;
using FutureArbitrage.Frame;
using FutureArbitrage.Util;

namespace FutureArbitrage.BLL
{
    public class AccountCenter : BindableBase
    {
        private const string MD_CONFIG_KEY = "ACCOUNT_CONFIG_KEY";

        private AccountCenter() { }
        private static readonly AccountCenter instance = new AccountCenter();
        public static AccountCenter Instance { get { return instance; } }

        public Account Account { get; private set; }

        public void Init()
        {
            this.Account = ConfigHelper.GetConfig<Account>(MD_CONFIG_KEY);
            if (this.Account != null)
            {
                MdAdapter.ChangeAccount();
                TradeAdapter.ChangeAccount();
            }
        }

        public void ChangeAccount(Account account)
        {
            this.Account.Update(account);
            MdAdapter.ChangeAccount();
            TradeAdapter.ChangeAccount();
            ConfigHelper.SaveConfig(MD_CONFIG_KEY, this.Account);
        }

        private bool isMDLogin;
        public bool IsMDLogin
        {
            get
            {
                return this.isMDLogin;
            }
            set
            {
                this.isMDLogin = value;
                this.NotifyPropertyChanged("IsMDLogin");
            }
        }

        private bool isTDLogin;
        public bool IsTDLogin
        {
            get
            {
                return this.isTDLogin;
            }
            set
            {
                this.isTDLogin = value;
                this.NotifyPropertyChanged("IsTDLogin");
            }
        }
    }
}
