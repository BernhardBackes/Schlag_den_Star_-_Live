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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.AudioMemory;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.AudioMemory {

    public partial class UserControlContent : _Base.Score.UserControlContent {

        #region Properties

        private Business business;

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

            bind = new Binding("Text", this.business, "GameboardClientHostname");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.textBoxLocalVentuzServerGameboard.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "Filename");
            bind.Format += (s, e) => { e.Value = string.IsNullOrEmpty((string)e.Value) ? "no file loaded" : (string)e.Value; };
            this.labelFilename.DataBindings.Add(bind);

            int index = 0;
            object[] items = new object[this.comboBoxDataCardID_00_00.Items.Count];
            this.comboBoxDataCardID_00_00.Items.CopyTo(items, 0);
            foreach (SongDataset item in this.business.DataList) {
                string key = string.Format("textBoxDataSongFilename_{0}", index.ToString("00"));
                TextBox textBoxDataSongFilename = this.panelData.Controls[key] as TextBox;
                if (textBoxDataSongFilename is TextBox) {
                    bind = new Binding("Text", item, "Name");
                    bind.Format += (s, e) => { e.Value = (string)e.Value; };
                    textBoxDataSongFilename.DataBindings.Add(bind);
                }
                key = string.Format("comboBoxDataCardID_{0}_00", index.ToString("00"));
                ComboBox comboBoxDataCardID = this.panelData.Controls[key] as ComboBox;
                if (comboBoxDataCardID is ComboBox) {
                    bind = new Binding("Text", item, "FirstCardID");
                    bind.Format += (s, e) => { e.Value = ((int)e.Value).ToString("00"); };
                    comboBoxDataCardID.DataBindings.Add(bind);
                    bind = new Binding("BackColor", item, "FirstCardIDIsValid");
                    bind.Format += (s, e) => { e.Value = ((bool)e.Value) ? SystemColors.Control : Constants.ColorMissing; };
                    comboBoxDataCardID.DataBindings.Add(bind);
                    if (index > 0) comboBoxDataCardID.Items.AddRange(items);
                }
                key = string.Format("comboBoxDataCardID_{0}_01", index.ToString("00"));
                comboBoxDataCardID = this.panelData.Controls[key] as ComboBox;
                if (comboBoxDataCardID is ComboBox) {
                    bind = new Binding("Text", item, "SecondCardID");
                    bind.Format += (s, e) => { e.Value = ((int)e.Value).ToString("00"); };
                    comboBoxDataCardID.DataBindings.Add(bind);
                    bind = new Binding("BackColor", item, "SecondCardIDIsValid");
                    bind.Format += (s, e) => { e.Value = ((bool)e.Value) ? SystemColors.Control : Constants.ColorMissing; };
                    comboBoxDataCardID.DataBindings.Add(bind);
                    comboBoxDataCardID.Items.AddRange(items);
                }
                index++;
            }

            this.labelGameClass.Text = this.business.ClassInfo;

            this.fill_listBoxDataUnusedCardIDs(this.business.UnusedCardIDs.ToArray());
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

            this.numericUpDownBorderPositionX.DataBindings.Clear();
            this.numericUpDownBorderPositionY.DataBindings.Clear();
            this.comboBoxBorderStyle.DataBindings.Clear();

            for (int i = 0; i < Business.SongsCount; i++) {
                string key = string.Format("textBoxDataSongFilename_{0}", i.ToString("00")); 
                Control control = this.panelData.Controls[key];
                if (control is Control) control.DataBindings.Clear();
                key = string.Format("comboBoxDataCardID_{0}_00", i.ToString("00"));
                control = this.panelData.Controls[key];
                if (control is Control) control.DataBindings.Clear();
                key = string.Format("comboBoxDataCardID_{0}_01", i.ToString("00"));
                control = this.panelData.Controls[key];
                if (control is Control) control.DataBindings.Clear();
            }

            this.textBoxLocalVentuzServerGameboard.DataBindings.Clear();
            this.labelFilename.DataBindings.Clear();

        }

        private void fill_listBoxDataUnusedCardIDs(
            int[] value) {
            this.listBoxDataUnusedCardIDs.BeginUpdate();
            this.listBoxDataUnusedCardIDs.Items.Clear();
            if (value is int[] &&
                value.Length > 0) {
                object[] items = new object[value.Length];
                value.CopyTo(items, 0);
                this.listBoxDataUnusedCardIDs.Items.AddRange(items);
            }
            this.listBoxDataUnusedCardIDs.EndUpdate();
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
                //this.business.Vinsert_SetScore(previewScene.Insert.Score, 2, 3);
                //if (this.previewSource == Preview.Sources.Insert) {
                //    previewScene.Insert.Score.SetIn();
                //}
            }
        }

        protected void setFullscreenPreview() {
            if (this.previewSceneIsAvailable) {
                this.setGameboardPreview();
            }
        }

        protected virtual void setGameboardPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.previewSource == Preview.Sources.Fullscreen) {
                }
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
                if (e.PropertyName == "UnusedCardIDs") this.fill_listBoxDataUnusedCardIDs(this.business.UnusedCardIDs.ToArray());
                //else if (e.PropertyName == "SampleIncluded") this.fillDataList();
            }
        }

        #endregion

        #region Events.Controls

        private void numericUpDownBorderPositionX_ValueChanged(object sender, EventArgs e) { this.business.BorderPositionX = (int)this.numericUpDownBorderPositionX.Value; }
        private void numericUpDownBorderPositionY_ValueChanged(object sender, EventArgs e) { this.business.BorderPositionY = (int)this.numericUpDownBorderPositionY.Value; }
        private void numericUpDownBorderScaling_ValueChanged(object sender, EventArgs e) { this.business.BorderScaling = (int)this.numericUpDownBorderScaling.Value; }
        private void comboBoxBorderStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.Border.Styles style;
            if (Enum.TryParse(this.comboBoxBorderStyle.Text, out style)) this.business.BorderStyle = style;
        }

        private void textBoxLocalVentuzServerGameboard_TextChanged(object sender, EventArgs e) { this.business.GameboardClientHostname = this.textBoxLocalVentuzServerGameboard.Text; }

        private void radioButtonSourceInsert_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceInsert.Checked) this.previewSource = Preview.Sources.Insert; }
        private void radioButtonSourceFullscreen_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceFullscreen.Checked) this.previewSource = Preview.Sources.Fullscreen; }

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

        private void textBoxDataSongFilename_Click(object sender, EventArgs e) {
            int index;
            SongDataset song;
            if (Helper.tryParseIndexFromControl(sender as Control, out index) &&
                this.business.TryGetSongDataset(index, out song)) {
                string filename = Helper.selectAudioFile("select song", song.Filename);
                if (filename != null) song.Filename = filename;
            }
        }

        private void comboBoxDataCardID_SelectedIndexChanged(object sender, EventArgs e) {
            int firstIndex;
            int secondIndex;
            ComboBox control = sender as ComboBox;
            SongDataset song;
            int result;
            if (control is ComboBox &&
                Helper.tryParseTwoIndicesFromControl(control, out firstIndex, out secondIndex) &&
                this.business.TryGetSongDataset(firstIndex, out song) &&
                int.TryParse(control.Text, out result)) {
                if (secondIndex == 0) song.FirstCardID = result;
                else if (secondIndex == 1) song.SecondCardID = result;
            }
        }

        private void buttonDataRandomize_Click(object sender, EventArgs e) { this.business.RandomizeData(); }

        #endregion

    }

}
