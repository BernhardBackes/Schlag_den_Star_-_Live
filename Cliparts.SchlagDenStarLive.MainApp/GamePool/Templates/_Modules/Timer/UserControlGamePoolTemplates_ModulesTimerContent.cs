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
    public partial class UserControlGamePoolTemplates_ModulesTimerContent : UserControl {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGamePoolTemplates_ModulesTimerContent() {
            InitializeComponent();

            this.numericUpDownPositionX.Minimum = int.MinValue;
            this.numericUpDownPositionX.Maximum = int.MaxValue;

            this.numericUpDownPositionY.Minimum = int.MinValue;
            this.numericUpDownPositionY.Maximum = int.MaxValue;

            this.comboBoxStyle.BeginUpdate();
            this.comboBoxStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.Timer.Styles)));
            this.comboBoxStyle.EndUpdate();

            this.numericUpDownScaling.Minimum = decimal.MinValue;
            this.numericUpDownScaling.Maximum = decimal.MaxValue;

            this.numericUpDownStartTime.Minimum = int.MinValue;
            this.numericUpDownStartTime.Maximum = int.MaxValue;

            this.numericUpDownExtraTime.Minimum = int.MinValue;
            this.numericUpDownExtraTime.Maximum = int.MaxValue;

            this.numericUpDownStopTime.Minimum = int.MinValue;
            this.numericUpDownStopTime.Maximum = int.MaxValue;

            this.numericUpDownAlarmTime1.Minimum = int.MinValue;
            this.numericUpDownAlarmTime1.Maximum = int.MaxValue;

            this.numericUpDownAlarmTime2.Minimum = int.MinValue;
            this.numericUpDownAlarmTime2.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business) {

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "PositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "PositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "Style");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxStyle.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "Scaling");
            bind.Format += (s, e) => { e.Value = (double)e.Value; };
            this.numericUpDownScaling.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "StartTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownStartTime.DataBindings.Add(bind);
            bind = new Binding("Text", this.business, "StartTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerStartTimeText.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "ExtraTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownExtraTime.DataBindings.Add(bind);
            bind = new Binding("Text", this.business, "ExtraTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerExtraTimeText.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "StopTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownStopTime.DataBindings.Add(bind);
            bind = new Binding("Text", this.business, "StopTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerStopTimeText.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "AlarmTime1");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownAlarmTime1.DataBindings.Add(bind);
            bind = new Binding("Text", this.business, "AlarmTime1");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerAlarmTime1Text.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "AlarmTime2");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownAlarmTime2.DataBindings.Add(bind);
            bind = new Binding("Text", this.business, "AlarmTime2");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerAlarmTime2Text.DataBindings.Add(bind);
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

            this.numericUpDownPositionX.DataBindings.Clear();
            this.numericUpDownPositionY.DataBindings.Clear();
            this.comboBoxStyle.DataBindings.Clear();
            this.numericUpDownScaling.DataBindings.Clear();
            this.numericUpDownStartTime.DataBindings.Clear();
            this.labelTimerStartTimeText.DataBindings.Clear();
            this.numericUpDownExtraTime.DataBindings.Clear();
            this.labelTimerExtraTimeText.DataBindings.Clear();
            this.numericUpDownStopTime.DataBindings.Clear();
            this.labelTimerStopTimeText.DataBindings.Clear();
            this.numericUpDownAlarmTime1.DataBindings.Clear();
            this.labelTimerAlarmTime1Text.DataBindings.Clear();
            this.numericUpDownAlarmTime2.DataBindings.Clear();
            this.labelTimerAlarmTime2Text.DataBindings.Clear();
        }

        #endregion


        #region Events.Incoming
        #endregion

        #region Events.Controls

        protected virtual void numericUpDownTimerPositionX_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.PositionX = (int)this.numericUpDownPositionX.Value; }
        protected virtual void numericUpDownTimerPositionY_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.PositionY = (int)this.numericUpDownPositionY.Value; }   
        protected virtual void comboBoxTimerStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.Timer.Styles style;
            if (this.business is Business &&
                Enum.TryParse(this.comboBoxStyle.Text, out style)) {
                this.business.Style = style;
            }
        }
        protected virtual void numericUpDownScaling_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.Scaling = (double)this.numericUpDownScaling.Value; }
        protected virtual void numericUpDownTimerStartTime_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.StartTime = (int)this.numericUpDownStartTime.Value; }
        protected virtual void numericUpDownTimerExtraTime_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.ExtraTime = (int)this.numericUpDownExtraTime.Value; }
        protected virtual void numericUpDownTimerStopTime_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.StopTime = (int)this.numericUpDownStopTime.Value; }
        protected virtual void numericUpDownTimerAlarmTime1_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.AlarmTime1 = (int)this.numericUpDownAlarmTime1.Value; }
        protected virtual void numericUpDownTimerAlarmTime2_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.AlarmTime2 = (int)this.numericUpDownAlarmTime2.Value; }

        #endregion
    }
}
