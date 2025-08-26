using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.Tools.Base;

using Cliparts.SchlagDenStarLive.MainApp.AMB;
using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimeToBeatAddition;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimeToBeatAddition {

    public partial class UserControlContent : _Base.UserControlContent {

        #region Properties

        private Business business;

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
            this.comboBoxTimeToBeatStyle.Items.AddRange(Enum.GetNames(typeof(Game.StyleElements)));
            this.comboBoxTimeToBeatStyle.EndUpdate();

            this.numericUpDownTimeToBeatStopTime.Minimum = int.MinValue;
            this.numericUpDownTimeToBeatStopTime.Maximum = int.MaxValue;

            this.numericUpDownLeftBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownLeftBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownRightBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownRightBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownStopBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownStopBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownLeftBuzzerDMXStartchannel.Minimum = int.MinValue;
            this.numericUpDownLeftBuzzerDMXStartchannel.Maximum = int.MaxValue;

            this.numericUpDownRightBuzzerDMXStartchannel.Minimum = int.MinValue;
            this.numericUpDownRightBuzzerDMXStartchannel.Maximum = int.MaxValue;

            this.numericUpDownSentenceTime.Minimum = int.MinValue;
            this.numericUpDownSentenceTime.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(previewPipe);

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

            bind = new Binding("Text", this.business, "IOUnitName");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.comboBoxIOUnit.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitStatus");
            bind.Format += this.bind_comboBoxIOUnitName_BackColor;
            this.comboBoxIOUnit.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftBuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftBuzzerChannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightBuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightBuzzerChannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "StopBuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownStopBuzzerChannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftBuzzerDMXStartchannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftBuzzerDMXStartchannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightBuzzerDMXStartchannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightBuzzerDMXStartchannel.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "LeftTeamOffColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelLeftTeamOffColor.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "LeftTeamOnColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelLeftTeamOffColor.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "LeftTeamOnColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelLeftTeamOnColor.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "LeftTeamOffColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelLeftTeamOnColor.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "RightTeamOffColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelRightTeamOffColor.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "RightTeamOnColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelRightTeamOffColor.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "RightTeamOnColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelRightTeamOnColor.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "RightTeamOffColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelRightTeamOnColor.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "SentenceTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownSentenceTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "SentenceTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelSentenceTimeText.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "BuzzerSound");
            bind.Format += (s, e) => { e.Value = Convert.ToBoolean(e.Value); };
            this.checkBoxBuzzerSound.DataBindings.Add(bind);

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

            this.numericUpDownTimeToBeatPositionX.DataBindings.Clear();
            this.numericUpDownTimeToBeatPositionY.DataBindings.Clear();
            this.comboBoxTimeToBeatStyle.DataBindings.Clear();

            this.comboBoxIOUnit.DataBindings.Clear();
            this.numericUpDownLeftBuzzerChannel.DataBindings.Clear();
            this.numericUpDownRightBuzzerChannel.DataBindings.Clear();
            this.numericUpDownStopBuzzerChannel.DataBindings.Clear();

            this.numericUpDownLeftBuzzerDMXStartchannel.DataBindings.Clear();
            this.numericUpDownRightBuzzerDMXStartchannel.DataBindings.Clear();
            this.labelLeftTeamOffColor.DataBindings.Clear();
            this.labelLeftTeamOnColor.DataBindings.Clear();
            this.labelRightTeamOffColor.DataBindings.Clear();
            this.labelRightTeamOnColor.DataBindings.Clear();

            this.numericUpDownTimeToBeatStopTime.DataBindings.Clear();
            this.labelTimeToBeatStopTimeText.DataBindings.Clear();

            this.checkBoxBuzzerSound.DataBindings.Clear();
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

        protected void setTimeToBeatPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetTimeToBeat(previewScene.Insert.Game, Content.Gameboard.PlayerSelection.LeftPlayer);
                previewScene.Insert.Game.SetIn();
            }
        }

        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setTimeToBeatPreview();
        }


        void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "IOUnitNameList") this.fillIOUnitList();
            }
        }

        #endregion

        #region Events.Controls

        protected virtual void numericUpDownTimeToBeatPositionX_ValueChanged(object sender, EventArgs e) { this.business.TimeToBeatPositionX = (int)this.numericUpDownTimeToBeatPositionX.Value; }
        protected virtual void numericUpDownTimeToBeatPositionY_ValueChanged(object sender, EventArgs e) { this.business.TimeToBeatPositionY = (int)this.numericUpDownTimeToBeatPositionY.Value; }
        protected virtual void comboBoxTimeToBeatStyle_SelectedIndexChanged(object sender, EventArgs e) {
            Game.StyleElements style;
            if (Enum.TryParse(this.comboBoxTimeToBeatStyle.Text, out style)) this.business.TimeToBeatStyle = style;
        }
        protected virtual void numericUpDownTimeToBeatStopTime_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.TimeToBeatStopTime = (int)this.numericUpDownTimeToBeatStopTime.Value; }

        protected virtual void comboBoxIOUnit_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.IOUnitName = this.comboBoxIOUnit.Text; }
        protected virtual void numericUpDownLeftBuzzerChannel_ValueChanged(object sender, EventArgs e) { this.business.LeftBuzzerChannel = (int)this.numericUpDownLeftBuzzerChannel.Value; }
        protected virtual void numericUpDownRightBuzzerChannel_ValueChanged(object sender, EventArgs e) { this.business.RightBuzzerChannel = (int)this.numericUpDownRightBuzzerChannel.Value; }
        private void numericUpDownStopBuzzerChannel_ValueChanged(object sender, EventArgs e) { this.business.StopBuzzerChannel = (int)this.numericUpDownStopBuzzerChannel.Value; }

        private void numericUpDownLeftBuzzerDMXStartchannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftBuzzerDMXStartchannel = (int)this.numericUpDownLeftBuzzerDMXStartchannel.Value; }

        private void numericUpDownRightBuzzerDMXStartchannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightBuzzerDMXStartchannel = (int)this.numericUpDownRightBuzzerDMXStartchannel.Value; }

        private void labelLeftTeamOffColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.LeftTeamOffColor;
            switch (dialog.ShowDialog())
            {
                case DialogResult.OK:
                    this.business.LeftTeamOffColor = dialog.Color;
                    break;
            }
        }
        private void labelLeftTeamOnColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.LeftTeamOnColor;
            switch (dialog.ShowDialog())
            {
                case DialogResult.OK:
                    this.business.LeftTeamOnColor = dialog.Color;
                    break;
            }
        }
        private void buttonLeftBuzzerOnLeftTeam_Click(object sender, EventArgs e) { this.business.SetLeftBuzzerOn(Content.Gameboard.PlayerSelection.LeftPlayer); }
        private void buttonLeftBuzzerOffLeftTeam_Click(object sender, EventArgs e) { this.business.SetLeftBuzzerOff(Content.Gameboard.PlayerSelection.LeftPlayer); }
        private void buttonRightBuzzerOnLeftTeam_Click(object sender, EventArgs e) { this.business.SetRightBuzzerOn(Content.Gameboard.PlayerSelection.LeftPlayer); }
        private void buttonRightBuzzerOffLeftTeam_Click(object sender, EventArgs e) { this.business.SetRightBuzzerOff(Content.Gameboard.PlayerSelection.LeftPlayer); }

        private void labelRightTeamOffColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.RightTeamOffColor;
            switch (dialog.ShowDialog())
            {
                case DialogResult.OK:
                    this.business.RightTeamOffColor = dialog.Color;
                    break;
            }
        }
        private void labelRightTeamOnColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.RightTeamOnColor;
            switch (dialog.ShowDialog())
            {
                case DialogResult.OK:
                    this.business.RightTeamOnColor = dialog.Color;
                    break;
            }
        }
        private void buttonLeftBuzzerOnRightTeam_Click(object sender, EventArgs e) { this.business.SetLeftBuzzerOn(Content.Gameboard.PlayerSelection.RightPlayer); }
        private void buttonLeftBuzzerOffRightTeam_Click(object sender, EventArgs e) { this.business.SetLeftBuzzerOff(Content.Gameboard.PlayerSelection.RightPlayer); }
        private void buttonRightBuzzerOnRightTeam_Click(object sender, EventArgs e) { this.business.SetRightBuzzerOn(Content.Gameboard.PlayerSelection.RightPlayer); }
        private void buttonRightBuzzerOffRightTeam_Click(object sender, EventArgs e) { this.business.SetRightBuzzerOff(Content.Gameboard.PlayerSelection.RightPlayer); }

        private void buttonBlack_Click(object sender, EventArgs e) { this.business.AllLightsBlack(); }

        private void numericUpDownSentenceTime_ValueChanged(object sender, EventArgs e) { this.business.SentenceTime = (int)this.numericUpDownSentenceTime.Value; }

        private void checkBoxBuzzerSound_CheckedChanged(object sender, EventArgs e) { this.business.BuzzerSound = this.checkBoxBuzzerSound.Checked; }

        #endregion
    }
}
