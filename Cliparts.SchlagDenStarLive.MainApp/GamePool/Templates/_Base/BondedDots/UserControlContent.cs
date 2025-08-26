using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.BondedDots {

    public partial class UserControlContent : _Base.UserControlContent {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownBondedDotsPositionX.Minimum = int.MinValue;
            this.numericUpDownBondedDotsPositionX.Maximum = int.MaxValue;

            this.numericUpDownBondedDotsPositionY.Minimum = int.MinValue;
            this.numericUpDownBondedDotsPositionY.Maximum = int.MaxValue;

            this.comboBoxBondedDotsStyle.BeginUpdate();
            this.comboBoxBondedDotsStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.BondedDots.Styles)));
            this.comboBoxBondedDotsStyle.EndUpdate();
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(previewPipe);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "BondedDotsPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownBondedDotsPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "BondedDotsPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownBondedDotsPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "BondedDotsStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxBondedDotsStyle.DataBindings.Add(bind);

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

            this.numericUpDownBondedDotsPositionX.DataBindings.Clear();
            this.numericUpDownBondedDotsPositionY.DataBindings.Clear();
            this.comboBoxBondedDotsStyle.DataBindings.Clear();
        }

        protected virtual void setBondedDotsPreview() { }

        #endregion


        #region Events.Incoming
        #endregion

        #region Events.Controls

        protected virtual void numericUpDownBondedDotsPositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.BondedDotsPositionX = (int)this.numericUpDownBondedDotsPositionX.Value;
                this.setBondedDotsPreview();
            }
        }
        protected virtual void numericUpDownBondedDotsPositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.BondedDotsPositionY = (int)this.numericUpDownBondedDotsPositionY.Value;
                this.setBondedDotsPreview();
            }
        }
        protected virtual void comboBoxBondedDotsStyle_SelectedIndexChanged(object sender, EventArgs e) { 
            VentuzScenes.GamePool._Modules.BondedDots.Styles style;
            if (this.business is Business && 
                Enum.TryParse(this.comboBoxBondedDotsStyle.Text, out style)) {
                this.business.BondedDotsStyle = style;
                this.setBondedDotsPreview();
            }
        }

        #endregion

    }
}
