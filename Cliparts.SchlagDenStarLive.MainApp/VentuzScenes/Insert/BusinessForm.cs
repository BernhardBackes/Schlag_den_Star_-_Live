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

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.Insert {

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

            Binding bind;

            bind = new Binding("BackColor", this.business, "SafeAreaIsVisible");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorSelected : this.BackColor; };
            this.groupBoxSafeArea.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "WelcomeIsVisible");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorSelected : this.BackColor; };
            this.groupBoxWelcome.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "CornerLogoIsVisible");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorSelected : this.BackColor; };
            this.groupBoxCornerLogo.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "BackgroundIsVisible");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorSelected : this.BackColor; };
            this.groupBoxBackground.DataBindings.Add(bind);

            this.numericUpDownTimerPositionX.Minimum = int.MinValue;
            this.numericUpDownTimerPositionX.Maximum = int.MaxValue;
            bind = new Binding("Value", this.business, "TimerPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerPositionX.DataBindings.Add(bind);

            this.numericUpDownTimerPositionY.Minimum = int.MinValue;
            this.numericUpDownTimerPositionY.Maximum = int.MaxValue;
            bind = new Binding("Value", this.business, "TimerPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerPositionY.DataBindings.Add(bind);

            this.comboBoxTimerStyle.BeginUpdate();
            this.comboBoxTimerStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.Timer.Styles)));
            this.comboBoxTimerStyle.EndUpdate();
            bind = new Binding("Text", this.business, "TimerStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxTimerStyle.DataBindings.Add(bind);

            this.numericUpDownTimerStartTime.Minimum = int.MinValue;
            this.numericUpDownTimerStartTime.Maximum = int.MaxValue;
            bind = new Binding("Value", this.business, "TimerStartTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerStartTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerStartTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerStartTimeText.DataBindings.Add(bind);

            this.numericUpDownTimerStopTime.Minimum = int.MinValue;
            this.numericUpDownTimerStopTime.Maximum = int.MaxValue;
            bind = new Binding("Value", this.business, "TimerStopTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerStopTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerStopTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerStopTimeText.DataBindings.Add(bind);

            this.numericUpDownTimerAlarmTime1.Minimum = int.MinValue;
            this.numericUpDownTimerAlarmTime1.Maximum = int.MaxValue;
            bind = new Binding("Value", this.business, "TimerAlarmTime1");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerAlarmTime1.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerAlarmTime1");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerAlarmTime1Text.DataBindings.Add(bind);

            this.numericUpDownTimerAlarmTime2.Minimum = int.MinValue;
            this.numericUpDownTimerAlarmTime2.Maximum = int.MaxValue;
            bind = new Binding("Value", this.business, "TimerAlarmTime2");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerAlarmTime2.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerAlarmTime2");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerAlarmTime2Text.DataBindings.Add(bind);

            this.userControlVentuzScenesMediaPlayer.BackColor = this.BackColor;
            this.userControlVentuzScenesMediaPlayer.Pose(this.business.MediaPlayer);
        }

        public new void Dispose() {
            this.groupBoxSafeArea.DataBindings.Clear();
            this.groupBoxWelcome.DataBindings.Clear();
            this.groupBoxCornerLogo.DataBindings.Clear();
            this.groupBoxBackground.DataBindings.Clear();
            this.numericUpDownTimerPositionX.DataBindings.Clear();
            this.numericUpDownTimerPositionY.DataBindings.Clear();
            this.comboBoxTimerStyle.DataBindings.Clear();
            this.numericUpDownTimerStartTime.DataBindings.Clear();
            this.labelTimerStartTimeText.DataBindings.Clear();
            this.numericUpDownTimerStopTime.DataBindings.Clear();
            this.labelTimerStopTimeText.DataBindings.Clear();
            this.numericUpDownTimerAlarmTime1.DataBindings.Clear();
            this.labelTimerAlarmTime1Text.DataBindings.Clear();
            this.numericUpDownTimerAlarmTime2.DataBindings.Clear();
            this.labelTimerAlarmTime2Text.DataBindings.Clear();
            this.userControlVentuzScenesMediaPlayer.Dispose();
        }


        #endregion


        #region Events.Incoming
        #endregion

        #region Events.Controls

        private void BusinessForm_FormClosing(object sender, FormClosingEventArgs e) {
            this.Dispose();
        }

        private void BusinessForm_BackColorChanged(object sender, EventArgs e) {
            this.userControlVentuzScenesMediaPlayer.BackColor = this.BackColor;
        }

        private void buttonClear_Click(object sender, EventArgs e) { this.business.Clear(); }

        private void buttonReset_Click(object sender, EventArgs e) { this.business.Reset(); }

        private void buttonSafeAreaIn_Click(object sender, EventArgs e) { this.business.SafeAreaToIn(); }

        private void buttonSafeAreaOut_Click(object sender, EventArgs e) { this.business.SafeAreaToOut(); }

        private void buttonWelcomeIn_Click(object sender, EventArgs e) { this.business.WelcomeSetIn(); }

        private void buttonWelcomeOut_Click(object sender, EventArgs e) { this.business.WelcomeSetOut(); }

        private void buttonCornerLogoIn_Click(object sender, EventArgs e) { this.business.CornerLogoToIn(); }

        private void buttonCornerLogoOut_Click(object sender, EventArgs e) { this.business.CornerLogoToOut(); }

        private void buttonBackgroundIn_Click(object sender, EventArgs e) { this.business.BackgroundSetIn(); }

        private void buttonBackgroundOut_Click(object sender, EventArgs e) { this.business.BackgroundSetOut(); }
        protected virtual void numericUpDownTimerPositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.TimerPositionX = (int)this.numericUpDownTimerPositionX.Value;
            }
        }
        protected virtual void numericUpDownTimerPositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.TimerPositionY = (int)this.numericUpDownTimerPositionY.Value;
            }
        }
        protected virtual void comboBoxTimerStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.Timer.Styles style;
            if (this.business is Business &&
                Enum.TryParse(this.comboBoxTimerStyle.Text, out style)) {
                this.business.TimerStyle = style;
            }
        }
        protected virtual void numericUpDownTimerStartTime_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.TimerStartTime = (int)this.numericUpDownTimerStartTime.Value;
            }
        }
        protected virtual void numericUpDownTimerStopTime_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.TimerStopTime = (int)this.numericUpDownTimerStopTime.Value; }
        protected virtual void numericUpDownTimerAlarmTime1_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.TimerAlarmTime1 = (int)this.numericUpDownTimerAlarmTime1.Value; }
        protected virtual void numericUpDownTimerAlarmTime2_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.TimerAlarmTime2 = (int)this.numericUpDownTimerAlarmTime2.Value; }

        private void buttonGewinnerStart_Click(object sender, EventArgs e) {
            this.business.Gewinner.SetAuto(this.textBoxGewinnerCar.Text);
            this.business.Gewinner.SetName(this.textBoxGewinnerName.Text);
            this.business.Gewinner.Start();
        }
        private void buttonGewinnerReset_Click(object sender, EventArgs e) { this.business.Gewinner.Reset(); }

        #endregion

    }
}
