using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.TwoTimersScores {

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
            //this.business.TimerAlarm1Fired += this.business_Alarm1Fired;
            //this.business.TimerAlarm2Fired += this.business_Alarm2Fired;

            Binding bind;

            bind = new Binding("Value", this.business, "LeftPlayerScore");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerScore.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerScore");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerScore.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftTimerCurrentTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelLeftPlayerTimerCurrentTime.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "LeftTimerIsRunning");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorEnabled : this.ForeColor; };
            this.labelLeftPlayerTimerCurrentTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TwoTimersScoresAlarmTime1");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.userControlRecTriggerLeftPlayerTimerAlarmTime1.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TwoTimersScoresAlarmTime2");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.userControlRecTriggerLeftPlayerTimerAlarmTime2.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightTimerCurrentTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelRightPlayerTimerCurrentTime.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "RightTimerIsRunning");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorEnabled : this.ForeColor; };
            this.labelRightPlayerTimerCurrentTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TwoTimersScoresAlarmTime1");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.userControlRecTriggerRightPlayerTimerAlarmTime1.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TwoTimersScoresAlarmTime2");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.userControlRecTriggerRightPlayerTimerAlarmTime2.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TwoTimersScoresStartTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTwoTimersScoresStartTimeText.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TwoTimersScoresStopTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTwoTimersScoresStopTimeText.DataBindings.Add(bind);

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
                //this.business.TimerAlarm1Fired -= this.business_Alarm1Fired;
                //this.business.TimerAlarm2Fired -= this.business_Alarm2Fired;
            }

            this.numericUpDownLeftPlayerScore.DataBindings.Clear();
            this.numericUpDownRightPlayerScore.DataBindings.Clear();

            this.labelLeftPlayerTimerCurrentTime.DataBindings.Clear();
            this.userControlRecTriggerLeftPlayerTimerAlarmTime1.DataBindings.Clear();
            this.userControlRecTriggerLeftPlayerTimerAlarmTime2.DataBindings.Clear();
            this.labelRightPlayerTimerCurrentTime.DataBindings.Clear();
            this.userControlRecTriggerRightPlayerTimerAlarmTime1.DataBindings.Clear();
            this.userControlRecTriggerRightPlayerTimerAlarmTime2.DataBindings.Clear();
            this.labelTwoTimersScoresStartTimeText.DataBindings.Clear();
            this.labelTwoTimersScoresStopTimeText.DataBindings.Clear();
        }

        #endregion


        #region Events.Incoming
        
        void business_Alarm1Fired(object sender, EventArgs e) {
            //if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_Alarm1Fired(sender, e)));
            //else { this.userControlRecTriggerTimerLeftPlayerAlarmTime1.StartTrigger(); }
        }

        void business_Alarm2Fired(object sender, EventArgs e) {
            //if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_Alarm2Fired(sender, e)));
            //else { this.userControlRecTriggerTimerLeftPlayerAlarmTime2.StartTrigger(); }
        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        protected virtual void numericUpDownLeftPlayerScore_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerScore = (int)this.numericUpDownLeftPlayerScore.Value; }
        protected virtual void buttonLeftPlayerAddScoreHot_01_Click(object sender, EventArgs e) {
            this.business.LeftPlayerScore++;
            this.business.Vinsert_SetScores();
            this.business.Vstage_SetScore();
        }

        protected virtual void numericUpDownRightPlayerScore_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerScore = (int)this.numericUpDownRightPlayerScore.Value; }
        protected virtual void buttonRightPlayerAddScoreHot_01_Click(object sender, EventArgs e) {
            this.business.RightPlayerScore++;
            this.business.Vinsert_SetScores();
            this.business.Vstage_SetScore();
        }


        protected virtual void buttonTwoTimersScoresIn_Click(object sender, EventArgs e) { this.business.Vinsert_TwoTimersScoresIn(); }
        protected virtual void buttonTwoTimersScoresOut_Click(object sender, EventArgs e) { this.business.Vinsert_TwoTimersScoresOut(); }

        private void buttonLeftPlayerTimerStart_Click(object sender, EventArgs e) { this.business.Vinsert_StartTopTimer(); }
        private void buttonLeftPlayerTimerStop_Click(object sender, EventArgs e) { this.business.Vinsert_StopTopTimer(); }
        private void buttonLeftPlayerTimerContinue_Click(object sender, EventArgs e) { this.business.Vinsert_ContinueTopTimer(); }
        private void buttonLeftPlayerTimerReset_Click(object sender, EventArgs e) { this.business.Vinsert_ResetTopTimer(); }

        private void buttonRightPlayerTimerStart_Click(object sender, EventArgs e) { this.business.Vinsert_StartBottomTimer(); }
        private void buttonRightPlayerTimerStop_Click(object sender, EventArgs e) { this.business.Vinsert_StopBottomTimer(); }
        private void buttonRightPlayerTimerContinue_Click(object sender, EventArgs e) { this.business.Vinsert_ContinueBottomTimer(); }
        private void buttonRightPlayerTimerReset_Click(object sender, EventArgs e) { this.business.Vinsert_ResetBottomTimer(); }

        protected virtual void buttonVinsert_TwoTimersScoresIn_Click(object sender, EventArgs e) { this.business.Vinsert_TwoTimersScoresIn(); }
        protected virtual void buttonVinsert_SetScore_Click(object sender, EventArgs e) { this.business.Vinsert_SetScores(); }
        protected virtual void buttonVinsert_TwoTimersScoresOut_Click(object sender, EventArgs e) { this.business.Vinsert_TwoTimersScoresOut(); }

        protected virtual void buttonVfullscreen_StartTimer_Click(object sender, EventArgs e) { this.business.Vfullscreen_StartTimer(); }
        protected virtual void buttonVfullscreen_StopTimer_Click(object sender, EventArgs e) { this.business.Vfullscreen_StopTimer(); }
        protected virtual void buttonVfullscreen_ResetTimer_Click(object sender, EventArgs e) { this.business.Vfullscreen_ResetTimer(); }

        protected virtual void buttonVstage_Init_Click(object sender, EventArgs e) { this.business.Vstage_Init(); }
        protected virtual void buttonVstage_SetScore_Click(object sender, EventArgs e) { this.business.Vstage_SetScore(); }

        #endregion

    }

}
