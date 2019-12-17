using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

using AutoMapper;
using Microsoft.Win32;
using Tracker.ECL.DTO;
using Tracker.ECL.Interfaces;
using Tracker.Infrastructure;
using Tracker.Models;

using TrackerCommon;

using TrackerLib;

namespace Tracker.ViewModels
{
    public class ReportsViewModel : ViewModelBase
    {
        #region Properties

        private ObservableCollection<int> _years;
        public ObservableCollection<int> Years
        {
            get => _years;
            set => SetProperty(ref _years, value);
        }

        private int? _selectedYear;
        public int? SelectedYear
        {
            get => _selectedYear;
            set
            {
                SetProperty(ref _selectedYear, value);
                LoadHoursAndMileage();
            }
        }

        private bool _allYears;
        public bool AllYears
        {
            get => _allYears;
            set
            {
                SetProperty(ref _allYears, value);
                if (AllYears)
                {
                    SingleYear = false;
                    RangeOfYears = false;
                    SelectedYear = null;
                    StartYear = 0;
                    EndYear = 0;
                    LoadHoursAndMileage();
                }
            }
        }

        private bool _singleYear;
        public bool SingleYear
        {
            get => _singleYear;
            set
            {
                SetProperty(ref _singleYear, value);
                if (SingleYear)
                {
                    AllYears = false;
                    RangeOfYears = false;
                    StartYear = 0;
                    EndYear = 0;
                    LoadHoursAndMileage();
                }
            }
        }

        private bool _rangeOfYears;
        public bool RangeOfYears
        {
            get => _rangeOfYears;
            set
            {
                SetProperty(ref _rangeOfYears, value);
                if (RangeOfYears)
                {
                    AllYears = false;
                    SingleYear = false;
                    SelectedYear = null;
                    LoadHoursAndMileage();
                }
            }
        }

        private int _startYear;
        public int StartYear
        {
            get => _startYear;
            set
            {
                SetProperty(ref _startYear, value);
                LoadHoursAndMileage();
            }
        }

        private int _endYear;
        public int EndYear
        {
            get => _endYear;
            set
            {
                SetProperty(ref _endYear, value);
                LoadHoursAndMileage();
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
                LoadHoursAndMileage();
            }
        }

        private bool _allClients;
        public bool AllClients
        {
            get => _allClients;
            set
            {
                SetProperty(ref _allClients, value);
                if (AllClients)
                {
                    SingleClient = false;
                    Clients = null;
                    LoadHoursAndMileage();
                }
            }
        }

        private bool _singleClient;
        public bool SingleClient
        {
            get => _singleClient;
            set
            {
                SetProperty(ref _singleClient, value);
                if (SingleClient)
                {
                    AllClients = false;
                    Clients = new ObservableCollection<Client>(_clientList);
                    LoadHoursAndMileage();
                }
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                Clients = new ObservableCollection<Client>
                    (from c in _clientList
                     where c.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)
                     select c);
                LoadHoursAndMileage();
            }
        }

        private ObservableCollection<HoursModel> _extractedHours;
        public ObservableCollection<HoursModel> ExtractedHours
        {
            get => _extractedHours;
            set => SetProperty(ref _extractedHours, value);
        }

        private HoursModel _selectedExtractedHours;
        public HoursModel SelectedExtractedHours
        {
            get => _selectedExtractedHours;
            set => SetProperty(ref _selectedExtractedHours, value);
        }

        private ObservableCollection<MileageModel> _extractedMileage;
        public ObservableCollection<MileageModel> ExtractedMileage
        {
            get => _extractedMileage;
            set => SetProperty(ref _extractedMileage, value);
        }

        private MileageModel _selectedExtractedMileage;
        public MileageModel SelectedExtractedMileage
        {
            get => _selectedExtractedMileage;
            set => SetProperty(ref _selectedExtractedMileage, value);
        }

        private decimal _totalHours;
        public decimal TotalHours
        {
            get => _totalHours;
            set => SetProperty(ref _totalHours, value);
        }

        private decimal _totalMiles;
        public decimal TotalMiles
        {
            get => _totalMiles;
            set => SetProperty(ref _totalMiles, value);
        }

        private readonly IClientECL _clientECL;
        private readonly IHoursECL _hoursECL;
        private readonly IMileageECL _mileageECL;
        private readonly IMapper _mapper;
        private readonly List<Client> _clientList;

        #endregion

        #region Commands

        private RelayCommand _exportCommand;
        public ICommand ExportCommand
        {
            get
            {
                if (_exportCommand is null)
                {
                    _exportCommand = new RelayCommand(parm => ExportClick(), parm => ExportCanClick());
                }
                return _exportCommand;
            }
        }

