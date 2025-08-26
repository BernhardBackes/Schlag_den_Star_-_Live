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

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MemoCourseCourt;
using System.Reflection;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.MemoCourseCourt {

    public partial class UserControlContent : _Base.UserControlContent {

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

            this.numericUpDownCounterPositionX.Minimum = int.MinValue;
            this.numericUpDownCounterPositionX.Maximum = int.MaxValue;

            this.numericUpDownCounterPositionY.Minimum = int.MinValue;
            this.numericUpDownCounterPositionY.Maximum = int.MaxValue;

            this.numericUpDownCoursePositionX.Minimum = int.MinValue;
            this.numericUpDownCoursePositionX.Maximum = int.MaxValue;

            this.numericUpDownCoursePositionY.Minimum = int.MinValue;
            this.numericUpDownCoursePositionY.Maximum = int.MaxValue;

            this.numericUpDownCourseScaling.Minimum = int.MinValue;
            this.numericUpDownCourseScaling.Maximum = int.MaxValue;

            this.numericUpDownTimerPositionX.Minimum = int.MinValue;
            this.numericUpDownTimerPositionX.Maximum = int.MaxValue;

            this.numericUpDownTimerPositionY.Minimum = int.MinValue;
            this.numericUpDownTimerPositionY.Maximum = int.MaxValue;

            this.numericUpDownTimerStartTime.Minimum = int.MinValue;
            this.numericUpDownTimerStartTime.Maximum = int.MaxValue;

            this.numericUpDownTimerStopTime.Minimum = int.MinValue;
            this.numericUpDownTimerStopTime.Maximum = int.MaxValue;

            this.numericUpDownTimerAlarmTime1.Minimum = int.MinValue;
            this.numericUpDownTimerAlarmTime1.Maximum = int.MaxValue;

            this.numericUpDownTimerAlarmTime2.Minimum = int.MinValue;
            this.numericUpDownTimerAlarmTime2.Maximum = int.MaxValue;

        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "CounterPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCounterPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "CounterPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCounterPositionY.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "CoursePositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCoursePositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "CoursePositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCoursePositionY.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "CourseScaling");
            bind.Format += (s, e) => { e.Value = (double)e.Value; };
            this.numericUpDownCourseScaling.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerPositionY.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerStartTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerStartTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerStartTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerStartTimeText.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerStopTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerStopTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerStopTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerStopTimeText.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerAlarmTime1");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerAlarmTime1.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerAlarmTime1");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerAlarmTime1Text.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerAlarmTime2");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerAlarmTime2.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerAlarmTime2");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerAlarmTime2Text.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "Filename");
            bind.Format += (s, e) => { e.Value = string.IsNullOrEmpty((string)e.Value) ? "no file loaded" : (string)e.Value; };
            this.labelFilename.DataBindings.Add(bind);

            this.labelGameClass.Text = this.business.ClassInfo;

            this.fillDataList();

            this.selectDataset(0);
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

            this.numericUpDownCounterPositionX.DataBindings.Clear();
            this.numericUpDownCounterPositionY.DataBindings.Clear();

            this.numericUpDownCoursePositionX.DataBindings.Clear();
            this.numericUpDownCoursePositionY.DataBindings.Clear();

            this.numericUpDownTimerPositionX.DataBindings.Clear();
            this.numericUpDownTimerPositionY.DataBindings.Clear();
            this.numericUpDownTimerStartTime.DataBindings.Clear();
            this.labelTimerStartTimeText.DataBindings.Clear();
            this.numericUpDownTimerStopTime.DataBindings.Clear();
            this.labelTimerStopTimeText.DataBindings.Clear();
            this.numericUpDownTimerAlarmTime1.DataBindings.Clear();
            this.labelTimerAlarmTime1Text.DataBindings.Clear();
            this.numericUpDownTimerAlarmTime2.DataBindings.Clear();
            this.labelTimerAlarmTime2Text.DataBindings.Clear();

            this.labelFilename.DataBindings.Clear();

        }

        private void fillDataList() {
            this.listBoxDataList.BeginUpdate();
            this.listBoxDataList.Items.Clear();
            int id = 0;
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
            if (this.listBoxDatasetItems.Enabled) this.listBoxDatasetItems.SelectedIndex = 0;
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

            this.adjustInvalidSize();

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
                this.buttonDatasetRemoveItem.Enabled = true;
                this.pictureBoxSelectedItem.Image = this.selectedDatasetItem.Image;
            }
            else {
                this.buttonDatasetRemoveItem.Enabled = false;
                this.pictureBoxSelectedItem.Image = null;
            }
            Helper.setControlBackColor(this.buttonDatasetRemoveItem);

            this.adjustDataItemMoveSet();
        }

        private void adjustDataItemMoveSet() {
            this.buttonDatasetMoveItemUp.Enabled = this.listBoxDatasetItems.SelectedIndex > 0;
            Helper.setControlBackColor(this.buttonDatasetMoveItemUp);

            this.buttonDatasetMoveItemDown.Enabled = this.listBoxDatasetItems.SelectedIndex >= 0 && this.listBoxDatasetItems.SelectedIndex < this.listBoxDatasetItems.Items.Count - 1;
            Helper.setControlBackColor(this.buttonDatasetMoveItemDown);
        }

        private void adjustInvalidSize() {
            if (this.selectedDataset is DatasetContent) this.labelDatasetInvalidSize.Visible = this.selectedDataset.InvalidSize;
            else this.labelDatasetInvalidSize.Visible = false;
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
                else if (e.PropertyName == "Name") {
                    this.textBoxDataSetName.Text = this.selectedDataset.Name;
                    this.adjustInvalidSize();
                }
            }
        }

        #endregion

        #region Events.Controls

        protected virtual void numericUpDownCounterPositionX_ValueChanged(object sender, EventArgs e) { this.business.CounterPositionX = (int)this.numericUpDownCounterPositionX.Value; }
        protected virtual void numericUpDownCounterPositionY_ValueChanged(object sender, EventArgs e) { this.business.CounterPositionY = (int)this.numericUpDownCounterPositionY.Value; }

        protected virtual void numericUpDownCoursePositionX_ValueChanged(object sender, EventArgs e) { this.business.CoursePositionX = (int)this.numericUpDownCoursePositionX.Value; }
        protected virtual void numericUpDownCoursePositionY_ValueChanged(object sender, EventArgs e) { this.business.CoursePositionY = (int)this.numericUpDownCoursePositionY.Value; }
        private void numericUpDownCourseScaling_ValueChanged(object sender, EventArgs e) { this.business.CourseScaling = (double)this.numericUpDownCourseScaling.Value; }

        protected virtual void numericUpDownTimerPositionX_ValueChanged(object sender, EventArgs e) { this.business.TimerPositionX = (int)this.numericUpDownTimerPositionX.Value; }
        protected virtual void numericUpDownTimerPositionY_ValueChanged(object sender, EventArgs e) { this.business.TimerPositionY = (int)this.numericUpDownTimerPositionY.Value; }
        protected virtual void numericUpDownTimerStartTime_ValueChanged(object sender, EventArgs e) { this.business.TimerStartTime = (int)this.numericUpDownTimerStartTime.Value; }
        protected virtual void numericUpDownTimerStopTime_ValueChanged(object sender, EventArgs e) { this.business.TimerStopTime = (int)this.numericUpDownTimerStopTime.Value; }
        protected virtual void numericUpDownTimerAlarmTime1_ValueChanged(object sender, EventArgs e) { this.business.TimerAlarmTime1 = (int)this.numericUpDownTimerAlarmTime1.Value; }
        protected virtual void numericUpDownTimerAlarmTime2_ValueChanged(object sender, EventArgs e) { this.business.TimerAlarmTime2 = (int)this.numericUpDownTimerAlarmTime2.Value; }

        private void buttonShowServer_Click(object sender, EventArgs e) { this.business.ShowServer(); }

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

        private void listBoxDatasetItems_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectDatasetItem(this.listBoxDatasetItems.SelectedIndex);
        }
        private void buttonDatasetMoveItemUp_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent && this.selectedDataset.TryMoveItemUp(this.selectedDatasetItemIndex)) this.listBoxDatasetItems.SelectedIndex--;
        }
        private void buttonDatasetMoveItemDown_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent && this.selectedDataset.TryMoveItemDown(this.selectedDatasetItemIndex)) this.listBoxDatasetItems.SelectedIndex++;
        }
        private void buttonDatasetAddNewItem_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                string filename = Helper.selectAudioFile("select image", string.Empty);
                if (!string.IsNullOrEmpty(filename)) this.selectedDataset.AddItem(new DatasetItem(filename));
            }
        }
        private void buttonDatasetRemoveItem_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.TryRemoveItem(this.selectedDatasetItemIndex);
        }
        private void buttonDatasetRemoveAllItems_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.RemoveAllItems();
        }


        #endregion

    }
}
