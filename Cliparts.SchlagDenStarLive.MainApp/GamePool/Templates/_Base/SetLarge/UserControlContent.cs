using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.SetLarge {

    public partial class UserControlContent : _Base.UserControlContent {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownSetLargePositionX.Minimum = int.MinValue;
            this.numericUpDownSetLargePositionX.Maximum = int.MaxValue;

            this.numericUpDownSetLargePositionY.Minimum = int.MinValue;
            this.numericUpDownSetLargePositionY.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(previewPipe);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "SetLargePositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownSetLargePositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "SetLargePositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownSetLargePositionY.DataBindings.Add(bind);

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

            this.numericUpDownSetLargePositionX.DataBindings.Clear();
            this.numericUpDownSetLargePositionY.DataBindings.Clear();
        }

        protected virtual void setSetLargePreview() { }

        #endregion


        #region Events.Incoming
        #endregion

        #region Events.Controls

        protected virtual void numericUpDownSetLargePositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.SetLargePositionX = (int)this.numericUpDownSetLargePositionX.Value;
                this.setSetLargePreview();
            }
        }
        protected virtual void numericUpDownSetLargePositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.SetLargePositionY = (int)this.numericUpDownSetLargePositionY.Value;
                this.setSetLargePreview();
            }
        }

        #endregion

    }
}
