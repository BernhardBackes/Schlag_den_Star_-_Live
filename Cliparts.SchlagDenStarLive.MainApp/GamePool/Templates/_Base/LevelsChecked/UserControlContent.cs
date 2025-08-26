using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.LevelsChecked {

    public partial class UserControlContent : _Base.UserControlContent {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownLevelsCheckedPositionX.Minimum = int.MinValue;
            this.numericUpDownLevelsCheckedPositionX.Maximum = int.MaxValue;

            this.numericUpDownLevelsCheckedPositionY.Minimum = int.MinValue;
            this.numericUpDownLevelsCheckedPositionY.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(previewPipe);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "LevelsCheckedPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLevelsCheckedPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LevelsCheckedPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLevelsCheckedPositionY.DataBindings.Add(bind);

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

            this.numericUpDownLevelsCheckedPositionX.DataBindings.Clear();
            this.numericUpDownLevelsCheckedPositionY.DataBindings.Clear();
        }

        protected virtual void setLevelsCheckedPreview() { }

        #endregion


        #region Events.Incoming
        #endregion

        #region Events.Controls

        protected virtual void numericUpDownLevelsCheckedPositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.LevelsCheckedPositionX = (int)this.numericUpDownLevelsCheckedPositionX.Value;
                this.setLevelsCheckedPreview();
            }
        }
        protected virtual void numericUpDownLevelsCheckedPositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.LevelsCheckedPositionY = (int)this.numericUpDownLevelsCheckedPositionY.Value;
                this.setLevelsCheckedPreview();
            }
        }

        #endregion

    }
}
