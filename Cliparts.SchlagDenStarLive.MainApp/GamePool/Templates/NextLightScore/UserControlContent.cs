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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.NextLightScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.NextLightScore {

    public partial class UserControlContent : _Base.Score.UserControlContent {

        #region Properties

        private Business business;

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownLeftPlayerBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownRightPlayerBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownDMXStartAddressLeft_0.Minimum = int.MinValue;
            this.numericUpDownDMXStartAddressLeft_0.Maximum = int.MaxValue;

            this.numericUpDownDMXStartAddressLeft_1.Minimum = int.MinValue;
            this.numericUpDownDMXStartAddressLeft_1.Maximum = int.MaxValue;

            this.numericUpDownDMXStartAddressLeft_2.Minimum = int.MinValue;
            this.numericUpDownDMXStartAddressLeft_2.Maximum = int.MaxValue;

            this.numericUpDownDMXStartAddressLeft_3.Minimum = int.MinValue;
            this.numericUpDownDMXStartAddressLeft_3.Maximum = int.MaxValue;

            this.numericUpDownDMXStartAddressLeft_4.Minimum = int.MinValue;
            this.numericUpDownDMXStartAddressLeft_4.Maximum = int.MaxValue;

            this.numericUpDownDMXStartAddressRight_0.Minimum = int.MinValue;
            this.numericUpDownDMXStartAddressRight_0.Maximum = int.MaxValue;

            this.numericUpDownDMXStartAddressRight_1.Minimum = int.MinValue;
            this.numericUpDownDMXStartAddressRight_1.Maximum = int.MaxValue;

            this.numericUpDownDMXStartAddressRight_2.Minimum = int.MinValue;
            this.numericUpDownDMXStartAddressRight_2.Maximum = int.MaxValue;

            this.numericUpDownDMXStartAddressRight_3.Minimum = int.MinValue;
            this.numericUpDownDMXStartAddressRight_3.Maximum = int.MaxValue;

            this.numericUpDownDMXStartAddressRight_4.Minimum = int.MinValue;
            this.numericUpDownDMXStartAddressRight_4.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

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

            bind = new Binding("BackColor", this.business, "LeftPlayerOffColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelLeftPlayerOffColor.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "LeftPlayerOnColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelLeftPlayerOffColor.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "LeftPlayerOnColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelLeftPlayerOnColor.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "LeftPlayerOffColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelLeftPlayerOnColor.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "DMXStartAddressLeft_0");
            bind.Format += (s, e) => { e.Value = (byte)e.Value; };
            this.numericUpDownDMXStartAddressLeft_0.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "DMXStartAddressLeft_1");
            bind.Format += (s, e) => { e.Value = (byte)e.Value; };
            this.numericUpDownDMXStartAddressLeft_1.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "DMXStartAddressLeft_2");
            bind.Format += (s, e) => { e.Value = (byte)e.Value; };
            this.numericUpDownDMXStartAddressLeft_2.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "DMXStartAddressLeft_3");
            bind.Format += (s, e) => { e.Value = (byte)e.Value; };
            this.numericUpDownDMXStartAddressLeft_3.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "DMXStartAddressLeft_4");
            bind.Format += (s, e) => { e.Value = (byte)e.Value; };
            this.numericUpDownDMXStartAddressLeft_4.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "RightPlayerOffColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelRightPlayerOffColor.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "RightPlayerOnColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelRightPlayerOffColor.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "RightPlayerOnColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelRightPlayerOnColor.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "RightPlayerOffColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelRightPlayerOnColor.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "DMXStartAddressRight_0");
            bind.Format += (s, e) => { e.Value = (byte)e.Value; };
            this.numericUpDownDMXStartAddressRight_0.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "DMXStartAddressRight_1");
            bind.Format += (s, e) => { e.Value = (byte)e.Value; };
            this.numericUpDownDMXStartAddressRight_1.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "DMXStartAddressRight_2");
            bind.Format += (s, e) => { e.Value = (byte)e.Value; };
            this.numericUpDownDMXStartAddressRight_2.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "DMXStartAddressRight_3");
            bind.Format += (s, e) => { e.Value = (byte)e.Value; };
            this.numericUpDownDMXStartAddressRight_3.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "DMXStartAddressRight_4");
            bind.Format += (s, e) => { e.Value = (byte)e.Value; };
            this.numericUpDownDMXStartAddressRight_4.DataBindings.Add(bind);

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
            this.numericUpDownLeftPlayerBuzzerChannel.DataBindings.Clear();
            this.numericUpDownRightPlayerBuzzerChannel.DataBindings.Clear();

            this.labelLeftPlayerOffColor.DataBindings.Clear();
            this.labelLeftPlayerOnColor.DataBindings.Clear();
            this.numericUpDownDMXStartAddressLeft_0.DataBindings.Clear();
            this.numericUpDownDMXStartAddressLeft_1.DataBindings.Clear();
            this.numericUpDownDMXStartAddressLeft_2.DataBindings.Clear();
            this.numericUpDownDMXStartAddressLeft_3.DataBindings.Clear();
            this.numericUpDownDMXStartAddressLeft_4.DataBindings.Clear();

            this.labelRightPlayerOffColor.DataBindings.Clear();
            this.labelRightPlayerOnColor.DataBindings.Clear();
            this.numericUpDownDMXStartAddressRight_0.DataBindings.Clear();
            this.numericUpDownDMXStartAddressRight_1.DataBindings.Clear();
            this.numericUpDownDMXStartAddressRight_2.DataBindings.Clear();
            this.numericUpDownDMXStartAddressRight_3.DataBindings.Clear();
            this.numericUpDownDMXStartAddressRight_4.DataBindings.Clear();
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

        public override void SetPreviewPipe(
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            bool selected = ((Preview)this.previewScene) is VRemote4.HandlerSi.Scene;
            base.SetPreviewPipe(previewPipe);
            if (selected) this.Select();
        }

        public override void Select() {
            if (this.previewPipe is VRemote4.HandlerSi.Client.Pipe.Business &&
                this.previewPipe.Resolution.HasValue &&
                this.previewPipe.ShareHandle.HasValue) {
                base.select(new Preview(WindowsFormsSynchronizationContext.Current, this.previewPipe));
            }
        }

        protected override void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetScore(previewScene.Insert.Score, 2, 3);
                previewScene.Insert.Score.SetIn();
            }
        }

        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setScorePreview();
        }

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

        private void labelLeftPlayerOffColor_Click(object sender, EventArgs e) {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.LeftPlayerOffColor;
            switch (dialog.ShowDialog()) {
                case DialogResult.OK:
                    this.business.LeftPlayerOffColor = dialog.Color;
                    break;
            }
        }
        private void labelLeftPlayerOnColor_Click(object sender, EventArgs e) {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.LeftPlayerOnColor;
            switch (dialog.ShowDialog()) {
                case DialogResult.OK:
                    this.business.LeftPlayerOnColor = dialog.Color;
                    break;
            }
        }
        private void labelRightPlayerOffColor_Click(object sender, EventArgs e) {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.RightPlayerOffColor;
            switch (dialog.ShowDialog()) {
                case DialogResult.OK:
                    this.business.RightPlayerOffColor = dialog.Color;
                    break;
            }
        }
        private void labelRightPlayerOnColor_Click(object sender, EventArgs e) {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.RightPlayerOnColor;
            switch (dialog.ShowDialog()) {
                case DialogResult.OK:
                    this.business.RightPlayerOnColor = dialog.Color;
                    break;
            }
        }

        private void numericUpDownDMXStartAddressLeft_0_ValueChanged(object sender, EventArgs e) { this.business.DMXStartAddressLeft_0 = (byte)this.numericUpDownDMXStartAddressLeft_0.Value; }
        private void numericUpDownDMXStartAddressLeft_1_ValueChanged(object sender, EventArgs e) { this.business.DMXStartAddressLeft_1 = (byte)this.numericUpDownDMXStartAddressLeft_1.Value; }
        private void numericUpDownDMXStartAddressLeft_2_ValueChanged(object sender, EventArgs e) { this.business.DMXStartAddressLeft_2 = (byte)this.numericUpDownDMXStartAddressLeft_2.Value; }
        private void numericUpDownDMXStartAddressLeft_3_ValueChanged(object sender, EventArgs e) { this.business.DMXStartAddressLeft_3 = (byte)this.numericUpDownDMXStartAddressLeft_3.Value; }
        private void numericUpDownDMXStartAddressLeft_4_ValueChanged(object sender, EventArgs e) { this.business.DMXStartAddressLeft_4 = (byte)this.numericUpDownDMXStartAddressLeft_4.Value; }
        
        private void numericUpDownDMXStartAddressRight_0_ValueChanged(object sender, EventArgs e) { this.business.DMXStartAddressRight_0 = (byte)this.numericUpDownDMXStartAddressRight_0.Value; }
        private void numericUpDownDMXStartAddressRight_1_ValueChanged(object sender, EventArgs e) { this.business.DMXStartAddressRight_1 = (byte)this.numericUpDownDMXStartAddressRight_1.Value; }
        private void numericUpDownDMXStartAddressRight_2_ValueChanged(object sender, EventArgs e) { this.business.DMXStartAddressRight_2 = (byte)this.numericUpDownDMXStartAddressRight_2.Value; }
        private void numericUpDownDMXStartAddressRight_3_ValueChanged(object sender, EventArgs e) { this.business.DMXStartAddressRight_3 = (byte)this.numericUpDownDMXStartAddressRight_3.Value; }
        private void numericUpDownDMXStartAddressRight_4_ValueChanged(object sender, EventArgs e) { this.business.DMXStartAddressRight_4 = (byte)this.numericUpDownDMXStartAddressRight_4.Value; }


        #endregion

    }

}
