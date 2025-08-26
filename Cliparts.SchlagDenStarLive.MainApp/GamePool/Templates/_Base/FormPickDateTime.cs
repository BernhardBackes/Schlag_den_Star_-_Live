using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base {

    public partial class FormPickDateTime : Form {

        public DateTime Value { get { return this.dateTimePicker.Value; } }

        public FormPickDateTime(
            DateTime startValue) {
            InitializeComponent();
            this.Text = "select date";
            this.dateTimePicker.Value = startValue;
        }

        private void buttonOK_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
