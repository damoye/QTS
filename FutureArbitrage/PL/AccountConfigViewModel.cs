using FutureArbitrage.BLL;
using FutureArbitrage.Contract;
using FutureArbitrage.Frame;

namespace FutureArbitrage.PL
{
    public class AccountConfigViewModel
    {
        private AccountConfigViewModel()
        {
            this.Account = new Account();
            if (AccountCenter.Instance.Account != null)
            {
                this.Account.Update(AccountCenter.Instance.Account);
            }
        }
        private static readonly AccountConfigViewModel instance = new AccountConfigViewModel();
        public static AccountConfigViewModel Instance { get { return instance; } }

        public Account Account { get; set; }

        public Command ApplyCommand { get { return new Command(this.DoApply); } }
        private void DoApply()
        {
            AccountCenter.Instance.ChangeAccount(this.Account);
        }

        public Command RestoreCommand { get { return new Command(this.DoRestore); } }
        private void DoRestore()
        {
            this.Account.Update(AccountCenter.Instance.Account);
        }
    }
}