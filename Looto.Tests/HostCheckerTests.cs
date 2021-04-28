using Looto.Models.HostScanner;
using Xunit;

namespace Looto.Tests
{
    public class HostCheckerTests
    {
        [Fact]
        public void CheckHost_ShouldFindLocalhost()
        {
            string testHost = "localhost"; 

            bool actual = HostChecker.CheckHost(testHost);

            Assert.True(actual);
        }

        [Fact]
        public void CheckHost_ShouldNotFindLocalehostes()
        {
            string testHost = "localehostes";

            bool actual = HostChecker.CheckHost(testHost);

            Assert.False(actual);
        }

        [Fact]
        public void CheckHost_ShouldNotFindEmpty()
        {
            bool actual = HostChecker.CheckHost("");

            Assert.False(actual);
        }

        [Fact]
        public void CheckHost_ShouldNotFindNull()
        {
            bool actual = HostChecker.CheckHost(null);

            Assert.False(actual);
        }

        // If one of theese 3 tests are successfull - test are successful, because theese hosts can be fallen down.
        [Fact]
        public void CheckHost_ShouldFindOnePopularHost()
        {
            string googleHost = "google.com";
            string vkHost = "vk.com";
            string githubHost = "github.com";

            bool googleFinded = HostChecker.CheckHost(googleHost);
            bool vkFinded = HostChecker.CheckHost(vkHost);
            bool githubFinded = HostChecker.CheckHost(githubHost);

            bool actual = googleFinded || vkFinded || githubFinded;

            Assert.True(actual);
        }

        [Fact]
        public void GetLocalIP_ShouldReturnLocalIP()
        {
            string[] actual = HostChecker.GetLocalIPs();

            Assert.True(actual.Length > 0);
            Assert.StartsWith("192.168.", actual[0]);
        }
    }
}
