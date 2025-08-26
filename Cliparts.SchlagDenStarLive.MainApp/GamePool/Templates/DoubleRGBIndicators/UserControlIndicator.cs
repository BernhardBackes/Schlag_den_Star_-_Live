using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.DoubleRGBIndicators {

    public partial class UserControlIndicator : UserControl {

        #region Properties

        private Indicator indicator;

        #endregion


        #region Funktionen

        public UserControlIndicator() {
            InitializeComponent();
        }

        public void Pose(
            Indicator indicator) {

            this.indicator = indicator;
            this.indicator.PropertyChanged += this.indicator_PropertyChanged;

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
   
            if (indicator is Indicator) this.indicator.PropertyChanged -= this.indicator_PropertyChanged;
        }

        private void setButtons() {
            if (this.indicator.LeftPlayerOn) this.buttonLeftPlayer.BackColor = Color.Red;
            else this.buttonLeftPlayer.BackColor = Color.LightSalmon;
            if (this.indicator.RightPlayerOn) this.buttonRightPlayer.BackColor = Color.Blue;
            else this.buttonRightPlayer.BackColor = Color.LightSkyBlue;
        }

        #endregion


        #region Events.Incoming

        void indicator_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.indicator_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "LeftPlayerOn" ||
                    e.PropertyName == "RightPlayerOn") this.setButtons();
            }
        }

        #endregion

        #region Events.Controls

        private void buttonLeft_Click(object sender, EventArgs e) { this.indicator.LeftPlayerOn = true; }
        private void buttonBothPlayers_Click(object sender, EventArgs e) {
            this.indicator.LeftPlayerOn = true;
            this.indicator.RightPlayerOn = true;
        }
        private void buttonRight_Click(object sender, EventArgs e) { this.indicator.RightPlayerOn = true; }

        #endregion

    }
}
