using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Patty.Model
{
    internal class MainWindowModel : ObservableObject
    {
        private object? windowContent;

        public string DbPath { get; set; } = string.Empty;

        public object? WindowContent { get => windowContent; set => SetProperty(ref windowContent, value); }
    }
}
