using Looto.Models.PortScanner;
using Looto.Models.Utils;
using Looto.Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Looto.Components
{
    /// <summary>User control to show one chunck of cache.</summary>
    public partial class CacheChunck : UserControl
    {
        /// <summary>If it is darker - background will be darker, not fully transparent.</summary>
        public bool IsDarker
        {
            get => (bool)GetValue(IsDarkerProperty);
            set { SetValue(IsDarkerProperty, value); }
        }

        /// <summary>DP for <see cref="IsDarker"/> property.</summary>
        public static readonly DependencyProperty IsDarkerProperty =
            DependencyProperty.Register("IsDarker", typeof(bool), typeof(CacheChunck), new PropertyMetadata(false));



        /// <summary>Scan result data from cache.</summary>
        public ScanResult ScanResult
        {
            get => (ScanResult)GetValue(ScanResultProperty);
            set { SetValue(ScanResultProperty, value); }
        }

        /// <summary>DP for <see cref="ScanResult"/> property.</summary>
        public static readonly DependencyProperty ScanResultProperty =
            DependencyProperty.Register("ScanResult", typeof(ScanResult), typeof(CacheChunck));



        /// <summary>Calls when user clicked on delete button.</summary>
        public event Action<ScanResult> OnDeleteClicked;

        /// <summary>Create new component.</summary>
        public CacheChunck()
        {
            InitializeComponent();
        }

        /// <summary>When grid, where placed all content was loaded, apply all DPs to show all info.</summary>
        /// <param name="sender">Xaml element, which called this method.</param>
        /// <param name="e">Some event arguments.</param>
        private void MainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            // Is is darker - made background slightly(127/255) transparent black color.
            if (IsDarker)
                MainGrid.Background = new SolidColorBrush(Color.FromArgb(0x7F, 0, 0, 0));

            Host.Text = ScanResult.Host;
            Time.Text = ScanResult.ScanDate.GetTimeString();
        }

        /// <summary>When user clicks at show button.</summary>
        /// <param name="sender">Xaml element, which called this method(show button).</param>
        /// <param name="e">Some event arguments.</param>
        private void Show_Click(object sender, RoutedEventArgs e)
        {
            ResultsWindow view = new ResultsWindow(ScanResult);
            view.Show();
        }

        /// <summary>When user clicks at delete button.</summary>
        /// <param name="sender">Xaml element, which called this method(delete button).</param>
        /// <param name="e">Some event arguments.</param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            OnDeleteClicked?.Invoke(ScanResult);
        }
    }
}
