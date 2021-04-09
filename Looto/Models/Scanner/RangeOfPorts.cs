using System.Collections.Generic;

namespace Looto.Models.Scanner
{
    /// <summary>Struct to present range of ports.</summary>
    public struct RangeOfPorts
    {
        /// <summary>First tcp port value.</summary>
        public ushort FromTcp { get; set; }
        /// <summary>Last tcp port value.</summary>
        public ushort ToTcp { get; set; }
        /// <summary>First udp port value.</summary>
        public ushort FromUdp { get; set; }
        /// <summary>Last udp port value.</summary>
        public ushort ToUdp { get; set; }
        /// <summary>Checks all struct values for correctness.</summary>
        public bool IsValid => IsValidRange();

        /// <summary>Generate array for correct scan in range scanner.</summary>
        /// <returns>Array of <see cref="Port"/></returns>
        /// <exception cref="RangeOfPortsException">Throws when struct values are not correct.</exception>
        public Port[] GetPortsArray()
        {
            if (!IsValid)
                throw new RangeOfPortsException("Range not valid for scanning", this);

            // [0] - From TCP (or UDP if TCP not defined)
            // [1] - To TCP   (or UDP if TCP not defined)
            // [2] - From UDP (can be null if UDP not defined)
            // [3] - To UDP   (can be null if UDP not defined)
            List<Port> portsRanges = new List<Port>();

            if (FromTcp != default(ushort) && ToTcp != default(ushort))
            {
                portsRanges.Add(new Port(FromTcp, System.Net.Sockets.ProtocolType.Tcp));
                portsRanges.Add(new Port(ToTcp, System.Net.Sockets.ProtocolType.Tcp));
            }

            if (FromUdp != default(ushort) && ToUdp != default(ushort))
            {
                portsRanges.Add(new Port(FromUdp, System.Net.Sockets.ProtocolType.Udp));
                portsRanges.Add(new Port(ToUdp, System.Net.Sockets.ProtocolType.Udp));
            }

            if (portsRanges.Count == 0)
                throw new RangeOfPortsException("Range not valid for scanning", this);

            return portsRanges.ToArray();
        }

        /// <summary>Check array of ports for correctness as range of ports.</summary>
        /// <param name="ports">Array to check.</param>
        /// <returns>Correctess of array.</returns>
        public static bool CheckArrayOnRangeCorrectness(Port[] ports)
        {
            if (ports.Length == 0)
                return false;

            RangeOfPorts rangeOfPorts = new RangeOfPorts();
            if (ports.Length == 2 || ports.Length == 4)
            {
                rangeOfPorts.FromTcp = ports[0].Value;
                rangeOfPorts.ToTcp = ports[1].Value;
            }
            else return false;

            if (ports.Length == 4)
            {
                rangeOfPorts.FromUdp = ports[2].Value;
                rangeOfPorts.ToUdp = ports[3].Value;
            }

            return rangeOfPorts.IsValid;
        }

        /// <summary>Checks all struct values for correctness.</summary>
        /// <returns>True if all values are correct.</returns>
        private bool IsValidRange()
        {
            bool validTcp = true;
            bool validUdp = true;
            byte notDefined = 0;

            if (FromTcp != default(ushort) && ToTcp != default(ushort))
                validTcp = FromTcp < ToTcp;
            else notDefined++;

            if (FromUdp != default(ushort) && ToUdp != default(ushort))
                validUdp = FromUdp < ToUdp;
            else notDefined++;

            // If not defined 2 times - input not valid.
            return (validTcp || validUdp) && notDefined != 2;
        }
    }
}
