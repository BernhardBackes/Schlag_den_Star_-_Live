using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.AddCounterToScore {

    public partial class UserControlContent : _Base.CounterInOutScore.UserControlContent {

        #region Properties

        private Business business;

        private bool previewSceneIsAvailable { get { return this.previewScene is VentuzScenes.GamePool.AddCounterToScore.Insert && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

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
            bool selected = ((VentuzScenes.GamePool.AddCounterToScore.Insert)this.previewScene) is VRemote4.HandlerSi.Scene;
            base.SetPreviewPipe(previewPipe);
            if (selected) this.Select();
        }

        public override void Select() {
            if (this.previewPipe is VRemote4.HandlerSi.Client.Pipe.Business &&
                this.previewPipe.Resolution.HasValue &&
                this.previewPipe.ShareHandle.HasValue) {
                base.select(new VentuzScenes.GamePool.AddCounterToScore.Insert(WindowsFormsSynchronizationContext.Current, this.previewPipe));
            }
        }

        private void setPreview() {
            if (this.previewSceneIsAvailable) {
                ((VentuzScenes.GamePool.AddCounterToScore.Insert)this.previewScene).CounterInOutScore.SetPositionX(this.business.CounterInOutScorePositionX);
                ((VentuzScenes.GamePool.AddCounterToScore.Insert)this.previewScene).CounterInOutScore.SetPositionY(this.business.CounterInOutScorePositionY);
                ((VentuzScenes.GamePool.AddCounterToScore.Insert)this.previewScene).CounterInOutScore.SetTopName(this.business.LeftPlayerName);
                ((VentuzScenes.GamePool.AddCounterToScore.Insert)this.previewScene).CounterInOutScore.SetTopScore(2);
                ((VentuzScenes.GamePool.AddCounterToScore.Insert)this.previewScene).CounterInOutScore.SetTopCounter(22);
                ((VentuzScenes.GamePool.AddCounterToScore.Insert)this.previewScene).CounterInOutScore.SetBottomName(this.business.RightPlayerName);
                ((VentuzScenes.GamePool.AddCounterToScore.Insert)this.previewScene).CounterInOutScore.SetBottomScore(3);
                ((VentuzScenes.GamePool.AddCounterToScore.Insert)this.previewScene).CounterInOutScore.SetBottomCounter(33);
                ((VentuzScenes.GamePool.AddCounterToScore.Insert)this.previewScene).CounterInOutScore.SetIn();
            }
        }

        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setPreview();
        }

        #endregion

        #region Events.Controls

        protected override void numericUpDownCounterInOutScorePositionX_ValueChanged(object sender, EventArgs e) {
            base.numericUpDownCounterInOutScorePositionX_ValueChanged(sender, e);
            this.setPreview();
        }

        protected override void numericUpDownCounterInOutScorePositionY_ValueChanged(object sender, EventArgs e) {
            base.numericUpDownCounterInOutScorePositionY_ValueChanged(sender, e);
            this.setPreview();
        }

        #endregion
    }

}
