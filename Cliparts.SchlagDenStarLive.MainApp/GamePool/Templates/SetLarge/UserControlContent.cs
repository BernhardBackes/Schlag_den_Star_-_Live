using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.SetLarge {

    public partial class UserControlContent : _Base.SetLarge.UserControlContent {

        #region Properties

        private Business business;

        private bool previewSceneIsAvailable { get { return this.previewScene is VentuzScenes.GamePool.SetLarge.Insert && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

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
            bool selected = ((VentuzScenes.GamePool.SetLarge.Insert)this.previewScene) is VRemote4.HandlerSi.Scene;
            base.SetPreviewPipe(previewPipe);
            if (selected) this.Select();
        }

        public override void Select() {
            if (this.previewPipe is VRemote4.HandlerSi.Client.Pipe.Business &&
                this.previewPipe.Resolution.HasValue &&
                this.previewPipe.ShareHandle.HasValue) {
                base.select(new VentuzScenes.GamePool.SetLarge.Insert(WindowsFormsSynchronizationContext.Current, this.previewPipe));
            }
        }

        private void setSetLargePreview() {
            if (this.previewSceneIsAvailable) {
                ((VentuzScenes.GamePool.SetLarge.Insert)this.previewScene).SetLarge.SetPositionX(this.business.SetLargePositionX);
                ((VentuzScenes.GamePool.SetLarge.Insert)this.previewScene).SetLarge.SetPositionY(this.business.SetLargePositionY);
                ((VentuzScenes.GamePool.SetLarge.Insert)this.previewScene).SetLarge.SetTopName(this.business.LeftPlayerName);
                ((VentuzScenes.GamePool.SetLarge.Insert)this.previewScene).SetLarge.SetBottomName(this.business.RightPlayerName);
                ((VentuzScenes.GamePool.SetLarge.Insert)this.previewScene).SetLarge.SetIn();
            }
        }

        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setSetLargePreview();
        }

        #endregion

        #region Events.Controls

        protected override void numericUpDownSetLargePositionX_ValueChanged(object sender, EventArgs e) {
            base.numericUpDownSetLargePositionX_ValueChanged(sender, e);
            this.setSetLargePreview();
        }
        protected override void numericUpDownSetLargePositionY_ValueChanged(object sender, EventArgs e) {
            base.numericUpDownSetLargePositionY_ValueChanged(sender, e);
            this.setSetLargePreview();
        }

        #endregion
    }

}
