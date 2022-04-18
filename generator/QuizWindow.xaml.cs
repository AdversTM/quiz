using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using common.model;
using common.util;
using generator.data;
using generator.viewmodel;

namespace generator {
    public partial class QuizWindow {
        private static App Current => Application.Current as App;
        private readonly QuizViewModel _model;

        public QuizWindow(Quiz quiz) {
            _model = new QuizViewModel();
            DataContext = _model;
            InitializeComponent();

            _model.PropertyChanged += OnQuizChanged;
            _model.Quiz = quiz;
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (e.Source is not Selector sel) return;
            _model.CurrentQuestion = sel.SelectedItem as Question;
        }

        private void ButtonAdd_OnClick(object sender, RoutedEventArgs e) {
            var q = new Question();
            _model.AddQuestion(q);
            Questions.SelectedIndex = _model.Quiz.Questions.Count - 1;
        }

        private void ButtonCopy_OnClick(object sender, RoutedEventArgs e) {
            if (Questions.SelectedItem is not Question q) return;
            q = (Question)q.Clone();
            _model.AddQuestion(q);
            Questions.SelectedItem = q;
        }

        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e) {
            if (Questions.SelectedItem is not Question q) return;
            q = _model.DeleteQuestion(q);
            if (q == null) return;
            Questions.SelectedItem = q;
        }

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e) {
            var file = WpfUtil.SaveFileDialog(App.PDir, _model.Quiz.GetFileName());
            if (file == null || !Current.SaveFile(file, _model.Quiz)) return;
            _model.Quiz = _model.Quiz;
        }

        private void ButtonClose_OnClick(object sender, RoutedEventArgs e) {
            if (!EnsureSaved()) return;
            Current.CloseQuiz();
        }

        private void ButtonImport_OnClick(object sender, RoutedEventArgs e) {
            var file = WpfUtil.OpenFileDialog(App.PDir, "Pliki XML (*.xml)|*.xml");
            if (file == null || !EnsureSaved()) return;
            _model.Quiz = Current.LoadFile(file, new XmlQuizStore());
        }

        private void ButtonExport_OnClick(object sender, RoutedEventArgs e) {
            var file = WpfUtil.SaveFileDialog(App.PDir, _model.Quiz.GetFileName(), "Pliki XML (*.xml)|*.xml");
            if (file == null) return;
            Current.SaveFile(file, _model.Quiz, new XmlQuizStore());
        }

        private bool EnsureSaved() {
            if (!_model.HasChanged) return true;
            var res = MessageBox.Show("Quiz posiada niezapisane zmiany, czy na pewno chcesz kontynuować?",
                "Niezapisane zmiany", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            return res == MessageBoxResult.Yes;
        }

        private void OnQuizChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName != "Quiz") return;
            Questions.ItemsSource = _model.Quiz.Questions;
            Questions.SelectedIndex = _model.Quiz.Questions.Count - 1;
            _model.Quiz.Questions.ItemPropertyChanged += delegate { Questions.Items.Refresh(); };
        }
    }
}