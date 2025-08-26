using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;

using Cliparts.TCPCom;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimeToBeatSpeedKickRemote {

    public class PassenClient : TCPCom.TCPClient {

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

        private List<string> courseNameList = new List<string>();
        public string[] CourseNameList { get { return this.courseNameList.ToArray(); } }

        private string selectedCourse = string.Empty;
        public string SelectedCourse {
            get { return this.selectedCourse; }
            private set {
                if (this.selectedCourse != value) {
                    if (string.IsNullOrEmpty(value)) this.selectedCourse = string.Empty;
                    else this.selectedCourse = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int selectedCourseLength = 0;
        public int SelectedCourseLength {
            get { return this.selectedCourseLength; }
            private set {
                if (this.selectedCourseLength != value) {
                    if (value < 0) this.selectedCourseLength = 0;
                    else this.selectedCourseLength = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int courseStepIndex = 0;
        public int CourseStepIndex {
            get { return this.courseStepIndex; }
            private set {
                if (this.courseStepIndex != value) {
                    if (value < 0) this.courseStepIndex = 0;
                    else this.courseStepIndex = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private System.Timers.Timer aliveTimer;
        private bool hostIsAlive = false;

        #endregion


        #region Funktionen

        public PassenClient(
            SynchronizationContext syncContext)
            : base(syncContext) {

            base.DataReveived += this.tcpClient_DataReveived;
            base.PropertyChanged += this.tcpClient_PropertyChanged;

            this.aliveTimer = new System.Timers.Timer(10000);
            this.aliveTimer.AutoReset = true;
            this.aliveTimer.Elapsed += this.aliveTimer_Elapsed;
        }

        public new void Dispose() {
            base.Dispose();

            this.aliveTimer.Stop();
            this.aliveTimer.Elapsed -= this.aliveTimer_Elapsed;

            base.DataReveived -= this.tcpClient_DataReveived;
            base.PropertyChanged -= this.tcpClient_PropertyChanged;

        }

        public void Connect() {
            if (!string.IsNullOrEmpty(this.Hostname)) this.Connect(this.Hostname, 61891);
        }
        private void sendAlive() {
            this.hostIsAlive = false;
            this.sendToHost("alive");
        }
        public void SelectCourse(
            string name) {
            this.sendToHost("SelectCourse\t" + name);
        }
        public void StartCourse() { this.sendToHost("StartCourse"); }
        public void StopCourse() { this.sendToHost("StopCourse"); }
        public void ResetCourse() { this.sendToHost("ResetCourse"); }

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
                    string[] messageArray = recText.Split(new char[] { NetControlCharacter.STX, NetControlCharacter.EOT }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string item in messageArray) {
                        string[] messageContent = item.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        if (messageContent.Length > 0) {
                            if (messageContent[0] == "alive") { }
                            else if (messageContent[0] == "CourseList") {
                                this.courseNameList = new List<string>();
                                for (int i = 1; i < messageContent.Length; i++) {
                                    if (!string.IsNullOrEmpty(messageContent[i])) this.courseNameList.Add(messageContent[i]);
                                }
                                this.on_PropertyChanged("CourseNameList");
                            }
                            else if (messageContent[0] == "Course") {
                                this.SelectedCourse = messageContent[1];
                                int result;
                                if (messageContent.Length > 1 &&
                                    int.TryParse(messageContent[2], out result)) this.SelectedCourseLength = result;
                                else this.SelectedCourseLength = 0;
                                this.CourseStepIndex = 0;
                            }
                            else if (messageContent[0] == "Start") {
                                int result;
                                if (int.TryParse(messageContent[1], out result)) this.SelectedCourseLength = result;
                                else this.SelectedCourseLength = 0;
                                this.CourseStepIndex = 0;
                            }
                            else if (messageContent[0] == "Stop") { this.on_StopTimer(this, new EventArgs()); }
                            else if (messageContent[0] == "HitPanel") { this.on_HitPanel(this, new EventArgs()); }
                            else if (messageContent[0] == "Finished") { this.on_Finished(this, new EventArgs()); }
                            else if (messageContent[0] == "#") {
                                int result;
                                if (int.TryParse(messageContent[1], out result)) {
                                    if (result == 1) this.on_StartTimer(this, new EventArgs());
                                    this.CourseStepIndex = result;
                                }
                                else this.CourseStepIndex = 0;
                            }
                        }
                    }
                }
            }
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler StartTimer;
        private void on_StartTimer(object sender, EventArgs e) { Helper.raiseEvent(sender, this.StartTimer, e); }

        public event EventHandler HitPanel;
        private void on_HitPanel(object sender, EventArgs e) { Helper.raiseEvent(sender, this.HitPanel, e); }

        public event EventHandler Finished;
        private void on_Finished(object sender, EventArgs e) { Helper.raiseEvent(sender, this.Finished, e); }

        public event EventHandler StopTimer;
        private void on_StopTimer(object sender, EventArgs e) { Helper.raiseEvent(sender, this.StopTimer, e); }


        #endregion

        #region Events.Incoming

        void tcpClient_DataReveived(object sender, byte[] e) {
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

        void tcpClient_PropertyChanged(object sender, PropertyChangedEventArgs e) {
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
                        this.sendToHost("SyncData");
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
