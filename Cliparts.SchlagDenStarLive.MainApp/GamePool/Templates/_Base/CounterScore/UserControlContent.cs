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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.CounterScore {

    public partial class UserControlContent : _Base.UserControlContent {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownLeftPlayerBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownRightPlayerBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownCounterScorePositionX.Minimum = int.MinValue;
            this.numericUpDownCounterScorePositionX.Maximum = int.MaxValue;

            this.numericUpDownCounterScorePositionY.Minimum = int.MinValue;
            this.numericUpDownCounterScorePositionY.Maximum = int.MaxValue;

            this.comboBoxCounterScoreStyle.BeginUpdate();
            this.comboBoxCounterScoreStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.CounterScore.Styles)));
            this.comboBoxCounterScoreStyle.EndUpdate();

            this.comboBoxWinnerMode.BeginUpdate();
            this.comboBoxWinnerMode.Items.AddRange(Enum.GetNames(typeof(WinnerModes)));
            this.comboBoxWinnerMode.EndUpdate();
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

            bind = new Binding("Value", this.business, "LeftPlayerBuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerBuzzerChannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerBuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerBuzzerChannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "CounterScorePositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCounterScorePositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "CounterScorePositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCounterScorePositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "CounterScoreStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxCounterScoreStyle.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "WinnerMode");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxWinnerMode.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "HidePlayerCounterByDefault");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxHidePlayerCounterByDefault.DataBindings.Add(bind);

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
            this.numericUpDownLeftPlayerBuzzerChannel.DataBindings.Clear();
            this.numericUpDownRightPlayerBuzzerChannel.DataBindings.Clear();

            this.numericUpDownCounterScorePositionX.DataBindings.Clear();
            this.numericUpDownCounterScorePositionY.DataBindings.Clear();
            this.comboBoxCounterScoreStyle.DataBindings.Clear();

            this.comboBoxWinnerMode.DataBindings.Clear();
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

        protected virtual void setCounterScorePreview() { }

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
        protected virtual void numericUpDownLeftPlayerBuzzerChannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPlayerBuzzerChannel = (int)this.numericUpDownLeftPlayerBuzzerChannel.Value; }
        protected virtual void numericUpDownRightPlayerBuzzerChannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPlayerBuzzerChannel = (int)this.numericUpDownRightPlayerBuzzerChannel.Value; }

        protected virtual void numericUpDownCounterScorePositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.CounterScorePositionX = (int)this.numericUpDownCounterScorePositionX.Value;
                this.setCounterScorePreview();
            }
        }
        protected virtual void numericUpDownCounterScorePositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.CounterScorePositionY = (int)this.numericUpDownCounterScorePositionY.Value;
                this.setCounterScorePreview();
            }
        }
        protected virtual void comboBoxCounterScoreStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.CounterScore.Styles style;
            if (this.business is Business &&
                Enum.TryParse(this.comboBoxCounterScoreStyle.Text, out style)) {
                this.business.CounterScoreStyle = style;
                this.setCounterScorePreview();
            }
        }

        protected virtual void comboBoxWinnerMode_SelectedIndexChanged(object sender, EventArgs e) {
            WinnerModes mode;
            if (Enum.TryParse(this.comboBoxWinnerMode.Text, out mode)) this.business.WinnerMode = mode;
        }

        private void checkBoxHidePlayerCounterByDefault_CheckedChanged(object sender, EventArgs e) {
            if (this.business is Business) this.business.HidePlayerCounterByDefault = this.checkBoxHidePlayerCounterByDefault.Checked;
        }

        #endregion

    }
}
