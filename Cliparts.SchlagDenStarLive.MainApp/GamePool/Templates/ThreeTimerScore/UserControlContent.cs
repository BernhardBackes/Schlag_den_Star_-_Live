using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.ThreeTimerScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.ThreeTimerScore {

    public partial class UserControlContent :_Base.TimerScore.UserControlContent {

        #region Properties

        private Business business;

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownLeftTimerPositionX.Minimum = int.MinValue;
            this.numericUpDownLeftTimerPositionX.Maximum = int.MaxValue;

            this.numericUpDownLeftTimerPositionY.Minimum = int.MinValue;
            this.numericUpDownLeftTimerPositionY.Maximum = int.MaxValue;

            this.comboBoxLeftTimerStyle.BeginUpdate();
            this.comboBoxLeftTimerStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.Timer.Styles)));
            this.comboBoxLeftTimerStyle.EndUpdate();

            this.numericUpDownLeftTimerStartTime.Minimum = int.MinValue;
            this.numericUpDownLeftTimerStartTime.Maximum = int.MaxValue;

            this.numericUpDownLeftTimerStopTime.Minimum = int.MinValue;
            this.numericUpDownLeftTimerStopTime.Maximum = int.MaxValue;

            this.numericUpDownRightTimerPositionX.Minimum = int.MinValue;
            this.numericUpDownRightTimerPositionX.Maximum = int.MaxValue;

            this.numericUpDownRightTimerPositionY.Minimum = int.MinValue;
            this.numericUpDownRightTimerPositionY.Maximum = int.MaxValue;

            this.comboBoxRightTimerStyle.BeginUpdate();
            this.comboBoxRightTimerStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.Timer.Styles)));
            this.comboBoxRightTimerStyle.EndUpdate();

            this.numericUpDownRightTimerStartTime.Minimum = int.MinValue;
            this.numericUpDownRightTimerStartTime.Maximum = int.MaxValue;

            this.numericUpDownRightTimerStopTime.Minimum = int.MinValue;
            this.numericUpDownRightTimerStopTime.Maximum = int.MaxValue;

            this.numericUpDownLeftRightTimerPositionX.Minimum = int.MinValue;
            this.numericUpDownLeftRightTimerPositionX.Maximum = int.MaxValue;

            this.numericUpDownLeftRightTimerPositionY.Minimum = int.MinValue;
            this.numericUpDownLeftRightTimerPositionY.Maximum = int.MaxValue;

        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "LeftTimerPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftTimerPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftTimerPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftTimerPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftTimerStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxLeftTimerStyle.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftTimerStartTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftTimerStartTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftTimerStartTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelLeftTimerStartTimeText.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftTimerStopTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftTimerStopTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftTimerStopTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelLeftTimerStopTimeText.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightTimerPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightTimerPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightTimerPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightTimerPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightTimerStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxRightTimerStyle.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightTimerStartTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightTimerStartTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightTimerStartTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelRightTimerStartTimeText.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightTimerStopTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightTimerStopTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightTimerStopTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelRightTimerStopTimeText.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftRightTimerPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftRightTimerPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftRightTimerPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftRightTimerPositionY.DataBindings.Add(bind);

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

            this.numericUpDownLeftTimerPositionX.DataBindings.Clear();
            this.numericUpDownLeftTimerPositionY.DataBindings.Clear();
            this.comboBoxLeftTimerStyle.DataBindings.Clear();
            this.numericUpDownLeftTimerStartTime.DataBindings.Clear();
            this.labelLeftTimerStartTimeText.DataBindings.Clear();
            this.numericUpDownLeftTimerStopTime.DataBindings.Clear();
            this.labelLeftTimerStopTimeText.DataBindings.Clear();

            this.numericUpDownRightTimerPositionX.DataBindings.Clear();
            this.numericUpDownRightTimerPositionY.DataBindings.Clear();
            this.comboBoxRightTimerStyle.DataBindings.Clear();
            this.numericUpDownRightTimerStartTime.DataBindings.Clear();
            this.labelRightTimerStartTimeText.DataBindings.Clear();
            this.numericUpDownRightTimerStopTime.DataBindings.Clear();
            this.labelRightTimerStopTimeText.DataBindings.Clear();

            this.numericUpDownLeftRightTimerPositionX.DataBindings.Clear();
            this.numericUpDownLeftRightTimerPositionY.DataBindings.Clear();
        }

        public override void SetPreviewPipe(
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            bool selected = ((VentuzScenes.GamePool.Timer.Insert)this.previewScene) is VRemote4.HandlerSi.Scene;
            base.SetPreviewPipe(previewPipe);
            if (selected) this.Select();
        }

        public override void Select() {
            if (this.previewPipe is VRemote4.HandlerSi.Client.Pipe.Business &&
                this.previewPipe.Resolution.HasValue &&
                this.previewPipe.ShareHandle.HasValue) {
                base.select(new VentuzScenes.GamePool.Timer.Insert(WindowsFormsSynchronizationContext.Current, this.previewPipe));
            }
        }

        private void setTimerPreview() {
            if (this.previewSceneIsAvailable) {
                ((VentuzScenes.GamePool.Timer.Insert)this.previewScene).Timer.SetPositionX(this.business.TimerPositionX);
                ((VentuzScenes.GamePool.Timer.Insert)this.previewScene).Timer.SetPositionY(this.business.TimerPositionY);
                ((VentuzScenes.GamePool.Timer.Insert)this.previewScene).Timer.SetStyle(this.business.TimerStyle);
                ((VentuzScenes.GamePool.Timer.Insert)this.previewScene).Timer.SetScaling(100);
                ((VentuzScenes.GamePool.Timer.Insert)this.previewScene).Timer.SetStartTime(this.business.TimerStartTime);
                ((VentuzScenes.GamePool.Timer.Insert)this.previewScene).Timer.SetIn();
            }
        }

        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setTimerPreview();
        }

        #endregion

        #region Events.Controls

        protected override void numericUpDownTimerPositionX_ValueChanged(object sender, EventArgs e) {
            base.numericUpDownTimerPositionX_ValueChanged(sender, e);
            this.setTimerPreview();
        }

        protected override void numericUpDownTimerPositionY_ValueChanged(object sender, EventArgs e) {
            base.numericUpDownTimerPositionY_ValueChanged(sender, e);
            this.setTimerPreview();
        }

        protected override void comboBoxTimerStyle_SelectedIndexChanged(object sender, EventArgs e) {
            base.comboBoxTimerStyle_SelectedIndexChanged(sender, e);
            this.setTimerPreview();
        }

        protected override void numericUpDownTimerStartTime_ValueChanged(object sender, EventArgs e) {
            base.numericUpDownTimerStartTime_ValueChanged(sender, e);
            this.setTimerPreview();
        }

        protected virtual void numericUpDownLeftTimerPositionX_ValueChanged(object sender, EventArgs e) { this.business.LeftTimerPositionX = (int)this.numericUpDownLeftTimerPositionX.Value; }
        protected virtual void numericUpDownLeftTimerPositionY_ValueChanged(object sender, EventArgs e) { this.business.LeftTimerPositionY = (int)this.numericUpDownLeftTimerPositionY.Value; }
        protected virtual void comboBoxLeftTimerStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.Timer.Styles style;
            if (Enum.TryParse(this.comboBoxLeftTimerStyle.Text, out style)) this.business.LeftTimerStyle = style;
        }
        protected virtual void numericUpDownLeftTimerStartTime_ValueChanged(object sender, EventArgs e) { this.business.LeftTimerStartTime = (int)this.numericUpDownLeftTimerStartTime.Value; }
        protected virtual void numericUpDownLeftTimerStopTime_ValueChanged(object sender, EventArgs e) { this.business.LeftTimerStopTime = (int)this.numericUpDownLeftTimerStopTime.Value; }

        protected virtual void numericUpDownRightTimerPositionX_ValueChanged(object sender, EventArgs e) { this.business.RightTimerPositionX = (int)this.numericUpDownRightTimerPositionX.Value; }
        protected virtual void numericUpDownRightTimerPositionY_ValueChanged(object sender, EventArgs e) { this.business.RightTimerPositionY = (int)this.numericUpDownRightTimerPositionY.Value; }
        protected virtual void comboBoxRightTimerStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.Timer.Styles style;
            if (Enum.TryParse(this.comboBoxRightTimerStyle.Text, out style)) this.business.RightTimerStyle = style;
        }
        protected virtual void numericUpDownRightTimerStartTime_ValueChanged(object sender, EventArgs e) { this.business.RightTimerStartTime = (int)this.numericUpDownRightTimerStartTime.Value; }
        protected virtual void numericUpDownRightTimerStopTime_ValueChanged(object sender, EventArgs e) { this.business.RightTimerStopTime = (int)this.numericUpDownRightTimerStopTime.Value; }

        protected virtual void numericUpDownLeftRightTimerPositionX_ValueChanged(object sender, EventArgs e) { this.business.LeftRightTimerPositionX = (int)this.numericUpDownLeftRightTimerPositionX.Value; }
        protected virtual void numericUpDownLeftRightTimerPositionY_ValueChanged(object sender, EventArgs e) { this.business.LeftRightTimerPositionY = (int)this.numericUpDownLeftRightTimerPositionY.Value; }

        #endregion
    }
}
