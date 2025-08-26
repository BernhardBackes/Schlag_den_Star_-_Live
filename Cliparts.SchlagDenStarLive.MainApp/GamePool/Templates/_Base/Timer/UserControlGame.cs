using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.Timer {

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

            this.business = business;

            this.userControlGamePoolTemplates_ModulesTimerGame.Pose(this.business.Vinsert_Timer);

            Binding bind;

            bind = new Binding("Enabled", this.business, "ShowFullscreenTimer");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.buttonVfullscreen_ShowTimer.DataBindings.Add(bind);

            bind = new Binding("Enabled", this.business, "ShowFullscreenTimer");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.buttonVfullscreen_StartTimer.DataBindings.Add(bind);

            bind = new Binding("Enabled", this.business, "ShowFullscreenTimer");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.buttonVfullscreen_StopTimer.DataBindings.Add(bind);

            bind = new Binding("Enabled", this.business, "ShowFullscreenTimer");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.buttonVfullscreen_ResetTimer.DataBindings.Add(bind);

            bind = new Binding("Enabled", this.business, "ShowFullscreenTimer");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.buttonVfullscreen_ResetTimer_1.DataBindings.Add(bind);

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

            if (this.business is Business) this.business.Dispose();

            this.userControlGamePoolTemplates_ModulesTimerGame.Dispose();

            this.buttonVfullscreen_ShowTimer.DataBindings.Clear();
            this.buttonVfullscreen_StartTimer.DataBindings.Clear();
            this.buttonVfullscreen_StopTimer.DataBindings.Clear();
            this.buttonVfullscreen_ResetTimer.DataBindings.Clear();
            this.buttonVfullscreen_ResetTimer_1.DataBindings.Clear();
        }

        #endregion


        #region Events.Incoming
        #endregion

        #region Events.Controls
    
        private void UserControlGame_BackColorChanged(object sender, EventArgs e) {
            this.userControlGamePoolTemplates_ModulesTimerGame.BackColor = this.BackColor;
        }

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        protected virtual void buttonVinsert_TimerIn_Click(object sender, EventArgs e) { this.business.Vinsert_Timer.In(); }
        protected virtual void buttonVinsert_StartTimer_Click(object sender, EventArgs e) { this.business.Vinsert_Timer.Start(); }
        protected virtual void buttonVinsert_StopTimer_Click(object sender, EventArgs e) { this.business.Vinsert_Timer.Stop(); }
        protected virtual void buttonVinsert_TimerOut_Click(object sender, EventArgs e) { this.business.Vinsert_Timer.Out(); }

        protected virtual void buttonVfullscreen_StartTimer_Click(object sender, EventArgs e) { this.business.Vfullscreen_StartTimer(); }
        protected virtual void buttonVfullscreen_StopTimer_Click(object sender, EventArgs e) { this.business.Vfullscreen_StopTimer(); }
        protected virtual void buttonVfullscreen_ResetTimer_Click(object sender, EventArgs e) { this.business.Vfullscreen_ResetTimer(); }

        #endregion

    }

}
 