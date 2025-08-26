using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.Content.GameList {

    public partial class UserControlGameList : UserControl {

        #region Properties

        private Business business;

        private GamePool.Templates._Base.Business selectedGame = null;

        private bool keyControl = false;
        public bool KeyControl {
            set {
                if (this.keyControl != value) {
                    this.keyControl = value;
                    if (this.selectedGame is GamePool.Templates._Base.Business &&
                        this.selectedGame.GameControl is GamePool.Templates._Base.UserControlGame) this.selectedGame.GameControl.KeyControl = value;
                }
            }
        }

        #endregion


        #region Funktionen

        public UserControlGameList() { InitializeComponent(); }

        public void Pose(
            Business business) {

            this.BackColor = this.Parent.BackColor;

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            this.fillGameList();

            this.selectGame(this.business.SelectedGame);

        }

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) { components.Dispose(); }

            this.business.PropertyChanged -= this.business_PropertyChanged;

            base.Dispose(disposing);
        }

        private void selectGame(
            GamePool.Templates._Base.Business game) {
            if (this.selectedGame != game) {
                if (this.selectedGame is GamePool.Templates._Base.Business) {
                    //Dispose
                    //this.selectedGame.Error -= this.;
                    if (this.selectedGame.GameControl is GamePool.Templates._Base.UserControlGame) {
                        if (this.Controls.Contains(this.selectedGame.GameControl)) {
                            //this.selectedGame.GameControl.EnterControl -= this.GameControl_EnterControl;
                            //this.selectedGame.GameControl.LeaveControl -= this.GameControl_LeaveControl;
                            this.Controls.Remove(this.selectedGame.GameControl);
                        }
                    }
                }
                foreach (Control items in this.Controls) {
                    if (items is GamePool.Templates._Base.UserControlGame) this.Controls.Remove(items);
                }

                this.selectedGame = game;

                if (game is GamePool.Templates._Base.Business) {
                    //Pose
                    this.selectedGame.GameControl.Location = this.labelNoGame.Location;
                    this.selectedGame.GameControl.BackColor = this.BackColor;
                    this.Controls.Add(this.selectedGame.GameControl);
                    this.selectedGame.GameControl.BringToFront();
                    this.selectedGame.GameControl.Activate(this.keyControl);
                    this.selectedGame.GameControl.EnterControl += this.GameControl_EnterControl;
                    this.selectedGame.GameControl.LeaveControl += this.GameControl_LeaveControl;
                    int index = this.business.GetGameIndex(this.selectedGame);
                    if (index >= 0 &&
                        index < this.listBoxGames.Items.Count) this.listBoxGames.SelectedIndex = index;
                    this.labelNotepad.Visible = true;
                    this.buttonNotepadFont.Visible = true;
                    this.richTextBoxNotepad.Visible = true;
                    this.richTextBoxNotepad.Rtf = game.Notepad;
                }
                else {
                    this.labelNotepad.Visible = false;
                    this.buttonNotepadFont.Visible = false;
                    this.richTextBoxNotepad.Visible = false;
                }
            }
        }

        public void ParseKey(
            Keys key) {
            if (this.selectedGame is GamePool.Templates._Base.Business &&
                this.selectedGame.GameControl is GamePool.Templates._Base.UserControlGame) this.selectedGame.GameControl.ParseKey(key);
        }

        private void fillGameList() {
            this.listBoxGames.BeginUpdate();
            this.listBoxGames.Items.Clear();
            int index = 0;
            foreach (string item in this.business.Names) {
                int id = index + 1;
                this.listBoxGames.Items.Add(id.ToString("00") + ": " + item);
                index++;
            }
            this.listBoxGames.Enabled = this.listBoxGames.Items.Count > 0;
            this.listBoxGames.EndUpdate();

            this.selectGame(this.selectedGame);
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler EnterControl;
        private void on_EnterControl(object sender, EventArgs e) { if (this.EnterControl != null) this.EnterControl(sender, e); }

        public event EventHandler LeaveControl;
        private void on_LeaveControl(object sender, EventArgs e) { if (this.LeaveControl != null) this.LeaveControl(sender, e); }

        #endregion

        #region Events.Incoming

        void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "Names") this.fillGameList();
                else if (e.PropertyName == "SelectedGame") this.selectGame(this.business.SelectedGame);
            }
        }

        private void GameControl_EnterControl(object sender, EventArgs e) {
            this.on_EnterControl(sender, e);
        }

        private void GameControl_LeaveControl(object sender, EventArgs e) {
            this.on_LeaveControl(sender, e);
        }

        #endregion

        #region Events.Controls

        private void UserControlGameList_BackColorChanged(object sender, EventArgs e) { }

        private void listBoxGames_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectGame(this.listBoxGames.SelectedIndex); }

        private void buttonNotepadFont_Click(object sender, EventArgs e) {
            FontDialog dialog = new FontDialog();
            dialog.ShowColor = true;
            dialog.ShowEffects = true;
            if (this.richTextBoxNotepad.SelectionFont == null) dialog.Font = this.richTextBoxNotepad.Font;
            else dialog.Font = this.richTextBoxNotepad.SelectionFont;
            if (this.richTextBoxNotepad.SelectionColor == null) dialog.Color = this.richTextBoxNotepad.ForeColor;
            else dialog.Color = this.richTextBoxNotepad.SelectionColor;
            switch (dialog.ShowDialog()) {
                case DialogResult.Cancel:
                    break;
                case DialogResult.OK:
                    if (this.selectedGame is GamePool.Templates._Base.Business) {
                        this.richTextBoxNotepad.SelectionFont = dialog.Font;
                        this.richTextBoxNotepad.SelectionColor = dialog.Color;
                        selectedGame.Notepad = this.richTextBoxNotepad.Rtf;
                    }
                    break;
            }
        }
        private void richTextBoxNotepad_Enter(object sender, EventArgs e) {
            //this.lastKeyControl = MainApp.KeyControl;
            //MainApp.KeyControl = false;
            //this.AdjustGUI();
            
        }
        private void richTextBoxNotepad_Leave(object sender, EventArgs e) {
            if (this.selectedGame is GamePool.Templates._Base.Business) {
                selectedGame.Notepad = this.richTextBoxNotepad.Rtf;
            }
        }

        #endregion

    }

}
