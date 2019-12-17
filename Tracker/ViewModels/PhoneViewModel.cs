using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

using Tracker.ECL.DTO;
using Tracker.ECL.Interfaces;
using Tracker.Infrastructure;

using TrackerCommon;

namespace Tracker.ViewModels
{
    public class PhoneViewModel : ViewModelBase
    {
        #region Properties

        private Client _client;
        public Client Client
        {
            get => _client;
            set
            {
                SetProperty(ref _client, value);
                if (Client != null && Client.ClientType != null && !string.IsNullOrEmpty(Client.ClientType.Background))
                {
                    (StartColor, EndColor) = Pallette.GetBrush(Client.ClientType.Background).Color.Adjust();
                }
            }
        }

        private ObservableCollection<PhoneType> _types;
        public ObservableCollection<PhoneType> Types
        {
            get => _types;
            set => SetProperty(ref _types, value);
        }

        private PhoneType _selectedType;
        public PhoneType SelectedType
        {
            get => _selectedType;
            set => SetProperty(ref _selectedType, value);
        }

        private string _number;
        public string Number
        {
            get => _number;
            set => SetProperty(ref _number, value);
        }

        private Color _startColor;
        public Color StartColor
        {
            get => _startColor;
            set => SetProperty(ref _startColor, value);
        }

        private Color _endColor;
        public Color EndColor
        {
            get => _endColor;
            set => SetProperty(ref _endColor, value);
        }

        #endregion

        #region Commands

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

        public override bool OkCanExecute() => SelectedType != null && !string.IsNullOrEmpty(Number);

        private void WindowLoaded()
        {
            if (Client is null)
            {
                PopupManager.Popup("Client not set", "Application Error", PopupButtons.Ok, PopupImage.Error);
                Cancel();
                return;
            }
        }

        #endregion

        public PhoneViewModel(IPhoneTypeECL ecl)
        {
            try
            {
                Types = new ObservableCollection<PhoneType>(ecl.Get());
            }
            catch (Exception ex)
            {
                PopupManager.Popup("Failed to retrieve Phone Types", Constants.DBE, ex.Innermost(), PopupButtons.Ok, PopupImage.Error);
                Cancel();
                return;
            }
        }
    }
}
