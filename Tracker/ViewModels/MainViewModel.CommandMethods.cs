using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Tracker.ECL.DTO;
using Tracker.Infrastructure;
using Tracker.Views;
using TrackerCommon;
using TrackerLib.Interfaces;

namespace Tracker.ViewModels
{
    public partial class MainViewModel
    {

        #region Toolbar related
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
            SelectedClient = null;
            LoadClients();
        }

        private void ManagePhoneTypesClick()
        {
            var vm = Tools.Locator.PhoneTypeViewModel;
            DialogSupport.ShowDialog<PhoneTypeWindow>(vm, Application.Current.MainWindow);
            Tools.Locator.StatusbarViewModel.Update();
            LoadPhoneTypeMenuItems();
            SelectedClient = null;
            LoadClients();
        }

        private void TogglePasswordClick()
        {
            ISettingsService settings = Tools.Locator.Settings;
            if (IsPasswordProtected)
            {
                if (PopupManager.Popup("Disable Password Protection?", "Disable Password", PopupButtons.YesNo, PopupImage.Question)
                    != PopupResult.Yes)
                {
                    return;
                }
                settings.PasswordHash = null;
                settings.PasswordSalt = null;
                IsPasswordProtected = false;
                Tools.Locator.StatusbarViewModel.IsPasswordProtected = false;
            }
            else
            {
                var vm = Tools.Locator.PasswordViewModel;
                vm.Password2Visibility = Visibility.Visible;
                if (DialogSupport.ShowDialog<PasswordWindow>(vm, Application.Current.MainWindow) != true)
                {
                    return;
                }
                var salt = Salter.CreateSalt(Constants.SaltLength);
                var hash = Tools.GenerateHash(Encoding.UTF8.GetBytes(vm.Password1), salt, Constants.HashIterations, Constants.HashLength);
                settings.PasswordHash = hash.ArrayCopy();
                settings.PasswordSalt = salt.ArrayCopy();
                IsPasswordProtected = true;
                Tools.Locator.StatusbarViewModel.IsPasswordProtected = true;
            }
        }

        private void BackupClick()
        {
            var vm = Tools.Locator.BackupViewModel;
            DialogSupport.ShowDialog<BackupWindow>(vm, Application.Current.MainWindow);
        }

        private void PalletteClick()
        {
            var vm = Tools.Locator.PalletteViewModel;
            DialogSupport.ShowDialog<PalletteWindow>(vm, Application.Current.MainWindow);
            Pallette.SetSystemColors();
        }

        private void AboutClick()
        {
            var vm = Tools.Locator.AboutViewModel;
            DialogSupport.ShowDialog<AboutWindow>(vm, Application.Current.MainWindow);
        }

        #endregion

        #region Client

        private void AddClientClick()
        {
            var vm = Tools.Locator.ClientViewModel;
            if (DialogSupport.ShowDialog<ClientWindow>(vm, Application.Current.MainWindow) != true)
            {
                return;
            }
            var ecl = Tools.Locator.ClientECL;
            var c = new Client
            {
                Name = vm.Client.Name.Caseify(),
                Address = vm.Client.Address ?? string.Empty,
                City = vm.Client.City ?? string.Empty,
                State = vm.Client.State ?? string.Empty,
                PostalCode = vm.Client.PostalCode ?? string.Empty,
                PrimaryContact = vm.Client.PrimaryContact ?? string.Empty,
                Comments = vm.Client.Comments ?? string.Empty,
                ClientTypeId = vm.Client.ClientTypeId,
                ClientType = vm.Client.ClientType
            };
            try
            {
                ecl.Insert(c);
            }
            catch (Exception ex)
            {
                PopupManager.Popup("Failed to add new Client", Constants.DBE, ex.Innermost(), PopupButtons.Ok, PopupImage.Error);
                return;
            }
            int ix = 0;
            while (ix < Clients.Count && Clients[ix] < c)
            {
                ix++;
            }
            Clients.Insert(ix, c);
            SelectedClient = c;
            SelectedClient = null;
        }

        private bool ClientSelected() => SelectedClient != null;

        private void DeselectClientClick() => SelectedClient = null;

