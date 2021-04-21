using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace Looto.Models.HostScanner
{
    /// <summary>
    /// Extends <see cref="SimpleHostScanner"/>.
    /// Checks all possible hosts in the LAN.
    /// </summary>
    public class LANHostScanner : SimpleHostScanner
    {
        private const string LAN_IP_BASE = "192.168.";

        /// <summary>Create LAN scanner. Init all possible LAN hosts.</summary>
        public LANHostScanner()
        {
            try
            {
                string localIP = HostChecker.GetLocalIP();

                // localIP: 192.168.1.3 (For example)
                // predicate        ^
                // predicates in the different LANs not same.
                string predicate = localIP.Split('.')[2];

                List<HostData> toCheck = new List<HostData>();

                for (byte i = 0; i < byte.MaxValue; i++)
                    toCheck.Add(new HostData($"{LAN_IP_BASE}1.{i}", false));
                for (byte i = 0; i < byte.MaxValue; i++)
                    toCheck.Add(new HostData($"{LAN_IP_BASE}{predicate}.{i}", false));

                Hosts = toCheck.ToArray();
            }
            catch (PingException)
            {
                Hosts = new HostData[] { };
            }
        }
    }
}
