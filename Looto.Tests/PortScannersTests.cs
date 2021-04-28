using Looto.Models.PortScanner;
using System;
using Xunit;

namespace Looto.Tests
{
    public class PortScannersTests
    {
        private ScanResult _results = new ScanResult();
        private readonly Port[] _multiplePorts = new Port[]
        {
            new Port(80, System.Net.Sockets.ProtocolType.Tcp),
            new Port(443, System.Net.Sockets.ProtocolType.Tcp),
            new Port(3000, System.Net.Sockets.ProtocolType.Tcp),
            new Port(8080, System.Net.Sockets.ProtocolType.Tcp),
        };
        private readonly RangeOfPorts _validRange = new RangeOfPorts
        {
            FromTcp = 20,
            ToTcp = 25
        };


        [Fact]
        public async void ScanAllMultiple_ShouldMultipleScanPorts()
        {
            string host = "localhost";
            Port[] ports = _multiplePorts;

            IScanner scanner = new MultipleScanner
            {
                Host = host,
                Ports = ports
            };
            scanner.OnScanEnding += Scanner_OnScanEnding;

            await scanner.ScanAllAsync();

            Assert.True(_results.HostIsValid);
            foreach (var result in _results.PortsAfterScan)
            {
                Assert.True(result.State == PortState.Opened || result.State == PortState.Closed);
            }
        }

        [Fact]
        public async void ScanAllMultiple_ShouldDoNothingBecausePortsAreEmpty()
        {
            string host = "localhost";
            Port[] ports = new Port[] { };

            IScanner scanner = new MultipleScanner
            {
                Host = host,
                Ports = ports
            };
            scanner.OnScanEnding += Scanner_OnScanEnding;

            await scanner.ScanAllAsync();

            Assert.True(_results.HostIsValid);
            Assert.True(_results.PortsAfterScan.Length == 0);
        }

        [Fact]
        public async void ScanAllMultiple_ShouldDoNothingBecauseHostAreEmpty()
        {
            string host = "";
            Port[] ports = new Port[] { };

            IScanner scanner = new MultipleScanner
            {
                Host = host,
                Ports = ports
            };
            scanner.OnScanEnding += Scanner_OnScanEnding;

            await scanner.ScanAllAsync();

            Assert.False(_results.HostIsValid);
        }

        [Fact]
        public async void ScanAllMultiple_ShouldThrowPortsNullException()
        {
            string host = "localhost";
            Port[] ports = null;

            IScanner scanner = new MultipleScanner
            {
                Host = host,
                Ports = ports
            };
            scanner.OnScanEnding += Scanner_OnScanEnding;

            await Assert.ThrowsAsync<ArgumentNullException>("Ports", async () => await scanner.ScanAllAsync());
        }

        [Fact]
        public async void ScanAllMultiple_ShouldThrowHostNullException()
        {
            string host = null;
            Port[] ports = _multiplePorts;

            IScanner scanner = new MultipleScanner
            {
                Host = host,
                Ports = ports
            };
            scanner.OnScanEnding += Scanner_OnScanEnding;

            await Assert.ThrowsAsync<ArgumentNullException>("Host", async () => await scanner.ScanAllAsync());
        }

        [Fact]
        public async void ScanAllRange_ShouldScanRangeOfPorts()
        {
            string host = "localhost";
            RangeOfPorts rangeOfPorts = _validRange;

            IScanner scanner = new RangeOfPortsScanner
            {
                Host = host,
                Ports = rangeOfPorts.GetPortsArray(),
            };
            scanner.OnScanEnding += Scanner_OnScanEnding;

            await scanner.ScanAllAsync();

            Assert.True(_results.HostIsValid);
            foreach (var result in _results.PortsAfterScan)
            {
                Assert.True(result.State == PortState.Opened || result.State == PortState.Closed);
            }
        }

        [Fact]
        public async void ScanAllRange_ShouldThrowRangeExceptionBecausePortsNotInitialized()
        {
            string host = "localhost";
            RangeOfPorts rangeOfPorts = new RangeOfPorts();

            await Assert.ThrowsAsync<RangeOfPortsException>(async () =>
            {
                IScanner scanner = new RangeOfPortsScanner
                {
                    Host = host,
                    Ports = rangeOfPorts.GetPortsArray(),
                };
                scanner.OnScanEnding += Scanner_OnScanEnding;

                await scanner.ScanAllAsync();
            });
        }

        [Fact]
        public async void ScanAllRange_ShouldThrowRangeExceptionBecausePortsIncorrect()
        {
            string host = "localhost";
            RangeOfPorts rangeOfPorts = new RangeOfPorts
            {
                FromTcp = 20,
                ToTcp = 15,
            };

            await Assert.ThrowsAsync<RangeOfPortsException>(async () =>
            {
                IScanner scanner = new RangeOfPortsScanner
                {
                    Host = host,
                    Ports = rangeOfPorts.GetPortsArray(),
                };
                scanner.OnScanEnding += Scanner_OnScanEnding;

                await scanner.ScanAllAsync();
            });
        }

        [Fact]
        public async void ScanAllRange_ShouldDoNothingBecauseHostAreEmpty()
        {
            string host = "";
            RangeOfPorts rangeOfPorts = _validRange;

            IScanner scanner = new RangeOfPortsScanner
            {
                Host = host,
                Ports = rangeOfPorts.GetPortsArray(),
            };
            scanner.OnScanEnding += Scanner_OnScanEnding;

            await scanner.ScanAllAsync();

            Assert.False(_results.HostIsValid);
        }

        [Fact]
        public async void ScanAllRange_ShouldThrowPortsNullException()
        {
            string host = "localhost";

            IScanner scanner = new RangeOfPortsScanner
            {
                Host = host,
                Ports = null,
            };
            scanner.OnScanEnding += Scanner_OnScanEnding;

            await Assert.ThrowsAsync<ArgumentNullException>("Ports", async () => await scanner.ScanAllAsync());
        }

        [Fact]
        public async void ScanAllRange_ShouldThrowHostNullException()
        {
            string host = null;
            RangeOfPorts rangeOfPorts = _validRange;

            IScanner scanner = new RangeOfPortsScanner
            {
                Host = host,
                Ports = rangeOfPorts.GetPortsArray(),
            };
            scanner.OnScanEnding += Scanner_OnScanEnding;

            await Assert.ThrowsAsync<ArgumentNullException>("Host", async () => await scanner.ScanAllAsync());
        }

        private void Scanner_OnScanEnding(ScanResult results) => _results = results;
    }
}
