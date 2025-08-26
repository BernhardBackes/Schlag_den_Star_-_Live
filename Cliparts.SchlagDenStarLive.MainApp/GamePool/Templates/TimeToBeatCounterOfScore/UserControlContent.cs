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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimeToBeatCounterOfScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimeToBeatCounterOfScore {

    public partial class UserControlContent : _Base.Score.UserControlContent {

        #region Properties

        private Business business;

        private bool _showTimeToBeat = false;
        public bool showTimeToBeat {
            get { return this._showTimeToBeat; }
            set {
                if (this._showTimeToBeat != value) {
                    this._showTimeToBeat = value;
                    this.setPreviewData();
                }
            }
        }

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownTimeToBeatPositionX.Minimum = int.MinValue;
            this.numericUpDownTimeToBeatPositionX.Maximum = int.MaxValue;

            this.numericUpDownTimeToBeatPositionY.Minimum = int.MinValue;
            this.numericUpDownTimeToBeatPositionY.Maximum = int.MaxValue;

            this.comboBoxTimeToBeatStyle.BeginUpdate();
            this.comboBoxTimeToBeatStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.TimeToBeat.Styles)));
            this.comboBoxTimeToBeatStyle.EndUpdate();

            this.numericUpDownTimeToBeatStopTime.Minimum = int.MinValue;
            this.numericUpDownTimeToBeatStopTime.Maximum = int.MaxValue;

            this.numericUpDownCounterPositionX.Minimum = int.MinValue;
            this.numericUpDownCounterPositionX.Maximum = int.MaxValue;

            this.numericUpDownCounterPositionY.Minimum = int.MinValue;
            this.numericUpDownCounterPositionY.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "TimeToBeatPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimeToBeatPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimeToBeatPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimeToBeatPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimeToBeatStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxTimeToBeatStyle.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimeToBeatStopTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimeToBeatStopTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimeToBeatStopTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimeToBeatStopTimeText.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "CounterPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCounterPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "CounterPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCounterPositionY.DataBindings.Add(bind);

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

            if (this.business is Business) this.business.PropertyChanged -= this.business_PropertyChanged;

            this.numericUpDownTimeToBeatPositionX.DataBindings.Clear();
            this.numericUpDownTimeToBeatPositionY.DataBindings.Clear();
            this.comboBoxTimeToBeatStyle.DataBindings.Clear();
            this.numericUpDownTimeToBeatStopTime.DataBindings.Clear();
            this.labelTimeToBeatStopTimeText.DataBindings.Clear();

            this.numericUpDownCounterPositionX.DataBindings.Clear();
            this.numericUpDownCounterPositionY.DataBindings.Clear();
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
            if (this.showTimeToBeat) this.radioButtonSourceTimeToBeat.Checked = true;
            else this.radioButtonSourceScore.Checked = true;
            this.setTimeToBeatPreview();
            this.setCounterPreview();
            this.setScorePreview();
        }

        protected void setTimeToBeatPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.showTimeToBeat) {
                    previewScene.Insert.TimeToBeat.SetPositionX(this.business.TimeToBeatPositionX);
                    previewScene.Insert.TimeToBeat.SetPositionY(this.business.TimeToBeatPositionY);
                    previewScene.Insert.TimeToBeat.SetStyle(this.business.TimeToBeatStyle);
                    previewScene.Insert.TimeToBeat.SetName(this.business.LeftPlayerName);
                    previewScene.Insert.TimeToBeat.SetCounter(5);
                    previewScene.Insert.TimeToBeat.SetOffset(1.23f);
                    previewScene.Insert.TimeToBeat.SetTimeToBeat(12.34f);
                    previewScene.Insert.TimeToBeat.ShowOffset();
                    previewScene.Insert.TimeToBeat.ShowTimeToBeat();
                    previewScene.Insert.TimeToBeat.SetIn();
                }
                else previewScene.Insert.TimeToBeat.SetOut();
            }
        }

        protected void setCounterPreview()
        {
            if (this.previewSceneIsAvailable)
            {
                Preview previewScene = this.previewScene as Preview;
                if (this.showTimeToBeat)
                {
                    this.business.Vinsert_SetCounter(previewScene.Insert.SingleCounterOf, 12, 2);
                    previewScene.Insert.SingleCounterOf.SetIn();                    
                }
                else previewScene.Insert.SingleCounterOf.SetOut();
            }
        }

        protected override void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (!this.showTimeToBeat) {
                    previewScene.Insert.Score.SetPositionX(this.business.ScorePositionX);
                    previewScene.Insert.Score.SetPositionY(this.business.ScorePositionY);
                    previewScene.Insert.Score.SetStyle(this.business.ScoreStyle);
                    previewScene.Insert.Score.SetLeftTopName(this.business.LeftPlayerName);
                    previewScene.Insert.Score.SetLeftTopScore(2);
                    previewScene.Insert.Score.SetRightBottomName(this.business.RightPlayerName);
                    previewScene.Insert.Score.SetRightBottomScore(3);
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

        protected virtual void numericUpDownTimeToBeatPositionX_ValueChanged(object sender, EventArgs e) { 
            this.business.TimeToBeatPositionX = (int)this.numericUpDownTimeToBeatPositionX.Value;
            this.setTimeToBeatPreview();
        }
        protected virtual void numericUpDownTimeToBeatPositionY_ValueChanged(object sender, EventArgs e) { 
            this.business.TimeToBeatPositionY = (int)this.numericUpDownTimeToBeatPositionY.Value;
            this.setTimeToBeatPreview();
        }
        protected virtual void comboBoxTimeToBeatStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.TimeToBeat.Styles style;
            if (Enum.TryParse(this.comboBoxTimeToBeatStyle.Text, out style)) this.business.TimeToBeatStyle = style;
            this.setTimeToBeatPreview();
        }
        protected virtual void numericUpDownTimeToBeatStopTime_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.TimeToBeatStopTime = (int)this.numericUpDownTimeToBeatStopTime.Value; }

        protected virtual void numericUpDownCounterPositionX_ValueChanged(object sender, EventArgs e)
        {
            if (this.business is Business)
            {
                this.business.CounterPositionX = (int)this.numericUpDownCounterPositionX.Value;
                this.setCounterPreview();
            }
        }
        protected virtual void numericUpDownCounterPositionY_ValueChanged(object sender, EventArgs e)
        {
            if (this.business is Business)
            {
                this.business.CounterPositionY = (int)this.numericUpDownCounterPositionY.Value;
                this.setCounterPreview();
            }
        }

        private void radioButtonSourceScore_CheckedChanged(object sender, EventArgs e) { this.showTimeToBeat = !this.radioButtonSourceScore.Checked; }
        private void radioButtonSourceTimeToBeat_CheckedChanged(object sender, EventArgs e) { this.showTimeToBeat = this.radioButtonSourceTimeToBeat.Checked; }

        #endregion
    }

}
