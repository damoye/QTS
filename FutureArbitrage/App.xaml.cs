using FutureArbitrage.BLL;
using FutureArbitrage.PL;
using System.Windows;

namespace FutureArbitrage
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            InstrumentCenter.Instance.Init();
            ArbitrageViewModel.Instance.Init();
            AccountCenter.Instance.Init();
        }
    }
}