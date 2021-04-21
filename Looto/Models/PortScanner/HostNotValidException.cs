using System;

namespace Looto.Models.PortScanner
{
    [Serializable]
    public class HostNotValidException : Exception
    {
        public string Host { get; }

        public HostNotValidException() { }
        public HostNotValidException(string message) : base(message) { }
        public HostNotValidException(string message, Exception inner) : base(message, inner) { }
        public HostNotValidException(string message, string host) : this(message)
        {
            Host = host;
        }
    }
}
