using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

using Tracker.ECL.DTO;
using Tracker.Infrastructure;

using TrackerCommon;

namespace Tracker.ViewModels
{
    public class ClientViewModel : ViewModelBase
    {
        #region Properties

        private string _savedName;

        private Client _client;
        public Client Client
        {
            get => _client;
            set
            {
                SetProperty(ref _client, value);
                _editing = Client.Id != 0;
                _savedName = Client.Name;
                if (_editing)
                {
                    SelectedType = Client.ClientType;
                }
            }
        }

        private ObservableCollection<ClientType> _types;
        public ObservableCollection<ClientType> Types
        {
            get => _types;
            set => SetProperty(ref _types, value);
        }

        private ClientType _selectedType;
        public ClientType SelectedType
        {
            get => _selectedType;
            set
            {
                SetProperty(ref _selectedType, value);
                if (SelectedType != null)
                {
                    Client.ClientTypeId = SelectedType.Id;
                    Client.ClientType = SelectedType;
                    (StartColor, EndColor) = Pallette.GetBrush(SelectedType.Background).Color.Adjust();
                }
                else
                {
                    Client.ClientTypeId = 0;
                    Client.ClientType = null;
                    StartColor = Colors.White;
                    EndColor = Colors.White;
                }
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

        private bool _editing;

        #endregion

        #region Command Methods

        public override bool OkCanExecute() => !string.IsNullOrEmpty(Client.Name) && SelectedType != null;

        public override void OK()
        {
            if (_editing && !Client.Name.Equals(_savedName, StringComparison.OrdinalIgnoreCase))
            {
                if (Tools.Locator.ClientECL.Read(Client.Name) != null)
                {
                    Duplicate();
                    return;
                }
            }
            else if (!_editing && Tools.Locator.ClientECL.Read(Client.Name) != null)
            {
                Duplicate();
                return;
            }
            base.OK();
        }

        #endregion

        #region Events

        public event EventHandler FocusRequested;

        #endregion

        #region Utility Methods

        private void Duplicate()
        {
            PopupManager.Popup($"A Client with the name '{Client.Name}' already exists", "Duplicate Client", PopupButtons.Ok,
                PopupImage.Stop);
            FocusRequested?.Invoke(this, EventArgs.Empty);
        }

        private void LoadClientTypes()
        {
            try
            {
                Types = new ObservableCollection<ClientType>(Tools.Locator.ClientTypeECL.Get());
            }
            catch (Exception ex)
            {
                PopupManager.Popup("Failed to retrieve Client Types", Constants.DBE, ex.Innermost(), PopupButtons.Ok, PopupImage.Error);
                Cancel();
                return;
            }
        }

        #endregion

        public ClientViewModel()
        {
            Client = new Client();
            LoadClientTypes();
            StartColor = Colors.White;
            EndColor = Colors.White;
        }
    }
}
