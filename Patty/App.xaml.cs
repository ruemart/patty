﻿using ControlzEx.Theming;
using System.Windows;

namespace Patty
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ThemeManager.Current.ChangeTheme(this, "Dark.Lime");
        }
    }
}
