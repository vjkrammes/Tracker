using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using Tracker.ECL.DTO;
using Tracker.ECL.Interfaces;
using Tracker.Infrastructure;
using Tracker.Models;
using Tracker.Views;

using TrackerCommon;

namespace Tracker.ViewModels
{
    public class ClientTypeViewModel : ViewModelBase
    {
        #region Properties

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private ObservableCollection<string> _colors;
        public ObservableCollection<string> Colors
        {
            get => _colors;
            set => SetProperty(ref _colors, value);
        }

        private string _selectedColor;
        public string SelectedColor
        {
            get => _selectedColor;
            set => SetProperty(ref _selectedColor, value);
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
            set => SetProperty(ref _selectedType, value);
        }

        private ObservableCollection<MenuInfo> _colorMenuItems;
        public ObservableCollection<MenuInfo> ColorMenuItems
        {
            get => _colorMenuItems;
            set => SetProperty(ref _colorMenuItems, value);
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
        public ICommand RenamCommand
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

        private RelayCommand _changeColorCommand;
        public ICommand ChangeColorCommand
        {
            get
            {
                if (_changeColorCommand is null)
                {
                    _changeColorCommand = new RelayCommand(parm => ChangeColorClick(parm), parm => TypeSelected());
                }
                return _changeColorCommand;
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

        private bool AddCanClick() => !string.IsNullOrEmpty(Name) && SelectedColor != null;

        private void AddClick()
        {
            IClientTypeECL _ecl = Tools.Locator.ClientTypeECL;
            if (!ColorIsValid(out long color))
            {
                PopupManager.Popup($"'{SelectedColor}' is not a valid color specification", "Bad Color", PopupButtons.Ok,
                    PopupImage.Stop);
                FocusRequested?.Invoke(this, EventArgs.Empty);
                return;
            }
            if (_ecl.Read(Name) != null)
            {
                PopupManager.Popup($"A Client Type with the name '{Name}' already exists", "Duplicate Client Type",
                    PopupButtons.Ok, PopupImage.Stop);
                FocusRequested?.Invoke(this, EventArgs.Empty);
                return;
            }
            ClientType c = new ClientType
            {
                Name = Name.Caseify(),
                Background = SelectedColor,
                ARGB = color
            };
            try
            {
                _ecl.Insert(c);
            }
            catch (Exception ex)
            {
                PopupManager.Popup("Failed to insert new Client Type", Constants.DBE, ex.Innermost(), PopupButtons.Ok,
                    PopupImage.Error);
                FocusRequested?.Invoke(this, EventArgs.Empty);
                return;
            }
            int ix = 0;
            while (ix < Types.Count && Types[ix] < c)
            {
                ix++;
            }
            Types.Insert(ix, c);
            SelectedType = c;
            SelectedType = null;
            Name = string.Empty;
            SelectedColor = "White";
            FocusRequested?.Invoke(this, EventArgs.Empty);
        }

        private bool TypeSelected() => SelectedType != null;

        private void RenameClick()
        {
            IClientTypeECL _ecl = Tools.Locator.ClientTypeECL;
            if (SelectedType is null)
            {
                return;
            }
            string save = SelectedType.Name;
            QAViewModel vm = Tools.Locator.QAViewModel;
            vm.Question = "Name:";
            vm.Answer = SelectedType.Name;
            vm.AnswerRequired = true;
            vm.BorderBrush = (SolidColorBrush)Application.Current.Resources[Constants.Border];
            if (DialogSupport.ShowDialog<QAWindow>(vm) != true || vm.Answer.Caseify() == save)
            {
                SelectedType = null;
                FocusRequested?.Invoke(this, EventArgs.Empty);
                return;
            }
            if (_ecl.Read(vm.Answer) != null)
            {
                PopupManager.Popup($"A Client Type with the name '{vm.Answer}' already exists", "Duplicate Client Type",
                    PopupButtons.Ok, PopupImage.Stop);
                SelectedType = null;
                FocusRequested?.Invoke(this, EventArgs.Empty);
                return;
            }
            ClientType c = SelectedType.Clone();
            c.Name = vm.Answer.Caseify();
            try
            {
                _ecl.Update(c);
            }
            catch (Exception ex)
            {
                PopupManager.Popup("Failed to update Client Type", Constants.DBE, ex.Innermost(), PopupButtons.Ok, PopupImage.Error);
                SelectedType = null;
                FocusRequested?.Invoke(this, EventArgs.Empty);
                return;
            }
            Types.Remove(SelectedType);
            SelectedType = null;
            int ix = 0;
            while (ix < Types.Count && Types[ix] < c)
            {
                ix++;
            }
            Types.Insert(ix, c);
            SelectedType = c;
            SelectedType = null;
            FocusRequested?.Invoke(this, EventArgs.Empty);
        }

        private void ChangeColorClick(object parm)
        {
            if (!(parm is string color) || SelectedType is null)
            {
                SelectedType = null;
                FocusRequested?.Invoke(this, EventArgs.Empty);
                return;
            }
            string save = SelectedType.Background;
            SelectedType.Background = color;
            try
            {
                Tools.Locator.ClientTypeECL.Update(SelectedType);
            }
            catch (Exception ex)
            {
                SelectedType.Background = save;
                PopupManager.Popup("Failed to update Client Type", Constants.DBE, ex.Innermost(), PopupButtons.Ok, PopupImage.Error);
            }
            SelectedType = null;
            FocusRequested?.Invoke(this, EventArgs.Empty);
        }

        private bool DeleteCanClick() => SelectedType != null && !Tools.Locator.ClientECL.ClientTypeHasClients(SelectedType.Id);

        private void DeleteClick()
        {
            if (SelectedType is null || Tools.Locator.ClientECL.ClientTypeHasClients(SelectedType.Id))
            {
                SelectedType = null;
                FocusRequested?.Invoke(this, EventArgs.Empty);
                return;
            }
            try
            {
                Tools.Locator.ClientTypeECL.Delete(SelectedType);
            }
            catch (Exception ex)
            {
                PopupManager.Popup("Failed to delete Client Type", Constants.DBE, ex.Innermost(), PopupButtons.Ok, PopupImage.Error);
                SelectedType = null;
                FocusRequested?.Invoke(this, EventArgs.Empty);
                return;
            }
            Types.Remove(SelectedType);
            SelectedType = null;
            FocusRequested?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Events

        public event EventHandler FocusRequested;

        #endregion

        #region Utility Methods

        private bool ColorIsValid(string color, out long c)
        {
            c = 0;
            if (string.IsNullOrEmpty(color))
            {
                return false;
            }
            if (color.StartsWith("#"))
            {
                return long.TryParse(color[1..], NumberStyles.HexNumber, null, out c);
            }
            if (!Pallette.HasColor(color))
            {
                if (Regex.IsMatch(color, Constants.HexPattern))
                {
                    return long.TryParse(color, NumberStyles.HexNumber, null, out c);
                }
                else if (Regex.IsMatch(color, Constants.NumberPattern))
                {
                    return long.TryParse(color, out c);
                }
                else
                {
                    return false;
                }
            }
            c = Pallette.GetBrush(color).Color.Value();
            return true;
        }

        private bool ColorIsValid(out long c) => ColorIsValid(SelectedColor, out c);

        private void LoadColorMenuItems()
        {
            ColorMenuItems = new ObservableCollection<MenuInfo>();
            foreach (var color in Pallette.Names())
            {
                MenuInfo item = new MenuInfo
                {
                    Header = color,
                    Tag = color,
                    Icon = new Image { Source = new BitmapImage(new Uri("/resources/pallette-32.png", UriKind.Relative)) },
                    Command = ChangeColorCommand,
                    CommandParameter = color
                };
                ColorMenuItems.Add(item);
            }
        }

        #endregion

        public ClientTypeViewModel()
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
            Colors = new ObservableCollection<string>(Pallette.Names());
            SelectedColor = "White";
            LoadColorMenuItems();
        }
    }
}
