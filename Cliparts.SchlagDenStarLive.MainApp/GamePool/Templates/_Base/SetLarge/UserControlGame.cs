using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.SetLarge {

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

            bind = new Binding("Text", this.business, "LeftPlayerSetValue");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxLeftPlayerSetValue.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerSetValue");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxRightPlayerSetValue.DataBindings.Add(bind);

            this.setLeftPlayerSetStatusButtons();
            this.setRightPlayerSetStatusButtons();

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

            this.textBoxLeftPlayerSetValue.DataBindings.Clear();
            this.textBoxRightPlayerSetValue.DataBindings.Clear();
        }

        private void setLeftPlayerSetStatusButtons() {
            switch (this.business.LeftPlayerSetStatus) {
                case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.SetLarge.ValueStates.Valid:
                    this.buttonRightPlayerSetStatusIdle.UseVisualStyleBackColor = true;
                    this.buttonLeftPlayerSetStatusValid.BackColor = Constants.ColorEnabled;
                    this.buttonLeftPlayerSetStatusInvalid.UseVisualStyleBackColor = true;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.SetLarge.ValueStates.Invalid:
                    this.buttonRightPlayerSetStatusIdle.UseVisualStyleBackColor = true;
                    this.buttonLeftPlayerSetStatusValid.UseVisualStyleBackColor = true;
                    this.buttonLeftPlayerSetStatusInvalid.BackColor = Constants.ColorDisabling;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.SetLarge.ValueStates.Idle:
                default:
                    this.buttonRightPlayerSetStatusIdle.UseVisualStyleBackColor = true;
                    this.buttonLeftPlayerSetStatusValid.UseVisualStyleBackColor = true;
                    this.buttonLeftPlayerSetStatusInvalid.UseVisualStyleBackColor = true;
                    break;
            }
        }

        private void setRightPlayerSetStatusButtons() {
            switch (this.business.RightPlayerSetStatus) {
                case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.SetLarge.ValueStates.Valid:
                    this.buttonRightPlayerSetStatusIdle.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerSetStatusValid.BackColor = Constants.ColorEnabled;
                    this.buttonRightPlayerSetStatusInvalid.UseVisualStyleBackColor = true;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.SetLarge.ValueStates.Invalid:
                    this.buttonRightPlayerSetStatusIdle.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerSetStatusValid.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerSetStatusInvalid.BackColor = Constants.ColorDisabling;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.SetLarge.ValueStates.Idle:
                default:
                    this.buttonRightPlayerSetStatusIdle.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerSetStatusValid.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerSetStatusInvalid.UseVisualStyleBackColor = true;
                    break;
            }
        }

        #endregion


        #region Events.Incoming

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.business_PropertyChanged(sender, e);
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "LeftPlayerSetStatus") this.setLeftPlayerSetStatusButtons();
                else if (e.PropertyName == "RightPlayerSetStatus") this.setRightPlayerSetStatusButtons();
            }
        }

        #endregion

        #region Events.Controls

        private void UserControlGame_BackColorChanged(object sender, EventArgs e) {
        }

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }

        private void textBoxLeftPlayerSetValue_TextChanged(object sender, EventArgs e) { this.business.LeftPlayerSetValue = this.textBoxLeftPlayerSetValue.Text; }
        private void buttonLeftPlayerSetStatusIdle_Click(object sender, EventArgs e) { this.business.LeftPlayerSetStatus = VentuzScenes.GamePool._Modules.SetLarge.ValueStates.Idle; }
        private void buttonLeftPlayerSetStatusValid_Click(object sender, EventArgs e) { this.business.LeftPlayerSetStatus = VentuzScenes.GamePool._Modules.SetLarge.ValueStates.Valid; }
        private void buttonLeftPlayerSetStatusInvalid_Click(object sender, EventArgs e) { this.business.LeftPlayerSetStatus = VentuzScenes.GamePool._Modules.SetLarge.ValueStates.Invalid; }

        private void textBoxRightPlayerSetValue_TextChanged(object sender, EventArgs e) { this.business.RightPlayerSetValue = this.textBoxRightPlayerSetValue.Text; }
        private void buttonRightPlayerSetStatusIdle_Click(object sender, EventArgs e) { this.business.RightPlayerSetStatus = VentuzScenes.GamePool._Modules.SetLarge.ValueStates.Idle; }
        private void buttonRightPlayerSetStatusValid_Click(object sender, EventArgs e) { this.business.RightPlayerSetStatus = VentuzScenes.GamePool._Modules.SetLarge.ValueStates.Valid; }
        private void buttonRightPlayerSetStatusInvalid_Click(object sender, EventArgs e) { this.business.RightPlayerSetStatus = VentuzScenes.GamePool._Modules.SetLarge.ValueStates.Invalid; }

        private void buttonVinsert_SetLargeIn_Click(object sender, EventArgs e) { this.business.Vinsert_SetLargeIn(); }

        #endregion

    }

}
