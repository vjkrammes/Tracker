using System.Windows;
using Tracker.ECL.DTO;
using Tracker.Infrastructure;
using Tracker.Views;
using TrackerCommon;

namespace Tracker.ViewModels
{
    public partial class MainViewModel
    {
        public override void Cancel()
        {
            Application.Current.MainWindow.Close();
        }

        private void ToggleStatusbar()
        {
            switch (StatusbarVisibility)
            {
                case Visibility.Visible:
                    StatusbarVisibility = Visibility.Collapsed;
                    Tools.Locator.Settings.ShowStatusBar = false;
                    break;
                default:
                    StatusbarVisibility = Visibility.Visible;
                    Tools.Locator.Settings.ShowStatusBar = true;
                    break;
            }
        }

        private void IconHeight(object parm)
        {
            if (!(parm is string heightstring) || !double.TryParse(heightstring, out double height))
            {
                return;
            }
            Application.Current.Resources[Constants.IconHeight] = height;
            Tools.Locator.Settings.IconHeight = height;
        }

        private void ManageClientTypesClick()
        {
            var vm = Tools.Locator.ClientTypeViewModel;
            DialogSupport.ShowDialog<ClientTypeWindow>(vm, Application.Current.MainWindow);
            Tools.Locator.StatusbarViewModel.Update();
            LoadClientTypeMenuItems();
        }

        private void ManagePhoneTypesClick()
        {
            var vm = Tools.Locator.PhoneTypeViewModel;
            DialogSupport.ShowDialog<PhoneTypeWindow>(vm, Application.Current.MainWindow);
            Tools.Locator.StatusbarViewModel.Update();
            LoadPhoneTypeMenuItems();
        }

        private void BackupClick()
        {

        }

        private void PalletteClick()
        {

        }

        private void AboutClick()
        {

        }

        private void AddClientClick()
        {

        }

        private bool ClientSelected() => SelectedClient != null;

        private void DeselectClientClick() => SelectedClient = null;

        private void EditClientClick()
        {

        }

        private void DeleteClientClick()
        {

        }

        private void ChangeClientType(object parm)
        {
            if (SelectedClient is null || !(parm is ClientType ct) || ct.Id == SelectedClient.Id)
            {
                return;
            }
        }

        private void AddNoteClick()
        {

        }

        private void AddMileageClick()
        {

        }

        private void AddHoursClick()
        {

        }

        private void AddPhoneClick()
        {

        }

        private bool NoteSelected() => SelectedNote != null;

        private void DeselectNoteClick() => SelectedNote = null;

        private void EditNoteClick()
        {

        }

        private void DeleteNoteClick()
        {

        }

        private bool HoursSelected() => SelectedHours != null;

        private void DeselectHoursClick() => SelectedHours = null;

        private void EditHoursClick()
        {

        }

        private void DeleteHoursClick()
        {

        }

        private bool MileageSelected() => SelectedMileage != null;

        private void DeselectMileageClick() => SelectedMileage = null;

        private void EditMileageClick()
        {

        }

        private void DeleteMileageClick()
        {

        }

        private bool PhoneSelected() => SelectedPhone != null;

        private void ChangePhoneType(object parm)
        {
            if (SelectedPhone is null || !(parm is PhoneType pt) || pt.Id == SelectedPhone.Id)
            {
                return;
            }
        }

        private void EditPhoneNumberClick()
        {

        }

        private void DeletePhoneClick()
        {

        }

        private void WindowLoaded()
        {
            var settings = Tools.Locator.Settings;
            WindowTitle = $"{settings.ProductName} Version {settings.ProductVersion:0.00}";
            Banner = $"{settings.ProductName} {settings.ProductVersion:0.00} - Track your Hours and Mileage";
            Tools.Locator.StatusbarViewModel.Update();
            LoadClients();
            LoadPhoneTypeMenuItems();
            LoadClientTypeMenuItems();
        }
    }
}
