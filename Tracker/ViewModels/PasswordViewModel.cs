using System.Text;
using System.Windows;

using Tracker.Infrastructure;

using TrackerCommon;

using TrackerLib.Interfaces;

namespace Tracker.ViewModels
{
    public class PasswordViewModel : ViewModelBase
    {
        #region Properties

        private bool _password2Required;

        public byte[] Salt;
        public byte[] Hash;

        private string _password1;
        public string Password1
        {
            get => _password1;
            set
            {
                SetProperty(ref _password1, value);
                PasswordStrength = PasswordChecker.GetPasswordStrength(Password1);
            }
        }

        private PasswordStrength _passwordStrength;
        public PasswordStrength PasswordStrength
        {
            get => _passwordStrength;
            set => SetProperty(ref _passwordStrength, value);
        }

        private string _password2;
        public string Password2
        {
            get => _password2;
            set => SetProperty(ref _password2, value);
        }

        private Visibility _password2Visibility;
        public Visibility Password2Visibility
        {
            get => _password2Visibility;
            set
            {
                SetProperty(ref _password2Visibility, value);
                _password2Required = Password2Visibility == Visibility.Visible;
            }
        }

        private string _shortTitle;
        public string ShortTitle
        {
            get => _shortTitle;
            set => SetProperty(ref _shortTitle, value);
        }

        #endregion

        #region Command Methods

        public override bool OkCanExecute()
        {
            if ((Salt is null || Hash is null) && !_password2Required)
            {
                return false;
            }
            if (string.IsNullOrEmpty(Password1))
            {
                return false;
            }
            if (_password2Required && string.IsNullOrEmpty(Password2))
            {
                return false;
            }
            if (_password2Required && Password1 != Password2)
            {
                return false;
            }
            if (!_password2Required)
            {
                var hash = Tools.GenerateHash(Encoding.UTF8.GetBytes(Password1), Salt, Constants.HashIterations, Constants.HashLength);
                if (!hash.ArrayEquals(Hash))
                {
                    return false;
                }
            }
            return true;
        }

        public override void OK()
        {
            Tools.Locator.PasswordManager.Set(Password1);
            base.OK();
        }

        #endregion

        public PasswordViewModel(ISettingsService settings)
        {
            ShortTitle = $"{settings.ProductName} {settings.ProductVersion:0.00}";
        }
    }
}
