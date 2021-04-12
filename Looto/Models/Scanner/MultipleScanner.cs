using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Looto.Models.Scanner
{
    /// <summary>
    /// <see cref="IScanner"/> interface implementation.<br/>
    /// Checks an array of ports for Opened/Closed state.
    /// </summary>
    class MultipleScanner : IScanner
    {
        private readonly PortChecker _checker;
        private int _scannedPortsCount;
        private bool _aborted;


        public event Action<int, int> OnOnePortWasScanned;
        public event Action<ScanResult> OnScanEnding;

        public string Host { get; set; }
        public Port[] Ports { get; set; }
        public int PortsCount => Ports.Length;

        /// <summary>Create new scanner instance.</summary>
        public MultipleScanner()
        {
            _checker = new PortChecker();
        }

        /// <summary>Scan all of ports in host.</summary>
        /// <exception cref="ArgumentNullException">Thrown when <see cref="Host"/> or <see cref="Ports"/> was equals null.</exception>
        public async void ScanAllAsync()
        {
            if (Host == null)
                throw new ArgumentNullException(nameof(Host), "Host value was equals null.");
            if (Ports == null)
                throw new ArgumentNullException(nameof(Ports), "Ports value was equals null.");

            _scannedPortsCount = 0;
            _checker.InstallHost(Host);

            Port[] result = await IteratePortsAsync();
            OnScanEnding?.Invoke(new ScanResult(Host, DateTime.Now, result));
            _scannedPortsCount = 0;
            _aborted = false;
        }

        public void Abort()
        {
            _aborted = true;
        }

        /// <summary>Itearte all ports asynchronously.</summary>
        /// <returns>Result of iteration checking.</returns>
        private async Task<Port[]> IteratePortsAsync()
        {
            List<Port> result = new List<Port>();

            await Task.Run(() =>
            {
                foreach (Port port in Ports)
                {
                    try
                    {
                        if (!_aborted)
                            port.ChangeState(_checker.CheckPort(port));

                        result.Add(port);

                        _scannedPortsCount++;
                        OnOnePortWasScanned?.Invoke(PortsCount, _scannedPortsCount);
                    }
                    catch (ArgumentNullException) // If host not initializes in checker.
                    {
                        result = new List<Port>();
                        break;
                    }
                }
            });

            // After foreach loop in heap can store a lot of objects of Socket and IPEndPoint classes.
            GC.Collect();

            return result.ToArray();
        }
    }
}
