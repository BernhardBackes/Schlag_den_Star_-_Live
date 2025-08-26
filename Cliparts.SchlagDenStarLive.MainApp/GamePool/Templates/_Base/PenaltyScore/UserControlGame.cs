using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.PenaltyScore {

    public partial class UserControlGame : _Base.UserControlGame {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownLeftPlayerCarryover.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerCarryover.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerCarryover.Minimum = int.MinValue;
            this.numericUpDownRightPlayerCarryover.Maximum = int.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            Binding bind;

            this.buildStepList();

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            bind = new Binding("Text", this.business, "LeftPlayerScore");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxLeftPlayerScore.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftPlayerPenaltySum");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxLeftPlayerSum.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerCarryover");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerCarryover.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerScore");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxRightPlayerScore.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerPenaltySum");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxRightPlayerSum.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerCarryover");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerCarryover.DataBindings.Add(bind);

            for (int i = 0; i < Business.PenaltyCountMax; i++) {
                UserControlGamePoolTemplatesPenaltyScoreSingleDot userControlPenaltyDotLeftPlayer;
                UserControlGamePoolTemplatesPenaltyScoreSingleDot userControlPenaltyDotRightPlayer;
                Label labelCounter;
                int left;
                if (i == 0) {
                    userControlPenaltyDotLeftPlayer = this.userControlPenaltyDotLeftPlayer_00;
                    userControlPenaltyDotRightPlayer = this.userControlPenaltyDotRightPlayer_00;
                }
                else {
                    left = this.userControlPenaltyDotLeftPlayer_00.Left + this.userControlPenaltyDotLeftPlayer_00.Width * i;
                    userControlPenaltyDotLeftPlayer = new UserControlGamePoolTemplatesPenaltyScoreSingleDot();
                    userControlPenaltyDotLeftPlayer.Left = left;
                    userControlPenaltyDotLeftPlayer.Name = "userControlPenaltyDotLeftPlayer_" + i.ToString("00");
                    userControlPenaltyDotLeftPlayer.Top = this.userControlPenaltyDotLeftPlayer_00.Top;
                    this.Controls.Add(userControlPenaltyDotLeftPlayer);
                    userControlPenaltyDotRightPlayer = new UserControlGamePoolTemplatesPenaltyScoreSingleDot();
                    userControlPenaltyDotRightPlayer.Left = left;
                    userControlPenaltyDotRightPlayer.Name = "userControlPenaltyDotRightPlayer_" + i.ToString("00");
                    userControlPenaltyDotRightPlayer.Top = this.userControlPenaltyDotRightPlayer_00.Top;
                    this.Controls.Add(userControlPenaltyDotRightPlayer);
                    labelCounter = new Label();
                    labelCounter.BackColor = this.BackColor;
                    labelCounter.Font = this.labelCounter_00.Font;
                    labelCounter.Left = left;
                    labelCounter.Name = "labelCounter_" + i.ToString("00");
                    labelCounter.Size = this.labelCounter_00.Size;
                    labelCounter.TextAlign = this.labelCounter_00.TextAlign;
                    labelCounter.Text = ((int)i + 1).ToString();
                    labelCounter.Top = this.labelCounter_00.Top;
                    this.Controls.Add(labelCounter);
                }
                userControlPenaltyDotLeftPlayer.Pose(this.business.LeftPlayerPenaltyDots[i]);
                userControlPenaltyDotRightPlayer.Pose(this.business.RightPlayerPenaltyDots[i]);
            }

            this.adjustUserControlDots();

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

            this.textBoxLeftPlayerScore.DataBindings.Clear();
            this.textBoxLeftPlayerSum.DataBindings.Clear();
            this.numericUpDownLeftPlayerCarryover.DataBindings.Clear();

            this.textBoxRightPlayerScore.DataBindings.Clear();
            this.textBoxRightPlayerSum.DataBindings.Clear();
            this.numericUpDownRightPlayerCarryover.DataBindings.Clear();

            UserControlGamePoolTemplatesPenaltyScoreSingleDot control;
            string key;
            for (int i = 0; i < Business.PenaltyCountMax; i++) {
                key = "userControlPenaltyDotLeftPlayer_" + i.ToString("00");
                control = this.Controls[key] as UserControlGamePoolTemplatesPenaltyScoreSingleDot;
                if (control is UserControlGamePoolTemplatesPenaltyScoreSingleDot) {
                    control.Dispose();
                }
                key = "userControlPenaltyDotRightPlayer_" + i.ToString("00");
                control = this.Controls[key] as UserControlGamePoolTemplatesPenaltyScoreSingleDot;
                if (control is UserControlGamePoolTemplatesPenaltyScoreSingleDot) {
                    control.Dispose();
                }
            }
        }

        private void adjustUserControlDots() {
            Control control;
            string key;
            for (int i = 0; i < Business.PenaltyCountMax; i++) {
                key = "userControlPenaltyDotLeftPlayer_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.Visible = i < this.business.PenaltyDotsCount;
                key = "userControlPenaltyDotRightPlayer_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.Visible = i < this.business.PenaltyDotsCount;
                key = "labelCounter_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.Visible = i < this.business.PenaltyDotsCount;
            }
        }

        #endregion


        #region Events.Incoming

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.business_PropertyChanged(sender, e);
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "PenaltyDotsCount") this.adjustUserControlDots();
            }
        }

        #endregion

        #region Events.Controls

        private void UserControlGame_BackColorChanged(object sender, EventArgs e) {
            Control control;
            string key;
            for (int i = 0; i < Business.PenaltyCountMax; i++) {
                key = "userControlPenaltyDotLeftPlayer_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.BackColor = this.BackColor;
                key = "userControlPenaltyDotRightPlayer_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.BackColor = this.BackColor;
                key = "labelCounter_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.BackColor = this.BackColor;
            }
        }

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }

        private void numericUpDownLeftPlayerCarryover_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPlayerCarryover = (int)this.numericUpDownLeftPlayerCarryover.Value; }
        private void numericUpDownRightPlayerCarryover_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPlayerCarryover = (int)this.numericUpDownRightPlayerCarryover.Value; }

        private void buttonVinsert_PenaltyIn_Click(object sender, EventArgs e) { this.business.Vinsert_PenaltyIn(); }

        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion

    }

}
