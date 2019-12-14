using System;
using System.Windows;

using Tracker.ViewModels;

namespace Tracker.Views
{
    /// <summary>
    /// Interaction logic for ClientTypeWindow.xaml
    /// </summary>
    public partial class ClientTypeWindow : Window
    {
        public ClientTypeWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((ClientTypeViewModel)DataContext).FocusRequested += Focus;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ((ClientTypeViewModel)DataContext).FocusRequested -= Focus;
        }

        private void Focus(object sender, EventArgs e) => tbName.Focus();
    }
}
