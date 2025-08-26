using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.PunchCounterApp {
    public partial class UserControlChannel : UserControl {

        #region Properties

        private Channel channel;

        #endregion


        #region Funktionen

        public UserControlChannel() {
            InitializeComponent();
        }

        public void Pose(
            Channel channel) {
            this.channel = channel;
            this.channel.PropertyChanged += this.Channel_PropertyChanged;

            this.labelIndex.Text = string.Format("channel {0}", this.channel.Index.ToString());
            this.setVoltage(this.channel.Voltage);
            this.setState(this.channel.State);
            this.setVoltagePeak(this.channel.VoltagePeak);
        }

        private void setVoltage(
            double? value) {
            if (value.HasValue) {
                this.labelVoltageValue.Enabled = true;
                this.labelVoltageValue.Text = value.Value.ToString("0.0");
            }
            else {
                this.labelVoltageValue.Enabled = false;
                this.labelVoltageValue.Text = string.Empty;
            }
        }

        private void setState(
            Channel.States value) {
            switch (value) {
                case Channel.States.Growing:
                    this.labelVoltageValue.BackColor = Color.LightGreen;
                    break;
                case Channel.States.Shrinking:
                    this.labelVoltageValue.BackColor = Color.Salmon;
                    break;
                case Channel.States.Idle:
                default:
                    this.labelVoltageValue.BackColor = SystemColors.Control;
                    break;
            }
        }

        private void setVoltagePeak(
            double? value) {
            if (value.HasValue) {
                this.labelVoltagePeakValue.Enabled = true;
                this.labelVoltagePeakValue.Text = value.Value.ToString("0.0");
            }
            else {
                this.labelVoltagePeakValue.Enabled = false;
                this.labelVoltagePeakValue.Text = string.Empty;
            }
        }

        #endregion


        #region Events.Incoming

        private void Channel_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.Channel_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "Voltage") this.setVoltage(this.channel.Voltage);
                else if (e.PropertyName == "State") this.setState(this.channel.State);
                else if (e.PropertyName == "VoltagePeak") this.setVoltagePeak(this.channel.VoltagePeak);
            }
        }

        #endregion

        #region Events.Controls

        private void buttonOpen_Click(object sender, EventArgs e) { this.channel.TryOpen(); }

        private void buttonClose_Click(object sender, EventArgs e) { this.channel.Close(); }

        #endregion
    }
}
