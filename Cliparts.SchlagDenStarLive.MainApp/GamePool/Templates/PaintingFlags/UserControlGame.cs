using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;
using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.PaintingFlags;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.PaintingFlags
{
    public partial class UserControlGame : _Base.TimerScore.UserControlGame {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.comboBoxLeftPlayerFlagColor1.BeginUpdate();
            this.comboBoxLeftPlayerFlagColor1.Items.AddRange(Enum.GetNames(typeof(ColorStates)));
            this.comboBoxLeftPlayerFlagColor1.EndUpdate();

            this.comboBoxLeftPlayerFlagColor2.BeginUpdate();
            this.comboBoxLeftPlayerFlagColor2.Items.AddRange(Enum.GetNames(typeof(ColorStates)));
            this.comboBoxLeftPlayerFlagColor2.EndUpdate();

            this.comboBoxLeftPlayerFlagColor3.BeginUpdate();
            this.comboBoxLeftPlayerFlagColor3.Items.AddRange(Enum.GetNames(typeof(ColorStates)));
            this.comboBoxLeftPlayerFlagColor3.EndUpdate();

            this.comboBoxRightPlayerFlagColor1.BeginUpdate();
            this.comboBoxRightPlayerFlagColor1.Items.AddRange(Enum.GetNames(typeof(ColorStates)));
            this.comboBoxRightPlayerFlagColor1.EndUpdate();

            this.comboBoxRightPlayerFlagColor2.BeginUpdate();
            this.comboBoxRightPlayerFlagColor2.Items.AddRange(Enum.GetNames(typeof(ColorStates)));
            this.comboBoxRightPlayerFlagColor2.EndUpdate();

            this.comboBoxRightPlayerFlagColor3.BeginUpdate();
            this.comboBoxRightPlayerFlagColor3.Items.AddRange(Enum.GetNames(typeof(ColorStates)));
            this.comboBoxRightPlayerFlagColor3.EndUpdate();

            this.comboBoxBuzzeredPlayer.BeginUpdate();
            this.comboBoxBuzzeredPlayer.Items.AddRange(Enum.GetNames(typeof(Content.Gameboard.PlayerSelection)));
            this.comboBoxBuzzeredPlayer.EndUpdate();

            this.numericUpDownTaskCounter.Minimum = int.MinValue;
            this.numericUpDownTaskCounter.Maximum = int.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Text", this.business, "LeftPlayerFlagColor1");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxLeftPlayerFlagColor1.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "LeftPlayerFlagColor1");
            bind.Format += this.bind_comboBoxPlayerFlagColor_BackColor;
            this.comboBoxLeftPlayerFlagColor1.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftPlayerFlagColor2");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxLeftPlayerFlagColor2.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "LeftPlayerFlagColor2");
            bind.Format += this.bind_comboBoxPlayerFlagColor_BackColor;
            this.comboBoxLeftPlayerFlagColor2.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftPlayerFlagColor3");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxLeftPlayerFlagColor3.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "LeftPlayerFlagColor3");
            bind.Format += this.bind_comboBoxPlayerFlagColor_BackColor;
            this.comboBoxLeftPlayerFlagColor3.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerFlagColor1");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxRightPlayerFlagColor1.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "RightPlayerFlagColor1");
            bind.Format += this.bind_comboBoxPlayerFlagColor_BackColor;
            this.comboBoxRightPlayerFlagColor1.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerFlagColor2");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxRightPlayerFlagColor2.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "RightPlayerFlagColor2");
            bind.Format += this.bind_comboBoxPlayerFlagColor_BackColor;
            this.comboBoxRightPlayerFlagColor2.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerFlagColor3");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxRightPlayerFlagColor3.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "RightPlayerFlagColor3");
            bind.Format += this.bind_comboBoxPlayerFlagColor_BackColor;
            this.comboBoxRightPlayerFlagColor3.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "BuzzerMode");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorSelected : SystemColors.Control; };
            this.checkBoxBuzzerMode.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "BuzzeredPlayer");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxBuzzeredPlayer.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TaskCounter");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTaskCounter.DataBindings.Add(bind);

            this.labelGameClass.Text = this.business.ClassInfo;

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

            this.comboBoxLeftPlayerFlagColor1.DataBindings.Clear();
            this.comboBoxLeftPlayerFlagColor2.DataBindings.Clear();
            this.comboBoxLeftPlayerFlagColor3.DataBindings.Clear();
            this.comboBoxRightPlayerFlagColor1.DataBindings.Clear();
            this.comboBoxRightPlayerFlagColor2.DataBindings.Clear();
            this.comboBoxRightPlayerFlagColor3.DataBindings.Clear();

            this.checkBoxBuzzerMode.DataBindings.Clear();

            this.numericUpDownTaskCounter.DataBindings.Clear();

        }

        private void bind_comboBoxPlayerFlagColor_BackColor(object sender, ConvertEventArgs e)
        {
            if (e.Value.ToString() == ColorStates.Blue.ToString()) e.Value = Business.ColorValues[1];
            else if (e.Value.ToString() == ColorStates.Green.ToString()) e.Value = Business.ColorValues[2];
            else if (e.Value.ToString() == ColorStates.Red.ToString()) e.Value = Business.ColorValues[3];
            else if (e.Value.ToString() == ColorStates.Brown.ToString()) e.Value = Business.ColorValues[4];
            else if (e.Value.ToString() == ColorStates.Orange.ToString()) e.Value = Business.ColorValues[5];
            else if (e.Value.ToString() == ColorStates.Yellow.ToString()) e.Value = Business.ColorValues[6];
            else if (e.Value.ToString() == ColorStates.White.ToString()) e.Value = Business.ColorValues[7];
            else if (e.Value.ToString() == ColorStates.Black.ToString()) e.Value = Business.ColorValues[8];
            else e.Value = Business.ColorValues[0];
        }


        protected override void buildStepList() {

            int index = 0;
            stepAction step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_LoadScene);
            step.AddButton(this.buttonVhost_LoadScene);
            step.AddButton(this.buttonVleftplayer_LoadScene);
            step.AddButton(this.buttonVrightplayer_LoadScene);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVstage_Clear);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_Init);
            step.AddButton(this.buttonVfullscreen_ShowTimer);
            step.AddButton(this.buttonVstage_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TextInsertIn);
            step.AddButton(this.buttonVinsert_TimerIn);
            step.AddButton(this.buttonVstage_ContentIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_StartTimer);
            step.AddButton(this.buttonVinsert_StartTimer);
            step.AddButton(this.buttonVleftplayer_Unlock);
            step.AddButton(this.buttonVrightplayer_Unlock);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_StopTimer);
            step.AddButton(this.buttonVinsert_StopTimer);
            step.AddButton(this.buttonVhost_SetInput);
            step.AddButton(this.buttonVleftplayer_Lock);
            step.AddButton(this.buttonVrightplayer_Lock);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerOut);
            step.AddButton(this.buttonVfullscreen_ResetTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ContentIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_SolutionIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ContentOut);
            step.AddButton(this.buttonVinsert_TextInsertOut);
            step.AddButton(this.buttonVstage_ContentOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 7);
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVstage_SetScore);
            step.AddButton(this.buttonGame_Next);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreOut);
            this.stepList.Add(step);

            index++;
            this.winnerStepIndex = index;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVstage_Clear);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_UnloadScene);
            step.AddButton(this.buttonVhost_UnloadScene);
            step.AddButton(this.buttonVleftplayer_UnloadScene);
            step.AddButton(this.buttonVrightplayer_UnloadScene);
            step.AddButton(this.buttonVfullscreen_ShowGameboard);
            step.AddButton(this.buttonGame_SetWinner);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex);
            step.AddButton(this.buttonVfullscreen_UpdateGameboard);
            this.stepList.Add(step);

            if (index < this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to short");
            if (index > this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to long");
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

            this.selectDataList();
        }

        private void selectDataList() {
            int index = this.business.GetDatasetIndex(this.selectedDataset);
            if (index >= 0 &&
                index < this.listBoxDataList.Items.Count) this.listBoxDataList.SelectedIndex = index;
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

            if (this.selectedDataset is DatasetContent)
            {
                this.pictureBoxDatasetPicture.Enabled = true;
                this.pictureBoxDatasetPicture.Image = this.selectedDataset.Picture;
            }
            else
            {
                this.pictureBoxDatasetPicture.Enabled = false;
                this.pictureBoxDatasetPicture.Image = null;
            }


            this.selectDataList();
        }

        #endregion


        #region Events.Incoming

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.business_PropertyChanged(sender, e);
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "NameList") this.fillDataList();
                else if (e.PropertyName == "SampleIncluded") this.fillDataList();
                else if (e.PropertyName == "SelectedDataset") this.selectDataset(this.business.SelectedDataset);
                else if (e.PropertyName == "SelectedDatasetIndex") this.selectDataList();
            }
        }

        void selectedDataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.selectedDataset_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "PictureFilename") this.pictureBoxDatasetPicture.Image = this.selectedDataset.Picture;
            }
        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void comboBoxLeftPlayerFlagColor1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ColorStates color;
            if (Enum.TryParse(this.comboBoxLeftPlayerFlagColor1.Text, out color)) this.business.LeftPlayerFlagColor1 = color;
        }
        private void comboBoxLeftPlayerFlagColor2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ColorStates color;
            if (Enum.TryParse(this.comboBoxLeftPlayerFlagColor2.Text, out color)) this.business.LeftPlayerFlagColor2 = color;
        }
        private void comboBoxLeftPlayerFlagColor3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ColorStates color;
            if (Enum.TryParse(this.comboBoxLeftPlayerFlagColor3.Text, out color)) this.business.LeftPlayerFlagColor3 = color;
        }
        private void buttonLeftPlayerAddScoreHot_02_Click(object sender, EventArgs e) {
            this.business.LeftPlayerScore += 2;
            this.business.Vinsert_SetScore();
            this.business.Vstage_SetScore();
        }
        private void buttonLeftPlayerAddScoreHot_03_Click(object sender, EventArgs e)
        {
            this.business.LeftPlayerScore += 3;
            this.business.Vinsert_SetScore();
            this.business.Vstage_SetScore();
        }

        private void comboBoxRightPlayerFlagColor1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ColorStates color;
            if (Enum.TryParse(this.comboBoxRightPlayerFlagColor1.Text, out color)) this.business.RightPlayerFlagColor1 = color;
        }
        private void comboBoxRightPlayerFlagColor2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ColorStates color;
            if (Enum.TryParse(this.comboBoxRightPlayerFlagColor2.Text, out color)) this.business.RightPlayerFlagColor2 = color;
        }
        private void comboBoxRightPlayerFlagColor3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ColorStates color;
            if (Enum.TryParse(this.comboBoxRightPlayerFlagColor3.Text, out color)) this.business.RightPlayerFlagColor3 = color;
        }
        private void buttonRightPlayerAddScoreHot_02_Click(object sender, EventArgs e)
        {
            this.business.RightPlayerScore += 2;
            this.business.Vinsert_SetScore();
            this.business.Vstage_SetScore();
        }
        private void buttonRightPlayerAddScoreHot_03_Click(object sender, EventArgs e)
        {
            this.business.RightPlayerScore += 3;
            this.business.Vinsert_SetScore();
            this.business.Vstage_SetScore();
        }

        private void checkBoxBuzzerMode_CheckedChanged(object sender, EventArgs e) { this.business.BuzzerMode = this.checkBoxBuzzerMode.Checked; }

        private void comboBoxBuzzeredPlayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Content.Gameboard.PlayerSelection value;
            if (Enum.TryParse(this.comboBoxBuzzeredPlayer.Text, out value)) this.business.BuzzeredPlayer = value;
        }

        private void numericUpDownTaskCounter_ValueChanged(object sender, EventArgs e) { this.business.TaskCounter = (int)this.numericUpDownTaskCounter.Value; }

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectDataset(this.listBoxDataList.SelectedIndex); }

        private void buttonVinsert_TextInsertIn_Click(object sender, EventArgs e) { this.business.Vinsert_TextInsertIn(); }
        private void buttonVinsert_ContentIn_Click(object sender, EventArgs e) { this.business.Vinsert_ContentIn(); }
        private void buttonVinsert_SolutionIn_Click(object sender, EventArgs e) { this.business.Vinsert_SolutionIn(); }
        private void buttonVinsert_ContentOut_Click(object sender, EventArgs e) { this.business.Vinsert_ContentOut(); }
        private void buttonVinsert_TextInsertOut_Click(object sender, EventArgs e) { this.business.Vinsert_TextInsertOut(); }

        private void buttonVstage_ContentIn_Click(object sender, EventArgs e) { this.business.Vstage_ContentIn(); }
        private void buttonVstage_ContentOut_Click(object sender, EventArgs e) { this.business.Vstage_ContentOut(); }

        private void buttonVhost_SetPlayerInput_Click(object sender, EventArgs e) { this.business.Vhost_SetPlayerInput(); }

        private void buttonVleftplayer_Unlock_Click(object sender, EventArgs e) { this.business.Vleftplayer_Unlock(); }
        private void buttonVleftplayer_Lock_Click(object sender, EventArgs e) { this.business.Vleftplayer_Lock(); }

        private void buttonVrightplayer_Unlock_Click(object sender, EventArgs e) { this.business.Vrightplayer_Unlock(); }
        private void buttonVrightplayer_Lock_Click(object sender, EventArgs e) { this.business.Vrightplayer_Lock(); }

        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion
    }
}
