using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Looto.Models.Scanner
{
    /// <summary>Have methods for check port for Opened/Closed state.</summary>
    class PortChecker
    {
        private readonly byte[] _message;

        private IPAddress _host;
        private Socket _socket;
        private SocketType _socketType;
        private IPEndPoint _endPoint;

        /// <summary>Create new instance of checker.</summary>
        public PortChecker()
        {
            _message = Encoding.ASCII.GetBytes("Scanning...");
        }

        /// <summary>Check port for Opened/Closed state.</summary>
        /// <param name="port">Port parameters</param>
        /// <exception cref="ArgumentNullException">If <see cref="InstallHost(IPAddress)"/> method wasn't executed and <see cref="_host"/> equals null.</exception>
        /// <returns><see cref="PortState"/> enum value.</returns>
        public PortState CheckPort(Port port)
        {
            //Null check
            if (_host == null)
                throw new ArgumentNullException(nameof(_host), "Host to check not initialized.");

            PortState result = PortState.NotChecked;

            // Configurate socket parameters.
            _socketType = port.Protocol == ProtocolType.Tcp ? SocketType.Stream : SocketType.Dgram;
            _socket = new Socket(_host.AddressFamily, _socketType, port.Protocol);
            _endPoint = new IPEndPoint(_host, port.Value);

            try
            {
                // Try to send content
                _socket.Connect(_endPoint);
                _socket.Send(_message);
                _socket.Shutdown(SocketShutdown.Both);

                // If sending are successful - port opened
                result = PortState.Opened;
            }
            catch (SocketException)
            {
                result = PortState.Closed;
            }
            finally
            {
                _socket.Close();
            }

            return result;
        }

        /// <summary>Install host for cheking.</summary>
        /// <param name="host">Host for cheking.</param>
        public void InstallHost(IPAddress host)
        {
            _host = host;
        }
    }
}
