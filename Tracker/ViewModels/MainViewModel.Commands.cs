using System.Windows.Input;

using Tracker.Infrastructure;

namespace Tracker.ViewModels
{
    public partial class MainViewModel
    {
        private RelayCommand _toggleStatusbarCommand;
        public ICommand ToggleStatusbarCommand
        {
            get
            {
                if (_toggleStatusbarCommand is null)
                {
                    _toggleStatusbarCommand = new RelayCommand(parm => ToggleStatusbar(), parm => AlwaysCanExecute());
                }
                return _toggleStatusbarCommand;
            }
        }

        private RelayCommand _iconHeightCommand;
        public ICommand IconHeightCommand
        {
            get
            {
                if (_iconHeightCommand is null)
                {
                    _iconHeightCommand = new RelayCommand(parm => IconHeight(parm), parm => AlwaysCanExecute());
                }
                return _iconHeightCommand;
            }
        }

        private RelayCommand _manageClientTypesCommand;
        public ICommand ManageClientTypesCommand
        {
            get
            {
                if (_manageClientTypesCommand is null)
                {
                    _manageClientTypesCommand = new RelayCommand(parm => ManageClientTypesClick(), parm => AlwaysCanExecute());
                }
                return _manageClientTypesCommand;
            }
        }

        private RelayCommand _managePhoneTypesCommand;
        public ICommand ManagePhoneTypesCommand
        {
            get
            {
                if (_managePhoneTypesCommand is null)
                {
                    _managePhoneTypesCommand = new RelayCommand(parm => ManagePhoneTypesClick(), parm => AlwaysCanExecute());
                }
                return _managePhoneTypesCommand;
            }
        }

        private RelayCommand _togglePasswordCommand;
        public ICommand TogglePasswordCommand
        {
            get
            {
                if (_togglePasswordCommand is null)
                {
                    _togglePasswordCommand = new RelayCommand(parm => TogglePasswordClick(), parm => AlwaysCanExecute());
                }
                return _togglePasswordCommand;
            }
        }

        private RelayCommand _backupCommand;
        public ICommand BackupCommand
        {
            get
            {
                if (_backupCommand is null)
                {
                    _backupCommand = new RelayCommand(parm => BackupClick(), parm => AlwaysCanExecute());
                }
                return _backupCommand;
            }
        }

        private RelayCommand _palletteCommand;
        public ICommand PalletteCommand
        {
            get
            {
                if (_palletteCommand is null)
                {
                    _palletteCommand = new RelayCommand(parm => PalletteClick(), parm => AlwaysCanExecute());
                }
                return _palletteCommand;
            }
        }

        private RelayCommand _aboutCommand;
        public ICommand AboutCommand
        {
            get
            {
                if (_aboutCommand is null)
                {
                    _aboutCommand = new RelayCommand(parm => AboutClick(), parm => AlwaysCanExecute());
                }
                return _aboutCommand;
            }
        }

        private RelayCommand _addClientCommand;
        public ICommand AddClientCommand
        {
            get
            {
                if (_addClientCommand is null)
                {
                    _addClientCommand = new RelayCommand(parm => AddClientClick(), parm => AlwaysCanExecute());
                }
                return _addClientCommand;
            }
        }

        private RelayCommand _deselectClientCommand;
        public ICommand DeselectClientCommand
        {
            get
            {
                if (_deselectClientCommand is null)
                {
                    _deselectClientCommand = new RelayCommand(parm => DeselectClientClick(), parm => ClientSelected());
                }
                return _deselectClientCommand;
            }
        }

        private RelayCommand _editClientCommand;
        public ICommand EditClientCommand
        {
            get
            {
                if (_editClientCommand is null)
                {
                    _editClientCommand = new RelayCommand(parm => EditClientClick(), parm => ClientSelected());
                }
                return _editClientCommand;
            }
        }

        private RelayCommand _deleteClientCommand;
        public ICommand DeleteClientCommand
        {
            get
            {
                if (_deleteClientCommand is null)
                {
                    _deleteClientCommand = new RelayCommand(parm => DeleteClientClick(), parm => DeleteClientCanClick());
                }
                return _deleteClientCommand;
            }
        }

        private RelayCommand _changeClientTypeCommand;
        public ICommand ChangeClientTypeCommand
        {
            get
            {
                if (_changeClientTypeCommand is null)
                {
                    _changeClientTypeCommand = new RelayCommand(parm => ChangeClientType(parm), parm => ClientSelected());
                }
                return _changeClientTypeCommand;
            }
        }

        private RelayCommand _addNoteCommand;
        public ICommand AddNoteCommand
        {
            get
            {
                if (_addNoteCommand is null)
                {
                    _addNoteCommand = new RelayCommand(parm => AddNoteClick(), parm => ClientSelected());
                }
                return _addNoteCommand;
            }
        }

        private RelayCommand _deselectNoteCommand;
        public ICommand DeselectNoteCommand
        {
            get
            {
                if (_deselectNoteCommand is null)
                {
                    _deselectNoteCommand = new RelayCommand(parm => DeselectNoteClick(), parm => NoteSelected());
                }
                return _deselectNoteCommand;
            }
        }

