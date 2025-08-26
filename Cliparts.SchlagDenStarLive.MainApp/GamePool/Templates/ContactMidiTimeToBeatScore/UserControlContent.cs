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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.ContactMidiTimeToBeatScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.ContactMidiTimeToBeatScore {

    public partial class UserControlContent : _Base.BuzzerScore.UserControlContent {

        #region Properties

        private Business business;

        private bool _showTimeToBeat = false;
        public bool showTimeToBeat {
            get { return this._showTimeToBeat; }
            set {
                if (this._showTimeToBeat != value) {
                    this._showTimeToBeat = value;
                    this.setPreviewData();
                }
            }
        }

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownTimeToBeatPositionX.Minimum = int.MinValue;
            this.numericUpDownTimeToBeatPositionX.Maximum = int.MaxValue;

            this.numericUpDownTimeToBeatPositionY.Minimum = int.MinValue;
            this.numericUpDownTimeToBeatPositionY.Maximum = int.MaxValue;

            this.comboBoxTimeToBeatStyle.BeginUpdate();
            this.comboBoxTimeToBeatStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.TimeToBeat.Styles)));
            this.comboBoxTimeToBeatStyle.EndUpdate();

            this.numericUpDownTimeToBeatStopTime.Minimum = int.MinValue;
            this.numericUpDownTimeToBeatStopTime.Maximum = int.MaxValue;

            this.numericUpDownBuzzerChannelFinishLeft.Minimum = int.MinValue;
            this.numericUpDownBuzzerChannelFinishLeft.Maximum = int.MaxValue;

            this.numericUpDownBuzzerChannelFinishRight.Minimum = int.MinValue;
            this.numericUpDownBuzzerChannelFinishRight.Maximum = int.MaxValue;

            this.numericUpDownSentenceTime.Minimum = int.MinValue;
            this.numericUpDownSentenceTime.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "TimeToBeatPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimeToBeatPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimeToBeatPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimeToBeatPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimeToBeatStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxTimeToBeatStyle.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimeToBeatStopTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimeToBeatStopTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimeToBeatStopTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimeToBeatStopTimeText.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "BuzzerChannelFinishLeft");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownBuzzerChannelFinishLeft.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "BuzzerChannelFinishRight");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownBuzzerChannelFinishRight.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "SentenceTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownSentenceTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "SentenceTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelSentenceTimeText.DataBindings.Add(bind);

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

            this.numericUpDownTimeToBeatPositionX.DataBindings.Clear();
            this.numericUpDownTimeToBeatPositionY.DataBindings.Clear();
            this.comboBoxTimeToBeatStyle.DataBindings.Clear();
            this.numericUpDownTimeToBeatStopTime.DataBindings.Clear();
            this.labelTimeToBeatStopTimeText.DataBindings.Clear();

            this.comboBoxIOUnit.DataBindings.Clear();
            this.numericUpDownBuzzerChannelFinishLeft.DataBindings.Clear();
            this.numericUpDownBuzzerChannelFinishRight.DataBindings.Clear();
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

        private void setPreviewData() {
            if (this.showTimeToBeat) this.radioButtonSourceTimeToBeat.Checked = true;
            else this.radioButtonSourceScore.Checked = true;
            this.setTimeToBeatPreview();
            this.setScorePreview();
        }

        protected void setTimeToBeatPreview() {
            if (this.previewSceneIsAvailable) {
                if (this.showTimeToBeat) {
                    ((Preview)this.previewScene).Insert.TimeToBeat.SetPositionX(this.business.TimeToBeatPositionX);
                    ((Preview)this.previewScene).Insert.TimeToBeat.SetPositionY(this.business.TimeToBeatPositionY);
                    ((Preview)this.previewScene).Insert.TimeToBeat.SetStyle(this.business.TimeToBeatStyle);
                    ((Preview)this.previewScene).Insert.TimeToBeat.SetName(this.business.LeftPlayerName);
                    ((Preview)this.previewScene).Insert.TimeToBeat.SetCounter(5);
                    ((Preview)this.previewScene).Insert.TimeToBeat.SetOffset(1.23f);
                    ((Preview)this.previewScene).Insert.TimeToBeat.SetTimeToBeat(12.34f);
                    ((Preview)this.previewScene).Insert.TimeToBeat.ShowOffset();
                    ((Preview)this.previewScene).Insert.TimeToBeat.ShowTimeToBeat();
                    ((Preview)this.previewScene).Insert.TimeToBeat.SetIn();
                }
                else ((Preview)this.previewScene).Insert.TimeToBeat.SetOut();
            }
        }

        protected override void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                if (!this.showTimeToBeat) {
                    ((Preview)this.previewScene).Insert.Score.SetPositionX(this.business.ScorePositionX);
                    ((Preview)this.previewScene).Insert.Score.SetPositionY(this.business.ScorePositionY);
                    ((Preview)this.previewScene).Insert.Score.SetStyle(this.business.ScoreStyle);
                    ((Preview)this.previewScene).Insert.Score.SetLeftTopName(this.business.LeftPlayerName);
                    ((Preview)this.previewScene).Insert.Score.SetLeftTopScore(2);
                    ((Preview)this.previewScene).Insert.Score.SetRightBottomName(this.business.RightPlayerName);
                    ((Preview)this.previewScene).Insert.Score.SetRightBottomScore(3);
                    ((Preview)this.previewScene).Insert.Score.SetIn();
                }
                else ((Preview)this.previewScene).Insert.Score.SetOut();
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

        protected virtual void numericUpDownTimeToBeatPositionX_ValueChanged(object sender, EventArgs e) { 
            this.business.TimeToBeatPositionX = (int)this.numericUpDownTimeToBeatPositionX.Value;
            this.setTimeToBeatPreview();
        }
        protected virtual void numericUpDownTimeToBeatPositionY_ValueChanged(object sender, EventArgs e) { 
            this.business.TimeToBeatPositionY = (int)this.numericUpDownTimeToBeatPositionY.Value;
            this.setTimeToBeatPreview();
        }
        protected virtual void comboBoxTimeToBeatStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.TimeToBeat.Styles style;
            if (Enum.TryParse(this.comboBoxTimeToBeatStyle.Text, out style)) this.business.TimeToBeatStyle = style;
            this.setTimeToBeatPreview();
        }
        protected virtual void numericUpDownTimeToBeatStopTime_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.TimeToBeatStopTime = (int)this.numericUpDownTimeToBeatStopTime.Value; }

        protected virtual void numericUpDownSportIconsPositionX_ValueChanged(object sender, EventArgs e) {
        }
        protected virtual void numericUpDownSportIconsPositionY_ValueChanged(object sender, EventArgs e) {
        }
        private void checkBoxUseSportIcons_CheckedChanged(object sender, EventArgs e) {}

        protected virtual void numericUpDownBuzzerChannelFinishLeft_ValueChanged(object sender, EventArgs e) { this.business.BuzzerChannelFinishLeft = (int)this.numericUpDownBuzzerChannelFinishLeft.Value; }
        private void numericUpDownBuzzerChannelFinishRight_ValueChanged(object sender, EventArgs e) { this.business.BuzzerChannelFinishRight = (int)this.numericUpDownBuzzerChannelFinishRight.Value; }

        private void radioButtonSourceScore_CheckedChanged(object sender, EventArgs e) { this.showTimeToBeat = !this.radioButtonSourceScore.Checked; }
        private void radioButtonSourceTimeToBeat_CheckedChanged(object sender, EventArgs e) { this.showTimeToBeat = this.radioButtonSourceTimeToBeat.Checked; }

        private void numericUpDownSentenceTime_ValueChanged(object sender, EventArgs e) { this.business.SentenceTime = (int)this.numericUpDownSentenceTime.Value; }

        #endregion

    }

}
