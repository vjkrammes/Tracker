using System;
using System.Windows.Input;
using System.Windows.Media;

using Tracker.ECL.DTO;
using Tracker.Infrastructure;

using TrackerCommon;

namespace Tracker.ViewModels
{
    public class HoursViewModel : ViewModelBase
    {
        #region Properties

        private Client _client;
        public Client Client
        {
            get => _client;
            set
            {
                SetProperty(ref _client, value);
                if (Client is null || Client.ClientType is null || string.IsNullOrEmpty(Client.ClientType.Background))
                {
                    StartColor = Colors.White;
                    EndColor = Colors.White;
                }
                else
                {
                    (StartColor, EndColor) = Pallette.GetBrush(Client.ClientType.Background).Color.Adjust();
                }
            }
        }

        private Hours _hours;
        public Hours Hours
        {
            get => _hours;
            set
            {
                SetProperty(ref _hours, value);
                if (Hours is null || Hours.Id == 0)
                {
                    Date = DateTime.Now;
                }
                else
                {
                    Date = Hours.Date;
                }
            }
        }

        private DateTime? _date;
        public DateTime? Date
        {
            get => _date;
            set
            {
                SetProperty(ref _date, value);
                Hours.Date = Date ?? (default);
            }
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

        public override bool OkCanExecute() => Date.HasValue && Date.Value != default && Hours.Time > 0M;

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

        public HoursViewModel()
        {
            Hours = new Hours();
            Date = DateTime.Now;
            StartColor = Colors.White;
            EndColor = Colors.White;
        }
    }
}
