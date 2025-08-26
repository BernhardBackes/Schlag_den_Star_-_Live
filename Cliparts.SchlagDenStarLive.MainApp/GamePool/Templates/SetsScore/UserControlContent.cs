using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SetsScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.SetsScore {

    public partial class UserControlContent : _Base.Sets.UserControlContent {

        #region Properties

        private Business business;

        private bool _showSetsInsert = false;
        public bool showSetsInsert {
            get { return this._showSetsInsert; }
            set {
                if (this._showSetsInsert != value) {
                    this._showSetsInsert = value;
                    this.setPreviewData();
                }
            }
        }

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownScorePositionX.Minimum = int.MinValue;
            this.numericUpDownScorePositionX.Maximum = int.MaxValue;

            this.numericUpDownScorePositionY.Minimum = int.MinValue;
            this.numericUpDownScorePositionY.Maximum = int.MaxValue;

            this.comboBoxScoreStyle.BeginUpdate();
            this.comboBoxScoreStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.Score.Styles)));
            this.comboBoxScoreStyle.EndUpdate();

        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "ScorePositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownScorePositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "ScorePositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownScorePositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "ScoreStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxScoreStyle.DataBindings.Add(bind);

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

            this.numericUpDownScorePositionX.DataBindings.Clear();
            this.numericUpDownScorePositionY.DataBindings.Clear();
            this.comboBoxScoreStyle.DataBindings.Clear();
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
            this.radioButtonSourceScore.Checked = !this.showSetsInsert;
            this.radioButtonSourceSets.Checked = this.showSetsInsert;
            this.setSetsPreview();
            this.setScorePreview();
        }

        protected override void setSetsPreview() {
            if (this.previewSceneIsAvailable) {
                VentuzScenes.GamePool._Modules.Sets scene = ((Preview)this.previewScene).Insert.Sets;
                this.business.Vinsert_SetSets(scene,
                    0,
                    0,
                    new _Base.Sets.SingleSet[] {
                    new _Base.Sets.SingleSet(1, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid),
                    new _Base.Sets.SingleSet(2, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Invalid),
                    new _Base.Sets.SingleSet(3, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid),
                    new _Base.Sets.SingleSet(4, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Invalid),
                    new _Base.Sets.SingleSet(5, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid),
                    new _Base.Sets.SingleSet(6, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Invalid),
                    new _Base.Sets.SingleSet(7, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid),
                    new _Base.Sets.SingleSet(8, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Invalid),
                    new _Base.Sets.SingleSet(9, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid),
                    new _Base.Sets.SingleSet(0, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Invalid)},
                    new _Base.Sets.SingleSet[] {
                    new _Base.Sets.SingleSet(0, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Invalid),
                    new _Base.Sets.SingleSet(1, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid),
                    new _Base.Sets.SingleSet(2, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Invalid),
                    new _Base.Sets.SingleSet(3, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid),
                    new _Base.Sets.SingleSet(4, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Invalid),
                    new _Base.Sets.SingleSet(5, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid),
                    new _Base.Sets.SingleSet(6, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Invalid),
                    new _Base.Sets.SingleSet(7, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid),
                    new _Base.Sets.SingleSet(8, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Invalid),
                    new _Base.Sets.SingleSet(9, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid)});
                if (this.showSetsInsert) scene.SetIn();
                else scene.SetOut();
            }
        }

        protected void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetScore(previewScene.Insert.Score, 3, 2);
                if (this.showSetsInsert) previewScene.Insert.Score.SetOut();
                else previewScene.Insert.Score.SetIn();
            }
        }

        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setPreviewData();
        }

        #endregion

        #region Events.Controls

        protected override void numericUpDownSetsPositionX_ValueChanged(object sender, EventArgs e) {
            base.numericUpDownSetsPositionX_ValueChanged(sender, e);
            this.setSetsPreview();
        }
        protected override void numericUpDownSetsPositionY_ValueChanged(object sender, EventArgs e) {
            base.numericUpDownSetsPositionY_ValueChanged(sender, e);
            this.setSetsPreview();
        }
        protected override void comboBoxSetsStyle_SelectedIndexChanged(object sender, EventArgs e) {
            base.comboBoxSetsStyle_SelectedIndexChanged(sender, e);
            this.setSetsPreview();
        }

        protected virtual void numericUpDownScorePositionX_ValueChanged(object sender, EventArgs e) { 
            this.business.ScorePositionX = (int)this.numericUpDownScorePositionX.Value;
            this.setScorePreview();
        }
        protected virtual void numericUpDownScorePositionY_ValueChanged(object sender, EventArgs e) { 
            this.business.ScorePositionY = (int)this.numericUpDownScorePositionY.Value;
            this.setScorePreview();
        }
        protected virtual void comboBoxScoreStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.Score.Styles style;
            if (Enum.TryParse(this.comboBoxScoreStyle.Text, out style)) this.business.ScoreStyle = style;
            this.setScorePreview();
        }

        private void radioButtonSourceScore_CheckedChanged(object sender, EventArgs e) { this.showSetsInsert = !this.radioButtonSourceScore.Checked; }
        private void radioButtonSourceSets_CheckedChanged(object sender, EventArgs e) { this.showSetsInsert = this.radioButtonSourceSets.Checked; }

        #endregion
    }

}
