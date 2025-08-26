using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.DecimalSets;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.DecimalSets {

    public partial class UserControlContent : _Base.DecimalSets.UserControlContent {

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

        protected override void setDecimalSetsPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetDecimalSets(
                    previewScene.Insert.DecimalSets,
                    new _Base.DecimalSets.SingleDecimalSet[] {
                        new _Base.DecimalSets.SingleDecimalSet(1.11f, VentuzScenes.GamePool._Modules.DecimalSets.ValueStates.Valid, false),
                        new _Base.DecimalSets.SingleDecimalSet(3.33f, VentuzScenes.GamePool._Modules.DecimalSets.ValueStates.Valid, false),
                        new _Base.DecimalSets.SingleDecimalSet(5.55f, VentuzScenes.GamePool._Modules.DecimalSets.ValueStates.Valid, false),
                        new _Base.DecimalSets.SingleDecimalSet(7.77f, VentuzScenes.GamePool._Modules.DecimalSets.ValueStates.Valid, false) },
                    new _Base.DecimalSets.SingleDecimalSet[] {
                        new _Base.DecimalSets.SingleDecimalSet(8.88f, VentuzScenes.GamePool._Modules.DecimalSets.ValueStates.Valid, true),
                        new _Base.DecimalSets.SingleDecimalSet(2.22f, VentuzScenes.GamePool._Modules.DecimalSets.ValueStates.Valid, false),
                        new _Base.DecimalSets.SingleDecimalSet(4.44f, VentuzScenes.GamePool._Modules.DecimalSets.ValueStates.Valid, false),
                        new _Base.DecimalSets.SingleDecimalSet(6.66f, VentuzScenes.GamePool._Modules.DecimalSets.ValueStates.Valid, false) } );
                previewScene.Insert.DecimalSets.SetIn();
            }
        }

        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setDecimalSetsPreview();
        }

        #endregion

        #region Events.Controls

        protected override void numericUpDownDecimalSetsPositionX_ValueChanged(object sender, EventArgs e) {
            base.numericUpDownDecimalSetsPositionX_ValueChanged(sender, e);
            this.setDecimalSetsPreview();
        }
        protected override void numericUpDownDecimalSetsPositionY_ValueChanged(object sender, EventArgs e) {
            base.numericUpDownDecimalSetsPositionY_ValueChanged(sender, e);
            this.setDecimalSetsPreview();
        }
        protected override void comboBoxDecimalSetsStyle_SelectedIndexChanged(object sender, EventArgs e) {
            base.comboBoxDecimalSetsStyle_SelectedIndexChanged(sender, e);
            this.setDecimalSetsPreview();
        }

        #endregion
    }

}
