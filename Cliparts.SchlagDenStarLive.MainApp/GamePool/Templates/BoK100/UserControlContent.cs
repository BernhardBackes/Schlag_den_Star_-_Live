using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.BoK;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.BoK100 {

    public partial class UserControlContent : _Base.BuzzerScore.UserControlContent {

        #region Properties

        private Business business;

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownTaskCounterPositionX.Minimum = int.MinValue;
            this.numericUpDownTaskCounterPositionX.Maximum = int.MaxValue;

            this.numericUpDownTaskCounterPositionY.Minimum = int.MinValue;
            this.numericUpDownTaskCounterPositionY.Maximum = int.MaxValue;

            this.numericUpDownTaskCounterSize.Minimum = int.MinValue;
            this.numericUpDownTaskCounterSize.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "TaskCounterPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTaskCounterPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TaskCounterPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTaskCounterPositionY.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TaskCounterSize");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTaskCounterSize.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "NegativeMode");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxNegativeMode.DataBindings.Add(bind);
            bind = new Binding("ForeColor", this.business, "NegativeMode");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorMissing : this.ForeColor; };
            this.checkBoxNegativeMode.DataBindings.Add(bind);

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

            this.numericUpDownTaskCounterPositionX.DataBindings.Clear();
            this.numericUpDownTaskCounterPositionY.DataBindings.Clear();
            this.numericUpDownTaskCounterSize.DataBindings.Clear();
            this.checkBoxNegativeMode.DataBindings.Clear();
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

        protected override void setTimeoutPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetTimeout(previewScene.Insert.Timeout);
                previewScene.Insert.Timeout.SetLeftRightToGreen(this.business.TimeoutDuration);
            }
        }

        protected void setTaskCounterPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                BoK.SingleDot[] taskCounterPenaltyDots = new BoK.SingleDot[] { 
                    new BoK.SingleDot(VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Blue),
                    new BoK.SingleDot(VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Red),
                    new BoK.SingleDot(VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Fail),
                    new BoK.SingleDot(VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Blue),
                    new BoK.SingleDot(VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Red),
                    new BoK.SingleDot(VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Fail),
                    new BoK.SingleDot(VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Off),
                    new BoK.SingleDot(VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Off),
                    new BoK.SingleDot(VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Off),
                    new BoK.SingleDot(VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Off),
                    new BoK.SingleDot(VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Off),
                    new BoK.SingleDot(VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Off),
                    new BoK.SingleDot(VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Off) };
                this.business.Vinsert_SetTaskCounter(previewScene.Insert.TaskCounter, taskCounterPenaltyDots, 6);
                previewScene.Insert.TaskCounter.SetIn();
            }
        }

        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setScorePreview();
            this.setTimeoutPreview();
            this.setTaskCounterPreview();
        }

        #endregion

        #region Events.Controls

        private void numericUpDownTaskCounterPositionX_ValueChanged(object sender, EventArgs e) {
            this.business.TaskCounterPositionX = (int)this.numericUpDownTaskCounterPositionX.Value;
            this.setTaskCounterPreview();
        }

        private void numericUpDownTaskCounterPositionY_ValueChanged(object sender, EventArgs e) {
            this.business.TaskCounterPositionY = (int)this.numericUpDownTaskCounterPositionY.Value;
            this.setTaskCounterPreview();
        }

        private void numericUpDownTaskCounterSize_ValueChanged(object sender, EventArgs e) {
            this.business.TaskCounterSize = (int)this.numericUpDownTaskCounterSize.Value;
            this.setTaskCounterPreview();
        }

        private void checkBoxNegativeMode_CheckedChanged(object sender, EventArgs e) { this.business.NegativeMode = this.checkBoxNegativeMode.Checked; }

        #endregion

    }
}
