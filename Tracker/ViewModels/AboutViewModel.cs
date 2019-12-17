using System.Linq;
using System.Reflection;

using Tracker.Controls;
using Tracker.Infrastructure;

using TrackerLib.Interfaces;

namespace Tracker.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        #region Properties

        private ObservableDictionary<string, string> _credits;
        public ObservableDictionary<string, string> Credits
        {
            get => _credits;
            set => SetProperty(ref _credits, value);
        }

        private ISettingsService _settings;
        public ISettingsService Settings
        {
            get => _settings;
            set => SetProperty(ref _settings, value);
        }

        private string _shortTitle;
        public string ShortTitle
        {
            get => _shortTitle;
            set => SetProperty(ref _shortTitle, value);
        }

        #endregion

        #region Utility Methods

        private string GetCopyrightFromAssembly()
        {
            var assem = GetType().Assembly;
            object[] attributes = assem.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), true);
            if (attributes != null && attributes.Any())
            {
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
            return "Copyright Information Unavailable";
        }

        private string GetCompanyFromAssembly()
        {
            var assem = GetType().Assembly;
            object[] attributes = assem.GetCustomAttributes(typeof(AssemblyCompanyAttribute), true);
            if (attributes != null && attributes.Any())
            {
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
            return "Company Information Unavailable";
        }

        #endregion

        public AboutViewModel(ISettingsService settings)
        {
            _settings = settings;
            ShortTitle = $"{_settings.ProductName} {_settings.ProductVersion:0.00}";
            Credits = new ObservableDictionary<string, string>
            {
                ["System Id"] = _settings.SystemId.ToString(),
                ["Product"] = _settings.ProductName,
                ["Version"] = _settings.ProductVersion.ToString("n2"),
                ["Author"] = "V. James Krammes",
                ["Company"] = GetCompanyFromAssembly(),
                ["Copyright"] = GetCopyrightFromAssembly(),
                ["Platform"] = "Windows Desktop",
                ["Architecture"] = "Model - View - ViewModel (MVVM)",
                [".NET Version"] = "Microsoft® .NET Core 3.1",
                ["Presentation"] = "Windows Presentation Foundation (WPF)",
                ["Database"] = "Microsoft® SQL (T-SQL)",
                ["Database Access"] = "Microsoft® Entity Framework Core",
                ["Entity Mapping"] = "Automapper by Jimmy Bogard",
                ["Excel Interface"] = "EPPlus by Jan Källman",
                ["Text Handling"] = "Humanizer by Mehdi Khalili, Oren Novotny",
                ["Repository"] = "https://github.com/vjkrammes/Tracker"
            };
        }
    }
}
