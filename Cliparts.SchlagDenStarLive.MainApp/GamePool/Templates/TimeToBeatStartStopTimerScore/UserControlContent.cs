using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimeToBeatStartStopTimerScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimeToBeatStartStopTimerScore {

    public partial class UserControlContent : _Base.Score.UserControlContent {

        #region Properties

        private Business business;

        private Preview.Sources _previewSource = Preview.Sources.Insert;
        protected Preview.Sources previewSource {
            get { return this._previewSource; }
            set {
                if (this._previewSource != value) {
                    this._previewSource = value;
                    this.setPreviewSource(value);
                }
            }
        }

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownTimerStartTime.Minimum = int.MinValue;
            this.numericUpDownTimerStartTime.Maximum = int.MaxValue;

            this.numericUpDownTimerStopTime.Minimum = int.MinValue;
            this.numericUpDownTimerStopTime.Maximum = int.MaxValue;

            this.numericUpDownTimerAlarmTime1.Minimum = int.MinValue;
            this.numericUpDownTimerAlarmTime1.Maximum = int.MaxValue;

            this.numericUpDownTimerAlarmTime2.Minimum = int.MinValue;
            this.numericUpDownTimerAlarmTime2.Maximum = int.MaxValue;

            this.numericUpDownBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownBuzzerChannel.Maximum = int.MaxValue;

            this.comboBoxContactMode.Items.AddRange(Enum.GetNames(typeof(ContactModes)));
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "TimerStartTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerStartTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerStartTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerStartTimeText.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerStopTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerStopTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerStopTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerStopTimeText.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerAlarmTime1");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerAlarmTime1.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerAlarmTime1");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerAlarmTime1Text.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerAlarmTime2");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerAlarmTime2.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerAlarmTime2");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerAlarmTime2Text.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "IOUnitName");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.comboBoxIOUnit.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitStatus");
            bind.Format += this.bind_comboBoxIOUnitName_BackColor;
            this.comboBoxIOUnit.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "BuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownBuzzerChannel.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "ContactMode");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxContactMode.DataBindings.Add(bind);

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

            if (this.business is Business) this.business.PropertyChanged -= this.business_PropertyChanged;

            this.numericUpDownTimerStartTime.DataBindings.Clear();
            this.labelTimerStartTimeText.DataBindings.Clear();
            this.numericUpDownTimerStopTime.DataBindings.Clear();
            this.labelTimerStopTimeText.DataBindings.Clear();
            this.numericUpDownTimerAlarmTime1.DataBindings.Clear();
            this.labelTimerAlarmTime1Text.DataBindings.Clear();
            this.numericUpDownTimerAlarmTime2.DataBindings.Clear();
            this.labelTimerAlarmTime2Text.DataBindings.Clear();

            this.comboBoxIOUnit.DataBindings.Clear();
            this.numericUpDownBuzzerChannel.DataBindings.Clear();

            this.comboBoxContactMode.DataBindings.Clear();
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

        protected void setPreviewSource(
            Preview.Sources source) {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                previewScene.SetSource(source);
                this.setPreviewData();
            }
        }

        private void setPreviewData() {
            switch (this.previewSource) {
                case Preview.Sources.Insert:
                    this.setInsertPreview();
                    this.radioButtonSourceInsert.Checked = true;
                    break;
                case Preview.Sources.Fullscreen:
                    this.setFullscreenPreview();
                    this.radioButtonSourceFullscreen.Checked = true;
                    break;
            }
        }

        protected void setInsertPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetScore(previewScene.Insert.Score, 2, 3);
                previewScene.Insert.Score.SetIn();
            }
        }


        protected void setFullscreenPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vfullscreen_SetTimer(previewScene.Fullscreen, this.business.TimerStartTime);
                this.business.Vfullscreen_SetTimeToBeat(previewScene.Fullscreen, "56.23");
                previewScene.Fullscreen.SetTimeToBeatIn();
            }
        }

        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setPreviewSource(this.previewSource);
        }

        void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "IOUnitNameList") this.fillIOUnitList();
                else if (e.PropertyName == "TimerStartTime") this.setFullscreenPreview();
            }
        }

        #endregion

        #region Events.Controls

        protected virtual void numericUpDownTimerStartTime_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.TimerStartTime = (int)this.numericUpDownTimerStartTime.Value;
                this.setFullscreenPreview();
            }
        }
        protected virtual void numericUpDownTimerStopTime_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.TimerStopTime = (int)this.numericUpDownTimerStopTime.Value; }
        protected virtual void numericUpDownTimerAlarmTime1_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.TimerAlarmTime1 = (int)this.numericUpDownTimerAlarmTime1.Value; }
        protected virtual void numericUpDownTimerAlarmTime2_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.TimerAlarmTime2 = (int)this.numericUpDownTimerAlarmTime2.Value; }

        protected virtual void comboBoxIOUnit_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.IOUnitName = this.comboBoxIOUnit.Text; }
        protected virtual void numericUpDownBuzzerChannel_ValueChanged(object sender, EventArgs e) { this.business.BuzzerChannel = (int)this.numericUpDownBuzzerChannel.Value; }

        private void radioButtonSourceInsert_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceInsert.Checked) this.previewSource = Preview.Sources.Insert; }
        private void radioButtonSourceFullscreen_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceFullscreen.Checked) this.previewSource = Preview.Sources.Fullscreen; }

        private void comboBoxContactMode_SelectedIndexChanged(object sender, EventArgs e) {
            ContactModes result;
            if (Enum.TryParse(this.comboBoxContactMode.Text, out result)) this.business.ContactMode = result;
        }

        #endregion

    }

}
