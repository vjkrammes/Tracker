using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Tracker.ECL.DTO;
using Tracker.Infrastructure;
using Tracker.Models;
using TrackerCommon;

namespace Tracker.ViewModels
{
    public partial class MainViewModel
    {
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
                    Icon = new Image { Source = new BitmapImage(new Uri(pt.ImageUri)) },
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
                Notes = new ObservableCollection<Note>(SelectedClient.Notes);
                Phones = new ObservableCollection<Phone>(SelectedClient.Phones);
                try
                {
                    Hours = new ObservableCollection<Hours>(Tools.Locator.HoursECL.GetForClient(SelectedClient.Id));
                }
                catch (Exception ex)
                {
                    PopupManager.Popup("Failed to retrieve Hours for selected Client", Constants.DBE, ex.Innermost(),
                        PopupButtons.Ok, PopupImage.Warning);
                }
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
        }
    }
}
