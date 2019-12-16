using System;
using System.Windows;

using Tracker.ViewModels;

namespace Tracker.Views
{
    /// <summary>
    /// Interaction logic for PhoneTypeWindow.xaml
    /// </summary>
    public partial class PhoneTypeWindow : Window
    {
        public PhoneTypeWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((PhoneTypeViewModel)DataContext).FocusRequested += Focus;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ((PhoneTypeViewModel)DataContext).FocusRequested -= Focus;
        }

        private void Focus(object sender, EventArgs e) => tbName.Focus();
    }
}
