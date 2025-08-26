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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.Sets {

    public partial class UserControlContent : _Base.UserControlContent {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownSetsPositionX.Minimum = int.MinValue;
            this.numericUpDownSetsPositionX.Maximum = int.MaxValue;

            this.numericUpDownSetsPositionY.Minimum = int.MinValue;
            this.numericUpDownSetsPositionY.Maximum = int.MaxValue;

            this.comboBoxSetsStyle.BeginUpdate();
            this.comboBoxSetsStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.Sets.StyleElements)));
            this.comboBoxSetsStyle.EndUpdate();
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Text", this.business, "IOUnitName");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.comboBoxIOUnit.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitStatus");
            bind.Format += this.bind_comboBoxIOUnitName_BackColor;
            this.comboBoxIOUnit.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "BuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownBuzzerChannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "SetsPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownSetsPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "SetsPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownSetsPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "SetsStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxSetsStyle.DataBindings.Add(bind);

            this.labelGameClass.Text = this.business.ClassInfo;

            this.fillIOUnitList();

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

            this.comboBoxIOUnit.DataBindings.Clear();
            this.numericUpDownBuzzerChannel.DataBindings.Clear();

            this.numericUpDownSetsPositionX.DataBindings.Clear();
            this.numericUpDownSetsPositionY.DataBindings.Clear();
            this.comboBoxSetsStyle.DataBindings.Clear();
        }

        private void fillIOUnitList() {
            this.comboBoxIOUnit.BeginUpdate();
            this.comboBoxIOUnit.Items.Clear();
            this.comboBoxIOUnit.Items.AddRange(this.business.IOUnitNameList);
            this.comboBoxIOUnit.EndUpdate();
        }

        private void bind_comboBoxIOUnitName_BackColor(object sender, ConvertEventArgs e) {
            switch ((BuzzerUnitStates)e.Value) {
                case BuzzerUnitStates.NotAvailable:
                    e.Value = Constants.ColorMissing;
                    break;
                case BuzzerUnitStates.Missing:
                    e.Value = Constants.ColorMissing;
                    break;
                case BuzzerUnitStates.Disconnected:
                    e.Value = Constants.ColorDisabled;
                    break;
                case BuzzerUnitStates.Connecting:
                    e.Value = Constants.ColorEnabling;
                    break;
                case BuzzerUnitStates.Connected:
                    e.Value = SystemColors.Control;
                    break;
                case BuzzerUnitStates.Locked:
                    e.Value = SystemColors.Control;
                    break;
                case BuzzerUnitStates.BuzzerMode:
                    e.Value = Constants.ColorBuzzered;
                    break;
                case BuzzerUnitStates.EventMode:
                    e.Value = Constants.ColorEnabled;
                    break;
                default:
                    break;
            }
        }

        protected virtual void setSetsPreview() { }

        #endregion


        #region Events.Incoming

        void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "IOUnitList") this.fillIOUnitList();
            }
        }

        #endregion

        #region Events.Controls

        protected virtual void comboBoxIOUnit_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.IOUnitName = this.comboBoxIOUnit.Text; }
        protected virtual void numericUpDownBuzzerChannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.BuzzerChannel = (int)this.numericUpDownBuzzerChannel.Value; }

        protected virtual void numericUpDownSetsPositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.SetsPositionX = (int)this.numericUpDownSetsPositionX.Value;
                this.setSetsPreview();
            }
        }
        protected virtual void numericUpDownSetsPositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.SetsPositionY = (int)this.numericUpDownSetsPositionY.Value;
                this.setSetsPreview();
            }
        }
        protected virtual void comboBoxSetsStyle_SelectedIndexChanged(object sender, EventArgs e) { 
            VentuzScenes.GamePool._Modules.Sets.StyleElements style;
            if (this.business is Business && 
                Enum.TryParse(this.comboBoxSetsStyle.Text, out style)) {
                this.business.SetsStyle = style;
                this.setSetsPreview();
            }
        }

        #endregion

    }
}
