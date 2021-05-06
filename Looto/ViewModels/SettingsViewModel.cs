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


        public SettingsData Data
        {
            get => _data;
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }

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

        public bool IsPortScanningCoresValid
        {
            get => _isPortScanningCoresValid;
            set
            {
                _isPortScanningCoresValid = value;
                OnPropertyChanged();
            }
        }
        public bool IsLANScanningCoresValid
        {
            get => _isLANScanningCoresValid;
            set
            {
                _isLANScanningCoresValid = value;
                OnPropertyChanged();
            }
        }
        public bool IsCacheLifetimeValid
        {
            get => _isCacheLifetimeValid; 
            set
            {
                _isCacheLifetimeValid = value;
                OnPropertyChanged();
            }
        }
        public bool IsDataSendingTimeoutValid
        {
            get => _isDataSendingTimeoutValid;
            set
            {
                _isDataSendingTimeoutValid = value;
                OnPropertyChanged();
            }
        }
        public bool IsHostCheckTimeoutValid
        {
            get => _isHostCheckTimeoutValid;
            set
            {
                _isHostCheckTimeoutValid = value;
                OnPropertyChanged();
            }
        }
        public bool IsUDPTimeoutValid
        {
            get => _isUDPTimeoutValid; 
            set
            {
                _isUDPTimeoutValid = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public SettingsViewModel()
        {
            _settings = new Settings();

            Data = _settings.GetSettings();
            MaxCoresInPortScanning = _data.MaximumCoresInPortScanning.ToString();
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
            Data = new SettingsData();
        }

        private bool ValidateInputCores(int? inputCores)
        {
            if (inputCores != null && inputCores >= 1 && inputCores <= Environment.ProcessorCount)
                return true;

            return false;
        }

        private bool ValidateInputOnNegativeValues(int? inputValue)
        {
            if (inputValue != null && inputValue >= 0)
                return true;

            return false;
        }
    }
}
