using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.RGBIndicatorsScore {

    public partial class UserControlGamePoolTemplatesRGBIndicatorsScoreIndicator : UserControl {

        #region Properties

        private Indicator indicator;

        #endregion


        #region Funktionen

        public UserControlGamePoolTemplatesRGBIndicatorsScoreIndicator() {
            InitializeComponent();
        }

        public void Pose(
            Indicator dot) {

            this.indicator = dot;
            this.indicator.PropertyChanged += this.dot_PropertyChanged;

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
   
            if (indicator is Indicator) this.indicator.PropertyChanged -= this.dot_PropertyChanged;
        }

        private void setButtons() {
            switch (this.indicator.Status) {
                case Indicator.States.Left:
                    this.buttonOff.UseVisualStyleBackColor = true;
                    this.buttonLeftPlayer.BackColor = Color.Red;
                    this.buttonRightPlayer.BackColor = Color.LightSkyBlue;
                    break;
                case Indicator.States.Right:
                    this.buttonOff.UseVisualStyleBackColor = true;
                    this.buttonLeftPlayer.BackColor = Color.LightSalmon;
                    this.buttonRightPlayer.BackColor = Color.Blue;
                    break;
                case Indicator.States.Off:
                default:
                    this.buttonOff.UseVisualStyleBackColor = true;
                    this.buttonLeftPlayer.BackColor = Color.LightSalmon;
                    this.buttonRightPlayer.BackColor = Color.LightSkyBlue;
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

        private void buttonIdle_Click(object sender, EventArgs e) { this.indicator.Status = Indicator.States.Off; }
        private void buttonLeft_Click(object sender, EventArgs e) { this.indicator.Status = Indicator.States.Left; }
        private void buttonRight_Click(object sender, EventArgs e) { this.indicator.Status = Indicator.States.Right; }

        #endregion

    }
}
