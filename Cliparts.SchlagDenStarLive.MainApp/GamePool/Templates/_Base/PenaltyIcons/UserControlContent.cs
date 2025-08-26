using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.PenaltyIcons {

    public partial class UserControlContent : _Base.UserControlContent {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownPenaltyPositionX.Minimum = int.MinValue;
            this.numericUpDownPenaltyPositionX.Maximum = int.MaxValue;

            this.numericUpDownPenaltyPositionY.Minimum = int.MinValue;
            this.numericUpDownPenaltyPositionY.Maximum = int.MaxValue;

            this.comboBoxPenaltyStyle.BeginUpdate();
            this.comboBoxPenaltyStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.PenaltyIcons.Styles)));
            this.comboBoxPenaltyStyle.EndUpdate();
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(previewPipe);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "PenaltyPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownPenaltyPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "PenaltyPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownPenaltyPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "PenaltyStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxPenaltyStyle.DataBindings.Add(bind);

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

            this.numericUpDownPenaltyPositionX.DataBindings.Clear();
            this.numericUpDownPenaltyPositionY.DataBindings.Clear();
            this.comboBoxPenaltyStyle.DataBindings.Clear();
        }

        protected virtual void setPenaltyPreview() { }

        #endregion


        #region Events.Incoming
        #endregion

        #region Events.Controls

        protected virtual void numericUpDownPenaltyPositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.PenaltyPositionX = (int)this.numericUpDownPenaltyPositionX.Value;
                this.setPenaltyPreview();
            }
        }
        protected virtual void numericUpDownPenaltyPositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.PenaltyPositionY = (int)this.numericUpDownPenaltyPositionY.Value;
                this.setPenaltyPreview();
            }
        }
        protected virtual void comboBoxPenaltyStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.PenaltyIcons.Styles style;
            if (this.business is Business &&
                Enum.TryParse(this.comboBoxPenaltyStyle.Text, out style)) {
                this.business.PenaltyStyle = style;
                this.setPenaltyPreview();
            }
        }

        #endregion

    }
}
