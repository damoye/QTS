using Host.PL;
using System.ComponentModel;
using System.Windows;

namespace Host
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            QuotationListViewModel.Instance.Save();
            base.OnClosing(e);
        }
    }
}