using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.Sets {

    public partial class UserControlGamePoolTemplatesSetsSingleSet : UserControl {

        #region Properties

        private SingleSet set;

        #endregion


        #region Funktionen

        public UserControlGamePoolTemplatesSetsSingleSet() {
            InitializeComponent();

            this.numericUpDownValue.Minimum = int.MinValue;
            this.numericUpDownValue.Maximum = int.MaxValue;
        }

        public void Pose(
            SingleSet set) {

            this.set = set;
            this.set.PropertyChanged += this.set_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.set, "Value");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownValue.DataBindings.Add(bind);

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

            if (set is SingleSet) this.set.PropertyChanged -= this.set_PropertyChanged;

            this.numericUpDownValue.DataBindings.Clear();
        }

        private void setButtons() {
            switch (this.set.Status) {
                case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid:
                    this.buttonIdle.UseVisualStyleBackColor = true;
                    this.buttonValid.BackColor = Constants.ColorEnabled;
                    this.buttonInvalid.UseVisualStyleBackColor = true;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Invalid:
                    this.buttonIdle.UseVisualStyleBackColor = true;
                    this.buttonValid.UseVisualStyleBackColor = true;
                    this.buttonInvalid.BackColor = Constants.ColorDisabling;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Idle:
                default:
                    this.buttonIdle.UseVisualStyleBackColor = true;
                    this.buttonValid.UseVisualStyleBackColor = true;
                    this.buttonInvalid.UseVisualStyleBackColor = true;
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

        public event EventHandler EnterNumericUoDown;
        private void on_EnterNumericUoDown(object sender, EventArgs e) { Helper.raiseEvent(sender, this.EnterNumericUoDown, e); }

        public event EventHandler LeaveNumericUoDown;
        private void on_LeaveNumericUoDown(object sender, EventArgs e) { Helper.raiseEvent(sender, this.LeaveNumericUoDown, e); }

        private void numericUpDownValue_ValueChanged(object sender, EventArgs e) { this.set.Value = (int)this.numericUpDownValue.Value; }

        private void buttonIdle_Click(object sender, EventArgs e) { this.set.Status = VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Idle; }
        private void buttonValid_Click(object sender, EventArgs e) { this.set.Status = VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid; }
        private void buttonInvalid_Click(object sender, EventArgs e) { this.set.Status = VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Invalid; }

        private void numericUpDownValue_Enter(object sender, EventArgs e) { this.on_EnterNumericUoDown(sender, e); }
        private void numericUpDownValue_Leave(object sender, EventArgs e) { this.on_LeaveNumericUoDown(sender, e); }

        #endregion

    }
}
