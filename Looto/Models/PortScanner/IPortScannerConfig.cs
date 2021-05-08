namespace Looto.Models.PortScanner
{
    /// <summary>Configuration interface for port scanner.</summary>
    public interface IPortScannerConfig
    {
        /// <summary>Maximum count of cores in port scanning.</summary>
        int MaximumCoresInPortScanning { get; set; }
        /// <summary>Timeout value for data sending in port scanning in ms.</summary>
        int DataSendingTimeout { get; set; }
        /// <summary>Timeout value for receiving data from UDP port in ms.</summary>
        int UDPDataReceivingTimeout { get; set; }
    }
}
