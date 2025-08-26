using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp {

    internal partial class UserControlRecTrigger : UserControl {

        public new Color ForeColor {
            get { return this.labelValue.ForeColor; }
            set { this.labelValue.ForeColor = value; }
        }

        public new Color BackColor {
            get { return this.labelValue.BackColor; }
            set { 
                this.labelValue.BackColor = value;
                base.BackColor = value;
            }
        }

        public new Font Font {
            get { return this.labelValue.Font; }
            set { this.labelValue.Font = value; }
        }

        public override String Text {
            get { return this.labelValue.Text; }
            set { this.labelValue.Text = value; }
        }

        private Timer triggerTimer;

        public UserControlRecTrigger() {
            InitializeComponent();
            this.triggerTimer = new Timer();
            this.triggerTimer.Interval = 300;
            this.triggerTimer.Tick += this.triggerTimer_Tick;       
        }

        public void StartTrigger() {
            this.triggerTimer.Stop();
            if (this.BackColor != Color.Magenta) this.labelValue.BackColor = Color.Magenta;
            else this.labelValue.BackColor = Color.Cyan;
            this.triggerTimer.Start();
        }

        void triggerTimer_Tick(object sender, EventArgs e) {
            this.triggerTimer.Stop();
            this.labelValue.BackColor = base.BackColor;
        }

        private void UserControlRecTrigger_BackColorChanged(object sender, EventArgs e) {
            this.labelValue.BackColor = base.BackColor;
        }
    }

}
