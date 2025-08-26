using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.PenaltySoloScore {

    public partial class UserControlContent : _Base.Penalty.UserControlContent {

        #region Properties

        private Business business;

        private bool previewSceneIsAvailable { get { return this.previewScene is VentuzScenes.GamePool.Penalty.Insert && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

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
            bool selected = ((VentuzScenes.GamePool.Penalty.Insert)this.previewScene) is VRemote4.HandlerSi.Scene;
            base.SetPreviewPipe(previewPipe);
            if (selected) this.Select();
        }

        public override void Select() {
            if (this.previewPipe is VRemote4.HandlerSi.Client.Pipe.Business &&
                this.previewPipe.Resolution.HasValue &&
                this.previewPipe.ShareHandle.HasValue) {
                base.select(new VentuzScenes.GamePool.Penalty.Insert(WindowsFormsSynchronizationContext.Current, this.previewPipe));
            }
        }

        private void setPenaltyPreview() {
            if (this.previewSceneIsAvailable) {
                ((VentuzScenes.GamePool.Penalty.Insert)this.previewScene).Penalty.SetPositionX(this.business.PenaltyPositionX);
                ((VentuzScenes.GamePool.Penalty.Insert)this.previewScene).Penalty.SetPositionY(this.business.PenaltyPositionY);
                ((VentuzScenes.GamePool.Penalty.Insert)this.previewScene).Penalty.SetSize(this.business.PenaltyDotsCount);
                ((VentuzScenes.GamePool.Penalty.Insert)this.previewScene).Penalty.SetTopName(this.business.LeftPlayerName);
                ((VentuzScenes.GamePool.Penalty.Insert)this.previewScene).Penalty.SetBottomName(this.business.RightPlayerName);
                ((VentuzScenes.GamePool.Penalty.Insert)this.previewScene).Penalty.SetIn();
            }
        }

        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setPenaltyPreview();
        }

        #endregion

        #region Events.Controls

        protected override void numericUpDownPenaltyPositionX_ValueChanged(object sender, EventArgs e) {
            base.numericUpDownPenaltyPositionX_ValueChanged(sender, e);
            this.setPenaltyPreview();
        }
        protected override void numericUpDownPenaltyPositionY_ValueChanged(object sender, EventArgs e) {
            base.numericUpDownPenaltyPositionY_ValueChanged(sender, e);
            this.setPenaltyPreview();
        }
        protected override void numericUpDownPenaltyDotsCount_ValueChanged(object sender, EventArgs e) {
            base.numericUpDownPenaltyDotsCount_ValueChanged(sender, e);
            this.setPenaltyPreview();
        }

        protected virtual void numericUpDownScorePositionX_ValueChanged(object sender, EventArgs e) { this.business.ScorePositionX = (int)this.numericUpDownScorePositionX.Value; }
        protected virtual void numericUpDownScorePositionY_ValueChanged(object sender, EventArgs e) { this.business.ScorePositionY = (int)this.numericUpDownScorePositionY.Value; }
        protected virtual void comboBoxScoreStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.Score.Styles style;
            if (Enum.TryParse(this.comboBoxScoreStyle.Text, out style)) this.business.ScoreStyle = style;
        }

        #endregion
    }

}
