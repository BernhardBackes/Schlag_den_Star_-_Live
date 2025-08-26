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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TwoTimersStartStopContinue {

    public partial class UserControlContent : _Base.UserControlContent {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownTimerPositionX.Minimum = int.MinValue;
            this.numericUpDownTimerPositionX.Maximum = int.MaxValue;

            this.numericUpDownTimerPositionY.Minimum = int.MinValue;
            this.numericUpDownTimerPositionY.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Text", this.business, "TimelineName");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.textBoxTimelineName.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "TimelineIsConnected");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? SystemColors.Control : Constants.ColorDisabled; };
            this.textBoxTimelineName.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftPlayerTransponderCode");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.textBoxLeftPlayerTransponderCode.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerTransponderCode");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.textBoxRightPlayerTransponderCode.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerPositionY.DataBindings.Add(bind);
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

            this.business.PropertyChanged -= this.business_PropertyChanged;

            this.textBoxTimelineName.DataBindings.Clear();

            this.textBoxLeftPlayerTransponderCode.DataBindings.Clear();

            this.textBoxRightPlayerTransponderCode.DataBindings.Clear();

            this.numericUpDownTimerPositionX.DataBindings.Clear();
            this.numericUpDownTimerPositionY.DataBindings.Clear();
        }

        #endregion


        #region Events.Incoming

        void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
            }
        }

        #endregion

        #region Events.Controls

        private void textBoxTimelineName_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.TimelineName = this.textBoxTimelineName.Text; }
        private void textBoxLeftPlayerTransponderCode_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPlayerTransponderCode = this.textBoxLeftPlayerTransponderCode.Text; }
        private void textBoxRightPlayerTransponderCode_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPlayerTransponderCode = this.textBoxRightPlayerTransponderCode.Text; }
        protected virtual void numericUpDownTimerPositionX_ValueChanged(object sender, EventArgs e) { this.business.TimerPositionX = (int)this.numericUpDownTimerPositionX.Value; }
        protected virtual void numericUpDownTimerPositionY_ValueChanged(object sender, EventArgs e) { this.business.TimerPositionY = (int)this.numericUpDownTimerPositionY.Value; }

        #endregion

    }

}
