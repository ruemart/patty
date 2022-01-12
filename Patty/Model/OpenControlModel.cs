using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Patty.Model
{
    internal class OpenControlModel : ObservableObject
    {
        private RecentFile? selectedRecentFile;

        public static string RecentFilesPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Patty", "RecentFileList.json");

        public ObservableCollection<RecentFile> RecentFiles { get; } = new();
        public RecentFile? SelectedRecentFile { get => selectedRecentFile; set => SetProperty(ref selectedRecentFile, value); }
    }
}
