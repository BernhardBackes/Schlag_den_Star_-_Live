using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.DotsAndBoxes;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.DotsAndBoxes {

    public partial class UserControlContent : _Base.TimerScore.UserControlContent {

        #region Properties

        private Business business;

        private bool previewSceneIsAvailable { get { return this.previewScene is VentuzScenes.GamePool.Timer.Insert && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownBorderPositionX.Minimum = int.MinValue;
            this.numericUpDownBorderPositionX.Maximum = int.MaxValue;

            this.numericUpDownBorderPositionY.Minimum = int.MinValue;
            this.numericUpDownBorderPositionY.Maximum = int.MaxValue;

            this.numericUpDownBorderScaling.Minimum = int.MinValue;
            this.numericUpDownBorderScaling.Maximum = int.MaxValue;

            this.comboBoxBorderStyle.BeginUpdate();
            this.comboBoxBorderStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.Border.Styles)));
            this.comboBoxBorderStyle.EndUpdate();
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "BorderPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownBorderPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "BorderPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownBorderPositionY.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "BorderScaling");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownBorderScaling.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "BorderStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxBorderStyle.DataBindings.Add(bind);

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

            this.numericUpDownBorderPositionX.DataBindings.Clear();
            this.numericUpDownBorderPositionY.DataBindings.Clear();
            this.comboBoxBorderStyle.DataBindings.Clear();
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

        protected override void setTimerPreview() {
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

        private void numericUpDownBorderPositionX_ValueChanged(object sender, EventArgs e) { this.business.BorderPositionX = (int)this.numericUpDownBorderPositionX.Value; }
        private void numericUpDownBorderPositionY_ValueChanged(object sender, EventArgs e) { this.business.BorderPositionY = (int)this.numericUpDownBorderPositionY.Value; }
        private void numericUpDownBorderScaling_ValueChanged(object sender, EventArgs e) { this.business.BorderScaling = (int)this.numericUpDownBorderScaling.Value; }
        private void comboBoxBorderStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.Border.Styles style;
            if (Enum.TryParse(this.comboBoxBorderStyle.Text, out style)) this.business.BorderStyle = style;
        }

        #endregion
    }
}
