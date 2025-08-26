using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.DecimalSets {

    public partial class UserControlContent : _Base.UserControlContent {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownDecimalSetsPositionX.Minimum = int.MinValue;
            this.numericUpDownDecimalSetsPositionX.Maximum = int.MaxValue;

            this.numericUpDownDecimalSetsPositionY.Minimum = int.MinValue;
            this.numericUpDownDecimalSetsPositionY.Maximum = int.MaxValue;

            this.comboBoxDecimalSetsStyle.BeginUpdate();
            this.comboBoxDecimalSetsStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.DecimalSets.Styles)));
            this.comboBoxDecimalSetsStyle.EndUpdate();
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(previewPipe);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "DecimalSetsPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownDecimalSetsPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "DecimalSetsPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownDecimalSetsPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "DecimalSetsStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxDecimalSetsStyle.DataBindings.Add(bind);

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

            this.numericUpDownDecimalSetsPositionX.DataBindings.Clear();
            this.numericUpDownDecimalSetsPositionY.DataBindings.Clear();
            this.comboBoxDecimalSetsStyle.DataBindings.Clear();
        }

        protected virtual void setDecimalSetsPreview() { }

        #endregion


        #region Events.Incoming
        #endregion

        #region Events.Controls

        protected virtual void numericUpDownDecimalSetsPositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.DecimalSetsPositionX = (int)this.numericUpDownDecimalSetsPositionX.Value;
                this.setDecimalSetsPreview();
            }
        }
        protected virtual void numericUpDownDecimalSetsPositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.DecimalSetsPositionY = (int)this.numericUpDownDecimalSetsPositionY.Value;
                this.setDecimalSetsPreview();
            }
        }
        protected virtual void comboBoxDecimalSetsStyle_SelectedIndexChanged(object sender, EventArgs e) { 
            VentuzScenes.GamePool._Modules.DecimalSets.Styles style;
            if (this.business is Business &&
                Enum.TryParse(this.comboBoxDecimalSetsStyle.Text, out style)) {
                this.business.DecimalSetsStyle = style;
                this.setDecimalSetsPreview();
            }
        }

        #endregion

    }
}
