using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.RGBIndicatorsScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.RGBIndicatorsScore {

    public partial class UserControlContent : _Base.TimerScore.UserControlContent {

        #region Properties

        private Business business;

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownIndicatorsCount.Minimum = int.MinValue;
            this.numericUpDownIndicatorsCount.Maximum = int.MaxValue;

            this.numericUpDownDMXStartchannel.Minimum = int.MinValue;
            this.numericUpDownDMXStartchannel.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "IndicatorsCount");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownIndicatorsCount.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "DMXStartchannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownDMXStartchannel.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "OffColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelOffColor.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "OffColor");
            bind.Format += this.bind_colorLabel_ForeColor;
            this.labelOffColor.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "LeftColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelLeftColor.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "LeftColor");
            bind.Format += this.bind_colorLabel_ForeColor;
            this.labelLeftColor.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "RightColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelRightColor.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "RightColor");
            bind.Format += this.bind_colorLabel_ForeColor;
            this.labelRightColor.DataBindings.Add(bind);

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

            this.numericUpDownIndicatorsCount.DataBindings.Clear();
            this.numericUpDownDMXStartchannel.DataBindings.Clear();
            this.labelOffColor.DataBindings.Clear();
            this.labelLeftColor.DataBindings.Clear();
            this.labelRightColor.DataBindings.Clear();

        }

        private void bind_colorLabel_ForeColor(object sender, ConvertEventArgs e) {
            Color value = (Color)e.Value;
            Color result = Color.FromArgb(255 - value.R, 255 - value.G, 255 - value.B);
            e.Value = result;
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

        protected override void setTimerPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetTimer(previewScene.Insert.Timer);
                previewScene.Insert.Timer.SetIn();
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
            this.setTimerPreview();
            this.setScorePreview();
        }

        #endregion

        #region Events.Controls

        protected override void numericUpDownTimerPositionX_ValueChanged(object sender, EventArgs e) {
            base.numericUpDownTimerPositionX_ValueChanged(sender, e);
            this.setTimerPreview();
        }

        protected override void numericUpDownTimerPositionY_ValueChanged(object sender, EventArgs e) {
            base.numericUpDownTimerPositionY_ValueChanged(sender, e);
            this.setTimerPreview();
        }

        protected override void comboBoxTimerStyle_SelectedIndexChanged(object sender, EventArgs e) {
            base.comboBoxTimerStyle_SelectedIndexChanged(sender, e);
            this.setTimerPreview();
        }

        protected override void numericUpDownTimerStartTime_ValueChanged(object sender, EventArgs e) {
            base.numericUpDownTimerStartTime_ValueChanged(sender, e);
            this.setTimerPreview();
        }

        private void numericUpDownIndicatorsCount_ValueChanged(object sender, EventArgs e) { this.business.IndicatorsCount = (int)this.numericUpDownIndicatorsCount.Value; }

        private void numericUpDownDMXStartchannel_ValueChanged(object sender, EventArgs e) { this.business.DMXStartchannel = (int)this.numericUpDownDMXStartchannel.Value; }

        private void labelOffColor_Click(object sender, EventArgs e) {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.OffColor;
            switch (dialog.ShowDialog()) {
                case DialogResult.OK:
                    this.business.OffColor = dialog.Color;
                    break;
            }
        }

        private void labelLeftColor_Click(object sender, EventArgs e) {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.LeftColor;
            switch (dialog.ShowDialog()) {
                case DialogResult.OK:
                    this.business.LeftColor = dialog.Color;
                    break;
            }
        }

        private void labelRightColor_Click(object sender, EventArgs e) {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.RightColor;
            switch (dialog.ShowDialog()) {
                case DialogResult.OK:
                    this.business.RightColor = dialog.Color;
                    break;
            }
        }

        private void buttonBlack_Click(object sender, EventArgs e) { this.business.AllLightsBlack(); }

        #endregion

    }
}
