using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.Tools.Base;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.JukeboxScore;
using SlimDX;
using System.Reflection;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.JukeboxScore {

    public partial class UserControlContent : _Base.Score.UserControlContent {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;
        private int selectedDatasetIndex = -1;

        private DatasetItem selectedDatasetItem = null;
        private int selectedDatasetItemIndex = -1;

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Text", this.business, "Filename");
            bind.Format += (s, e) => { e.Value = string.IsNullOrEmpty((string)e.Value) ? "no file loaded" : (string)e.Value; };
            this.labelFilename.DataBindings.Add(bind);

            this.labelGameClass.Text = this.business.ClassInfo;

            this.fillDataList();

            this.selectDataset(this.business.SelectedDatasetIndex);
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
            this.labelFilename.DataBindings.Clear();

        }

        private void fillDataList() {
            this.listBoxDataList.BeginUpdate();
            this.listBoxDataList.Items.Clear();
            int id = 1;
            foreach (string item in this.business.NameList) {
                this.listBoxDataList.Items.Add(string.Format("{0}: {1}", id.ToString("00"), item));
                id++;
            }
            this.listBoxDataList.EndUpdate();

            this.listBoxDataList.Enabled = this.listBoxDataList.Items.Count > 0;
            Helper.setControlBackColor(this.listBoxDataList);

            this.buttonDataRemoveAllSets.Enabled = this.listBoxDataList.Items.Count > 0;
            Helper.setControlBackColor(this.buttonDataRemoveAllSets);

            this.selectDataset(this.selectedDatasetIndex);
        }

        private void selectDataset(
            int index) {
            if (index < 0) index = 0;
            if (index >= this.business.DatasetsCount) index = this.business.DatasetsCount - 1;
            DatasetContent selectedDataset = this.business.GetDataset(index);
            this.selectedDatasetIndex = this.business.GetDatasetIndex(selectedDataset);

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
                this.groupBoxDataset.Enabled = true;
                this.textBoxDataSetName.Text = this.selectedDataset.Name;
                this.buttonDataRemoveSet.Enabled = true;
                if (this.listBoxDataList.Items.Count > this.selectedDatasetIndex) this.listBoxDataList.SelectedIndex = this.selectedDatasetIndex;
            }
            else {
                this.groupBoxDataset.Enabled = false;
                this.textBoxDataSetName.Text = string.Empty;
                this.buttonDataRemoveSet.Enabled = false;
            }
            Helper.setControlBackColor(this.buttonDataRemoveSet);

            this.adjustDataMoveSet();

            this.fillListBoxDatasetItems();
        }

        private void adjustDataMoveSet() {
            this.buttonDataMoveSetUp.Enabled = this.listBoxDataList.SelectedIndex > 0;
            Helper.setControlBackColor(this.buttonDataMoveSetUp);

            this.buttonDataMoveSetDown.Enabled = this.listBoxDataList.SelectedIndex >= 0 && this.listBoxDataList.SelectedIndex < this.listBoxDataList.Items.Count - 1;
            Helper.setControlBackColor(this.buttonDataMoveSetDown);
        }

        private void fillListBoxDatasetItems() {
            this.listBoxDatasetItems.BeginUpdate();
            this.listBoxDatasetItems.Items.Clear();
            if (this.selectedDataset is DatasetContent) this.listBoxDatasetItems.Items.AddRange(this.selectedDataset.ItemList);
            this.listBoxDatasetItems.Enabled = this.listBoxDatasetItems.Items.Count > 0;
            Helper.setControlBackColor(this.listBoxDatasetItems);
            this.listBoxDatasetItems.EndUpdate();

            this.buttonDataRemoveAllSets.Enabled = this.listBoxDatasetItems.Items.Count > 0;
            Helper.setControlBackColor(this.buttonDataRemoveAllSets);

            this.selectDatasetItem(this.selectedDatasetItemIndex);
        }

        private void selectDatasetItem(
            int index) {
            if (index < 0) index = 0;
            this.selectedDatasetItem = null;
            if (this.selectedDataset is DatasetContent) {
                if (index >= this.selectedDataset.ItemsCount) index = this.selectedDataset.ItemsCount - 1;
                this.selectedDatasetItem = this.selectedDataset.GetItem(index);
                this.selectedDatasetItemIndex = index;
            }

            if (this.selectedDatasetItem is DatasetItem) {
                if (this.listBoxDatasetItems.Items.Count > this.selectedDatasetItemIndex) this.listBoxDatasetItems.SelectedIndex = this.selectedDatasetItemIndex;
                this.buttonDatasetRemoveSong.Enabled = true;
                this.buttonDatasetPlaySong.Enabled = true;
                this.business.SetSong(this.selectedDatasetItem.Filename);
            }
            else {
                this.buttonDatasetRemoveSong.Enabled = false;
                this.buttonDatasetPlaySong.Enabled = false;
            }
            Helper.setControlBackColor(this.buttonDatasetRemoveSong);
            Helper.setControlBackColor(this.buttonDatasetPlaySong);

            this.adjustDataItemMoveSet();
        }

        private void adjustDataItemMoveSet() {
            this.buttonDatasetMoveSongUp.Enabled = this.listBoxDatasetItems.SelectedIndex > 0;
            Helper.setControlBackColor(this.buttonDatasetMoveSongUp);

            this.buttonDatasetMoveSongDown.Enabled = this.listBoxDatasetItems.SelectedIndex >= 0 && this.listBoxDatasetItems.SelectedIndex < this.listBoxDatasetItems.Items.Count - 1;
            Helper.setControlBackColor(this.buttonDatasetMoveSongDown);
        }

        public override void SetPreviewPipe(
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            bool selected = ((Preview)this.previewScene) is VRemote4.HandlerSi.Scene;
            base.SetPreviewPipe(previewPipe);
            if (selected) this.Select();
        }

        public override void Select() {
            if (this.previewPipe is VRemote4.HandlerSi.Client.Pipe.Business &&
                this.previewPipe.Resolution.HasValue &&
                this.previewPipe.ShareHandle.HasValue) {
                base.select(new Preview(WindowsFormsSynchronizationContext.Current, this.previewPipe));
            }
        }

        protected override void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetScore(previewScene.Insert.Score, 2, 3);
                previewScene.Insert.Score.SetIn();
            }
        }

        #endregion


        #region Events.Incoming

        void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "NameList") this.fillDataList();
            }
        }

        void selectedDataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.selectedDataset_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "ItemList") this.fillListBoxDatasetItems();
                else if (e.PropertyName == "Name") this.textBoxDataSetName.Text = this.selectedDataset.Name;
            }
        }

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setScorePreview();
        }

        #endregion

        #region Events.Controls

        private void buttonLoad_Click(object sender, EventArgs e) {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Load Data";
            dialog.InitialDirectory = ApplicationAttributes.ContentPath;
            if (File.Exists(this.business.Filename)) dialog.FileName = this.business.Filename;
            dialog.DefaultExt = "*.xml";
            dialog.Filter = "XML-File (*.xml)|*.xml|all files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.Multiselect = false;
            dialog.RestoreDirectory = true;
            switch (dialog.ShowDialog()) {
                case DialogResult.Cancel:
                    break;
                case DialogResult.OK:
                    this.business.Load(dialog.FileName);
                    break;
            }
            dialog = null;

        }
        private void buttonSave_Click(object sender, EventArgs e) {
            if (File.Exists(this.business.Filename)) this.business.Save();
            else buttonSaveAs_Click(sender, e);
        }
        private void buttonSaveAs_Click(object sender, EventArgs e) {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Save Data As...";
            dialog.InitialDirectory = ApplicationAttributes.ContentPath;
            if (File.Exists(this.business.Filename)) dialog.FileName = this.business.Filename;
            dialog.DefaultExt = "*.xml";
            dialog.Filter = "XML-File (*.xml)|*.xml|all files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;
            switch (dialog.ShowDialog()) {
                case DialogResult.Cancel:
                    break;
                case DialogResult.OK:
                    this.business.SaveAs(dialog.FileName);
                    break;
            }
            dialog = null;
        }

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.selectDataset(this.listBoxDataList.SelectedIndex); }
        private void buttonDataMoveSetUp_Click(object sender, EventArgs e) {
            if (this.business.TryMoveDatasetUp(this.selectedDatasetIndex)) this.selectDataset(this.selectedDatasetIndex - 1);
        }
        private void buttonDataMoveSetDown_Click(object sender, EventArgs e) {
            if (this.business.TryMoveDatasetDown(this.selectedDatasetIndex)) this.selectDataset(this.selectedDatasetIndex + 1);
        }

        private void buttonDataAddNewSet_Click(object sender, EventArgs e) {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowNewFolderButton = false;
            dialog.SelectedPath = Helper.getContentPath(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyTitleAttribute>().Title);
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK) {
                int listIndex = this.listBoxDataList.SelectedIndex + 1;
                this.business.AddDataset(new DatasetContent(dialog.SelectedPath), listIndex);
                this.selectDataset(listIndex);
            }
        }
        private void buttonDataRemoveSet_Click(object sender, EventArgs e) {
            if (this.business.TryRemoveDataset(this.selectedDatasetIndex)) this.selectDataset(this.selectedDatasetIndex - 1);
        }
        private void buttonDataRemoveAllSets_Click(object sender, EventArgs e) {
            this.business.RemoveAllDatasets();
            this.selectDataset(0);
        }

        private void textBoxDataSetName_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Name = this.textBoxDataSetName.Text; }

        private void listBoxDatasetSongs_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectDatasetItem(this.listBoxDatasetItems.SelectedIndex);
        }
        private void buttonDatasetMoveSongUp_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent && this.selectedDataset.TryMoveItemUp(this.selectedDatasetItemIndex)) this.listBoxDatasetItems.SelectedIndex--;
        }
        private void buttonDatasetMoveSongDown_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent && this.selectedDataset.TryMoveItemDown(this.selectedDatasetItemIndex)) this.listBoxDatasetItems.SelectedIndex++;
        }
        private void buttonDatasetAddNewSong_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                string filename = Helper.selectAudioFile("select song", string.Empty);
                if (!string.IsNullOrEmpty(filename)) this.selectedDataset.AddItem(new DatasetItem(filename));
            }
        }
        private void buttonDatasetRemoveSong_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.TryRemoveItem(this.selectedDatasetItemIndex);
        }
        private void buttonDatasetRemoveAllSongs_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.RemoveAllItems();
        }

        private void buttonDatasetPlaySong_Click(object sender, EventArgs e) { this.business.PlaySong(); }
        private void buttonDatasetStopSong_Click(object sender, EventArgs e) { this.business.StopSong(); }

        #endregion

    }

}
