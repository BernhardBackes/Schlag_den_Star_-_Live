using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.DecimalAddition {

    public partial class UserControlContent : _Base.UserControlContent {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownDecimalAdditionPositionX.Minimum = int.MinValue;
            this.numericUpDownDecimalAdditionPositionX.Maximum = int.MaxValue;

            this.numericUpDownDecimalAdditionPositionY.Minimum = int.MinValue;
            this.numericUpDownDecimalAdditionPositionY.Maximum = int.MaxValue;

            this.comboBoxDecimalAdditionStyle.BeginUpdate();
            this.comboBoxDecimalAdditionStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.DecimalAddition.Styles)));
            this.comboBoxDecimalAdditionStyle.EndUpdate();

            this.numericUpDownValidSetsCount.Minimum = int.MinValue;
            this.numericUpDownValidSetsCount.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(previewPipe);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "DecimalAdditionPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownDecimalAdditionPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "DecimalAdditionPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownDecimalAdditionPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "DecimalAdditionStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxDecimalAdditionStyle.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "ValidSetsCount");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownValidSetsCount.DataBindings.Add(bind);

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

            this.numericUpDownDecimalAdditionPositionX.DataBindings.Clear();
            this.numericUpDownDecimalAdditionPositionY.DataBindings.Clear();
            this.comboBoxDecimalAdditionStyle.DataBindings.Clear();
            this.numericUpDownValidSetsCount.DataBindings.Clear();
        }

        protected virtual void setDecimalAdditionPreview() { }

        #endregion


        #region Events.Incoming
        #endregion

        #region Events.Controls

        protected virtual void numericUpDownDecimalAdditionPositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.DecimalAdditionPositionX = (int)this.numericUpDownDecimalAdditionPositionX.Value;
                this.setDecimalAdditionPreview();
            }
        }
        protected virtual void numericUpDownDecimalAdditionPositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.DecimalAdditionPositionY = (int)this.numericUpDownDecimalAdditionPositionY.Value;
                this.setDecimalAdditionPreview();
            }
        }
        protected virtual void comboBoxDecimalAdditionStyle_SelectedIndexChanged(object sender, EventArgs e) { 
            VentuzScenes.GamePool._Modules.DecimalAddition.Styles style;
            if (this.business is Business &&
                Enum.TryParse(this.comboBoxDecimalAdditionStyle.Text, out style)) {
                this.business.DecimalAdditionStyle = style;
                this.setDecimalAdditionPreview();
            }
        }

        private void numericUpDownValidSetsCount_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.ValidSetsCount = (int)this.numericUpDownValidSetsCount.Value;
                this.setDecimalAdditionPreview();
            }
        }

        #endregion

    }
}
