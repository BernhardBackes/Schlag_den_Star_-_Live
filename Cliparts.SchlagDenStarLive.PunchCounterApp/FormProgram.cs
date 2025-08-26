using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.TCPCom;

using Cliparts.SchlagDenStarLive.PunchCounterApp.TCPCom;

//#region Properties
//#endregion

//#region Funktionen
//#endregion

//#region Events.Incoming
//#endregion

//#region Events.Controls
//#endregion

namespace Cliparts.SchlagDenStarLive.PunchCounterApp {
    public partial class FormProgram : Form {

        #region Properties

        private Channel channel0;
        private Channel channel1;
        private Channel channel2;
        private Channel channel3;

        private Player leftPlayer;
        private Player rightPlayer;

        private double threshold = 50;
        public double Threshold {
            get { return this.threshold; }
            set {
                if (this.threshold != value) {
                    if (value <= 0.1) this.threshold = 0.1;
                    else this.threshold = value;
                    this.numericUpDownThreshold.Value = Convert.ToDecimal(this.threshold);
                    this.leftPlayer.Threshold = this.threshold;
                    this.rightPlayer.Threshold = this.threshold;
                }
            }
        }

        private bool enabled = false;

        private TCPServer tcpServer;

        #endregion


        #region Funktionen
        public FormProgram() {
            InitializeComponent();

            this.BackColor = ClipartsColors.DE_DARKBLUE;
            this.Text = String.Format("{0} - Version {1}", Application.ProductName, Application.ProductVersion);

            this.channel0 = new Channel(WindowsFormsSynchronizationContext.Current, 0);
            this.channel1 = new Channel(WindowsFormsSynchronizationContext.Current, 1);
            this.channel2 = new Channel(WindowsFormsSynchronizationContext.Current, 2);
            this.channel3 = new Channel(WindowsFormsSynchronizationContext.Current, 3);

            this.leftPlayer = new Player(WindowsFormsSynchronizationContext.Current, "left player", this.channel0, this.channel1);
            this.leftPlayer.Threshold = this.Threshold;
            this.leftPlayer.PropertyChanged += this.Player_PropertyChanged;

            this.rightPlayer = new Player(WindowsFormsSynchronizationContext.Current, "right player", this.channel2, this.channel3);
            this.rightPlayer.Threshold = this.Threshold;
            this.rightPlayer.PropertyChanged += this.Player_PropertyChanged;

            this.userControlLeftPlayer.Pose(this.leftPlayer);
            this.userControlLeftPlayer.BackColor = this.BackColor;

            this.userControlRightPlayer.Pose(this.rightPlayer);
            this.userControlRightPlayer.BackColor = this.BackColor;

            this.numericUpDownThreshold.Value = Convert.ToDecimal(this.Threshold);

            this.tcpServer = new TCPServer(WindowsFormsSynchronizationContext.Current);
            this.tcpServer.DataReveived += this.tcpServer_DataReveived;
            this.tcpServer.PropertyChanged += this.tcpServer_PropertyChanged;
            this.tcpServer.StartListening(61882);

            this.channel0.TryOpen();
            this.channel1.TryOpen();
            this.channel2.TryOpen();
            this.channel3.TryOpen();
        }

        private void Start() {
            this.enabled = true;
            this.leftPlayer.Enabled = true;
            this.rightPlayer.Enabled = true;
        }
        private void Stop() {
            this.enabled = false; ;
            this.leftPlayer.Enabled = false;
            this.rightPlayer.Enabled = false;
        }
        private void Reset() {
            this.Stop();
            this.leftPlayer.Counter = 0;
            this.rightPlayer.Counter = 0;
        }

        private void sendAlive() { this.sendToHost("alive"); }

        private void sendCounter(
            int leftCounter,
            int rightCounter) {
            this.sendToHost(string.Format("Counter{0}{1}{0}{2}", NetControlCharacter.HT.ToString(), leftCounter.ToString(), rightCounter.ToString()));
        }

        private void sendToHost(
            string value) {
            if (!string.IsNullOrEmpty(value)) {
                string sendText = NetControlCharacter.STX.ToString() + value + NetControlCharacter.EOT.ToString();
                byte[] data = Encoding.ASCII.GetBytes(sendText);
                this.tcpServer.SendData(data);
            }
        }

        private void parseReceivedData(
            byte[] data) {
            if (data is byte[] && data.Length > 2) {
                string recText = Encoding.ASCII.GetString(data);
                if (recText.StartsWith(NetControlCharacter.STX.ToString()) &&
                    recText.EndsWith(NetControlCharacter.EOT.ToString())) {
                    string[] recArray = recText.Split(new char[] { NetControlCharacter.STX, NetControlCharacter.EOT }, StringSplitOptions.RemoveEmptyEntries);
                    if (recArray.Length > 0) {
                        foreach (string item in recArray) {
                            string[] itemArray = item.Split(new char[] { NetControlCharacter.HT }, StringSplitOptions.RemoveEmptyEntries);
                            if (itemArray.Length >= 1) {
                                if (itemArray[0] == "Start") this.Start();
                                else if (itemArray[0] == "Stop") this.Stop();
                                else if (itemArray[0] == "Reset") this.Reset();
                                else if (itemArray[0] == "Threshold") {
                                    double result;
                                    if (itemArray.Length >= 2 &&
                                        Double.TryParse(itemArray[1].Replace(",", "."), out result)) this.Threshold = result;
                                }
                            }
                        }
                    }
                }
            }
            this.sendAlive();
        }



        #endregion


        #region Events.Incoming

        private void Player_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.Player_PropertyChanged(sender, e)));
            else {
                if (this.enabled &&
                    e.PropertyName == "Counter") this.sendCounter(this.leftPlayer.Counter, this.rightPlayer.Counter);
            }
        }

        void tcpServer_DataReveived(object sender, byte[] e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.tcpServer_DataReveived(sender, e)));
            else {
                if (e.Length > 0) {
                    int i = e.Length - 1;
                    while (e[i] == 0) --i;
                    byte[] bar = new byte[i + 1];
                    Array.Copy(e, bar, i + 1);
                    this.parseReceivedData(bar);
                }
            }
        }

        void tcpServer_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.tcpServer_PropertyChanged(sender, e)));
            else {
                //if (e.PropertyName == "WorkerList") this.pictureBoxLink.Visible = this.tcpServer.WorkerList.Length > 0;
            }
        }

        #endregion

        #region Events.Controls

        private void numericUpDownThreshold_ValueChanged(object sender, EventArgs e) { this.Threshold = (double)this.numericUpDownThreshold.Value; }
        private void buttonStart_Click(object sender, EventArgs e) { this.Start(); }
        private void buttonStop_Click(object sender, EventArgs e) { this.Stop(); }
        private void buttonReset_Click(object sender, EventArgs e) { this.Reset(); }


        #endregion
    }
}
