﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Tracker.Infrastructure
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Commands

        private RelayCommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(param => Cancel(), param => AlwaysCanExecute());
                }
                return _cancelCommand;
            }
        }

        private RelayCommand _okCommand;
        public ICommand OkCommand
        {
            get
            {
                if (_okCommand == null)
                {
                    _okCommand = new RelayCommand(param => OK(), param => OkCanExecute());
                }
                return _okCommand;
            }
        }

        #endregion

        #region Command Methods

        public bool AlwaysCanExecute() => true;

        public virtual void Cancel()
        {
            DialogResult = false;
        }

        public virtual bool OkCanExecute() => true;

        public virtual void OK()
        {
            DialogResult = true;
        }

        #endregion

        #region DialogResult stuff

        private bool? dialogResult;
        public bool? DialogResult
        {
            get { return dialogResult; }
            set { SetProperty(ref dialogResult, value); }
        }

        #endregion

        #region PropertyChanged methods

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion
    }
}
