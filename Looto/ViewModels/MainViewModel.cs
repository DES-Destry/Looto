using Looto.Models;
using Looto.Models.Scanner;
using Looto.Views;
using System;
using System.Collections.Generic;
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
        private IScanner _scanner;

        #region Fields for binding

        // Values for xaml bindng.
        // Meaning all of them you can understand with UI(MainWindow.xaml file in views)
        private bool _isMultiplePorts = true;
        private bool _isRangeOfPorts = false;
        private bool _isBothProtocols = false;
        private bool _isUndefinedProtocols = false;
        private bool _isWrongInput = true;
        private bool _isLoading = false;

        private string _host = "";
        private string _tcpPorts = "";
        private string _udpPorts = "";
        private string _fromTcpPort = "";
        private string _toTcpPort = "";
        private string _fromUdpPort = "";
        private string _toUdpPort = "";

        private int _maxProgress = 1;
        private int _currentProgress = 0;


        /// <summary>Shows input, where ports separated by commas.</summary>
        /// <value>The <see cref="IsMultiplePorts"/> property gets/sets the value of the bool field, <see cref="_isMultiplePorts"/>.</value>
        public bool IsMultiplePorts
        {
            get => _isMultiplePorts;
            set
            {
                _isMultiplePorts = value;
                IsWrongInput = IsNotValidInputs();
                OnPropertyChanged();
            }
        }
        /// <summary>Shows input, where user need write range of ports (from, to).</summary>
        /// <value>The <see cref="IsRangeOfPorts"/> property gets/sets the value of the bool field, <see cref="_isRangeOfPorts"/>.</value>
        public bool IsRangeOfPorts
        {
            get => _isRangeOfPorts;
            set
            {
                _isRangeOfPorts = value;
                IsWrongInput = IsNotValidInputs();
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Checks only TCP and ignoring UDP. <br/>
        /// TCP = TCP/UDP; UDP => ignore.
        /// </summary>
        /// <value>The <see cref="IsBothProtocols"/> property gets/sets the value of the bool field, <see cref="_isBothProtocols"/>.</value>
        public bool IsBothProtocols
        {
            get => _isBothProtocols;
            set
            {
                _isBothProtocols = value;
                IsWrongInput = IsNotValidInputs();
                OnPropertyChanged();
                OnPropertyChanged("FirstHeader");
                OnPropertyChanged("IsUdpFieldsVisible");
            }
        }
        /// <summary>
        /// If user not selected no one of TCP or UDP.<br/>
        /// Feature for future.
        /// </summary>
        /// <value>The <see cref="IsUndefinedProtocols"/> property gets/sets the value of the bool field, <see cref="_isUndefinedProtocols"/>.</value>
        public bool IsUndefinedProtocols
        {
            get => _isUndefinedProtocols;
            set
            {
                _isUndefinedProtocols = value;
                IsWrongInput = IsNotValidInputs();
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// If users input are wrong.<br/>
        /// If it equals true, then scanning not able to start.
        /// </summary>
        /// <value>The <see cref="IsWrongInput"/> property gets/sets the value of the bool field, <see cref="_isWrongInput"/>.</value>
        public bool IsWrongInput
        {
            get => _isWrongInput;
            set
            {
                _isWrongInput = value;
                OnPropertyChanged();
                OnPropertyChanged("IsScanButtonEnabled");
            }
        }
        /// <summary>Locks button if wrong input equals true.</summary>
        /// <value>The <see cref="IsScanButtonEnabled"/> property gets and invert the value of the bool field, <see cref="_isWrongInput"/>.</value>
        public bool IsScanButtonEnabled => !_isWrongInput;
        /// <summary>Equals true if scanning in progress.</summary>
        /// <value>The <see cref="IsLoading"/> property gets/sets the value of the bool field, <see cref="_isLoading"/>.</value>
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
                OnPropertyChanged("IsShowScanButton");
            }
        }
        /// <summary>When it is loading, then scan button are hide.</summary>
        /// <value>The <see cref="IsShowScanButton"/> property gets and invert the value of the bool field, <see cref="_isLoading"/>.</value>
        public bool IsShowScanButton => !_isLoading;
        /// <summary>Hides UDP fields if user selected both protocols.</summary>
        /// <value>The <see cref="IsUdpFieldsVisible"/> property gets the value of the bool field, <see cref="_isBothProtocols"/>.</value>
        public bool IsUdpFieldsVisible => !_isBothProtocols;


        /// <summary>Header of the first line(tcp ports line)</summary>
        /// <value>The <see cref="FirstHeader"/> property gets the value of the bool field, <see cref="_isBothProtocols"/>.</value>
        public string FirstHeader => _isBothProtocols ? "TCP/UDP" : "TCP";
        /// <summary>IP address to check.</summary>
        /// <value>The <see cref="Host"/> property gets/sets the value of the string field, <see cref="_host"/>.</value>
        public string Host
        {
            get => _host;
            set
            {
                _host = value;
                IsWrongInput = IsNotValidInputs();
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
                IsWrongInput = IsNotValidInputs();
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
                IsWrongInput = IsNotValidInputs();
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
                IsWrongInput = IsNotValidInputs();
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
                IsWrongInput = IsNotValidInputs();
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
                IsWrongInput = IsNotValidInputs();
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
                IsWrongInput = IsNotValidInputs();
                OnPropertyChanged();
            }
        }


        /// <summary>Maximum value of progress bar.</summary>
        /// <value>The <see cref="MaxProgress"/> property gets/sets the value of the int field, <see cref="_maxProgress"/>.</value>
        public int MaxProgress
        {
            get => _maxProgress;
            set
            {
                _maxProgress = value;
                OnPropertyChanged();
            }
        }
        /// <summary>Current progress value of progress bar.</summary>
        /// <value>The <see cref="CurrentProgress"/> property gets/sets the value of the int field, <see cref="_currentProgress"/>.</value>
        public int CurrentProgress
        {
            get => _currentProgress;
            set
            {
                _currentProgress = value;
                OnPropertyChanged();
            }
        }



        /// <summary>
        /// Scan button command. <br/>
        /// Start scanning ports with values from input.
        /// </summary>
        public ICommand Scan => new BaseCommand(ScanCommand);

        #endregion

        /// <summary>Scan host with parameters in input.</summary>
        /// <param name="parameter">
        /// Basic <see cref="BaseCommand"/> parameter. <br/>
        /// Value of this gets from xaml (CommandParameter property).
        /// </param>
        private void ScanCommand(object parameter)
        {
            if (_isWrongInput || _isLoading) return;

            Port[] portsToScan;
            // Set ports to the scanner.
            if (_isMultiplePorts)
            {
                _scanner = new MultipleScanner();
                portsToScan = GetMultiplePortsCollection();
            }
            else if (_isRangeOfPorts)
            {
                _scanner = new RangeOfPortsScanner();
                portsToScan = GetRangeOfPortsCollection();
            }
            else
            {
                IsWrongInput = true;
                return;
            }
            StartScanning(portsToScan);
        }

        /// <summary>Scan all ports.</summary>
        /// <param name="ports">Ports collection.</param>
        private void StartScanning(Port[] ports)
        {
            _scanner.Host = _host;
            _scanner.Ports = ports;
            _scanner.OnOnePortWasScanned += ProgressWasChanged;
            _scanner.OnScanEnding += ScanEnded;

            IsLoading = true;

            try
            {
                _scanner.ScanAllAsync();
            }
            catch (ArgumentNullException) // If host or ports are incorrect
            {
                Host = TcpPorts = UdpPorts = FromTcpPort = ToTcpPort = FromUdpPort = ToUdpPort = "";
            }
            catch (RangeOfPortsException) // Input range of ports are incorrect
            {
                Host = FromTcpPort = ToTcpPort = FromUdpPort = ToUdpPort = "";
            }
        }

        /// <summary>Calls on invoking <see cref="IScanner.OnOnePortWasScanned"/> event.</summary>
        /// <param name="dest">Count of all ports for scanning.</param>
        /// <param name="currentProgress">Count of already scanned ports.</param>
        private void ProgressWasChanged(int dest, int currentProgress)
        {
            // Change progress bar paremeters
            MaxProgress = dest;
            CurrentProgress = currentProgress;
        }

        /// <summary>Calls on invoking <see cref="IScanner.OnScanEnding"/> event.</summary>
        /// <param name="results">Results after scanning.</param>
        private void ScanEnded(ScanResult results)
        {
            IsLoading = false;
            MaxProgress = 1;
            CurrentProgress = 0;

            // Open results in new window.
            var resultsShowCase = new ResultsWindow(results);
            resultsShowCase.Show();

            _scanner = null;
        }

        /// <summary>Create the correct array of ports from inputs for multiple scanning.</summary>
        /// <returns>Correct multiple ports array.</returns>
        private Port[] GetMultiplePortsCollection()
        {
            List<Port> portsToScan = new List<Port>();

            ushort[] tcpPortValues = GetPortsArrayFromString(_tcpPorts);
            ushort[] udpPortValues = GetPortsArrayFromString(_udpPorts);

            if (_isBothProtocols)
            {
                foreach (ushort portValue in tcpPortValues)
                {
                    portsToScan.Add(new Port(portValue, System.Net.Sockets.ProtocolType.Tcp));
                    portsToScan.Add(new Port(portValue, System.Net.Sockets.ProtocolType.Udp));
                }
            }
            else
            {
                foreach (ushort tcpPortValue in tcpPortValues)
                    portsToScan.Add(new Port(tcpPortValue, System.Net.Sockets.ProtocolType.Tcp));

                foreach (ushort udpPortValue in udpPortValues)
                    portsToScan.Add(new Port(udpPortValue, System.Net.Sockets.ProtocolType.Udp));
            }

            return portsToScan.ToArray();
        }

        /// <summary>Create the correct array of ports from inputs for range scanning.</summary>
        /// <returns>Correct range of ports array.</returns>
        private Port[] GetRangeOfPortsCollection()
        {
            RangeOfPorts rangeOfPorts = GetRangeOfPortsFromInputs();
            Port[] result = rangeOfPorts.GetPortsArray();

            return result;
        }

        /// <summary>Translate string ports separeted by commas to numeric array.</summary>
        /// <param name="portString">Ports separated by commas.</param>
        /// <returns>Array of numeric ports</returns>
        private ushort[] GetPortsArrayFromString(string portString)
        {
            // Check null value.
            if (portString == null) return new ushort[] { };

            List<ushort> numPorts = new List<ushort>();
            // Ports are separated bu commas.
            string[] strPorts = portString.Split(',');
            foreach (string port in strPorts)
            {
                if (ushort.TryParse(port, out ushort numPort))
                    numPorts.Add(numPort);
                else return new ushort[] { };
            }

            return numPorts.ToArray();
        }

        /// <summary>Generate <see cref="RangeOfPorts"/> instance from inputs.</summary>
        /// <returns><see cref="RangeOfPorts"/> instance.</returns>
        private RangeOfPorts GetRangeOfPortsFromInputs()
        {
            RangeOfPorts rangeOfPorts = new RangeOfPorts();

            if (_isBothProtocols)
            {
                if (ushort.TryParse(_fromTcpPort, out ushort fromTcp))
                    rangeOfPorts.FromTcp = rangeOfPorts.FromUdp = fromTcp;
                if (ushort.TryParse(_toTcpPort, out ushort toTcp))
                    rangeOfPorts.ToTcp = rangeOfPorts.ToUdp = toTcp;
            }
            else
            {
                if (ushort.TryParse(_fromTcpPort, out ushort fromTcp))
                    rangeOfPorts.FromTcp = fromTcp;
                if (ushort.TryParse(_toTcpPort, out ushort toTcp))
                    rangeOfPorts.ToTcp = toTcp;
                if (ushort.TryParse(_fromUdpPort, out ushort fromUdp))
                    rangeOfPorts.FromUdp = fromUdp;
                if (ushort.TryParse(_toUdpPort, out ushort toUdp))
                    rangeOfPorts.ToUdp = toUdp;
            }

            return rangeOfPorts;
        }

        /// <summary>Checks all user inputs.</summary>
        /// <returns>
        /// If input right returns <see cref="false"/>, else returns <see cref="true"/>. <br/>
        /// This inverse of values needs to show user wrong input message (true - visible; false - collapsed).
        /// </returns>
        private bool IsNotValidInputs()
        {
            // Check IP address for correctness.
            if (_host.Trim() == "")
                return true;

            // If user selected string of ports (separeted by commas), then it will check it in IsValidPortsString(string) method.
            // If user selected same protocols - UDP protocol input will not check.
            if (_isMultiplePorts)
            {
                byte notDefined = 0;

                if (_tcpPorts.Trim() == "")
                    notDefined++;
                if (_udpPorts.Trim() == "")
                    notDefined++;

                if (_tcpPorts.Trim() != "" && !IsValidPortsString(_tcpPorts)) 
                    return true;
                if (_udpPorts.Trim() != "" && !(_isBothProtocols || IsValidPortsString(_udpPorts)))
                    return true;

                return notDefined == 2;
            }

            // If user selected range of ports, then it will check all ports in IsValidPort(string) method.
            // From value must be less than To value.
            // If user selected same protocols - UDP protocol input will not check.
            // TODO: fix something. If tcp port correct, udp can be not correct and vice versa.
            if (_isRangeOfPorts)
                return !GetRangeOfPortsFromInputs().IsValid;

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
