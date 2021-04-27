using Looto.Models.HostScanner;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Looto.Components
{
    /// <summary>User control to show one LAN host element.</summary>
    public partial class HostInfo : UserControl
    {
        /// <summary>If it is darker - background will be darker, not fully transparent.</summary>
        public bool IsDarker
        {
            get => (bool)GetValue(IsDarkerProperty);
            set { SetValue(IsDarkerProperty, value); }
        }

        /// <summary>DP for <see cref="IsDarker"/> property.</summary>
        public static readonly DependencyProperty IsDarkerProperty =
            DependencyProperty.Register("IsDarker", typeof(bool), typeof(HostInfo), new PropertyMetadata(false));




        /// <summary>Host text.</summary>
        public string HostText
        {
            get => (string)GetValue(HostTextProperty);
            set { SetValue(HostTextProperty, value); }
        }

        /// <summary>DP for <see cref="HostText"/> property.</summary>
        public static readonly DependencyProperty HostTextProperty =
            DependencyProperty.Register("HostText", typeof(string), typeof(HostInfo), new PropertyMetadata(string.Empty));




        /// <summary>Time text.</summary>
        public string TimeText
        {
            get => (string)GetValue(TimeTextProperty);
            set { SetValue(TimeTextProperty, value); }
        }

        /// <summary>DP for <see cref="TimeText"/> property.</summary>
        public static readonly DependencyProperty TimeTextProperty =
            DependencyProperty.Register("TimeText", typeof(string), typeof(HostInfo), new PropertyMetadata(string.Empty));




        /// <summary>Host apply button was clicked.</summary>
        public Action<string> HostApplied
        {
            get => (Action<string>)GetValue(HostAppliedProperty);
            set { SetValue(HostAppliedProperty, value); }
        }

        /// <summary>DP for <see cref="HostApplied"/> property.</summary>
        public static readonly DependencyProperty HostAppliedProperty =
            DependencyProperty.Register("HostApplied", typeof(Action<string>), typeof(HostInfo));




        /// <summary>Create new component.</summary>
        public HostInfo()
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

            // If host in this list - current device where started Looto, then show it.
            if (HostChecker.GetLocalIPs().Contains(HostText))
            {
                HostText += " (me)";
            }

            Host.Text = HostText;
            Time.Text = TimeText;
        }

        /// <summary>When user clicked at "Apply" button.</summary>
        /// <param name="sender">Xaml element, which called this method.</param>
        /// <param name="e">Some event arguments.</param>
        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            HostApplied?.Invoke(HostText);
        }
    }
}
