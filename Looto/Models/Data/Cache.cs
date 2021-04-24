using Looto.Models.PortScanner;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Looto.Models.Data
{
    /// <summary>Class for working with cache data from file.</summary>
    public class Cache
    {
        private const string DEFAULT_FILE_PATH = ".data\\.cache";

        private readonly BinaryFormatter _cacheFile = new BinaryFormatter();
        private readonly CacheData _data;
        private readonly string _filePath;

        /// <summary>
        /// Create new cache file edit instance with default path. <br/>
        /// Default path = ".data\\.cache".
        /// </summary>
        public Cache()
        {
            _filePath = DEFAULT_FILE_PATH;
            _data = GetDataFromFile();
        }

        /// <summary>Create new cache file edit instance with custom path.</summary>
        /// <param name="customPath">Custom cache file path.</param>
        public Cache(string customPath)
        {
            _filePath = customPath;
            _data = GetDataFromFile();
        }

        /// <summary>Add new collection with cache data to existed collection.</summary>
        /// <param name="newChuncks">Collection with data.</param>
        public void PushNewChuncks(List<ScanResult> newChuncks)
        {
            _data.Chuncks.AddRange(newChuncks);
        }

        /// <summary>Set new life time value for cache data.</summary>
        /// <param name="newLifeTime">New life time value.</param>
        public void ChangeChunksLifeTime(TimeSpan newLifeTime)
        {
            _data.ChunckLifetime = newLifeTime;
        }

        /// <summary>Save all changes to file.</summary>
        public void Save()
        {
            _data.Chuncks = _data.Chuncks.Where(chunck => chunck.ScanDate - DateTime.Now == _data.ChunckLifetime).ToList();

            using (FileStream fs = new FileStream(_filePath, FileMode.OpenOrCreate))
                _cacheFile.Serialize(fs, _data);
        }

        /// <summary>Get cache data from file.</summary>
        /// <returns><see cref="CacheData"/> object from file or new instance of <see cref="CacheData"/>.</returns>
        private CacheData GetDataFromFile()
        {
            using (FileStream fs = new FileStream(_filePath, FileMode.OpenOrCreate))
                return (CacheData)_cacheFile.Deserialize(fs) ?? new CacheData();
        }
    }
}
