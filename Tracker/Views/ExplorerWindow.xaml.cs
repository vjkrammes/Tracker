using System.Windows;

using Tracker.Models;
using Tracker.ViewModels;

namespace Tracker.Views
{
    /// <summary>
    /// Interaction logic for ExplorerWindow.xaml
    /// </summary>
    public partial class ExplorerWindow : Window
    {
        public ExplorerWindow()
        {
            InitializeComponent();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is ExplorerItem item && DataContext != null)
            {
                ((ExplorerViewModel)DataContext).SelectedItem = item;
            }
        }
    }
}
