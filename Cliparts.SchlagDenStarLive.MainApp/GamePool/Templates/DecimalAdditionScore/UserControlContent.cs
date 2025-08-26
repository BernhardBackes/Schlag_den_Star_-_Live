using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.DecimalAdditionScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.DecimalAdditionScore {

    public partial class UserControlContent : _Base.DecimalAddition.UserControlContent {

        #region Properties

        private Business business;

        private bool _showDecimalAdditionInsert = false;
        public bool showDecimalAdditionInsert {
            get { return this._showDecimalAdditionInsert; }
            set {
                if (this._showDecimalAdditionInsert != value) {
                    this._showDecimalAdditionInsert = value;
                    this.setPreviewData();
                }
            }
        }

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownScorePositionX.Minimum = int.MinValue;
            this.numericUpDownScorePositionX.Maximum = int.MaxValue;

            this.numericUpDownScorePositionY.Minimum = int.MinValue;
            this.numericUpDownScorePositionY.Maximum = int.MaxValue;

            this.comboBoxScoreStyle.BeginUpdate();
            this.comboBoxScoreStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.Score.Styles)));
            this.comboBoxScoreStyle.EndUpdate();

        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "ScorePositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownScorePositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "ScorePositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownScorePositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "ScoreStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxScoreStyle.DataBindings.Add(bind);

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

            this.numericUpDownScorePositionX.DataBindings.Clear();
            this.numericUpDownScorePositionY.DataBindings.Clear();
            this.comboBoxScoreStyle.DataBindings.Clear();

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
            this.radioButtonSourceScore.Checked = !this.showDecimalAdditionInsert;
            this.radioButtonSourceDecimalAddition.Checked = this.showDecimalAdditionInsert;
            this.setScorePreview();
            this.setDecimalAdditionPreview();
        }

        protected void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetScore(previewScene.Insert.Score, 3, 2);
                if (this.showDecimalAdditionInsert) previewScene.Insert.Score.SetOut();
                else previewScene.Insert.Score.SetIn();
            }
        }

        protected override void setDecimalAdditionPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetDecimalAddition(
                    previewScene.Insert.DecimalAddition,
                    new _Base.DecimalAddition.SingleDecimalSet[] {
                        new _Base.DecimalAddition.SingleDecimalSet(1.11f, VentuzScenes.GamePool._Modules.DecimalAddition.ValueStates.Valid, true),
                        new _Base.DecimalAddition.SingleDecimalSet(2.22f, VentuzScenes.GamePool._Modules.DecimalAddition.ValueStates.Invalid, true),
                        new _Base.DecimalAddition.SingleDecimalSet(3.33f, VentuzScenes.GamePool._Modules.DecimalAddition.ValueStates.Idle, true),
                        new _Base.DecimalAddition.SingleDecimalSet(4.44f, VentuzScenes.GamePool._Modules.DecimalAddition.ValueStates.Valid, true) },
                    88.88f,
                    new _Base.DecimalAddition.SingleDecimalSet[] {
                        new _Base.DecimalAddition.SingleDecimalSet(11.11f, VentuzScenes.GamePool._Modules.DecimalAddition.ValueStates.Idle, true),
                        new _Base.DecimalAddition.SingleDecimalSet(22.22f, VentuzScenes.GamePool._Modules.DecimalAddition.ValueStates.Valid, true),
                        new _Base.DecimalAddition.SingleDecimalSet(33.33f, VentuzScenes.GamePool._Modules.DecimalAddition.ValueStates.Invalid, true),
                        new _Base.DecimalAddition.SingleDecimalSet(44.44f, VentuzScenes.GamePool._Modules.DecimalAddition.ValueStates.Idle, true) },
                    99.99f);

                if (this.showDecimalAdditionInsert) previewScene.Insert.DecimalAddition.SetIn();
                else previewScene.Insert.DecimalAddition.SetOut();
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

        protected override void numericUpDownDecimalAdditionPositionX_ValueChanged(object sender, EventArgs e) {
            base.numericUpDownDecimalAdditionPositionX_ValueChanged(sender, e);
            this.setDecimalAdditionPreview();
        }
        protected override void numericUpDownDecimalAdditionPositionY_ValueChanged(object sender, EventArgs e) {
            base.numericUpDownDecimalAdditionPositionY_ValueChanged(sender, e);
            this.setDecimalAdditionPreview();
        }
        protected override void comboBoxDecimalAdditionStyle_SelectedIndexChanged(object sender, EventArgs e) {
            base.comboBoxDecimalAdditionStyle_SelectedIndexChanged(sender, e);
            this.setDecimalAdditionPreview();
        }

        protected virtual void numericUpDownScorePositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.ScorePositionX = (int)this.numericUpDownScorePositionX.Value;
                this.setScorePreview();
            }
        }
        protected virtual void numericUpDownScorePositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.ScorePositionY = (int)this.numericUpDownScorePositionY.Value;
                this.setScorePreview();
            }
        }
        protected virtual void comboBoxScoreStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.Score.Styles style;
            if (this.business is Business &&
                Enum.TryParse(this.comboBoxScoreStyle.Text, out style)) {
                this.business.ScoreStyle = style;
                this.setScorePreview();
            }
        }

        private void radioButtonSourceScore_CheckedChanged(object sender, EventArgs e) { this.showDecimalAdditionInsert = !this.radioButtonSourceScore.Checked; }
        private void radioButtonSourceDecimalAddition_CheckedChanged(object sender, EventArgs e) { this.showDecimalAdditionInsert = this.radioButtonSourceDecimalAddition.Checked; }

        #endregion

    }

}
