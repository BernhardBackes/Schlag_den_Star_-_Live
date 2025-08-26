using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.BondedDots;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.BondedDots {

    public partial class UserControlContent : _Base.BondedDots.UserControlContent {

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

        protected override void setBondedDotsPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                int[] leftPlayerDots = new int[] { 1,2,3};
                int[] rightPlayerDots = new int[] { 2,3,3};
                this.business.Vinsert_SetBondedDots(previewScene.Insert.BondedDots, leftPlayerDots, rightPlayerDots);
                previewScene.Insert.BondedDots.SetIn();
            }
        }

        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setBondedDotsPreview();
        }

        #endregion

        #region Events.Controls

        protected override void numericUpDownBondedDotsPositionX_ValueChanged(object sender, EventArgs e) {
            base.numericUpDownBondedDotsPositionX_ValueChanged(sender, e);
            this.setBondedDotsPreview();
        }
        protected override void numericUpDownBondedDotsPositionY_ValueChanged(object sender, EventArgs e) {
            base.numericUpDownBondedDotsPositionY_ValueChanged(sender, e);
            this.setBondedDotsPreview();
        }
        protected override void comboBoxBondedDotsStyle_SelectedIndexChanged(object sender, EventArgs e) {
            base.comboBoxBondedDotsStyle_SelectedIndexChanged(sender, e);
            this.setBondedDotsPreview();
        }

        #endregion
    }

}
