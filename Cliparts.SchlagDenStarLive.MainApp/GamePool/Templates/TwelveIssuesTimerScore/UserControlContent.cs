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

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TwelveIssuesTimerScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TwelveIssuesTimerScore {

    public partial class UserControlContent : _Base.TimerScore.UserControlContent {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        private int selectedDatasetIndex = -1;

        private Preview.Sources _previewSource = Preview.Sources.Insert;
        protected Preview.Sources previewSource {
            get { return this._previewSource; }
            set {
                if (this._previewSource != value) {
                    this._previewSource = value;
                    this.setPreviewSource(value);
                }
            }
        }

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownBorderPositionX.Minimum = int.MinValue;
            this.numericUpDownBorderPositionX.Maximum = int.MaxValue;

            this.numericUpDownBorderPositionY.Minimum = int.MinValue;
            this.numericUpDownBorderPositionY.Maximum = int.MaxValue;

            this.numericUpDownBorderScaling.Minimum = int.MinValue;
            this.numericUpDownBorderScaling.Maximum = int.MaxValue;

            this.comboBoxBorderStyle.BeginUpdate();
            this.comboBoxBorderStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.Border.Styles)));
            this.comboBoxBorderStyle.EndUpdate();
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "BorderPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownBorderPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "BorderPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownBorderPositionY.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "BorderScaling");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownBorderScaling.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "BorderStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxBorderStyle.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "Filename");
            bind.Format += (s, e) => { e.Value = string.IsNullOrEmpty((string)e.Value) ? "no file loaded" : (string)e.Value; };
            this.labelFilename.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "SampleIncluded");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxDataSampleIncluded.DataBindings.Add(bind);

            this.labelGameClass.Text = this.business.ClassInfo;

            this.fillDataList();

            this.selectDataset(this.business.SelectedDatasetIndex);

            this.labelGameClass.Text = this.business.ClassInfo;

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

            this.numericUpDownBorderPositionX.DataBindings.Clear();
            this.numericUpDownBorderPositionY.DataBindings.Clear();
            this.comboBoxBorderStyle.DataBindings.Clear();
            this.labelFilename.DataBindings.Clear();
            this.checkBoxDataSampleIncluded.DataBindings.Clear();
        }

        private void fillDataList() {
            this.listBoxDataList.BeginUpdate();
            this.listBoxDataList.Items.Clear();
            int id = 1;
            if (this.business.SampleIncluded) id = 0;
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
                this.buttonDataRemoveSet.Enabled = true;
                if (this.listBoxDataList.Items.Count > this.selectedDatasetIndex) this.listBoxDataList.SelectedIndex = this.selectedDatasetIndex;

                this.pictureBoxDatasetBackground.Image = this.selectedDataset.Background;
                this.textBoxDataSetName.Text = this.selectedDataset.Name;
                for (int i = 0; i < Business.ITEMS_COUNT; i++) this.setDataItem(i, this.selectedDataset.GetItem(i));
            }
            else {
                this.groupBoxDataset.Enabled = false;
                this.pictureBoxDatasetBackground.Image = null;
                this.textBoxDataSetName.Text = string.Empty;
                for (int i = 0; i < Business.ITEMS_COUNT; i++) this.setDataItem(i, null);
                this.buttonDataRemoveSet.Enabled = false;
            }

            Helper.setControlBackColor(this.buttonDataRemoveSet);

            this.adjustDataMoveSet();

            this.setPreviewData();
        }

        private void setDataItem(
            int index,
            DataItem item) {
            if (index >= 0 &&
                index < Business.ITEMS_COUNT) {
                string labelDataItemKey = string.Format("labelDataItem_{0}", index.ToString("00"));
                string textBoxDataItemKey = string.Format("textBoxDataItem_{0}", index.ToString("00"));
                Label label = this.groupBoxDataset.Controls[labelDataItemKey] as Label;
                TextBox textBox = this.groupBoxDataset.Controls[textBoxDataItemKey] as TextBox;
                if (label is Label &&
                    textBox is TextBox) {
                    if (item is DataItem &&
                        item.FileExists) {
                        label.Visible = true;
                        textBox.Text = item.Name;
                        textBox.Visible = true;
                    }
                    else {
                        label.Visible = false;
                        textBox.Text = string.Empty;
                        textBox.Visible = false;
                    }

                }
            }
        }

        private void adjustDataMoveSet() {
            this.buttonDataMoveSetUp.Enabled = this.listBoxDataList.SelectedIndex > 0;
            Helper.setControlBackColor(this.buttonDataMoveSetUp);

            this.buttonDataMoveSetDown.Enabled = this.listBoxDataList.SelectedIndex >= 0 && this.listBoxDataList.SelectedIndex < this.listBoxDataList.Items.Count - 1;
            Helper.setControlBackColor(this.buttonDataMoveSetDown);
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

        protected void setPreviewSource(
            Preview.Sources source) {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                previewScene.SetSource(source);
                this.setPreviewData();
            }
        }

        private void setPreviewData() {
            switch (this.previewSource) {
                case Preview.Sources.Insert:
                    this.setInsertPreview();
                    this.radioButtonSourceInsert.Checked = true;
                    break;
                case Preview.Sources.Fullscreen:
                    this.setFullscreenPreview();
                    this.radioButtonSourceFullscreen.Checked = true;
                    break;
                case Preview.Sources.Host:
                    this.setHostPreview();
                    this.radioButtonSourceHost.Checked = true;
                    break;
            }
        }

        protected void setInsertPreview() {
            if (this.previewSceneIsAvailable) {
                this.setScorePreview();
            }
        }

        protected override void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetScore(previewScene.Insert.Score, 2, 3);
                if (this.previewSource == Preview.Sources.Insert) {
                    previewScene.Insert.Border.SetOut();
                    previewScene.Insert.Score.SetIn();
                }
            }
        }

        protected void setFullscreenPreview() {
            if (this.previewSceneIsAvailable) {
                this.setBorderPreview();
                this.setPicturePreview();
            }
        }

        protected virtual void setBorderPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                //this.business.Vinsert_SetBorder(previewScene.Insert.Border, 2, 3);
                if (this.previewSource == Preview.Sources.Fullscreen) {
                    previewScene.Insert.Score.SetOut();
                    previewScene.Insert.Border.SetIn();
                }
            }
        }

        protected void setPicturePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                //if (this.selectedDataset is DatasetContent) this.business.Vfullscreen_SetContent(previewScene.Fullscreen, this.selectedDataset.SolutionFilename, string.Empty);
                //else this.business.Vfullscreen_SetContent(previewScene.Fullscreen, string.Empty, string.Empty);
                //previewScene.Fullscreen.ResetSolution();
                previewScene.Fullscreen.SetIn();
            }
        }

        protected void setHostPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.selectedDataset is DatasetContent) {
                    //this.business.Vhost_Set(previewScene.Host, this.selectedDataset.Name);
                    //previewScene.SetPictureFilename(this.selectedDataset.SolutionFilename);
                }
                else {
                    //this.business.Vhost_Set(previewScene.Host, string.Empty);
                    //previewScene.SetPictureFilename(string.Empty);
                }
                previewScene.Host.SetIn();
            }
        }

        #endregion


        #region Events.Incoming

        void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "NameList") this.fillDataList();
                else if (e.PropertyName == "SampleIncluded") this.fillDataList();
            }
        }

        void selectedDataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.selectedDataset_PropertyChanged(sender, e)));
            else {
                //if (e.PropertyName == "TopText") this.textBoxDataSetTopText.Text = this.selectedDataset.TopText;
                //else if (e.PropertyName == "TaskFirstname") this.textBoxDataSetTaskFirstname.Text = this.selectedDataset.TaskFirstname;
                //else if (e.PropertyName == "SolutionFirstname") this.textBoxDataSetSolutionFirstname.Text = this.selectedDataset.SolutionFirstname;
                //else if (e.PropertyName == "Surname") this.textBoxDataSetSurname.Text = this.selectedDataset.Surname;
            }
        }

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setPreviewSource(this.previewSource);
        }

        #endregion

        #region Events.Controls

        private void numericUpDownBorderPositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.BorderPositionX = (int)this.numericUpDownBorderPositionX.Value;
                this.setFullscreenPreview();
            }
        }
        private void numericUpDownBorderPositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.BorderPositionY = (int)this.numericUpDownBorderPositionY.Value;
                this.setFullscreenPreview();
            }
        }
        private void numericUpDownBorderScaling_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.BorderScaling = (int)this.numericUpDownBorderScaling.Value;
                this.setFullscreenPreview();
            }
        }
        private void comboBoxBorderStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.Border.Styles style;
            if (this.business is Business &&
                Enum.TryParse(this.comboBoxBorderStyle.Text, out style)) {
                this.business.BorderStyle = style;
                this.setFullscreenPreview();
            }
        }

        private void radioButtonSourceInsert_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceInsert.Checked) this.previewSource = Preview.Sources.Insert; }
        private void radioButtonSourceFullscreen_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceFullscreen.Checked) this.previewSource = Preview.Sources.Fullscreen; }
        private void radioButtonSourceHost_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceHost.Checked) this.previewSource = Preview.Sources.Host; }

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
        private void buttonDataResort_Click(object sender, EventArgs e) { this.business.ResortAllDatasets(); }

        private void checkBoxSampleIncluded_CheckedChanged(object sender, EventArgs e) { this.business.SampleIncluded = this.checkBoxDataSampleIncluded.Checked; }
        private void buttonDataAddNewSet_Click(object sender, EventArgs e) {
            string filename = Helper.selectImageFile("select picture", string.Empty);
            if (filename != null) {
                int insertIndex = this.listBoxDataList.SelectedIndex + 1;
                this.business.AddDataset(new DatasetContent(filename), insertIndex);
                this.selectDataset(insertIndex);
            }
        }
        private void buttonDataRemoveSet_Click(object sender, EventArgs e) {
            if (this.business.TryRemoveDataset(this.selectedDatasetIndex)) this.selectDataset(this.selectedDatasetIndex - 1);
        }
        private void buttonDataRemoveAllSets_Click(object sender, EventArgs e) {
            this.business.RemoveAllDatasets();
            this.selectDataset(0);
        }

        private void pictureBoxDatasetBackground_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                string filename = Helper.selectImageFile("select picture", this.selectedDataset.BackgroundFilename);
                if (filename != null) this.selectedDataset.BackgroundFilename = filename;
            }
        }

        private void textBoxDataSetName_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Name = this.textBoxDataSetName.Text; }

        private void textBoxDataItem_TextChanged(object sender, EventArgs e) {
            int index;
            TextBox control = sender as TextBox;
            if (control is TextBox &&
                this.selectedDataset is DatasetContent &&
                Helper.tryParseIndexFromControl(sender as Control, out index) &&
                this.selectedDataset.GetItem(index) is DataItem) this.selectedDataset.GetItem(index).Name = control.Text;
        }

        #endregion

    }

}
