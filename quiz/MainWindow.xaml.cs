using System.Windows;
using common.model;
using quiz.viewmodel;

namespace quiz {
    public partial class MainWindow {
        private readonly QuizViewModel _model;

        public MainWindow(Quiz quiz) {
            _model = new QuizViewModel();
            DataContext = _model;
            InitializeComponent();
            _model.Quiz = quiz;
        }

        private void ButtonStart_OnClick(object sender, RoutedEventArgs e) {
            var quizWindow = new QuizWindow(_model);
            quizWindow.Show();
            _model.StartTimer();
            Close();
        }
    }
}