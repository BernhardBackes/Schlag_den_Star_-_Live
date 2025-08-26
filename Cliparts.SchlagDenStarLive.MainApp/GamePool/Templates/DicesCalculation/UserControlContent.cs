using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.Tools.Base;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.DicesCalculation;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.DicesCalculation {

    public partial class UserControlContent : _Base.Score.UserControlContent {

        #region Properties

        private Business business;

        public enum InsertSources { Input, Solution, Score };
        private InsertSources _insertSource = InsertSources.Input;
        protected InsertSources insertSource {
            get { return this._insertSource; }
            set {
                if (this._insertSource != value) {
                    this._insertSource = value;
                    this.setPreviewData();
                }
            }
        }


        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownTimerPositionX.Minimum = int.MinValue;
            this.numericUpDownTimerPositionX.Maximum = int.MaxValue;

            this.numericUpDownTimerPositionY.Minimum = int.MinValue;
            this.numericUpDownTimerPositionY.Maximum = int.MaxValue;

            this.comboBoxTimerStyle.BeginUpdate();
            this.comboBoxTimerStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.Timer.Styles)));
            this.comboBoxTimerStyle.EndUpdate();

            this.numericUpDownTimerStartTime.Minimum = int.MinValue;
            this.numericUpDownTimerStartTime.Maximum = int.MaxValue;

            this.numericUpDownTimerExtraTime.Minimum = int.MinValue;
            this.numericUpDownTimerExtraTime.Maximum = int.MaxValue;

            this.numericUpDownTimerStopTime.Minimum = int.MinValue;
            this.numericUpDownTimerStopTime.Maximum = int.MaxValue;

            this.numericUpDownTimerAlarmTime1.Minimum = int.MinValue;
            this.numericUpDownTimerAlarmTime1.Maximum = int.MaxValue;

            this.numericUpDownTimerAlarmTime2.Minimum = int.MinValue;
            this.numericUpDownTimerAlarmTime2.Maximum = int.MaxValue;

            this.numericUpDownContentPositionX.Minimum = int.MinValue;
            this.numericUpDownContentPositionX.Maximum = int.MaxValue;

            this.numericUpDownContentPositionY.Minimum = int.MinValue;
            this.numericUpDownContentPositionY.Maximum = int.MaxValue;

            this.numericUpDownTargetResult.Minimum = int.MinValue;
            this.numericUpDownTargetResult.Maximum = int.MaxValue;

        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "TimerPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxTimerStyle.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerStartTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerStartTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerStartTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerStartTimeText.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerExtraTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerExtraTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerExtraTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerExtraTimeText.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerStopTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerStopTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerStopTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerStopTimeText.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerAlarmTime1");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerAlarmTime1.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerAlarmTime1");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerAlarmTime1Text.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerAlarmTime2");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerAlarmTime2.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerAlarmTime2");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerAlarmTime2Text.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "ShowFullscreenTimer");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxTimerShowFullscreenTimer.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "ContentPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownContentPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "ContentPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownContentPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TabletLeftClientHostname");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.textBoxLocalVentuzServerLeftTablet.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TabletRightClientHostname");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.textBoxLocalVentuzServerRightTablet.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TargetResult");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTargetResult.DataBindings.Add(bind);

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

            this.business.PropertyChanged -= this.business_PropertyChanged;

            this.numericUpDownTimerPositionX.DataBindings.Clear();
            this.numericUpDownTimerPositionY.DataBindings.Clear();
            this.comboBoxTimerStyle.DataBindings.Clear();
            this.numericUpDownTimerStartTime.DataBindings.Clear();
            this.labelTimerStartTimeText.DataBindings.Clear();
            this.numericUpDownTimerExtraTime.DataBindings.Clear();
            this.labelTimerExtraTimeText.DataBindings.Clear();
            this.numericUpDownTimerStopTime.DataBindings.Clear();
            this.labelTimerStopTimeText.DataBindings.Clear();
            this.numericUpDownTimerAlarmTime1.DataBindings.Clear();
            this.labelTimerAlarmTime1Text.DataBindings.Clear();
            this.numericUpDownTimerAlarmTime2.DataBindings.Clear();
            this.labelTimerAlarmTime2Text.DataBindings.Clear();
            this.checkBoxTimerShowFullscreenTimer.DataBindings.Clear();

            this.numericUpDownContentPositionX.DataBindings.Clear();
            this.numericUpDownContentPositionY.DataBindings.Clear();

            this.textBoxLocalVentuzServerLeftTablet.DataBindings.Clear();
            this.textBoxLocalVentuzServerRightTablet.DataBindings.Clear();

            this.numericUpDownTargetResult.DataBindings.Clear();
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

        private void setPreviewData() {
            switch (this.insertSource) {
                case InsertSources.Input:
                    this.radioButtonSourceInput.Checked = true;
                    break;
                case InsertSources.Solution:
                    this.radioButtonSourceSolution.Checked = true;
                    break;
                case InsertSources.Score:
                    this.radioButtonSourceScore.Checked = true;
                    break;
            }
            this.setDicesPreview();
            this.setInputPreview();
            this.setSolutionPreview();
            this.setScorePreview();
        }

        protected void setDicesPreview() {
            base.setScorePreview();
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.insertSource == InsertSources.Input) {
                    previewScene.Insert.SetDice(1, 1);
                    previewScene.Insert.SetDice(2, 2);
                    previewScene.Insert.SetDice(3, 3);
                    previewScene.Insert.SetDicesIn();
                }
                else previewScene.Insert.SetDicesOut();
            }
        }

        protected void setInputPreview() {
            base.setScorePreview();
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.insertSource == InsertSources.Input || this.insertSource == InsertSources.Solution) {
                    previewScene.Insert.SetLeftDice(1, 1);
                    previewScene.Insert.SetLeftOperation(1, Tablet.Operations.Add);
                    previewScene.Insert.SetLeftDice(2, 2);
                    previewScene.Insert.SetLeftOperation(2, Tablet.Operations.Divide);
                    previewScene.Insert.SetLeftDice(3, 3);
                    previewScene.Insert.SetLeftResult(1);
                    previewScene.Insert.SetLeftIn();
                    previewScene.Insert.SetRightDice(1, 4);
                    previewScene.Insert.SetRightOperation(1, Tablet.Operations.Multiply);
                    previewScene.Insert.SetRightDice(2, 5);
                    previewScene.Insert.SetRightOperation(2, Tablet.Operations.Subtract);
                    previewScene.Insert.SetRightDice(3, 6);
                    previewScene.Insert.SetRightResult(14);
                    previewScene.Insert.SetRightIn();
                }
                else {
                    previewScene.Insert.SetLeftOut();
                    previewScene.Insert.SetRightOut();
                }
            }
        }

        protected void setSolutionPreview() {
            base.setScorePreview();
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.insertSource == InsertSources.Solution) {
                    previewScene.Insert.SetSolutionDice(1, 6);
                    previewScene.Insert.SetLeftOperation(1, Tablet.Operations.Divide);
                    previewScene.Insert.SetSolutionDice(2, 2);
                    previewScene.Insert.SetLeftOperation(2, Tablet.Operations.Multiply);
                    previewScene.Insert.SetSolutionDice(3, 5);
                    previewScene.Insert.SetSolutionResult(15);
                    previewScene.Insert.SetSolutionIn();
                }
                else previewScene.Insert.SetSolutionOut();
            }
        }

        protected override void setScorePreview() {
            base.setScorePreview();
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.insertSource == InsertSources.Score) {
                    this.business.Vinsert_SetScore(previewScene.Insert.Score, 2, 3);
                    previewScene.Insert.Score.SetIn();
                }
                else previewScene.Insert.Score.SetOut();
            }
        }

        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setPreviewData();
        }

        void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
            }
        }

        #endregion

        #region Events.Controls

        protected virtual void numericUpDownTimerPositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.TimerPositionX = (int)this.numericUpDownTimerPositionX.Value;
                //this.setTimerPreview();
            }
        }
        protected virtual void numericUpDownTimerPositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.TimerPositionY = (int)this.numericUpDownTimerPositionY.Value;
                //this.setTimerPreview();
            }
        }
        protected virtual void comboBoxTimerStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.Timer.Styles style;
            if (this.business is Business &&
                Enum.TryParse(this.comboBoxTimerStyle.Text, out style)) {
                this.business.TimerStyle = style;
                //this.setTimerPreview();
            }
        }
        protected virtual void numericUpDownTimerStartTime_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.TimerStartTime = (int)this.numericUpDownTimerStartTime.Value;
                //this.setTimerPreview();
            }
        }
        protected virtual void numericUpDownTimerExtraTime_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.TimerExtraTime = (int)this.numericUpDownTimerExtraTime.Value; }
        protected virtual void numericUpDownTimerStopTime_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.TimerStopTime = (int)this.numericUpDownTimerStopTime.Value; }
        protected virtual void numericUpDownTimerAlarmTime1_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.TimerAlarmTime1 = (int)this.numericUpDownTimerAlarmTime1.Value; }
        protected virtual void numericUpDownTimerAlarmTime2_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.TimerAlarmTime2 = (int)this.numericUpDownTimerAlarmTime2.Value; }
        protected virtual void checkBoxTimerShowFullscreenTimer_CheckedChanged(object sender, EventArgs e) { if (this.business is Business) this.business.ShowFullscreenTimer = this.checkBoxTimerShowFullscreenTimer.Checked; }

        private void textBoxLocalVentuzServerLeftTablet_TextChanged(object sender, EventArgs e) { this.business.TabletLeftClientHostname = this.textBoxLocalVentuzServerLeftTablet.Text; }
        private void textBoxLocalVentuzServerRightTablet_TextChanged(object sender, EventArgs e) { this.business.TabletRightClientHostname = this.textBoxLocalVentuzServerRightTablet.Text; }

        private void numericUpDownContentPositionX_ValueChanged(object sender, EventArgs e) { this.business.ContentPositionX = (int)this.numericUpDownContentPositionX.Value; }
        private void numericUpDownContentPositionY_ValueChanged(object sender, EventArgs e) { this.business.ContentPositionY = (int)this.numericUpDownContentPositionY.Value; }

        private void numericUpDownTargetResult_ValueChanged(object sender, EventArgs e) { this.business.TargetResult = (int)this.numericUpDownTargetResult.Value; }

        private void radioButtonSourceInput_CheckedChanged(object sender, EventArgs e) { this.insertSource = InsertSources.Input; }
        private void radioButtonSourceSolution_CheckedChanged(object sender, EventArgs e) { this.insertSource = InsertSources.Solution; }
        private void radioButtonSourceScore_CheckedChanged(object sender, EventArgs e) { this.insertSource = InsertSources.Score; }

        #endregion

    }

}
