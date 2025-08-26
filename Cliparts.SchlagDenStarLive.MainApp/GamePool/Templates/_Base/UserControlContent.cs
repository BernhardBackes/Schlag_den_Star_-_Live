using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base {

    public partial class UserControlContent : UserControl {

        #region Properties

        protected VRemote4.HandlerSi.Client.Pipe.Business previewPipe = null;

        protected VRemote4.HandlerSi.Scene previewScene = null;

        private bool _showSafeArea = false;
        protected bool showSafeArea {
            get { return this._showSafeArea; }
            set {
                if (this._showSafeArea != value) {
                    this._showSafeArea = value;
                    this.setSafeArea(value);
                }
            }
        }
        
        //public virtual bool PreviewSceneIsActive { get { return false; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();
            this.checkBoxShowSafeArea.Checked = this.showSafeArea;
            this.groupBoxPreview.Enabled = false;
        }

        public virtual void Pose(
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            this.SetPreviewPipe(previewPipe);
        }

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            this.userControlViewer.Stop();
            base.Dispose(disposing);
        }

        public virtual void SetPreviewPipe(
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            if (this.previewPipe != previewPipe) {
                this.Deselect();
                this.previewPipe = previewPipe;
            }
        }

        public virtual new void Select() { 
            this.select(this.previewScene); 
        }

        protected virtual void select(
            VRemote4.HandlerSi.Scene scene) {
            this.Deselect();
            this.previewScene = scene;
            if (this.previewScene is VRemote4.HandlerSi.Scene) {
                this.previewScene.StatusChanged += previewScene_StatusChanged;
            }
            if (this.previewPipe is VRemote4.HandlerSi.Client.Pipe.Business &&
                this.previewPipe.Resolution.HasValue &&
                this.previewPipe.ShareHandle.HasValue) {
                this.userControlViewer.Start(this.previewPipe.Resolution.Value, this.previewPipe.ShareHandle.Value);
                if (this.previewScene is VRemote4.HandlerSi.Scene) this.previewScene.Load();
                else {
                    this.previewScene = new VentuzScenes.GamePool.Dummy(WindowsFormsSynchronizationContext.Current, this.previewPipe);
                    this.previewScene.Load();
                }
            }
        }

        public void Deselect() {
            this.userControlViewer.Stop();
            if (this.previewScene is VRemote4.HandlerSi.Scene) {
                this.previewScene.Unload();
                this.previewScene.StatusChanged -= previewScene_StatusChanged;
            }
        }

        private void setSafeArea(bool value) {
            if (this.previewScene is VentuzScenes._PreviewBase) ((VentuzScenes._PreviewBase)this.previewScene).SetShowSafeArea(value);
        }

        #endregion


        #region Events.Incoming

        protected virtual void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            this.pictureBoxLoading.Visible = this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Loading;
            this.groupBoxPreview.Enabled = this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available;
            this.setSafeArea(this.showSafeArea);
        }

        #endregion

        #region Events.Controls

        private void checkBoxShowSafeArea_CheckedChanged(object sender, EventArgs e) {
            this.showSafeArea = this.checkBoxShowSafeArea.Checked;
        }
        
        #endregion
    }
}
