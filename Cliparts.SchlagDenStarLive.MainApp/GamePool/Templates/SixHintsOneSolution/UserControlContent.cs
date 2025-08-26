using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.Tools.Base;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.SixHintsOneSolution {

    public partial class UserControlContent : _Base.BuzzerScore.UserControlContent {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        private int selectedDatasetIndex = -1;

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownItemInsertPositionX.Minimum = int.MinValue;
            this.numericUpDownItemInsertPositionX.Maximum = int.MaxValue;

            this.numericUpDownItemInsertPositionY.Minimum = int.MinValue;
            this.numericUpDownItemInsertPositionY.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "ItemInsertPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownItemInsertPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "ItemInsertPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownItemInsertPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "Filename");
            bind.Format += (s, e) => { e.Value = string.IsNullOrEmpty((string)e.Value) ? "no file loaded" : (string)e.Value; };
            this.labelFilename.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "SampleIncluded");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxDataSampleIncluded.DataBindings.Add(bind);

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

            this.numericUpDownItemInsertPositionX.DataBindings.Clear();
            this.numericUpDownItemInsertPositionY.DataBindings.Clear();

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
                this.pictureBoxDatasetSolution.Image = this.selectedDataset.Solution;
                this.textBoxDatasetItem1.Text = this.selectedDataset.Item1;
                this.textBoxDatasetItem2.Text = this.selectedDataset.Item2;
                this.textBoxDatasetItem3.Text = this.selectedDataset.Item3;
                this.textBoxDatasetItem4.Text = this.selectedDataset.Item4;
                this.textBoxDatasetItem5.Text = this.selectedDataset.Item5;
                this.textBoxDatasetItem6.Text = this.selectedDataset.Item6;
                this.textBoxDataSetName.Text = this.selectedDataset.Name;
                this.buttonDataRemoveSet.Enabled = true;
                if (this.listBoxDataList.Items.Count > this.selectedDatasetIndex) this.listBoxDataList.SelectedIndex = this.selectedDatasetIndex;
            }
            else {
                this.groupBoxDataset.Enabled = false;
                this.pictureBoxDatasetSolution.Image = null;
                this.textBoxDatasetItem1.Text = string.Empty;
                this.textBoxDatasetItem2.Text = string.Empty;
                this.textBoxDatasetItem3.Text = string.Empty;
                this.textBoxDatasetItem4.Text = string.Empty;
                this.textBoxDatasetItem5.Text = string.Empty;
                this.textBoxDatasetItem6.Text = string.Empty;
                this.textBoxDataSetName.Text = string.Empty;
                this.buttonDataRemoveSet.Enabled = false;
            }
            Helper.setControlBackColor(this.buttonDataRemoveSet);

            this.setTextBoxDatasetAllItems();

            this.adjustDataMoveSet();
        }

        private void adjustDataMoveSet() {
            this.buttonDataMoveSetUp.Enabled = this.listBoxDataList.SelectedIndex > 0;
            Helper.setControlBackColor(this.buttonDataMoveSetUp);

            this.buttonDataMoveSetDown.Enabled = this.listBoxDataList.SelectedIndex >= 0 && this.listBoxDataList.SelectedIndex < this.listBoxDataList.Items.Count - 1;
            Helper.setControlBackColor(this.buttonDataMoveSetDown);
        }

        private void setTextBoxDatasetAllItems() {
            if (this.selectedDataset is DatasetContent) {
                this.textBoxDatasetAllItems.Text = string.Format("{0}\r\n{1}\r\n{2}\r\n{3}\r\n{4}\r\n{5}",
                    this.selectedDataset.Item1,
                    this.selectedDataset.Item2,
                    this.selectedDataset.Item3,
                    this.selectedDataset.Item4,
                    this.selectedDataset.Item5,
                    this.selectedDataset.Item6);
            }
            else this.textBoxDatasetAllItems.Text = string.Empty;
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
                if (e.PropertyName == "PictureFilename") this.pictureBoxDatasetSolution.Image = this.selectedDataset.Solution;
                else if (e.PropertyName == "Item1") this.textBoxDatasetItem1.Text = this.selectedDataset.Item1;
                else if (e.PropertyName == "Item2") this.textBoxDatasetItem2.Text = this.selectedDataset.Item2;
                else if (e.PropertyName == "Item3") this.textBoxDatasetItem3.Text = this.selectedDataset.Item3;
                else if (e.PropertyName == "Item4") this.textBoxDatasetItem4.Text = this.selectedDataset.Item4;
                else if (e.PropertyName == "Item5") this.textBoxDatasetItem5.Text = this.selectedDataset.Item5;
                else if (e.PropertyName == "Item6") this.textBoxDatasetItem6.Text = this.selectedDataset.Item6;
                else if (e.PropertyName == "Name") this.textBoxDataSetName.Text = this.selectedDataset.Name;
                this.setTextBoxDatasetAllItems();
            }
        }

        #endregion

        #region Events.Controls

        protected virtual void numericUpDownItemInsertPositionX_ValueChanged(object sender, EventArgs e) { this.business.ItemInsertPositionX = (int)this.numericUpDownItemInsertPositionX.Value; }
        protected virtual void numericUpDownItemInsertPositionY_ValueChanged(object sender, EventArgs e) { this.business.ItemInsertPositionY = (int)this.numericUpDownItemInsertPositionY.Value; }

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
            string filename = Helper.selectImageFile("select solution", string.Empty);
            if (filename != null) {
                int listIndex = this.listBoxDataList.SelectedIndex + 1;
                this.business.AddDataset(new DatasetContent(filename), listIndex);
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

        private void pictureBoxDatasetMovie_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                string filename = Helper.selectImageFile("select solution", this.selectedDataset.SolutionFilename);
                if (filename != null) this.selectedDataset.SolutionFilename = filename;
            }
        }
        private void textBoxDatasetItem1_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Item1 = this.textBoxDatasetItem1.Text; }
        private void textBoxDatasetItem2_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Item2 = this.textBoxDatasetItem2.Text; }
        private void textBoxDatasetItem3_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Item3 = this.textBoxDatasetItem3.Text; }
        private void textBoxDatasetItem4_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Item4 = this.textBoxDatasetItem4.Text; }
        private void textBoxDatasetItem5_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Item5 = this.textBoxDatasetItem5.Text; }
        private void textBoxDatasetItem6_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Item6 = this.textBoxDatasetItem6.Text; }
        private void textBoxDataSetName_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Name = this.textBoxDataSetName.Text; }
        private void textBoxDatasetAllItems_TextChanged(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                string[] itemArray = this.textBoxDatasetAllItems.Text.Split('\n');
                if (itemArray.Length >= 1) this.selectedDataset.Item1 = itemArray[0].Replace("\r", "");
                else this.selectedDataset.Item1 = string.Empty;
                if (itemArray.Length >= 2) this.selectedDataset.Item2 = itemArray[1].Replace("\r", "");
                else this.selectedDataset.Item2 = string.Empty;
                if (itemArray.Length >= 3) this.selectedDataset.Item3 = itemArray[2].Replace("\r", "");
                else this.selectedDataset.Item3 = string.Empty;
                if (itemArray.Length >= 4) this.selectedDataset.Item4 = itemArray[3].Replace("\r", "");
                else this.selectedDataset.Item4 = string.Empty;
                if (itemArray.Length >= 5) this.selectedDataset.Item5 = itemArray[4].Replace("\r", "");
                else this.selectedDataset.Item5 = string.Empty;
                if (itemArray.Length >= 6) this.selectedDataset.Item6 = itemArray[5].Replace("\r", "");
                else this.selectedDataset.Item6 = string.Empty;
            }
        }

        #endregion

    }
}
