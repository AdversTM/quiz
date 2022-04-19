using System.Linq;
using System.Windows;
using common.model;
using common.viewmodel;

namespace generator.viewmodel {
    public class QuizViewModel : ViewModelBase {
        private Quiz _quiz;
        private Question _currentQuestion;

        public Quiz Quiz {
            get => _quiz;
            set {
                _quiz = value;
                CurrentQuestion = value.Questions.Last();
                _quiz.HasChanged = false;
                OnPropertyChanged();
            }
        }

        public Question CurrentQuestion {
            get => _currentQuestion;
            set {
                _currentQuestion = value;
                OnPropertyChanged();
            }
        }

        public void AddQuestion(Question q) {
            Quiz.Questions.Add(q);
            CurrentQuestion = q;
        }

        public Question DeleteQuestion(Question q) {
            if (Quiz.Questions.Count < 2) {
                MessageBox.Show("Quiz musi zawierać conajmniej jedno pytanie.", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }

            Quiz.Questions.Remove(q);
            q = Quiz.Questions.Last();
            CurrentQuestion = q;
            return q;
        }
    }
}