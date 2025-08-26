using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.ValuedSets;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.ValuedSets {

    public partial class UserControlContent : _Base.Sets.UserControlContent {

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

        protected override void setSetsPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                List<_Base.Sets.SingleSet> leftPlayerSets = new List<_Base.Sets.SingleSet>();
                leftPlayerSets.Add(new _Base.Sets.SingleSet(11, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid));
                leftPlayerSets.Add(new _Base.Sets.SingleSet(12, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid));
                leftPlayerSets.Add(new _Base.Sets.SingleSet(13, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid));
                leftPlayerSets.Add(new _Base.Sets.SingleSet(14, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid));
                leftPlayerSets.Add(new _Base.Sets.SingleSet(15, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid));
                leftPlayerSets.Add(new _Base.Sets.SingleSet());
                leftPlayerSets.Add(new _Base.Sets.SingleSet());
                leftPlayerSets.Add(new _Base.Sets.SingleSet());
                leftPlayerSets.Add(new _Base.Sets.SingleSet());
                leftPlayerSets.Add(new _Base.Sets.SingleSet());
                List<_Base.Sets.SingleSet> rightPlayerSets = new List<_Base.Sets.SingleSet>();
                rightPlayerSets.Add(new _Base.Sets.SingleSet(21, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid));
                rightPlayerSets.Add(new _Base.Sets.SingleSet(22, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid));
                rightPlayerSets.Add(new _Base.Sets.SingleSet(23, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid));
                rightPlayerSets.Add(new _Base.Sets.SingleSet(24, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid));
                rightPlayerSets.Add(new _Base.Sets.SingleSet());
                rightPlayerSets.Add(new _Base.Sets.SingleSet());
                rightPlayerSets.Add(new _Base.Sets.SingleSet());
                rightPlayerSets.Add(new _Base.Sets.SingleSet());
                rightPlayerSets.Add(new _Base.Sets.SingleSet());
                rightPlayerSets.Add(new _Base.Sets.SingleSet());
                this.business.Vinsert_SetSets(previewScene.Insert.Sets, 0, 0, leftPlayerSets.ToArray(), rightPlayerSets.ToArray());
                previewScene.Insert.Sets.SetIn();
            }
        }

        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setSetsPreview();
        }

        #endregion

        #region Events.Controls

        protected override void numericUpDownSetsPositionX_ValueChanged(object sender, EventArgs e) {
            base.numericUpDownSetsPositionX_ValueChanged(sender, e);
            this.setSetsPreview();
        }
        protected override void numericUpDownSetsPositionY_ValueChanged(object sender, EventArgs e) {
            base.numericUpDownSetsPositionY_ValueChanged(sender, e);
            this.setSetsPreview();
        }
        protected override void comboBoxSetsStyle_SelectedIndexChanged(object sender, EventArgs e) {
            base.comboBoxSetsStyle_SelectedIndexChanged(sender, e);
            this.setSetsPreview();
        }

        #endregion
    }

}
