using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.DecimalAddition {

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

            bind = new Binding("Text", this.business, "LeftPlayerSum");
            bind.Format += (s, e) => { e.Value = ((float)e.Value).ToString("0.00"); };
            this.textBoxLeftPlayerSum.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerSum");
            bind.Format += (s, e) => { e.Value = ((float)e.Value).ToString("0.00"); };
            this.textBoxRightPlayerSum.DataBindings.Add(bind);

            for (int i = 0; i < Business.DecimalAdditionCountMax; i++) {
                UserControlGamePoolTemplatesDecimalAdditionSingleSet userControlSetLeftPlayer;
                UserControlGamePoolTemplatesDecimalAdditionSingleSet userControlSetRightPlayer;
                Label labelCounter;
                int left;
                if (i == 0) {
                    userControlSetLeftPlayer = this.userControlSetLeftPlayer_00;
                    userControlSetRightPlayer = this.userControlSetRightPlayer_00;
                }
                else {
                    left = this.userControlSetLeftPlayer_00.Left + this.userControlSetLeftPlayer_00.Width * i;
                    userControlSetLeftPlayer = new UserControlGamePoolTemplatesDecimalAdditionSingleSet();
                    userControlSetLeftPlayer.Left = left;
                    userControlSetLeftPlayer.Name = "userControlSetLeftPlayer_" + i.ToString("00");
                    userControlSetLeftPlayer.Top = this.userControlSetLeftPlayer_00.Top;
                    this.Controls.Add(userControlSetLeftPlayer);
                    userControlSetRightPlayer = new UserControlGamePoolTemplatesDecimalAdditionSingleSet();
                    userControlSetRightPlayer.Left = left;
                    userControlSetRightPlayer.Name = "userControlSetRightPlayer_" + i.ToString("00");
                    userControlSetRightPlayer.Top = this.userControlSetRightPlayer_00.Top;
                    this.Controls.Add(userControlSetRightPlayer);
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
                userControlSetLeftPlayer.Enter += this.control_Enter;
                userControlSetLeftPlayer.Leave += this.control_Leave;
                userControlSetLeftPlayer.Pose(this.business.LeftPlayerDecimalAddition[i]);
                userControlSetRightPlayer.Enter += this.control_Enter;
                userControlSetRightPlayer.Leave += this.control_Leave;
                userControlSetRightPlayer.Pose(this.business.RightPlayerDecimalAddition[i]);
            }

            this.adjustUserControlSet();

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

            this.textBoxLeftPlayerSum.DataBindings.Clear();
            this.textBoxRightPlayerSum.DataBindings.Clear();

            UserControlGamePoolTemplatesDecimalAdditionSingleSet control;
            string key;
            for (int i = 0; i < Business.DecimalAdditionCountMax; i++) {
                key = "userControlSetLeftPlayer_" + i.ToString("00");
                control = this.Controls[key] as UserControlGamePoolTemplatesDecimalAdditionSingleSet;
                if (control is UserControlGamePoolTemplatesDecimalAdditionSingleSet) {
                    control.Dispose();
                    control.Enter -= this.control_Enter;
                    control.Leave -= this.control_Leave;
                }
                key = "userControlSetRightPlayer_" + i.ToString("00");
                control = this.Controls[key] as UserControlGamePoolTemplatesDecimalAdditionSingleSet;
                if (control is UserControlGamePoolTemplatesDecimalAdditionSingleSet) {
                    control.Dispose();
                    control.Enter -= this.control_Enter;
                    control.Leave -= this.control_Leave;
                }
            }
        }

        private void adjustUserControlSet() {
            Control control;
            string key;
            for (int i = 0; i < Business.DecimalAdditionCountMax; i++) {
                key = "userControlSetLeftPlayer_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.Visible = i < this.business.DecimalAdditionCount;
                key = "userControlSetRightPlayer_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.Visible = i < this.business.DecimalAdditionCount;
                key = "labelCounter_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.Visible = i < this.business.DecimalAdditionCount;
            }
        }

        #endregion


        #region Events.Incoming

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.business_PropertyChanged(sender, e);
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "DecimalAdditionCount") this.adjustUserControlSet();
            }
        }

        #endregion

        #region Events.Controls

        private void UserControlGame_BackColorChanged(object sender, EventArgs e) {
            Control control;
            string key;
            for (int i = 0; i < Business.DecimalAdditionCountMax; i++) {
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

        private void buttonVinsert_DecimalAdditionIn_Click(object sender, EventArgs e) { this.business.Vinsert_DecimalAdditionIn(); }

        #endregion

    }

}