        private void EditClientClick()
        {
            var ecl = Tools.Locator.ClientECL;
            if (SelectedClient is null)
            {
                return;
            }
            var vm = Tools.Locator.ClientViewModel;
            vm.Client = SelectedClient.Clone();
            string save = SelectedClient.Name;
            if (DialogSupport.ShowDialog<ClientWindow>(vm, Application.Current.MainWindow) != true)
            {
                SelectedClient = null;
                return;
            }
            Client c = vm.Client.Clone();
            c.Name = c.Name.Caseify();
            if (!save.Equals(c.Name, StringComparison.OrdinalIgnoreCase) && ecl.Read(c.Name) != null)
            {
                PopupManager.Popup($"A Client with the name '{c.Name}' already exists", "Duplicate Client", PopupButtons.Ok,
                    PopupImage.Stop);
                SelectedClient = null;
                return;
            }
            try
            {
                ecl.Update(c);
            }
            catch (Exception ex)
            {
                PopupManager.Popup("Failed to update Client", Constants.DBE, ex.Innermost(), PopupButtons.Ok, PopupImage.Error);
                SelectedClient = null;
                return;
            }
            Clients.Remove(SelectedClient);
            SelectedClient = null;
            int ix = 0;
            while (ix < Clients.Count && Clients[ix] < c)
            {
                ix++;
            }
            Clients.Insert(ix, c);
            SelectedClient = c;
            SelectedClient = null;
            Tools.Locator.StatusbarViewModel.Update();
        }

        private bool DeleteClientCanClick()
        {
            if (SelectedClient is null)
            {
                return false;
            }
            int cid = SelectedClient.Id;
            if (Tools.Locator.HoursECL.ClientHasHours(cid))
            {
                return false;
            }
            if (Tools.Locator.MileageECL.ClientHasMileage(cid))
            {
                return false;
            }
            if (Tools.Locator.NoteECL.ClientHasNotes(cid))
            {
                return false;
            }
            if (Tools.Locator.PhoneECL.ClientHasPhones(cid))
            {
                return false;
            }
            return true;
        }

        private void DeleteClientClick()
        {
            if (SelectedClient is null || !DeleteClientCanClick())
            {
                return;
            }
            string msg = $"Delete client '{SelectedClient.Name}'? Action cannot be undone.";
            if (PopupManager.Popup(msg,"Delete Client?", PopupButtons.YesNo,PopupImage.Question) != PopupResult.Yes)
            {
                SelectedClient = null;
                return;
            }
            try
            {
                Tools.Locator.ClientECL.Delete(SelectedClient);
            }
            catch (Exception ex)
            {
                PopupManager.Popup("Failed to delete Client", Constants.DBE, ex.Innermost(), PopupButtons.Ok, PopupImage.Error);
                SelectedClient = null;
                return;
            }
            Clients.Remove(SelectedClient);
            SelectedClient = null;
            Tools.Locator.StatusbarViewModel.Update();
        }

        private void ChangeClientType(object parm)
        {
            if (SelectedClient is null || !(parm is ClientType ct) || ct.Id == SelectedClient.ClientTypeId)
            {
                return;
            }
            int saveid = SelectedClient.ClientTypeId;
            ClientType savect = SelectedClient.ClientType;
            SelectedClient.ClientTypeId = ct.Id;
            SelectedClient.ClientType = ct;
            try
            {
                Tools.Locator.ClientECL.Update(SelectedClient);
            }
            catch (Exception ex)
            {
                SelectedClient.ClientTypeId = saveid;
                SelectedClient.ClientType = savect;
                PopupManager.Popup("Failed to update Client", Constants.DBE, ex.Innermost(), PopupButtons.Ok, PopupImage.Error);
            }
            SelectedClient = null;
        }

        #endregion

        #region Notes

        private void AddNoteClick()
        {
            if (SelectedClient is null)
            {
                return;
            }
            var vm = Tools.Locator.NoteViewModel;
            vm.Client = SelectedClient;
            if (DialogSupport.ShowDialog<NoteWindow>(vm, Application.Current.MainWindow) != true)
            {
                return;
            }
            if (!vm.Date.HasValue || string.IsNullOrEmpty(vm.Text))
            {
                return;
            }
            Note n = new Note
            {
                ClientId = SelectedClient.Id,
                Date = vm.Date.Value,
                Text = vm.Text.TrimEnd(Constants.CRLF)
            };
            try
            {
                Tools.Locator.NoteECL.Insert(n);
            }
            catch (Exception ex)
            {
                PopupManager.Popup("Failed to add new Note", Constants.DBE, ex.Innermost(), PopupButtons.Ok, PopupImage.Error);
                return;
            }
            int ix = 0;
            while (ix < Notes.Count && Notes[ix] > n)
            {
                ix++;
            }
            Notes.Insert(ix, n);
            SelectedNote = n;
            SelectedNote = null;
        }

