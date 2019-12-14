using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Text;
using Tracker.ECL.DTO;
using Tracker.ECL.Interfaces;
using Tracker.Infrastructure;
using Tracker.Views;
using Tracker.Models;
using TrackerCommon;
using System.Reflection;
using System.Resources;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Tracker.ViewModels
{
    public class PhoneTypeViewModel : ViewModelBase
    {
        #region Properties

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private ObservableCollection<string> _icons;
        public ObservableCollection<string> Icons
        {
            get => _icons;
            set => SetProperty(ref _icons, value);
        }

        private string _selectedIcon;
        public string SelectedIcon
        {
            get => _selectedIcon;
            set => SetProperty(ref _selectedIcon, value);
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

        private ObservableCollection<MenuInfo> _iconMenuItems;
        public ObservableCollection<MenuInfo> IconMenuItems
        {
            get => _iconMenuItems;
            set => SetProperty(ref _iconMenuItems, value);
        }

        #endregion

        #region Commands

        private RelayCommand _addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand is null)
                {
                    _addCommand = new RelayCommand(parm => AddClick(), parm => AddCanClick());
                }
                return _addCommand;
            }
        }

        private RelayCommand _renameCommand;
        public ICommand RenameCommand
        {
            get
            {
                if (_renameCommand is null)
                {
                    _renameCommand = new RelayCommand(parm => RenameClick(), parm => TypeSelected());
                }
                return _renameCommand;
            }
        }

        private RelayCommand _changeIconCommand;
        public ICommand ChangeIconCommand
        {
            get
            {
                if (_changeIconCommand is null)
                {
                    _changeIconCommand = new RelayCommand(parm => ChangeIcon(parm), parm => TypeSelected());
                }
                return _changeIconCommand;
            }
        }

        private RelayCommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand is null)
                {
                    _deleteCommand = new RelayCommand(parm => DeleteClick(), parm => DeleteCanClick());
                }
                return _deleteCommand;
            }
        }

        #endregion

        #region Command Methods

        private bool AddCanClick() => !string.IsNullOrEmpty(Name) && SelectedIcon != null;

        private void AddClick()
        {

        }

        private bool TypeSelected() => SelectedType != null;

        private void RenameClick()
        {

        }

        private void ChangeIcon(object parm)
        {
            if (SelectedType is null || !(parm is string uri) || string.IsNullOrEmpty(uri))
            {
                SelectedType = null;
                FocusRequested?.Invoke(this, EventArgs.Empty);
                return;
            }
        }

        private bool DeleteCanClick() => SelectedType != null && !Tools.Locator.PhoneECL.PhoneTypeHasPhones(SelectedType.Id);

        private void DeleteClick()
        {

        }

        #endregion

        #region Events

        public event EventHandler FocusRequested;

        #endregion

        #region Utility Methods

        private void LoadIconMenuItems()
        {
            Icons = new ObservableCollection<string>();
            IconMenuItems = new ObservableCollection<MenuInfo>();
            Assembly a = Assembly.GetExecutingAssembly();
            string[] resources = a.GetManifestResourceNames();
            foreach (var resource in resources)
            {
                if (!resource.Contains("g.resources"))
                {
                    continue;
                }
                ResourceSet rs = null;
                try
                {
                    rs = new ResourceSet(a.GetManifestResourceStream(resource));
                }
                catch
                {
                    continue;
                }
                var hashes = rs.Cast<DictionaryEntry>().ToList();
                var keys = new List<string>();
                foreach (DictionaryEntry hash in hashes)
                {
                    if (!hash.Key.ToString().EndsWith("-32.png"))
                    {
                        continue;
                    }
                    keys.Add(hash.Key.ToString());
                }
                var uris = from k in keys orderby k select k;
                foreach (var uri in uris)
                {
                    string u = uri;
                    if (!u.StartsWith("/"))
                    {
                        u = "/" + u;
                    }
                    string l = u.ToLower();
                    if (l.Contains("database"))
                    {
                        continue;
                    }
                    if (!l.Contains("resources/"))
                    {
                        continue;
                    }
                    string header = l.Replace("resources/", "").Replace("-32.png", "").TrimStart('/');
                    string[] parts = header.Split('-');
                    if (parts.Length <= 1)
                    {
                        header = parts[0];
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < parts.Length; i++)
                        {
                            sb.Append(parts[i]);
                            if (i < parts.Length - 1)
                            {
                                sb.Append("-");
                            }
                        }
                        header = sb.ToString();
                    }
                    header = header.Capitalize();
                    Icons.Add(u);
                    IconMenuItems.Add(new MenuInfo
                    {
                        Header = header,
                        Tag = u,
                        Icon = new Image { Source = new BitmapImage(new Uri(u, UriKind.Relative)) },
                        Command = ChangeIconCommand,
                        CommandParameter = u
                    });
                }
            }
        }

        #endregion

        public PhoneTypeViewModel()
        {
            try
            {
                Types = new ObservableCollection<PhoneType>(Tools.Locator.PhoneTypeECL.Get());
            }
            catch (Exception ex)
            {
                PopupManager.Popup("Failed to retrieve Phone Types", Constants.DBE, ex.Innermost(), PopupButtons.Ok, PopupImage.Error);
                Cancel();
                return;
            }
            LoadIconMenuItems();
        }
    }
}
