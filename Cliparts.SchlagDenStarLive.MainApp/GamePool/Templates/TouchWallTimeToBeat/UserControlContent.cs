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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TouchWallTimeToBeat {

    public partial class UserControlContent : _Base.UserControlContent {

        #region Properties

        private Business business;

        private TileCrowd[] selectedTilesQueueList = null;

        private bool challengeSelected = false;

        private int selectedTileCrowdIndex = -1;

        private TileCrowd selectedTileCrowd = null;

        //private bool previewSceneIsAvailable { get { return this.previewScene is VentuzScenes.Games.BestRatedPuls.Insert && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownTimeToBeatPositionX.Minimum = int.MinValue;
            this.numericUpDownTimeToBeatPositionX.Maximum = int.MaxValue;

            this.numericUpDownTimeToBeatPositionY.Minimum = int.MinValue;
            this.numericUpDownTimeToBeatPositionY.Maximum = int.MaxValue;

            this.comboBoxTimeToBeatStyle.BeginUpdate();
            this.comboBoxTimeToBeatStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.TimeToBeat.Styles)));
            this.comboBoxTimeToBeatStyle.EndUpdate();
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "TimeToBeatPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimeToBeatPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimeToBeatPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimeToBeatPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimeToBeatStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxTimeToBeatStyle.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "IOUnitAName");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.comboBoxIOUnitA.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitAStatus");
            bind.Format += this.bind_comboBoxIOUnitName_BackColor;
            this.comboBoxIOUnitA.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "IOUnitBName");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.comboBoxIOUnitB.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitBStatus");
            bind.Format += this.bind_comboBoxIOUnitName_BackColor;
            this.comboBoxIOUnitB.DataBindings.Add(bind);

            this.userControlTouchWallTimeToBeatTile_00.Pose(this.business.Tiles[0]);
            this.userControlTouchWallTimeToBeatTile_01.Pose(this.business.Tiles[1]);
            this.userControlTouchWallTimeToBeatTile_02.Pose(this.business.Tiles[2]);
            this.userControlTouchWallTimeToBeatTile_03.Pose(this.business.Tiles[3]);
            this.userControlTouchWallTimeToBeatTile_04.Pose(this.business.Tiles[4]);
            this.userControlTouchWallTimeToBeatTile_05.Pose(this.business.Tiles[5]);
            this.userControlTouchWallTimeToBeatTile_06.Pose(this.business.Tiles[6]);
            this.userControlTouchWallTimeToBeatTile_07.Pose(this.business.Tiles[7]);
            this.userControlTouchWallTimeToBeatTile_08.Pose(this.business.Tiles[8]);

            bind = new Binding("Text", this.business, "Filename");
            bind.Format += (s, e) => { e.Value = string.IsNullOrEmpty((string)e.Value) ? "no file loaded" : (string)e.Value; };
            this.labelFilename.DataBindings.Add(bind);

            this.labelGameClass.Text = this.business.ClassInfo;

            this.fillIOUnitList();

            this.selectSample();
            this.selectTileCrowd(0);
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

            this.numericUpDownTimeToBeatPositionX.DataBindings.Clear();
            this.numericUpDownTimeToBeatPositionY.DataBindings.Clear();
            this.comboBoxTimeToBeatStyle.DataBindings.Clear();

            this.comboBoxIOUnitA.DataBindings.Clear();
            this.comboBoxIOUnitB.DataBindings.Clear();

            this.userControlTouchWallTimeToBeatTile_00.Dispose();
            this.userControlTouchWallTimeToBeatTile_01.Dispose();
            this.userControlTouchWallTimeToBeatTile_02.Dispose();
            this.userControlTouchWallTimeToBeatTile_03.Dispose();
            this.userControlTouchWallTimeToBeatTile_04.Dispose();
            this.userControlTouchWallTimeToBeatTile_05.Dispose();
            this.userControlTouchWallTimeToBeatTile_06.Dispose();
            this.userControlTouchWallTimeToBeatTile_07.Dispose();
            this.userControlTouchWallTimeToBeatTile_08.Dispose();

            this.labelFilename.DataBindings.Clear();
        }

        private void fillIOUnitList() {
            this.comboBoxIOUnitA.BeginUpdate();
            this.comboBoxIOUnitA.Items.Clear();
            this.comboBoxIOUnitA.Items.AddRange(this.business.IOUnitNameList);
            this.comboBoxIOUnitA.EndUpdate();
            this.comboBoxIOUnitB.BeginUpdate();
            this.comboBoxIOUnitB.Items.Clear();
            this.comboBoxIOUnitB.Items.AddRange(this.business.IOUnitNameList);
            this.comboBoxIOUnitB.EndUpdate();
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

        private void selectSample() {
            this.challengeSelected = false;
            this.buttonSample.BackColor = Constants.ColorBuzzered;
            this.buttonChallenge.UseVisualStyleBackColor = true;
            this.fillListBoxTileCrowdList();
        }

        private void selectChallenge() {
            this.challengeSelected = true;
            this.buttonSample.UseVisualStyleBackColor = true;
            this.buttonChallenge.BackColor = Constants.ColorBuzzered;
            this.fillListBoxTileCrowdList();
        }

        private void fillListBoxTileCrowdList() {
            if (this.challengeSelected) this.selectedTilesQueueList = this.business.Challenge;
            else this.selectedTilesQueueList = this.business.Sample;

            this.listBoxTileCrowdList.BeginUpdate();
            this.listBoxTileCrowdList.Items.Clear();
            this.listBoxTileCrowdList.Items.AddRange(this.selectedTilesQueueList);
            this.listBoxTileCrowdList.EndUpdate();

            this.listBoxTileCrowdList.Enabled = this.listBoxTileCrowdList.Items.Count > 0;
            Helper.setControlBackColor(this.listBoxTileCrowdList);

            this.buttonDataRemoveAllTileCrowds.Enabled = this.listBoxTileCrowdList.Items.Count > 0;
            Helper.setControlBackColor(this.buttonDataRemoveAllTileCrowds);
        }

        private void selectTileCrowd(
            int index) {
            this.selectedTileCrowd = null;
            if (index < 0) index = 0;
            if (index >= this.selectedTilesQueueList.Length) index = this.selectedTilesQueueList.Length - 1;
            string key;
            Button buttonTileCrowdTile;
            int id;
            if (index >= 0) {
                this.listBoxTileCrowdList.SelectedIndex = index;
                TileCrowd tileQueue = this.selectedTilesQueueList[index];
                for (int i = 0; i < Business.TilesCount; i++) {
                    key = "buttonTileCrowdTile_" + i.ToString("00");
                    buttonTileCrowdTile = this.groupBoxTileCrowd.Controls[key] as Button;
                    if (buttonTileCrowdTile is Button) {
                        id = i + 1;
                        buttonTileCrowdTile.Enabled = true;
                        if (tileQueue.Crowd.Contains(id.ToString())) buttonTileCrowdTile.BackColor = Constants.ColorBuzzered;
                        else buttonTileCrowdTile.UseVisualStyleBackColor = true;
                    }
                }
                this.selectedTileCrowd = tileQueue;
            }
            else {
                for (int i = 0; i < Business.TilesCount; i++) {
                    key = "buttonTileCrowdTile_" + i.ToString("00");
                    buttonTileCrowdTile = this.groupBoxTileCrowd.Controls[key] as Button;
                    if (buttonTileCrowdTile is Button) {
                        buttonTileCrowdTile.Enabled = false;
                        Helper.setControlBackColor(buttonTileCrowdTile);
                    }
                }
            }
            this.buttonDataRemoveTileCrowd.Enabled = this.listBoxTileCrowdList.SelectedIndex >= 0;
            Helper.setControlBackColor(this.buttonDataRemoveTileCrowd);
            this.selectedTileCrowdIndex = index;
        }

        #endregion


        #region Events.Incoming

        void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "IOUnitNameList") this.fillIOUnitList();
            }
        }

        #endregion

        #region Events.Controls

        private void UserControlContent_BackColorChanged(object sender, EventArgs e) {
            this.userControlTouchWallTimeToBeatTile_00.BackColor = this.BackColor;
            this.userControlTouchWallTimeToBeatTile_01.BackColor = this.BackColor;
            this.userControlTouchWallTimeToBeatTile_02.BackColor = this.BackColor;
            this.userControlTouchWallTimeToBeatTile_03.BackColor = this.BackColor;
            this.userControlTouchWallTimeToBeatTile_04.BackColor = this.BackColor;
            this.userControlTouchWallTimeToBeatTile_05.BackColor = this.BackColor;
            this.userControlTouchWallTimeToBeatTile_06.BackColor = this.BackColor;
            this.userControlTouchWallTimeToBeatTile_07.BackColor = this.BackColor;
            this.userControlTouchWallTimeToBeatTile_08.BackColor = this.BackColor;
        }

        protected virtual void numericUpDownTimeToBeatPositionX_ValueChanged(object sender, EventArgs e) { this.business.TimeToBeatPositionX = (int)this.numericUpDownTimeToBeatPositionX.Value; }
        protected virtual void numericUpDownTimeToBeatPositionY_ValueChanged(object sender, EventArgs e) { this.business.TimeToBeatPositionY = (int)this.numericUpDownTimeToBeatPositionY.Value; }
        protected virtual void comboBoxTimeToBeatStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.TimeToBeat.Styles style;
            if (Enum.TryParse(this.comboBoxTimeToBeatStyle.Text, out style)) this.business.TimeToBeatStyle = style;
        }

        protected virtual void comboBoxIOUnitA_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.IOUnitAName = this.comboBoxIOUnitA.Text; }
        protected virtual void comboBoxIOUnitB_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.IOUnitBName = this.comboBoxIOUnitB.Text; }

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
            this.selectSample();
            this.selectTileCrowd(0);
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

        private void buttonSample_Click(object sender, EventArgs e) { 
            this.selectSample();
            this.selectTileCrowd(0);
        }
        private void buttonChallenge_Click(object sender, EventArgs e) { 
            this.selectChallenge();
            this.selectTileCrowd(0);
        }

        private void buttonFillRandomized_Click(object sender, EventArgs e) {
            List<TileCrowd> list = new List<TileCrowd>(this.selectedTilesQueueList);
            this.business.FillTileCrowdListRandomized(list, (int)this.numericUpDownFillRandomized.Value);
            if (this.challengeSelected) this.business.SetChallenge(list.ToArray());
            else this.business.SetSample(list.ToArray());
            this.fillListBoxTileCrowdList();
            this.selectTileCrowd(this.listBoxTileCrowdList.Items.Count - 1);
            this.business.Save();
        }

        private void listBoxTileCrowdList_Click(object sender, EventArgs e) { this.selectTileCrowd(this.listBoxTileCrowdList.SelectedIndex); }

        private void buttonDataAddNewTileCrowd_Click(object sender, EventArgs e) {
            List<TileCrowd> list = new List<TileCrowd>(this.selectedTilesQueueList);
            list.Add(new TileCrowd());
            if (this.challengeSelected) this.business.SetChallenge(list.ToArray());
            else this.business.SetSample(list.ToArray());
            this.fillListBoxTileCrowdList();
            this.selectTileCrowd(this.listBoxTileCrowdList.Items.Count - 1);
            this.business.Save();
        }

        private void buttonDataRemoveTileCrowd_Click(object sender, EventArgs e) {
            List<TileCrowd> list = new List<TileCrowd>(this.selectedTilesQueueList);
            if (this.selectedTileCrowdIndex >= 0 &&
                this.selectedTileCrowdIndex < list.Count) list.RemoveAt(this.selectedTileCrowdIndex);
            if (this.challengeSelected) this.business.SetChallenge(list.ToArray());
            else this.business.SetSample(list.ToArray());
            this.fillListBoxTileCrowdList();
            this.selectTileCrowd(this.selectedTileCrowdIndex);
            this.business.Save();
        }

        private void buttonDataRemoveAllTileCrowds_Click(object sender, EventArgs e) {
            if (this.challengeSelected) this.business.SetChallenge(null);
            else this.business.SetSample(null);
            this.fillListBoxTileCrowdList();
            this.selectTileCrowd(0);
            this.business.Save();
        }

        private void buttonTileCrowdTile_Click(object sender, EventArgs e) {
            int index;
            if (this.selectedTileCrowd is TileCrowd &&
                Helper.tryParseIndexFromControl(sender as Control, out index)) {
                int id = index + 1;
                string sequence = this.selectedTileCrowd.Crowd;
                int idIndex = sequence.IndexOf(id.ToString());
                if (idIndex >= 0) sequence = sequence.Remove(idIndex, 1);
                else sequence += id.ToString();
                this.selectedTileCrowd.Crowd = sequence;
                if (this.challengeSelected) this.business.SetChallenge(this.selectedTilesQueueList);
                else this.business.SetSample(this.selectedTilesQueueList);
                this.fillListBoxTileCrowdList();
                this.selectTileCrowd(this.selectedTileCrowdIndex);
                this.business.Save();
            }
        }

        #endregion
    }
}
