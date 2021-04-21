using Looto.Models.DebugTools;
using Looto.Models.HostScanner;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Looto.Models.PortScanner
{
    /// <summary>Have methods for check port for Opened/Closed state.</summary>
    public class PortChecker
    {
        private readonly byte[] _message;

        private string _host;
        private Socket _socket;
        private SocketType _socketType;

        /// <summary>Create new instance of checker.</summary>
        public PortChecker()
        {
            _message = Encoding.ASCII.GetBytes("Scanning...");
        }

        /// <summary>Check port for Opened/Closed state.</summary>
        /// <param name="port">Port parameters</param>
        /// <exception cref="ArgumentNullException">If <see cref="InstallHost(string)"/> method wasn't executed and <see cref="_host"/> equals null.</exception>
        /// <returns><see cref="PortState"/> enum value.</returns>
        public PortState CheckPort(Port port)
        {
            //Null check
            if (_host == null)
                throw new ArgumentNullException(nameof(_host), "Host to check not initialized.");

            PortState result;

            // Configurate socket parameters.
            _socketType = port.Protocol == ProtocolType.Tcp ? SocketType.Stream : SocketType.Dgram;
            _socket = new Socket(_socketType, port.Protocol);

            try
            {
                // Try to send content
                _socket.Connect(_host, port.Value);
                _socket.Send(_message);
                _socket.Shutdown(SocketShutdown.Both);

                // If sending are successful - port opened (not allowed for UDP)
                result = PortState.Opened;
            }
            catch (SocketException)
            {
                result = PortState.Closed;
            }
            catch (Exception ex)
            {
                result = PortState.NotChecked;
                Application.Current.Dispatcher.Invoke(new Error(ex).HandleError);
            }

            _socket.Close();
            return result;
        }

        /// <summary>Install host for cheking.</summary>
        /// <param name="host">Host for cheking.</param>
        public void InstallHost(string host)
        {
            _host = host;
        }

        /// <summary>Check host on existance.</summary>
        /// <param name="host">Host to check.</param>
        /// <exception cref="HostNotValidException">Throws when host doesn't exist.</exception>
        public async Task HostIsValidAsync(string host)
        {
            await Task.Run(() =>
            {
                if (!HostChecker.CheckHost(new HostData(host, false)))
                    throw new HostNotValidException("Host not exists", host);
            });
        }
    }
}
