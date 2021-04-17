using Looto.Components;
using Looto.ViewModels;
using System;
using System.Windows;

namespace Looto.Views
{
    /// <summary>View for LAN hosts list.</summary>
    public partial class LANHostsWindow : Window
    {
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
        public LANHostsWindow(Action<string> onHostApplied)
        {
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
                HostInfo component = new HostInfo()
                {
                    IsDarker = _isDarkerComponent,
                    HostText = host,
                    HostApplied = HostApplied,
                };
                ResultsPanel.Children.Add(component);
                _isDarkerComponent = !_isDarkerComponent;
            });
        }
    }
}
