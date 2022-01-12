using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Win32;
using Patty.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Patty.ViewModel
{
    internal class OpenControlViewModel : ObservableRecipient
    {
        public OpenControlViewModel()
        {
            CreateCommand = new RelayCommand(CreateNewDatabase);
            OpenCommand = new RelayCommand(OpenDatabase);
            DeleteRecentFileCommand = new RelayCommand<string>(DeleteRecentFile);
            OpenRecentCommand = new RelayCommand(OpenRecentFile);
            Model = new();
            LoadRecentItems();
        }

        public ICommand CreateCommand { get; }
        public ICommand OpenCommand { get; }
        public ICommand DeleteRecentFileCommand { get; }
        public ICommand OpenRecentCommand { get; }
        public OpenControlModel Model { get; set; }

        public event EventHandler<string>? Submit;

        private void OpenRecentFile()
        {
            if (Model.SelectedRecentFile != null)
            {
                Submit?.Invoke(this, Model.SelectedRecentFile.Path);
            }
        }

        private void LoadRecentItems()
        {
            var directoryPath = Path.GetDirectoryName(OpenControlModel.RecentFilesPath);
            if (string.IsNullOrEmpty(directoryPath))
            {
                return;
            }

            // Create the directory if it does not exist.
            Directory.CreateDirectory(directoryPath!);
            if (!File.Exists(OpenControlModel.RecentFilesPath))
            {
                // There is no recent files json on disk, yet -> Create an empty one and exit this method.
                WriteRecentItems();
                return;
            }

            // Load the recent files list if it exists.

            var recentFiles = JsonSerializer.Deserialize<List<RecentFile>>(File.ReadAllText(OpenControlModel.RecentFilesPath)) ?? new();
            var nonExistingFiles = recentFiles.Where(recentItem => !File.Exists(recentItem.Path)).ToList();

            // Remove invalid entries from recent files list.
            foreach (var fileToRemove in nonExistingFiles)
            {
                recentFiles.Remove(fileToRemove);
            }

            // Some items are not valid anymore -> Rewrite recent files list.
            if (nonExistingFiles.Count > 0)
            {
                WriteRecentItems();
            }

            var distinctFiles = recentFiles.DistinctBy(file => file.Path).ToList();
            // The list had some duplicates -> Rewrite recent files list.
            if (distinctFiles.Count != recentFiles.Count)
            {
                WriteRecentItems();
            }

            Model.RecentFiles.Clear();
            foreach (var file in distinctFiles.OrderByDescending(item => item.LastUsed).ToList())
            {
                Model.RecentFiles.Add(file);
            }
        }

        private void WriteRecentItems()
        {
            var recentFilesString = JsonSerializer.Serialize(Model.RecentFiles);
            File.WriteAllText(OpenControlModel.RecentFilesPath, recentFilesString);
        }

        private async void OpenDatabase()
        {
            var dbFilePath = await OpenFileOpenExplorerAsync();
            if (dbFilePath == null)
            {
                return;
            }

            Model.RecentFiles.Add(new RecentFile
            {
                Path = dbFilePath,
                LastUsed = DateTime.Now,
                Name = new FileInfo(dbFilePath).Name,
            });
            WriteRecentItems();

            if (!string.IsNullOrEmpty(dbFilePath))
            {
                Submit?.Invoke(this, dbFilePath);
            }
        }

        private async void CreateNewDatabase()
        {
            var dbFilePath = await OpenFileSaveDialogAsync();
            if (dbFilePath == null)
            {
                return;
            }

            Model.RecentFiles.Add(new RecentFile
            {
                Path = dbFilePath,
                LastUsed = DateTime.Now,
                Name = new FileInfo(dbFilePath).Name,
            });
            WriteRecentItems();

            if (!string.IsNullOrEmpty(dbFilePath))
            {
                Submit?.Invoke(this, dbFilePath);
            }
        }

        private void DeleteRecentFile(string? filePath)
        {
            if (Model.RecentFiles.All(file => file.Path != filePath))
            {
                return;
            }

            Model.RecentFiles.Remove(Model.RecentFiles.Single(file => file.Path == filePath));
            WriteRecentItems();
        }

        private static async Task<string?> OpenFileSaveDialogAsync() =>
            await Task.Run(() =>
            {
                var dialog = new SaveFileDialog
                {
                    Title = "Erstellen",
                    Filter = "DB-Dateien (*.db)|*.db",
                };
                return dialog.ShowDialog() != true ? default : dialog.FileName;
            });

        private static async Task<string?> OpenFileOpenExplorerAsync() =>
            await Task.Run(
                () =>
                {
                    //TODO Localization
                    var dialog = new OpenFileDialog
                    {
                        Title = "Öffnen",
                        CheckFileExists = true,
                        CheckPathExists = true,
                        Filter = "DB-Dateien (*.db)|*.db",
                        Multiselect = false,
                    };
                    return dialog.ShowDialog() != true ? default : dialog.FileName;
                });
    }
}
