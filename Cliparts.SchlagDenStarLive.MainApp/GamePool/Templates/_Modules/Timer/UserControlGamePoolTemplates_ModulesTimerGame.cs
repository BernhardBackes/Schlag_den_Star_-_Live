using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Modules.Timer {
    public partial class UserControlGamePoolTemplates_ModulesTimerGame : UserControl {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGamePoolTemplates_ModulesTimerGame() {
            InitializeComponent();
        }

        public void Pose(
            Business business) {

            this.business = business;
            this.business.Alarm1Fired += this.business_Alarm1Fired;
            this.business.Alarm2Fired += this.business_Alarm2Fired;

            Binding bind;

            bind = new Binding("Text", this.business, "CurrentTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelCurrentTime.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "IsRunning");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorEnabled : this.ForeColor; };
            this.labelCurrentTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "StartTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelStartTimeText.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "ExtraTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelExtraTimeText.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "RunExtraTime");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxRunExtraTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "StopTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelStopTimeText.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "AlarmTime1");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.userControlRecTriggerAlarmTime1.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "AlarmTime2");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.userControlRecTriggerAlarmTime2.DataBindings.Add(bind);
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
                this.business.Dispose();
                this.business.Alarm1Fired -= this.business_Alarm1Fired;
                this.business.Alarm2Fired -= this.business_Alarm2Fired;
            }

            this.labelCurrentTime.DataBindings.Clear();
            this.labelStartTimeText.DataBindings.Clear();
            this.labelExtraTimeText.DataBindings.Clear();
            this.checkBoxRunExtraTime.DataBindings.Clear();
            this.labelStopTimeText.DataBindings.Clear();
            this.userControlRecTriggerAlarmTime1.DataBindings.Clear();
            this.userControlRecTriggerAlarmTime2.DataBindings.Clear();
        }

        #endregion


        #region Events.Incoming

        void business_Alarm1Fired(object sender, EventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_Alarm1Fired(sender, e)));
            else { this.userControlRecTriggerAlarmTime1.StartTrigger(); }
        }

        void business_Alarm2Fired(object sender, EventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_Alarm2Fired(sender, e)));
            else { this.userControlRecTriggerAlarmTime2.StartTrigger(); }
        }

        #endregion

        #region Events.Controls

        protected virtual void buttonIn_Click(object sender, EventArgs e) { this.business.In(); }
        protected virtual void buttonOut_Click(object sender, EventArgs e) { this.business.Out(); }
        protected virtual void buttonStart_Click(object sender, EventArgs e) { this.business.Start(); }
        protected virtual void buttonStop_Click(object sender, EventArgs e) { this.business.Stop(); }
        protected virtual void buttonContinue_Click(object sender, EventArgs e) { this.business.Continue(); }
        protected virtual void buttonReset_Click(object sender, EventArgs e) { this.business.Reset(); }
        protected virtual void checkBoxRunExtraTime_CheckedChanged(object sender, EventArgs e) { this.business.RunExtraTime = this.checkBoxRunExtraTime.Checked; }

        #endregion

    }
}
