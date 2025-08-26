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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.CounterPointerScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.CounterPointerScore {

    public partial class UserControlContent : _Base.Score.UserControlContent {

        #region Properties

        private Business business;

        private bool _showScorePointerInsert = false;
        public bool showScorePointerInsert {
            get { return this._showScorePointerInsert; }
            set {
                if (this._showScorePointerInsert != value) {
                    this._showScorePointerInsert = value;
                    this.setPreviewData();
                }
            }
        }

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownScorePointerPositionX.Minimum = int.MinValue;
            this.numericUpDownScorePointerPositionX.Maximum = int.MaxValue;

            this.numericUpDownScorePointerPositionY.Minimum = int.MinValue;
            this.numericUpDownScorePointerPositionY.Maximum = int.MaxValue;

            this.comboBoxScorePointerStyle.BeginUpdate();
            this.comboBoxScorePointerStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.ScorePointer.Styles)));
            this.comboBoxScorePointerStyle.EndUpdate();
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "ScorePointerPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownScorePointerPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "ScorePointerPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownScorePointerPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "ScorePointerStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxScorePointerStyle.DataBindings.Add(bind);

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

            this.numericUpDownScorePointerPositionX.DataBindings.Clear();
            this.numericUpDownScorePointerPositionY.DataBindings.Clear();
            this.comboBoxScorePointerStyle.DataBindings.Clear();
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
            this.radioButtonSourceScore.Checked = !this.showScorePointerInsert;
            this.radioButtonSourceScorePointer.Checked = this.showScorePointerInsert;
            this.setScorePreview();
            this.setScorePointerPreview();
        }

        protected override void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetScore(previewScene.Insert.Score, 3, 2);
                if (this.showScorePointerInsert) previewScene.Insert.Score.SetOut();
                else previewScene.Insert.Score.SetIn();
            }
        }

        protected void setScorePointerPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetScorePointer(previewScene.Insert.ScorePointer, 8, 11, 22);
                if (this.showScorePointerInsert) previewScene.Insert.ScorePointer.SetIn();
                else previewScene.Insert.ScorePointer.SetOut();
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

        protected virtual void numericUpDownScorePointerPositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.ScorePointerPositionX = (int)this.numericUpDownScorePointerPositionX.Value;
                this.setScorePointerPreview();
            }
        }
        protected virtual void numericUpDownScorePointerPositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.ScorePointerPositionY = (int)this.numericUpDownScorePointerPositionY.Value;
                this.setScorePointerPreview();
            }
        }
        protected virtual void comboBoxScorePointerStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.ScorePointer.Styles style;
            if (this.business is Business &&
                Enum.TryParse(this.comboBoxScorePointerStyle.Text, out style)) {
                this.business.ScorePointerStyle = style;
                this.setScorePointerPreview();
            }
        }

        private void radioButtonSourceScore_CheckedChanged(object sender, EventArgs e) { this.showScorePointerInsert = !this.radioButtonSourceScore.Checked; }
        private void radioButtonSourceCounter_CheckedChanged(object sender, EventArgs e) { this.showScorePointerInsert = this.radioButtonSourceScorePointer.Checked; }

        #endregion

    }
}
