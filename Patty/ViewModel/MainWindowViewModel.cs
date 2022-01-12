using Microsoft.Toolkit.Mvvm.Input;
using Patty.Model;
using Patty.View;

namespace Patty.ViewModel
{
    internal class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            Model = new MainWindowModel();
            var openControlView = new OpenControlView
            {
                OpenDbCommand = new RelayCommand<string>(OnOpenDb)
            };
            Model.WindowContent = openControlView;
        }

        public MainWindowModel Model { get; set; }

        private void OnOpenDb(string? dbPath)
        {
            if (string.IsNullOrEmpty(dbPath))
            {
                return;
            }

            Model.DbPath = dbPath;
            //var dbControlView
        }

    }
}