        private bool NoteSelected() => SelectedNote != null;

        private void DeselectNoteClick() => SelectedNote = null;

        private void EditNoteDateClick()
        {
            if (SelectedNote is null)
            {
                return;
            }
            var vm = Tools.Locator.DatePickerViewModel;
            vm.Question = "Note Date:";
            vm.Answer = SelectedNote.Date;
            vm.AnswerRequired = true;
            vm.BorderBrush = (SolidColorBrush)Application.Current.Resources[Constants.Border];
            if (DialogSupport.ShowDialog<DatePickerWindow>(vm, Application.Current.MainWindow) != true)
            {
                SelectedNote = null;
                return;
            }
            if (!vm.Answer.HasValue || vm.Answer.Value == default)
            {
                SelectedNote = null;
                return;
            }
            var n = SelectedNote.Clone();
            n.Date = vm.Answer.Value;
            try
            {
                Tools.Locator.NoteECL.Update(n);
            }
            catch (Exception ex)
            {
                PopupManager.Popup("Failed to update Note", Constants.DBE, ex.Innermost(), PopupButtons.Ok, PopupImage.Error);
                SelectedNote = null;
                return;
            }
            Notes.Remove(SelectedNote);
            SelectedNote = null;
            int ix = 0;
            ix = 0;
            while (ix < Notes.Count && Notes[ix] > n)
            {
                ix++;
            }
            Notes.Insert(ix, n);
            SelectedNote = n;
            SelectedNote = null;
        }

        private void EditNoteTextClick()
        {
            if (SelectedNote is null)
            {
                return;
            }
            var vm = Tools.Locator.QAViewModel;
            vm.Question = "Note Text:";
            vm.Answer = SelectedNote.Text;
            vm.AnswerRequired = true;
            vm.BorderBrush = (SolidColorBrush)Application.Current.Resources[Constants.Border];
            if (DialogSupport.ShowDialog<LongQAWindow>(vm, Application.Current.MainWindow) != true)
            {
                SelectedNote = null;
                return;
            }
            string save = SelectedNote.Text;
            string answer = vm.Answer.TrimEnd(Constants.CRLF);
            SelectedNote.Text = answer;
            try
            {
                Tools.Locator.NoteECL.Update(SelectedNote);
            }
            catch (Exception ex)
            {
                SelectedNote.Text = save;
                PopupManager.Popup("Failed to update Note", Constants.DBE, ex.Innermost(), PopupButtons.Ok, PopupImage.Error);
                SelectedNote = null;
                return;
            }
            SelectedNote = null;
            Tools.Locator.StatusbarViewModel.Update();
        }

        private void DeleteNoteClick()
        {
            if (SelectedNote is null)
            {
                return;
            }
            try
            {
                Tools.Locator.NoteECL.Delete(SelectedNote);
            }
            catch (Exception ex)
            {
                PopupManager.Popup("Failed to delete Note", Constants.DBE, ex.Innermost(), PopupButtons.Ok, PopupImage.Error);
                SelectedNote = null;
                return;
            }
            int id = SelectedNote.Id;
            Notes.Remove(SelectedNote);
            SelectedNote = null;
            Tools.Locator.StatusbarViewModel.Update();
        }

        #endregion

        #region Hours

        private void AddHoursClick()
        {
            if (SelectedClient is null)
            {
                return;
            }
            var vm = Tools.Locator.HoursViewModel;
            vm.Client = SelectedClient;
            if (DialogSupport.ShowDialog<HoursWindow>(vm, Application.Current.MainWindow) != true)
            {
                return;
            }
        }

        private bool HoursSelected() => SelectedHours != null;

        private void DeselectHoursClick() => SelectedHours = null;

        private void EditHoursClick()
        {

        }

        private void DeleteHoursClick()
        {

        }

        #endregion

        #region Mileage

        private void AddMileageClick()
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

        #endregion

        #region Phone Numbers

