using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.AMB;
using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimerForTwoStopoverAddition;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimerForTwoStopoverAddition {

    public partial class UserControlContent : _Base.Buzzer.UserControlContent {

        #region Properties

        private Business business;

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownTimerPositionX.Minimum = int.MinValue;
            this.numericUpDownTimerPositionX.Maximum = int.MaxValue;

            this.numericUpDownTimerPositionY.Minimum = int.MinValue;
            this.numericUpDownTimerPositionY.Maximum = int.MaxValue;

            this.numericUpDownTimerPositionX.Minimum = int.MinValue;
            this.numericUpDownTimerPositionX.Maximum = int.MaxValue;

            this.numericUpDownTimerPositionY.Minimum = int.MinValue;

            this.numericUpDownLeftStopoverChannel.Minimum = int.MinValue;
            this.numericUpDownLeftStopoverChannel.Maximum = int.MaxValue;

            this.numericUpDownRightStopoverChannel.Minimum = int.MinValue;
            this.numericUpDownRightStopoverChannel.Maximum = int.MaxValue;

            this.numericUpDownRoundsCount.Minimum = int.MinValue;
            this.numericUpDownRoundsCount.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "TimerPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerPositionY.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftStopoverChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftStopoverChannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightStopoverChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightStopoverChannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RoundsCount");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRoundsCount.DataBindings.Add(bind);

            this.labelGameClass.Text = this.business.ClassInfo;

            this.fillIOUnitList();
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

            this.business.PropertyChanged -= this.business_PropertyChanged;

            this.numericUpDownTimerPositionX.DataBindings.Clear();
            this.numericUpDownTimerPositionY.DataBindings.Clear();

            this.numericUpDownLeftStopoverChannel.DataBindings.Clear();
            this.numericUpDownRightStopoverChannel.DataBindings.Clear();

            this.numericUpDownRoundsCount.DataBindings.Clear();
        }

        private void bind_textBoxTimelineName_BackColor(object sender, ConvertEventArgs e) {
            switch ((TimelineStates)e.Value) {
                case TimelineStates.Offline:
                    e.Value = Constants.ColorMissing;
                    break;
                case TimelineStates.Locked:
                    e.Value = SystemColors.Control;
                    break;
                case TimelineStates.Unlocked:
                    e.Value = Constants.ColorEnabled;
                    break;
            }
        }

        private void fillIOUnitList() {
            //this.comboBoxIOUnit.BeginUpdate();
            //this.comboBoxIOUnit.Items.Clear();
            //this.comboBoxIOUnit.Items.AddRange(this.business.IOUnitNameList);
            //this.comboBoxIOUnit.EndUpdate();
        }

        private void bind_comboBoxIOUnitName_BackColor(object sender, ConvertEventArgs e) {
            switch ((BuzzerUnitStates)e.Value) {
                case BuzzerUnitStates.NotAvailable:
                    e.Value = Constants.ColorMissing;
                    break;
                case BuzzerUnitStates.Missing:
                    e.Value = Constants.ColorMissing;
                    break;
                case BuzzerUnitStates.Disconnected:
                    e.Value = Constants.ColorDisabled;
                    break;
                case BuzzerUnitStates.Connecting:
                    e.Value = Constants.ColorEnabling;
                    break;
                case BuzzerUnitStates.Connected:
                    e.Value = SystemColors.Control;
                    break;
                case BuzzerUnitStates.Locked:
                    e.Value = SystemColors.Control;
                    break;
                case BuzzerUnitStates.BuzzerMode:
                    e.Value = Constants.ColorEnabled;
                    break;
                case BuzzerUnitStates.EventMode:
                    e.Value = Constants.ColorBuzzered;
                    break;
                default:
                    break;
            }
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
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.setTimerPreview();
                previewScene.Insert.SetIn();
                previewScene.Insert.SetAdditionIn();
            }
        }

        protected void setTimerPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetTimer(previewScene.Insert, this.business.RoundsCount, 1.23, 4.56, 2.34, 5.67);
            }
        }

        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setPreviewData();
        }

        void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "IOUnitNameList") this.fillIOUnitList();
            }
        }

        #endregion

        #region Events.Controls

        protected virtual void numericUpDownTimerPositionX_ValueChanged(object sender, EventArgs e) {
            this.business.TimerPositionX = (int)this.numericUpDownTimerPositionX.Value;
            this.setTimerPreview();
        }
        protected virtual void numericUpDownTimerPositionY_ValueChanged(object sender, EventArgs e) {
            this.business.TimerPositionY = (int)this.numericUpDownTimerPositionY.Value;
            this.setTimerPreview();
        }

        private void numericUpDownLeftPlayerStopoverChannel_ValueChanged(object sender, EventArgs e) { this.business.LeftStopoverChannel = (int)this.numericUpDownLeftStopoverChannel.Value; }
        private void numericUpDownRightPlayerStopoverChannel_ValueChanged(object sender, EventArgs e) { this.business.RightStopoverChannel = (int)this.numericUpDownRightStopoverChannel.Value; }

        private void numericUpDownLeftPlayerDMXStartchannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPlayerDMXStartchannel = (int)this.numericUpDownLeftPlayerDMXStartchannel.Value; }
        private void buttonLeftPlayerOn_Click(object sender, EventArgs e) { this.business.SetLeftPlayerOn(); }
        private void buttonLeftPlayerOff_Click(object sender, EventArgs e) { this.business.SetLeftPlayerOff(); }

        private void numericUpDownRightPlayerDMXStartchannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPlayerDMXStartchannel = (int)this.numericUpDownRightPlayerDMXStartchannel.Value; }
        private void buttonRightPlayerOn_Click(object sender, EventArgs e) { this.business.SetRightPlayerOn(); }
        private void buttonRightPlayerOff_Click(object sender, EventArgs e) { this.business.SetRightPlayerOff(); }

        private void buttonBlack_Click(object sender, EventArgs e) { this.business.AllLightsBlack(); }

        private void numericUpDownRoundsCount_ValueChanged(object sender, EventArgs e) { this.business.RoundsCount = (int)this.numericUpDownRoundsCount.Value; }

        #endregion

    }

}
