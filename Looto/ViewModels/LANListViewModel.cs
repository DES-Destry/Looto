using Looto.Models.Data;
using Looto.Models.HostScanner;
using System;

namespace Looto.ViewModels
{
    /// <summary>
    /// View model for window with list of available hosts in LAN.<br/>
    /// Extends <see cref="BaseViewModel"/> class.
    /// </summary>
    class LANListViewModel : BaseViewModel
    {
        private readonly IHostScanner _hostScanner;
        private readonly Settings _settings;

        #region Fields for binding
        private bool _isSearching = false;

        private int _maxSearchProgress = 1;
        private int _currentSearchProgress = 0;


        /// <summary>Need for show or hide progress bar.</summary>
        /// <value>The <see cref="IsSearching"/> property gets/sets the value of the <see cref="bool"/> field, <see cref="_isSearching"/>.</value>
        public bool IsSearching
        {
            get => _isSearching;
            set
            {
                _isSearching = value;
                OnPropertyChanged();
            }
        }

        /// <summary>Maximum value of progress bar.</summary>
        /// <value>The <see cref="MaxSearchProgress"/> property gets/sets the value of the <see cref="int"/> field, <see cref="_maxSearchProgress"/>.</value>
        public int MaxSearchProgress
        {
            get => _maxSearchProgress;
            set
            {
                _maxSearchProgress = value;
                OnPropertyChanged();
            }
        }
        /// <summary>Value of progress bar.</summary>
        /// <value>The <see cref="CurrentSearchProgress"/> property gets/sets the value of the <see cref="int"/> field, <see cref="_currentSearchProgress"/>.</value>
        public int CurrentSearchProgress
        {
            get => _currentSearchProgress;
            set
            {
                _currentSearchProgress = value;
                OnPropertyChanged();
            }
        }
        #endregion

        /// <summary>Calls, when one host was checked.</summary>
        public event Action<string> OnHostFinded;


        /// <summary>Create new view model for view.</summary>
        public LANListViewModel()
        {
            _settings = new Settings();

            _hostScanner = new LANHostScanner();
            _hostScanner.Configure(_settings.GetSettings());
            _hostScanner.OnOneHostWasScanned += HostScanned;
            _hostScanner.OnScanEnding += HostScanningEnded;

            _hostScanner.ScanAllAsync();
            IsSearching = true;
        }

        /// <summary>When one host was checked.</summary>
        /// <param name="dest">Count of hosts to check.</param>
        /// <param name="currentCount">Count of hosts, that already was checked.</param>
        /// <param name="hostData">Info about checked host. This is host string and hosts existance.</param>
        private void HostScanned(int dest, int currentCount, HostData hostData)
        {
            // Render only if host exists.
            if (hostData.Exists)
                OnHostFinded?.Invoke(hostData.Host);

            // Progress bar stats.
            MaxSearchProgress = dest;
            CurrentSearchProgress = currentCount;
        }

        /// <summary>When all hosts was checked.</summary>
        /// <param name="hostsResult"></param>
        private void HostScanningEnded(HostData[] hostsResult)
        {
            IsSearching = false;
        }
    }
}
