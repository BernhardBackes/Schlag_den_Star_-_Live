using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.LevelsChecked {

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

            for (int i = 0; i < Business.LevelsCheckedCountMax; i++) {
                UserControlGamePoolTemplatesLevelsCheckedSingleDot userControlLevelsCheckedDotLeftPlayer;
                UserControlGamePoolTemplatesLevelsCheckedSingleDot userControlLevelsCheckedDotRightPlayer;
                Label labelCounter;
                int left;
                if (i == 0) {
                    userControlLevelsCheckedDotLeftPlayer = this.userControlLevelsCheckedDotLeftPlayer_00;
                    userControlLevelsCheckedDotRightPlayer = this.userControlLevelsCheckedDotRightPlayer_00;
                }
                else {
                    left = this.userControlLevelsCheckedDotLeftPlayer_00.Left + this.userControlLevelsCheckedDotLeftPlayer_00.Width * i;
                    userControlLevelsCheckedDotLeftPlayer = new UserControlGamePoolTemplatesLevelsCheckedSingleDot();
                    userControlLevelsCheckedDotLeftPlayer.Left = left;
                    userControlLevelsCheckedDotLeftPlayer.Name = "userControlLevelsCheckedDotLeftPlayer_" + i.ToString("00");
                    userControlLevelsCheckedDotLeftPlayer.Top = this.userControlLevelsCheckedDotLeftPlayer_00.Top;
                    this.Controls.Add(userControlLevelsCheckedDotLeftPlayer);
                    userControlLevelsCheckedDotRightPlayer = new UserControlGamePoolTemplatesLevelsCheckedSingleDot();
                    userControlLevelsCheckedDotRightPlayer.Left = left;
                    userControlLevelsCheckedDotRightPlayer.Name = "userControlLevelsCheckedDotRightPlayer_" + i.ToString("00");
                    userControlLevelsCheckedDotRightPlayer.Top = this.userControlLevelsCheckedDotRightPlayer_00.Top;
                    this.Controls.Add(userControlLevelsCheckedDotRightPlayer);
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
                userControlLevelsCheckedDotLeftPlayer.Pose(this.business.LeftPlayerLevelsCheckedDots[i], Constants.ColorLeftPlayer);
                userControlLevelsCheckedDotRightPlayer.Pose(this.business.RightPlayerLevelsCheckedDots[i], Constants.ColorRightPlayer);
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

            UserControlGamePoolTemplatesLevelsCheckedSingleDot control;
            string key;
            for (int i = 0; i < Business.LevelsCheckedCountMax; i++) {
                key = "userControlLevelsCheckedDotLeftPlayer_" + i.ToString("00");
                control = this.Controls[key] as UserControlGamePoolTemplatesLevelsCheckedSingleDot;
                if (control is UserControlGamePoolTemplatesLevelsCheckedSingleDot) {
                    control.Dispose();
                }
                key = "userControlLevelsCheckedDotRightPlayer_" + i.ToString("00");
                control = this.Controls[key] as UserControlGamePoolTemplatesLevelsCheckedSingleDot;
                if (control is UserControlGamePoolTemplatesLevelsCheckedSingleDot) {
                    control.Dispose();
                }
            }
        }

        private void adjustUserControlDots() {
            Control control;
            string key;
            for (int i = 0; i < Business.LevelsCheckedCountMax; i++) {
                key = "userControlLevelsCheckedDotLeftPlayer_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.Visible = i < Business.LevelsCheckedCountMax;
                key = "userControlLevelsCheckedDotRightPlayer_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.Visible = i < Business.LevelsCheckedCountMax;
                key = "labelCounter_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.Visible = i < Business.LevelsCheckedCountMax;
            }
        }

        #endregion


        #region Events.Incoming

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.business_PropertyChanged(sender, e);
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "LevelsCheckedDotsCount") this.adjustUserControlDots();
            }
        }

        #endregion

        #region Events.Controls

        private void UserControlGame_BackColorChanged(object sender, EventArgs e) {
            Control control;
            string key;
            for (int i = 0; i < Business.LevelsCheckedCountMax; i++) {
                key = "userControlLevelsCheckedDotLeftPlayer_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.BackColor = this.BackColor;
                key = "userControlLevelsCheckedDotRightPlayer_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.BackColor = this.BackColor;
                key = "labelCounter_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.BackColor = this.BackColor;
            }
        }

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }

        private void buttonVinsert_LevelsCheckedIn_Click(object sender, EventArgs e) { this.business.Vinsert_LevelsCheckedIn(); }

        #endregion

    }

}