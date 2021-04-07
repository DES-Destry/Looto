using Looto.Models;
using System.Net;
using System.Windows.Input;

namespace Looto.ViewModels
{
    /// <summary>
    /// View model for main window of application.<br/>
    /// Extends <see cref="BaseViewModel"/> class.
    /// </summary>
    class MainViewModel : BaseViewModel
    {
        #region Fields for binding

        // Values for xaml bindng.
        // Meaning all of them you can understand with UI(MainWindow.xaml file in views)
        private bool _multiplePorts = true;
        private bool _rangeOfPorts = false;
        private bool _sameProtocols = false;
        private bool _undefinedProtocols = false;
        private bool _wrongInput = true;

        private string _host = "";
        private string _tcpPorts = "";
        private string _udpPorts = "";
        private string _fromTcpPort = "";
        private string _toTcpPort = "";
        private string _fromUdpPort = "";
        private string _toUdpPort = "";


        /// <summary>Shows input, where ports separated by commas.</summary>
        /// <value>The <see cref="MultiplePorts"/> property gets/sets the value of the bool field, <see cref="_multiplePorts"/>.</value>
        public bool MultiplePorts
        {
            get => _multiplePorts;
            set
            {
                _multiplePorts = value;
                WrongInput = IsNotValidInputs();
                OnPropertyChanged();
            }
        }
        /// <summary>Shows input, where user need write range of ports (from, to).</summary>
        /// <value>The <see cref="RangeOfPorts"/> property gets/sets the value of the bool field, <see cref="_rangeOfPorts"/>.</value>
        public bool RangeOfPorts
        {
            get => _rangeOfPorts;
            set
            {
                _rangeOfPorts = value;
                WrongInput = IsNotValidInputs();
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Checks only TCP and ignoring UDP. <br/>
        /// TCP = TCP/UDP; UDP => ignore.
        /// </summary>
        /// <value>The <see cref="SameProtocols"/> property gets/sets the value of the bool field, <see cref="_sameProtocols"/>.</value>
        public bool SameProtocols
        {
            get => _sameProtocols;
            set
            {
                _sameProtocols = value;
                WrongInput = IsNotValidInputs();
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// If user not selected no one of TCP or UDP.<br/>
        /// Feature for future.
        /// </summary>
        /// <value>The <see cref="UndefinedProtocols"/> property gets/sets the value of the bool field, <see cref="_undefinedProtocols"/>.</value>
        public bool UndefinedProtocols
        {
            get => _undefinedProtocols;
            set
            {
                _undefinedProtocols = value;
                WrongInput = IsNotValidInputs();
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// If users input are wrong.<br/>
        /// If it equals true, then scanning not able to start.
        /// </summary>
        /// <value>The <see cref="WrongInput"/> property gets/sets the value of the bool field, <see cref="_wrongInput"/>.</value>
        public bool WrongInput
        {
            get => _wrongInput;
            set
            {
                _wrongInput = value;
                OnPropertyChanged();
            }
        }


        /// <summary>IP address to check.</summary>
        /// <value>The <see cref="Host"/> property gets/sets the value of the string field, <see cref="_host"/>.</value>
        public string Host
        {
            get => _host;
            set
            {
                _host = value;
                WrongInput = IsNotValidInputs();
                OnPropertyChanged();
            }
        }
        /// <summary>All TCP ports separated by commas to check.</summary>
        /// <value>The <see cref="TcpPorts"/> property gets/sets the value of the string field, <see cref="_tcpPorts"/>.</value>
        public string TcpPorts
        {
            get => _tcpPorts;
            set
            {
                _tcpPorts = value;
                WrongInput = IsNotValidInputs();
                OnPropertyChanged();
            }
        }
        /// <summary>All UDP ports separated by commas to check.</summary>
        /// <value>The <see cref="UdpPorts"/> property gets/sets the value of the string field, <see cref="_udpPorts"/>.</value>
        public string UdpPorts
        {
            get => _udpPorts;
            set
            {
                _udpPorts = value;
                WrongInput = IsNotValidInputs();
                OnPropertyChanged();
            }
        }
        /// <summary>TCP port First value in range.</summary>
        /// <value>The <see cref="FromTcpPort"/> property gets/sets the value of the string field, <see cref="_fromTcpPort"/>.</value>
        public string FromTcpPort
        {
            get => _fromTcpPort;
            set
            {
                _fromTcpPort = value;
                WrongInput = IsNotValidInputs();
                OnPropertyChanged();
            }
        }
        /// <summary>TCP port Last value in range.</summary>
        /// <value>The <see cref="ToTcpPort"/> property gets/sets the value of the string field, <see cref="_toTcpPort"/>.</value>
        public string ToTcpPort
        {
            get => _toTcpPort;
            set
            {
                _toTcpPort = value;
                WrongInput = IsNotValidInputs();
                OnPropertyChanged();
            }
        }
        /// <summary>UDP port First value in range.</summary>
        /// <value>The <see cref="FromUdpPort"/> property gets/sets the value of the string field, <see cref="_fromTcpPort"/>.</value>
        public string FromUdpPort
        {
            get => _fromUdpPort;
            set
            {
                _fromUdpPort = value;
                WrongInput = IsNotValidInputs();
                OnPropertyChanged();
            }
        }
        /// <summary>UDP port Last value in range.</summary>
        /// <value>The <see cref="ToUdpPort"/> property gets/sets the value of the string field, <see cref="_toUdpPort"/>.</value>
        public string ToUdpPort
        {
            get => _toUdpPort;
            set
            {
                _toUdpPort = value;
                WrongInput = IsNotValidInputs();
                OnPropertyChanged();
            }
        }



        /// <summary>
        /// Scan button command. <br/>
        /// Start scanning ports with values from input.
        /// </summary>
        public ICommand Scan => new BaseCommand(StartScan);

        #endregion

        /// <summary>Scan host with parameters in input.</summary>
        /// <param name="parameter">
        /// Basic <see cref="BaseCommand"/> parameter. <br/>
        /// Value of this gets from xaml (CommandParameter property).
        /// </param>
        private void StartScan(object parameter)
        {
            if (_wrongInput) return;

            if (_multiplePorts)
            {

            }
            else if (_rangeOfPorts)
            {

            }
            else
            {
                WrongInput = true;
                return;
            }
        }

        /// <summary>Checks all user inputs.</summary>
        /// <returns>
        /// If input right returns <see cref="false"/>, else returns <see cref="true"/>. <br/>
        /// This inverse of values needs to show user wrong input message (true - visible; false - collapsed).
        /// </returns>
        private bool IsNotValidInputs()
        {
            // Check IP address for correctness.
            if (!IPAddress.TryParse(_host, out IPAddress _))
                return true;

            // If user selected string of ports (separeted by commas), then it will check it in IsValidPortsString(string) method.
            // If user selected same protocols - UDP protocol input will not check.
            if (_multiplePorts)
                return !(IsValidPortsString(_tcpPorts) || _sameProtocols || IsValidPortsString(_udpPorts));

            // If user selected range of ports, then it will check all ports in IsValidPort(string) method.
            // From value must be less than To value.
            // If user selected same protocols - UDP protocol input will not check.
            // TODO: fix something. If tcp port correct, udp can be not correct and vice versa.
            if (_rangeOfPorts)
                return !((IsValidPort(_fromTcpPort) && IsValidPort(_toTcpPort) && int.Parse(_fromTcpPort) < int.Parse(_toTcpPort)) ||
                       _sameProtocols || IsValidPort(_fromUdpPort) && IsValidPort(_toUdpPort) && int.Parse(_fromUdpPort) < int.Parse(_toUdpPort));

            return true;
        }

        /// <summary>Checks string of ports.</summary>
        /// <param name="portString">
        /// String with many ports which are separated by commas. <br/> 
        /// For example: "120, 121, 122".</param>
        /// <returns>If all of theese ports are correct - returns <see cref="true"/>.</returns>
        private bool IsValidPortsString(string portString)
        {
            // Check null value.
            if (portString == null) return false;

            // Ports are separated bu commas.
            string[] ports = portString.Split(',');
            foreach (string port in ports)
            {
                // Now ports array contains only port values. ["80", "443"] for example.
                // Check each element for correctness in IsValidPort(string) method.
                if (IsValidPort(port)) continue;
                else return false;
            }

            // If all ports array was iterated successful - return true, validation successful.
            return true;
        }

        /// <summary>
        /// Check one port for correctness. <br/>
        /// It must be a number in range from 1 to 65534.
        /// </summary>
        /// <param name="port">String representation of the port.</param>
        /// <returns>If this port are correct - retruns <see cref="true"/>.</returns>
        private bool IsValidPort(string port)
        {
            // Check null value.
            if (port == null) return false;

            // Port must be a number.
            if (int.TryParse(port.Trim(), out int numPort))
            {
                // Port must be in range [from 1 to 65534].
                if (numPort >= 1 && numPort < ushort.MaxValue) return true;
                else return false;
            }

            return false;
        }
    }
}
