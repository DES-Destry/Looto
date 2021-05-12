using Looto.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace Looto.Views
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            DataContext = new SettingsViewModel();
        }

        /// <summary>
        /// Exit from application.
        /// </summary>
        /// <param name="sender">Button.</param>
        /// <param name="e">Some event args.</param>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Move window.
        /// </summary>
        /// <param name="sender">Bar(Grid element).</param>
        /// <param name="e">Some event args.</param>
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
