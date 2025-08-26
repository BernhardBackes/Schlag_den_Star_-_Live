using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.CounterToLock {

    public partial class UserControlContent : _Base.UserControlContent {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownCounterToLockPositionX.Minimum = int.MinValue;
            this.numericUpDownCounterToLockPositionX.Maximum = int.MaxValue;

            this.numericUpDownCounterToLockPositionY.Minimum = int.MinValue;
            this.numericUpDownCounterToLockPositionY.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(previewPipe);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "CounterToLockPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCounterToLockPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "CounterToLockPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCounterToLockPositionY.DataBindings.Add(bind);

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

            this.numericUpDownCounterToLockPositionX.DataBindings.Clear();
            this.numericUpDownCounterToLockPositionY.DataBindings.Clear();
        }

        protected virtual void setCounterToLockPreview() {}

        #endregion


        #region Events.Incoming
        #endregion

        #region Events.Controls

        protected virtual void numericUpDownCounterToLockPositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.CounterToLockPositionX = (int)this.numericUpDownCounterToLockPositionX.Value;
                this.setCounterToLockPreview();
            }
        }
        protected virtual void numericUpDownCounterToLockPositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.CounterToLockPositionY = (int)this.numericUpDownCounterToLockPositionY.Value;
                this.setCounterToLockPreview();
            }
        }

        #endregion

    }
}
