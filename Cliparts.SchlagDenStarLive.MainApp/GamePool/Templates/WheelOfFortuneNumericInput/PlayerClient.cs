using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

using Cliparts.TCPCom;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.WheelOfFortuneNumericInput {

    public class PlayerClient : TCPClient {

        #region Properties

        private string hostname = string.Empty;
        public string Hostname {
            get { return this.hostname; }
            set {
                if (this.hostname != value) {
                    if (this.ConnectionStatus == ConnectionStates.Connected) this.Disconnect();
                    if (string.IsNullOrEmpty(value)) this.hostname = string.Empty;
                    else this.hostname = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int playerValue = 0;
        public int PlayerValue {
            get { return this.playerValue; }
            set {
                if (this.playerValue != value) {
                    this.playerValue = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private System.Timers.Timer aliveTimer;
        private bool hostIsAlive = false;

        #endregion


        #region Funktionen

        public PlayerClient(
            SynchronizationContext syncContext)
            : base(syncContext) {

            base.DataReveived += this.playerClient_DataReveived;
            base.PropertyChanged += this.playerClient_PropertyChanged;

            this.aliveTimer = new System.Timers.Timer(10000);
            this.aliveTimer.AutoReset = true;
            this.aliveTimer.Elapsed += this.aliveTimer_Elapsed;
        }

        public new void Dispose() {
            base.Dispose();

            this.aliveTimer.Stop();
            this.aliveTimer.Elapsed -= this.aliveTimer_Elapsed;

            base.DataReveived -= this.playerClient_DataReveived;
            base.PropertyChanged -= this.playerClient_PropertyChanged;

        }

        public void Connect() {
            if (!string.IsNullOrEmpty(this.Hostname)) this.Connect(this.Hostname, 61881);
        }
        private void sendAlive() {
            this.hostIsAlive = false;
            this.sendToHost("alive");
        }
        public void SetIdle() { this.sendToHost("SetIdle"); }
        public void SetActive() {
            this.PlayerValue = 0;
            this.sendToHost("SetActive");
        }
        public void SetLocked() { this.sendToHost("SetLocked"); }
        public void SetUnlocked() { this.sendToHost("SetUnlocked"); }
        public void SetInputValue(int value) {
            string sendText = string.Format("SetInputValue{0}{1}", NetControlCharacter.HT.ToString(), value.ToString());
            this.sendToHost(sendText);
        }

        private void sendToHost(
            string sendText) {
            sendText = NetControlCharacter.STX.ToString() + sendText + NetControlCharacter.EOT.ToString();
            byte[] data = Encoding.ASCII.GetBytes(sendText);
            this.SendData(data);
        }

        private void parseReceivedData(
            byte[] data) {
            if (data is byte[] && data.Length > 2) {
                string recText = Encoding.ASCII.GetString(data);
                if (recText.StartsWith(NetControlCharacter.STX.ToString()) &&
                    recText.EndsWith(NetControlCharacter.EOT.ToString())) {
                    string[] recArray = recText.Split(new char[] { NetControlCharacter.STX, NetControlCharacter.EOT }, StringSplitOptions.RemoveEmptyEntries);
                    if (recArray.Length > 0) {
                        if (recArray[0] == "alive") {
                        }
                        else {
                            int result;
                            if (int.TryParse(recArray[0], out result)) this.PlayerValue = result;
                            else this.PlayerValue = 0;
                            this.on_PlayerValueReceived(this, this.PlayerValue);
                        }
                    }
                }
            }
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler<int?> PlayerValueReceived;
        private void on_PlayerValueReceived(object sender, int? e) { Helper.raiseEvent(sender, this.PlayerValueReceived, e); }

        #endregion

        #region Events.Incoming

        void playerClient_DataReveived(object sender, byte[] e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_playerClient_DataReveived);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected void sync_playerClient_DataReveived(object content) {
            this.hostIsAlive = true;
            byte[] foo = content as byte[];
            if (foo is byte[]
                && foo.Length > 0) {
                int i = foo.Length - 1;
                while (foo[i] == 0) --i;
                byte[] bar = new byte[i + 1];
                Array.Copy(foo, bar, i + 1);
                this.parseReceivedData(bar);
            }
        }

        void playerClient_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_playerClient_PropertyChanged);
            PropertyChangedEventSyncArgs pcesa = new PropertyChangedEventSyncArgs(sender, e);
            if (this.syncContext != null) this.syncContext.Post(callback, pcesa);
        }
        protected void sync_playerClient_PropertyChanged(object content) {
            PropertyChangedEventSyncArgs pcesa = content as PropertyChangedEventSyncArgs;
            if (pcesa is PropertyChangedEventSyncArgs) {
                if (pcesa.EventArgs.PropertyName == "ConnectionStatus") {
                    if (this.ConnectionStatus == ConnectionStates.Connected) {
                        this.sendAlive();
                        this.aliveTimer.Start();
                    }
                    else this.aliveTimer.Stop();
                }
            }
        }

        void aliveTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_aliveTimer_Elapsed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected void sync_aliveTimer_Elapsed(object content) {
            if (this.ConnectionStatus == ConnectionStates.Connected) {
                //Console.WriteLine("sync_aliveTimer_Elapsed; hostIsAlive: " + this.hostIsAlive.ToString());
                if (this.hostIsAlive) this.sendAlive();
                else {
                    this.aliveTimer.Stop();
                    this.Disconnect();
                }
            }
            else this.aliveTimer.Stop();
        }

        #endregion

    }

}
