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

    public partial class UserControlGamePoolTemplatesLevelsCheckedSingleDot : UserControl {

        #region Properties

        private SingleDot dot;

        private Color checkedColor;

        #endregion


        #region Funktionen

        public UserControlGamePoolTemplatesLevelsCheckedSingleDot() {
            InitializeComponent();
        }

        public void Pose(
            SingleDot dot,
            Color checkedColor) {

            this.dot = dot;
            this.dot.PropertyChanged += this.dot_PropertyChanged;

            this.checkedColor = checkedColor;

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
            this.checkBoxIsChecked.Checked = this.dot.IsChecked;
            if (this.checkBoxIsChecked.Checked) this.checkBoxIsChecked.BackColor = SystemColors.Control;
            else this.checkBoxIsChecked.BackColor = this.checkedColor;
        }

        #endregion


        #region Events.Incoming

        void dot_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.dot_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "IsChecked") this.setButtons();
            }
        }

        #endregion

        #region Events.Controls

        #endregion

        private void checkBoxIsChecked_CheckedChanged(object sender, EventArgs e) { this.dot.IsChecked = this.checkBoxIsChecked.Checked; }
    }
}
