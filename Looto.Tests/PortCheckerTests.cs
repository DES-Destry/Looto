using Looto.Models.PortScanner;
using System;
using Xunit;

namespace Looto.Tests
{
    public class PortCheckerTests
    {
        [Theory]
        [InlineData("google.com", 80, PortState.Opened)]
        [InlineData("google.com", 443, PortState.Opened)]
        [InlineData("google.com", 3000, PortState.Closed)]
        [InlineData("google.com", 8080, PortState.Closed)]
        public void CheckPort_ValidGoogleTcpPortCheck(string host, ushort port, PortState expect)
        {
            PortChecker checker = new PortChecker();
            checker.InstallHost(host);
            Port portToCheck = new Port(port, System.Net.Sockets.ProtocolType.Tcp);

            PortState actual = checker.CheckPort(portToCheck);

            Assert.Equal(expect, actual);
        }

        [Fact]
        public void CheckPort_ShouldThrowExceptionBecauseHostNull()
        {
            PortChecker checker = new PortChecker();
            Port portToCheck = new Port();

            Assert.Throws<ArgumentNullException>("_host", () => checker.CheckPort(portToCheck));
        }

        [Fact]
        public void CheckPort_ShouldSetOpenedBecauseTcpPortIsZero()
        {
            int expectPort = 0;
            PortState expectState = PortState.Opened;
            PortChecker checker = new PortChecker();
            checker.InstallHost("localhost");
            Port port = new Port();

            PortState actual = checker.CheckPort(port);

            Assert.Equal(expectPort, port.Value);
            Assert.Equal(expectState, actual);
        }
    }
}
