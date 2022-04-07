using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using generator.model;
using generator.util;

namespace generator {
    public partial class MainWindow {
        private static App Current => Application.Current as App;

        public MainWindow() {
            InitializeComponent();
            ListDirectory(App.PDir);
            if (TreeView.Items[0] is not TreeViewItem item) return;
            item.IsExpanded = true;
        }

        private void ButtonOpen_OnClick(object sender, RoutedEventArgs e) {
            var file = WpfUtil.OpenFileDialog(App.PDir);
            if (file == null) return;
            Current.LoadQuiz(file);
        }

        private void ListDirectory(string path) {
            TreeView.Items.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            TreeView.Items.Add(WpfUtil.CreateDirectoryNode(rootDirectoryInfo));
        }

        private void TreeView_OnDoubleClick(object sender, MouseButtonEventArgs e) {
            if (sender is not TreeViewItem { IsSelectionActive: true }) return;
            var item = TreeView.SelectedItem as TreeViewItem;
            if (item?.Items.Count != 0) return;
            var path = WpfUtil.GetFullPath(item, App.PDir);
            Debug.WriteLine(path);
            Current.LoadQuiz(path);
        }

        private void ButtonCreate_OnClick(object sender, RoutedEventArgs e) {
            var q = new Quiz();
            q.Questions.Add(new Question());
            Current.OpenQuiz(q);
        }
    }
}