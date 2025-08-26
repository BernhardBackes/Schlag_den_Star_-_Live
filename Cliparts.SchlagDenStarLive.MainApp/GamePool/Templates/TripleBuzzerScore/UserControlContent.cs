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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TripleBuzzerScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TripleBuzzerScore {

    public partial class UserControlContent : _Base.Score.UserControlContent {

        #region Properties

        private Business business;

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownLeftPlayerBuzzerChannel1.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerBuzzerChannel1.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerBuzzerChannel2.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerBuzzerChannel2.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerBuzzerChannel3.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerBuzzerChannel3.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerBuzzerChannel1.Minimum = int.MinValue;
            this.numericUpDownRightPlayerBuzzerChannel1.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerBuzzerChannel2.Minimum = int.MinValue;
            this.numericUpDownRightPlayerBuzzerChannel2.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerBuzzerChannel3.Minimum = int.MinValue;
            this.numericUpDownRightPlayerBuzzerChannel3.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerDMXStart1.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerDMXStart1.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerDMXStart2.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerDMXStart2.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerDMXStart3.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerDMXStart3.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerDMXStart1.Minimum = int.MinValue;
            this.numericUpDownRightPlayerDMXStart1.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerDMXStart2.Minimum = int.MinValue;
            this.numericUpDownRightPlayerDMXStart2.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerDMXStart3.Minimum = int.MinValue;
            this.numericUpDownRightPlayerDMXStart3.Maximum = int.MaxValue;

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

            bind = new Binding("Value", this.business, "LeftPlayerBuzzerChannel1");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerBuzzerChannel1.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerBuzzerChannel2");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerBuzzerChannel2.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerBuzzerChannel3");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerBuzzerChannel3.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerBuzzerChannel1");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerBuzzerChannel1.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerBuzzerChannel2");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerBuzzerChannel2.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerBuzzerChannel3");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerBuzzerChannel3.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerDMXStart1");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerDMXStart1.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerDMXStart2");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerDMXStart2.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerDMXStart3");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerDMXStart3.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerDMXStart1");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerDMXStart1.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerDMXStart2");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerDMXStart2.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerDMXStart3");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerDMXStart3.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "LeftPlayerColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelLeftPlayerColor.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "RightPlayerColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelRightPlayerColor.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "OffColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelOffColor.DataBindings.Add(bind);

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

            this.comboBoxIOUnit.DataBindings.Clear();
            this.numericUpDownLeftPlayerBuzzerChannel1.DataBindings.Clear();
            this.numericUpDownLeftPlayerBuzzerChannel2.DataBindings.Clear();
            this.numericUpDownLeftPlayerBuzzerChannel3.DataBindings.Clear();
            this.numericUpDownRightPlayerBuzzerChannel1.DataBindings.Clear();
            this.numericUpDownRightPlayerBuzzerChannel2.DataBindings.Clear();
            this.numericUpDownRightPlayerBuzzerChannel3.DataBindings.Clear();

            this.numericUpDownLeftPlayerDMXStart1.DataBindings.Clear();
            this.numericUpDownLeftPlayerDMXStart2.DataBindings.Clear();
            this.numericUpDownLeftPlayerDMXStart3.DataBindings.Clear();
            this.numericUpDownRightPlayerDMXStart1.DataBindings.Clear();
            this.numericUpDownRightPlayerDMXStart2.DataBindings.Clear();
            this.numericUpDownRightPlayerDMXStart3.DataBindings.Clear();

            this.labelLeftPlayerColor.DataBindings.Clear();
            this.labelRightPlayerColor.DataBindings.Clear();
            this.labelOffColor.DataBindings.Clear();

            this.business.PropertyChanged -= this.business_PropertyChanged;
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

        protected void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "IOUnitList") this.fillIOUnitList();
            }
        }


        #endregion

        #region Events.Controls

        private void comboBoxIOUnit_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.IOUnitName = this.comboBoxIOUnit.Text; }

        private void numericUpDownLeftPlayerBuzzerChannel1_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPlayerBuzzerChannel1 = (int)this.numericUpDownLeftPlayerBuzzerChannel1.Value; }
        private void numericUpDownLeftPlayerBuzzerChannel2_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPlayerBuzzerChannel2 = (int)this.numericUpDownLeftPlayerBuzzerChannel2.Value; }
        private void numericUpDownLeftPlayerBuzzerChannel3_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPlayerBuzzerChannel3 = (int)this.numericUpDownLeftPlayerBuzzerChannel3.Value; }

        private void numericUpDownRightPlayerBuzzerChannel1_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPlayerBuzzerChannel1 = (int)this.numericUpDownRightPlayerBuzzerChannel1.Value; }
        private void numericUpDownRightPlayerBuzzerChannel2_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPlayerBuzzerChannel2 = (int)this.numericUpDownRightPlayerBuzzerChannel2.Value; }
        private void numericUpDownRightPlayerBuzzerChannel3_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPlayerBuzzerChannel3 = (int)this.numericUpDownRightPlayerBuzzerChannel3.Value; }

        private void numericUpDownLeftPlayerDMXStart1_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPlayerDMXStart1 = (int)this.numericUpDownLeftPlayerDMXStart1.Value; }
        private void numericUpDownLeftPlayerDMXStart2_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPlayerDMXStart2 = (int)this.numericUpDownLeftPlayerDMXStart2.Value; }
        private void numericUpDownLeftPlayerDMXStart3_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPlayerDMXStart3 = (int)this.numericUpDownLeftPlayerDMXStart3.Value; }
        private void buttonLeftPlayerDMXOn1_Click(object sender, EventArgs e) { this.business.LeftPlayerDMXOn(1); }
        private void buttonLeftPlayerDMXOff1_Click(object sender, EventArgs e) { this.business.LeftPlayerDMXOff(1); }
        private void buttonLeftPlayerDMXOn2_Click(object sender, EventArgs e) { this.business.LeftPlayerDMXOn(2); }
        private void buttonLeftPlayerDMXOff2_Click(object sender, EventArgs e) { this.business.LeftPlayerDMXOff(2); }
        private void buttonLeftPlayerDMXOn3_Click(object sender, EventArgs e) { this.business.LeftPlayerDMXOn(3); }
        private void buttonLeftPlayerDMXOff3_Click(object sender, EventArgs e) { this.business.LeftPlayerDMXOff(3); }

        private void numericUpDownRightPlayerDMXStart1_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPlayerDMXStart1 = (int)this.numericUpDownRightPlayerDMXStart1.Value; }
        private void numericUpDownRightPlayerDMXStart2_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPlayerDMXStart2 = (int)this.numericUpDownRightPlayerDMXStart2.Value; }
        private void numericUpDownRightPlayerDMXStart3_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPlayerDMXStart3 = (int)this.numericUpDownRightPlayerDMXStart3.Value; }
        private void buttonRightPlayerDMXOn1_Click(object sender, EventArgs e) { this.business.RightPlayerDMXOn(1); }
        private void buttonRightPlayerDMXOff1_Click(object sender, EventArgs e) { this.business.RightPlayerDMXOff(1); }
        private void buttonRightPlayerDMXOn2_Click(object sender, EventArgs e) { this.business.RightPlayerDMXOn(2); }
        private void buttonRightPlayerDMXOff2_Click(object sender, EventArgs e) { this.business.RightPlayerDMXOff(2); }
        private void buttonRightPlayerDMXOn3_Click(object sender, EventArgs e) { this.business.RightPlayerDMXOn(3); }
        private void buttonRightPlayerDMXOff3_Click(object sender, EventArgs e) { this.business.RightPlayerDMXOff(3); }

        private void buttonDMXAllBlack_Click(object sender, EventArgs e) { this.business.AllLightsBlack(); }

        private void labelLeftPlayerColor_Click(object sender, EventArgs e) {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.LeftPlayerColor;
            switch (dialog.ShowDialog()) {
                case DialogResult.OK:
                    this.business.LeftPlayerColor = dialog.Color;
                    break;
            }
        }

        private void labelRightPlayerColor_Click(object sender, EventArgs e) {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.RightPlayerColor;
            switch (dialog.ShowDialog()) {
                case DialogResult.OK:
                    this.business.RightPlayerColor = dialog.Color;
                    break;
            }
        }

        private void labelOffColor_Click(object sender, EventArgs e) {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.OffColor;
            switch (dialog.ShowDialog()) {
                case DialogResult.OK:
                    this.business.OffColor = dialog.Color;
                    break;
            }
        }

        #endregion
    }

}
