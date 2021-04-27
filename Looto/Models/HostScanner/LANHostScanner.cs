using System.Collections.Generic;

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
            string[] localIPs = HostChecker.GetLocalIPs();

            List<string> predicates = new List<string>();

            foreach (var ip in localIPs)
            {
                // ip: 192.168.1.3 (For example)
                // predicate   ^
                // predicates in the different LANs not same.
                predicates.Add(ip.Split('.')[2]);
            }

            List<HostData> toCheck = new List<HostData>();

            for (byte i = 0; i < byte.MaxValue; i++)
                for (int j = 0; j < predicates.Count; j++)
                    toCheck.Add(new HostData($"{LAN_IP_BASE}{predicates[j]}.{i}", false));


            Hosts = toCheck.ToArray();
        }
    }
}
