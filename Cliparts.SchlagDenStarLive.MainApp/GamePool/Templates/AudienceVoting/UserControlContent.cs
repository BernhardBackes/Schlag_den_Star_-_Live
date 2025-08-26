using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.AudienceVoting;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.AudienceVoting {

    public partial class UserControlContent : _Base.Timer.UserControlContent {

        #region Properties

        private Business business;

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownContentPositionX.Minimum = int.MinValue;
            this.numericUpDownContentPositionX.Maximum = int.MaxValue;

            this.numericUpDownContentPositionY.Minimum = int.MinValue;
            this.numericUpDownContentPositionY.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "ContentPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownContentPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "ContentPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownContentPositionY.DataBindings.Add(bind);

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

            this.numericUpDownContentPositionX.DataBindings.Clear();
            this.numericUpDownContentPositionY.DataBindings.Clear();
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

        protected void setInsertPreview() {
            if (this.previewSceneIsAvailable) {
                this.setTimerPreview();
                this.setContentPreview();
            }
        }

        protected override void setTimerPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_Timer.Set(previewScene.Insert.Timer);
                previewScene.Insert.Timer.SetIn();
            }
        }

        protected void setContentPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetGame(previewScene.Insert, 39);
                previewScene.Insert.Game.SetIn();
            }
        }


        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setInsertPreview();
        }

        #endregion

        #region Events.Controls

        protected virtual void numericUpDownContentPositionX_ValueChanged(object sender, EventArgs e) {
            this.business.ContentPositionX = (int)this.numericUpDownContentPositionX.Value;
            this.setContentPreview();
        }
        protected virtual void numericUpDownContentPositionY_ValueChanged(object sender, EventArgs e) {
            this.business.ContentPositionY = (int)this.numericUpDownContentPositionY.Value;
            this.setContentPreview();
        }

        #endregion

    }
}
