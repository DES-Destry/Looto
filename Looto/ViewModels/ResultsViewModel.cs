using Looto.Models.Scanner;

namespace Looto.ViewModels
{
    /// <summary>
    /// View model for window with results.<br/>
    /// Extends <see cref="BaseViewModel"/> class.
    /// </summary>
    class ResultsViewModel : BaseViewModel
    {
        #region Fields for binding
        private ScanResult _result;

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
        public string Host => $"Scanned host: {_result.Host}";
        /// <summary>User-friendly scanned date information.</summary>
        /// <value>The <see cref="Host"/> property gets the value of the <see cref="ScanResult.ScanDate"/> field, <see cref="_result"/>.</value>
        public string ScanDate => $"Scanned at: {_result.ScanDate:U}(UTC+0)";
        #endregion

        /// <summary>Base constructor.</summary>
        public ResultsViewModel() { }
        /// <summary>Create new view model with result information.</summary>
        /// <param name="result">Result information.</param>
        public ResultsViewModel(ScanResult result)
        {
            Result = result;
        }
    }
}
