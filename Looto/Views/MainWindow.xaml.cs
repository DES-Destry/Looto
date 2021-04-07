using System.Windows;
using Looto.ViewModels;

namespace Looto.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Initilalize view model for view
            DataContext = new MainViewModel();
        }

        /// <summary>
        /// Exit from application
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">Some event args</param>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Move window
        /// </summary>
        /// <param name="sender">Bar(Grid element)</param>
        /// <param name="e"></param>
        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
