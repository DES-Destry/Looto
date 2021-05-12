using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Looto.Components
{
    public partial class ComboBoxSettingsElement : UserControl
    {
        /// <summary>Image source of settings element.</summary>
        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        /// <summary>DP for <see cref="ImageSource"/> property.</summary>
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(string), typeof(ComboBoxSettingsElement), new PropertyMetadata(string.Empty));




        /// <summary>Title of the settings element.</summary>
        public string TitleText
        {
            get => (string)GetValue(TitleTextProperty);
            set { SetValue(TitleTextProperty, value); }
        }

        /// <summary>DP for <see cref="TitleText"/> property.</summary>
        public static readonly DependencyProperty TitleTextProperty =
            DependencyProperty.Register("TitleText", typeof(string), typeof(ComboBoxSettingsElement), new PropertyMetadata(string.Empty));




        /// <summary>Description text of the settings element.</summary>
        public string DescriptionText
        {
            get => (string)GetValue(DescriptionTextProperty);
            set { SetValue(DescriptionTextProperty, value); }
        }

        /// <summary>DP for <see cref="DescriptionText"/> property.</summary>
        public static readonly DependencyProperty DescriptionTextProperty =
            DependencyProperty.Register("DescriptionText", typeof(string), typeof(ComboBoxSettingsElement), new PropertyMetadata(string.Empty));




        /// <summary>Value of the settings.</summary>
        public string ComboElements
        {
            get => (string)GetValue(ComboElementsProperty);
            set { SetValue(ComboElementsProperty, value); }
        }

        /// <summary>DP for <see cref="ComboElements"/> property.</summary>
        public static readonly DependencyProperty ComboElementsProperty =
            DependencyProperty.Register("ComboElements", typeof(string), typeof(ComboBoxSettingsElement), new PropertyMetadata(string.Empty));




        /// <summary>Value of the settings.</summary>
        public int CurrentSelectedItem
        {
            get => (int)GetValue(CurrentSelectedItemProperty);
            set { SetValue(CurrentSelectedItemProperty, value); }
        }

        /// <summary>DP for <see cref="ComboElements"/> property.</summary>
        public static readonly DependencyProperty CurrentSelectedItemProperty =
            DependencyProperty.Register(
                "CurrentSelectedItem",
                typeof(int),
                typeof(ComboBoxSettingsElement),
                new FrameworkPropertyMetadata(
                    0,
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
                typeof(ComboBoxSettingsElement),
                new FrameworkPropertyMetadata(
                    false,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(PropertyChanged)));




        /// <summary>Create new component.</summary>
        public ComboBoxSettingsElement()
        {
            InitializeComponent();
        }

        /// <summary>Rerender component with new DP values.</summary>
        public void Render()
        {
            Title.Text = TitleText;
            Description.Text = DescriptionText;
            Content.ItemsSource = ComboElements.Split(';');
            Content.SelectedIndex = CurrentSelectedItem;
        }

        /// <summary>Calls when main grid was loaded and it ready for render values from DP.</summary>
        /// <param name="sender">Grid which loaded event binded on this method.</param>
        /// <param name="e">Some event arguments.</param>
        private void MainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Render();
        }

        /// <summary>
        /// Calls when bindable DP was changed with <see cref="ViewModels.BaseViewModel.OnPropertyChanged(string)"/> method call.<br/>
        /// Bindable DP's: <see cref="ContentTextProperty"/>, <see cref="IsValidInputProperty"/>.
        /// </summary>
        /// <param name="sender">Instance of <see cref="ComboBoxSettingsElement"/> which bindable DP was changed.</param>
        /// <param name="e">Some event arguments.</param>
        private static void PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var component = sender as ComboBoxSettingsElement;

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
        }

        /// <summary>
        /// Calls when selected item in the combobox was changed.<br/>
        /// Refresh <see cref="CurrentSelectedItem"/> DP.</summary>
        /// <param name="sender"><see cref="ComboBox"/> which text changed event binded on this method.</param>
        /// <param name="e">Some event arguments.</param>
        private void Content_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentSelectedItem = (sender as ComboBox).SelectedIndex;
        }
    }
}
