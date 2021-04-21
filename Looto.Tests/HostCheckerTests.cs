using Looto.Models.HostScanner;
using Xunit;

namespace Looto.Tests
{
    public class HostCheckerTests
    {
        [Fact]
        public void CheckHost_ShouldFindLocalhost()
        {
            HostData data = new HostData
            {
                Host = "localhost"
            };

            bool actual = HostChecker.CheckHost(data);

            Assert.True(actual);
        }

        [Fact]
        public void CheckHost_ShouldNotFindLocalehostes()
        {
            HostData data = new HostData
            {
                Host = "localehostes"
            };

            bool actual = HostChecker.CheckHost(data);

            Assert.False(actual);
        }

        [Fact]
        public void CheckHost_ShouldNotFindEmpty()
        {
            HostData data = new HostData
            {
                Host = ""
            };

            bool actual = HostChecker.CheckHost(data);

            Assert.False(actual);
        }

        [Fact]
        public void CheckHost_ShouldNotFindNull()
        {
            HostData data = new HostData
            {
                Host = null
            };

            bool actual = HostChecker.CheckHost(data);

            Assert.False(actual);
        }

        // If one of theese 3 tests are successfull - test are successful, because theese hosts can be fallen down.
        [Fact]
        public void CheckHost_ShouldFindOnePopularHost()
        {
            HostData googleHost = new HostData
            {
                Host = "google.com"
            };
            HostData vkHost = new HostData
            {
                Host = "vk.com"
            };
            HostData githubHost = new HostData
            {
                Host = "github.com"
            };

            bool googleFinded = HostChecker.CheckHost(googleHost);
            bool vkFinded = HostChecker.CheckHost(vkHost);
            bool githubFinded = HostChecker.CheckHost(githubHost);

            bool actual = googleFinded || vkFinded || githubFinded;

            Assert.True(actual);
        }

        [Fact]
        public void GetLocalIP_ShouldReturnSomething()
        {
            string actual = HostChecker.GetLocalIP();

            Assert.True(actual.Length > 0);
            Assert.StartsWith("192.168.", actual);
        }
    }
}
