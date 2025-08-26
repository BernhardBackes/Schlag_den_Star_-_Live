using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.ShootingScore {

    public partial class UserControlContent : _Base.UserControlContent {

        #region Properties

        private Business business;

        private bool _showShootingInsert = false;
        public bool showShootingInsert {
            get { return this._showShootingInsert; }
            set {
                if (this._showShootingInsert != value) {
                    this._showShootingInsert = value;
                    this.setPreviewData();
                }
            }
        }

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

            this.numericUpDownShootingPositionX.Minimum = int.MinValue;
            this.numericUpDownShootingPositionX.Maximum = int.MaxValue;

            this.numericUpDownShootingPositionY.Minimum = int.MinValue;
            this.numericUpDownShootingPositionY.Maximum = int.MaxValue;

            this.comboBoxShootingStyle.BeginUpdate();
            this.comboBoxShootingStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.Shooting.Styles)));
            this.comboBoxShootingStyle.EndUpdate();

            this.numericUpDownShootingHitsCount.Minimum = int.MinValue;
            this.numericUpDownShootingHitsCount.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(previewPipe);

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

            bind = new Binding("Value", this.business, "ShootingPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownShootingPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "ShootingPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownShootingPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "ShootingStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxShootingStyle.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "HitsCount");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownShootingHitsCount.DataBindings.Add(bind);
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
            this.numericUpDownShootingPositionX.DataBindings.Clear();
            this.numericUpDownShootingPositionY.DataBindings.Clear();
            this.comboBoxShootingStyle.DataBindings.Clear();
            this.numericUpDownShootingHitsCount.DataBindings.Clear();
        }

        protected void setPreviewData() {
            this.radioButtonSourceScore.Checked = !this.showShootingInsert;
            this.radioButtonSourceShooting.Checked = this.showShootingInsert;
            this.setScorePreview();
            this.setShootingPreview();
        }

        protected virtual void setScorePreview() { }

        protected virtual void setShootingPreview() { }

        #endregion


        #region Events.Incoming
        #endregion

        #region Events.Controls

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

        protected virtual void numericUpDownShootingPositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.ShootingPositionX = (int)this.numericUpDownShootingPositionX.Value;
                this.setShootingPreview();
            }
        }
        protected virtual void numericUpDownShootingPositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.ShootingPositionY = (int)this.numericUpDownShootingPositionY.Value;
                this.setShootingPreview();
            }
        }
        protected virtual void comboBoxShootingStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.Shooting.Styles style;
            if (this.business is Business &&
                Enum.TryParse(this.comboBoxShootingStyle.Text, out style)) {
                this.business.ShootingStyle = style;
                this.setShootingPreview();
            }
        }
        private void numericUpDownShootingHitsCount_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.HitsCount = (int)this.numericUpDownShootingHitsCount.Value;
                this.setShootingPreview();
            }
        }

        private void radioButtonSourceScore_CheckedChanged(object sender, EventArgs e) { this.showShootingInsert = !this.radioButtonSourceScore.Checked; }
        private void radioButtonSourceShooting_CheckedChanged(object sender, EventArgs e) { this.showShootingInsert = this.radioButtonSourceShooting.Checked; }

        #endregion
    }
}
