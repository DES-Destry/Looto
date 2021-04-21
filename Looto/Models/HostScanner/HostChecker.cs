using System;
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
        /// <returns>Bollean value. true - exists. false - don't exists.</returns>
        public static bool CheckHost(string host)
        {
            if (host == string.Empty || host == null)
                return false;

            try
            {
                Ping hostCheck = new Ping();
                PingReply reply = hostCheck.Send(host, 1000);

                if (reply.Status == IPStatus.Success)
                    return true;
                else return false;
            }
            catch (PingException)
            {
                return false;
            }
        }

        /// <summary>Get local IPv4 address of current device.</summary>
        /// <returns>IP address in the string format.</returns>
        /// <exception cref="PingException">Throws when device not connected to the LAN.</exception>
        public static string GetLocalIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            }

            throw new PingException("No network adapters with an IPv4 address in the system!");
        }
    }
}
