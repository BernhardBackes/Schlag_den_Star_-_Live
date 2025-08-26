using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.ScorePyramid;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.ScorePyramid {

    public partial class UserControlContent : _Base.UserControlContent {

        #region Properties

        private Business business;

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownGamePositionX.Minimum = int.MinValue;
            this.numericUpDownGamePositionX.Maximum = int.MaxValue;

            this.numericUpDownGamePositionY.Minimum = int.MinValue;
            this.numericUpDownGamePositionY.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(previewPipe);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "GamePositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownGamePositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "GamePositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownGamePositionY.DataBindings.Add(bind);

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

            this.numericUpDownGamePositionX.DataBindings.Clear();
            this.numericUpDownGamePositionY.DataBindings.Clear();

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

        protected void setGamePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                DatasetLevel[] levellist = new DatasetLevel[Business.LevelsCount];
                levellist[0] = new DatasetLevel(0, 4, 6);
                levellist[1] = new DatasetLevel(1, 0, 1);
                levellist[2] = new DatasetLevel(2, 0, 1);
                levellist[3] = new DatasetLevel(3, 0, 0);
                levellist[4] = new DatasetLevel(4, 2, 0);
                levellist[5] = new DatasetLevel(5, 2, 1);
                levellist[6] = new DatasetLevel(6, 2, 0);
                levellist[7] = new DatasetLevel(7, 0, 3);
                levellist[8] = new DatasetLevel(8, 1, 2);
                levellist[9] = new DatasetLevel(9, 2, 0);
                levellist[10] = new DatasetLevel(10, 2, 1);
                this.business.Vinsert_SetGame(levellist, previewScene.Insert.Game);
                previewScene.Insert.Game.SetIn();
            }
        }

        #endregion


        #region Events.Incoming
        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setGamePreview();
        }

        #endregion

        #region Events.Controls

        protected virtual void numericUpDownGamePositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.GamePositionX = (int)this.numericUpDownGamePositionX.Value;
                this.setGamePreview();
            }
        }
        protected virtual void numericUpDownGamePositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.GamePositionY = (int)this.numericUpDownGamePositionY.Value;
                this.setGamePreview();
            }
        }

        #endregion

    }
}
