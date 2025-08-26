using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Cliparts.Tools.Base;

namespace Cliparts.SchlagDenStarLive.MainApp.Content.GameList {

    public partial class FormRenameGame : Form {

        GamePool.Templates._Base.Business game;
        List<string> nameList;

        public FormRenameGame(
            GamePool.Templates._Base.Business game,
            string[] nameList) {

            InitializeComponent();

            this.BackColor = ClipartsColors.DE_DARKBLUE;
            this.Text = "Rename Game";

            this.game = game;

            this.nameList = new List<string>(nameList);

            this.textBoxName.Text = game.Name;

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

        private void textBoxName_TextChanged(object sender, EventArgs e) {
            if (this.textBoxName.Text.Length > 0) {
                if (this.nameList.Contains(this.textBoxName.Text)
                    && this.textBoxName.Text != this.game.Name) {
                    this.textBoxName.BackColor = Color.Salmon;
                    this.buttonOK.Enabled = false;
                }
                else {
                    this.textBoxName.BackColor = Color.White;
                    this.buttonOK.Enabled = true;
                }
            }
            else {
                this.textBoxName.BackColor = Color.White;
                this.buttonOK.Enabled = false;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e) {
            if (!this.nameList.Contains(this.textBoxName.Text)) {
                this.game.Name = this.textBoxName.Text;
                this.Close();
            }
        }
    }
}