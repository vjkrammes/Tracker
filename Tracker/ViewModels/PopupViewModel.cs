﻿using System;
using System.Windows;
using System.Windows.Input;

using Tracker.Infrastructure;

using TrackerCommon;

namespace Tracker.ViewModels
{
    public class PopupViewModel : ViewModelBase
    {

        #region Properties

        private PopupResult _result;
        public PopupResult Result
        {
            get { return _result; }
            set { SetProperty(ref _result, value); }
        }

        private Uri _windowIcon;
        public Uri WindowIcon
        {
            get { return _windowIcon; }
            set { SetProperty(ref _windowIcon, value); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _majorText;
        public string MajorText
        {
            get { return _majorText; }
            set { SetProperty(ref _majorText, value); }
        }

        private string _minorText;
        public string MinorText
        {
            get { return _minorText; }
            set { SetProperty(ref _minorText, value); }
        }

        private Uri _icon;
        public Uri Icon
        {
            get { return _icon; }
            set { SetProperty(ref _icon, value); }
        }

        private double _maxWidth;
        public double MaxWidth
        {
            get => _maxWidth;
            set => SetProperty(ref _maxWidth, value);
        }

        private GridLength _yesWidth;
        public GridLength YesWidth
        {
            get { return _yesWidth; }
            set { SetProperty(ref _yesWidth, value); }
        }

        private Uri _yesIcon;
        public Uri YesIcon
        {
            get { return _yesIcon; }
            set { SetProperty(ref _yesIcon, value); }
        }

        private string _yesText;
        public string YesText
        {
            get { return _yesText; }
            set { SetProperty(ref _yesText, value); }
        }

        private bool _yesDefault;
        public bool YesDefault
        {
            get { return _yesDefault; }
            set { SetProperty(ref _yesDefault, value); }
        }

        private GridLength _noWidth;
        public GridLength NoWidth
        {
            get { return _noWidth; }
            set { SetProperty(ref _noWidth, value); }
        }

        private Uri _noIcon;
        public Uri NoIcon
        {
            get { return _noIcon; }
            set { SetProperty(ref _noIcon, value); }
        }

        private string _noText;
        public string NoText
        {
            get { return _noText; }
            set { SetProperty(ref _noText, value); }
        }

        private bool _noCancel;
        public bool NoCancel
        {
            get { return _noCancel; }
            set { SetProperty(ref _noCancel, value); }
        }

        private GridLength _okWidth;
        public GridLength OkWidth
        {
            get { return _okWidth; }
            set { SetProperty(ref _okWidth, value); }
        }

        private Uri _okIcon;
        public Uri OkIcon
        {
            get { return _okIcon; }
            set { SetProperty(ref _okIcon, value); }
        }

        private string _okText;
        public string OkText
        {
            get { return _okText; }
            set { SetProperty(ref _okText, value); }
        }

        private bool _okDefault;
        public bool OkDefault
        {
            get { return _okDefault; }
            set { SetProperty(ref _okDefault, value); }
        }

        private bool _okCancel;
        public bool OkCancel
        {
            get { return _okCancel; }
            set { SetProperty(ref _okCancel, value); }
        }

        private GridLength _cancelWidth;
        public GridLength Cancelwidth
        {
            get { return _cancelWidth; }
            set { SetProperty(ref _cancelWidth, value); }
        }

        private Uri _cancelIcon;
        public Uri CancelIcon
        {
            get { return _cancelIcon; }
            set { SetProperty(ref _cancelIcon, value); }
        }

        private string _cancelText;
        public string CancelText
        {
            get { return _cancelText; }
            set { SetProperty(ref _cancelText, value); }
        }

        private bool _cancelCancel;
        public bool CancelCancel
        {
            get { return _cancelCancel; }
            set { SetProperty(ref _cancelCancel, value); }
        }

        private Visibility _iconVisibility;
        public Visibility IconVisibility
        {
            get { return _iconVisibility; }
            set { SetProperty(ref _iconVisibility, value); }
        }

        private Visibility _minorVisibility;
        public Visibility MinorVisibility
        {
            get { return _minorVisibility; }
            set { SetProperty(ref _minorVisibility, value); }
        }

        private double _buttonWidth;
        public double ButtonWidth
        {
            get { return _buttonWidth; }
            set { SetProperty(ref _buttonWidth, value); }
        }

        private double _majorFontSize;
        public double MajorFontSize
        {
            get { return _majorFontSize; }
            set { SetProperty(ref _majorFontSize, value); }
        }

        private double _minorFontSize;
        public double MinorFontSize
        {
            get { return _minorFontSize; }
            set { SetProperty(ref _minorFontSize, value); }
        }

        #endregion

        #region Commands

        private RelayCommand _yesCommand;
        public ICommand YesCommand
        {
            get
            {
                if (_yesCommand == null)
                {
                    _yesCommand = new RelayCommand(param => YesClick(), param => true);
                }
                return _yesCommand;
            }
        }

        private RelayCommand _noCommand;
        public ICommand NoCommand
        {
            get
            {
                if (_noCommand == null)
                {
                    _noCommand = new RelayCommand(param => NoClick(), param => true);
                }
                return _noCommand;
            }
        }

        #endregion

        #region Command Methods

        private void YesClick()
        {
            Result = PopupResult.Yes;
            DialogResult = true;
        }

        private void NoClick()
        {
            Result = PopupResult.No;
            DialogResult = false;
        }

        public void OKClick()
        {
            Result = PopupResult.Ok;
            DialogResult = true;
        }

        public void CancelClick()
        {
            Result = PopupResult.Cancel;
            DialogResult = false;
        }

        #endregion

        #region Constructors

        public PopupViewModel()
        {

        }

        public void SetParameters(string title, string major, string minor, Uri icon, PopupButtons buttons,
            string[] buttonTexts, Uri[] buttonImages, double buttonWidth = 65.0, Uri windowUri = null,
            double majorFontSize = 18.0, double minorFontSize = 12.0, double maxwidth = 600)
        {
            WindowIcon = windowUri;
            ButtonWidth = buttonWidth;
            MajorFontSize = majorFontSize;
            MinorFontSize = minorFontSize;
            Title = title;
            MajorText = major;
            MinorText = minor;
            MaxWidth = maxwidth;
            if (string.IsNullOrEmpty(MinorText))
                MinorVisibility = Visibility.Collapsed;
            else
                MinorVisibility = Visibility.Visible;
            Icon = icon;
            if (Icon == null)
            {
                IconVisibility = Visibility.Collapsed;
            }
            else
            {
                IconVisibility = Visibility.Visible;
            }
            if (buttonTexts.Length != 4)
            {
                throw new InvalidOperationException("Button Texts need to be an array of 4");
            }
            if (buttonImages.Length != 4)
            {
                throw new InvalidOperationException("Button Images need to be an array of 4");
            }
            YesText = buttonTexts[0];
            YesIcon = buttonImages[0];
            NoText = buttonTexts[1];
            NoIcon = buttonImages[1];
            OkText = buttonTexts[2];
            OkIcon = buttonImages[2];
            CancelText = buttonTexts[3];
            CancelIcon = buttonImages[3];
            YesWidth = new GridLength(0);
            NoWidth = new GridLength(0);
            OkWidth = new GridLength(0);
            Cancelwidth = new GridLength(0);
            YesDefault = false;
            NoCancel = false;
            OkDefault = false;
            OkCancel = buttons == PopupButtons.Ok;
            CancelCancel = false;
            if ((buttons & PopupButtons.Yes) == PopupButtons.Yes)
            {
                YesWidth = new GridLength(1, GridUnitType.Star);
                YesDefault = true;
            }
            if ((buttons & PopupButtons.No) == PopupButtons.No)
            {
                NoWidth = new GridLength(1, GridUnitType.Star);
                NoCancel = true;
            }
            if ((buttons & PopupButtons.Ok) == PopupButtons.Ok)
            {
                OkWidth = new GridLength(1, GridUnitType.Star);
                YesDefault = false;
                OkDefault = true;
            }
            if ((buttons & PopupButtons.Cancel) == PopupButtons.Cancel)
            {
                Cancelwidth = new GridLength(1, GridUnitType.Star);
                NoCancel = false;
                CancelCancel = true;
            }
        }

        #endregion
    }
}
