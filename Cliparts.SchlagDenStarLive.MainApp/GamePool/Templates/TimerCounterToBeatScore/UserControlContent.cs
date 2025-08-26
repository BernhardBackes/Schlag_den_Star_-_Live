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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimerCounterToBeatScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimerCounterToBeatScore {

    public partial class UserControlContent :_Base.TimerScore.UserControlContent {

        #region Properties

        private Business business;

        private bool _showCounterInsert = false;
        public bool showCounterInsert {
            get { return this._showCounterInsert; }
            set {
                if (this._showCounterInsert != value) {
                    this._showCounterInsert = value;
                    this.setPreviewData();
                }
            }
        }

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownCounterPositionX.Minimum = int.MinValue;
            this.numericUpDownCounterPositionX.Maximum = int.MaxValue;

            this.numericUpDownCounterPositionY.Minimum = int.MinValue;
            this.numericUpDownCounterPositionY.Maximum = int.MaxValue;

            this.comboBoxCounterSize.BeginUpdate();
            this.comboBoxCounterSize.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.CounterToBeat.SizeElements)));
            this.comboBoxCounterSize.EndUpdate();

        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "CounterPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCounterPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "CounterPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCounterPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "CounterSize");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxCounterSize.DataBindings.Add(bind);

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

            this.numericUpDownCounterPositionX.DataBindings.Clear();
            this.numericUpDownCounterPositionY.DataBindings.Clear();
            this.comboBoxCounterSize.DataBindings.Clear();

        }

        public override void Select() {
            if (this.previewPipe is VRemote4.HandlerSi.Client.Pipe.Business &&
                this.previewPipe.Resolution.HasValue &&
                this.previewPipe.ShareHandle.HasValue) {
                base.select(new Preview(WindowsFormsSynchronizationContext.Current, this.previewPipe));
            }
        }

        private void setPreviewData() {
            this.radioButtonSourceScore.Checked = !this.showCounterInsert;
            this.radioButtonSourceCounter.Checked = this.showCounterInsert;
            this.setScorePreview();
            this.setCounterPreview();
            this.setTimerPreview();
        }

        protected override void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetScore(previewScene.Insert.Score, 3, 2);
                if (this.showCounterInsert) previewScene.Insert.Score.SetOut();
                else previewScene.Insert.Score.SetIn();
            }
        }

        protected void setCounterPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetCounter(previewScene.Insert.CounterToBeat, 88, 99);
                if (this.showCounterInsert) previewScene.Insert.CounterToBeat.SetIn();
                else previewScene.Insert.CounterToBeat.SetOut();
            }
        }

        protected override void setTimerPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetTimer(previewScene.Insert.Timer, this.business.TimerStartTime);
                if (this.showCounterInsert) previewScene.Insert.Timer.SetIn();
                else previewScene.Insert.Timer.SetOut();
            }
        }

        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setPreviewData();
        }

        void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
            }
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

        protected virtual void numericUpDownCounterPositionX_ValueChanged(object sender, EventArgs e) { 
            this.business.CounterPositionX = (int)this.numericUpDownCounterPositionX.Value;
            this.setCounterPreview();
        }
        protected virtual void numericUpDownCounterPositionY_ValueChanged(object sender, EventArgs e) { 
            this.business.CounterPositionY = (int)this.numericUpDownCounterPositionY.Value;
            this.setCounterPreview();
        }
        protected virtual void comboBoxCounterSize_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.CounterToBeat.SizeElements size;
            if (this.business is Business &&
                Enum.TryParse(this.comboBoxCounterSize.Text, out size)) {
                this.business.CounterSize = size;
                this.setCounterPreview();
            }
        }

        private void radioButtonSourceScore_CheckedChanged(object sender, EventArgs e) { this.showCounterInsert = !this.radioButtonSourceScore.Checked; }
        private void radioButtonSourceCounter_CheckedChanged(object sender, EventArgs e) { this.showCounterInsert = this.radioButtonSourceCounter.Checked; }

        #endregion
    }
}
