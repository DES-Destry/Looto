using Looto.Models.HostScanner;
using Looto.Models.PortScanner;
using System;

namespace Looto.Models.Data
{
    /// <summary>
    /// Class for settings json file data presentation. <br/>
    /// Implemets <see cref="IPortScannerConfig"/> and <see cref="IHostScannerConfig"/> interfaces.
    /// </summary>
    public class SettingsData : IPortScannerConfig, IHostScannerConfig
    {
        private const ResultsRenderMode DEFAULT_RENDER_MODE = ResultsRenderMode.Full;
        private const ResultsSortingMode DEFAULT_SORTING_MODE = ResultsSortingMode.ByPortValue;

        private const bool DEFAULT_IS_DESC_SORTING = false;

        private const int DEFAULT_CACHE_LIFETIME = 3;
        private const int DEFAULT_DATA_SENDING_TIMEOUT = 2500;
        private const int DEFAULT_HOST_CHECK_TIMEOUT = 1000;
        private const int DEFAULT_DATA_RECEIVING_TIMEOUT = 2500;

        private static readonly int DEFAULT_PORT_SCANNING_CORES = Environment.ProcessorCount;
        private static readonly int DEFAULT_HOST_SCANNING_CORES = Environment.ProcessorCount;


        /// <summary>
        /// <see cref="Looto.Views.ResultsWindow"/> render mode.<br/>
        /// Default value = <see cref="ResultsRenderMode.Full"/>.
        /// </summary>
        public ResultsRenderMode ResultsRenderMode { get; set; } = DEFAULT_RENDER_MODE;

        /// <summary>
        /// <see cref="Looto.Views.ResultsWindow"/> sorting mode. It will affect on data of result files.<br/>
        /// Default value = <see cref="ResultsSortingMode.ByPortValue"/>.
        /// </summary>
        public ResultsSortingMode ResultsSortingMode { get; set; } = DEFAULT_SORTING_MODE;

        /// <summary>
        /// Invert results sorting. It will affect on data of result files.<br/>
        /// Default value = false.
        /// </summary>
        public bool ResultsIsDescSorting { get; set; } = DEFAULT_IS_DESC_SORTING;

        /// <summary>
        /// Maximum count of cores in port scanning.<br/>
        /// Defalult value = max cores count - <see cref="Environment.ProcessorCount"/>.
        /// </summary>
        public int MaximumCoresInPortScanning { get; set; } = DEFAULT_PORT_SCANNING_CORES;

        /// <summary>
        /// Maximum count of cores in LAN scanning.<br/>
        /// Defalult value = max cores count - <see cref="Environment.ProcessorCount"/>.
        /// </summary>
        public int MaximumCoresInLANScanning { get; set; } = DEFAULT_HOST_SCANNING_CORES;

        /// <summary>
        /// Cache chunck lifetime in days.<br/>
        /// Default value = 3.
        /// </summary>
        public int CacheLifeTime { get; set; } = DEFAULT_CACHE_LIFETIME;

        /// <summary>
        /// Timeout value for data sending in port scanning in ms.<br/>
        /// Default value = 2500.
        /// </summary>
        public int DataSendingTimeout { get; set; } = DEFAULT_DATA_SENDING_TIMEOUT;

        /// <summary>
        /// Timeout value for data sending in host scanning in ms.<br/>
        /// Default value = 1000.
        /// </summary>
        public int HostCheckTimeout { get; set; } = DEFAULT_HOST_CHECK_TIMEOUT;

        /// <summary>
        /// Timeout value for receiving data from UDP port in ms.<br/>
        /// Default value = 2500.
        /// </summary>
        public int UDPDataReceivingTimeout { get; set; } = DEFAULT_DATA_RECEIVING_TIMEOUT;
    }
}
