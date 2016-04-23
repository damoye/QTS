using FutureArbitrage.PL;
using System.Windows;

namespace FutureArbitrage
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Closed += this.WindowClosed;
        }

        private void WindowClosed(object sender, System.EventArgs e)
        {
            ArbitrageViewModel.Instance.Save();
        }
    }
}