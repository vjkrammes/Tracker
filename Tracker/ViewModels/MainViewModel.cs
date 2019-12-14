using System;
using System.Windows;

using Tracker.Infrastructure;

using TrackerCommon;

namespace Tracker.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            var settings = Tools.Locator.Settings;
            Pallette.SetSystemColors();
            Application.Current.Resources[Constants.IconHeight] = settings.IconHeight;
            WindowTitle = string.Empty; // null will throw an exception
            Banner = string.Empty;
            StatusbarVisibility = settings.ShowStatusBar ? Visibility.Visible : Visibility.Collapsed;
            PopupManager.SetMaxWidth(400);
            PopupManager.SetWindowIcon(new Uri("/resources/book-32.png", UriKind.Relative));
        }
    }
}
