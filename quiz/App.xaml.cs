using System;
using System.IO;
using System.Windows;
using common.data;
using common.model;
using common.util;

namespace quiz {
    public partial class App {
        internal static readonly string PDir = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName;

        private readonly IQuizStore _defaultStore;
        private MainWindow _mainWindow;

        public App() {
            var cipher = new AESCipher("tPuRkUT5ELcQd6wAe2FrBzMaH7KsmCfZ", "ykaXN3G6SeWVpsqx");
            _defaultStore = new XmlQuizStore(cipher);
            
            var file = WpfUtil.OpenFileDialog(PDir);
            if (file == null) return;
            LoadQuiz(file);
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
        
        public void OpenQuiz(Quiz quiz) {
            _mainWindow = new MainWindow(quiz);
            _mainWindow.Show();
            _mainWindow.Activate();
        }
    }
}