using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Cliparts.SchlagDenStarLive.NumericSelectApp.TCPCom {

    public class Worker {

        #region Properties

        protected SynchronizationContext syncContext;

        public Socket Socket { get; private set; }

        public IPAddress RemoteIP { get; private set; }

        private byte[] buffer;

        private const int receiveBufferSize = 4096;

        #endregion


        #region Funktionen

        public Worker(
            Socket socket,
            IPAddress remoteIP) {
            this.Socket = socket;
            this.RemoteIP = remoteIP;
            this.buffer = new byte[receiveBufferSize];
            this.Socket.BeginReceive(this.buffer, 0, this.buffer.Length, 0, new AsyncCallback(this.receiveCallback), this.Socket);
        }

        public void Dispose() {
        }

        public void SendData(
            byte[] data) {
            if (data is byte[] &&
                data.Length > 0) {
                try {
                    this.Socket.BeginSend(data, 0, data.Length, SocketFlags.None, this.sendCallback, data);
                }
                catch (Exception) {
                }
            }
        }

        #endregion


        #region Events.Outgoing

        internal event EventHandler<byte[]> DataReveived;
        protected void on_DataReveived(object sender, byte[] e) { Helper.raiseEvent(sender, this.DataReveived, e); }

        internal event EventHandler Disposing;
        protected void on_Disposing(object sender, EventArgs e) { Helper.raiseEvent(sender, this.Disposing, e); }

        #endregion

        #region Events.Incoming

        private void receiveCallback(IAsyncResult ar) {
            try {
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
            catch (Exception) {
                // Die Verbindung wurde abgebaut
                this.on_Disposing(this, new EventArgs());
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

    public class TCPServer : INotifyPropertyChanged {

        #region Properties

        protected SynchronizationContext syncContext;

        public IPAddress LocalIPAddress = null;

        private bool isListening = false;
        public bool IsListening {
            get { return this.isListening; }
            set {
                if (this.isListening != value) {
                    this.isListening = value;
                    this.on_PropertyChanged();
                }
            }
        }

        Dictionary<string, Worker> workerList = new Dictionary<string, Worker>();
        public Worker[] WorkerList { get { return this.workerList.Values.ToArray(); } }

        #endregion


        #region Funktionen

        public TCPServer(
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

        }

        public void Dispose() {
            foreach (Worker worker in this.workerList.Values) {
                worker.DataReveived -= this.on_DataReveived;
                worker.Disposing -= this.worker_Disposing;
                worker.Dispose();
            }
            this.workerList.Clear();
        }

        public void StartListening(
            int port) {

            if (!this.IsListening) {

                this.IsListening = true;

                try {
                    Socket listener = new Socket(this.LocalIPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    IPEndPoint localEndPoint = new IPEndPoint(this.LocalIPAddress, port);
                    listener.Bind(localEndPoint);
                    listener.Listen(100);

                    listener.BeginAccept(new AsyncCallback(acceptCallback), listener);

                }
                catch (Exception e) {
                    this.IsListening = false;
                }
            }
        }

        public void StoptListening() {
            if (!this.IsListening) {
                this.IsListening = false;
            }
        }

        public void SendData(
            byte[] data) {
            foreach (Worker worker in this.WorkerList) worker.SendData(data);
        }

        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_PropertyChanged([CallerMemberName]string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        public event EventHandler<byte[]> DataReveived;
        protected void on_DataReveived(object sender, byte[] e) { Helper.raiseEvent(sender, this.DataReveived, e); }

        public event EventHandler<String> Error;
        protected void on_Error(object sender, String e) { Helper.raiseEvent(sender, this.Error, e); }

        #endregion

        #region Events.Incoming

        private void acceptCallback(IAsyncResult ar) {

            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create new worker.  
            EndPoint ep = handler.RemoteEndPoint;
            IPAddress remoteIP;
            string[] values = ep.ToString().Split(':');
            if (values.Length == 2 &&
                IPAddress.TryParse(values[0], out remoteIP) &&
                !this.workerList.ContainsKey(remoteIP.ToString())) {
                Worker worker = new Worker(handler, remoteIP);
                worker.DataReveived += this.on_DataReveived;
                worker.Disposing += this.worker_Disposing;
                this.workerList.Add(remoteIP.ToString(), worker);
                this.on_PropertyChanged("WorkerList");
            }

            listener.BeginAccept(new AsyncCallback(acceptCallback), listener);
        }

        void worker_Disposing(object sender, EventArgs e) {
            Worker worker = sender as Worker;
            if (worker is Worker &&
                this.workerList.Keys.Contains(worker.RemoteIP.ToString())) {
                worker.DataReveived -= this.on_DataReveived;
                worker.Disposing -= this.worker_Disposing;
                this.workerList.Remove(worker.RemoteIP.ToString());
                this.on_PropertyChanged("WorkerList");
            }
        }

        #endregion
    }
}