        private void AddPhoneClick()
        {
            if (SelectedClient is null)
            {
                return;
            }
            var vm = Tools.Locator.PhoneViewModel;
            vm.Client = SelectedClient;
            if (DialogSupport.ShowDialog<PhoneWindow>(vm, Application.Current.MainWindow) != true)
            {
                return;
            }
            Phone p = new Phone
            {
                ClientId = SelectedClient.Id,
                PhoneTypeId = vm.SelectedType.Id,
                PhoneType = vm.SelectedType,
                Number = vm.Number
            };
            try
            {
                Tools.Locator.PhoneECL.Insert(p);
            }
            catch (Exception ex)
            {
                PopupManager.Popup("Failed to add new Phone", Constants.DBE, ex.Innermost(), PopupButtons.Ok, PopupImage.Error);
                return;
            }
            int ix = 0;
            while (ix < Phones.Count && Phones[ix] < p)
            {
                ix++;
            }
            Phones.Insert(ix, p);
            SelectedPhone = p;
            SelectedPhone = null;
            Tools.Locator.StatusbarViewModel.Update();
        }

        private bool PhoneSelected() => SelectedPhone != null;

        private void ChangePhoneType(object parm)
        {
            if (SelectedPhone is null || !(parm is PhoneType pt) || pt.Id == SelectedPhone.Id)
            {
                return;
            }
            int saveid = SelectedPhone.PhoneTypeId;
            PhoneType savept = SelectedPhone.PhoneType;
            SelectedPhone.PhoneTypeId = pt.Id;
            SelectedPhone.PhoneType = pt;
            try
            {
                Tools.Locator.PhoneECL.Update(SelectedPhone);
            }
            catch (Exception ex)
            {
                SelectedPhone.PhoneTypeId = saveid;
                SelectedPhone.PhoneType = savept;
                PopupManager.Popup("Failed to update Phone", Constants.DBE, ex.Innermost(), PopupButtons.Ok, PopupImage.Error);
                SelectedPhone = null;
                return;
            }
            SelectedPhone = null;
        }

        private void EditPhoneNumberClick()
        {
            if (SelectedPhone is null)
            {
                return;
            }
            var ecl = Tools.Locator.PhoneECL;
            var vm = Tools.Locator.QAViewModel;
            vm.Question = "Number:";
            vm.Answer = SelectedPhone.Number;
            vm.AnswerRequired = true;
            vm.BorderBrush = (SolidColorBrush)Application.Current.Resources[Constants.Border];
            if (DialogSupport.ShowDialog<QAWindow>(vm, Application.Current.MainWindow) != true)
            {
                SelectedPhone = null;
                return;
            }
            Phone p = SelectedPhone.Clone();
            p.Number = vm.Answer;
            try
            {
                ecl.Update(p);
            }
            catch (Exception ex)
            {
                PopupManager.Popup("Failed to update Phone number", Constants.DBE, ex.Innermost(), PopupButtons.Ok, PopupImage.Error);
                SelectedPhone = null;
                return;
            }
            Phones.Remove(SelectedPhone);
            SelectedPhone = null;
            int ix = 0;
            while (ix < Phones.Count && Phones[ix] < p)
            {
                ix++;
            }
            Phones.Insert(ix, p);
            SelectedPhone = p;
            SelectedPhone = null;
        }

        private void DeletePhoneClick()
        {
            if (SelectedPhone is null)
            {
                return;
            }
            try
            {
                Tools.Locator.PhoneECL.Delete(SelectedPhone);
            }
            catch (Exception ex)
            {
                PopupManager.Popup("Failed to delete Phone", Constants.DBE, ex.Innermost(), PopupButtons.Ok, PopupImage.Error);
                SelectedPhone = null;
                return;
            }
            Phones.Remove(SelectedPhone);
            SelectedPhone = null;
        }

        #endregion

        #region Window Loaded

        private void WindowLoaded()
        {
            var settings = Tools.Locator.Settings;
            IsPasswordProtected = settings.PasswordHash != null;
            if (IsPasswordProtected)
            {
                Authenticate(settings);
            }
            WindowTitle = $"{settings.ProductName} Version {settings.ProductVersion:0.00}";
            Banner = $"{settings.ProductName} {settings.ProductVersion:0.00} - Track your Hours and Mileage";
            Tools.Locator.StatusbarViewModel.Update();
            LoadClients();
            LoadPhoneTypeMenuItems();
            LoadClientTypeMenuItems();
        }

        #endregion
    }
}
