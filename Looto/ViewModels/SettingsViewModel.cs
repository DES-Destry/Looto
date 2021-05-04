using Looto.Models.Data;

namespace Looto.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly Settings _settings;

        #region Fields for binding

        private SettingsData _data;

        public SettingsData Data
        {
            get => _data;
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public SettingsViewModel()
        {
            _settings = new Settings();
            Data = _settings.GetSettings();
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
    }
}
