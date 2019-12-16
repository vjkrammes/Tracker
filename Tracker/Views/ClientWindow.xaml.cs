using System;
using System.Windows;

using Tracker.ViewModels;

namespace Tracker.Views
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        public ClientWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((ClientViewModel)DataContext).FocusRequested += Focus;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ((ClientViewModel)DataContext).FocusRequested -= Focus;
        }

        private void Focus(object sender, EventArgs e) => tbName.Focus();
    }
}
