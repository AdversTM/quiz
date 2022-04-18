using System.Windows;
using System.Windows.Controls;
using common.model;

namespace generator {
    public partial class AnswerWindow {
        private readonly Quiz _quiz;

        public AnswerWindow(Quiz quiz) {
            _quiz = quiz;
            InitializeComponent();
            
            
        }

        private void ButtonAdd_OnClick(object sender, RoutedEventArgs e) {
        }

        private void ButtonModify_OnClick(object sender, RoutedEventArgs e) {
        }

        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e) {
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
        }
    }
}