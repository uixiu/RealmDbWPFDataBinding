using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RealmDb
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static MainWindowViewModel? _mainWindowsViewModel;
        public static MainWindowViewModel MainViewModel => _mainWindowsViewModel ??= new MainWindowViewModel();

    }
}
