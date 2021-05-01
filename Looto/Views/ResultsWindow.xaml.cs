using Looto.Components;
using Looto.Models.PortScanner;
using Looto.Models.Utils;
using Looto.ViewModels;
using System.Threading.Tasks;
using System.Windows;

namespace Looto.Views
{
    public partial class ResultsWindow : Window
    {
        public ResultsWindow()
        {
            InitializeComponent();

            // Initilalize view model for view
            DataContext = new ResultsViewModel();
        }
        public ResultsWindow(ScanResult results)
        {
            InitializeComponent();
            // Initilalize view model for view with ready results
            var vm = new ResultsViewModel(results);
            vm.OnRenderRequest += RenderResult;

            DataContext = vm;
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

            RenderResult(resultsToRender);
        }

        /// <summary>Add components for all ports of result.</summary>
        /// <param name="resultsToRender">Ports to render.</param>
        private async void RenderResult(Port[] resultsToRender)
        {
            ResultsContainer.Children.Clear();

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
                foreach (Port result in resultsToRender)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if ((showOpened && result.State == PortState.Opened)
                            || (showClosed && result.State == PortState.Closed)
                            || (showNotChecked && result.State == PortState.NotChecked)
                            || (showFiltered && result.State == PortState.Filtered) 
                            || (showOpenedOrFiltered && result.State == PortState.OpenedOrFiltered))
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
                    });
                }
            });
            (DataContext as ResultsViewModel).IsLoading = false;
        }
    }
}
