using System.Windows;

using Tracker.Infrastructure;

using TrackerLib;

namespace Tracker.ViewModels
{
    public class StatusbarViewModel : ViewModelBase
    {
        private readonly Context _context;

        private Visibility _statusbarVisibility;
        public Visibility StatusbarVisibility
        {
            get => _statusbarVisibility;
            set => SetProperty(ref _statusbarVisibility, value);
        }

        private int _clientCount;
        public int ClientCount
        {
            get => _clientCount;
            set => SetProperty(ref _clientCount, value);
        }

        private int _clientTypeCount;
        public int ClientTypeCount
        {
            get => _clientTypeCount;
            set => SetProperty(ref _clientTypeCount, value);
        }

        private int _hoursCount;
        public int HoursCount
        {
            get => _hoursCount;
            set => SetProperty(ref _hoursCount, value);
        }

        private int _mileageCount;
        public int MileageCount
        {
            get => _mileageCount;
            set => SetProperty(ref _mileageCount, value);
        }

        private int _noteCount;
        public int NoteCount
        {
            get => _noteCount;
            set => SetProperty(ref _noteCount, value);
        }

        private int _phoneCount;
        public int PhoneCount
        {
            get => _phoneCount;
            set => SetProperty(ref _phoneCount, value);
        }

        private int _phoneTypeCount;
        public int PhoneTypeCount
        {
            get => _phoneTypeCount;
            set => SetProperty(ref _phoneTypeCount, value);
        }

        private double _databaseSize;
        public double DatabaseSize
        {
            get => _databaseSize;
            set => SetProperty(ref _databaseSize, value);
        }

        private double _quota;
        public double Quota
        {
            get => _quota;
            set => SetProperty(ref _quota, value);
        }

        private double _howFull;
        public double HowFull
        {
            get => _howFull;
            set => SetProperty(ref _howFull, value);
        }

        private decimal _hoursTotal;
        public decimal HoursTotal
        {
            get => _hoursTotal;
            set => SetProperty(ref _hoursTotal, value);
        }

        private decimal _mileageTotal;
        public decimal MileageTotal
        {
            get => _mileageTotal;
            set => SetProperty(ref _mileageTotal, value);
        }

        public void Update()
        {
            ClientCount = Tools.Locator.ClientECL.Count;
            ClientTypeCount = Tools.Locator.ClientTypeECL.Count;
            HoursCount = Tools.Locator.HoursECL.Count;
            MileageCount = Tools.Locator.MileageECL.Count;
            NoteCount = Tools.Locator.NoteECL.Count;
            PhoneCount = Tools.Locator.PhoneECL.Count;
            PhoneTypeCount = Tools.Locator.PhoneTypeECL.Count;
            HoursTotal = Tools.Locator.HoursECL.TotalHours();
            MileageTotal = Tools.Locator.MileageECL.TotalMiles();
            DatabaseSize = _context.DatabaseInfo().Size;
            Quota = 10_000_000_000.0;
            HowFull = DatabaseSize / Quota;
        }

        public StatusbarViewModel(Context context) => _context = context;
    }
}
