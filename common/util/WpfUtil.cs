using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;

namespace common.util {
    public static class WpfUtil {
        public static string GetFullPath(HeaderedItemsControl node, string parentDir = "") {
            var result = Convert.ToString(node.Header);
            
            for (var i = node.Parent as TreeViewItem;
                 i != null;
                 i = VisualTreeHelper.GetParent(i) as TreeViewItem)
                if (i.Parent is not TreeView)
                    result = i.Header + "\\" + result;

            return parentDir + "\\" + result;
        }
        
        public static TreeViewItem CreateDirectoryNode(DirectoryInfo directoryInfo) {
            var node = new TreeViewItem { Header = directoryInfo.Name };
            foreach (var directory in directoryInfo.GetDirectories())
                node.Items.Add(CreateDirectoryNode(directory));
            foreach (var file in directoryInfo.GetFiles())
                node.Items.Add(new TreeViewItem { Header = file.Name });
            return node;
        }
        
        public static string OpenFileDialog(string parentDir, string filter = "Pliki Quiz-u (*.quiz)|*.quiz") {
            var dialog = new OpenFileDialog {
                InitialDirectory = parentDir,
                Filter = filter + "|Wszystkie pliki (*.*)|*.*",
                RestoreDirectory = true
            };
            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }

        public static string SaveFileDialog(string parentDir, string name = "quiz1", string filter = "Pliki Quiz-u (*.quiz)|*.quiz") {
            var dialog = new SaveFileDialog {
                FileName = name,
                InitialDirectory = parentDir,
                Filter = filter + "|Wszystkie pliki (*.*)|*.*",
                RestoreDirectory = true
            };
            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }
    }
}