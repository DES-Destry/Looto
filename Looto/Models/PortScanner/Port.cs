using System.Net.Sockets;

namespace Looto.Models.PortScanner
{
    /// <summary>Ports integer presentation with his protocol.</summary>
    public struct Port
    {
        /// <summary>Numeric value of port.</summary>
        public ushort Value { get; set; }
        /// <summary>Ports protocol type to scan.</summary>
        public ProtocolType Protocol { get; set; }
        /// <summary>Ports checking state.</summary>
        public PortState State { get; set; }

        /// <summary>Create new port instance with base parameters.</summary>
        /// <param name="value">Numeric presentation of port.</param>
        /// <param name="protocol">Ports protocol to scan.</param>
        public Port(ushort value, ProtocolType protocol)
        {
            Value = value;
            Protocol = protocol;
            State = PortState.NotChecked;
        }

        /// <summary>Change checking state of port.</summary>
        /// <param name="state">New port's state.</param>
        public void ChangeState(PortState state)
        {
            State = state;
        }
    }
}
