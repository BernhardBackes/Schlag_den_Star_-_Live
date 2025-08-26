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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.SetsPenalty {

    public partial class UserControlGame : _Base.UserControlGame {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            Binding bind;

            this.buildStepList();

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            bind = new Binding("Text", this.business, "LeftPlayerSetSum");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxLeftPlayerSum.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerSetSum");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxRightPlayerSum.DataBindings.Add(bind);

            for (int i = 0; i < Business.SetsCountMax; i++) {
                _Base.Sets.UserControlGamePoolTemplatesSetsSingleSet userControlSetLeftPlayer;
                _Base.Sets.UserControlGamePoolTemplatesSetsSingleSet userControlSetRightPlayer;
                Button buttonCounter;
                int left;
                if (i == 0) {
                    userControlSetLeftPlayer = this.userControlSetLeftPlayer_00;
                    userControlSetRightPlayer = this.userControlSetRightPlayer_00;
                }
                else {
                    left = this.userControlSetLeftPlayer_00.Left + this.userControlSetLeftPlayer_00.Width * i;
                    userControlSetLeftPlayer = new _Base.Sets.UserControlGamePoolTemplatesSetsSingleSet();
                    userControlSetLeftPlayer.Left = left;
                    userControlSetLeftPlayer.Name = "userControlSetLeftPlayer_" + i.ToString("00");
                    userControlSetLeftPlayer.Top = this.userControlSetLeftPlayer_00.Top;
                    this.Controls.Add(userControlSetLeftPlayer);
                    userControlSetRightPlayer = new _Base.Sets.UserControlGamePoolTemplatesSetsSingleSet();
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
                userControlSetLeftPlayer.Enter += this.control_Enter;
                userControlSetLeftPlayer.Leave += this.control_Leave;
                userControlSetLeftPlayer.Pose(this.business.LeftPlayerSets[i]);
                userControlSetRightPlayer.Enter += this.control_Enter;
                userControlSetRightPlayer.Leave += this.control_Leave;
                userControlSetRightPlayer.Pose(this.business.RightPlayerSets[i]);
            }

            for (int i = 0; i < Business.PenaltyCountMax; i++) {
                _Base.Penalty.UserControlGamePoolTemplatesPenaltySingleDot userControlPenaltyDot;
                int left;
                if (i == 0) userControlPenaltyDot = this.userControlPenaltyDot_00;
                else {
                    left = this.userControlPenaltyDot_00.Left + this.userControlPenaltyDot_00.Width * i;
                    userControlPenaltyDot = new _Base.Penalty.UserControlGamePoolTemplatesPenaltySingleDot();
                    userControlPenaltyDot.Left = left;
                    userControlPenaltyDot.Name = "userControlPenaltyDot_" + i.ToString("00");
                    userControlPenaltyDot.Top = this.userControlPenaltyDot_00.Top;
                    this.Controls.Add(userControlPenaltyDot);
                }
                userControlPenaltyDot.Pose(this.business.PenaltyDots[i]);
            }

            this.setSelectedPlayer();

            this.adjustUserControlSet();

            this.adjustUserControlShots();

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

            _Base.Sets.UserControlGamePoolTemplatesSetsSingleSet singleSet;
            string key;
            for (int i = 0; i < Business.SetsCountMax; i++) {
                key = "userControlSetLeftPlayer_" + i.ToString("00");
                singleSet = this.Controls[key] as _Base.Sets.UserControlGamePoolTemplatesSetsSingleSet;
                if (singleSet is _Base.Sets.UserControlGamePoolTemplatesSetsSingleSet) {
                    singleSet.Dispose();
                    singleSet.Enter -= this.control_Enter;
                    singleSet.Leave -= this.control_Leave;
                }
                key = "userControlSetRightPlayer_" + i.ToString("00");
                singleSet = this.Controls[key] as _Base.Sets.UserControlGamePoolTemplatesSetsSingleSet;
                if (singleSet is _Base.Sets.UserControlGamePoolTemplatesSetsSingleSet) {
                    singleSet.Dispose();
                    singleSet.Enter -= this.control_Enter;
                    singleSet.Leave -= this.control_Leave;
                }
            }

            _Base.Penalty.UserControlGamePoolTemplatesPenaltySingleDot singleDot;
            for (int i = 0; i < Business.PenaltyCountMax; i++) {
                key = "userControlPenaltyDot_" + i.ToString("00");
                singleDot = this.Controls[key] as _Base.Penalty.UserControlGamePoolTemplatesPenaltySingleDot;
                if (singleDot is _Base.Penalty.UserControlGamePoolTemplatesPenaltySingleDot) {
                    singleDot.Dispose();
                }
            }

        }

        protected override void buildStepList() {

            int index = 0;
            stepAction step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_LoadScene);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVstage_Clear);
            step.AddButton(this.buttonGame_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_SetsIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_ActivatePlayer);
            step.AddButton(this.buttonVinsert_PenaltyIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex);
            step.AddButton(this.buttonVinsert_Set);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 2);
            step.AddButton(this.buttonVinsert_PenaltyOut);
            step.AddButton(this.buttonGame_Next);
            this.stepList.Add(step);

            index++;
            this.winnerStepIndex = index;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVstage_Clear);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_UnloadScene);
            step.AddButton(this.buttonVfullscreen_ShowGameboard);
            step.AddButton(this.buttonGame_SetWinner);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_UpdateGameboard);
            this.stepList.Add(step);

            if (index < this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to short");
            if (index > this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to long");
        }

        private void setSelectedPlayer() {
            switch (this.business.SelectedPlayer) {
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.LeftPlayer:
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
            for (int i = 0; i < Business.SetsCountMax; i++) {
                key = "userControlSetLeftPlayer_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.Visible = i < this.business.SetsCount;
                key = "userControlSetRightPlayer_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.Visible = i < this.business.SetsCount;
                key = "buttonCounter_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.Visible = i < this.business.SetsCount;
            }
        }

        private void adjustUserControlShots() {
            Control control;
            string key;
            for (int i = 0; i < Business.PenaltyCountMax; i++) {
                key = "userControlPenaltyDot_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.Visible = i < this.business.PenaltyCount;
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
                else if (e.PropertyName == "PenaltyShots") this.adjustUserControlShots();
            }
        }

        #endregion

        #region Events.Controls

        private void UserControlGame_BackColorChanged(object sender, EventArgs e) {
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
            for (int i = 0; i < Business.PenaltyCountMax; i++) {
                key = "userControlPenaltyDot_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.BackColor = this.BackColor;
            }
        }

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }

        private void buttonLeftPlayerSelected_Click(object sender, EventArgs e) { this.business.SelectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer; }
        private void buttonRightPlayerSelected_Click(object sender, EventArgs e) { this.business.SelectedPlayer = Content.Gameboard.PlayerSelection.RightPlayer; }

        private void buttonCounter_Click(object sender, EventArgs e) {
            int index;
            if (Helper.tryParseIndexFromControl(sender as Control, out index)) this.business.SelectedSet = index;
        }

        private void buttonVinsert_SetsIn_Click(object sender, EventArgs e) { this.business.Vinsert_SetsIn(); }
        private void buttonVinsert_PenaltyIn_Click(object sender, EventArgs e) { this.business.Vinsert_PenaltyIn(); }
        private void buttonVinsert_Set_Click(object sender, EventArgs e) {
            this.business.Vinsert_SetPenalty();
            this.business.Vinsert_SetSets();
        }
        private void buttonVinsert_PenaltyOut_Click(object sender, EventArgs e) { this.business.Vinsert_PenaltyOut(); }

        private void buttonGame_ActivatePlayer_Click(object sender, EventArgs e) { this.business.ActivatePlayer(); }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion

    }

}
