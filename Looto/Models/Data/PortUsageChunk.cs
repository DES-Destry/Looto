namespace Looto.Models.Data
{
    /// <summary>
    /// Data about port usage for one port. <br/>
    /// Data parse from https://en.wikipedia.org/wiki/List_of_TCP_and_UDP_port_numbers <br/>
    /// Data parse with ..\..\Scripts\portDataParser.js script.
    /// </summary>
    public struct PortUsageChunk
    {
        /// <summary>Numeric port value.</summary>
        public ushort Port { get; set; }
        /// <summary>How this port using.</summary>
        public string Description { get; set; }
        /// <summary>TCP usage for this description.</summary>
        public PortAssignationLevel TCPUsage { get; set; }
        /// <summary>UDP usage for this description.</summary>
        public PortAssignationLevel UDPUsage { get; set; }
        /// <summary>SCTP usage for this description.</summary>
        public PortAssignationLevel SCTPUsage { get; set; }
        /// <summary>DCCP usage for this description.</summary>
        public PortAssignationLevel DCCPUsage { get; set; }
    }
}
