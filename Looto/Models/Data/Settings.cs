using Newtonsoft.Json;
using System.IO;

namespace Looto.Models.Data
{
    /// <summary>Class for working with settings json file.</summary>
    public class Settings
    {
        private readonly string _path = ".data\\settings.json";

        private SettingsData _data;

        /// <summary>
        /// Create new <see cref="Settings"/> instance with default path. <br/>
        /// Default path = ".data\settings.json"
        /// </summary>
        public Settings()
        {
            _data = GetDataFromFile();
        }

        /// <summary>Create new <see cref="Settings"/> instance with custom path.</summary>
        /// <param name="path">Custom path.</param>
        public Settings(string path)
        {
            _path = path;
            _data = GetDataFromFile();
        }

        /// <summary>Get data from current <see cref="Settings"/> instance.</summary>
        /// <returns><see cref="SettingsData"/> with data from current <see cref="Settings"/> instance.</returns>
        public SettingsData GetSettings()
        {
            return _data;
        }

        /// <summary>
        /// Change data in the <see cref="Settings"/> instance. <br/>
        /// Don't forget about <see cref="Save"/> method to write data to json file.
        /// </summary>
        /// <param name="data">Data to save.</param>
        public void ChangeData(SettingsData data)
        {
            _data = data;
        }

        /// <summary>
        /// Change data in the <see cref="Settings"/> instance to default <see cref="SettingsData"/> value.<br/>
        /// Don't forget about <see cref="Save"/> method to write data to json file.
        /// </summary>
        public void SetSettingsToDefault()
        {
            _data = new SettingsData();
        }

        /// <summary>Save all changes to the json file.</summary>
        public void Save()
        {
            WriteDataToFile();
        }

        /// <summary>
        /// Get data from file, that locat at <see cref="_path"/>.<br/>
        /// If file doesn't exists it return <see cref="SettingsData"/> with default values.
        /// </summary>
        /// <returns><see cref="SettingsData"/> with data from json file.</returns>
        private SettingsData GetDataFromFile()
        {
            try
            {
                string jsonData = File.ReadAllText(_path);
                return JsonConvert.DeserializeObject<SettingsData>(jsonData) ?? new SettingsData();
            }
            catch
            {
                return new SettingsData();
            }
        }

        /// <summary>Write content in <see cref="_data"/> to json file, that locate at <see cref="_path"/></summary>
        private void WriteDataToFile()
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(_data);
                File.WriteAllText(_path, jsonData);
            }
            catch { }
        }
    }
}
