using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.TCPCom {

    public static class NetControlCharacter {
        /// <summary> Null; Nullzeichen; 0; 0x00</summary>
        public const char NUL = '\0';	    //= 
        /// <summary> Start of Header; Beginn der Überschrift; 1; 0x01</summary>
        public const char SOH = '\u0001';
        /// <summary> Start of Text; Beginn des Textes; 2; 0x02</summary>
        public const char STX = '\u0002';
        /// <summary> End of Text; Ende des Textes; 3;  0x03</summary>
        public const char ETX = '\u0003';
        /// <summary> End of Transmission; Ende der Übertragung; 4;  0x04</summary>
        public const char EOT = '\u0004';
        /// <summary> Enquiry; Anfrage; 5;  0x05</summary>
        public const char ENQ = '\u0005';
        /// <summary> ACKnowledge; Zustimmung/Bestätigung; 6;  0x06</summary>
        public const char ACK = '\u0006';
        /// <summary> Bell; Tonsignal; 7;  0x07</summary>
        public const char BEL = '\a';
        /// <summary> Backspace; Rückschritt; 8;  0x08</summary>
        public const char BS = '\b';
        /// <summary> Horizontal Tab; Horizontaler Tabulator; 9;  0x09</summary>
        public const char HT = '\t';
        /// <summary> LineFeed; Zeilenvorschub; 10; 0x0A</summary>
        public const char LF = '\n';
        /// <summary> Vertical Tab; Vertikaler Tabulator; 11; 0x0B</summary>
        public const char VT = '\v';
        /// <summary> FormFeed; Seitenvorschub; 12; 0x0C</summary>
        public const char FF = '\f';
        /// <summary> CarriageReturn; Wagenrücklauf; 13; 0x0D</summary>
        public const char CR = '\r';
        /// <summary> Device Control 1; Gerätekontrollkode 1; 17; 0x11</summary>
        public const char DC1 = '\u0011';
        /// <summary> Device Control 2; Gerätekontrollkode 2; 18; 0x12</summary>
        public const char DC2 = '\u0012';
        /// <summary> Device Control 3; Gerätekontrollkode 3; 19; 0x13</summary>
        public const char DC3 = '\u0013';
        /// <summary> Negative Acknowledge; Negative Bestätigung; 21; 0x15</summary>
        public const char NAK = '\u0015';
        /// <summary> Synchronous Idle; Synchronisierungssignal; 22; 0x16</summary>
        public const char SYN = '\u0016';
        /// <summary> Substitute; Ersetzen; 26; 0x1A</summary>
        public const char SUB = '\u001A';
        /// <summary> Delimiter, kein offizielles Zeichen, nur aus Kompatibilitätsgründen</summary>
        public const char DLI = '\u0005';
    }

    public enum ConnectionStates {
        Idle,           // das Interface ist ungenutzt, es wurde noch keine Verbindung aufgebaut
        Connecting,     // eine Verbindung wird aufgebaut
        Connected,      // es besteht eine Verbindung
        Disconnecting,  // die Verbindung wird abgebaut
        Disconnected    // die Verbindung ist abgebaut
    }

    public class TCPClient : Cliparts.Messaging.Message, INotifyPropertyChanged {

        #region Properties

        protected SynchronizationContext syncContext;

        protected ConnectionStates connectionStatus;
        public ConnectionStates ConnectionStatus {
            get {
                return this.connectionStatus;
            }
            protected set {
                if (this.connectionStatus != value) {
                    this.connectionStatus = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private TcpClient tcpClient = null;

        public IPAddress LocalIPAddress = null;

        private byte[] buffer;

        private const int receiveBufferSize = 4096;

        protected System.Timers.Timer connectWatchdog;

        #endregion


        #region Funktionen

        public TCPClient(
            SynchronizationContext syncContext) {

            this.syncContext = syncContext;

            // Get a list of all network interfaces (usually one per network card, dialup, and VPN connection) 
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface network in networkInterfaces) {
                // Read the IP configuration for each network 
                IPInterfaceProperties properties = network.GetIPProperties();

                if (network.OperationalStatus == OperationalStatus.Up) {
                    // Each network interface may have multiple IP addresses 
                    foreach (IPAddressInformation address in properties.UnicastAddresses) {
                        // We're only interested in IPv4 addresses for now 
                        if (address.Address.AddressFamily == AddressFamily.InterNetwork &&
                            !IPAddress.IsLoopback(address.Address)) {
                            this.LocalIPAddress = address.Address;
                            break;
                        }
                    }
                }
            }

            this.connectWatchdog = new System.Timers.Timer(5000);
            this.connectWatchdog.AutoReset = false;
            this.connectWatchdog.Elapsed += this.connectWatchdog_Elapsed;

        }

        public void Dispose() {
            this.closeConnection();
            this.connectWatchdog.Elapsed -= this.connectWatchdog_Elapsed;
        }

        public void Connect(
            string hostName,
            int port) {

            switch (this.ConnectionStatus) {
                case ConnectionStates.Idle:
                case ConnectionStates.Disconnected:
                    // Verbindung aufbauen
                    try {
                        if (this.tcpClient == null) {
                            this.tcpClient = new TcpClient(new IPEndPoint(this.LocalIPAddress, 0));
                            this.tcpClient.ReceiveBufferSize = receiveBufferSize;
                        }
                        this.tcpClient.BeginConnect(hostName, port, this.connectCallback, this.tcpClient);
                        this.ConnectionStatus = ConnectionStates.Connecting;
                        this.connectWatchdog.Start();
                    }
                    catch (Exception exc) {
                        this.on_Error(this, "Connect: " + exc.Message);
                    }
                    break;
                case ConnectionStates.Disconnecting:
                    // es läuft gerade ein Verbindungsabbau, der Aufbau wird "on hold" gesetzt
                    break;
                case ConnectionStates.Connecting:
                    // es läuft bereits ein Verbindungsaufbau
                    break;
                case ConnectionStates.Connected:
                    // es besteht bereits eine Verbindung
                    break;
            }

        }

        public void Disconnect() {

            switch (this.ConnectionStatus) {
                case ConnectionStates.Idle:
                case ConnectionStates.Disconnected:
                    // die Verbindung ist bereits abgebaut
                    break;
                case ConnectionStates.Disconnecting:
                    // es läuft gerade ein Verbindungsabbau
                    break;
                case ConnectionStates.Connecting:
                    // es läuft gerade ein Verbindungsaufbau, der Abbau wird "on hold" gesetzt
                    this.closeConnection();
                    break;
                case ConnectionStates.Connected:
                    // Verbindung abbauen
                    this.closeConnection();
                    break;
            }
        }

        protected void closeConnection() {
            this.connectWatchdog.Stop();
            this.ConnectionStatus = ConnectionStates.Disconnected;
            if (this.tcpClient is TcpClient) {
                this.tcpClient.Client.Dispose();
                this.tcpClient.Close();
            }
            this.tcpClient = null;
        }

        public void SendData(
            byte[] data) {
            if (this.ConnectionStatus == ConnectionStates.Connected &&
                data is byte[]) {
                try {
                    this.tcpClient.Client.BeginSend(data, 0, data.Length, SocketFlags.None, this.sendCallback, data);
                }
                catch (Exception) {
                }
            }
        }

        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void on_PropertyChanged([CallerMemberName]string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected virtual void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected virtual void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        protected event EventHandler<byte[]> DataReveived;
        protected void on_DataReveived(object sender, byte[] e) { Helper.raiseEvent(sender, this.DataReveived, e); }

        #endregion

        #region Events.Incoming

        protected void connectCallback(IAsyncResult ar) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_connectCallback);
            if (this.syncContext != null) this.syncContext.Post(callback, ar);
        }
        protected void sync_connectCallback(object content) {
            try {
                this.connectWatchdog.Stop();
                IAsyncResult ar = (IAsyncResult)content;
                TcpClient tcpClient = ar.AsyncState as TcpClient;
                try {
                    if (tcpClient.Connected) {
                        tcpClient.EndConnect(ar);
                        IPEndPoint endPoint = tcpClient.Client.LocalEndPoint as IPEndPoint;
                        this.ConnectionStatus = ConnectionStates.Connected;
                        this.buffer = new byte[receiveBufferSize];
                        this.tcpClient.Client.BeginReceive(this.buffer, 0, receiveBufferSize, SocketFlags.None, this.receiveCallback, this.tcpClient.Client);
                    }
                    else {
                        this.closeConnection();
                    }
                }
                catch (Exception) {
                    this.closeConnection();
                }
            }
            catch (Exception exc) {
                this.on_Error(this, "connectCallback: " + exc.Message);
            }

        }

        void connectWatchdog_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_connectWatchdog_Elapsed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected void sync_connectWatchdog_Elapsed(object content) {
            this.closeConnection();
        }

        protected void receiveCallback(IAsyncResult ar) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_receiveCallback);
            if (this.syncContext != null) this.syncContext.Post(callback, ar);
        }
        protected void sync_receiveCallback(object content) {
            try {
                IAsyncResult ar = (IAsyncResult)content;
                Socket socket = ar.AsyncState as Socket;
                if (socket is Socket) {
                    socket.EndReceive(ar);
                    byte[] data = new byte[receiveBufferSize];
                    this.buffer.CopyTo(data, 0);
                    this.on_DataReveived(this, data);
                    this.buffer = new byte[receiveBufferSize];
                    socket.BeginReceive(this.buffer, 0, receiveBufferSize, SocketFlags.None, this.receiveCallback, socket);
                }
            }
            catch (Exception exc) {
                this.on_Error(this, "receiveCallback: " + exc.Message);
            }
        }

        protected void sendCallback(IAsyncResult ar) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_sendCallback);
            if (this.syncContext != null) this.syncContext.Post(callback, ar);
        }
        protected void sync_sendCallback(object content) {
        }

        #endregion
    }


}
