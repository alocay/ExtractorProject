using System;
using System.Windows;
using System.Windows.Forms;
using MahApps.Metro.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace AudioExtractor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        Extractor extractor = new Extractor();
        readonly string[] delimiters = { "\r\n", "\n" };

        public MainWindow()
        {
            InitializeComponent();
            this.ExtractButton.Click += this.OnExtractButtonClick;
            this.OutputPathButton.Click += this.OnOutputPathButtonClick;
            this.DataContext = this.extractor;
            this.Closing += this.OnExit;
        }

        private void OnOutputPathButtonClick(object sender, RoutedEventArgs e)
        {
            string path = string.Empty;

            if (CommonFileDialog.IsPlatformSupported)
            {
                using (CommonOpenFileDialog dialog = new CommonOpenFileDialog())
                {
                    dialog.IsFolderPicker = true;
                    dialog.Multiselect = false;
                    dialog.DefaultDirectory = this.OutputPathBox.Text;
                    if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        path = dialog.FileName;
                    }
                }
            }
            else 
            {
                using (FolderBrowserDialog dialog = new FolderBrowserDialog())
                {
                    dialog.RootFolder = Environment.SpecialFolder.MyMusic;
                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        path = dialog.SelectedPath;
                    }
                }
            }

            extractor.OutputPath = path;
        }

        private void OnExtractButtonClick(object sender, RoutedEventArgs e)
        {
            string[] urls = this.UrlBox.Text.Split(delimiters, StringSplitOptions.None);
            this.extractor.Extract(urls);
        }

        private void OnExit(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.extractor.CancelAll();
        }
    }
}
