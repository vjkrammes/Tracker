using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Tracker.Infrastructure;
using TrackerCommon;

namespace Tracker.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Properties

        private string _windowTitle;
        public string WindowTitle
        {
            get => _windowTitle;
            set => SetProperty(ref _windowTitle, value);
        }

        private string _banner;
        public string Banner
        {
            get => _banner;
            set => SetProperty(ref _banner, value);
        }

        private Visibility _statusbarVisibility;
        public Visibility StatusbarVisibility
        {
            get => _statusbarVisibility;
            set
            {
                SetProperty(ref _statusbarVisibility, value);
                Tools.Locator.StatusbarViewModel.StatusbarVisibility = StatusbarVisibility;
            }
        }

        #endregion

        #region Commands

        private RelayCommand _toggleStatusbarCommand;
        public ICommand ToggleStatusbarCommand
        {
            get
            {
                if (_toggleStatusbarCommand is null)
                {
                    _toggleStatusbarCommand = new RelayCommand(parm => ToggleStatusbar(), parm => AlwaysCanExecute());
                }
                return _toggleStatusbarCommand;
            }
        }

        private RelayCommand _iconHeightCommand;
        public ICommand IconHeightCommand
        {
            get
            {
                if (_iconHeightCommand is null)
                {
                    _iconHeightCommand = new RelayCommand(parm => IconHeight(parm), parm => AlwaysCanExecute());
                }
                return _iconHeightCommand;
            }
        }

        private RelayCommand _manageClientsCommand;
        public ICommand ManageClientsCommand
        {
            get
            {
                if (_manageClientsCommand is null)
                {
                    _manageClientsCommand = new RelayCommand(parm => ManageClientsClick(), parm => AlwaysCanExecute());
                }
                return _manageClientsCommand;
            }
        }

        private RelayCommand _managePhoneTypesCommand;
        public ICommand ManagePhoneTypesCommand
        {
            get
            {
                if (_managePhoneTypesCommand is null)
                {
                    _managePhoneTypesCommand = new RelayCommand(parm => ManagePhoneTypesClick(), parm => AlwaysCanExecute());
                }
                return _managePhoneTypesCommand;
            }
        }

        private RelayCommand _backupCommand;
        public ICommand BackupCommand
        {
            get
            {
                if (_backupCommand is null)
                {
                    _backupCommand = new RelayCommand(parm => BackupClick(), parm => AlwaysCanExecute());
                }
                return _backupCommand;
            }
        }

        private RelayCommand _palletteCommand;
        public ICommand PalletteCommand
        {
            get
            {
                if (_palletteCommand is null)
                {
                    _palletteCommand = new RelayCommand(parm => PalletteClick(), parm => AlwaysCanExecute());
                }
                return _palletteCommand;
            }
        }

        private RelayCommand _aboutCommand;
        public ICommand AboutCommand
        {
            get
            {
                if (_aboutCommand is null)
                {
                    _aboutCommand = new RelayCommand(parm => AboutClick(), parm => AlwaysCanExecute());
                }
                return _aboutCommand;
            }
        }

        private RelayCommand _windowLoadedCommand;
        public ICommand WindowLoadedCommand
        {
            get
            {
                if (_windowLoadedCommand is null)
                {
                    _windowLoadedCommand = new RelayCommand(parm => WindowLoaded(), parm => AlwaysCanExecute());
                }
                return _windowLoadedCommand;
            }
        }

        #endregion

        #region Command Methods

        public override void Cancel()
        {
            Application.Current.MainWindow.Close();
        }

        private void ToggleStatusbar()
        {
            switch (StatusbarVisibility)
            {
                case Visibility.Visible:
                    StatusbarVisibility = Visibility.Collapsed;
                    Tools.Locator.Settings.ShowStatusBar = false;
                    break;
                default:
                    StatusbarVisibility = Visibility.Visible;
                    Tools.Locator.Settings.ShowStatusBar = true;
                    break;
            }
        }

        private void IconHeight(object parm)
        {
            if (!(parm is string heightstring) || !double.TryParse(heightstring, out double height))
            {
                return;
            }
            Application.Current.Resources[Constants.IconHeight] = height;
            Tools.Locator.Settings.IconHeight = height;
        }

        private void ManageClientsClick()
        {

        }

        private void ManagePhoneTypesClick()
        {

        }

        private void BackupClick()
        {

        }

        private void PalletteClick()
        {

        }

        private void AboutClick()
        {

        }

        private void WindowLoaded()
        {
            var settings = Tools.Locator.Settings;
            WindowTitle = $"{settings.ProductName} Version {settings.ProductVersion:0.00}";
            Banner = $"{settings.ProductName} {settings.ProductVersion:0.00} - Track your Hours and Mileage";
            Tools.Locator.StatusbarViewModel.Update();
        }

        #endregion

        #region Utility Methods

        

        #endregion

        public MainViewModel()
        {
            var settings = Tools.Locator.Settings;
            Pallette.SetSystemColors();
            Application.Current.Resources[Constants.IconHeight] = settings.IconHeight;
            WindowTitle = string.Empty; // null will throw an exception
            Banner = string.Empty;
            StatusbarVisibility = settings.ShowStatusBar ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
