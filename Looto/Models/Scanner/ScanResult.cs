using System;
using System.Net;

namespace Looto.Models.Scanner
{
    /// <summary>Struct for storage data about scanning result.</summary>
    public struct ScanResult
    {
        /// <summary>Host, that was scanned.</summary>
        public IPAddress Host { get; set; }
        /// <summary>Time of scanning.</summary>
        public DateTime ScanDate { get; set; }
        /// <summary>
        /// If user uses undefined protocol.<br/>
        /// Feature for future.
        /// </summary>
        public bool UndefinedProtocols => false;
        /// <summary>
        /// All data about all ports:<br/>
        /// Numeric value;<br/>
        /// Protocol;<br/>
        /// State(Opened/Closed/Not checked).
        /// </summary>
        public Port[] PortsAfterScan { get; set; }

        /// <summary>Create new instance of result</summary>
        /// <param name="host">Host, that was scanned.</param>
        /// <param name="scanDate">Time of scanning.</param>
        /// <param name="results">All data about all ports.</param>
        public ScanResult(IPAddress host, DateTime scanDate, Port[] results)
        {
            Host = host;
            ScanDate = scanDate;
            PortsAfterScan = results;
        }
    }
}
