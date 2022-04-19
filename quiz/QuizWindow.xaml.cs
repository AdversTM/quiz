using System.Windows;
using System.Windows.Controls;
using quiz.viewmodel;

namespace quiz {
    public partial class QuizWindow {
        private readonly QuizViewModel _model;

        public QuizWindow(QuizViewModel quizViewModel) {
            _model = quizViewModel;
            DataContext = _model;
            InitializeComponent();
            _model.OnPropertyChanged();
        }

        private void ButtonAnswer_OnClick(object sender, RoutedEventArgs e) {
            var i = int.Parse((string)((Button)sender).Tag);
            _model.ToggleAnswer(i);
        }

        private void ButtonPrev_OnClick(object sender, RoutedEventArgs e) {
            _model.PrevQuestion();
        }

        private void ButtonNext_OnClick(object sender, RoutedEventArgs e) {
            _model.NextQuestion();
        }

        private void ButtonEnd_OnClick(object sender, RoutedEventArgs e) {
            if (_model.IsRunning)
                _model.EndQuiz();
            else Application.Current.Shutdown();
        }
    }
}