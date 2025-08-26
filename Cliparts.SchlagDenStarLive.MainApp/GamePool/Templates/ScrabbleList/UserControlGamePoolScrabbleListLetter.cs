using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.ScrabbleList {
    public partial class UserControlGamePoolScrabbleListLetter : UserControl {

        private LetterContent letter = null;
        public UserControlGamePoolScrabbleListLetter() {
            InitializeComponent();
        }

        public void Pose(
            LetterContent letter) {
            this.letter = letter;

            Binding bind;

            bind = new Binding("Text", this.letter, "Value");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.labelLetterValue.DataBindings.Add(bind);
            bind = new Binding("Enabled", this.letter, "IsIdle");
            bind.Format += (s, e) => { e.Value = Convert.ToBoolean(e.Value); };
            this.labelLetterValue.DataBindings.Add(bind);

            bind = new Binding("Checked", this.letter, "IsIdle");
            bind.Format += (s, e) => { e.Value = Convert.ToBoolean(e.Value); };
            this.checkBoxIsIdle.DataBindings.Add(bind);
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
            this.labelLetterValue.DataBindings.Clear();
            this.checkBoxIsIdle.DataBindings.Clear();
        }

        private void labelLetterValue_EnabledChanged(object sender, EventArgs e) {
            Helper.setControlBackColor(sender as Control);
        }

        private void checkBoxIsIdle_CheckedChanged(object sender, EventArgs e) {
            if (this.letter is LetterContent) this.letter.IsIdle = this.checkBoxIsIdle.Checked;
        }
    }
}
