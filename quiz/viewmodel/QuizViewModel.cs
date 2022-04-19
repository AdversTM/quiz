using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using common.model;
using common.util;
using common.viewmodel;

namespace quiz.viewmodel {
    public class QuizViewModel : ViewModelBase {
        private Quiz _quiz;
        private Question _currentQuestion;
        private int _index;
        
        private bool[][] _currentAnswers;
        private bool[] _currentAnswer;
        private SolidColorBrush[] _currentResult;
        
        private SolidColorBrush[][] _results;
        private string _resultText;

        private DispatcherTimer _timer;
        private TimeSpan _time;
        private double _points;

        public Quiz Quiz {
            get => _quiz;
            set {
                _quiz = value;
                _quiz.Questions = new FullyObservableCollection<Question>(_quiz.Questions.Shuffle());

                _currentAnswers = new bool[_quiz.Questions.Count][];
                for (var i = 0; i < _quiz.Questions.Count; i++)
                    _currentAnswers[i] = new bool[4];

                _results = new SolidColorBrush[_quiz.Questions.Count][];
                for (var i = 0; i < _quiz.Questions.Count; i++)
                    _results[i] = Enumerable.Repeat(Brushes.Black, 4).ToArray();

                CurrentQuestion = value.Questions.First();
                Index = 0;
                Points = -1;

                OnPropertyChanged();
            }
        }

        public Question CurrentQuestion {
            get => _currentQuestion;
            private set {
                _currentQuestion = value;
                CurrentAnswer = _currentAnswers[_index];
                CurrentResult = _results[_index];
                OnPropertyChanged();
            }
        }

        public bool[] CurrentAnswer {
            get => _currentAnswer;
            private set {
                _currentAnswer = value;
                OnPropertyChanged();
            }
        }

        public int Index {
            get => _index;
            private set {
                _index = value;
                OnPropertyChanged("PositionText");
                OnPropertyChanged("HasPrev");
                OnPropertyChanged("HasNext");
            }
        }

        public SolidColorBrush[] CurrentResult {
            get => _currentResult;
            private set {
                _currentResult = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan Time {
            get => _time;
            private set {
                _time = value;
                OnPropertyChanged("TimeLeft");
                OnPropertyChanged("Progress");
            }
        }
        
        private double Points {
            get => _points;
            set {
                _points = value;
                OnPropertyChanged("IsRunning");
            }
        }

        public string ResultText {
            get => _resultText;
            set {
                _resultText = value;
                OnPropertyChanged();
            }
        }

        public long TimeLeft => (long)_time.TotalSeconds;
        public double Progress => _time.TotalSeconds / Quiz.Time;
        public string PositionText => $"{Index + 1}/{Quiz.Questions.Count}";
        public bool IsRunning => Points < 0;
        public bool HasPrev => Index > 0;
        public bool HasNext => Index < Quiz.Questions.Count - 1;

        public void PrevQuestion() {
            if (!HasPrev) return;
            CurrentQuestion = Quiz.Questions.ElementAt(--Index);
            SetAnswers(Index);
        }

        public void NextQuestion() {
            if (!HasNext) return;
            CurrentQuestion = Quiz.Questions.ElementAt(++Index);
            SetAnswers(Index);
        }

        public void StartTimer() {
            Time = TimeSpan.FromSeconds(Quiz.Time);
            _timer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Normal, delegate {
                if (Time == TimeSpan.Zero) EndQuiz();
                Time = Time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
        }

        public void EndQuiz() {
            _timer.Stop();
            ShowResults();
            CalcPoints();

            ResultText = $"Twój wynik: {Points}/{Quiz.Questions.Count}";
            MessageBox.Show(ResultText, "Wynik", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ToggleAnswer(int i) {
            if (!IsRunning) return;
            CurrentAnswer[i] = !CurrentAnswer[i];
            CurrentResult[i] = CurrentAnswer[i] ? Brushes.DodgerBlue : Brushes.Black;
            OnPropertyChanged("CurrentResult");
        }

        private void SetAnswers(int newIndex) {
            CurrentAnswer = _currentAnswers[newIndex];
        }

        private void ShowResults() {
            for (var i = 0; i < _quiz.Questions.Count; i++)
                for (var j = 0; j < 4; j++) {
                    SolidColorBrush res;
                    var a = _currentAnswers[i][j];
                    var b = _quiz.Questions[i].Answers[j].IsCorrect;
                    if (a == b)
                        res = a ? Brushes.LawnGreen : Brushes.Black;
                    else
                        res = a ? Brushes.Red : Brushes.DodgerBlue;
                    _results[i][j] = res;
                }

            OnPropertyChanged("CurrentResult");
        }

        private void CalcPoints() {
            var points = 0D;
            for (var i = 0; i < _quiz.Questions.Count; i++) {
                var count = 0D;
                for (var j = 0; j < 4; j++) {
                    if (!_currentAnswers[i][j]) continue;
                    if (_quiz.Questions[i].Answers[j].IsCorrect)
                        count++;
                    else
                        count -= 0.5;
                }

                var p = Math.Max(count, 0) / _quiz.Questions[i].CorrectAnswers;
                Debug.WriteLine($"{i}: +{p}");
                points += p;
            }

            Points = points;
        }
    }
}