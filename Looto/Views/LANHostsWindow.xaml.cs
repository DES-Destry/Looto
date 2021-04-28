using Looto.Components;
using Looto.Models.Data;
using Looto.Models.Utils;
using Looto.ViewModels;
using System;
using System.Linq;
using System.Windows;

namespace Looto.Views
{
    /// <summary>View for LAN hosts list.</summary>
    public partial class LANHostsWindow : Window
    {
        private readonly Cache _cache;
        private bool _isDarkerComponent = true;

        /// <summary>Calls, when user clicks at "Apply" button.</summary>
        public event Action<string> HostApplied;

        /// <summary>Create new view.</summary>
        public LANHostsWindow()
        {
            var vm = new LANListViewModel();
            vm.OnHostFinded += HostFinded;

            InitializeComponent();
            DataContext = vm;
        }

        /// <summary>Create view with apply event.</summary>
        /// <param name="onHostApplied">Calls, when user clicks at "Apply" button.</param>
        public LANHostsWindow(Cache cache, Action<string> onHostApplied)
        {
            _cache = cache;

            var vm = new LANListViewModel();
            HostApplied += onHostApplied;
            HostApplied += CloseOnApply;
            vm.OnHostFinded += HostFinded;

            InitializeComponent();
            DataContext = vm;
        }

        /// <summary>
        /// Exit from application
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">Some event args</param>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>Close window, when host was applied.</summary>
        /// <param name="_">Applied host. There this parameter not nessecary. Need for event <see cref="Action{string}"/> compliance.</param>
        private void CloseOnApply(string _) => Close();

        /// <summary>
        /// Move window
        /// </summary>
        /// <param name="sender">Bar(Grid element)</param>
        /// <param name="e">Some event args</param>
        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        /// <summary>Calls when finded available LAN host, then new component will be rendered in the StackPanel.</summary>
        /// <param name="host">Host, that was finded.</param>
        private void HostFinded(string host)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                string time = "";
                var alreadyScannedResults = _cache.GetCache().Chuncks
                    .Where(chunck => chunck.Host == host).ToArray();

                if (alreadyScannedResults.Length > 0)
                {
                    var alreadyScannedResult = alreadyScannedResults.Reverse().FirstOrDefault();
                    time = alreadyScannedResult.ScanDate.GetTimeString();
                }

                HostInfo component = new HostInfo()
                {
                    IsDarker = _isDarkerComponent,
                    HostText = host,
                    TimeText = time,
                    HostApplied = HostApplied,
                };
                ResultsPanel.Children.Add(component);
                _isDarkerComponent = !_isDarkerComponent;
            });
        }
    }
}
