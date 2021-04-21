using System;
using System.Threading.Tasks;

namespace Looto.Models.PortScanner
{
    /// <summary>Base port scanner interface.</summary>
    public interface IScanner
    {
        /// <summary>Calls when one port was scanned.</summary>
        event Action<int, int> OnOnePortWasScanned;
        /// <summary>Calls when all port has been scanned.</summary>
        event Action<ScanResult> OnScanEnding;

        /// <summary>Host, that will be scanned.</summary>
        string Host { get; set; }
        /// <summary>Ports, that will be checked</summary>
        Port[] Ports { get; set; }
        /// <summary>Count of all ports that will be scanned.</summary>
        int PortsCount { get; }

        /// <summary>Scan async all of ports in host.</summary>
        Task ScanAllAsync();
        /// <summary>Abort current scanning.</summary>
        void Abort();
    }
}
