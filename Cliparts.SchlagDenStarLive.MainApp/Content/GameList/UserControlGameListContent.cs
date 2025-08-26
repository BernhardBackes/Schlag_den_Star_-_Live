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

    public partial class UserControlGameListContent : UserControl {

        #region Properties

        private FormAddGame addGameForm;

        private FormRenameGame renameGameForm;

        private Business business;

        private GamePool.Templates._Base.Business selectedGame = null;

        #endregion


        #region Funktionen

        public UserControlGameListContent() {
            InitializeComponent();
        }

        public void Pose(
            Business business) {

            this.BackColor = this.Parent.BackColor;

            this.buttonMoveUp.Enabled = false;
            this.buttonMoveDown.Enabled = false;
            this.buttonRename.Enabled = false;
            this.buttonRemove.Enabled = false;

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            //Binding bind;
            //bind = new Binding("Items", this.business, "GameList");
            //bind.Format += (s, e) => { e.Value = string.Format("Team {0}", e.Value.ToString()); };
            //this.listBoxGames.DataBindings.Add(bind);

            this.fillGameList();

            this.selectGame(this.business.SelectedGameIndex);

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);

            foreach (Control item in this.Controls) {
                if (item is GamePool.Templates._Base.UserControlContent) {
                    ((GamePool.Templates._Base.UserControlContent)item).Deselect();
                    this.Controls.Remove(item);
                }
            }

            this.business.PropertyChanged -= this.business_PropertyChanged;

        }
        public void DeselectAllGames() {
            foreach (Control item in this.Controls) {
                if (item is GamePool.Templates._Base.UserControlContent) {
                    ((GamePool.Templates._Base.UserControlContent)item).Deselect();
                    this.Controls.Remove(item);
                }
            }
        }

        private void selectGame(
            int index) {
            if (this.business.GamesCount > 0) {
                if (index < 0) index = 0;
                if (index >= this.business.GamesCount) index = this.business.GamesCount - 1;
                GamePool.Templates._Base.Business game = this.business.GetGame(index);
                this.selectGame(game);
            }
        }
        private void selectGame(
            GamePool.Templates._Base.Business game) {
            if (game == null && this.business.GamesCount > 0) this.selectGame(0);
            else {
                if (this.selectedGame != game) {
                    //Dispose
                    DeselectAllGames();
                    this.selectedGame = game;
                    if (this.selectedGame is GamePool.Templates._Base.Business) {
                        //Pose
                        this.selectedGame.ContentControl.Location = this.labelNoGame.Location;
                        this.selectedGame.ContentControl.BackColor = this.BackColor;
                        if (this.selectedGame.ContentControl.IsDisposed) {
                            ;
                        }

                        this.Controls.Add(this.selectedGame.ContentControl);
                        this.selectedGame.ContentControl.Select();
                        this.selectedGame.ContentControl.BringToFront();                        
                        this.labelNoGame.Visible = false;
                    }
                    else this.labelNoGame.Visible = true;
                }
                if (this.selectedGame is GamePool.Templates._Base.Business) {
                    int index = this.business.GetGameIndex(this.selectedGame);
                    if (this.listBoxGames.Items.Count > index) this.listBoxGames.SelectedIndex = index;
                }
                this.buttonRename.Enabled = this.selectedGame is GamePool.Templates._Base.Business;
                this.buttonRemove.Enabled = this.selectedGame is GamePool.Templates._Base.Business;
                this.checkUpDownButtons();
            }
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

        private void checkUpDownButtons() {
            if (this.listBoxGames.Items.Count >= 2) {
                this.buttonMoveUp.Enabled = this.listBoxGames.SelectedIndex > 0;
                this.buttonMoveDown.Enabled = this.listBoxGames.SelectedIndex < this.listBoxGames.Items.Count - 1;
            }
            else {
                this.buttonMoveUp.Enabled = false;
                this.buttonMoveDown.Enabled = false;
            }
        }

        #endregion


        #region Events.Incoming

        void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "Names") this.fillGameList();
                else if (e.PropertyName == "SelectedGame") this.selectGame(this.business.SelectedGameIndex);
            }
        }

        #endregion

        #region Events.Controls

        private void listBoxGames_SelectedIndexChanged(object sender, EventArgs e) { this.selectGame(this.listBoxGames.SelectedIndex); }

        private void listBoxGames_EnabledChanged(object sender, EventArgs e) { Helper.setControlBackColor(sender as Control); }

        private void buttonMoveDown_EnabledChanged(object sender, EventArgs e) { Helper.setControlBackColor(sender as Control); }
        private void buttonMoveDown_Click(object sender, EventArgs e) {
            if (this.selectedGame is GamePool.Templates._Base.Business) {
                int index = this.business.GetGameIndex(this.selectedGame);
                if (this.business.TryMoveGameDown(index)) this.selectGame(this.selectedGame);
            }
        }

        private void buttonMoveUp_EnabledChanged(object sender, EventArgs e) { Helper.setControlBackColor(sender as Control); }
        private void buttonMoveUp_Click(object sender, EventArgs e) {
            if (this.selectedGame is GamePool.Templates._Base.Business) {
                int index = this.business.GetGameIndex(this.selectedGame);
                if (this.business.TryMoveGameUp(index)) this.selectGame(this.selectedGame);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e) {
            if (this.addGameForm == null) this.addGameForm = new FormAddGame(this.business, this.listBoxGames.SelectedIndex + 1);
            this.addGameForm.ShowDialog();
            this.addGameForm.Dispose();
            this.addGameForm = null;
        }

        private void buttonRename_EnabledChanged(object sender, EventArgs e) { Helper.setControlBackColor(sender as Control); }
        private void buttonRename_Click(object sender, EventArgs e) {
            if (this.selectedGame is GamePool.Templates._Base.Business) {
                //this.business.RenameGame(this.business.GetGameIndex(this.selectedGame));
            }
        }

        private void buttonRemove_EnabledChanged(object sender, EventArgs e) { Helper.setControlBackColor(sender as Control); }
        private void buttonRemove_Click(object sender, EventArgs e) { if (this.selectedGame is GamePool.Templates._Base.Business) this.business.RemoveGame(this.business.GetGameIndex(this.selectedGame)); }
        private void buttonExportSettings_Click(object sender, EventArgs e) { this.business.SaveGameHistory(); }

        #endregion

    }

}
