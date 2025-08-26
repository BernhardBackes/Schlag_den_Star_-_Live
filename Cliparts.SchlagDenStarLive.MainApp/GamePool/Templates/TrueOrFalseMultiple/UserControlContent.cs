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

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TrueOrFalseMultiple;
using Cliparts.Tools.Base;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TrueOrFalseMultiple {

    public partial class UserControlContent : _Base.Score.UserControlContent {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;
        private int selectedDatasetIndex = -1;

        private DatasetItem selectedDatasetItem = null;
        private int selectedDatasetItemIndex = -1;

        private Preview.Sources _previewSource = Preview.Sources.Insert;
        protected Preview.Sources previewSource
        {
            get { return this._previewSource; }
            set
            {
                if (this._previewSource != value)
                {
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

            this.numericUpDownGamePositionX.Minimum = int.MinValue;
            this.numericUpDownGamePositionX.Maximum = int.MaxValue;

            this.numericUpDownGamePositionY.Minimum = int.MinValue;
            this.numericUpDownGamePositionY.Maximum = int.MaxValue;

            this.numericUpDownCountDownDuration.Minimum = int.MinValue;
            this.numericUpDownCountDownDuration.Maximum = int.MaxValue;

            this.numericUpDownCounterPositionX.Minimum = int.MinValue;
            this.numericUpDownCounterPositionX.Maximum = int.MaxValue;

            this.numericUpDownCounterPositionY.Minimum = int.MinValue;
            this.numericUpDownCounterPositionY.Maximum = int.MaxValue;

            this.comboBoxCounterStyle.BeginUpdate();
            this.comboBoxCounterStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.Score.Styles)));
            this.comboBoxCounterStyle.EndUpdate();

            this.comboBoxDatasetItemSolution.BeginUpdate();
            this.comboBoxDatasetItemSolution.Items.AddRange(Enum.GetNames(typeof(SolutionValues)));
            this.comboBoxDatasetItemSolution.EndUpdate();

            this.comboBoxDatasetItemSelectionLeft.BeginUpdate();
            this.comboBoxDatasetItemSelectionLeft.Items.AddRange(Enum.GetNames(typeof(SelectionValues)));
            this.comboBoxDatasetItemSelectionLeft.EndUpdate();

            this.comboBoxDatasetItemSelectionRight.BeginUpdate();
            this.comboBoxDatasetItemSelectionRight.Items.AddRange(Enum.GetNames(typeof(SelectionValues)));
            this.comboBoxDatasetItemSelectionRight.EndUpdate();
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "GamePositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownGamePositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "GamePositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownGamePositionY.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "CountDownDuration");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCountDownDuration.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "CounterPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCounterPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "CounterPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCounterPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "CounterStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxCounterStyle.DataBindings.Add(bind);

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

            this.numericUpDownGamePositionX.DataBindings.Clear();
            this.numericUpDownGamePositionY.DataBindings.Clear();
            this.numericUpDownCountDownDuration.DataBindings.Clear();

            this.numericUpDownCounterPositionX.DataBindings.Clear();
            this.numericUpDownCounterPositionY.DataBindings.Clear();
            this.comboBoxCounterStyle.DataBindings.Clear();
            this.labelFilename.DataBindings.Clear();
            this.checkBoxDataSampleIncluded.DataBindings.Clear();
        }

        private void fillDataList()
        {
            this.listBoxDataList.BeginUpdate();
            this.listBoxDataList.Items.Clear();
            int id = 1;
            if (this.business.SampleIncluded) id = 0;
            foreach (string item in this.business.NameList)
            {
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
            int index)
        {
            if (index < 0) index = 0;
            if (index >= this.business.DatasetsCount) index = this.business.DatasetsCount - 1;
            DatasetContent selectedDataset = this.business.GetDataset(index);
            this.selectedDatasetIndex = this.business.GetDatasetIndex(selectedDataset);

            if (this.selectedDataset != selectedDataset)
            {
                //Dispose...
                if (this.selectedDataset is DatasetContent)
                {
                    this.selectedDataset.PropertyChanged -= this.selectedDataset_PropertyChanged;
                }
                this.selectedDataset = selectedDataset;
                //Pose...
                if (this.selectedDataset is DatasetContent)
                {
                    this.selectedDataset.PropertyChanged += this.selectedDataset_PropertyChanged;
                }
            }

            if (this.selectedDataset is DatasetContent)
            {
                this.groupBoxDataset.Enabled = true;
                this.textBoxDatasetName.Text = this.selectedDataset.Name;
                this.textBoxDatasetText.Text = this.selectedDataset.Text;
                this.textBoxDatasetHostText.Text = this.selectedDataset.HostText;
                this.buttonDataRemoveSet.Enabled = true;
                if (this.listBoxDataList.Items.Count > this.selectedDatasetIndex) this.listBoxDataList.SelectedIndex = this.selectedDatasetIndex;
            }
            else
            {
                this.groupBoxDataset.Enabled = false;
                this.textBoxDatasetName.Text = string.Empty;
                this.textBoxDatasetText.Text = string.Empty;
                this.textBoxDatasetHostText.Text = string.Empty;
                this.buttonDataRemoveSet.Enabled = false;
            }
            Helper.setControlBackColor(this.buttonDataRemoveSet);

            this.adjustDataMoveSet();

            this.fillListBoxDatasetItems();

            this.selectDatasetItem(0);

            this.setPreviewData();
        }

        private void adjustDataMoveSet()
        {
            this.buttonDataMoveSetUp.Enabled = this.listBoxDataList.SelectedIndex > 0;
            Helper.setControlBackColor(this.buttonDataMoveSetUp);

            this.buttonDataMoveSetDown.Enabled = this.listBoxDataList.SelectedIndex >= 0 && this.listBoxDataList.SelectedIndex < this.listBoxDataList.Items.Count - 1;
            Helper.setControlBackColor(this.buttonDataMoveSetDown);
        }

        private void fillListBoxDatasetItems()
        {
            this.listBoxDatasetItems.BeginUpdate();
            this.listBoxDatasetItems.Items.Clear();
            if (this.selectedDataset is DatasetContent) this.listBoxDatasetItems.Items.AddRange(this.selectedDataset.ItemList);
            this.listBoxDatasetItems.Enabled = this.listBoxDatasetItems.Items.Count > 0;
            Helper.setControlBackColor(this.listBoxDatasetItems);
            this.listBoxDatasetItems.EndUpdate();

            this.selectDatasetItem(this.selectedDatasetItemIndex);
        }

        private void selectDatasetItem(
            int index)
        {
            if (index < 0) index = 0;
            DatasetItem selectedDatasetItem = null;
            if (this.selectedDataset is DatasetContent)
            {
                if (index >= this.selectedDataset.ItemsCount) index = this.selectedDataset.ItemsCount - 1;
                selectedDatasetItem = this.selectedDataset.GetItem(index);
                this.selectedDatasetItemIndex = index;
            }

            if (this.selectedDatasetItem != selectedDatasetItem)
            {
                //Dispose...
                if (this.selectedDatasetItem is DatasetItem)
                {
                    this.selectedDatasetItem.PropertyChanged -= this.selectedDatasetItem_PropertyChanged;
                }
                this.selectedDatasetItem = selectedDatasetItem;
                //Pose...
                if (this.selectedDatasetItem is DatasetItem)
                {
                    this.selectedDatasetItem.PropertyChanged += this.selectedDatasetItem_PropertyChanged;
                }
            }

            if (this.selectedDatasetItem is DatasetItem)
            {
                this.groupBoxDatasetItem.Enabled = true;
                this.textBoxDatasetItemText.Text = this.selectedDatasetItem.Text;
                this.textBoxDatasetItemHostText.Text = this.selectedDatasetItem.HostText;
                this.comboBoxDatasetItemSolution.Text = this.selectedDatasetItem.Solution.ToString();
                this.comboBoxDatasetItemSelectionLeft.Text = this.selectedDatasetItem.SelectionLeft.ToString();
                this.comboBoxDatasetItemSelectionRight.Text = this.selectedDatasetItem.SelectionRight.ToString();
                this.buttonDatasetRemoveItem.Enabled = true;
                if (this.listBoxDatasetItems.Items.Count > this.selectedDatasetItemIndex) this.listBoxDatasetItems.SelectedIndex = this.selectedDatasetItemIndex;
            }
            else
            {
                this.groupBoxDatasetItem.Enabled = false;
                this.textBoxDatasetItemText.Text = string.Empty;
                this.textBoxDatasetItemHostText.Text = string.Empty;
                this.comboBoxDatasetItemSolution.Text = string.Empty;
                this.comboBoxDatasetItemSelectionLeft.Text = string.Empty;
                this.comboBoxDatasetItemSelectionRight.Text = string.Empty;
                this.buttonDatasetRemoveItem.Enabled = false;
            }
            Helper.setControlBackColor(this.buttonDatasetRemoveItem);

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
            Preview.Sources source)
        {
            if (this.previewSceneIsAvailable)
            {
                Preview previewScene = this.previewScene as Preview;
                previewScene.SetSource(source);
                this.setPreviewData();
            }
        }

        private void setPreviewData()
        {
            switch (this.previewSource)
            {
                case Preview.Sources.Insert:
                    this.setInsertPreview();
                    this.radioButtonSourceInsert.Checked = true;
                    break;
                case Preview.Sources.Host:
                    this.setHostPreview();
                    this.radioButtonSourceHost.Checked = true;
                    break;
                case Preview.Sources.Player:
                    this.setPlayerPreview();
                    this.radioButtonSourcePlayer.Checked = true;
                    break;
            }
        }

        protected void setInsertPreview()
        {
            if (this.previewSceneIsAvailable)
            {
                this.setScorePreview();
                this.setGamePreview();
                this.setCounterPreview();
            }
        }

        protected override void setScorePreview()
        {
            if (this.previewSceneIsAvailable)
            {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetScore(previewScene.Insert.Score, 2, 3);
                previewScene.Insert.Score.SetIn();
            }
        }

        protected void setGamePreview()
        {
            if (this.previewSceneIsAvailable)
            {
                Preview previewScene = this.previewScene as Preview;
                int size = Game.ItemsCount;
                if (this.selectedDataset is DatasetContent) size = this.selectedDataset.ItemsCount;
                this.business.Vinsert_SetGame(previewScene.Insert.Game, this.selectedDataset);
                this.business.Vinsert_SetItemsSelection(previewScene.Insert.Game, this.selectedDataset, size - 1);
                this.business.Vinsert_SetItemsSolution(previewScene.Insert.Game, this.selectedDataset, size - 1);
                if (size > 0) previewScene.Insert.Game.SetAnswerIn(size);
                else previewScene.Insert.Game.ResetAnswers();
                previewScene.Insert.Game.SetIn();
            }
        }

        protected void setCounterPreview()
        {
            if (this.previewSceneIsAvailable)
            {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetCounter(previewScene.Insert.Counter, 6, -9);
                previewScene.Insert.Counter.SetIn();
            }
        }

        protected void setHostPreview()
        {
            if (this.previewSceneIsAvailable)
            {
                Preview previewScene = this.previewScene as Preview;
                int size = Game.ItemsCount;
                if (this.selectedDataset is DatasetContent) size = this.selectedDataset.ItemsCount;
                this.business.Vhost_SetGame(previewScene.Host, this.selectedDataset);
                this.business.Vhost_SetItemsSelection(previewScene.Host, this.selectedDataset, size - 1);
                this.business.Vhost_SetItemsSolution(previewScene.Host, this.selectedDataset, size - 1);
                previewScene.Host.SetIn();
            }
        }

        protected void setPlayerPreview()
        {
            if (this.previewSceneIsAvailable)
            {
                Preview previewScene = this.previewScene as Preview;
                int size = Game.ItemsCount;
                if (this.selectedDataset is DatasetContent) size = this.selectedDataset.ItemsCount;
                this.business.Vplayer_SetGame(previewScene.Player, this.selectedDataset);
                this.business.Vplayer_SetItem(previewScene.Player, this.selectedDataset, size - 1);
                this.business.Vplayer_SetInput(previewScene.Player, this.selectedDataset, size - 1);
                this.business.Vplayer_SetSolution(previewScene.Player, this.selectedDataset, size - 1);
                previewScene.Player.SetIn();
            }
        }

        #endregion


        #region Events.Incoming

        void business_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else
            {
                if (e.PropertyName == "NameList") this.fillDataList();
                else if (e.PropertyName == "SampleIncluded") this.fillDataList();
            }
        }

        void selectedDataset_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.selectedDataset_PropertyChanged(sender, e)));
            else
            {
                if (e.PropertyName == "Name")
                {
                    this.textBoxDatasetName.Text = this.selectedDataset.Name;
                }
                else if (e.PropertyName == "Text")
                {
                    this.textBoxDatasetText.Text = this.selectedDataset.Text;
                    if (this.previewSource == Preview.Sources.Insert) this.setInsertPreview();
                    else if (this.previewSource == Preview.Sources.Player) this.setPlayerPreview();
                }
                else if (e.PropertyName == "HostText")
                {
                    this.textBoxDatasetHostText.Text = this.selectedDataset.HostText;
                    if (this.previewSource == Preview.Sources.Host) this.setHostPreview();
                }
                else if (e.PropertyName == "NameList") this.fillDataList();
                else if (e.PropertyName == "ItemList") this.fillListBoxDatasetItems();
            }
        }

        void selectedDatasetItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.selectedDatasetItem_PropertyChanged(sender, e)));
            else
            {
                if (e.PropertyName == "Text")
                {
                    this.textBoxDatasetItemText.Text = this.selectedDatasetItem.Text;
                    if (this.previewSource == Preview.Sources.Insert) this.setInsertPreview();
                    else if (this.previewSource == Preview.Sources.Player) this.setPlayerPreview();
                }
                else if (e.PropertyName == "HostText")
                {
                    this.textBoxDatasetItemHostText.Text = this.selectedDatasetItem.HostText;
                    if (this.previewSource == Preview.Sources.Host) this.setHostPreview();
                }
                else if (e.PropertyName == "Solution")
                {
                    this.comboBoxDatasetItemSolution.Text = this.selectedDatasetItem.Solution.ToString();
                    if (this.previewSource == Preview.Sources.Insert) this.setInsertPreview();
                    else if (this.previewSource == Preview.Sources.Host) this.setHostPreview();
                    else if (this.previewSource == Preview.Sources.Player) this.setPlayerPreview();
                }
                else if (e.PropertyName == "SelectionLeft")
                {
                    this.comboBoxDatasetItemSelectionLeft.Text = this.selectedDatasetItem.SelectionLeft.ToString();
                    if (this.previewSource == Preview.Sources.Insert) this.setInsertPreview();
                    else if (this.previewSource == Preview.Sources.Host) this.setHostPreview();
                    else if (this.previewSource == Preview.Sources.Player) this.setPlayerPreview();
                }
                else if (e.PropertyName == "SelectionRight")
                {
                    this.comboBoxDatasetItemSelectionRight.Text = this.selectedDatasetItem.SelectionRight.ToString();
                    if (this.previewSource == Preview.Sources.Insert) this.setInsertPreview();
                    else if (this.previewSource == Preview.Sources.Host) this.setHostPreview();
                    else if (this.previewSource == Preview.Sources.Player) this.setPlayerPreview();
                }
            }
        }

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e)
        {
            base.previewScene_StatusChanged(sender, e);
            this.setPreviewSource(this.previewSource);
        }

        #endregion

        #region Events.Controls

        private void numericUpDownGamePositionX_ValueChanged(object sender, EventArgs e)
        {
            this.business.GamePositionX = (int)this.numericUpDownGamePositionX.Value;
            if (this.previewSource == Preview.Sources.Insert) this.setGamePreview();
        }

        private void numericUpDownGamePositionY_ValueChanged(object sender, EventArgs e)
        {
            this.business.GamePositionY = (int)this.numericUpDownGamePositionY.Value;
            if (this.previewSource == Preview.Sources.Insert) this.setGamePreview();
        }

        private void numericUpDownCountDownDuration_ValueChanged(object sender, EventArgs e)
        {
            this.business.CountDownDuration = (int)this.numericUpDownCountDownDuration.Value;
        }

        protected virtual void numericUpDownCounterPositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.CounterPositionX = (int)this.numericUpDownCounterPositionX.Value;
                this.setCounterPreview();
            }
        }
        protected virtual void numericUpDownCounterPositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.CounterPositionY = (int)this.numericUpDownCounterPositionY.Value;
                this.setCounterPreview();
            }
        }
        protected virtual void comboBoxCounterStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.Score.Styles style;
            if (this.business is Business &&
                Enum.TryParse(this.comboBoxCounterStyle.Text, out style)) {
                this.business.CounterStyle = style;
                this.setCounterPreview();
            }
        }

        private void radioButtonSourceInsert_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceInsert.Checked) this.previewSource = Preview.Sources.Insert; }
        private void radioButtonSourceHost_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceHost.Checked) this.previewSource = Preview.Sources.Host; }
        private void radioButtonSourcePlayer_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourcePlayer.Checked) this.previewSource = Preview.Sources.Player; }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Load Data";
            dialog.InitialDirectory = ApplicationAttributes.ContentPath;
            if (File.Exists(this.business.Filename)) dialog.FileName = this.business.Filename;
            dialog.DefaultExt = "*.xml";
            dialog.Filter = "XML-File (*.xml)|*.xml|all files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.Multiselect = false;
            dialog.RestoreDirectory = true;
            switch (dialog.ShowDialog())
            {
                case DialogResult.Cancel:
                    break;
                case DialogResult.OK:
                    this.business.Load(dialog.FileName);
                    break;
            }
            dialog = null;

        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (File.Exists(this.business.Filename)) this.business.Save();
            else buttonSaveAs_Click(sender, e);
        }
        private void buttonSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Save Data As...";
            dialog.InitialDirectory = ApplicationAttributes.ContentPath;
            if (File.Exists(this.business.Filename)) dialog.FileName = this.business.Filename;
            dialog.DefaultExt = "*.xml";
            dialog.Filter = "XML-File (*.xml)|*.xml|all files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;
            switch (dialog.ShowDialog())
            {
                case DialogResult.Cancel:
                    break;
                case DialogResult.OK:
                    this.business.SaveAs(dialog.FileName);
                    break;
            }
            dialog = null;
        }

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.selectDataset(this.listBoxDataList.SelectedIndex); }
        private void buttonDataMoveSetUp_Click(object sender, EventArgs e)
        {
            if (this.business.TryMoveDatasetUp(this.selectedDatasetIndex)) this.selectDataset(this.selectedDatasetIndex - 1);
        }
        private void buttonDataMoveSetDown_Click(object sender, EventArgs e)
        {
            if (this.business.TryMoveDatasetDown(this.selectedDatasetIndex)) this.selectDataset(this.selectedDatasetIndex + 1);
        }
        private void buttonDataResort_Click(object sender, EventArgs e) { this.business.ResortAllDatasets(); }

        private void checkBoxSampleIncluded_CheckedChanged(object sender, EventArgs e) { this.business.SampleIncluded = this.checkBoxDataSampleIncluded.Checked; }
        private void buttonDataAddNewSet_Click(object sender, EventArgs e)
        {
            int listIndex = this.listBoxDataList.SelectedIndex + 1;
            this.business.AddDataset(new DatasetContent(), listIndex);
            this.selectDataset(listIndex);
        }
        private void buttonDataRemoveSet_Click(object sender, EventArgs e)
        {
            if (this.business.TryRemoveDataset(this.selectedDatasetIndex)) this.selectDataset(this.selectedDatasetIndex - 1);
        }
        private void buttonDataRemoveAllSets_Click(object sender, EventArgs e)
        {
            this.business.RemoveAllDatasets();
            this.selectDataset(0);
        }

        private void textBoxDatasetName_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Name = this.textBoxDatasetName.Text; }
        private void buttonDatasetReset_Click(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Reset(); }
        private void textBoxDatasetText_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Text = this.textBoxDatasetText.Text; }
        private void textBoxDatasetHostText_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.HostText = this.textBoxDatasetHostText.Text; }

        private void listBoxDatasetItems_SelectedIndexChanged(object sender, EventArgs e) { this.selectDatasetItem(this.listBoxDatasetItems.SelectedIndex); }
        private void buttonDataAddNewItem_Click(object sender, EventArgs e)
        {
            if (this.selectedDataset is DatasetContent)
            {
                this.selectedDataset.AddItem(new DatasetItem());
                this.selectDatasetItem(this.selectedDataset.ItemsCount - 1);
            }
        }
        private void buttonDataRemoveItem_Click(object sender, EventArgs e)
        {
            if (this.selectedDataset is DatasetContent
                && this.selectedDataset.TryRemoveItem(this.selectedDatasetItemIndex)) this.selectDatasetItem(this.selectedDatasetItemIndex);
        }
        private void textBoxDatasetItemText_TextChanged(object sender, EventArgs e) { if (this.selectedDatasetItem is DatasetItem) this.selectedDatasetItem.Text = this.textBoxDatasetItemText.Text; }
        private void textBoxDatasetItemHostText_TextChanged(object sender, EventArgs e) { if (this.selectedDatasetItem is DatasetItem) this.selectedDatasetItem.HostText = this.textBoxDatasetItemHostText.Text; }
        private void comboBoxDatasetItemSolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            SolutionValues result;
            if (this.selectedDatasetItem is DatasetItem &&
                Enum.TryParse(this.comboBoxDatasetItemSolution.Text, out result)) this.selectedDatasetItem.Solution = result;
        }
        private void comboBoxDatasetItemSelectionLeft_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectionValues result;
            if (this.selectedDatasetItem is DatasetItem &&
                Enum.TryParse(this.comboBoxDatasetItemSelectionLeft.Text, out result)) this.selectedDatasetItem.SelectionLeft = result;
        }
        private void comboBoxDatasetItemSelectionRight_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectionValues result;
            if (this.selectedDatasetItem is DatasetItem &&
                Enum.TryParse(this.comboBoxDatasetItemSelectionRight.Text, out result)) this.selectedDatasetItem.SelectionRight = result;
        }
        private void buttonDatasetItemReset_Click(object sender, EventArgs e) { if (this.selectedDatasetItem is DatasetItem) this.selectedDatasetItem.Reset(); }

        #endregion

    }
}
