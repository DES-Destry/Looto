using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Looto.Components
{
    public partial class TextBoxSettingsElement : UserControl
    {
        /// <summary>Image source of settings element.</summary>
        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        /// <summary>DP for <see cref="ImageSource"/> property.</summary>
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(string), typeof(TextBoxSettingsElement), new PropertyMetadata(string.Empty));




        /// <summary>Title of the settings element.</summary>
        public string TitleText
        {
            get => (string)GetValue(TitleTextProperty);
            set { SetValue(TitleTextProperty, value); }
        }

        /// <summary>DP for <see cref="TitleText"/> property.</summary>
        public static readonly DependencyProperty TitleTextProperty =
            DependencyProperty.Register("TitleText", typeof(string), typeof(TextBoxSettingsElement), new PropertyMetadata(string.Empty));




        /// <summary>Description text of the settings element.</summary>
        public string DescriptionText
        {
            get => (string)GetValue(DescriptionTextProperty);
            set { SetValue(DescriptionTextProperty, value); }
        }

        /// <summary>DP for <see cref="DescriptionText"/> property.</summary>
        public static readonly DependencyProperty DescriptionTextProperty =
            DependencyProperty.Register("DescriptionText", typeof(string), typeof(TextBoxSettingsElement), new PropertyMetadata(string.Empty));




        /// <summary>Value of the settings.</summary>
        public string ContentText
        {
            get => (string)GetValue(ContentTextProperty);
            set { SetValue(ContentTextProperty, value); }
        }

        /// <summary>DP for <see cref="ContentText"/> property.</summary>
        public static readonly DependencyProperty ContentTextProperty =
            DependencyProperty.Register(
                "ContentText", 
                typeof(string), 
                typeof(TextBoxSettingsElement), 
                new FrameworkPropertyMetadata(
                    string.Empty,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(ContentChanged)));




        /// <summary>Value of the settings.</summary>
        public bool IsValidInput
        {
            get => (bool)GetValue(IsValidInputProperty);
            set { SetValue(IsValidInputProperty, value); }
        }

        /// <summary>DP for <see cref="IsValidInput"/> property.</summary>
        public static readonly DependencyProperty IsValidInputProperty =
            DependencyProperty.Register(
                "IsValidInput",
                typeof(bool),
                typeof(TextBoxSettingsElement),
                new FrameworkPropertyMetadata(
                    false,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(ContentChanged)));




        public TextBoxSettingsElement()
        {
            InitializeComponent();

            ShowBorder.Storyboard.Completed += Storyboard_Completed;
            HideBorder.Storyboard.Completed += Storyboard_Completed;
        }

        private void MainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            SettingsImage.Source = new BitmapImage(new Uri(ImageSource, UriKind.Relative));
            Title.Text = TitleText;
            Description.Text = DescriptionText;
            Content.Text = ContentText;
        }

        private static void ContentChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as TextBoxSettingsElement).MainGrid_Loaded(sender, null);
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            if (IsValidInput)
                OutBorder.BorderBrush = (Brush)Application.Current.MainWindow.FindResource("ClosedBrush");
        }
    }
}
