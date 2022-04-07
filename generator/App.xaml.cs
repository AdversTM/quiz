﻿using System;
using System.IO;
using System.Windows;
using generator.data;
using generator.model;

namespace generator {
    public partial class App {
        internal static readonly string PDir = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName;

        private readonly IQuizStore _defaultStore;
        private QuizWindow _quizWindow;

        public App() {
            var cipher = new AESCipher("tPuRkUT5ELcQd6wAe2FrBzMaH7KsmCfZ", "ykaXN3G6SeWVpsqx");
            _defaultStore = new XmlQuizStore(cipher);
        }
        
        public void LoadQuiz(string file, IQuizStore store = null) {
            var q = LoadFile(file, store);
            if (q == null) return;
            OpenQuiz(q);
        }

        public Quiz LoadFile(string file, IQuizStore store = null) {
            var q = (store ?? _defaultStore).Load(file);
            if (q != null) return q;

            MessageBox.Show("Nie można wczytać pliku!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            return null;
        }

        public bool SaveFile(string file, Quiz quiz, IQuizStore store = null) {
            if ((store ?? _defaultStore).Save(quiz, file)) {
                MessageBox.Show("Quiz został zapisany.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }

            MessageBox.Show("Nie można zapisać quizu!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }

        public void OpenQuiz(Quiz quiz) {
            _quizWindow = new QuizWindow(quiz);
            _quizWindow.Show();
            MainWindow?.Hide();
        }

        public void CloseQuiz() {
            _quizWindow?.Hide();
            _quizWindow = null;
            MainWindow?.Show();
        }

        
    }
}