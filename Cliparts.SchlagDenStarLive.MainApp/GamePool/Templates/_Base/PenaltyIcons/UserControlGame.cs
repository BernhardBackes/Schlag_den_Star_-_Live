using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.PenaltyIcons {

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

            bind = new Binding("Text", this.business, "LeftPlayerPenaltySum");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxLeftPlayerSum.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerPenaltySum");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxRightPlayerSum.DataBindings.Add(bind);

            for (int i = 0; i < Business.PenaltyCountMax; i++) {
                UserControlGamePoolTemplatesPenaltyIconsSingleDot userControlPenaltyIconsDotLeftPlayer;
                UserControlGamePoolTemplatesPenaltyIconsSingleDot userControlPenaltyIconsDotRightPlayer;
                Label labelCounter;
                int left;
                if (i == 0) {
                    userControlPenaltyIconsDotLeftPlayer = this.userControlPenaltyIconsDotLeftPlayer_00;
                    userControlPenaltyIconsDotRightPlayer = this.userControlPenaltyIconsDotRightPlayer_00;
                }
                else {
                    left = this.userControlPenaltyIconsDotLeftPlayer_00.Left + this.userControlPenaltyIconsDotLeftPlayer_00.Width * i;
                    userControlPenaltyIconsDotLeftPlayer = new UserControlGamePoolTemplatesPenaltyIconsSingleDot();
                    userControlPenaltyIconsDotLeftPlayer.Left = left;
                    userControlPenaltyIconsDotLeftPlayer.Name = "userControlPenaltyIconsDotLeftPlayer_" + i.ToString("00");
                    userControlPenaltyIconsDotLeftPlayer.Top = this.userControlPenaltyIconsDotLeftPlayer_00.Top;
                    this.Controls.Add(userControlPenaltyIconsDotLeftPlayer);
                    userControlPenaltyIconsDotRightPlayer = new UserControlGamePoolTemplatesPenaltyIconsSingleDot();
                    userControlPenaltyIconsDotRightPlayer.Left = left;
                    userControlPenaltyIconsDotRightPlayer.Name = "userControlPenaltyIconsDotRightPlayer_" + i.ToString("00");
                    userControlPenaltyIconsDotRightPlayer.Top = this.userControlPenaltyIconsDotRightPlayer_00.Top;
                    this.Controls.Add(userControlPenaltyIconsDotRightPlayer);
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
                userControlPenaltyIconsDotLeftPlayer.Pose(this.business.LeftPlayerPenaltyDots[i]);
                userControlPenaltyIconsDotRightPlayer.Pose(this.business.RightPlayerPenaltyDots[i]);
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

            UserControlGamePoolTemplatesPenaltyIconsSingleDot control;
            string key;
            for (int i = 0; i < Business.PenaltyCountMax; i++) {
                key = "userControlPenaltyIconsDotLeftPlayer_" + i.ToString("00");
                control = this.Controls[key] as UserControlGamePoolTemplatesPenaltyIconsSingleDot;
                if (control is UserControlGamePoolTemplatesPenaltyIconsSingleDot) {
                    control.Dispose();
                }
                key = "userControlPenaltyIconsDotRightPlayer_" + i.ToString("00");
                control = this.Controls[key] as UserControlGamePoolTemplatesPenaltyIconsSingleDot;
                if (control is UserControlGamePoolTemplatesPenaltyIconsSingleDot) {
                    control.Dispose();
                }
            }
        }

        private void adjustUserControlDots() {
            Control control;
            string key;
            for (int i = 0; i < Business.PenaltyCountMax; i++) {
                key = "userControlPenaltyIconsDotLeftPlayer_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.Visible = i < this.business.PenaltyDotsCount;
                key = "userControlPenaltyIconsDotRightPlayer_" + i.ToString("00");
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
                if (e.PropertyName == "PenaltyStyle") this.adjustUserControlDots();
            }
        }

        #endregion

        #region Events.Controls

        private void UserControlGame_BackColorChanged(object sender, EventArgs e) {
            Control control;
            string key;
            for (int i = 0; i < Business.PenaltyCountMax; i++) {
                key = "userControlPenaltyIconsDotLeftPlayer_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.BackColor = this.BackColor;
                key = "userControlPenaltyIconsDotRightPlayer_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.BackColor = this.BackColor;
                key = "labelCounter_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.BackColor = this.BackColor;
            }
        }

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }

        private void buttonVinsert_PenaltyIn_Click(object sender, EventArgs e) { this.business.Vinsert_PenaltyIn(); }

        #endregion

    }

}
