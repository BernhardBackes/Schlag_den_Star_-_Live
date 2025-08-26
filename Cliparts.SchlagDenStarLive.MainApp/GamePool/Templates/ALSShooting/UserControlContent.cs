using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.ALSShooting;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.ALSShooting {

    public partial class UserControlContent : _Base.ShootingScore.UserControlContent {

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

            Binding bind;

            bind = new Binding("Text", this.business, "ALSHost");
            bind.Format += (s, e) => { e.Value.ToString(); };
            this.textBoxALSHost.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftPlayerTargets");
            bind.Format += (s, e) => { e.Value.ToString(); };
            this.textBoxALSLeftPlayerTargets.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerTargets");
            bind.Format += (s, e) => { e.Value.ToString(); };
            this.textBoxALSRightPlayerTargets.DataBindings.Add(bind);

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

            this.textBoxALSHost.DataBindings.Clear();
            this.textBoxALSLeftPlayerTargets.DataBindings.Clear();
            this.textBoxALSRightPlayerTargets.DataBindings.Clear();
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

        protected override void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetScore(previewScene.Insert.Score, 2, 3);
                if (!this.showShootingInsert) previewScene.Insert.Score.SetIn();
                else previewScene.Insert.Score.SetOut();
            }
        }

        protected override void setShootingPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetShooting(previewScene.Insert.Shooting, 3, 1, 2, 3);
                if (this.showShootingInsert) {
                    previewScene.Insert.Shooting.SetIn();
                    previewScene.Insert.Shooting.SetLeftTopHitsIn();
                    previewScene.Insert.Shooting.SetRightBottomHitsIn();
                }
                else previewScene.Insert.Shooting.SetOut();
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

        private void textBoxALSHost_TextChanged(object sender, EventArgs e) { this.business.ALSHost = this.textBoxALSHost.Text; }
        private void textBoxALSLeftPlayerTargets_TextChanged(object sender, EventArgs e) { this.business.LeftPlayerTargets = this.textBoxALSLeftPlayerTargets.Text; }
        private void textBoxALSRightPlayerTargets_TextChanged(object sender, EventArgs e) { this.business.RightPlayerTargets = this.textBoxALSRightPlayerTargets.Text; }

        #endregion

    }

}
