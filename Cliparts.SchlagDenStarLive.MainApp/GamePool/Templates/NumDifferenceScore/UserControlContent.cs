using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.NumDifferenceScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.NumDifferenceScore {

    public partial class UserControlContent : _Base.Score.UserControlContent {

        #region Properties

        private Business business;

        private bool _showNumericValuesInsert = false;
        public bool showNumericValuesInsert {
            get { return this._showNumericValuesInsert; }
            set {
                if (this._showNumericValuesInsert != value) {
                    this._showNumericValuesInsert = value;
                    this.setPreviewData();
                }
            }
        }

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownNumericValuesPositionX.Minimum = int.MinValue;
            this.numericUpDownNumericValuesPositionX.Maximum = int.MaxValue;

            this.numericUpDownNumericValuesPositionY.Minimum = int.MinValue;
            this.numericUpDownNumericValuesPositionY.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "NumericValuesPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownNumericValuesPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "NumericValuesPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownNumericValuesPositionY.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "Show1stHalf");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxShow1stHalf.DataBindings.Add(bind);

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

            this.numericUpDownNumericValuesPositionX.DataBindings.Clear();
            this.numericUpDownNumericValuesPositionY.DataBindings.Clear();

            this.checkBoxShow1stHalf.DataBindings.Clear();
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

        private void setPreviewData() {
            this.radioButtonSourceScore.Checked = !this.showNumericValuesInsert;
            this.radioButtonSourceNumericValues.Checked = this.showNumericValuesInsert;
            this.setScorePreview();
            this.setNumericValuesPreview();
        }

        protected override void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetScore(previewScene.Insert.Score, 3, 2);
                if (this.showNumericValuesInsert) previewScene.Insert.Score.SetOut();
                else previewScene.Insert.Score.SetIn();
            }
        }

        protected void setNumericValuesPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetNumericValues(previewScene.Insert.NumericValuesInsert, "123,4", string.Empty, "56,7", string.Empty, VentuzScenes.GamePool._Modules.NumericValuesInsert.BorderPosition.Bottom);
                if (this.showNumericValuesInsert) previewScene.Insert.NumericValuesInsert.SetIn();
                else previewScene.Insert.NumericValuesInsert.SetOut();
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

        protected virtual void numericUpDownNumericValuesPositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.NumericValuesPositionX = (int)this.numericUpDownNumericValuesPositionX.Value;
                this.setNumericValuesPreview();
            }
        }
        protected virtual void numericUpDownNumericValuesPositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.NumericValuesPositionY = (int)this.numericUpDownNumericValuesPositionY.Value;
                this.setNumericValuesPreview();
            }
        }

        private void checkBoxShow1stHalf_CheckedChanged(object sender, EventArgs e) { this.business.Show1stHalf = this.checkBoxShow1stHalf.Checked; }

        private void radioButtonSourceScore_CheckedChanged(object sender, EventArgs e) { this.showNumericValuesInsert = !this.radioButtonSourceScore.Checked; }
        private void radioButtonSourceNumericValues_CheckedChanged(object sender, EventArgs e) { this.showNumericValuesInsert = this.radioButtonSourceNumericValues.Checked; }

        #endregion
    }

}
