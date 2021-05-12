using Looto.Components;
using Looto.Models.Data;
using Looto.Models.PortScanner;
using Looto.Models.Utils;
using Looto.ViewModels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Looto.Views
{
    public partial class ResultsWindow : Window
    {
        private readonly Settings _settings;

        public ResultsWindow()
        {
            InitializeComponent();

            // Initilalize view model for view
            DataContext = new ResultsViewModel();

            _settings = new Settings();
        }
        public ResultsWindow(ScanResult results)
        {
            InitializeComponent();
            // Initilalize view model for view with ready results
            var vm = new ResultsViewModel(results);
            vm.OnRenderRequest += RenderResult;

            DataContext = vm;

            _settings = new Settings();
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

        /// <summary>
        /// Move window
        /// </summary>
        /// <param name="sender">Bar(Grid element)</param>
        /// <param name="e">Some event args</param>
        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        /// <summary>When container with results was loaded.</summary>
        /// <param name="sender">Container which call this method.</param>
        /// <param name="e">Some event arguments.</param>
        private void ResultsContainer_Loaded(object sender, RoutedEventArgs e)
        {
            Port[] resultsToRender = (DataContext as ResultsViewModel).Result.PortsAfterScan;
            (DataContext as ResultsViewModel).MaxProgress = resultsToRender.Length;

            if (_settings?.GetSettings().ResultsRenderMode != ResultsRenderMode.NotRender)
                RenderResult(resultsToRender);
        }

        /// <summary>Add components for all ports of result.</summary>
        /// <param name="resultsToRender">Ports to render.</param>
        private async void RenderResult(Port[] resultsToRender)
        {
            ResultsContainer.Children.Clear();
            TextContainer.Text = "";

            ResultsRenderMode renderMode = _settings.GetSettings().ResultsRenderMode;
            StringBuilder stringBuilder = new StringBuilder();

            var vm = DataContext as ResultsViewModel;

            vm.CurrentProgress = 0;
            vm.IsLoading = true;

            bool showOpened = vm.ShowOpened;
            bool showClosed = vm.ShowClosed;
            bool showFiltered = vm.ShowFiltered;
            bool showOpenedOrFiltered = vm.ShowOpenedOrFiltered;
            bool showNotChecked = vm.ShowNotChecked;

            bool isDarker = true;
            await Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    foreach (Port result in resultsToRender)
                    {
                        if ((showOpened && result.State == PortState.Opened)
                            || (showClosed && result.State == PortState.Closed)
                            || (showNotChecked && result.State == PortState.NotChecked)
                            || (showFiltered && result.State == PortState.Filtered)
                            || (showOpenedOrFiltered && result.State == PortState.OpenedOrFiltered))
                        {
                            if (renderMode == ResultsRenderMode.Full)
                            {
                                PortInfo component = new PortInfo
                                {
                                    Port = result.Value.ToString(),
                                    Protocol = result.Protocol.ToString(),
                                    State = result.State.EnumToString(),
                                    IsDarker = isDarker,
                                };
                                ResultsContainer.Children.Add(component);
                                (DataContext as ResultsViewModel).CurrentProgress++;
                                isDarker = !isDarker;
                            }
                            else if (renderMode == ResultsRenderMode.AsText)
                            {
                                stringBuilder.Append($"{result.Value}/{result.Protocol}: {result.State.EnumToString()} \n");
                            }
                        }
                    }

                    if (renderMode == ResultsRenderMode.Full)
                    {
                        TableInfo.Visibility = Visibility.Visible;
                    }
                    else if (renderMode == ResultsRenderMode.AsText)
                    {
                        TableInfo.Visibility = Visibility.Collapsed;
                        TextContainer.Text = stringBuilder.ToString();
                    }
                });
            });
            vm.IsLoading = false;
        }
    }
}
