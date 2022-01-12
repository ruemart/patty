using Patty.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Patty.View
{
    /// <summary>
    /// Interaction logic for OpenControlView.xaml
    /// </summary>
    public partial class OpenControlView : UserControl
    {
        public static readonly DependencyProperty OpenDbCommandProperty = DependencyProperty.RegisterAttached(
            "OpenDbCommand",
            typeof(ICommand),
            typeof(OpenControlView));

        public OpenControlView()
        {
            InitializeComponent();
            DataContext = new OpenControlViewModel();
            ((OpenControlViewModel)DataContext).Submit += OnOpenDb;
        }

        public ICommand? OpenDbCommand { get => (ICommand)GetValue(OpenDbCommandProperty); set => SetValue(OpenDbCommandProperty, value); }

        private void OnOpenDb(object? sender, string path)
        {
            OpenDbCommand?.Execute(path);
        }
    }
}
