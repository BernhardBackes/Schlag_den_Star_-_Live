using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.ScorePyramid {

    public partial class UserControlGamePoolTemplatesScorePyramidLevel : UserControl {

        #region Properties

        private DatasetLevel level;

        #endregion


        #region Funktionen

        public UserControlGamePoolTemplatesScorePyramidLevel() {
            InitializeComponent();

            this.numericUpDownLeftPlayerHits.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerHits.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerHits.Minimum = int.MinValue;
            this.numericUpDownRightPlayerHits.Maximum = int.MaxValue;
        }

        public void Pose(
            DatasetLevel level) {

            this.level = level;
            this.level.PropertyChanged += this.level_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.level, "LeftPlayerHits");
            bind.Format += (s, e) => { e.Value = Convert.ToInt32(e.Value); };
            this.numericUpDownLeftPlayerHits.DataBindings.Add(bind);

            bind = new Binding("Value", this.level, "RightPlayerHits");
            bind.Format += (s, e) => { e.Value = Convert.ToInt32(e.Value); };
            this.numericUpDownRightPlayerHits.DataBindings.Add(bind);


            this.labelValue.Text = this.level.Value.ToString();

            this.set_labelValue();
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

            if (level is DatasetLevel) this.level.PropertyChanged -= this.level_PropertyChanged;

            this.numericUpDownLeftPlayerHits.DataBindings.Clear();
            this.numericUpDownRightPlayerHits.DataBindings.Clear();
        }

        private void set_labelValue() {
            if (this.level.Value > 0 &&
                this.level.LeftPlayerValue != this.level.RightPlayerValue) {
                if (this.level.LeftPlayerValue > this.level.RightPlayerValue) this.labelValue.BackColor = Constants.ColorLeftPlayer;
                else this.labelValue.BackColor = Constants.ColorRightPlayer;
                this.labelValue.ForeColor = Color.White;
            } 
            else {
                this.labelValue.BackColor = SystemColors.Control;
                this.labelValue.ForeColor = Color.Black;
            }
        }

        #endregion


        #region Events.Incoming

        void level_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.level_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "LeftPlayerValue" ||
                    e.PropertyName == "RightPlayerValue") this.set_labelValue();
            }
        }

        #endregion

        #region Events.Controls
        private void buttonAddLeft_Click(object sender, EventArgs e) { this.level.LeftPlayerHits++; }
        private void buttonAddRight_Click(object sender, EventArgs e) { this.level.RightPlayerHits++; }

        #endregion

    }
}
