using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SetsPenalty;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.SetsPenalty {

    public partial class UserControlContent : _Base.UserControlContent {

        #region Properties

        private Business business;

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownSetsPositionX.Minimum = int.MinValue;
            this.numericUpDownSetsPositionX.Maximum = int.MaxValue;

            this.numericUpDownSetsPositionY.Minimum = int.MinValue;
            this.numericUpDownSetsPositionY.Maximum = int.MaxValue;

            this.comboBoxSetsStyle.BeginUpdate();
            this.comboBoxSetsStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool.SetsPenalty.Insert.Styles)));
            this.comboBoxSetsStyle.EndUpdate();

            this.comboBoxPenaltyShots.BeginUpdate();
            this.comboBoxPenaltyShots.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool.SetsPenalty.Insert.PenaltyShots)));
            this.comboBoxPenaltyShots.EndUpdate();
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(previewPipe);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "SetsPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownSetsPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "SetsPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownSetsPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "SetsStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxSetsStyle.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "PenaltyShots");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxPenaltyShots.DataBindings.Add(bind);

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

            this.numericUpDownSetsPositionX.DataBindings.Clear();
            this.numericUpDownSetsPositionY.DataBindings.Clear();
            this.comboBoxSetsStyle.DataBindings.Clear();
            this.comboBoxPenaltyShots.DataBindings.Clear();
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

        protected void setSetsPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                List<_Base.Sets.SingleSet> leftPlayerSets = new List<_Base.Sets.SingleSet>();
                leftPlayerSets.Add(new _Base.Sets.SingleSet(11, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid));
                leftPlayerSets.Add(new _Base.Sets.SingleSet(12, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid));
                leftPlayerSets.Add(new _Base.Sets.SingleSet(13, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid));
                leftPlayerSets.Add(new _Base.Sets.SingleSet(14, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid));
                leftPlayerSets.Add(new _Base.Sets.SingleSet(15, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid));
                leftPlayerSets.Add(new _Base.Sets.SingleSet());
                leftPlayerSets.Add(new _Base.Sets.SingleSet());
                leftPlayerSets.Add(new _Base.Sets.SingleSet());
                leftPlayerSets.Add(new _Base.Sets.SingleSet());
                leftPlayerSets.Add(new _Base.Sets.SingleSet());
                List<_Base.Sets.SingleSet> rightPlayerSets = new List<_Base.Sets.SingleSet>();
                rightPlayerSets.Add(new _Base.Sets.SingleSet(21, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid));
                rightPlayerSets.Add(new _Base.Sets.SingleSet(22, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid));
                rightPlayerSets.Add(new _Base.Sets.SingleSet(23, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid));
                rightPlayerSets.Add(new _Base.Sets.SingleSet(24, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid));
                rightPlayerSets.Add(new _Base.Sets.SingleSet());
                rightPlayerSets.Add(new _Base.Sets.SingleSet());
                rightPlayerSets.Add(new _Base.Sets.SingleSet());
                rightPlayerSets.Add(new _Base.Sets.SingleSet());
                rightPlayerSets.Add(new _Base.Sets.SingleSet());
                rightPlayerSets.Add(new _Base.Sets.SingleSet());
                this.business.Vinsert_SetSets(previewScene.Insert, leftPlayerSets.ToArray(), rightPlayerSets.ToArray());
                previewScene.Insert.SetIn();
                List<_Base.Penalty.SingleDot> penaltyDots = new List<_Base.Penalty.SingleDot>();
                penaltyDots.Add(new _Base.Penalty.SingleDot(VentuzScenes.GamePool._Modules.Penalty.DotStates.Red));
                penaltyDots.Add(new _Base.Penalty.SingleDot(VentuzScenes.GamePool._Modules.Penalty.DotStates.Red));
                penaltyDots.Add(new _Base.Penalty.SingleDot(VentuzScenes.GamePool._Modules.Penalty.DotStates.Green));
                this.business.Vinsert_SetPenalty(previewScene.Insert, this.business.PenaltyShots, Insert.PenaltyPositions.Top, penaltyDots.ToArray());
                previewScene.Insert.SetPenaltyIn();
            }
        }

        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setSetsPreview();
        }

        #endregion

        #region Events.Controls

        protected virtual void numericUpDownSetsPositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.SetsPositionX = (int)this.numericUpDownSetsPositionX.Value;
                this.setSetsPreview();
            }
        }
        protected virtual void numericUpDownSetsPositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.SetsPositionY = (int)this.numericUpDownSetsPositionY.Value;
                this.setSetsPreview();
            }
        }
        protected virtual void comboBoxSetsStyle_SelectedIndexChanged(object sender, EventArgs e) { 
            VentuzScenes.GamePool.SetsPenalty.Insert.Styles style;
            if (this.business is Business && 
                Enum.TryParse(this.comboBoxSetsStyle.Text, out style)) {
                this.business.SetsStyle = style;
                this.setSetsPreview();
            }
        }

        private void comboBoxPenaltyShots_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool.SetsPenalty.Insert.PenaltyShots shots;
            if (this.business is Business &&
                Enum.TryParse(this.comboBoxPenaltyShots.Text, out shots)) {
                this.business.PenaltyShots = shots;
                this.setSetsPreview();
            }
        }

        #endregion

    }
}
