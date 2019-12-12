using System.Windows;

using Tracker.Infrastructure;

namespace Tracker.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = Tools.Locator.MainViewModel;
        }
    }
}
