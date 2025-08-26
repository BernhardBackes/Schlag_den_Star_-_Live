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

    public partial class UserControlGamePoolTemplatesPenaltyScoreSingleDot : UserControl {

        #region Properties

        private SingleDot dot;

        #endregion


        #region Funktionen

        public UserControlGamePoolTemplatesPenaltyScoreSingleDot() {
            InitializeComponent();
        }

        public void Pose(
            SingleDot dot) {

            this.dot = dot;
            this.dot.PropertyChanged += this.set_PropertyChanged;

            this.setButtons();
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
   
            if (dot is SingleDot) this.dot.PropertyChanged -= this.set_PropertyChanged;
        }

        private void setButtons() {
            switch (this.dot.Status) {
                case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.PenaltyScore.DotStates.Green:
                    this.buttonOff.UseVisualStyleBackColor = true;
                    this.buttonGreen.BackColor = Color.LimeGreen;
                    this.buttonRed.BackColor = Color.LightSalmon;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.PenaltyScore.DotStates.Red:
                    this.buttonOff.UseVisualStyleBackColor = true;
                    this.buttonGreen.BackColor = Color.PaleGreen;
                    this.buttonRed.BackColor = Color.Red;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.PenaltyScore.DotStates.Off:
                default:
                    this.buttonOff.UseVisualStyleBackColor = true;
                    this.buttonGreen.BackColor = Color.PaleGreen;
                    this.buttonRed.BackColor = Color.LightSalmon;
                    break;
            }
        }

        #endregion


        #region Events.Incoming

        void set_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.set_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "Status") this.setButtons();
            }
        }

        #endregion

        #region Events.Controls

        private void buttonIdle_Click(object sender, EventArgs e) { this.dot.Status = VentuzScenes.GamePool._Modules.PenaltyScore.DotStates.Off; }
        private void buttonGreen_Click(object sender, EventArgs e) { this.dot.Status = VentuzScenes.GamePool._Modules.PenaltyScore.DotStates.Green; }
        private void buttonRed_Click(object sender, EventArgs e) { this.dot.Status = VentuzScenes.GamePool._Modules.PenaltyScore.DotStates.Red; }

        #endregion

    }
}
