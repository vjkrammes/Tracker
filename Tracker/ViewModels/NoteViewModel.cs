using System;
using System.Windows.Input;
using System.Windows.Media;
using Tracker.ECL.DTO;
using Tracker.Infrastructure;
using TrackerCommon;

namespace Tracker.ViewModels
{
    public class NoteViewModel : ViewModelBase
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

        private DateTime? _date;
        public DateTime? Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        private string _text;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
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

        public override bool OkCanExecute() => !string.IsNullOrEmpty(Text) && Date != null && Date.Value != default;

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

        public NoteViewModel()
        {
            Date = DateTime.Now;
            Text = string.Empty;
            StartColor = Colors.White;
            EndColor = Colors.White;
        }
    }
}
