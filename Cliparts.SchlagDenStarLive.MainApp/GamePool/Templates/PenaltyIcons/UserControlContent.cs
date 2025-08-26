using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.PenaltyIcons;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.PenaltyIcons {

    public partial class UserControlContent : _Base.PenaltyIcons.UserControlContent {

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
                _Base.PenaltyIcons.SingleDot[] leftPlayerPenaltyDots = new _Base.PenaltyIcons.SingleDot[] { 
                    new _Base.PenaltyIcons.SingleDot(VentuzScenes.GamePool._Modules.PenaltyIcons.DotStates.Green),
                    new _Base.PenaltyIcons.SingleDot(VentuzScenes.GamePool._Modules.PenaltyIcons.DotStates.Red),
                    new _Base.PenaltyIcons.SingleDot(VentuzScenes.GamePool._Modules.PenaltyIcons.DotStates.Green),
                    new _Base.PenaltyIcons.SingleDot(VentuzScenes.GamePool._Modules.PenaltyIcons.DotStates.Red),
                    new _Base.PenaltyIcons.SingleDot(VentuzScenes.GamePool._Modules.PenaltyIcons.DotStates.Green),
                    new _Base.PenaltyIcons.SingleDot(VentuzScenes.GamePool._Modules.PenaltyIcons.DotStates.Red),
                    new _Base.PenaltyIcons.SingleDot(VentuzScenes.GamePool._Modules.PenaltyIcons.DotStates.Green),
                    new _Base.PenaltyIcons.SingleDot(VentuzScenes.GamePool._Modules.PenaltyIcons.DotStates.Red),
                    new _Base.PenaltyIcons.SingleDot(VentuzScenes.GamePool._Modules.PenaltyIcons.DotStates.Green),
                    new _Base.PenaltyIcons.SingleDot(VentuzScenes.GamePool._Modules.PenaltyIcons.DotStates.Red) };
                _Base.PenaltyIcons.SingleDot[] rightPlayerPenaltyDots = new _Base.PenaltyIcons.SingleDot[] { 
                    new _Base.PenaltyIcons.SingleDot(VentuzScenes.GamePool._Modules.PenaltyIcons.DotStates.Red),
                    new _Base.PenaltyIcons.SingleDot(VentuzScenes.GamePool._Modules.PenaltyIcons.DotStates.Red),
                    new _Base.PenaltyIcons.SingleDot(VentuzScenes.GamePool._Modules.PenaltyIcons.DotStates.Green),
                    new _Base.PenaltyIcons.SingleDot(VentuzScenes.GamePool._Modules.PenaltyIcons.DotStates.Green),
                    new _Base.PenaltyIcons.SingleDot(VentuzScenes.GamePool._Modules.PenaltyIcons.DotStates.Red),
                    new _Base.PenaltyIcons.SingleDot(VentuzScenes.GamePool._Modules.PenaltyIcons.DotStates.Red),
                    new _Base.PenaltyIcons.SingleDot(VentuzScenes.GamePool._Modules.PenaltyIcons.DotStates.Green),
                    new _Base.PenaltyIcons.SingleDot(VentuzScenes.GamePool._Modules.PenaltyIcons.DotStates.Green),
                    new _Base.PenaltyIcons.SingleDot(VentuzScenes.GamePool._Modules.PenaltyIcons.DotStates.Red),
                    new _Base.PenaltyIcons.SingleDot(VentuzScenes.GamePool._Modules.PenaltyIcons.DotStates.Red) };
                this.business.Vinsert_SetPenalty(previewScene.Insert.PenaltyIcons, leftPlayerPenaltyDots, rightPlayerPenaltyDots);
                previewScene.Insert.PenaltyIcons.SetIn();
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

        //protected override void numericUpDownPenaltyPositionX_ValueChanged(object sender, EventArgs e) {
        //    base.numericUpDownPenaltyPositionX_ValueChanged(sender, e);
        //    this.setPenaltyPreview();
        //}
        //protected override void numericUpDownPenaltyPositionY_ValueChanged(object sender, EventArgs e) {
        //    base.numericUpDownPenaltyPositionY_ValueChanged(sender, e);
        //    this.setPenaltyPreview();
        //}
        //protected override void comboBoxPenaltyStyle_SelectedIndexChanged(object sender, EventArgs e) {
        //    base.comboBoxPenaltyStyle_SelectedIndexChanged(sender, e);
        //    this.setPenaltyPreview();
        //}

        #endregion
    }

}
