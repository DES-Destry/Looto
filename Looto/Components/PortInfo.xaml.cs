using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Looto.Components
{
    /// <summary>User control to show state of one port.</summary>
    public partial class PortInfo : UserControl
    {
        /// <summary>If it is darker - background will be darker, not fully transparent.</summary>
        public bool IsDarker
        {
            get => (bool)GetValue(IsDarkerProperty);
            set { SetValue(IsDarkerProperty, value); }
        }

        /// <summary>DP for <see cref="IsDarker"/> property</summary>
        public static readonly DependencyProperty IsDarkerProperty =
            DependencyProperty.Register("IsDarker", typeof(bool), typeof(PortInfo), new PropertyMetadata(false));


        /// <summary>Shows port number.</summary>
        public string Port
        {
            get => (string)GetValue(PortProperty);
            set { SetValue(PortProperty, value); }
        }

        /// <summary>DP for <see cref="Port"/> property.</summary>
        public static readonly DependencyProperty PortProperty =
            DependencyProperty.Register("Port", typeof(string), typeof(PortInfo), new PropertyMetadata(string.Empty));


        /// <summary>Shows which protocol was used.</summary>
        public string Protocol
        {
            get => (string)GetValue(ProtocolProperty);
            set { SetValue(ProtocolProperty, value); }
        }

        /// <summary>DP for <see cref="Protocol"/> property.</summary>
        public static readonly DependencyProperty ProtocolProperty =
            DependencyProperty.Register("Protocol", typeof(string), typeof(PortInfo), new PropertyMetadata(string.Empty));


        /// <summary>State of port(Opened/Closed/NotChecked).</summary>
        public string State
        {
            get => (string)GetValue(StateProperty);
            set { SetValue(StateProperty, value); }
        }

        /// <summary>DP for <see cref="State"/> property.</summary>
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(string), typeof(PortInfo), new PropertyMetadata(string.Empty));


        /// <summary>Create new component.</summary>
        public PortInfo()
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

            // Apply all text content.
            PortText.Text = Port;
            ProtocolText.Text = Protocol;
            StateText.Text = State;

            // Color state foe more informative.
            if (State == "Opened")
                StateText.Foreground = (Brush)Application.Current.MainWindow.FindResource("OpenedBrush");
            else if (State == "Closed")
                StateText.Foreground = (Brush)Application.Current.MainWindow.FindResource("ClosedBrush");
            else if (State == "NotChecked")
                StateText.Foreground = (Brush)Application.Current.MainWindow.FindResource("NotCheckedBrush");
        }
    }
}
