using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.TargetCounter {

    public partial class UserControlContent : _Base.UserControlContent {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownTargetCounterPositionX.Minimum = int.MinValue;
            this.numericUpDownTargetCounterPositionX.Maximum = int.MaxValue;

            this.numericUpDownTargetCounterPositionY.Minimum = int.MinValue;
            this.numericUpDownTargetCounterPositionY.Maximum = int.MaxValue;

            this.numericUpDownStartValue.Minimum = int.MinValue;
            this.numericUpDownStartValue.Maximum = int.MaxValue;

            this.numericUpDownTargetValue.Minimum = int.MinValue;
            this.numericUpDownTargetValue.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(previewPipe);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "TargetCounterPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTargetCounterPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TargetCounterPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTargetCounterPositionY.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "StartValue");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownStartValue.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TargetValue");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTargetValue.DataBindings.Add(bind);

            this.labelGameClass.Text = this.business.ClassInfo;
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

            this.numericUpDownTargetCounterPositionX.DataBindings.Clear();
            this.numericUpDownTargetCounterPositionY.DataBindings.Clear();
            this.numericUpDownStartValue.DataBindings.Clear();
            this.numericUpDownTargetValue.DataBindings.Clear();
        }

        protected virtual void setTargetCounterPreview() { }

        #endregion


        #region Events.Incoming
        #endregion

        #region Events.Controls

        protected virtual void numericUpDownTargetCounterPositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.TargetCounterPositionX = (int)this.numericUpDownTargetCounterPositionX.Value;
                this.setTargetCounterPreview();
            }
        }
        protected virtual void numericUpDownTargetCounterPositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.TargetCounterPositionY = (int)this.numericUpDownTargetCounterPositionY.Value;
                this.setTargetCounterPreview();
            }
        }

        private void numericUpDownStartValue_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.StartValue= (int)this.numericUpDownStartValue.Value;
                this.setTargetCounterPreview();
            }
        }
        private void numericUpDownTargetValue_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.TargetValue = (int)this.numericUpDownTargetValue.Value;
                this.setTargetCounterPreview();
            }
        }

        #endregion

    }
}
