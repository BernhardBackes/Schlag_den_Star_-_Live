using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.NumericSelectApp {

    public partial class FormDisplay : Form {

        public const int ValueMinimum = 0;
        public const int ValueMaximum = 20;

        public FormDisplay() {
            InitializeComponent();
            this.Size = new Size(1024, 768);
            this.pictureBoxBuzzer.Location = new Point(0, 0);
            this.pictureBoxBuzzer.Size = this.Size;
            Cursor.Hide();
        }

        public void SetBuzzer(
            int value) {
            if (value == 0) this.pictureBoxBuzzer.Image = Properties.Resources.Buzzer_00;
            else if (value == 1) this.pictureBoxBuzzer.Image = global::Cliparts.SchlagDenStarLive.NumericSelectApp.Properties.Resources.Buzzer_01;
            else if (value == 2) this.pictureBoxBuzzer.Image = global::Cliparts.SchlagDenStarLive.NumericSelectApp.Properties.Resources.Buzzer_02;
            else if (value == 3) this.pictureBoxBuzzer.Image = global::Cliparts.SchlagDenStarLive.NumericSelectApp.Properties.Resources.Buzzer_03;
            else if (value == 4) this.pictureBoxBuzzer.Image = global::Cliparts.SchlagDenStarLive.NumericSelectApp.Properties.Resources.Buzzer_04;
            else if (value == 5) this.pictureBoxBuzzer.Image = global::Cliparts.SchlagDenStarLive.NumericSelectApp.Properties.Resources.Buzzer_05;
            else if (value == 6) this.pictureBoxBuzzer.Image = global::Cliparts.SchlagDenStarLive.NumericSelectApp.Properties.Resources.Buzzer_06;
            else if (value == 7) this.pictureBoxBuzzer.Image = global::Cliparts.SchlagDenStarLive.NumericSelectApp.Properties.Resources.Buzzer_07;
            else if (value == 8) this.pictureBoxBuzzer.Image = global::Cliparts.SchlagDenStarLive.NumericSelectApp.Properties.Resources.Buzzer_08;
            else if (value == 9) this.pictureBoxBuzzer.Image = global::Cliparts.SchlagDenStarLive.NumericSelectApp.Properties.Resources.Buzzer_09;
            else if (value == 10) this.pictureBoxBuzzer.Image = global::Cliparts.SchlagDenStarLive.NumericSelectApp.Properties.Resources.Buzzer_10;
            else if (value == 11) this.pictureBoxBuzzer.Image = global::Cliparts.SchlagDenStarLive.NumericSelectApp.Properties.Resources.Buzzer_11;
            else if (value == 12) this.pictureBoxBuzzer.Image = global::Cliparts.SchlagDenStarLive.NumericSelectApp.Properties.Resources.Buzzer_12;
            else if (value == 13) this.pictureBoxBuzzer.Image = global::Cliparts.SchlagDenStarLive.NumericSelectApp.Properties.Resources.Buzzer_13;
            else if (value == 14) this.pictureBoxBuzzer.Image = global::Cliparts.SchlagDenStarLive.NumericSelectApp.Properties.Resources.Buzzer_14;
            else if (value == 15) this.pictureBoxBuzzer.Image = global::Cliparts.SchlagDenStarLive.NumericSelectApp.Properties.Resources.Buzzer_15;
            else if (value == 16) this.pictureBoxBuzzer.Image = global::Cliparts.SchlagDenStarLive.NumericSelectApp.Properties.Resources.Buzzer_16;
            else if (value == 17) this.pictureBoxBuzzer.Image = global::Cliparts.SchlagDenStarLive.NumericSelectApp.Properties.Resources.Buzzer_17;
            else if (value == 18) this.pictureBoxBuzzer.Image = global::Cliparts.SchlagDenStarLive.NumericSelectApp.Properties.Resources.Buzzer_18;
            else if (value == 19) this.pictureBoxBuzzer.Image = global::Cliparts.SchlagDenStarLive.NumericSelectApp.Properties.Resources.Buzzer_19;
            else if (value == 20) this.pictureBoxBuzzer.Image = global::Cliparts.SchlagDenStarLive.NumericSelectApp.Properties.Resources.Buzzer_20;
            else this.pictureBoxBuzzer.Image = global::Cliparts.SchlagDenStarLive.NumericSelectApp.Properties.Resources.BG_NEUTRAL;
            this.BringToFront();
        }

    }
}
