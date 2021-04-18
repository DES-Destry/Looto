using Looto.Models.PortScanner;
using System.Windows;
using System.Windows.Media;

namespace Looto.ViewModels
{
    /// <summary>
    /// View model for window with results.<br/>
    /// Extends <see cref="BaseViewModel"/> class.
    /// </summary>
    class ResultsViewModel : BaseViewModel
    {
        #region Fields for binding
        private readonly string _hostState;
        private ScanResult _result;
        private Brush _hostColor;

        private bool _isLoading = true;
        private int _currentProgress = 1;
        private int _maxProgress = 2;

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
        public string Host => $"Scanned host: {_result.Host} {_hostState}";
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
        /// <summary>User-friendly scanned date information.</summary>
        /// <value>The <see cref="Host"/> property gets the value of the <see cref="ScanResult.ScanDate"/> field, <see cref="_result"/>.</value>
        public string ScanDate => $"Scanned at: {_result.ScanDate:G}";

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
        #endregion

        /// <summary>Base constructor.</summary>
        public ResultsViewModel() { }
        /// <summary>Create new view model with result information.</summary>
        /// <param name="result">Result information.</param>
        public ResultsViewModel(ScanResult result)
        {
            Result = result;
            HostColor = _result.HostIsValid ?
                (Brush)Application.Current.MainWindow.FindResource("WhiteBrush")
                : (Brush)Application.Current.MainWindow.FindResource("RedBrush");

            _hostState = _result.HostIsValid ? string.Empty : "(DOESN'T EXISTS)";
        }
    }
}
