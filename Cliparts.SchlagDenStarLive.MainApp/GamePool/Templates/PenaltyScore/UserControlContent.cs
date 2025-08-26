using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.PenaltyScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.PenaltyScore {

    public partial class UserControlContent : _Base.PenaltyScore.UserControlContent {

        #region Properties

        private Business business;

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;

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

        protected override void setPenaltyPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                _Base.PenaltyScore.SingleDot[] leftPlayerPenaltyDots = new _Base.PenaltyScore.SingleDot[] { 
                    new _Base.PenaltyScore.SingleDot(VentuzScenes.GamePool._Modules.PenaltyScore.DotStates.Green),
                    new _Base.PenaltyScore.SingleDot(VentuzScenes.GamePool._Modules.PenaltyScore.DotStates.Red),
                    new _Base.PenaltyScore.SingleDot(VentuzScenes.GamePool._Modules.PenaltyScore.DotStates.Green),
                    new _Base.PenaltyScore.SingleDot(VentuzScenes.GamePool._Modules.PenaltyScore.DotStates.Red),
                    new _Base.PenaltyScore.SingleDot(VentuzScenes.GamePool._Modules.PenaltyScore.DotStates.Green),
                    new _Base.PenaltyScore.SingleDot(VentuzScenes.GamePool._Modules.PenaltyScore.DotStates.Red),
                    new _Base.PenaltyScore.SingleDot(VentuzScenes.GamePool._Modules.PenaltyScore.DotStates.Green),
                    new _Base.PenaltyScore.SingleDot(VentuzScenes.GamePool._Modules.PenaltyScore.DotStates.Red),
                    new _Base.PenaltyScore.SingleDot(VentuzScenes.GamePool._Modules.PenaltyScore.DotStates.Green),
                    new _Base.PenaltyScore.SingleDot(VentuzScenes.GamePool._Modules.PenaltyScore.DotStates.Red) };
                _Base.PenaltyScore.SingleDot[] rightPlayerPenaltyDots = new _Base.PenaltyScore.SingleDot[] { 
                    new _Base.PenaltyScore.SingleDot(VentuzScenes.GamePool._Modules.PenaltyScore.DotStates.Red),
                    new _Base.PenaltyScore.SingleDot(VentuzScenes.GamePool._Modules.PenaltyScore.DotStates.Red),
                    new _Base.PenaltyScore.SingleDot(VentuzScenes.GamePool._Modules.PenaltyScore.DotStates.Green),
                    new _Base.PenaltyScore.SingleDot(VentuzScenes.GamePool._Modules.PenaltyScore.DotStates.Green),
                    new _Base.PenaltyScore.SingleDot(VentuzScenes.GamePool._Modules.PenaltyScore.DotStates.Red),
                    new _Base.PenaltyScore.SingleDot(VentuzScenes.GamePool._Modules.PenaltyScore.DotStates.Red),
                    new _Base.PenaltyScore.SingleDot(VentuzScenes.GamePool._Modules.PenaltyScore.DotStates.Green),
                    new _Base.PenaltyScore.SingleDot(VentuzScenes.GamePool._Modules.PenaltyScore.DotStates.Green),
                    new _Base.PenaltyScore.SingleDot(VentuzScenes.GamePool._Modules.PenaltyScore.DotStates.Red),
                    new _Base.PenaltyScore.SingleDot(VentuzScenes.GamePool._Modules.PenaltyScore.DotStates.Red) };
                this.business.Vinsert_SetPenalty(previewScene.Insert.PenaltyScore, leftPlayerPenaltyDots, rightPlayerPenaltyDots);
                previewScene.Insert.PenaltyScore.SetIn();
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

        #endregion
    }

}
