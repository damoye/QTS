using Host.BLL;
using System.Windows;

namespace Host
{
    public partial class App : Application
    {
        public App()
        {
            StatusMonitor.Start();
        }
    }
}
