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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MovieQuestionsBuzzerScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.MovieQuestionsBuzzerScore {

    public partial class UserControlContent : _Base.BuzzerScore.UserControlContent {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        private int selectedDatasetIndex = -1;

        private DatasetQuestion selectedQuestion = null;

        private int selectedQuestionIndex = -1;

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

        private bool _showScore = false;
        protected bool showScore {
            get { return this._showScore; }
            set {
                if (this._showScore != value) {
                    this._showScore = value;
                    if (this.previewSource == Preview.Sources.Insert) this.setPreviewData();
                }
            }
        }

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownCounterPositionX.Minimum = int.MinValue;
            this.numericUpDownCounterPositionX.Maximum = int.MaxValue;

            this.numericUpDownCounterPositionY.Minimum = int.MinValue;
            this.numericUpDownCounterPositionY.Maximum = int.MaxValue;

            this.comboBoxCounterStyle.BeginUpdate();
            this.comboBoxCounterStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.Score.Styles)));
            this.comboBoxCounterStyle.EndUpdate();

            this.numericUpDownTextInsertPositionX.Minimum = int.MinValue;
            this.numericUpDownTextInsertPositionX.Maximum = int.MaxValue;

            this.numericUpDownTextInsertPositionY.Minimum = int.MinValue;
            this.numericUpDownTextInsertPositionY.Maximum = int.MaxValue;

        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "CounterPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCounterPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "CounterPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCounterPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "CounterStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxCounterStyle.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TextInsertPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTextInsertPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TextInsertPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTextInsertPositionY.DataBindings.Add(bind);

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

            this.numericUpDownCounterPositionX.DataBindings.Clear();
            this.numericUpDownCounterPositionY.DataBindings.Clear();
            this.comboBoxCounterStyle.DataBindings.Clear();

            this.numericUpDownTextInsertPositionX.DataBindings.Clear();
            this.numericUpDownTextInsertPositionY.DataBindings.Clear();

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
                this.pictureBoxDatasetMovie.Image = this.selectedDataset.Movie;
                this.textBoxDataSetName.Text = this.selectedDataset.Name;
                this.buttonDataRemoveSet.Enabled = true;
                if (this.listBoxDataList.Items.Count > this.selectedDatasetIndex) this.listBoxDataList.SelectedIndex = this.selectedDatasetIndex;
            }
            else {
                this.groupBoxDataset.Enabled = false;
                this.pictureBoxDatasetMovie.Image = null;
                this.textBoxDataSetName.Text = string.Empty;
                this.buttonDataRemoveSet.Enabled = false;
            }
            Helper.setControlBackColor(this.buttonDataRemoveSet);

            this.adjustDataMoveSet();

            this.fillDataQuestionList();

            this.selectQuestion(0);
        }

        private void adjustDataMoveSet() {
            this.buttonDataMoveSetUp.Enabled = this.listBoxDataList.SelectedIndex > 0;
            Helper.setControlBackColor(this.buttonDataMoveSetUp);

            this.buttonDataMoveSetDown.Enabled = this.listBoxDataList.SelectedIndex >= 0 && this.listBoxDataList.SelectedIndex < this.listBoxDataList.Items.Count - 1;
            Helper.setControlBackColor(this.buttonDataMoveSetDown);
        }

        private void fillDataQuestionList() {
            int selectedIndex = this.listBoxDataQuestionList.SelectedIndex;
            this.listBoxDataQuestionList.BeginUpdate();
            this.listBoxDataQuestionList.Items.Clear();
            int id = 1;
            if (this.selectedDataset is DatasetContent) {
                foreach (DatasetQuestion item in this.selectedDataset.QuestionList) {
                    this.listBoxDataQuestionList.Items.Add(string.Format("{0}: {1}", id.ToString("00"), item));
                    id++;
                }
            }
            this.listBoxDataQuestionList.EndUpdate();

            this.listBoxDataQuestionList.Enabled = this.listBoxDataQuestionList.Items.Count > 0;
            Helper.setControlBackColor(this.listBoxDataQuestionList);

            this.buttonDataRemoveAllQuestions.Enabled = this.listBoxDataQuestionList.Items.Count > 0;
            Helper.setControlBackColor(this.buttonDataRemoveAllQuestions);

            if (this.listBoxDataQuestionList.Enabled &&
                selectedIndex >= 0 &&
                selectedIndex < this.listBoxDataQuestionList.Items.Count) this.listBoxDataQuestionList.SelectedIndex = selectedIndex;
        }

        private void selectQuestion(
            int index) {
            DatasetQuestion selectedQuestion = null;
            if (this.selectedDataset is DatasetContent) {
                if (index < 0) index = 0;
                if (index >= this.selectedDataset.QuestionsCount) index = this.selectedDataset.QuestionsCount - 1;
                selectedQuestion = this.selectedDataset.GetQuestion(index);
                this.selectedQuestionIndex = this.selectedDataset.GetQuestionIndex(selectedQuestion);
            }
            else this.selectedQuestionIndex = -1;

            if (this.selectedQuestion != selectedQuestion) {
                //Dispose...
                if (this.selectedQuestion is DatasetQuestion) {
                    this.selectedQuestion.PropertyChanged -= this.selectedQuestion_PropertyChanged;
                }
                this.selectedQuestion = selectedQuestion;
                //Pose...
                if (this.selectedQuestion is DatasetQuestion) {
                    this.selectedQuestion.PropertyChanged += this.selectedQuestion_PropertyChanged;
                }
            }

            if (this.selectedQuestion is DatasetQuestion) {
                this.groupBoxDataQuestion.Enabled = true;
                this.textBoxDataQuestionText.Text = this.selectedQuestion.Text;
                this.textBoxDataQuestionAnswer.Text = this.selectedQuestion.Answer;
                this.buttonDataRemoveQuestion.Enabled = true;
                if (this.listBoxDataQuestionList.Items.Count > this.selectedQuestionIndex) this.listBoxDataQuestionList.SelectedIndex = this.selectedQuestionIndex;
            }
            else {
                this.groupBoxDataQuestion.Enabled = false;
                this.textBoxDataQuestionText.Text = string.Empty;
                this.textBoxDataQuestionAnswer.Text = string.Empty;
                this.buttonDataRemoveQuestion.Enabled = false;
            }
            Helper.setControlBackColor(this.buttonDataRemoveQuestion);

            this.adjustQuestionMoveSet();

            this.setPreviewData();
        }

        private void adjustQuestionMoveSet() {
            this.buttonDataMoveQuestionUp.Enabled = this.listBoxDataQuestionList.SelectedIndex > 0;
            Helper.setControlBackColor(this.buttonDataMoveQuestionUp);

            this.buttonDataMoveQuestionDown.Enabled = this.listBoxDataQuestionList.SelectedIndex >= 0 && this.listBoxDataQuestionList.SelectedIndex < this.listBoxDataQuestionList.Items.Count - 1;
            Helper.setControlBackColor(this.buttonDataMoveQuestionDown);
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
                    if (this.showScore) this.radioButtonSourceScore.Checked = true;
                    else this.radioButtonSourceInsert.Checked = true;
                    break;
                case Preview.Sources.Fullscreen:
                    this.setFullscreenPreview();
                    this.radioButtonSourceFullscreen.Checked = true;
                    break;
                case Preview.Sources.Stage:
                    this.setStagePreview();
                    this.radioButtonSourceStage.Checked = true;
                    break;
            }
        }

        protected void setInsertPreview() {
            if (this.previewSceneIsAvailable) {
                this.setScorePreview();
                this.setTextPreview();
                this.setCounterPreview();
                this.setTimeoutPreview();
            }
        }

        protected void setCounterPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.showScore) previewScene.Insert.Counter.SetOut();
                else {
                    this.business.Vinsert_SetCounter(previewScene.Insert.Counter, 2, 1);
                    previewScene.Insert.Counter.SetIn();
                }
            }
        }

        protected void setTextPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.showScore) previewScene.Insert.TextInsert.SetOut();
                else if (this.selectedQuestion is DatasetQuestion) {
                    this.business.Vinsert_SetTextInsert(
                        previewScene.Insert.TextInsert,
                        this.selectedQuestion.Text,
                        this.selectedQuestion.Answer);
                    previewScene.Insert.TextInsert.SetSolutionIn();
                }
            }
        }

        protected override void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;                
                if (this.previewSource == Preview.Sources.Insert &&
                    this.showScore) {
                    this.business.Vinsert_SetScore(previewScene.Insert.Score, 2, 3);
                    previewScene.Insert.Score.SetIn();
                }
                else previewScene.Insert.Score.SetOut();
            }
        }

        protected override void setTimeoutPreview() {
            base.setTimeoutPreview();
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetTimeout(previewScene.Insert.Timeout);
                if (this.previewSource == Preview.Sources.Insert) previewScene.Insert.Timeout.SetLeftRightToGreen(this.business.TimeoutDuration);
                else previewScene.Insert.Timeout.Reset();
            }
        }

        protected void setFullscreenPreview() {
            if (this.previewSceneIsAvailable) {
                this.setMoviePreview();
            }
        }

        protected void setMoviePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.selectedDataset is DatasetContent) {
                    this.business.Vfullscreen_SetContent(previewScene.Fullscreen, this.selectedDataset.MovieFilename);
                    previewScene.Fullscreen.SetIn();
                    previewScene.Fullscreen.Start();
                }
            }
        }

        protected void setStagePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.selectedDataset is DatasetContent &&
                    this.selectedQuestion is DatasetQuestion) {
                    this.business.Vhost_Set(
                        previewScene.Stage, 
                        this.selectedQuestionIndex + 1,
                        this.selectedDataset.QuestionsCount,
                        this.selectedQuestion.Text, 
                        this.selectedQuestion.Answer);
                    previewScene.Stage.SetIn();
                }
                else previewScene.Stage.SetOut();
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
                if (e.PropertyName == "PictureFilename") {
                    this.pictureBoxDatasetMovie.Image = this.selectedDataset.Movie;
                    if (this.previewSource == Preview.Sources.Fullscreen) this.setMoviePreview();
                }
                else if (e.PropertyName == "QuestionList" ||
                    e.PropertyName == "Text") this.fillDataQuestionList();
            }
        }

        void selectedQuestion_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.selectedQuestion_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "Text") this.textBoxDataQuestionText.Text = this.selectedQuestion.Text;
                else if (e.PropertyName == "Answer") this.textBoxDataQuestionAnswer.Text = this.selectedQuestion.Answer;
                if (this.previewSource == Preview.Sources.Insert &&
                    !this.showScore) this.setInsertPreview();
            }
        }

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setPreviewSource(this.previewSource);
        }

        #endregion

        #region Events.Controls

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
        protected virtual void numericUpDownTextInsertPositionX_ValueChanged(object sender, EventArgs e) {
            this.business.TextInsertPositionX = (int)this.numericUpDownTextInsertPositionX.Value;
            this.setTextPreview();
        }
        protected virtual void numericUpDownTextInsertPositionY_ValueChanged(object sender, EventArgs e) {
            this.business.TextInsertPositionY = (int)this.numericUpDownTextInsertPositionY.Value;
            this.setTextPreview();
        }

        private void radioButtonSourceInsert_CheckedChanged(object sender, EventArgs e) {
            if (this.radioButtonSourceInsert.Checked) {
                this.showScore = false;
                this.previewSource = Preview.Sources.Insert;
            }
        }
        private void radioButtonSourceScore_CheckedChanged(object sender, EventArgs e) {
            if (this.radioButtonSourceScore.Checked) {
                this.showScore = true;
                this.previewSource = Preview.Sources.Insert;
            }
        }
        private void radioButtonSourceFullscreen_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceFullscreen.Checked) this.previewSource = Preview.Sources.Fullscreen; }
        private void radioButtonSourceStage_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceStage.Checked) this.previewSource = Preview.Sources.Stage; }

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
            string filename = Helper.selectVideoFile("select movie", string.Empty);
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

        private void textBoxDataSetName_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Name = this.textBoxDataSetName.Text; }
        private void pictureBoxDatasetMovie_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                string filename = Helper.selectVideoFile("select movie", this.selectedDataset.MovieFilename);
                if (filename != null) this.selectedDataset.MovieFilename = filename;
            }
        }

        private void listBoxDataQuestionList_SelectedIndexChanged(object sender, EventArgs e) { this.selectQuestion(this.listBoxDataQuestionList.SelectedIndex); }

        private void buttonDataMoveQuestionUp_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent &&
                this.selectedDataset.TryMoveQuestionUp(this.selectedQuestionIndex)) this.selectQuestion(this.selectedQuestionIndex - 1);
        }

        private void buttonbuttonDataMoveQuestionDown_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent &&
                this.selectedDataset.TryMoveQuestionDown(this.selectedQuestionIndex)) this.selectQuestion(this.selectedQuestionIndex + 1);
        }

        private void buttonDataAddNewQuestion_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.AddQuestion(new DatasetQuestion("?"), this.selectedQuestionIndex + 1);
            this.selectQuestion(this.selectedQuestionIndex + 1);
        }

        private void buttonDataRemoveQuestion_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent &&
                this.selectedDataset.TryRemoveQuestion(this.selectedQuestionIndex)) this.selectQuestion(this.selectedQuestionIndex - 1);
        }

        private void buttonDataRemoveAllQuestions_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.RemoveAllQuestions();
            this.selectQuestion(0);
        }

        private void textBoxDataQuestionText_TextChanged(object sender, EventArgs e) {
            if (this.selectedQuestion is DatasetQuestion) this.selectedQuestion.Text = this.textBoxDataQuestionText.Text;
        }

        private void textBoxDataQuestionAnswer_TextChanged(object sender, EventArgs e) {
            if (this.selectedQuestion is DatasetQuestion) this.selectedQuestion.Answer = this.textBoxDataQuestionAnswer.Text;
        }

        #endregion
    }
}
