using Looto.ViewModels;
using System;
using System.Windows;

namespace Looto.Views
{
    public partial class LANHostsWindow : Window
    {
        public LANHostsWindow()
        {
            InitializeComponent();
        }

        public LANHostsWindow(Action<string> onHostApplyed)
        {
            var vm = new LANListViewModel();
            vm.OnHostApplyed += onHostApplyed;

            InitializeComponent();
            DataContext = vm;
        }

        /// Exit from application
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">Some event args</param>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Move window
        /// </summary>
        /// <param name="sender">Bar(Grid element)</param>
        /// <param name="e">Some event args</param>
        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
