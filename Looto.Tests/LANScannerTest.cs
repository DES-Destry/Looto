using Looto.Models.HostScanner;
using Xunit;

namespace Looto.Tests
{
    public class LANScannerTest
    {
        private HostData[] _results = new HostData[] { };

        [Fact]
        public async void ScanAll_ShouldFindCurrentMachineInLANList()
        {
            HostData thisMachineDevice = new HostData
            {
                Host = HostChecker.GetLocalIP(),
                Exists = true
            };

            IHostScanner scanner = new LANHostScanner();
            scanner.OnScanEnding += Scanner_OnScanEnding;

            await scanner.ScanAllAsync();

            Assert.True(_results.Length > 0);
            Assert.Contains(thisMachineDevice, _results);
        }

        private void Scanner_OnScanEnding(HostData[] results) => _results = results;
    }
}
