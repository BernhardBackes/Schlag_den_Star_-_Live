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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimerForTwo;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimerForTwo {

    public partial class UserControlContent : _Base.Score.UserControlContent {

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

            this.comboBoxTimerStyle.BeginUpdate();
            this.comboBoxTimerStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool.TimerForTwo.Insert.Styles)));
            this.comboBoxTimerStyle.EndUpdate();

            this.numericUpDownTimerLimit.Minimum = int.MinValue;
            this.numericUpDownTimerLimit.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownRightPlayerBuzzerChannel.Maximum = int.MaxValue;

        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Text", this.business, "TimelineName");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.textBoxTimelineName.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "TimelineStatus");
            bind.Format += this.bind_textBoxTimelineName_BackColor;
            this.textBoxTimelineName.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftPlayerTransponderCode");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.textBoxLeftPlayerTransponderCode.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerTransponderCode");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.textBoxRightPlayerTransponderCode.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "IOUnitName");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.comboBoxIOUnit.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitStatus");
            bind.Format += this.bind_comboBoxIOUnitName_BackColor;
            this.comboBoxIOUnit.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerBuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerBuzzerChannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerBuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerBuzzerChannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxTimerStyle.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerLimit");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerLimit.DataBindings.Add(bind);

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

            this.business.PropertyChanged -= this.business_PropertyChanged;

            this.textBoxTimelineName.DataBindings.Clear();
            this.textBoxLeftPlayerTransponderCode.DataBindings.Clear();
            this.textBoxRightPlayerTransponderCode.DataBindings.Clear();

            this.numericUpDownTimerPositionX.DataBindings.Clear();
            this.numericUpDownTimerPositionY.DataBindings.Clear();
            this.comboBoxTimerStyle.DataBindings.Clear();
            this.numericUpDownTimerLimit.DataBindings.Clear();

            this.comboBoxIOUnit.DataBindings.Clear();
            this.numericUpDownLeftPlayerBuzzerChannel.DataBindings.Clear();
            this.numericUpDownRightPlayerBuzzerChannel.DataBindings.Clear();
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
            this.comboBoxIOUnit.BeginUpdate();
            this.comboBoxIOUnit.Items.Clear();
            this.comboBoxIOUnit.Items.AddRange(this.business.IOUnitNameList);
            this.comboBoxIOUnit.EndUpdate();
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

        protected void setTimerPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetTimer(previewScene.Insert, 2, 3);                
                previewScene.Insert.SetIn();
            }
        }

        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setTimerPreview();
        }

        void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "IOUnitNameList") this.fillIOUnitList();
            }
        }

        #endregion

        #region Events.Controls

        private void textBoxTimelineName_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.TimelineName = this.textBoxTimelineName.Text; }
        private void textBoxLeftPlayerTransponderCode_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPlayerTransponderCode = this.textBoxLeftPlayerTransponderCode.Text; }
        private void textBoxRightPlayerTransponderCode_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPlayerTransponderCode = this.textBoxRightPlayerTransponderCode.Text; }

        protected virtual void numericUpDownTimerPositionX_ValueChanged(object sender, EventArgs e) { 
            this.business.TimerPositionX = (int)this.numericUpDownTimerPositionX.Value;
            this.setTimerPreview();
        }
        protected virtual void numericUpDownTimerPositionY_ValueChanged(object sender, EventArgs e) { 
            this.business.TimerPositionY = (int)this.numericUpDownTimerPositionY.Value;
            this.setTimerPreview();
        }
        protected virtual void comboBoxTimerStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool.TimerForTwo.Insert.Styles style;
            if (Enum.TryParse(this.comboBoxTimerStyle.Text, out style)) this.business.TimerStyle = style;
            this.setTimerPreview();
        }

        protected virtual void comboBoxIOUnit_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.IOUnitName = this.comboBoxIOUnit.Text; }
        protected virtual void numericUpDownLeftPlayerBuzzerChannel_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerBuzzerChannel = (int)this.numericUpDownLeftPlayerBuzzerChannel.Value; }
        protected virtual void numericUpDownRightPlayerBuzzerChannel_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerBuzzerChannel = (int)this.numericUpDownRightPlayerBuzzerChannel.Value; }

        #endregion
    }

}
