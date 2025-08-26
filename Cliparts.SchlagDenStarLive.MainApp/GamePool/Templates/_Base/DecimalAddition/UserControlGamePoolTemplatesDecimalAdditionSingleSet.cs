using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.DecimalAddition {

    public partial class UserControlGamePoolTemplatesDecimalAdditionSingleSet : UserControl {

        #region Properties

        private SingleDecimalSet set;

        #endregion


        #region Funktionen

        public UserControlGamePoolTemplatesDecimalAdditionSingleSet() {
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

            bind = new Binding("Checked", this.set, "Active");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxActive.DataBindings.Add(bind);

            this.setStatus();
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
            this.checkBoxActive.DataBindings.Clear();
        }

        private void setStatus() {
            switch (this.set.Status) {
                case VentuzScenes.GamePool._Modules.DecimalAddition.ValueStates.Valid:
                    this.numericUpDownValue.BackColor = Constants.ColorEnabled;
                    break;
                case VentuzScenes.GamePool._Modules.DecimalAddition.ValueStates.Invalid:
                    this.numericUpDownValue.BackColor = Constants.ColorBuzzered;
                    break;
                case VentuzScenes.GamePool._Modules.DecimalAddition.ValueStates.Idle:
                default:
                    this.numericUpDownValue.BackColor = SystemColors.Control;
                    break;
            }
        }

        #endregion


        #region Events.Incoming

        void set_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.set_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "Status") this.setStatus();
            }
        }

        #endregion

        #region Events.Controls

        private void numericUpDownValue_ValueChanged(object sender, EventArgs e) { this.set.Value = Convert.ToSingle(this.numericUpDownValue.Value); }
        private void checkBoxActive_CheckedChanged(object sender, EventArgs e) { this.set.Active = this.checkBoxActive.Checked; }

        #endregion

    }
}
