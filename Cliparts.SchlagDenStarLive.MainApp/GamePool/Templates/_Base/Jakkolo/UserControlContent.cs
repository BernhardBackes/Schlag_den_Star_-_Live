using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.Jakkolo {

    public partial class UserControlContent : _Base.UserControlContent {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownJakkoloPositionX.Minimum = int.MinValue;
            this.numericUpDownJakkoloPositionX.Maximum = int.MaxValue;

            this.numericUpDownJakkoloPositionY.Minimum = int.MinValue;
            this.numericUpDownJakkoloPositionY.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(previewPipe);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "JakkoloPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownJakkoloPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "JakkoloPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownJakkoloPositionY.DataBindings.Add(bind);
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

            this.numericUpDownJakkoloPositionX.DataBindings.Clear();
            this.numericUpDownJakkoloPositionY.DataBindings.Clear();
        }

        protected virtual void setJakkoloPreview() {}

        #endregion


        #region Events.Incoming
        #endregion

        #region Events.Controls

        protected virtual void numericUpDownJakkoloPositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.JakkoloPositionX = (int)this.numericUpDownJakkoloPositionX.Value;
                this.setJakkoloPreview();
            }
        }
        protected virtual void numericUpDownJakkoloPositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.JakkoloPositionY = (int)this.numericUpDownJakkoloPositionY.Value;
                this.setJakkoloPreview();
            }
        }

        #endregion

    }
}
