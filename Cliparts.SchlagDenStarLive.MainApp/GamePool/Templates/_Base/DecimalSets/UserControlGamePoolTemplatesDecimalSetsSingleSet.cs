using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.DecimalSets {

    public partial class UserControlGamePoolTemplatesDecimalSetsSingleSet : UserControl {

        #region Properties

        private SingleDecimalSet set;

        #endregion


        #region Funktionen

        public UserControlGamePoolTemplatesDecimalSetsSingleSet() {
            InitializeComponent();

            this.numericUpDownValue.Minimum = int.MinValue;
            this.numericUpDownValue.Maximum = int.MaxValue;
        }

        public void Pose(
            SingleDecimalSet set) {

            this.set = set;
            this.set.PropertyChanged += this.set_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.set, "Value");
            bind.Format += (s, e) => { e.Value = (Single)e.Value; };
            this.numericUpDownValue.DataBindings.Add(bind);

            this.setButtons();

            this.setBorder();
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

            this.set.PropertyChanged -= this.set_PropertyChanged;

            this.numericUpDownValue.DataBindings.Clear();
        }

        private void setButtons() {
            switch (this.set.Status) {
                case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.DecimalSets.ValueStates.Valid:
                    this.buttonIdle.UseVisualStyleBackColor = true;
                    this.buttonValid.BackColor = Constants.ColorEnabled;
                    this.buttonInvalid.UseVisualStyleBackColor = true;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.DecimalSets.ValueStates.Invalid:
                    this.buttonIdle.UseVisualStyleBackColor = true;
                    this.buttonValid.UseVisualStyleBackColor = true;
                    this.buttonInvalid.BackColor = Constants.ColorDisabling;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.DecimalSets.ValueStates.Idle:
                default:
                    this.buttonIdle.UseVisualStyleBackColor = true;
                    this.buttonValid.UseVisualStyleBackColor = true;
                    this.buttonInvalid.UseVisualStyleBackColor = true;
                    break;
            }
        }

        private void setBorder() {
            if (this.set.BorderIsSet) this.numericUpDownValue.BackColor = Constants.ColorEnabled;
            else this.numericUpDownValue.BackColor = SystemColors.Control;
        }

        #endregion


        #region Events.Incoming

        void set_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.set_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "Status") this.setButtons();
                else if (e.PropertyName == "BorderIsSet") this.setBorder();
            }
        }

        #endregion

        #region Events.Controls

        private void numericUpDownValue_ValueChanged(object sender, EventArgs e) { this.set.Value = Convert.ToSingle(this.numericUpDownValue.Value); }

        private void buttonIdle_Click(object sender, EventArgs e) { this.set.Status = VentuzScenes.GamePool._Modules.DecimalSets.ValueStates.Idle; }
        private void buttonValid_Click(object sender, EventArgs e) { this.set.Status = VentuzScenes.GamePool._Modules.DecimalSets.ValueStates.Valid; }
        private void buttonInvalid_Click(object sender, EventArgs e) { this.set.Status = VentuzScenes.GamePool._Modules.DecimalSets.ValueStates.Invalid; }

        #endregion

    }
}
