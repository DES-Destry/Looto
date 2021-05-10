namespace Looto.Models.HostScanner
{
    /// <summary>Configuration interface for host scanner.</summary>
    public interface IHostScannerConfig
    {
        /// <summary> Maximum count of cores in LAN scanning.</summary>
        int MaximumCoresInLANScanning { get; set; }
        /// <summary>Timeout value for data sending in host scanning in ms.</summary>
        int HostCheckTimeout { get; set; }
    }
}
