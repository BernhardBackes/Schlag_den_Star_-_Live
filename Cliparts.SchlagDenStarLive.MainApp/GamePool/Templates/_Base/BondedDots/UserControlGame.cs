using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.BondedDots {

    public partial class UserControlGame : _Base.UserControlGame {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            Binding bind;

            this.buildStepList();

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            bind = new Binding("Text", this.business, "LeftPlayerDotsSum");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxLeftPlayerSum.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerDotsSum");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxRightPlayerSum.DataBindings.Add(bind);

            this.labelGameClass.Text = this.business.ClassInfo;

            this.fillLeftPlayerDots();
            this.fillRightPlayerDots();
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

        }

        private void fillLeftPlayerDots() {
            int selectedIndex = this.listBoxLeftPlayerDots.SelectedIndex;
            this.listBoxLeftPlayerDots.BeginUpdate();
            this.listBoxLeftPlayerDots.Items.Clear();
            foreach (int item in this.business.LeftPlayerDots) this.listBoxLeftPlayerDots.Items.Add(item);
            this.listBoxLeftPlayerDots.EndUpdate();
            if (this.listBoxLeftPlayerDots.Items.Count > 0) {
                if (selectedIndex < 0) selectedIndex = 0;
                if (selectedIndex >= this.listBoxLeftPlayerDots.Items.Count) selectedIndex = this.listBoxLeftPlayerDots.Items.Count - 1;
                this.listBoxLeftPlayerDots.SelectedIndex = selectedIndex;
            }
            this.listBoxLeftPlayerDots.Enabled = this.listBoxLeftPlayerDots.Items.Count > 0;
            Helper.setControlBackColor(this.listBoxLeftPlayerDots);

            this.buttonLeftPlayerRemoveDot.Enabled = this.listBoxLeftPlayerDots.Items.Count > 0;
            Helper.setControlBackColor(this.buttonLeftPlayerRemoveDot);

            int remaining = this.business.BondedDotsCount - this.business.LeftPlayerDotsSum;
            this.buttonLeftPlayerAdd_01.Enabled = remaining >= 1;
            this.buttonLeftPlayerAdd_02.Enabled = remaining >= 2;
            this.buttonLeftPlayerAdd_03.Enabled = remaining >= 3;
        }

        private void fillRightPlayerDots() {
            int selectedIndex = this.listBoxRightPlayerDots.SelectedIndex;
            this.listBoxRightPlayerDots.BeginUpdate();
            this.listBoxRightPlayerDots.Items.Clear();
            foreach (int item in this.business.RightPlayerDots) this.listBoxRightPlayerDots.Items.Add(item);
            this.listBoxRightPlayerDots.EndUpdate();
            if (this.listBoxRightPlayerDots.Items.Count > 0) {
                if (selectedIndex < 0) selectedIndex = 0;
                if (selectedIndex >= this.listBoxRightPlayerDots.Items.Count) selectedIndex = this.listBoxRightPlayerDots.Items.Count - 1;
                this.listBoxRightPlayerDots.SelectedIndex = selectedIndex;
            }
            this.listBoxRightPlayerDots.Enabled = this.listBoxRightPlayerDots.Items.Count > 0;
            Helper.setControlBackColor(this.listBoxRightPlayerDots);

            this.buttonRightPlayerRemoveDot.Enabled = this.listBoxRightPlayerDots.Items.Count > 0;
            Helper.setControlBackColor(this.buttonRightPlayerRemoveDot);

            int remaining = this.business.BondedDotsCount - this.business.RightPlayerDotsSum;
            this.buttonRightPlayerAdd_01.Enabled = remaining >= 1;
            this.buttonRightPlayerAdd_02.Enabled = remaining >= 2;
            this.buttonRightPlayerAdd_03.Enabled = remaining >= 3;
        }

        #endregion


        #region Events.Incoming

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.business_PropertyChanged(sender, e);
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e is PropertyChangedEventArgs) {
                    if (e.PropertyName == "LeftPlayerDots") this.fillLeftPlayerDots();
                    else if (e.PropertyName == "RightPlayerDots") this.fillRightPlayerDots();
                    else if (e.PropertyName == "BondedDotsCount") {
                        this.fillLeftPlayerDots();
                        this.fillRightPlayerDots();
                    }
                }
            }
        }

        #endregion

        #region Events.Controls

        private void UserControlGame_BackColorChanged(object sender, EventArgs e) {
            //Control control;
            //string key;
        }

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }

        private void buttonLeftPlayerAdd_Click(object sender, EventArgs e) {
            int value;
            if (Helper.tryParseIndexFromControl(sender as Control, out value) &&
                this.business.TryAddDotsToTop(value)) this.listBoxLeftPlayerDots.SelectedIndex = this.listBoxLeftPlayerDots.Items.Count - 1;
        }
        private void buttonLeftPlayerRemoveDot_Click(object sender, EventArgs e) {
            this.business.TryRemoveDotsFromTop(this.listBoxLeftPlayerDots.SelectedIndex);
        }

        private void buttonRightPlayerAdd_Click(object sender, EventArgs e) {
            int value;
            if (Helper.tryParseIndexFromControl(sender as Control, out value) &&
                this.business.TryAddDotsToBottom(value)) this.listBoxRightPlayerDots.SelectedIndex = this.listBoxRightPlayerDots.Items.Count - 1;
        }
        private void buttonRightPlayerRemoveDot_Click(object sender, EventArgs e) {
            this.business.TryRemoveDotsFromBottom(this.listBoxRightPlayerDots.SelectedIndex);
        }

        private void buttonVinsert_BondedDotsIn_Click(object sender, EventArgs e) { this.business.Vinsert_BondedDotsIn(); }

        #endregion

    }

}
