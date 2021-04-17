using System;

namespace Looto.Models.PortScanner
{
    [Serializable]
    class RangeOfPortsException : Exception
    {
        public RangeOfPorts Range { get; }

        public RangeOfPortsException() { }
        public RangeOfPortsException(string message) : base(message) { }
        public RangeOfPortsException(string message, Exception inner) : base(message, inner) { }
        public RangeOfPortsException(string message, RangeOfPorts range) : this(message)
        {
            Range = range;
        }
    }
}
