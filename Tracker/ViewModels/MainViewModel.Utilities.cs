using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

using Tracker.ECL.DTO;
using Tracker.Infrastructure;
using Tracker.Models;
using Tracker.Views;

using TrackerCommon;

using TrackerLib.Interfaces;

namespace Tracker.ViewModels
{
    public partial class MainViewModel
    {
        private void Authenticate(ISettingsService settings)
        {
            var vm = Tools.Locator.PasswordViewModel;
            vm.Password2Visibility = Visibility.Collapsed;
            vm.Salt = settings.PasswordSalt.ArrayCopy();
            vm.Hash = settings.PasswordHash.ArrayCopy();
            if (DialogSupport.ShowDialog<PasswordWindow>(vm, Application.Current.MainWindow) != true)
            {
                Environment.Exit(Constants.NoPasswordEntered);
            }
            var hash = Tools.GenerateHash(Encoding.UTF8.GetBytes(vm.Password1), settings.PasswordSalt, Constants.HashIterations,
                Constants.HashLength);
            if (!hash.ArrayEquals(settings.PasswordHash))
            {
                PopupManager.Popup("Incorrect Password Entered", "Incorrect Password", PopupButtons.Ok, PopupImage.Error);
                Environment.Exit(Constants.BadPasswordEntered);
            }
        }

        private void LoadClients()
        {
            try
            {
                Clients = new ObservableCollection<Client>(Tools.Locator.ClientECL.Get());
            }
            catch (Exception ex)
            {
                PopupManager.Popup("Failed to load Clients", Constants.DBE, ex.Innermost(), PopupButtons.Ok, PopupImage.Error);
                Environment.Exit(Constants.ClientLoadFailed);
            }
        }

        private void LoadPhoneTypeMenuItems()
        {
            PhoneTypeMenuItems = new ObservableCollection<Models.MenuInfo>();
            var phonetypes = Tools.Locator.PhoneTypeECL.Get();
            foreach (var pt in phonetypes)
            {
                MenuInfo item = new MenuInfo
                {
                    Header = pt.Name,
                    Tag = pt,
                    Icon = new Image { Source = new BitmapImage(new Uri(pt.ImageUri, UriKind.Relative)) },
                    Command = ChangePhoneTypeCommand,
                    CommandParameter = pt
                };
                PhoneTypeMenuItems.Add(item);
            }
        }

        private void LoadClientTypeMenuItems()
        {
            ClientTypeMenuItems = new ObservableCollection<MenuInfo>();
            var clienttypes = Tools.Locator.ClientTypeECL.Get();
            foreach (var ct in clienttypes)
            {
                MenuInfo item = new MenuInfo
                {
                    Header = ct.Name,
                    Tag = ct,
                    Icon = new Image { Source = new BitmapImage(new Uri("/resources/user-32.png", UriKind.Relative)) },
                    Command = ChangeClientTypeCommand,
                    CommandParameter = ct
                };
                ClientTypeMenuItems.Add(item);
            }
        }

        private void LoadPhones()
        {
            try
            {
                Phones = new ObservableCollection<Phone>(Tools.Locator.PhoneECL.GetForClient(SelectedClient.Id));
            }
            catch (Exception ex)
            {
                PopupManager.Popup("Failed to retrieve Phones for selected Client", Constants.DBE, ex.Innermost(),
                    PopupButtons.Ok, PopupImage.Warning);
            }
        }

        private void LoadNotes()
        {
            try
            {
                Notes = new ObservableCollection<Note>(Tools.Locator.NoteECL.GetForClient(SelectedClient.Id));
            }
            catch (Exception ex)
            {
                PopupManager.Popup("Failed to retrieve Notes for selected Client", Constants.DBE, ex.Innermost(),
                    PopupButtons.Ok, PopupImage.Warning);
            }
        }

        private void LoadHours()
        {
            try
            {
                Hours = new ObservableCollection<Hours>(Tools.Locator.HoursECL.GetForClient(SelectedClient.Id));
            }
            catch (Exception ex)
            {
                PopupManager.Popup("Failed to retrieve Hours for selected Client", Constants.DBE, ex.Innermost(),
                    PopupButtons.Ok, PopupImage.Warning);
            }
        }

        private void LoadMileage()
        {
            try
            {
                Mileage = new ObservableCollection<Mileage>(Tools.Locator.MileageECL.GetForClient(SelectedClient.Id));
            }
            catch (Exception ex)
            {
                PopupManager.Popup("Failed to retrieve Mileage for selected Client", Constants.DBE, ex.Innermost(),
                    PopupButtons.Ok, PopupImage.Warning);
            }
        }

        private void LoadClient()
        {
            if (SelectedClient is null)
            {
                Notes = null;
                Phones = null;
                Hours = null;
                Mileage = null;
            }
            else
            {
                LoadPhones();
                LoadNotes();
                LoadHours();
                LoadMileage();
            }
        }
    }
}
