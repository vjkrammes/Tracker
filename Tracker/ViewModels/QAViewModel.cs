using System.Windows.Media;

using Tracker.Infrastructure;

namespace Tracker.ViewModels
{
    public class QAViewModel : ViewModelBase
    {
        #region Properties

        private string _question;
        public string Question
        {
            get => _question;
            set => SetProperty(ref _question, value);
        }

        private string _answer;
        public string Answer
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

        public override bool OkCanExecute() => AnswerRequired ? !string.IsNullOrEmpty(Answer) : true;

        #endregion

        public QAViewModel()
        {
            BorderBrush = Brushes.Black;
        }
    }
}
