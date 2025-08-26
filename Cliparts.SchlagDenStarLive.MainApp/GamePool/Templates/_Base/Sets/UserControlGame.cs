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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.Sets {

    public partial class UserControlGame : _Base.UserControlGame {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownLeftPlayerScoreOffset.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerScoreOffset.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerScoreOffset.Minimum = int.MinValue;
            this.numericUpDownRightPlayerScoreOffset.Maximum = int.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            Binding bind;

            this.buildStepList();

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            bind = new Binding("Text", this.business, "IOUnitName");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxIOUnitName.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitStatus");
            bind.Format += this.bind_textBoxIOUnitName_BackColor;
            this.textBoxIOUnitName.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftPlayerSetSum");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.textBoxLeftPlayerSum.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerScoreOffset");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerScoreOffset.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerSetSum");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.textBoxRightPlayerSum.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerScoreOffset");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerScoreOffset.DataBindings.Add(bind);

            for (int i = 0; i < Business.SetsCountMax; i++) {
                UserControlGamePoolTemplatesSetsSingleSet userControlSetLeftPlayer;
                UserControlGamePoolTemplatesSetsSingleSet userControlSetRightPlayer;
                Button buttonCounter;
                int left;
                if (i == 0) {
                    userControlSetLeftPlayer = this.userControlSetLeftPlayer_00;
                    userControlSetRightPlayer = this.userControlSetRightPlayer_00;
                }
                else {
                    left = this.userControlSetLeftPlayer_00.Left + this.userControlSetLeftPlayer_00.Width * i;
                    userControlSetLeftPlayer = new UserControlGamePoolTemplatesSetsSingleSet();
                    userControlSetLeftPlayer.Left = left;
                    userControlSetLeftPlayer.Name = "userControlSetLeftPlayer_" + i.ToString("00");
                    userControlSetLeftPlayer.Top = this.userControlSetLeftPlayer_00.Top;
                    this.Controls.Add(userControlSetLeftPlayer);
                    userControlSetRightPlayer = new UserControlGamePoolTemplatesSetsSingleSet();
                    userControlSetRightPlayer.Left = left;
                    userControlSetRightPlayer.Name = "userControlSetRightPlayer_" + i.ToString("00");
                    userControlSetRightPlayer.Top = this.userControlSetRightPlayer_00.Top;
                    this.Controls.Add(userControlSetRightPlayer);
                    buttonCounter = new Button();
                    buttonCounter.ForeColor = this.buttonCounter_00.ForeColor;
                    buttonCounter.Font = this.buttonCounter_00.Font;
                    buttonCounter.Left = left;
                    buttonCounter.Name = "buttonCounter_" + i.ToString("00");
                    buttonCounter.Size = this.buttonCounter_00.Size;
                    buttonCounter.TextAlign = this.buttonCounter_00.TextAlign;
                    buttonCounter.Text = ((int)i + 1).ToString();
                    buttonCounter.Top = this.buttonCounter_00.Top;
                    buttonCounter.Click += this.buttonCounter_Click;
                    this.Controls.Add(buttonCounter);
                }
                userControlSetLeftPlayer.EnterNumericUoDown += this.control_Enter;
                userControlSetLeftPlayer.LeaveNumericUoDown += this.control_Leave;
                userControlSetLeftPlayer.Pose(this.business.LeftPlayerSets[i]);
                userControlSetRightPlayer.EnterNumericUoDown += this.control_Enter;
                userControlSetRightPlayer.LeaveNumericUoDown += this.control_Leave;
                userControlSetRightPlayer.Pose(this.business.RightPlayerSets[i]);
            }

            this.setSelectedPlayer();

            this.adjustUserControlSet();

            this.setSelectedSet();

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

            this.textBoxIOUnitName.DataBindings.Clear();

            this.textBoxLeftPlayerSum.DataBindings.Clear();
            this.numericUpDownLeftPlayerScoreOffset.DataBindings.Clear();
            this.textBoxRightPlayerSum.DataBindings.Clear();
            this.numericUpDownRightPlayerScoreOffset.DataBindings.Clear();

            UserControlGamePoolTemplatesSetsSingleSet control;
            string key;
            for (int i = 0; i < Business.SetsCountMax; i++) {
                key = "userControlSetLeftPlayer_" + i.ToString("00");
                control = this.Controls[key] as UserControlGamePoolTemplatesSetsSingleSet;
                if (control is UserControlGamePoolTemplatesSetsSingleSet) {
                    control.Dispose();
                    control.EnterNumericUoDown -= this.control_Enter;
                    control.LeaveNumericUoDown -= this.control_Leave;
                }
                key = "userControlSetRightPlayer_" + i.ToString("00");
                control = this.Controls[key] as UserControlGamePoolTemplatesSetsSingleSet;
                if (control is UserControlGamePoolTemplatesSetsSingleSet) {
                    control.Dispose();
                    control.EnterNumericUoDown -= this.control_Enter;
                    control.LeaveNumericUoDown -= this.control_Leave;
                }
            }
        }

        private void setSelectedPlayer() {
            switch (this.business.SelectedPlayer) {
                case Content.Gameboard.PlayerSelection.LeftPlayer:
                    this.buttonLeftPlayerSelected.BackColor = Constants.ColorSelected;
                    this.buttonRightPlayerSelected.UseVisualStyleBackColor = true;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.RightPlayer:
                    this.buttonLeftPlayerSelected.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerSelected.BackColor = Constants.ColorSelected;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.NotSelected:
                default:
                    this.buttonLeftPlayerSelected.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerSelected.UseVisualStyleBackColor = true;
                    break;
            }
        }

        private void bind_textBoxIOUnitName_BackColor(object sender, ConvertEventArgs e) {
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
                    e.Value = Constants.ColorBuzzered;
                    break;
                case BuzzerUnitStates.EventMode:
                    e.Value = Constants.ColorEnabled;
                    break;
                default:
                    break;
            }
        }

        private void setSelectedSet() {
            Button control;
            string key;
            for (int i = 0; i < Business.SetsCountMax; i++) {
                key = "buttonCounter_" + i.ToString("00");
                control = this.Controls[key] as Button;
                if (control is Button) {
                    if (this.business.SelectedSet == i) control.BackColor = Constants.ColorSelected;
                    else control.UseVisualStyleBackColor = true;
                }
            }
        }

        private void adjustUserControlSet() {
            Control control;
            string key;
            for (int i = 1; i < Business.SetsCountMax; i++) {
                key = "userControlSetLeftPlayer_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.Visible = i < this.business.SetsCount;
                key = "userControlSetRightPlayer_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.Visible = i < this.business.SetsCount;
                key = "buttonCounter_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.Visible = buttonCounter_00.Visible && i < this.business.SetsCount;
            }
        }

        #endregion


        #region Events.Incoming

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.business_PropertyChanged(sender, e);
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "SelectedPlayer") this.setSelectedPlayer();
                else if (e.PropertyName == "SelectedSet") this.setSelectedSet();
                else if (e.PropertyName == "SetsCount") this.adjustUserControlSet();
            }
        }

        #endregion

        #region Events.Controls

        protected virtual void UserControlGame_BackColorChanged(object sender, EventArgs e) {
            Control control;
            string key;
            for (int i = 0; i < Business.SetsCountMax; i++) {
                key = "userControlSetLeftPlayer_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.BackColor = this.BackColor;
                key = "userControlSetRightPlayer_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.BackColor = this.BackColor;
                key = "labelCounter_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.BackColor = this.BackColor;
            }
        }

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }

        private void buttonLeftPlayerSelected_Click(object sender, EventArgs e) { this.business.SelectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer; }
        private void numericUpDownLeftPlayerScoreOffset_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerScoreOffset = (int)this.numericUpDownLeftPlayerScoreOffset.Value; }
        private void buttonRightPlayerSelected_Click(object sender, EventArgs e) { this.business.SelectedPlayer = Content.Gameboard.PlayerSelection.RightPlayer; }
        private void numericUpDownRightPlayerScoreOffset_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerScoreOffset = (int)this.numericUpDownRightPlayerScoreOffset.Value; }

        private void buttonCounter_Click(object sender, EventArgs e) {
            int index;
            if (Helper.tryParseIndexFromControl(sender as Control, out index)) this.business.SelectedSet = index;
        }

        protected void buttonReleaseBuzzer_Click(object sender, EventArgs e) {this.business.ReleaseBuzzer(); }
        protected void buttonLockBuzzer_Click(object sender, EventArgs e) { this.business.LockBuzzer(); }
        protected void buttonDoBuzzer_Click(object sender, EventArgs e) { this.business.DoBuzzer(); }

        private void buttonVinsert_SetsIn_Click(object sender, EventArgs e) { this.business.Vinsert_SetsIn(); }

        #endregion


    }

}
