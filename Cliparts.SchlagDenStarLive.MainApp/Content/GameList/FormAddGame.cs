using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.Tools.Base;

namespace Cliparts.SchlagDenStarLive.MainApp.Content.GameList {

    public partial class FormAddGame : Form {

        #region Properties

        private Business business;

        private int insertIndex;

        #endregion


        #region Funktionen

        public FormAddGame(
            Business business,
            int insertIndex) {

            InitializeComponent();

            this.BackColor = ClipartsColors.DE_DARKBLUE;
            this.Text = "Add Game";

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            this.insertIndex = insertIndex;

            this.comboBoxIdentifier.BeginUpdate();
            this.comboBoxIdentifier.Items.AddRange(GamePool.AvailableGames.TypeList.Keys.ToArray());
            this.comboBoxIdentifier.SelectedIndex = 0;
            this.comboBoxIdentifier.EndUpdate();

            this.textBoxName.Text = this.comboBoxIdentifier.Text;

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }

            this.business.PropertyChanged -= this.business_PropertyChanged;

            base.Dispose(disposing);
        }

        #endregion


        #region Events.Incoming

        void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "Names") this.textBoxName_TextChanged(this, null);
            }
        }

        #endregion

        #region Events.Controls

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e) {
            this.textBoxName.Text = this.comboBoxIdentifier.Text;
        }

        private void textBoxName_TextChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                if (this.textBoxName.Text.Length == 0 ||
                    this.business.Names.Contains(this.textBoxName.Text)) this.textBoxName.BackColor = Constants.ColorDisabled;
                else this.textBoxName.BackColor = SystemColors.Control;
                this.buttonAdd.Enabled = this.textBoxName.Text.Length > 0 && !this.business.Names.Contains(this.textBoxName.Text);
                Helper.setControlBackColor(this.buttonAdd);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e) {
            this.business.AddGame(this.comboBoxIdentifier.Text, this.textBoxName.Text, this.insertIndex);
            this.insertIndex++;
        }

        #endregion

    }

}