        private RelayCommand _editNoteDateCommand;
        public ICommand EditNoteDateCommand
        {
            get
            {
                if (_editNoteDateCommand is null)
                {
                    _editNoteDateCommand = new RelayCommand(parm => EditNoteDateClick(), parm => NoteSelected());
                }
                return _editNoteDateCommand;
            }
        }

        private RelayCommand _editNoteTextCommand;
        public ICommand EditNoteTextCommand
        {
            get
            {
                if (_editNoteTextCommand is null)
                {
                    _editNoteTextCommand = new RelayCommand(parm => EditNoteTextClick(), parm => NoteSelected());
                }
                return _editNoteTextCommand;
            }
        }

        private RelayCommand _deleteNoteCommand;
        public ICommand DeleteNoteCommand
        {
            get
            {
                if (_deleteNoteCommand is null)
                {
                    _deleteNoteCommand = new RelayCommand(parm => DeleteNoteClick(), parm => NoteSelected());
                }
                return _deleteNoteCommand;
            }
        }

        private RelayCommand _addMileageCommand;
        public ICommand AddMileageCommand
        {
            get
            {
                if (_addMileageCommand is null)
                {
                    _addMileageCommand = new RelayCommand(parm => AddMileageClick(), parm => ClientSelected());
                }
                return _addMileageCommand;
            }
        }

        private RelayCommand _deselectMileageCommand;
        public ICommand DeselectMileageCommand
        {
            get
            {
                if (_deselectMileageCommand is null)
                {
                    _deselectMileageCommand = new RelayCommand(parm => DeselectMileageClick(), parm => MileageSelected());
                }
                return _deselectMileageCommand;
            }
        }

        private RelayCommand _editMileageCommand;
        public ICommand EditMileageCommand
        {
            get
            {
                if (_editMileageCommand is null)
                {
                    _editMileageCommand = new RelayCommand(parm => EditMileageClick(), parm => MileageSelected());
                }
                return _editMileageCommand;
            }
        }

        private RelayCommand _deleteMileageCommand;
        public ICommand DeleteMileageCommand
        {
            get
            {
                if (_deleteMileageCommand is null)
                {
                    _deleteMileageCommand = new RelayCommand(parm => DeleteMileageClick(), parm => MileageSelected());
                }
                return _deleteMileageCommand;
            }
        }

        private RelayCommand _addHoursCommand;
        public ICommand AddHoursCommand
        {
            get
            {
                if (_addHoursCommand is null)
                {
                    _addHoursCommand = new RelayCommand(parm => AddHoursClick(), parm => ClientSelected());
                }
                return _addHoursCommand;
            }
        }

        private RelayCommand _deselectHoursCommand;
        public ICommand DeselectHoursCommand
        {
            get
            {
                if (_deselectHoursCommand is null)
                {
                    _deselectHoursCommand = new RelayCommand(parm => DeselectHoursClick(), parm => HoursSelected());
                }
                return _deselectHoursCommand;
            }
        }

        private RelayCommand _editHoursCommand;
        public ICommand EditHoursCommand
        {
            get
            {
                if (_editHoursCommand is null)
                {
                    _editHoursCommand = new RelayCommand(parm => EditHoursClick(), parm => HoursSelected());
                }
                return _editHoursCommand;
            }
        }

        private RelayCommand _deleteHoursCommand;
        public ICommand DeleteHoursCommand
        {
            get
            {
                if (_deleteHoursCommand is null)
                {
                    _deleteHoursCommand = new RelayCommand(parm => DeleteHoursClick(), parm => HoursSelected());
                }
                return _deleteHoursCommand;
            }
        }

        private RelayCommand _addPhoneCommand;
        public ICommand AddPhoneCommand
        {
            get
            {
                if (_addPhoneCommand is null)
                {
                    _addPhoneCommand = new RelayCommand(parm => AddPhoneClick(), parm => ClientSelected());
                }
                return _addPhoneCommand;
            }
        }

        private RelayCommand _changePhoneTypeCommand;
        public ICommand ChangePhoneTypeCommand
        {
            get
            {
                if (_changePhoneTypeCommand is null)
                {
                    _changePhoneTypeCommand = new RelayCommand(parm => ChangePhoneType(parm), parm => PhoneSelected());
                }
                return _changePhoneTypeCommand;
            }
        }

        private RelayCommand _editPhoneNumberCommand;
        public ICommand EditPhoneNumberCommand
        {
            get
            {
                if (_editPhoneNumberCommand is null)
                {
                    _editPhoneNumberCommand = new RelayCommand(parm => EditPhoneNumberClick(), parm => PhoneSelected());
                }
                return _editPhoneNumberCommand;
            }
        }

        private RelayCommand _deletePhoneCommand;
        public ICommand DeletePhoneCommand
        {
            get
            {
                if (_deletePhoneCommand is null)
                {
                    _deletePhoneCommand = new RelayCommand(parm => DeletePhoneClick(), parm => PhoneSelected());
                }
                return _deletePhoneCommand;
            }
        }

        private RelayCommand _windowLoadedCommand;
        public ICommand WindowLoadedCommand
        {
            get
            {
                if (_windowLoadedCommand is null)
                {
                    _windowLoadedCommand = new RelayCommand(parm => WindowLoaded(), parm => AlwaysCanExecute());
                }
                return _windowLoadedCommand;
            }
        }
    }
}
