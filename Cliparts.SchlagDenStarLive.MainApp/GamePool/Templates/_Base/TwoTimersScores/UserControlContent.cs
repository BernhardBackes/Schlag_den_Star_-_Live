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

    public partial class UserControlContent : _Base.UserControlContent {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownTwoTimersScoresPositionX.Minimum = int.MinValue;
            this.numericUpDownTwoTimersScoresPositionX.Maximum = int.MaxValue;

            this.numericUpDownTwoTimersScoresPositionY.Minimum = int.MinValue;
            this.numericUpDownTwoTimersScoresPositionY.Maximum = int.MaxValue;

            this.comboBoxTwoTimersScoresStyle.BeginUpdate();
            this.comboBoxTwoTimersScoresStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.TwoTimersScores.Styles)));
            this.comboBoxTwoTimersScoresStyle.EndUpdate();

            this.numericUpDownTwoTimersScoresStartTime.Minimum = int.MinValue;
            this.numericUpDownTwoTimersScoresStartTime.Maximum = int.MaxValue;

            this.numericUpDownTwoTimersScoresStopTime.Minimum = int.MinValue;
            this.numericUpDownTwoTimersScoresStopTime.Maximum = int.MaxValue;

            this.numericUpDownTwoTimersScoresAlarmTime1.Minimum = int.MinValue;
            this.numericUpDownTwoTimersScoresAlarmTime1.Maximum = int.MaxValue;

            this.numericUpDownTwoTimersScoresAlarmTime2.Minimum = int.MinValue;
            this.numericUpDownTwoTimersScoresAlarmTime2.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(previewPipe);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "TwoTimersScoresPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTwoTimersScoresPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TwoTimersScoresPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTwoTimersScoresPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TwoTimersScoresTimerStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxTwoTimersScoresStyle.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TwoTimersScoresStartTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTwoTimersScoresStartTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TwoTimersScoresStartTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTwoTimersScoresStartTimeText.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TwoTimersScoresStopTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTwoTimersScoresStopTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TwoTimersScoresStopTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTwoTimersScoresStopTimeText.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TwoTimersScoresAlarmTime1");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTwoTimersScoresAlarmTime1.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TwoTimersScoresAlarmTime1");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTwoTimersScoresAlarmTime1Text.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TwoTimersScoresAlarmTime2");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTwoTimersScoresAlarmTime2.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TwoTimersScoresAlarmTime2");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTwoTimersScoresAlarmTime2Text.DataBindings.Add(bind);

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

            this.numericUpDownTwoTimersScoresPositionX.DataBindings.Clear();
            this.numericUpDownTwoTimersScoresPositionY.DataBindings.Clear();
            this.comboBoxTwoTimersScoresStyle.DataBindings.Clear();
            this.numericUpDownTwoTimersScoresStartTime.DataBindings.Clear();
            this.labelTwoTimersScoresStartTimeText.DataBindings.Clear();
            this.numericUpDownTwoTimersScoresStopTime.DataBindings.Clear();
            this.labelTwoTimersScoresStopTimeText.DataBindings.Clear();
            this.numericUpDownTwoTimersScoresAlarmTime1.DataBindings.Clear();
            this.labelTwoTimersScoresAlarmTime1Text.DataBindings.Clear();
            this.numericUpDownTwoTimersScoresAlarmTime2.DataBindings.Clear();
            this.labelTwoTimersScoresAlarmTime2Text.DataBindings.Clear();
        }

        protected virtual void setTimerPreview() { }

        protected virtual void setTwoTimersScoresPreview() { }

        #endregion


        #region Events.Incoming
        #endregion

        #region Events.Controls

        protected virtual void numericUpDownTwoTimersScoresPositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.TwoTimersScoresPositionX = (int)this.numericUpDownTwoTimersScoresPositionX.Value;
                this.setTimerPreview();
            }
        }
        protected virtual void numericUpDownTwoTimersScoresPositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.TwoTimersScoresPositionY = (int)this.numericUpDownTwoTimersScoresPositionY.Value;
                this.setTimerPreview();
            }
        }
        protected virtual void comboBoxTwoTimersScoresStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.TwoTimersScores.Styles style;
            if (this.business is Business &&
                Enum.TryParse(this.comboBoxTwoTimersScoresStyle.Text, out style)) {
                this.business.TwoTimersScoresTimerStyle = style;
                this.setTimerPreview();
            }
        }
        protected virtual void numericUpDownTwoTimersScoresStartTime_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.TwoTimersScoresStartTime = (int)this.numericUpDownTwoTimersScoresStartTime.Value;
                this.setTimerPreview();
            }
        }
        protected virtual void numericUpDownTwoTimersScoresStopTime_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.TwoTimersScoresStopTime = (int)this.numericUpDownTwoTimersScoresStopTime.Value; }
        protected virtual void numericUpDownTwoTimersScoresAlarmTime1_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.TwoTimersScoresAlarmTime1 = (int)this.numericUpDownTwoTimersScoresAlarmTime1.Value; }
        protected virtual void numericUpDownTwoTimersScoresAlarmTime2_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.TwoTimersScoresAlarmTime2 = (int)this.numericUpDownTwoTimersScoresAlarmTime2.Value; }

        #endregion

    }
}
