using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Looto.Components
{
    /// <summary>User control for using in the <see cref="Views.SettingsWindow"/> as input/output settings data as text.</summary>
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
                    new PropertyChangedCallback(PropertyChanged)));




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
                    new PropertyChangedCallback(PropertyChanged)));




        private bool IsChanged = false;
        private bool IsPropertyChanged = false;

        /// <summary>Create new component.</summary>
        public TextBoxSettingsElement()
        {
            InitializeComponent();
        }

        /// <summary>Rerender component with new DP values.</summary>
        public void Render()
        {
            SettingsImage.Source = new BitmapImage(new Uri(ImageSource, UriKind.Relative));
            Title.Text = TitleText;
            Description.Text = DescriptionText;
            Content.Text = ContentText;

            if (!IsValidInput)
                OutBorder.BorderBrush = (Brush)Application.Current.MainWindow.FindResource("RedBrush");
        }

        /// <summary>Calls when main grid was loaded and it ready for render values from DP.</summary>
        /// <param name="sender">Grid which loaded event binded on this method.</param>
        /// <param name="e">Some event arguments.</param>
        private void MainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Render();
        }

        // TODO: this method crash the VS XAML constructor.
        /// <summary>
        /// Calls when bindable DP was changed with <see cref="ViewModels.BaseViewModel.OnPropertyChanged(string)"/> method call.<br/>
        /// Bindable DP's: <see cref="ContentTextProperty"/>, <see cref="IsValidInputProperty"/>.
        /// </summary>
        /// <param name="sender">Instance of <see cref="TextBoxSettingsElement"/> which bindable DP was changed.</param>
        /// <param name="e">Some event arguments.</param>
        private static void PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var component = sender as TextBoxSettingsElement;

            if (component?.IsValidInput ?? false)
            {
                component.ShowBorderAnimation.Value = (Brush)Application.Current.MainWindow.FindResource("MainBrush");
                component.ImageSource = component.ImageSource.Replace("red", "main");
                component.SettingsImage.Source = new BitmapImage(new Uri(component.ImageSource, UriKind.Relative));
            }
            else
            {
                component.ShowBorderAnimation.Value = (Brush)Application.Current.MainWindow.FindResource("RedBrush");
                component.ImageSource = component.ImageSource.Replace("main", "red");
                component.SettingsImage.Source = new BitmapImage(new Uri(component.ImageSource, UriKind.Relative));
            }

            component.IsPropertyChanged = true;
            component.Content.Text = component.ContentText;
            component.IsPropertyChanged = false;
        }

        /// <summary>
        /// Calls when text in textbox was changed.<br/>
        /// Refresh <see cref="ContentText"/> DP.
        /// </summary>
        /// <param name="sender"><see cref="TextBox"/> which text changed event binded on this method.</param>
        /// <param name="e">Some event arguments.</param>
        private void Content_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsPropertyChanged)
            {
                ContentText = (sender as TextBox).Text;
                return;
            }

            if (IsPropertyChanged && !IsChanged)
            {
                ContentText = (sender as TextBox).Text;
                IsChanged = !IsChanged;
            }
        }
    }
}