        private RelayCommand _clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                if (_clearCommand is null)
                {
                    _clearCommand = new RelayCommand(parm => ClearClick(), parm => ClearCanClick());
                }
                return _clearCommand;
            }
        }

        private RelayCommand _resetCommand;
        public ICommand ResetCommand
        {
            get
            {
                if (_resetCommand is null)
                {
                    _resetCommand = new RelayCommand(parm => ResetClick(), parm => AlwaysCanExecute());
                }
                return _resetCommand;
            }
        }

        #endregion

        #region Command Methods

        private bool ExportCanClick() => ExtractedHours != null && ExtractedHours.Any() && ExtractedMileage != null && ExtractedMileage.Any();

        private void ExportClick()
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                DefaultExt = "*.xlsx",
                AddExtension = true
            };
            if (sfd.ShowDialog() != true)
            {
                return;
            }
            var excelmanager = Tools.Locator.ExcelManager;
            try
            {
                excelmanager.Create(new FileInfo(sfd.FileName), ExtractedHours, ExtractedMileage);
            }
            catch (Exception ex)
            {
                PopupManager.Popup("Excel Build Error", "Excel Error", ex.Innermost(), PopupButtons.Ok, PopupImage.Error);
                return;
            }
            string msg = $"The Excel Spreadsheet '{sfd.FileName}' was created successfully";
            PopupManager.Popup("Spreadsheet created successfully", "Export Complete", msg, PopupButtons.Ok, PopupImage.Information);
        }

        private bool ClearCanClick() => !string.IsNullOrEmpty(SearchText) || SelectedClient != null;

        private void ClearClick()
        {
            SearchText = string.Empty;
            Clients = new ObservableCollection<Client>(_clientList);
        }

        private void ResetClick()
        {
            AllYears = true;
            SelectedYear = null;
            StartYear = 0;
            EndYear = 0;
            AllClients = true;
            TotalHours = 0M;
            TotalMiles = 0M;
        }

        #endregion

        #region Utility Methods

        private void LoadHoursAndMileage()
        {
            ExtractedHours = new ObservableCollection<HoursModel>();
            ExtractedMileage = new ObservableCollection<MileageModel>();
            TotalHours = 0M;
            TotalMiles = 0M;
            if (SingleClient && SelectedClient is null)
            {
                return;
            }
            if (SingleYear && SelectedYear is null)
            {
                return;
            }
            if (RangeOfYears && (StartYear == 0 || EndYear == 0))
            {
                return;
            }
            if (AllYears)
            {
                if (AllClients)
                {
                    foreach (var client in _clientList)
                    {
                        foreach (var hours in _hoursECL.GetForClient(client.Id))
                        {
                            ExtractedHours.Add(new HoursModel(client, hours));
                        }
                        foreach (var mileage in _mileageECL.GetForClient(client.Id))
                        {
                            ExtractedMileage.Add(new MileageModel(client, mileage));
                        }
                    }
                }
                else if (SingleClient)
                {
                    foreach (var hours in _hoursECL.GetForClient(SelectedClient.Id))
                    {
                        ExtractedHours.Add(new HoursModel(SelectedClient, hours));
                    }
                    foreach (var mileage in _mileageECL.GetForClient(SelectedClient.Id))
                    {
                        ExtractedMileage.Add(new MileageModel(SelectedClient, mileage));
                    }
                }
            }
            else if (SingleYear)
            {
                if (AllClients)
                {
                    foreach (var client in _clientList)
                    {
                        foreach (var hours in _hoursECL.ExtractYear(SelectedYear.Value))
                        {
                            ExtractedHours.Add(new HoursModel(client, hours));
                        }
                        foreach (var mileage in _mileageECL.ExtractYear(SelectedYear.Value))
                        {
                            ExtractedMileage.Add(new MileageModel(client, mileage));
                        }
                    }
                }
                else if (SingleClient)
                {
                    foreach (var hours in _hoursECL.ExtractYear(SelectedYear.Value))
                    {
                        ExtractedHours.Add(new HoursModel(SelectedClient, hours));
                    }
                    foreach (var mileage in _mileageECL.ExtractYear(SelectedYear.Value))
                    {
                        ExtractedMileage.Add(new MileageModel(SelectedClient, mileage));
                    }
                }
            }
            else
            {
                if (AllClients)
                {
                    foreach (var client in _clientList)
                    {
                        foreach (var hours in _hoursECL.ExtractRange(StartYear, EndYear))
                        {
                            ExtractedHours.Add(new HoursModel(client, hours));
                        }
                        foreach (var mileage in _mileageECL.ExtractRange(StartYear, EndYear))
                        {
                            ExtractedMileage.Add(new MileageModel(client, mileage));
                        }
                    }
                }
                else if (SingleClient)
                {
                    foreach (var hours in _hoursECL.ExtractRange(StartYear, EndYear))
                    {
                        ExtractedHours.Add(new HoursModel(SelectedClient, hours));
                    }
                    foreach (var mileage in _mileageECL.ExtractRange(StartYear, EndYear))
                    {
                        ExtractedMileage.Add(new MileageModel(SelectedClient, mileage));
                    }
                }
            }
            TotalHours = ExtractedHours.Sum(x => x.Time);
            TotalMiles = ExtractedMileage.Sum(x => x.Miles);
        }


        #endregion

        public ReportsViewModel(IMapper mapper, Context context, IClientECL cecl, IHoursECL hecl, IMileageECL mecl)
        {
            _clientECL = cecl;
            _hoursECL = hecl;
            _mileageECL = mecl;
            _mapper = mapper;
            try
            {
                Years = new ObservableCollection<int>(context.Years());
            }
            catch (Exception ex)
            {
                PopupManager.Popup("Failed to retrieve Years", Constants.DBE, ex.Innermost(), PopupButtons.Ok, PopupImage.Error);
                Cancel();
                return;
            }
            try
            {
                _clientList = _mapper.Map<List<Client>>(_clientECL.Get());
            }
            catch (Exception ex)
            {
                PopupManager.Popup("Failed to retrieve Clients", Constants.DBE, ex.Innermost(), PopupButtons.Ok, PopupImage.Error);
                Cancel();
                return;
            }
            TotalHours = 0M;
            TotalMiles = 0M;
            AllYears = true;
            SelectedYear = null;
            StartYear = 0;
            EndYear = 0;
            AllClients = true;
        }
    }
}