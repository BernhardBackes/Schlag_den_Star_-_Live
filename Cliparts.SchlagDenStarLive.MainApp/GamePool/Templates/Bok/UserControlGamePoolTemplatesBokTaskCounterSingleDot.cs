using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.BoK {

    public partial class UserControlGamePoolTemplatesBokTaskCounterSingleDot : UserControl {

        #region Properties

        private SingleDot dot;

        #endregion


        #region Funktionen

        public UserControlGamePoolTemplatesBokTaskCounterSingleDot() {
            InitializeComponent();
        }

        public void Pose(
            SingleDot dot) {

            this.dot = dot;
            this.dot.PropertyChanged += this.dot_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.dot, "Value");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };

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

            if (dot is SingleDot) this.dot.PropertyChanged -= this.dot_PropertyChanged;
        }

        private void setButtons() {
            switch (this.dot.Status) {
                case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Blue:
                    this.buttonOff.UseVisualStyleBackColor = true;
                    this.buttonBlue.BackColor = Color.DodgerBlue;
                    this.buttonRed.BackColor = Color.LightSalmon;
                    this.buttonFail.BackColor = Color.Tan;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Red:
                    this.buttonOff.UseVisualStyleBackColor = true;
                    this.buttonBlue.BackColor = Color.LightSkyBlue;
                    this.buttonRed.BackColor = Color.Firebrick;
                    this.buttonFail.BackColor = Color.Tan;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Fail:
                    this.buttonOff.UseVisualStyleBackColor = true;
                    this.buttonBlue.BackColor = Color.LightSkyBlue;
                    this.buttonRed.BackColor = Color.LightSalmon;
                    this.buttonFail.BackColor = Color.SaddleBrown;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Off:
                default:
                    this.buttonOff.UseVisualStyleBackColor = true;
                    this.buttonBlue.BackColor = Color.LightSkyBlue;
                    this.buttonRed.BackColor = Color.LightSalmon;
                    this.buttonFail.BackColor = Color.Tan;
                    break;
            }
        }

        #endregion


        #region Events.Incoming

        void dot_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.dot_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "Status") this.setButtons();
            }
        }

        #endregion

        #region Events.Controls

        private void buttonOff_Click(object sender, EventArgs e) { this.dot.Status = VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Off; }
        private void buttonBlue_Click(object sender, EventArgs e) { this.dot.Status = VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Blue; }
        private void buttonRed_Click(object sender, EventArgs e) { this.dot.Status = VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Red; }
        private void buttonFail_Click(object sender, EventArgs e) { this.dot.Status = VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Fail; }

        #endregion

    }
}
