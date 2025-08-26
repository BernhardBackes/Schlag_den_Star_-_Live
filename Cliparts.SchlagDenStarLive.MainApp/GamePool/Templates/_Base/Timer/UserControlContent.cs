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

    public partial class UserControlContent : _Base.UserControlContent {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            this.userControlGamePoolTemplates_ModulesTimerContent.Pose(this.business.Vinsert_Timer);

            Binding bind;

            bind = new Binding("Checked", this.business, "ShowFullscreenTimer");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxShowFullscreenTimer.DataBindings.Add(bind);

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

            this.userControlGamePoolTemplates_ModulesTimerContent.Dispose();

            this.checkBoxShowFullscreenTimer.DataBindings.Clear();
        }

        protected virtual void setTimerPreview() { }

        #endregion


        #region Events.Incoming
        protected virtual void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "PositionX" ||
                    e.PropertyName == "PositionY" ||
                    e.PropertyName == "Scaling" ||
                    e.PropertyName == "StartTime") this.setTimerPreview();
            }
        }

        #endregion

        #region Events.Controls
        private void UserControlContent_BackColorChanged(object sender, EventArgs e) {
            this.userControlGamePoolTemplates_ModulesTimerContent.BackColor = this.BackColor;
        }

        private void checkBoxShowFullscreenTimer_CheckedChanged(object sender, EventArgs e) { this.business.ShowFullscreenTimer = this.checkBoxShowFullscreenTimer.Checked; }

        #endregion

    }

}
