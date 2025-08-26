using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.Tools.Base;

using Cliparts.SchlagDenStarLive.MainApp.Settings;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.Fullscreen {

    public partial class BusinessForm : Form {

        #region Properties

        Business business;

        private bool adjustingGUI = false;

        #endregion


        #region Funktionen

        public BusinessForm(
            Business business) {

            InitializeComponent();

            this.BackColor = ClipartsColors.DE_DARKBLUE;

            this.business = business;
            this.business.Timer.StopFired += this.clock_StopTriggered;
            this.business.Timer.Alarm1Fired += this.clock_Alarm1Triggered;
            this.business.Timer.Alarm2Fired += this.clock_Alarm2Triggered;

            this.comboBoxClockStyle.BeginUpdate();
            this.comboBoxClockStyle.Items.Clear();
            this.comboBoxClockStyle.Items.AddRange(Enum.GetNames(typeof(Clock.Styles)));
            this.comboBoxClockStyle.EndUpdate();

            this.numericUpDownClockStart.Minimum = int.MinValue;
            this.numericUpDownClockStart.Maximum = int.MaxValue;

            this.numericUpDownClockStop.Minimum = int.MinValue;
            this.numericUpDownClockStop.Maximum = int.MaxValue;

            this.numericUpDownClockAlarm1.Minimum = int.MinValue;
            this.numericUpDownClockAlarm1.Maximum = int.MaxValue;

            this.numericUpDownClockAlarm2.Minimum = int.MinValue;
            this.numericUpDownClockAlarm2.Maximum = int.MaxValue;

            Binding bind;

            bind = new Binding("BackColor", this.business, "BackloopStatus");
            bind.Format += this.bind_groupBox_BackColor;
            this.groupBoxBackloop.DataBindings.Add(bind);

            bind = new Binding("Text", this.business.Timer, "CurrentTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelClockCurrentTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business.Timer, "Style");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxClockStyle.DataBindings.Add(bind);

            bind = new Binding("Value", this.business.Timer, "StartTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownClockStart.DataBindings.Add(bind);

            bind = new Binding("Text", this.business.Timer, "StartTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelClockStartValue.DataBindings.Add(bind);

            bind = new Binding("Value", this.business.Timer, "StopTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownClockStop.DataBindings.Add(bind);

            bind = new Binding("Text", this.business.Timer, "StopTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.userControlRecTriggerClockStop.DataBindings.Add(bind);

            bind = new Binding("Value", this.business.Timer, "AlarmTime1");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownClockAlarm1.DataBindings.Add(bind);

            bind = new Binding("Text", this.business.Timer, "AlarmTime1");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.userControlRecTriggerClockAlarm1.DataBindings.Add(bind);

            bind = new Binding("Value", this.business.Timer, "AlarmTime2");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownClockAlarm2.DataBindings.Add(bind);

            bind = new Binding("Text", this.business.Timer, "AlarmTime2");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.userControlRecTriggerClockAlarm2.DataBindings.Add(bind);

            this.userControlVentuzScenesMediaPlayer.BackColor = this.BackColor;
            this.userControlVentuzScenesMediaPlayer.Pose(this.business.MediaPlayer);
        }

        public new void Dispose() {
            this.business.Timer.StopFired -= this.clock_StopTriggered;
            this.business.Timer.Alarm1Fired -= this.clock_Alarm1Triggered;
            this.business.Timer.Alarm2Fired -= this.clock_Alarm2Triggered;

            this.groupBoxBackloop.DataBindings.Clear();
            this.labelClockCurrentTime.DataBindings.Clear();
            this.comboBoxClockStyle.DataBindings.Clear();
            this.numericUpDownClockStart.DataBindings.Clear();
            this.labelClockStartValue.DataBindings.Clear();
            this.numericUpDownClockStop.DataBindings.Clear();
            this.userControlRecTriggerClockStop.DataBindings.Clear();
            this.numericUpDownClockAlarm1.DataBindings.Clear();
            this.userControlRecTriggerClockAlarm1.DataBindings.Clear();
            this.numericUpDownClockAlarm2.DataBindings.Clear();
            this.userControlRecTriggerClockAlarm2.DataBindings.Clear();

            this.userControlVentuzScenesMediaPlayer.Dispose();
        }

        #endregion


        #region Events.Incoming

        void clock_StopTriggered(object sender, EventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.clock_StopTriggered(sender, e)));
            else { this.userControlRecTriggerClockStop.StartTrigger(); }
        }

        void clock_Alarm1Triggered(object sender, EventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.clock_Alarm1Triggered(sender, e)));
            else { this.userControlRecTriggerClockAlarm1.StartTrigger(); }
        }

        void clock_Alarm2Triggered(object sender, EventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.clock_Alarm2Triggered(sender, e)));
            else { this.userControlRecTriggerClockAlarm2.StartTrigger(); }
        }

        #endregion

        #region Events.Controls

        private void BusinessForm_FormClosing(object sender, FormClosingEventArgs e) {
            this.Dispose();
        }

        private void BusinessForm_BackColorChanged(object sender, EventArgs e) {
            this.userControlVentuzScenesMediaPlayer.BackColor = this.BackColor;
        }

        void bind_groupBox_BackColor(object sender, ConvertEventArgs e) {
            switch ((LoopStates)e.Value) {
                case LoopStates.Loaded:
                    e.Value = this.BackColor;
                    break;
                case LoopStates.Playing:
                    e.Value = Constants.ColorEnabled;
                    break;
                case LoopStates.Idle:
                default:
                    e.Value = Constants.ColorDisabled;
                    break;
            }
        }

        private void buttonAllOut_Click(object sender, EventArgs e) { this.business.Clear(); }

        private void buttonRestartBackloop_Click(object sender, EventArgs e) { this.business.RestartBackloop(); }
        private void buttonFadeBackloopIn_Click(object sender, EventArgs e) { this.business.FadeBackloopIn(); }
        private void buttonFadeBackloopOut_Click(object sender, EventArgs e) { this.business.FadeBackloopOut(); }

        private void buttonShowGameboard_Click(object sender, EventArgs e) { this.business.ShowGameboard(); }

        private void buttonShowClock_Click(object sender, EventArgs e) { this.business.ShowTimer(); }
        private void comboBoxClockStyle_SelectedIndexChanged(object sender, EventArgs e) {
            Clock.Styles style;
            if (Enum.TryParse(this.comboBoxClockStyle.Text, out style)) this.business.Timer.SetStyle(style);
        }
        private void numericUpDownClockStart_ValueChanged(object sender, EventArgs e) { this.business.Timer.SetStartTime((int)this.numericUpDownClockStart.Value); }
        private void numericUpDownClockStop_ValueChanged(object sender, EventArgs e) { this.business.Timer.SetStopTime((int)this.numericUpDownClockStop.Value); }
        private void numericUpDownClockAlarm1_ValueChanged(object sender, EventArgs e) { this.business.Timer.SetAlarmTime1((int)this.numericUpDownClockAlarm1.Value); }
        private void numericUpDownClockAlarm2_ValueChanged(object sender, EventArgs e) { this.business.Timer.SetAlarmTime2((int)this.numericUpDownClockAlarm2.Value); }
        private void buttonClockStart_Click(object sender, EventArgs e) { this.business.Timer.StartTimer(); }
        private void buttonClockStop_Click(object sender, EventArgs e) { this.business.Timer.StopTimer(); }
        private void buttonClockContinue_Click(object sender, EventArgs e) { this.business.Timer.ContinueTimer(); }
        private void buttonClockReset_Click(object sender, EventArgs e) { this.business.Timer.ResetTimer(); }

        private void buttonShowGame_Click(object sender, EventArgs e) { this.business.ShowGame(); }

        private void buttonShowFreetext_Click(object sender, EventArgs e) {
            this.business.Freetext.SetTextValue(this.textBoxFreetext.Text);
            this.business.Freetext.SetTextColor(Color.Black);
            this.business.ShowFreetext(); 
        }
        private void textBoxFreetext_TextChanged(object sender, EventArgs e) { this.business.Freetext.SetTextValue(this.textBoxFreetext.Text); }

        #endregion
    }
}
