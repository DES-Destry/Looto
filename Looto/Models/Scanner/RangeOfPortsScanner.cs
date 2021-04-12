using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Looto.Models.Scanner
{
    /// <summary>
    /// <see cref="IScanner"/> interface implementation.<br/>
    /// Checks an range of ports for Opened/Closed state.
    /// </summary>
    class RangeOfPortsScanner : IScanner
    {
        private readonly PortChecker _checker;
        private int _scannedPortsCount;
        private bool _aborted = false;


        public string Host { get; set; }
        public Port[] Ports { get; set; }

        public int PortsCount
        {
            get
            {
                if (!RangeOfPorts.CheckArrayOnRangeCorrectness(Ports))
                    throw new RangeOfPortsException("Ports array are not correct.");

                int count = 0;
                if (Ports.Length == 2 || Ports.Length == 4)
                    count += Ports[1].Value - Ports[0].Value + 1;
                if (Ports.Length == 4)
                    count += Ports[3].Value - Ports[2].Value + 1;

                return count;
            }
        }

        public event Action<int, int> OnOnePortWasScanned;
        public event Action<ScanResult> OnScanEnding;

        /// <summary>Create new scanner instance.</summary>
        public RangeOfPortsScanner()
        {
            _checker = new PortChecker();
        }

        /// <summary>Scan all of ports in host.</summary>
        /// <exception cref="ArgumentNullException">Throws when <see cref="Host"/> or <see cref="Ports"/> was equals null.</exception>
        /// <exception cref="RangeOfPortsException">Throws when <see cref="Ports"/> parameter not looks like a range.</exception>
        public async void ScanAllAsync()
        {
            if (Host == null)
                throw new ArgumentNullException(nameof(Host), "Host value was equals null.");
            if (Ports == null)
                throw new ArgumentNullException(nameof(Ports), "Ports value was equals null.");
            if (!RangeOfPorts.CheckArrayOnRangeCorrectness(Ports))
                throw new RangeOfPortsException("Ports array are not correct.");

            _scannedPortsCount = 0;
            _checker.InstallHost(Host);

            List<Port> results = new List<Port>();

            if (Ports.Length == 2 || Ports.Length == 4)
            {
                Port[] scannedPorts = await ScanRange(Ports[0], Ports[1]);
                results.AddRange(scannedPorts);
            }
            if (Ports.Length == 4)
            {
                Port[] scannedPorts = await ScanRange(Ports[2], Ports[3]);
                results.AddRange(scannedPorts);
            }

            OnScanEnding?.Invoke(new ScanResult(Host, DateTime.Now, results.ToArray()));
            _scannedPortsCount = 0;
            _aborted = false;
        }

        public void Abort()
        {
            _aborted = true;
        }

        /// <summary>Scan range of ports.</summary>
        /// <param name="from">First port.</param>
        /// <param name="to">Last port.</param>
        /// <returns>Array with scan results</returns>
        /// <exception cref="RangeOfPortsException">Throws when from and to parameters not looks like a range.</exception>
        private async Task<Port[]> ScanRange(Port from, Port to)
        {
            if (from.Value >= to.Value && from.Protocol == to.Protocol)
                throw new RangeOfPortsException("Ports array are not correct.");

            List<Port> result = new List<Port>();
            ushort currentPort = from.Value;
            ProtocolType protocol = from.Protocol;

            await Task.Run(() =>
            {
                while (currentPort <= to.Value)
                {
                    Port portToScan = new Port(currentPort, protocol);
                    if (!_aborted)
                        portToScan.ChangeState(_checker.CheckPort(portToScan));

                    result.Add(portToScan);

                    currentPort++;
                    _scannedPortsCount++;
                    OnOnePortWasScanned?.Invoke(PortsCount, _scannedPortsCount);
                }
            });

            return result.ToArray();
        }
    }
}
