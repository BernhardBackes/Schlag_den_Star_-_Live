using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.BuzzerStartTimerScore {

    public partial class UserControlGame : _Base.UserControlGame {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;

            this.business = business;
            this.business.TimerAlarm1Fired += this.business_Alarm1Fired;
            this.business.TimerAlarm2Fired += this.business_Alarm2Fired;

            Binding bind;

            bind = new Binding("Value", this.business, "LeftPlayerScore");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerScore.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerScore");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerScore.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "FlipPlayers");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxFlipPlayers.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerCurrentTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerCurrentTime.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "TimerIsRunning");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorEnabled : this.ForeColor; };
            this.labelTimerCurrentTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerStartTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerStartTimeText.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerExtraTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerExtraTimeText.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "RunExtraTime");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxTimerRunExtraTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerStopTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerStopTimeText.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerAlarmTime1");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.userControlRecTriggerTimerAlarmTime1.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerAlarmTime2");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.userControlRecTriggerTimerAlarmTime2.DataBindings.Add(bind);

            bind = new Binding("Enabled", this.business, "ShowFullscreenTimer");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.buttonVfullscreen_ShowTimer.DataBindings.Add(bind);

            bind = new Binding("Enabled", this.business, "ShowFullscreenTimer");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.buttonVfullscreen_StartTimer.DataBindings.Add(bind);

            bind = new Binding("Enabled", this.business, "ShowFullscreenTimer");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.buttonVfullscreen_StopTimer.DataBindings.Add(bind);

            bind = new Binding("Enabled", this.business, "ShowFullscreenTimer");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.buttonVfullscreen_ResetTimer.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "IOUnitName");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxIOUnitName.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitStatus");
            bind.Format += this.bind_textBoxIOUnitName_BackColor;
            this.textBoxIOUnitName.DataBindings.Add(bind);

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

            if (this.business is Business) {
                this.business.TimerAlarm1Fired -= this.business_Alarm1Fired;
                this.business.TimerAlarm2Fired -= this.business_Alarm2Fired;
            }

            this.numericUpDownLeftPlayerScore.DataBindings.Clear();
            this.numericUpDownRightPlayerScore.DataBindings.Clear();

            this.checkBoxFlipPlayers.DataBindings.Clear();

            this.labelTimerCurrentTime.DataBindings.Clear();
            this.labelTimerStartTimeText.DataBindings.Clear();
            this.labelTimerExtraTimeText.DataBindings.Clear();
            this.checkBoxTimerRunExtraTime.DataBindings.Clear();
            this.labelTimerStopTimeText.DataBindings.Clear();
            this.userControlRecTriggerTimerAlarmTime1.DataBindings.Clear();
            this.userControlRecTriggerTimerAlarmTime2.DataBindings.Clear();

            this.textBoxIOUnitName.DataBindings.Clear();

            this.buttonVfullscreen_ShowTimer.DataBindings.Clear();
            this.buttonVfullscreen_StartTimer.DataBindings.Clear();
            this.buttonVfullscreen_StopTimer.DataBindings.Clear();
            this.buttonVfullscreen_ResetTimer.DataBindings.Clear();
        }

        private void setBuzzeredPlayer() {
            switch (this.business.BuzzeredPlayer) {
                case Content.Gameboard.PlayerSelection.LeftPlayer:
                    this.buttonLeftPlayerBuzzer.BackColor = Constants.ColorBuzzered;
                    this.buttonRightPlayerBuzzer.UseVisualStyleBackColor = true;
                    break;
                case Content.Gameboard.PlayerSelection.RightPlayer:
                    this.buttonLeftPlayerBuzzer.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerBuzzer.BackColor = Constants.ColorBuzzered;
                    break;
                case Content.Gameboard.PlayerSelection.NotSelected:
                default:
                    this.buttonLeftPlayerBuzzer.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerBuzzer.UseVisualStyleBackColor = true;
                    break;
            }
        }

        private void bind_textBoxIOUnitName_BackColor(object sender, ConvertEventArgs e) {
            switch ((BuzzerUnitStates)e.Value) {
                case BuzzerUnitStates.NotAvailable:
                    e.Value = Constants.ColorMissing;
                    break;
                case BuzzerUnitStates.Missing:
                    e.Value = Constants.ColorMissing;
                    break;
                case BuzzerUnitStates.Disconnected:
                    e.Value = Constants.ColorDisabled;
                    break;
                case BuzzerUnitStates.Connecting:
                    e.Value = Constants.ColorEnabling;
                    break;
                case BuzzerUnitStates.Connected:
                    e.Value = SystemColors.Control;
                    break;
                case BuzzerUnitStates.Locked:
                    e.Value = SystemColors.Control;
                    break;
                case BuzzerUnitStates.BuzzerMode:
                    e.Value = Constants.ColorEnabled;
                    break;
                case BuzzerUnitStates.EventMode:
                    e.Value = Constants.ColorBuzzered;
                    break;
                default:
                    break;
            }
        }

        #endregion


        #region Events.Incoming

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.business_PropertyChanged(sender, e);
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "BuzzeredPlayer") this.setBuzzeredPlayer();
            }

        }

        void business_Alarm1Fired(object sender, EventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_Alarm1Fired(sender, e)));
            else { this.userControlRecTriggerTimerAlarmTime1.StartTrigger(); }
        }

        void business_Alarm2Fired(object sender, EventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_Alarm2Fired(sender, e)));
            else { this.userControlRecTriggerTimerAlarmTime2.StartTrigger(); }
        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        protected virtual void numericUpDownLeftPlayerScore_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerScore = (int)this.numericUpDownLeftPlayerScore.Value; }
        protected virtual void buttonLeftPlayerAddScoreHot_01_Click(object sender, EventArgs e) {
            this.business.LeftPlayerScore++;
            this.business.Vinsert_SetScore();
            this.business.Vstage_SetScore();
        }
        protected void buttonLeftPlayerBuzzer_Click(object sender, EventArgs e) { this.business.DoBuzzer(Content.Gameboard.PlayerSelection.LeftPlayer); }

        protected virtual void numericUpDownRightPlayerScore_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerScore = (int)this.numericUpDownRightPlayerScore.Value; }
        protected virtual void buttonRightPlayerAddScoreHot_01_Click(object sender, EventArgs e) {
            this.business.RightPlayerScore++;
            this.business.Vinsert_SetScore();
            this.business.Vstage_SetScore();
        }
        protected void buttonRightPlayerBuzzer_Click(object sender, EventArgs e) { this.business.DoBuzzer(Content.Gameboard.PlayerSelection.RightPlayer); }

        private void checkBoxFlipPlayers_CheckedChanged(object sender, EventArgs e) { this.business.FlipPlayers = this.checkBoxFlipPlayers.Checked; }

        protected virtual void buttonTimerIn_Click(object sender, EventArgs e) { this.business.Vinsert_TimerIn(); }
        protected virtual void buttonTimerOut_Click(object sender, EventArgs e) { this.business.Vinsert_TimerOut(); }
        protected virtual void buttonTimerStart_Click(object sender, EventArgs e) { 
            this.business.Vinsert_StartTimer();
            this.business.Vfullscreen_StartTimer();
        }
        protected virtual void buttonTimerStop_Click(object sender, EventArgs e) { 
            this.business.Vinsert_StopTimer();
            this.business.Vfullscreen_StopTimer();
        }
        protected virtual void buttonTimerContinue_Click(object sender, EventArgs e) { 
            this.business.Vinsert_ContinueTimer();
            this.business.Vfullscreen_ContinueTimer();
        }
        protected virtual void buttonTimerReset_Click(object sender, EventArgs e) { 
            this.business.Vinsert_ResetTimer();
            this.business.Vfullscreen_ResetTimer();
        }
        protected virtual void checkBoxTimerRunExtraTime_CheckedChanged(object sender, EventArgs e) { this.business.RunExtraTime = this.checkBoxTimerRunExtraTime.Checked; }

        protected void buttonReleaseBuzzer_Click(object sender, EventArgs e) { if (this.business is Business) this.business.ReleaseBuzzer(); }
        protected void buttonLockBuzzer_Click(object sender, EventArgs e) { if (this.business is Business) this.business.LockBuzzer(); }

        protected virtual void buttonVinsert_ScoreIn_Click(object sender, EventArgs e) { this.business.Vinsert_ScoreIn(); }
        protected virtual void buttonVinsert_SetScore_Click(object sender, EventArgs e) { this.business.Vinsert_SetScore(); }
        protected virtual void buttonVinsert_ScoreOut_Click(object sender, EventArgs e) { this.business.Vinsert_ScoreOut(); }
        protected virtual void buttonVinsert_TimerIn_Click(object sender, EventArgs e) { this.business.Vinsert_TimerIn(); }
        protected virtual void buttonVinsert_StartTimer_Click(object sender, EventArgs e) { this.business.Vinsert_StartTimer(); }
        protected virtual void buttonVinsert_StopTimer_Click(object sender, EventArgs e) { this.business.Vinsert_StopTimer(); }
        protected virtual void buttonVinsert_TimerOut_Click(object sender, EventArgs e) { this.business.Vinsert_TimerOut(); }

        protected virtual void buttonVfullscreen_StartTimer_Click(object sender, EventArgs e) { this.business.Vfullscreen_StartTimer(); }
        protected virtual void buttonVfullscreen_StopTimer_Click(object sender, EventArgs e) { this.business.Vfullscreen_StopTimer(); }
        protected virtual void buttonVfullscreen_ResetTimer_Click(object sender, EventArgs e) { this.business.Vfullscreen_ResetTimer(); }

        protected virtual void buttonVstage_Init_Click(object sender, EventArgs e) { this.business.Vstage_Init(); }
        protected virtual void buttonVstage_SetScore_Click(object sender, EventArgs e) { this.business.Vstage_SetScore(); }

        private void buttonGame_ReleaseBuzzer_Click(object sender, EventArgs e) { this.business.ReleaseBuzzer(); }
        private void buttonGame_LockBuzzer_Click(object sender, EventArgs e) { this.business.LockBuzzer(); }

        #endregion

    }

}
