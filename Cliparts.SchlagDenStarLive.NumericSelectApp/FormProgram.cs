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

using Cliparts.SchlagDenStarLive.NumericSelectApp.TCPCom;

namespace Cliparts.SchlagDenStarLive.NumericSelectApp {

    public partial class FormProgram : Form {

        #region Properties

        private FormDisplay display;

        private int buzzerCount = 20;
        private bool zeroIncluded = false;

        private int _value;
        private int value {
            get { return this._value; }
            set { this._value = value; }
        }


        private bool idle = false;
        private bool locked = false;

        private Image[] activeImages = new Image[] {
            Properties.Resources.Buzzer_00,
            Properties.Resources.Buzzer_01,
            Properties.Resources.Buzzer_02,
            Properties.Resources.Buzzer_03,
            Properties.Resources.Buzzer_04,
            Properties.Resources.Buzzer_05,
            Properties.Resources.Buzzer_06,
            Properties.Resources.Buzzer_07,
            Properties.Resources.Buzzer_08,
            Properties.Resources.Buzzer_09,
            Properties.Resources.Buzzer_10,
            Properties.Resources.Buzzer_11,
            Properties.Resources.Buzzer_12,
            Properties.Resources.Buzzer_13,
            Properties.Resources.Buzzer_14,
            Properties.Resources.Buzzer_15,
            Properties.Resources.Buzzer_16,
            Properties.Resources.Buzzer_17,
            Properties.Resources.Buzzer_18,
            Properties.Resources.Buzzer_19,
            Properties.Resources.Buzzer_20
        };

        private Image[] passivImages = new Image[] {
            Properties.Resources.Buzzer_00_out,
            Properties.Resources.Buzzer_01_out,
            Properties.Resources.Buzzer_02_out,
            Properties.Resources.Buzzer_03_out,
            Properties.Resources.Buzzer_04_out,
            Properties.Resources.Buzzer_05_out,
            Properties.Resources.Buzzer_06_out,
            Properties.Resources.Buzzer_07_out,
            Properties.Resources.Buzzer_08_out,
            Properties.Resources.Buzzer_09_out,
            Properties.Resources.Buzzer_10_out,
            Properties.Resources.Buzzer_11_out,
            Properties.Resources.Buzzer_12_out,
            Properties.Resources.Buzzer_13_out,
            Properties.Resources.Buzzer_14_out,
            Properties.Resources.Buzzer_15_out,
            Properties.Resources.Buzzer_16_out,
            Properties.Resources.Buzzer_17_out,
            Properties.Resources.Buzzer_18_out,
            Properties.Resources.Buzzer_19_out,
            Properties.Resources.Buzzer_20_out
        };

        private TCPServer tcpServer;

        #endregion


        #region Funktionen

        public FormProgram(
            int buzzerCount,
            bool zeroIncluded) {

            InitializeComponent();

            this.buzzerCount = buzzerCount;
            this.zeroIncluded = zeroIncluded;

            this.Size = new Size(1024,768);
            this.Location = new Point(0, 0);

            this.display = new FormDisplay();
            this.display.Location = new Point(this.Location.X + this.Size.Width, 0);

            this.pictureBoxLogo.Size = this.Size;
            this.pictureBoxLogo.Location = new Point(0, 0);
            this.pictureBoxLogo.SendToBack();

            this.panelInput.Size = this.Size;
            this.panelInput.Location = new Point(0, 0);
            this.panelInput.BringToFront();

            this.panelButton_00.Left = this.panelButton_13.Left;
            this.panelButton_00.Top = this.panelButton_13.Top;

            //Cursor.Hide();

            this.setUnlocked();

            this.BringToFront();

            this.display.Show();

            this.tcpServer = new TCPServer(WindowsFormsSynchronizationContext.Current);
            this.tcpServer.DataReveived += this.tcpServer_DataReveived;
            this.tcpServer.PropertyChanged += this.tcpServer_PropertyChanged;
            this.tcpServer.StartListening(61881);

        }

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);

            this.tcpServer.StoptListening();

        }

        private void setIdle() {
            this.idle = true;
            this.locked = true;
            this.setControls();
            this.display.SetBuzzer(-1);
        }
        private void setActive() {
            this.idle = false;
            this.locked = true;
            this.value = -1;
            this.setControls();
            this.display.SetBuzzer(-1);
        }
        private void setLocked() {
            this.idle = false;
            this.locked = true;
            this.setControls();
            this.display.SetBuzzer(-1);
        }
        private void setUnlocked() {
            this.idle = false;
            this.locked = false;
            this.value = -1;
            this.setControls();
            this.display.SetBuzzer(-1);
        }

        private void setControls() {

            if (this.idle) {
                this.panelInput.Visible = false;
            }
            else {
                this.panelInput.Visible = true;
                string key;
                Panel panel;
                PictureBox pictureBox;
                for (int i = 0; i <= 20; i++) {
                    key = "panelButton_" + i.ToString("00");
                    panel = this.panelInput.Controls[key] as Panel;
                    if (panel is Panel) {
                        if (this.value == i) panel.BackColor = Color.LimeGreen;
                        else panel.BackColor = Color.Transparent;
                    }
                    panel.Visible = i <= this.buzzerCount;

                    key = "pictureBoxButton_" + i.ToString("00");
                    pictureBox = panel.Controls[key] as PictureBox;
                    if (pictureBox is PictureBox) {
                        if (this.locked) {
                            if (this.value == i) pictureBox.Image = this.activeImages[i];
                            else pictureBox.Image = this.passivImages[i];
                        }
                        else pictureBox.Image = this.activeImages[i];
                        pictureBox.Enabled = !this.locked;
                    }
                    if (i == 0) pictureBox.Visible = this.zeroIncluded;
                    else pictureBox.Visible = i <= this.buzzerCount;
                }

                if (this.locked) this.buttonOK.Enabled = false;
                else this.buttonOK.Enabled = this.value >= 0;
                Helper.setControlBackColor(this.buttonOK, true, Color.LimeGreen);

            }
        }

        private void sendAlive() { this.sendToHost("alive"); }

        private void sendValue(
            int? value) {
            if (value.HasValue) this.sendToHost(value.Value.ToString());
            else this.sendToHost("off");
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
                                if (itemArray[0] == "SetIdle") this.setIdle();
                                else if (itemArray[0] == "SetActive") this.setActive();
                                else if (itemArray[0] == "SetLocked") this.setLocked();
                                else if (itemArray[0] == "SetUnlocked") this.setUnlocked();
                                else if (itemArray[0] == "SetInputValue") {
                                    int result;
                                    if (itemArray.Length >= 2 &&
                                        int.TryParse(itemArray[1], out result)) this.display.SetBuzzer(result);
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

        private void FormProgram_Resize(object sender, EventArgs e) {
        }

        private void pictureBoxButton_Click(object sender, EventArgs e) {
            int result;
            if (Helper.tryParseIndexFromControl(sender as Control, out result)) {
                this.value = result;
                this.setControls();
            }
        }

        private void buttonOK_Click(object sender, EventArgs e) {
            if (this.value >= 0) {
                this.sendValue(this.value);
                this.setLocked();
            }
        }

        #endregion

    }
}
