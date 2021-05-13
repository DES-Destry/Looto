using Looto.Models;
using Looto.Models.Data;
using Looto.Models.PortScanner;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Looto.ViewModels
{
    /// <summary>
    /// View model for window with results.<br/>
    /// Extends <see cref="BaseViewModel"/> class.
    /// </summary>
    class ResultsViewModel : BaseViewModel
    {
        private readonly Settings _settings;

        #region Fields for binding
        private ScanResult _result;
        private Brush _hostColor;
        private BitmapImage _scanImage;

        private bool _isLoading = false;
        private int _currentProgress = 1;
        private int _maxProgress = 2;

        private bool _showOpened = true;
        private bool _showClosed = true;
        private bool _showNotChecked = true;
        private bool _showFiltered = true;
        private bool _showOpenedOrFiltered = true;

        /// <summary>Result of scanning.</summary>
        /// <value>The <see cref="Result"/> property gets/sets the value of the <see cref="ScanResult"/> field, <see cref="_result"/>.</value>
        public ScanResult Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged();
                OnPropertyChanged("Host");
                OnPropertyChanged("ScanDate");
            }
        }
        /// <summary>User-friendly host information.</summary>
        /// <value>The <see cref="Host"/> property gets the value of the <see cref="ScanResult.Host"/> field, <see cref="_result"/>.</value>
        public string Host => $"{_result.Host}";
        /// <summary>Color of host string.</summary>
        /// <value>The <see cref="HostColor"/> property gets/sets the value of the <see cref="Brush"/> field, <see cref="_hostColor"/>.</value>
        public Brush HostColor
        {
            get => _hostColor;
            set
            {
                _hostColor = value;
                OnPropertyChanged();
            }
        }
        /// <summary>Image in the border.</summary>
        /// <value>The <see cref="ScanImage"/> property gets/sets the value of the <see cref="BitmapImage"/> field, <see cref="_scanImage"/>.</value>
        public BitmapImage ScanImage
        {
            get => _scanImage;
            set
            {
                _scanImage = value;
                OnPropertyChanged();
            }
        }
        /// <summary>User-friendly scanned date information.</summary>
        /// <value>The <see cref="Host"/> property gets the value of the <see cref="ScanResult.ScanDate"/> field, <see cref="_result"/>.</value>
        public string ScanDate => $"Scanned at: {_result.ScanDate:G}";
        public bool IsValidHost => _result.HostIsValid;
        public bool IsNotValidHost => !_result.HostIsValid;

        /// <summary>If result not rendered yet.</summary>
        /// <value>The <see cref="IsLoading"/> property gets/sets the value of the <see cref="bool"/> field, <see cref="_isLoading"/>.</value>
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }
        /// <summary>Value of progress bar.</summary>
        /// <value>The <see cref="CurrentProgress"/> property gets/sets the value of the <see cref="int"/> field, <see cref="_currentProgress"/>.</value>
        public int CurrentProgress
        {
            get => _currentProgress;
            set
            {
                _currentProgress = value;
                OnPropertyChanged();
            }
        }
        /// <summary>Maximum of progress bar.</summary>
        /// <value>The <see cref="MaxProgress"/> property gets/sets the value of the <see cref="int"/> field, <see cref="_maxProgress"/>.</value>
        public int MaxProgress
        {
            get => _maxProgress;
            set
            {
                _maxProgress = value;
                OnPropertyChanged();
            }
        }

        /// <summary>Results filter. Show ports with Opened state.</summary>
        /// <value>The <see cref="ShowOpened"/> property gets/sets the value of the <see cref="bool"/> field, <see cref="_showOpened"/>.</value>
        public bool ShowOpened
        {
            get => _showOpened;
            set
            {
                if (!_isLoading)
                {
                    _showOpened = value;
                    OnPropertyChanged();
                    OnRenderRequest?.Invoke(_result.PortsAfterScan);
                }
            }
        }
        /// <summary>Results filter. Show ports with Closed state.</summary>
        /// <value>The <see cref="ShowClosed"/> property gets/sets the value of the <see cref="bool"/> field, <see cref="_showClosed"/>.</value>
        public bool ShowClosed
        {
            get => _showClosed;
            set
            {
                if (!_isLoading)
                {
                    _showClosed = value;
                    OnPropertyChanged();
                    OnRenderRequest?.Invoke(_result.PortsAfterScan);
                }
            }
        }
        /// <summary>Results filter. Show ports with "Not checked" state.</summary>
        /// <value>The <see cref="ShowNotChecked"/> property gets/sets the value of the <see cref="bool"/> field, <see cref="_showNotChecked"/>.</value>
        public bool ShowNotChecked
        {
            get => _showNotChecked;
            set
            {
                if (!_isLoading)
                {
                    _showNotChecked = value;
                    OnPropertyChanged();
                    OnRenderRequest?.Invoke(_result.PortsAfterScan);
                }
            }
        }
        /// <summary>Results filter. Show ports with Filtered state.</summary>
        /// <value>The <see cref="ShowFiltered"/> property gets/sets the value of the <see cref="bool"/> field, <see cref="_showFiltered"/>.</value>
        public bool ShowFiltered
        {
            get => _showFiltered;
            set
            {
                if (!_isLoading)
                {
                    _showFiltered = value;
                    OnPropertyChanged();
                    OnRenderRequest?.Invoke(_result.PortsAfterScan);
                }
                
            }
        }
        /// <summary>Results filter. Show ports with "Opened / Filtered" state.</summary>
        /// <value>The <see cref="ShowOpenedOrFiltered"/> property gets/sets the value of the <see cref="bool"/> field, <see cref="_showOpenedOrFiltered"/>.</value>
        public bool ShowOpenedOrFiltered
        {
            get => _showOpenedOrFiltered;
            set
            {
                if (!_isLoading)
                {
                    _showOpenedOrFiltered = value;
                    OnPropertyChanged();
                    OnRenderRequest?.Invoke(_result.PortsAfterScan);
                }
            }
        }


        public ICommand SaveResults => new BaseCommand(SaveResultsCommand);
        #endregion

        /// <summary>Render components again, when user changed filters.</summary>
        public event Action<Port[]> OnRenderRequest; 

        /// <summary>Base constructor.</summary>
        public ResultsViewModel() { }
        /// <summary>Create new view model with result information.</summary>
        /// <param name="result">Result information.</param>
        public ResultsViewModel(ScanResult result)
        {
            _settings = new Settings();

            Result = GetSortedResultBySettings(result);

            HostColor = _result.HostIsValid ?
                (Brush)Application.Current.MainWindow.FindResource("WhiteBrush")
                : (Brush)Application.Current.MainWindow.FindResource("RedBrush");
            ScanImage = _result.HostIsValid ?
                new BitmapImage(new Uri("/Looto;component/Images/scan_main.png", UriKind.Relative))
                : new BitmapImage(new Uri("/Looto;component/Images/scan_red.png", UriKind.Relative));
        }

        private void SaveResultsCommand(object parameter)
        {
            using (var dialog = new System.Windows.Forms.SaveFileDialog())
            {
                dialog.Filter = "json files (*.json)|*.json";
                dialog.DefaultExt = "json";
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.FileName))
                {
                    var jsonResult = JsonConvert.SerializeObject(_result);
                    File.WriteAllText(dialog.FileName, jsonResult);
                }
            }
        }

        /// <summary>Sorts results array by settings values from <see cref="_settings"/>.</summary>
        /// <param name="notSortedResult">Result with not sorted array of ports.</param>
        /// <returns>Result with sorted array of ports by settings.</returns>
        private ScanResult GetSortedResultBySettings(ScanResult notSortedResult)
        {
            ResultsSortingMode sortingMode = _settings.GetSettings().ResultsSortingMode;
            bool isDescSorting = _settings.GetSettings().ResultsIsDescSorting;

            if (sortingMode == ResultsSortingMode.ByPortValue)
                if (isDescSorting)
                    notSortedResult.PortsAfterScan = notSortedResult.PortsAfterScan.OrderByDescending(portInfo => portInfo.Value).ToArray();
                else
                    notSortedResult.PortsAfterScan = notSortedResult.PortsAfterScan.OrderBy(portInfo => portInfo.Value).ToArray();
            else if (sortingMode == ResultsSortingMode.ByPortState)
                if (isDescSorting)
                    notSortedResult.PortsAfterScan = notSortedResult.PortsAfterScan.OrderByDescending(portInfo => portInfo.State).ToArray();
                else
                    notSortedResult.PortsAfterScan = notSortedResult.PortsAfterScan.OrderBy(portInfo => portInfo.State).ToArray();
            else if (sortingMode == ResultsSortingMode.ByPortProtocol)
                if (isDescSorting)
                    notSortedResult.PortsAfterScan = notSortedResult.PortsAfterScan.OrderByDescending(portInfo => portInfo.Protocol).ToArray();
                else
                    notSortedResult.PortsAfterScan = notSortedResult.PortsAfterScan.OrderBy(portInfo => portInfo.Protocol).ToArray();

            return notSortedResult;
        }
    }
}
