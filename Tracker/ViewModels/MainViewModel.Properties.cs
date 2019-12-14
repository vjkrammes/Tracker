using System.Collections.ObjectModel;
using System.Windows;

using Tracker.ECL.DTO;
using Tracker.Infrastructure;
using Tracker.Models;

namespace Tracker.ViewModels
{
    public partial class MainViewModel
    {
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

        private ObservableCollection<Client> _clients;
        public ObservableCollection<Client> Clients
        {
            get => _clients;
            set => SetProperty(ref _clients, value);
        }

        private Client _selectedClient;
        public Client SelectedClient
        {
            get => _selectedClient;
            set
            {
                SetProperty(ref _selectedClient, value);
                LoadClient();
            }
        }

        private ObservableCollection<Phone> _phones;
        public ObservableCollection<Phone> Phones
        {
            get => _phones;
            set => SetProperty(ref _phones, value);
        }

        private Phone _selectedPhone;
        public Phone SelectedPhone
        {
            get => _selectedPhone;
            set => SetProperty(ref _selectedPhone, value);
        }

        private ObservableCollection<Note> _notes;
        public ObservableCollection<Note> Notes
        {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }

        private Note _selectedNote;
        public Note SelectedNote
        {
            get => _selectedNote;
            set => SetProperty(ref _selectedNote, value);
        }

        private ObservableCollection<Hours> _hours;
        public ObservableCollection<Hours> Hours
        {
            get => _hours;
            set => SetProperty(ref _hours, value);
        }

        private Hours _selectedHours;
        public Hours SelectedHours
        {
            get => _selectedHours;
            set => SetProperty(ref _selectedHours, value);
        }

        private ObservableCollection<Mileage> _mileage;
        public ObservableCollection<Mileage> Mileage
        {
            get => _mileage;
            set => SetProperty(ref _mileage, value);
        }

        private Mileage _selectedMileage;
        public Mileage SelectedMileage
        {
            get => _selectedMileage;
            set => SetProperty(ref _selectedMileage, value);
        }

        private ObservableCollection<MenuInfo> _phoneTypeMenuItems;
        public ObservableCollection<MenuInfo> PhoneTypeMenuItems
        {
            get => _phoneTypeMenuItems;
            set => SetProperty(ref _phoneTypeMenuItems, value);
        }

        private ObservableCollection<MenuInfo> _clientTypeMenuItems;
        public ObservableCollection<MenuInfo> ClientTypeMenuItems
        {
            get => _clientTypeMenuItems;
            set => SetProperty(ref _clientTypeMenuItems, value);
        }
    }
}
