using System.Windows.Controls;

using Tracker.Infrastructure;

namespace Tracker.UserControls
{
    /// <summary>
    /// Interaction logic for Statusbar.xaml
    /// </summary>
    public partial class Statusbar : UserControl
    {
        public Statusbar()
        {
            InitializeComponent();
            DataContext = Tools.Locator.StatusbarViewModel;
        }
    }
}
