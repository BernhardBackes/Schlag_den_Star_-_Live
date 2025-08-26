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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.Buzzer {
    
    public partial class UserControlContent : _Base.UserControlContent {

        #region Properties

        private Business business;

        private List<string> dataNameList = new List<string>();

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownLeftPlayerBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownRightPlayerBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownTimeoutPositionX.Minimum = int.MinValue;
            this.numericUpDownTimeoutPositionX.Maximum = int.MaxValue;

            this.numericUpDownTimeoutPositionY.Minimum = int.MinValue;
            this.numericUpDownTimeoutPositionY.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerDMXStartchannel.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerDMXStartchannel.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerDMXStartchannel.Minimum = int.MinValue;
            this.numericUpDownRightPlayerDMXStartchannel.Maximum = int.MaxValue;
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

            bind = new Binding("Value", this.business, "TimeoutPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimeoutPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimeoutPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimeoutPositionY.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "TimeoutIsVisible");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxTimeoutIsVisible.DataBindings.Add(bind);
            bind = new Binding("ForeColor", this.business, "TimeoutIsVisible");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? this.ForeColor : Constants.ColorMissing; };
            this.checkBoxTimeoutIsVisible.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "TimeoutDuration");
            bind.Format += (s, e) => { e.Value = (VentuzScenes.GamePool._Modules.Timeout.Duration)e.Value == VentuzScenes.GamePool._Modules.Timeout.Duration.FiveSeconds; };
            this.radioButtonTimeoutDurationFiveSeconds.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "TimeoutDuration");
            bind.Format += (s, e) => { e.Value = (VentuzScenes.GamePool._Modules.Timeout.Duration)e.Value == VentuzScenes.GamePool._Modules.Timeout.Duration.TenSeconds; };
            this.radioButtonTimeoutDurationTenSeconds.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerDMXStartchannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerDMXStartchannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerDMXStartchannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerDMXStartchannel.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "LeftOffColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelLeftOffColor.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "LeftOnColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelLeftOffColor.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "LeftOnColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelLeftOnColor.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "LeftOffColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelLeftOnColor.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "RightOffColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelRightOffColor.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "RightOnColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelRightOffColor.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "RightOnColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelRightOnColor.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "RightOffColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelRightOnColor.DataBindings.Add(bind);

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

            this.numericUpDownTimeoutPositionX.DataBindings.Clear();
            this.numericUpDownTimeoutPositionY.DataBindings.Clear();
            this.checkBoxTimeoutIsVisible.DataBindings.Clear();
            this.radioButtonTimeoutDurationFiveSeconds.DataBindings.Clear();
            this.radioButtonTimeoutDurationTenSeconds.DataBindings.Clear();

            this.numericUpDownLeftPlayerDMXStartchannel.DataBindings.Clear();
            this.numericUpDownRightPlayerDMXStartchannel.DataBindings.Clear();
            this.labelLeftOffColor.DataBindings.Clear();
            this.labelLeftOnColor.DataBindings.Clear();
            this.labelRightOnColor.DataBindings.Clear();
            this.labelRightOffColor.DataBindings.Clear();

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
                    e.Value = Constants.ColorEnabled;
                    break;
                case BuzzerUnitStates.EventMode:
                    e.Value = Constants.ColorBuzzered;
                    break;
                default:
                    break;
            }
        }

        protected virtual void setTimeoutPreview() { }

        #endregion


        #region Events.Incoming

        void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "IOUnitNameList") this.fillIOUnitList();
            }
        }

        #endregion

        #region Events.Controls

        protected virtual void comboBoxIOUnit_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.IOUnitName = this.comboBoxIOUnit.Text; }
        protected virtual void numericUpDownLeftPlayerBuzzerChannel_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerBuzzerChannel = (int)this.numericUpDownLeftPlayerBuzzerChannel.Value; }
        protected virtual void numericUpDownRightPlayerBuzzerChannel_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerBuzzerChannel = (int)this.numericUpDownRightPlayerBuzzerChannel.Value; }

        private void numericUpDownLeftPlayerDMXStartchannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPlayerDMXStartchannel = (int)this.numericUpDownLeftPlayerDMXStartchannel.Value; }
        private void buttonLeftPlayerOn_Click(object sender, EventArgs e) { this.business.SetLeftPlayerOn(); }
        private void buttonLeftPlayerOff_Click(object sender, EventArgs e) { this.business.SetLeftPlayerOff(); }

        private void numericUpDownRightPlayerDMXStartchannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPlayerDMXStartchannel = (int)this.numericUpDownRightPlayerDMXStartchannel.Value; }
        private void buttonRightPlayerOn_Click(object sender, EventArgs e) { this.business.SetRightPlayerOn(); }
        private void buttonRightPlayerOff_Click(object sender, EventArgs e) { this.business.SetRightPlayerOff(); }

        private void labelLeftOffColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.LeftOffColor;
            switch (dialog.ShowDialog())
            {
                case DialogResult.OK:
                    this.business.LeftOffColor = dialog.Color;
                    break;
            }
        }

        private void labelLeftOnColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.LeftOnColor;
            switch (dialog.ShowDialog())
            {
                case DialogResult.OK:
                    this.business.LeftOnColor = dialog.Color;
                    break;
            }
        }

        private void labelRightOffColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.RightOffColor;
            switch (dialog.ShowDialog())
            {
                case DialogResult.OK:
                    this.business.RightOffColor = dialog.Color;
                    break;
            }
        }

        private void labelRightOnColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.RightOnColor;
            switch (dialog.ShowDialog())
            {
                case DialogResult.OK:
                    this.business.RightOnColor = dialog.Color;
                    break;
            }
        }

        private void buttonBlack_Click(object sender, EventArgs e) { this.business.AllLightsBlack(); }

        protected virtual void numericUpDownTimeoutPositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.TimeoutPositionX = (int)this.numericUpDownTimeoutPositionX.Value;
                this.setTimeoutPreview();
            }
        }
        protected virtual void numericUpDownTimeoutPositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.TimeoutPositionY = (int)this.numericUpDownTimeoutPositionY.Value;
                this.setTimeoutPreview();
            }
        }
        protected virtual void checkBoxTimeoutIsVisible_Click(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.TimeoutIsVisible = this.checkBoxTimeoutIsVisible.Checked;
                this.setTimeoutPreview();
            }
        }
        private void radioButtonTimeoutDurationFiveSeconds_CheckedChanged(object sender, EventArgs e)
        {
            if (this.business is Business)
            {
                if (this.radioButtonTimeoutDurationFiveSeconds.Checked) this.business.TimeoutDuration = VentuzScenes.GamePool._Modules.Timeout.Duration.FiveSeconds;
                this.setTimeoutPreview();
            }
        }
        private void radioButtonTimeoutDurationTenSeconds_CheckedChanged(object sender, EventArgs e)
        {
            if (this.business is Business)
            {
                if (this.radioButtonTimeoutDurationTenSeconds.Checked) this.business.TimeoutDuration = VentuzScenes.GamePool._Modules.Timeout.Duration.TenSeconds;
                this.setTimeoutPreview();
            }
        }

        #endregion

    }

}
