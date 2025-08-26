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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.Remembory;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.Remembory {

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

        private bool _showContentInsert = false;
        public bool showContentInsert {
            get { return this._showContentInsert; }
            set {
                if (this._showContentInsert != value) {
                    this._showContentInsert = value;
                    this.setPreviewData();
                }
            }
        }

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownLeftPlayerBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownRightPlayerBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownBorderPositionX.Minimum = int.MinValue;
            this.numericUpDownBorderPositionX.Maximum = int.MaxValue;

            this.numericUpDownBorderPositionY.Minimum = int.MinValue;
            this.numericUpDownBorderPositionY.Maximum = int.MaxValue;

            this.comboBoxBorderStyle.BeginUpdate();
            this.comboBoxBorderStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.Border.Styles)));
            this.comboBoxBorderStyle.EndUpdate();

            this.numericUpDownInsertPositionX.Minimum = int.MinValue;
            this.numericUpDownInsertPositionX.Maximum = int.MaxValue;

            this.numericUpDownInsertPositionY.Minimum = int.MinValue;
            this.numericUpDownInsertPositionY.Maximum = int.MaxValue;

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

            bind = new Binding("Text", this.business, "BorderStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxBorderStyle.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "InsertPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownInsertPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "InsertPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownInsertPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "IOUnitName");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxIOUnit.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitStatus");
            bind.Format += this.bind_comboBoxIOUnitName_BackColor;
            this.comboBoxIOUnit.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerBuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerBuzzerChannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerBuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerBuzzerChannel.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "Filename");
            bind.Format += (s, e) => { e.Value = string.IsNullOrEmpty((string)e.Value) ? "no file loaded" : (string)e.Value; };
            this.labelFilename.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "SampleIncluded");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxDataSampleIncluded.DataBindings.Add(bind);

            this.labelGameClass.Text = this.business.ClassInfo;

            this.fillIOUnitList();

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
            this.checkBoxDataSampleIncluded.DataBindings.Clear();
        }

        private void fillIOUnitList() {
            this.comboBoxIOUnit.BeginUpdate();
            this.comboBoxIOUnit.Items.Clear();
            this.comboBoxIOUnit.Items.AddRange(this.business.IOUnitNameList);
            this.comboBoxIOUnit.EndUpdate();
        }

        private void bind_comboBoxIOUnitName_BackColor(object sender, ConvertEventArgs e) {
            switch ((BuzzerUnitStates)e.Value) {
                case BuzzerUnitStates.NotAvailable:
                    e.Value = Constants.ColorMissing;
                    break;
                case BuzzerUnitStates.Missing:
                    e.Value = Constants.ColorMissing;
                    break;
                case BuzzerUnitStates.Disconnected:
                    e.Value = Constants.ColorDisabled;
                    break;
                case BuzzerUnitStates.Connecting:
                    e.Value = Constants.ColorEnabling;
                    break;
                case BuzzerUnitStates.Connected:
                    e.Value = SystemColors.Control;
                    break;
                case BuzzerUnitStates.Locked:
                    e.Value = SystemColors.Control;
                    break;
                case BuzzerUnitStates.BuzzerMode:
                    e.Value = Constants.ColorEnabled;
                    break;
                case BuzzerUnitStates.EventMode:
                    e.Value = Constants.ColorBuzzered;
                    break;
                default:
                    break;
            }
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
                this.pictureBoxDatasetPicture.Image = this.selectedDataset.Picture;
                this.textBoxDatasetName.Text = this.selectedDataset.Name;
                this.pictureBoxImage_01.Image = this.selectedDataset.Image1;
                this.pictureBoxImage_02.Image = this.selectedDataset.Image2;
                this.pictureBoxImage_03.Image = this.selectedDataset.Image3;
                this.pictureBoxImage_04.Image = this.selectedDataset.Image4;
                this.pictureBoxImage_05.Image = this.selectedDataset.Image5;
                this.pictureBoxImage_06.Image = this.selectedDataset.Image6;
                this.pictureBoxImage_07.Image = this.selectedDataset.Image7;
                this.pictureBoxImage_08.Image = this.selectedDataset.Image8;
                this.textBoxImage_01.Text = this.selectedDataset.Image1Text;
                this.textBoxImage_02.Text = this.selectedDataset.Image2Text;
                this.textBoxImage_03.Text = this.selectedDataset.Image3Text;
                this.textBoxImage_04.Text = this.selectedDataset.Image4Text;
                this.textBoxImage_05.Text = this.selectedDataset.Image5Text;
                this.textBoxImage_06.Text = this.selectedDataset.Image6Text;
                this.textBoxImage_07.Text = this.selectedDataset.Image7Text;
                this.textBoxImage_08.Text = this.selectedDataset.Image8Text;
                this.buttonDataRemoveSet.Enabled = true;
                if (this.listBoxDataList.Items.Count > this.selectedDatasetIndex) this.listBoxDataList.SelectedIndex = this.selectedDatasetIndex;
            }
            else {
                this.groupBoxDataset.Enabled = false;
                this.pictureBoxDatasetPicture.Image = null;
                this.textBoxDatasetName.Text = string.Empty;
                this.pictureBoxImage_01.Image = null;
                this.pictureBoxImage_02.Image = null;
                this.pictureBoxImage_03.Image = null;
                this.pictureBoxImage_04.Image = null;
                this.pictureBoxImage_05.Image = null;
                this.pictureBoxImage_06.Image = null;
                this.pictureBoxImage_07.Image = null;
                this.pictureBoxImage_08.Image = null;
                this.textBoxImage_01.Text = string.Empty;
                this.textBoxImage_02.Text = string.Empty;
                this.textBoxImage_03.Text = string.Empty;
                this.textBoxImage_04.Text = string.Empty;
                this.textBoxImage_05.Text = string.Empty;
                this.textBoxImage_06.Text = string.Empty;
                this.textBoxImage_07.Text = string.Empty;
                this.textBoxImage_08.Text = string.Empty;
                this.buttonDataRemoveSet.Enabled = false;
            }
            Helper.setControlBackColor(this.buttonDataRemoveSet);

            this.adjustDataMoveSet();

            this.setPreviewData();
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
                    if (this.showContentInsert) this.radioButtonSourceContent.Checked = true;
                    else this.radioButtonSourceScore.Checked = true;
                    break;
                case Preview.Sources.Fullscreen:
                    this.setFullscreenPreview();
                    this.radioButtonSourceFullscreen.Checked = true;
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

        protected void setInsertPreview() {
            if (this.previewSceneIsAvailable) {
                this.setScorePreview();
                this.setTimerPreview();
                this.setInsertContentPreview();
            }
        }
        protected override void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.previewSource == Preview.Sources.Insert &&
                    !this.showContentInsert) {
                    this.business.Vinsert_SetScore(previewScene.Insert.Score, 2, 3);
                    previewScene.Insert.Score.SetIn();
                }
                else previewScene.Insert.Score.SetOut();
                previewScene.Insert.Border.SetOut();
            }
        }
        protected override void setTimerPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.previewSource == Preview.Sources.Insert &&
                    this.showContentInsert) {
                    this.business.Vinsert_SetTimer(previewScene.Insert.Timer);
                    if (this.previewSource == Preview.Sources.Insert) previewScene.Insert.Timer.SetIn();
                }
                else previewScene.Insert.Timer.SetOut();
            }
        }
        protected virtual void setInsertContentPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                List<string> imagesFilename = new List<string>();
                if (this.previewSource == Preview.Sources.Insert &&
                    this.showContentInsert) {
                    if (this.selectedDataset is DatasetContent) {
                        imagesFilename.Add(this.selectedDataset.Image1Filename);
                        imagesFilename.Add(this.selectedDataset.Image2Filename);
                        imagesFilename.Add(this.selectedDataset.Image3Filename);
                        imagesFilename.Add(this.selectedDataset.Image4Filename);
                        imagesFilename.Add(this.selectedDataset.Image5Filename);
                        imagesFilename.Add(this.selectedDataset.Image6Filename);
                        imagesFilename.Add(this.selectedDataset.Image7Filename);
                        imagesFilename.Add(this.selectedDataset.Image8Filename);
                        this.business.Vinsert_SetContent(previewScene.Insert, this.selectedDataset.ImagesCount, imagesFilename);
                    }
                    else this.business.Vinsert_SetContent(previewScene.Insert, 0, imagesFilename);
                    previewScene.Insert.SetImagesIn();
                }
                else previewScene.Insert.SetImagesOut();       
            }
        }

        protected void setFullscreenPreview() {
            if (this.previewSceneIsAvailable) {
                this.setBorderPreview();
                this.setTimerPreview();
                this.setInsertContentPreview();
                this.setFullscreenContentPreview();
            }
        }
        protected virtual void setBorderPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetBorder(previewScene.Insert.Border, 2, 3);
                if (this.previewSource == Preview.Sources.Fullscreen) {
                    previewScene.Insert.Score.SetOut();
                    previewScene.Insert.Border.SetIn();
                }
            }
        }
        protected virtual void setFullscreenContentPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.selectedDataset is DatasetContent) this.business.Vfullscreen_SetContent(previewScene.Fullscreen, this.selectedDataset.PictureFilename);
                else this.business.Vfullscreen_SetContent(previewScene.Fullscreen, string.Empty);
                previewScene.Fullscreen.Deselect();
                previewScene.Fullscreen.ToIn();
            }
        }

        protected void setHostPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                List<bool> resolved = new List<bool>();
                resolved.Add(false);
                resolved.Add(true);
                this.business.Vhost_Set(previewScene.Host, this.selectedDataset, resolved.ToArray());
                previewScene.Host.SetIn();
            }
        }

        protected void setPlayerPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vplayers_SetTimer(previewScene.Player.Timer, this.business.TimerStartTime);
                this.business.Vplayers_TimerIn(previewScene.Player.Timer);
                List<string> imagesFilename = new List<string>();
                if (this.selectedDataset is DatasetContent) {
                    imagesFilename.Add(this.selectedDataset.Image1Filename);
                    imagesFilename.Add(this.selectedDataset.Image2Filename);
                    imagesFilename.Add(this.selectedDataset.Image3Filename);
                    imagesFilename.Add(this.selectedDataset.Image4Filename);
                    imagesFilename.Add(this.selectedDataset.Image5Filename);
                    imagesFilename.Add(this.selectedDataset.Image6Filename);
                    imagesFilename.Add(this.selectedDataset.Image7Filename);
                    imagesFilename.Add(this.selectedDataset.Image8Filename);
                    this.business.Vplayers_SetContent(previewScene.Player, this.selectedDataset.ImagesCount, imagesFilename);
                }
                else this.business.Vplayers_SetContent(previewScene.Player, 0, imagesFilename);
                previewScene.Player.SetImagesIn();
            }
        }

        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setPreviewSource(this.previewSource);
        }

        void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "IOUnitNameList") this.fillIOUnitList();
                else if (e.PropertyName == "NameList") this.fillDataList();
                else if (e.PropertyName == "SampleIncluded") this.fillDataList();
            }
        }

        void selectedDataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.selectedDataset_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "PictureFilename") {
                    this.pictureBoxDatasetPicture.Image = this.selectedDataset.Picture;
                    if (this.previewSource == Preview.Sources.Fullscreen) this.setFullscreenContentPreview();
                }
                else {
                    if (e.PropertyName == "Name") this.textBoxDatasetName.Text = this.selectedDataset.Name;
                    else if (e.PropertyName == "Image1Filename") this.pictureBoxImage_01.Image = this.selectedDataset.Image1;
                    else if (e.PropertyName == "Image2Filename") this.pictureBoxImage_02.Image = this.selectedDataset.Image2;
                    else if (e.PropertyName == "Image3Filename") this.pictureBoxImage_03.Image = this.selectedDataset.Image3;
                    else if (e.PropertyName == "Image4Filename") this.pictureBoxImage_04.Image = this.selectedDataset.Image4;
                    else if (e.PropertyName == "Image5Filename") this.pictureBoxImage_05.Image = this.selectedDataset.Image5;
                    else if (e.PropertyName == "Image6Filename") this.pictureBoxImage_06.Image = this.selectedDataset.Image6;
                    else if (e.PropertyName == "Image7Filename") this.pictureBoxImage_07.Image = this.selectedDataset.Image7;
                    else if (e.PropertyName == "Image8Filename") this.pictureBoxImage_08.Image = this.selectedDataset.Image8;
                    else if (e.PropertyName == "Image1Text") this.textBoxImage_01.Text = this.selectedDataset.Image1Text;
                    else if (e.PropertyName == "Image2Text") this.textBoxImage_02.Text = this.selectedDataset.Image2Text;
                    else if (e.PropertyName == "Image3Text") this.textBoxImage_03.Text = this.selectedDataset.Image3Text;
                    else if (e.PropertyName == "Image4Text") this.textBoxImage_04.Text = this.selectedDataset.Image4Text;
                    else if (e.PropertyName == "Image5Text") this.textBoxImage_05.Text = this.selectedDataset.Image5Text;
                    else if (e.PropertyName == "Image6Text") this.textBoxImage_06.Text = this.selectedDataset.Image6Text;
                    else if (e.PropertyName == "Image7Text") this.textBoxImage_07.Text = this.selectedDataset.Image7Text;
                    else if (e.PropertyName == "Image8Text") this.textBoxImage_08.Text = this.selectedDataset.Image8Text;
                    switch (this.previewSource) {
                        case Preview.Sources.Insert:
                            this.setInsertContentPreview();
                            break;
                        case Preview.Sources.Fullscreen:
                            break;
                        case Preview.Sources.Host:
                            this.setHostPreview();
                            break;
                        case Preview.Sources.Player:
                            this.setPlayerPreview();
                            break;
                    }
                }
            }
        }

        #endregion

        #region Events.Controls

        private void numericUpDownBorderPositionX_ValueChanged(object sender, EventArgs e) { 
            this.business.BorderPositionX = (int)this.numericUpDownBorderPositionX.Value;
            if (this.previewSource == Preview.Sources.Fullscreen) this.setBorderPreview();
        }
        private void numericUpDownBorderPositionY_ValueChanged(object sender, EventArgs e) { 
            this.business.BorderPositionY = (int)this.numericUpDownBorderPositionY.Value;
            if (this.previewSource == Preview.Sources.Fullscreen) this.setBorderPreview();
        }
        private void comboBoxBorderStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.Border.Styles style;
            if (Enum.TryParse(this.comboBoxBorderStyle.Text, out style)) this.business.BorderStyle = style;
            if (this.previewSource == Preview.Sources.Fullscreen) this.setBorderPreview();
        }

        protected virtual void numericUpDownInsertPositionX_ValueChanged(object sender, EventArgs e) { this.business.InsertPositionX = (int)this.numericUpDownInsertPositionX.Value; }
        protected virtual void numericUpDownInsertPositionY_ValueChanged(object sender, EventArgs e) { this.business.InsertPositionY = (int)this.numericUpDownInsertPositionY.Value; }

        private void radioButtonSourceScore_CheckedChanged(object sender, EventArgs e) {
            if (this.radioButtonSourceScore.Checked) {
                this.previewSource = Preview.Sources.Insert;
                this.showContentInsert = false;
            }
        }
        private void radioButtonSourceContent_CheckedChanged(object sender, EventArgs e) {
            if (this.radioButtonSourceContent.Checked) {
                this.previewSource = Preview.Sources.Insert;
                this.showContentInsert = true;
            }
        }
        private void radioButtonSourceFullscreen_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceFullscreen.Checked) this.previewSource = Preview.Sources.Fullscreen; }
        private void radioButtonSourceHost_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceHost.Checked) this.previewSource = Preview.Sources.Host; }
        private void radioButtonSourcePlayer_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourcePlayer.Checked) this.previewSource = Preview.Sources.Player; }

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

        private void pictureBoxDatasetTask_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                string filename = Helper.selectImageFile("select picture", this.selectedDataset.PictureFilename);
                if (filename != null) this.selectedDataset.PictureFilename = filename;
            }
        }
        private void textBoxDataSetName_TextChanged(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.Name = this.textBoxDatasetName.Text;
        }
        private void buttonSelectAllImages_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                List<string> filenames = Helper.selectImageFilelist("select all images");
                this.selectedDataset.SetAllImages(filenames);
            }
        }

        private void pictureBoxImage_01_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                string filename = Helper.selectImageFile("select image 1", this.selectedDataset.PictureFilename);
                if (filename != null) this.selectedDataset.Image1Filename = filename;
            }
        }
        private void textBoxImage_01_TextChanged(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.Image1Text = this.textBoxImage_01.Text;
        }
        private void pictureBoxImage_02_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                string filename = Helper.selectImageFile("select image 2", this.selectedDataset.PictureFilename);
                if (filename != null) this.selectedDataset.Image2Filename = filename;
            }
        }
        private void textBoxImage_02_TextChanged(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.Image2Text = this.textBoxImage_02.Text;
        }
        private void pictureBoxImage_03_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                string filename = Helper.selectImageFile("select image 3", this.selectedDataset.PictureFilename);
                if (filename != null) this.selectedDataset.Image3Filename = filename;
            }
        }
        private void textBoxImage_03_TextChanged(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.Image3Text = this.textBoxImage_03.Text;
        }
        private void pictureBoxImage_04_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                string filename = Helper.selectImageFile("select image 4", this.selectedDataset.PictureFilename);
                if (filename != null) this.selectedDataset.Image4Filename = filename;
            }
        }
        private void textBoxImage_04_TextChanged(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.Image4Text = this.textBoxImage_04.Text;
        }
        private void pictureBoxImage_05_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                string filename = Helper.selectImageFile("select image 5", this.selectedDataset.PictureFilename);
                if (filename != null) this.selectedDataset.Image5Filename = filename;
            }
        }
        private void textBoxImage_05_TextChanged(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.Image5Text = this.textBoxImage_05.Text;
        }
        private void pictureBoxImage_06_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                string filename = Helper.selectImageFile("select image 6", this.selectedDataset.PictureFilename);
                if (filename != null) this.selectedDataset.Image6Filename = filename;
            }
        }
        private void textBoxImage_06_TextChanged(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.Image6Text = this.textBoxImage_06.Text;
        }
        private void pictureBoxImage_07_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                string filename = Helper.selectImageFile("select image 7", this.selectedDataset.PictureFilename);
                if (filename != null) this.selectedDataset.Image7Filename = filename;
            }
        }
        private void textBoxImage_07_TextChanged(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.Image7Text = this.textBoxImage_07.Text;
        }
        private void pictureBoxImage_08_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                string filename = Helper.selectImageFile("select image 8", this.selectedDataset.PictureFilename);
                if (filename != null) this.selectedDataset.Image8Filename = filename;
            }

        }
        private void textBoxImage_08_TextChanged(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.Image8Text = this.textBoxImage_08.Text;
        }

        #endregion

    }
}
