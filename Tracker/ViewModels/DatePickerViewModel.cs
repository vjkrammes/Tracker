using System;
using System.Windows.Media;

using Tracker.Infrastructure;

namespace Tracker.ViewModels
{
    public class DatePickerViewModel : ViewModelBase
    {
        #region Properties

        private string _question;
        public string Question
        {
            get => _question;
            set => SetProperty(ref _question, value);
        }

        private DateTime? _answer;
        public DateTime? Answer
        {
            get => _answer;
            set => SetProperty(ref _answer, value);
        }

        private bool _answerRequired;
        public bool AnswerRequired
        {
            get => _answerRequired;
            set => SetProperty(ref _answerRequired, value);
        }

        private SolidColorBrush _borderBrush;
        public SolidColorBrush BorderBrush
        {
            get => _borderBrush;
            set => SetProperty(ref _borderBrush, value);
        }

        #endregion

        #region Command Methods

        public override bool OkCanExecute() => AnswerRequired ? Answer.HasValue && Answer.Value != default : true;

        #endregion

        public DatePickerViewModel()
        {
            Answer = null;
        }
    }
}
