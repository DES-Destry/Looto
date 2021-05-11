using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Looto.Models.HostScanner
{
    /// <summary>Class for checking host on existance.</summary>
    public class HostChecker
    {
        /// <summary>Check host on existance.</summary>
        /// <param name="host">Host to check.</param>
        /// <returns>Boolean value. true - exists. false - don't exists.</returns>
        public static bool CheckHost(string host, IHostScannerConfig config = null)
        {
            if (host == string.Empty || host == null)
                return false;

            try
            {
                Ping hostCheck = new Ping();
                PingReply reply = hostCheck.Send(host, config?.HostCheckTimeout ?? 1000);

                if (reply.Status == IPStatus.Success)
                    return true;
                else return false;
            }
            catch (PingException)
            {
                return false;
            }
        }

        /// <summary>Get local IPv4 addresses of current device.</summary>
        /// <returns>IP addresses in the string format.</returns>
        public static string[] GetLocalIPs()
        {
            List<string> IPv4Hosts = new List<string>();
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    IPv4Hosts.Add(ip.ToString());
            }

            return IPv4Hosts.ToArray();
        }
    }
}
