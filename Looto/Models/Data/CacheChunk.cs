using Looto.Models.PortScanner;
using System;
using System.Collections.Generic;

namespace Looto.Models.Data
{
    /// <summary>Object of cache data.</summary>
    [Serializable]
    public class CacheData
    {
        /// <summary>Life time of the one chunck.</summary>
        public TimeSpan ChunckLifetime { get; set; }
        /// <summary>Chuncks of results(cache).</summary>
        public List<ScanResult> Chuncks { get; set; }

        /// <summary>Create instance of cache data with defaults.</summary>
        public CacheData()
        {
            Chuncks = new List<ScanResult>();
            ChunckLifetime = TimeSpan.FromDays(3);
        }
    }
}
