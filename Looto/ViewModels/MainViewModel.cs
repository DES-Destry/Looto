using Looto.Models;
using System.Net;
using System.Net.Sockets;
using System.Windows.Input;

namespace Looto.ViewModels
{
    /// <summary>
    /// View model for main window of application
    /// </summary>
    class MainViewModel : BaseViewModel
    {
        #region Fields for binding

        private bool _multiplePorts = true;
        private bool _rangeOfPorts = false;
        private bool _sameProtocols = false;
        private bool _wrongInput = true;

        private string _host = "";
        private string _tcpPorts = "";
        private string _udpPorts = "";
        private string _fromTcpPort = "";
        private string _toTcpPort = "";
        private string _fromUdpPort = "";
        private string _toUdpPort = "";

        // Values for xaml bindng.
        // Meaning all of them you can understand with UI(MainWindow.xaml file in views) 
        public bool MultiplePorts
        {
            get => _multiplePorts;
            set
            {
                _multiplePorts = value;
                OnPropertyChanged();
            }
        }
        public bool RangeOfPorts
        {
            get => _rangeOfPorts;
            set
            {
                _rangeOfPorts = value;
                OnPropertyChanged();
            }
        }
        public bool SameProtocols
        {
            get => _sameProtocols;
            set
            {
                _sameProtocols = value;
                OnPropertyChanged();
            }
        }
        public bool WrongInput
        {
            get => _wrongInput;
            set
            {
                _wrongInput = value;
                OnPropertyChanged();
            }
        }

        public string Host
        {
            get => _host;
            set
            {
                _host = value;
                WrongInput = CheckInputs();
                OnPropertyChanged();
            }
        }
        public string TcpPorts
        {
            get => _tcpPorts;
            set
            {
                _tcpPorts = value;
                WrongInput = CheckInputs();
                OnPropertyChanged();
            }
        }
        public string UdpPorts
        {
            get => _udpPorts;
            set
            {
                _udpPorts = value;
                WrongInput = CheckInputs();
                OnPropertyChanged();
            }
        }
        public string FromTcpPort
        {
            get => _fromTcpPort;
            set
            {
                _fromTcpPort = value;
                WrongInput = CheckInputs();
                OnPropertyChanged();
            }
        }
        public string ToTcpPort
        {
            get => _toTcpPort;
            set
            {
                _toTcpPort = value;
                WrongInput = CheckInputs();
                OnPropertyChanged();
            }
        }
        public string FromUdpPort
        {
            get => _fromUdpPort;
            set
            {
                _fromUdpPort = value;
                WrongInput = CheckInputs();
                OnPropertyChanged();
            }
        }
        public string ToUdpPort
        {
            get => _toUdpPort;
            set
            {
                _toUdpPort = value;
                WrongInput = CheckInputs();
                OnPropertyChanged();
            }
        }


        public ICommand Scan => new BaseCommand(StartScan);

        #endregion

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

        private bool CheckInputs() => true;
    }
}
