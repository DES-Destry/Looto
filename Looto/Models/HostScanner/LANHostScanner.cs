using System.Collections.Generic;

namespace Looto.Models.HostScanner
{
    /// <summary>
    /// Extends <see cref="SimpleHostScanner"/>.
    /// Checks all possible hosts in the LAN.
    /// </summary>
    class LANHostScanner : SimpleHostScanner
    {
        private const string LAN_IP_BASE = "192.168.1.";

        /// <summary>Create LAN scanner. Init all possible LAN hosts.</summary>
        public LANHostScanner()
        {
            List<HostData> toCheck = new List<HostData>();

            for (byte i = 0; i <= byte.MaxValue; i++)
                toCheck.Add(new HostData(LAN_IP_BASE + i.ToString(), false));

            Hosts = toCheck.ToArray();
        }
    }
}
