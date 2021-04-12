using System;

namespace Looto.Models.Scanner
{
    /// <summary>Base scanner interface.</summary>
    interface IScanner
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
        void ScanAllAsync();
        /// <summary>Abort current scanning.</summary>
        void Abort();
    }
}
