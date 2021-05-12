using System;
using System.Threading.Tasks;

namespace Looto.Models.HostScanner
{
    /// <summary>Base host scanner interface.</summary>
    public interface IHostScanner
    {
        /// <summary>Calls when one host was scanned.</summary>
        event Action<int, int, HostData> OnOneHostWasScanned;
        /// <summary>Calls when all hosts has been scanned.</summary>
        event Action<HostData[]> OnScanEnding;

        /// <summary>Hosts, that will be checked</summary>
        HostData[] Hosts { get; set; }
        /// <summary>Count of all hosts that will be scanned.</summary>
        int HostsCount { get; }


        /// <summary>Configure host scanner with custom settings.</summary>
        /// <param name="config">Custom settings.</param>
        void Configure(IHostScannerConfig config);

        /// <summary>Scan async all hosts.</summary>
        Task ScanAllAsync();
        /// <summary>Abort current scanning.</summary>
        void Abort();
    }
}
