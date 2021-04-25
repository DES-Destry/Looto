using Looto.Components;
using Looto.Models.Data;
using Looto.Models.PortScanner;
using Looto.ViewModels;
using System.Windows;

namespace Looto.Views
{
    public partial class ScanHistoryWindow : Window
    {
        private readonly Cache _cache;

        /// <summary>Create dafault view.</summary>
        public ScanHistoryWindow()
        {
            InitializeComponent();
        }

        /// <summary>Create view with cache file instance.</summary>
        /// <param name="cache">Cache file.</param>
        public ScanHistoryWindow(Cache cache) : this()
        {
            _cache = cache;

            ScanHistoryViewModel vm = new ScanHistoryViewModel(_cache);
            vm.RenderCache += RenderCache;
            vm.Render();

            DataContext = vm;
        }

        /// <summary>Render cache data.</summary>
        /// <param name="cache">Cahce data.</param>
        private void RenderCache(CacheData cache)
        {
            bool isDarker = true;
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var chunck in cache.Chuncks)
                {
                    var component = new CacheChunck()
                    {
                        IsDarker = isDarker,
                        ScanResult = chunck,
                    };
                    component.OnDeleteClicked += Component_OnDeleteClicked;
                    ResultsContainer.Children.Add(component);

                    isDarker = !isDarker;
                }
            });
        }

        /// <summary>Delete cache chunck if components delete button was clicked.</summary>
        /// <param name="chunck">Chunck for deletion.</param>
        private void Component_OnDeleteClicked(ScanResult chunck)
        {
            _cache.RemoveChunck(chunck);
            _cache.Save();
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
    }
}
