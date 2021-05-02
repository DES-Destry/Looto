using System;

namespace Looto.Models.Data
{
    /// <summary>Class for settings json file data presentation.</summary>
    public class SettingsData
    {
        private const ResultsRenderMode DEFAULT_RENDER_MODE = ResultsRenderMode.Full;
        private const ResultsSortingMode DEFAULT_SORTING_MODE = ResultsSortingMode.ByPortValue;

        private const int DEFAULT_CACHE_LIFETIME = 3;
        private const int DEFAULT_DATA_SENDING_TIMEOUT = 2500;
        private const int DEFAULT_HOST_CHECK_TIMEOUT = 1000;
        private const int DEFAULT_DATA_RECEIVING_TIMEOUT = 2500;

        private static readonly int DEFAULT_PORT_SCANNING_CORES = Environment.ProcessorCount;
        private static readonly int DEFAULT_HOST_SCANNING_CORES = Environment.ProcessorCount;


        /// <summary><see cref="Looto.Views.ResultsWindow"/> render mode.</summary>
        public ResultsRenderMode ResultsRenderMode { get; set; } = DEFAULT_RENDER_MODE;

        /// <summary><see cref="Looto.Views.ResultsWindow"/> sorting mode.</summary>
        public ResultsSortingMode ResultsSortingMode { get; set; } = DEFAULT_SORTING_MODE;

        /// <summary>Maximum count of cores in port scanning<./summary>
        public int MaximumCoresInPortScanning { get; set; } = DEFAULT_PORT_SCANNING_CORES;

        /// <summary>Maximum count of cores in LAN scanning.</summary>
        public int MaximumCoresInLANScanning { get; set; } = DEFAULT_HOST_SCANNING_CORES;

        /// <summary>Cache chunck lifetime in days.</summary>
        public int CacheLifeTime { get; set; } = DEFAULT_CACHE_LIFETIME;

        /// <summary>Timeout value for data sending in port scanning in ms.</summary>
        public int DataSendingTimeout { get; set; } = DEFAULT_DATA_SENDING_TIMEOUT;

        /// <summary>Timeout value for data sending in host scanning in ms.</summary>
        public int HostCheckTimeout { get; set; } = DEFAULT_HOST_CHECK_TIMEOUT;

        /// <summary>Timeout value for receiving data from UDP port in ms.</summary>
        public int UDPDataReceivingTimeout { get; set; } = DEFAULT_DATA_RECEIVING_TIMEOUT;
    }
}
