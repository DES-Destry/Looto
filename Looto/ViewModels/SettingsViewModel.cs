using Looto.Models.Data;
using System;

namespace Looto.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly SettingsData _defaultSettings = new SettingsData();
        private readonly Settings _settings;

        #region Fields for binding

        private SettingsData _data;

        private bool _isPortScanningCoresValid = true;
        private bool _isLANScanningCoresValid = true;
        private bool _isCacheLifetimeValid = true;
        private bool _isDataSendingTimeoutValid = true;
        private bool _isHostCheckTimeoutValid = true;
        private bool _isUDPTimeoutValid = true;

        private string _maxCoresInPortScanning;
        private string _maxCoresInLANScanning;
        private string _cacheLifetime;
        private string _dataSendingTimeout;
        private string _hostCheckTimeout;
        private string _UDPTimeout;


        /// <summary>Settings data for write/read from the file.</summary>
        /// <value>The <see cref="_data"/> property gets/sets the value of the <see cref="SettingsData"/> field, <see cref="_data"/>.</value>
        public SettingsData Data
        {
            get => _data;
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }

        /// <summary>Text of the settings element dynamic description.</summary>
        public string MaxCoresInPortScanningDescription => $"Сores count of proccessor for port scanning. If this value is more, then speed of port scanning will be faster. Minimum: 1. Maximum(default): {Environment.ProcessorCount}";
        /// <summary>Text of the settings element dynamic description.</summary>
        public string MaxCoresInLANScanningDescription => $"Сores count of proccessor for LAN scanning. If this value is more, then speed of port scanning will be faster. Minimum: 1. Maximum(default): {Environment.ProcessorCount}";

        /// <summary>Maximum processor cores count for port scanning.</summary>
        /// <value>The <see cref="MaxCoresInPortScanning"/> property gets/sets the value of the <see cref="string"/> field, <see cref="_maxCoresInPortScanning"/>.</value>
        public string MaxCoresInPortScanning
        {
            get => _maxCoresInPortScanning;
            set
            {
                if (int.TryParse(value, out int parsedValue))
                {
                    IsPortScanningCoresValid = ValidateInputCores(parsedValue);

                    if (IsPortScanningCoresValid)
                        _data.MaximumCoresInPortScanning = parsedValue;
                }
                else IsPortScanningCoresValid = false;

                _maxCoresInPortScanning = value;
                OnPropertyChanged();
            }
        }
        /// <summary>Maximum processor cores count for LAN scanning.</summary>
        /// <value>The <see cref="MaxCoresInLANScanning"/> property gets/sets the value of the <see cref="string"/> field, <see cref="_maxCoresInLANScanning"/>.</value>
        public string MaxCoresInLANScanning
        {
            get => _maxCoresInLANScanning;
            set
            {
                if (int.TryParse(value, out int parsedValue))
                {
                    IsLANScanningCoresValid = ValidateInputCores(parsedValue);

                    if (IsLANScanningCoresValid)
                        _data.MaximumCoresInLANScanning = parsedValue;
                }
                else IsLANScanningCoresValid = false;

                _maxCoresInLANScanning = value;
                OnPropertyChanged();
            }
        }
        /// <summary>Cache lifetime settings. Expired cache chunks will delete.</summary>
        /// <value>The <see cref="CacheLifetime"/> property gets/sets the value of the <see cref="string"/> field, <see cref="_cacheLifetime"/>.</value>
        public string CacheLifetime
        {
            get => _cacheLifetime;
            set
            {
                if (int.TryParse(value, out int parsedValue))
                {
                    IsCacheLifetimeValid = ValidateInputOnNegativeValues(parsedValue);

                    if (IsCacheLifetimeValid)
                        _data.CacheLifeTime = parsedValue;
                }
                else IsCacheLifetimeValid = false;

                _cacheLifetime = value;
                OnPropertyChanged();
            }
        }
        /// <summary>Timeout for port scanning.</summary>
        /// <value>The <see cref="DataSendingTimeout"/> property gets/sets the value of the <see cref="string"/> field, <see cref="_dataSendingTimeout"/>.</value>
        public string DataSendingTimeout
        {
            get => _dataSendingTimeout;
            set
            {
                if (int.TryParse(value, out int parsedValue))
                {
                    IsDataSendingTimeoutValid = ValidateInputOnNegativeValues(parsedValue);

                    if (IsDataSendingTimeoutValid)
                        _data.DataSendingTimeout = parsedValue;
                }
                else IsDataSendingTimeoutValid = false;

                _dataSendingTimeout = value;
                OnPropertyChanged();
            }
        }
        /// <summary>Timeout for host scanning.</summary>
        /// <value>The <see cref="HostCheckTimeout"/> property gets/sets the value of the <see cref="string"/> field, <see cref="_hostCheckTimeout"/>.</value>
        public string HostCheckTimeout
        {
            get => _hostCheckTimeout;
            set
            {
                if (int.TryParse(value, out int parsedValue))
                {
                    IsHostCheckTimeoutValid = ValidateInputOnNegativeValues(parsedValue);

                    if (IsHostCheckTimeoutValid)
                        _data.HostCheckTimeout = parsedValue;
                }
                else IsHostCheckTimeoutValid = false;

                _hostCheckTimeout = value;
                OnPropertyChanged();
            }
        }
        /// <summary>Timeout for receiving data from UDP socket.</summary>
        /// <value>The <see cref="UDPTimeout"/> property gets/sets the value of the <see cref="string"/> field, <see cref="_UDPTimeout"/>.</value>
        public string UDPTimeout
        {
            get => _UDPTimeout;
            set
            {
                if (int.TryParse(value, out int parsedValue))
                {
                    IsUDPTimeoutValid = ValidateInputOnNegativeValues(parsedValue);

                    if (IsUDPTimeoutValid)
                        _data.UDPDataReceivingTimeout = parsedValue;
                }
                else IsUDPTimeoutValid = false;

                _UDPTimeout = value;
                OnPropertyChanged();
            }
        }

        /// <summary>Validness of <see cref="MaxCoresInPortScanning"/>. It change settings element appearance.</summary>
        /// <value>The <see cref="IsPortScanningCoresValid"/> property gets/sets the value of the <see cref="bool"/> field, <see cref="_isPortScanningCoresValid"/>.</value>
        public bool IsPortScanningCoresValid
        {
            get => _isPortScanningCoresValid;
            set
            {
                _isPortScanningCoresValid = value;
                OnPropertyChanged();
            }
        }
        /// <summary>Validness of <see cref="MaxCoresInLANScanning"/>. It change settings element appearance.</summary>
        /// <value>The <see cref="IsLANScanningCoresValid"/> property gets/sets the value of the <see cref="bool"/> field, <see cref="_isLANScanningCoresValid"/>.</value>
        public bool IsLANScanningCoresValid
        {
            get => _isLANScanningCoresValid;
            set
            {
                _isLANScanningCoresValid = value;
                OnPropertyChanged();
            }
        }
        /// <summary>Validness of <see cref="CacheLifetime"/>. It change settings element appearance.</summary>
        /// <value>The <see cref="IsCacheLifetimeValid"/> property gets/sets the value of the <see cref="bool"/> field, <see cref="_isCacheLifetimeValid"/>.</value>
        public bool IsCacheLifetimeValid
        {
            get => _isCacheLifetimeValid; 
            set
            {
                _isCacheLifetimeValid = value;
                OnPropertyChanged();
            }
        }
        /// <summary>Validness of <see cref="DataSendingTimeout"/>. It change settings element appearance.</summary>
        /// <value>The <see cref="IsDataSendingTimeoutValid"/> property gets/sets the value of the <see cref="bool"/> field, <see cref="_isDataSendingTimeoutValid"/>.</value>
        public bool IsDataSendingTimeoutValid
        {
            get => _isDataSendingTimeoutValid;
            set
            {
                _isDataSendingTimeoutValid = value;
                OnPropertyChanged();
            }
        }
        /// <summary>Validness of <see cref="HostCheckTimeout"/>. It change settings element appearance.</summary>
        /// <value>The <see cref="IsHostCheckTimeoutValid"/> property gets/sets the value of the <see cref="bool"/> field, <see cref="_isHostCheckTimeoutValid"/>.</value>
        public bool IsHostCheckTimeoutValid
        {
            get => _isHostCheckTimeoutValid;
            set
            {
                _isHostCheckTimeoutValid = value;
                OnPropertyChanged();
            }
        }
        /// <summary>Validness of <see cref="UDPTimeout"/>. It change settings element appearance.</summary>
        /// <value>The <see cref="IsUDPTimeoutValid"/> property gets/sets the value of the <see cref="bool"/> field, <see cref="_isUDPTimeoutValid"/>.</value>
        public bool IsUDPTimeoutValid
        {
            get => _isUDPTimeoutValid; 
            set
            {
                _isUDPTimeoutValid = value;
                OnPropertyChanged();
            }
        }
        /// <summary>Availableness for applying new settings. All inputs must be valid.</summary>
        /// <value>
        /// The <see cref="IsApplyButtonClickable"/> property gets the value of the <see cref="bool"/> fields: 
        /// <see cref="_isPortScanningCoresValid"/>, <see cref="_isLANScanningCoresValid"/>, <see cref="_isCacheLifetimeValid"/>, 
        /// <see cref="_isDataSendingTimeoutValid"/>, <see cref="_isHostCheckTimeoutValid"/>, <see cref="_isUDPTimeoutValid"/>.
        /// </value>
        public bool IsApplyButtonClickable =>
            _isPortScanningCoresValid
            && _isLANScanningCoresValid
            && _isCacheLifetimeValid
            && _isDataSendingTimeoutValid
            && _isHostCheckTimeoutValid
            && _isUDPTimeoutValid;

        #endregion

        /// <summary>Create new view model for <see cref="Views.SettingsWindow"/> view.</summary>
        public SettingsViewModel()
        {
            _settings = new Settings();

            Data = _settings.GetSettings();
            MaxCoresInPortScanning = _data.MaximumCoresInPortScanning.ToString();
            MaxCoresInLANScanning = _data.MaximumCoresInLANScanning.ToString();
            CacheLifetime = _data.CacheLifeTime.ToString();
            DataSendingTimeout = _data.DataSendingTimeout.ToString();
            HostCheckTimeout = _data.HostCheckTimeout.ToString();
            UDPTimeout = _data.UDPDataReceivingTimeout.ToString();
        }

        private void ApplyChanges(object parameter)
        {
            _settings.ChangeData(_data);
            _settings.Save();
        }

        private void CancelChanges(object parameter)
        {
            Data = _settings.GetSettings();
        }

        private void SetAllToDefault(object parameter)
        {
            Data = _defaultSettings;
        }

        /// <summary>Check input number on correctness - it must be not less than 1 and not more than cores count in the current machine.</summary>
        /// <param name="inputCores">User Input value.</param>
        /// <returns>true - if input correct.</returns>
        private bool ValidateInputCores(int? inputCores)
        {
            if (inputCores != null && inputCores >= 1 && inputCores <= Environment.ProcessorCount)
                return true;

            return false;
        }

        /// <summary>Check input number on correctness - it must be not less than 0.</summary>
        /// <param name="inputValue">User Input value.</param>
        /// <returns>true - if input correct.</returns>
        private bool ValidateInputOnNegativeValues(int? inputValue)
        {
            if (inputValue != null && inputValue >= 0)
                return true;

            return false;
        }
    }
}
