using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.LevelsChecked;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.LevelsChecked {

    public partial class UserControlContent : _Base.LevelsChecked.UserControlContent {

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

        protected override void setLevelsCheckedPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                _Base.LevelsChecked.SingleDot[] leftPlayerLevelsCheckedDots = new _Base.LevelsChecked.SingleDot[] { 
                    new _Base.LevelsChecked.SingleDot(true),
                    new _Base.LevelsChecked.SingleDot(false),
                    new _Base.LevelsChecked.SingleDot(true),
                    new _Base.LevelsChecked.SingleDot(false),
                    new _Base.LevelsChecked.SingleDot(true),
                    new _Base.LevelsChecked.SingleDot(false),
                    new _Base.LevelsChecked.SingleDot(true),
                    new _Base.LevelsChecked.SingleDot(false),
                    new _Base.LevelsChecked.SingleDot(true),
                    new _Base.LevelsChecked.SingleDot(false) };
                _Base.LevelsChecked.SingleDot[] rightPlayerLevelsCheckedDots = new _Base.LevelsChecked.SingleDot[] { 
                    new _Base.LevelsChecked.SingleDot(false),
                    new _Base.LevelsChecked.SingleDot(false),
                    new _Base.LevelsChecked.SingleDot(true),
                    new _Base.LevelsChecked.SingleDot(true),
                    new _Base.LevelsChecked.SingleDot(false),
                    new _Base.LevelsChecked.SingleDot(false),
                    new _Base.LevelsChecked.SingleDot(true),
                    new _Base.LevelsChecked.SingleDot(true),
                    new _Base.LevelsChecked.SingleDot(false),
                    new _Base.LevelsChecked.SingleDot(false) };
                this.business.Vinsert_SetLevelsChecked(previewScene.Insert.LevelsChecked, leftPlayerLevelsCheckedDots, rightPlayerLevelsCheckedDots);
                previewScene.Insert.LevelsChecked.SetIn();
            }
        }

        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setLevelsCheckedPreview();
        }

        #endregion

        #region Events.Controls

        protected override void numericUpDownLevelsCheckedPositionX_ValueChanged(object sender, EventArgs e) {
            base.numericUpDownLevelsCheckedPositionX_ValueChanged(sender, e);
            this.setLevelsCheckedPreview();
        }
        protected override void numericUpDownLevelsCheckedPositionY_ValueChanged(object sender, EventArgs e) {
            base.numericUpDownLevelsCheckedPositionY_ValueChanged(sender, e);
            this.setLevelsCheckedPreview();
        }

        #endregion
    }

}
