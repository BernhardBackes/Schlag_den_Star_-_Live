using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.Content.Playlist {

    public partial class UserControlPlaylist : UserControl {

        #region Properties

        private Business business;
        private FormEditPlaylist businessForm;

        private DatasetContent selectedDataset = null;

        #endregion


        #region Funktionen

        public UserControlPlaylist() {
            InitializeComponent();
        }

        public void Pose(
            Business business) {

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            this.setPlayoutChannel();
            this.fillDataList();
            this.selectDataset(this.business.SelectedDataset);
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

            this.business.PropertyChanged -= this.business_PropertyChanged;
            if (this.selectedDataset is DatasetContent) this.selectedDataset.PropertyChanged -= this.selectedDataset_PropertyChanged;
        }

        private void setPlayoutChannel() {
            this.radioButtonInsert.Checked = this.business.PlayoutChannel == PlayoutChannelElements.Insert;
            this.radioButtonFullscreen.Checked = this.business.PlayoutChannel == PlayoutChannelElements.Fullscreen;
        }

        private void fillDataList() {
            this.listBoxDataList.BeginUpdate();
            this.listBoxDataList.Items.Clear();
            this.listBoxDataList.Items.AddRange(this.business.NameList);
            this.listBoxDataList.EndUpdate();

            this.listBoxDataList.Enabled = this.listBoxDataList.Items.Count > 0;
            Helper.setControlBackColor(this.listBoxDataList);

            this.selectDataList();
        }

        private void selectDataList() {
            int index = this.business.SelectedDatasetIndex;
            if (index >= 0 &&
                index < this.listBoxDataList.Items.Count) this.listBoxDataList.SelectedIndex = index;
            else this.listBoxDataList.SelectedIndex = -1;
        }

        private void selectDataset(
            DatasetContent selectedDataset) {
            if (this.selectedDataset != selectedDataset) {
                //Dispose...
                if (this.selectedDataset is DatasetContent) {
                    this.selectedDataset.PropertyChanged -= this.selectedDataset_PropertyChanged;
                }
                this.selectedDataset = selectedDataset;
                //Pose...
                if (this.selectedDataset is DatasetContent) {
                    this.selectedDataset.PropertyChanged += this.selectedDataset_PropertyChanged;
                }
            }

            if (this.selectedDataset is DatasetContent) {
                this.buttonPlay.Enabled = true;
                this.buttonPause.Enabled = true;
            }
            else {
                this.buttonPlay.Enabled = false;
                this.buttonPause.Enabled = false;
            }

            Helper.setControlBackColor(this.buttonPlay);
            Helper.setControlBackColor(this.buttonPause);

            this.selectDataList();
        }

        #endregion


        #region Events.Incoming

        private void businessForm_Disposed(object sender, EventArgs e) {
            if (this.businessForm is FormEditPlaylist) {
                this.businessForm.Disposed -= this.businessForm_Disposed;
                this.businessForm = null;
            }
        }

        protected void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "PlayoutChannel") this.setPlayoutChannel();
                else if (e.PropertyName == "NameList") this.fillDataList();
                else if (e.PropertyName == "SelectedDataset") this.selectDataset(this.business.SelectedDataset);
                else if (e.PropertyName == "SelectedDatasetIndex") this.selectDataList();
            }
        }

        void selectedDataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.selectedDataset_PropertyChanged(sender, e)));
            else {
            }
        }

        #endregion

        #region Events.Controls

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.listBoxDataList.SelectedIndex >= 0) this.business.SelectDataset(this.listBoxDataList.SelectedIndex);
        }

        private void buttonEditList_Click(object sender, EventArgs e) {
            if (this.businessForm == null) {
                this.businessForm = new FormEditPlaylist(this.business);
                this.businessForm.Disposed += this.businessForm_Disposed;
            }
            this.businessForm.BackColor = this.BackColor;
            this.businessForm.Show();
            this.businessForm.BringToFront();
        }

        private void radioButtonInsert_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonInsert.Checked) this.business.PlayoutChannel = PlayoutChannelElements.Insert; }
        private void radioButtonFullscreen_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonFullscreen.Checked) this.business.PlayoutChannel = PlayoutChannelElements.Fullscreen; }

        private void buttonPlay_Click(object sender, EventArgs e) { this.business.Play(); }
        private void buttonPause_Click(object sender, EventArgs e) { this.business.Pause(); }
        private void buttonEject_Click(object sender, EventArgs e) { this.business.Eject(); }

        #endregion
    }
}
