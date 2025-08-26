using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.SelectQuestionScore {

    public partial class UserControlGame : _Base.BuzzerScore.UserControlGame {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownTaskCounter.Minimum = int.MinValue;
            this.numericUpDownTaskCounter.Maximum = int.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "TaskCounter");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTaskCounter.DataBindings.Add(bind);

            this.setSelectedPlayer();

            this.setIdleDataListCat1();
            this.setIdleDataListCat2();
            this.setIdleDataListCat3();
            this.setIdleDataListCat4();
            this.setIdleDataListCat5();
            this.setIdleDataListCat6();

            this.setPreselectedCategory();

            this.setActiveDataList();

            this.setUsedDataList();

            this.setSelectedQuestion();

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

            this.numericUpDownTaskCounter.DataBindings.Clear();
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
            step.AddButton(this.buttonGame_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVstage_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_GameIn);
            step.AddButton(this.buttonVhost_GameIn);
            step.AddButton(this.buttonVplayers_GameIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreIn);
            step.AddButton(this.buttonVinsert_TaskCounterIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_StartTimeout);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVstage_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 2);
            step.AddButton(this.buttonGame_Next);
            step.AddButton(this.buttonVinsert_SetTaskCounter);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 3);
            step.AddButton(this.buttonVinsert_TaskCounterOut);
            step.AddButton(this.buttonGame_ReleaseBuzzer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TaskCounterOut_01);
            step.AddButton(this.buttonVinsert_ScoreOut);
            step.AddButton(this.buttonVinsert_GameOut);
            step.AddButton(this.buttonVhost_GameOut);
            step.AddButton(this.buttonVplayers_GameOut_01);
            this.stepList.Add(step);

            index++;
            this.winnerStepIndex = index;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_ShowGameboard);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVstage_Clear);
            step.AddButton(this.buttonGame_LockBuzzer);
            step.AddButton(this.buttonVinsert_UnloadScene);
            step.AddButton(this.buttonVhost_UnloadScene);
            step.AddButton(this.buttonVleftplayer_UnloadScene);
            step.AddButton(this.buttonVrightplayer_UnloadScene);
            step.AddButton(this.buttonGame_SetWinner);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_UpdateGameboard);
            this.stepList.Add(step);

            if (index < this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to short");
            if (index > this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to long");
        }

        private void setSelectedPlayer() {
            switch (this.business.SelectedPlayer) {
                case Content.Gameboard.PlayerSelection.LeftPlayer:
                    this.buttonLeftPlayerSelected.BackColor = Constants.ColorSelected;
                    this.buttonRightPlayerSelected.UseVisualStyleBackColor = true;
                    break;
                case Content.Gameboard.PlayerSelection.RightPlayer:
                    this.buttonLeftPlayerSelected.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerSelected.BackColor = Constants.ColorSelected;
                    break;
                case Content.Gameboard.PlayerSelection.NotSelected:
                default:
                    this.buttonLeftPlayerSelected.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerSelected.UseVisualStyleBackColor = true;
                    break;
            }
        }

        private void setIdleDataListCat1() {
            this.listBoxIdle1.BeginUpdate();
            this.listBoxIdle1.Items.Clear();
            this.listBoxIdle1.Items.AddRange(this.business.IdleDataListCat1.ToArray());
            this.listBoxIdle1.EndUpdate();
            if (this.business.PreselectedCategory == DatasetContent.Categories.Cat1) this.setPreselectedDatasetIndex();
        }
        private void setIdleDataListCat2() {
            this.listBoxIdle2.BeginUpdate();
            this.listBoxIdle2.Items.Clear();
            this.listBoxIdle2.Items.AddRange(this.business.IdleDataListCat2.ToArray());
            this.listBoxIdle2.EndUpdate();
            if (this.business.PreselectedCategory == DatasetContent.Categories.Cat2) this.setPreselectedDatasetIndex();
        }
        private void setIdleDataListCat3() {
            this.listBoxIdle3.BeginUpdate();
            this.listBoxIdle3.Items.Clear();
            this.listBoxIdle3.Items.AddRange(this.business.IdleDataListCat3.ToArray());
            this.listBoxIdle3.EndUpdate();
            if (this.business.PreselectedCategory == DatasetContent.Categories.Cat3) this.setPreselectedDatasetIndex();
        }
        private void setIdleDataListCat4() {
            this.listBoxIdle4.BeginUpdate();
            this.listBoxIdle4.Items.Clear();
            this.listBoxIdle4.Items.AddRange(this.business.IdleDataListCat4.ToArray());
            this.listBoxIdle4.EndUpdate();
            if (this.business.PreselectedCategory == DatasetContent.Categories.Cat4) this.setPreselectedDatasetIndex();
        }
        private void setIdleDataListCat5() {
            this.listBoxIdle5.BeginUpdate();
            this.listBoxIdle5.Items.Clear();
            this.listBoxIdle5.Items.AddRange(this.business.IdleDataListCat5.ToArray());
            this.listBoxIdle5.EndUpdate();
            if (this.business.PreselectedCategory == DatasetContent.Categories.Cat5) this.setPreselectedDatasetIndex();
        }
        private void setIdleDataListCat6() {
            this.listBoxIdle6.BeginUpdate();
            this.listBoxIdle6.Items.Clear();
            this.listBoxIdle6.Items.AddRange(this.business.IdleDataListCat6.ToArray());
            this.listBoxIdle6.EndUpdate();
            if (this.business.PreselectedCategory == DatasetContent.Categories.Cat6) this.setPreselectedDatasetIndex();
        }

        private void setPreselectedDatasetIndex() {
            if (this.tabControlIdle.SelectedTab == this.tabPageCat1) {
                if (this.business.PreselectedDatasetIndex >= 0 &&
                    this.business.PreselectedDatasetIndex < this.listBoxIdle1.Items.Count) {
                    this.listBoxIdle1.SelectedIndex = this.business.PreselectedDatasetIndex;
                }
                else if (this.listBoxIdle1.Items.Count > 0) this.listBoxIdle1.SelectedIndex = 0;
            }
            else if (this.tabControlIdle.SelectedTab == this.tabPageCat2) {
                if (this.business.PreselectedDatasetIndex >= 0 &&
                    this.business.PreselectedDatasetIndex < this.listBoxIdle2.Items.Count) {
                    this.listBoxIdle2.SelectedIndex = this.business.PreselectedDatasetIndex;
                }
                else if (this.listBoxIdle2.Items.Count > 0) this.listBoxIdle2.SelectedIndex = 0;
            }
            else if (this.tabControlIdle.SelectedTab == this.tabPageCat3) {
                if (this.business.PreselectedDatasetIndex >= 0 &&
                    this.business.PreselectedDatasetIndex < this.listBoxIdle3.Items.Count) {
                    this.listBoxIdle3.SelectedIndex = this.business.PreselectedDatasetIndex;
                }
                else if (this.listBoxIdle3.Items.Count > 0) this.listBoxIdle3.SelectedIndex = 0;
            }
            else if (this.tabControlIdle.SelectedTab == this.tabPageCat4) {
                if (this.business.PreselectedDatasetIndex >= 0 &&
                    this.business.PreselectedDatasetIndex < this.listBoxIdle4.Items.Count) {
                    this.listBoxIdle4.SelectedIndex = this.business.PreselectedDatasetIndex;
                }
                else if (this.listBoxIdle4.Items.Count > 0) this.listBoxIdle4.SelectedIndex = 0;
            }
            else if (this.tabControlIdle.SelectedTab == this.tabPageCat5) {
                if (this.business.PreselectedDatasetIndex >= 0 &&
                    this.business.PreselectedDatasetIndex < this.listBoxIdle5.Items.Count) {
                    this.listBoxIdle5.SelectedIndex = this.business.PreselectedDatasetIndex;
                }
                else if (this.listBoxIdle5.Items.Count > 0) this.listBoxIdle5.SelectedIndex = 0;
            }
            else if (this.tabControlIdle.SelectedTab == this.tabPageCat6) {
                if (this.business.PreselectedDatasetIndex >= 0 &&
                    this.business.PreselectedDatasetIndex < this.listBoxIdle6.Items.Count) {
                    this.listBoxIdle6.SelectedIndex = this.business.PreselectedDatasetIndex;
                }
                else if (this.listBoxIdle6.Items.Count > 0) this.listBoxIdle6.SelectedIndex = 0;
            }
        }

        private void setPreselectedCategory() {
            this.tabControlIdle.SelectedIndex = (int)this.business.PreselectedCategory;
            this.setPreselectedDatasetIndex();
        }

        private void setActiveDataList() {
            if (this.business.ActiveDataList.Count >= 1) {
                this.labelQuestionID_01.Enabled = true;
                this.buttonSelectQuestion_01.Enabled = true;
                this.buttonSetQuestionUsed_01.Enabled = true;
                this.textBoxQuestion_01.Enabled = true;
                this.textBoxQuestion_01.Text = this.business.ActiveDataList[0].HostText;
            }
            else {
                this.labelQuestionID_01.Enabled = false;
                this.buttonSelectQuestion_01.Enabled = false;
                this.buttonSetQuestionUsed_01.Enabled = false;
                this.textBoxQuestion_01.Enabled = false;
                this.textBoxQuestion_01.Text = string.Empty;
            }
            if (this.business.ActiveDataList.Count >= 2) {
                this.labelQuestionID_02.Enabled = true;
                this.buttonSelectQuestion_02.Enabled = true;
                this.buttonSetQuestionUsed_02.Enabled = true;
                this.textBoxQuestion_02.Enabled = true;
                this.textBoxQuestion_02.Text = this.business.ActiveDataList[1].HostText;
            }
            else {
                this.labelQuestionID_02.Enabled = false;
                this.buttonSelectQuestion_02.Enabled = false;
                this.buttonSetQuestionUsed_02.Enabled = false;
                this.textBoxQuestion_02.Enabled = false;
                this.textBoxQuestion_02.Text = string.Empty;
            }
            if (this.business.ActiveDataList.Count >= 3) {
                this.labelQuestionID_03.Enabled = true;
                this.buttonSelectQuestion_03.Enabled = true;
                this.buttonSetQuestionUsed_03.Enabled = true;
                this.textBoxQuestion_03.Enabled = true;
                this.textBoxQuestion_03.Text = this.business.ActiveDataList[2].HostText;
            }
            else {
                this.labelQuestionID_03.Enabled = false;
                this.buttonSelectQuestion_03.Enabled = false;
                this.buttonSetQuestionUsed_03.Enabled = false;
                this.textBoxQuestion_03.Enabled = false;
                this.textBoxQuestion_03.Text = string.Empty;
            }
            if (this.business.ActiveDataList.Count >= 4) {
                this.labelQuestionID_04.Enabled = true;
                this.buttonSelectQuestion_04.Enabled = true;
                this.buttonSetQuestionUsed_04.Enabled = true;
                this.textBoxQuestion_04.Enabled = true;
                this.textBoxQuestion_04.Text = this.business.ActiveDataList[3].HostText;
            }
            else {
                this.labelQuestionID_04.Enabled = false;
                this.buttonSelectQuestion_04.Enabled = false;
                this.buttonSetQuestionUsed_04.Enabled = false;
                this.textBoxQuestion_04.Enabled = false;
                this.textBoxQuestion_04.Text = string.Empty;
            }
            if (this.business.ActiveDataList.Count >= 5) {
                this.labelQuestionID_05.Enabled = true;
                this.buttonSelectQuestion_05.Enabled = true;
                this.buttonSetQuestionUsed_05.Enabled = true;
                this.textBoxQuestion_05.Enabled = true;
                this.textBoxQuestion_05.Text = this.business.ActiveDataList[4].HostText;
            }
            else {
                this.labelQuestionID_05.Enabled = false;
                this.buttonSelectQuestion_05.Enabled = false;
                this.buttonSetQuestionUsed_05.Enabled = false;
                this.textBoxQuestion_05.Enabled = false;
                this.textBoxQuestion_05.Text = string.Empty;
            }
            if (this.business.ActiveDataList.Count >= 6) {
                this.labelQuestionID_06.Enabled = true;
                this.buttonSelectQuestion_06.Enabled = true;
                this.buttonSetQuestionUsed_06.Enabled = true;
                this.textBoxQuestion_06.Enabled = true;
                this.textBoxQuestion_06.Text = this.business.ActiveDataList[5].HostText;
            }
            else {
                this.labelQuestionID_06.Enabled = false;
                this.buttonSelectQuestion_06.Enabled = false;
                this.buttonSetQuestionUsed_06.Enabled = false;
                this.textBoxQuestion_06.Enabled = false;
                this.textBoxQuestion_06.Text = string.Empty;
            }
            if (this.business.ActiveDataList.Count >= 7) {
                this.labelQuestionID_07.Enabled = true;
                this.buttonSelectQuestion_07.Enabled = true;
                this.buttonSetQuestionUsed_07.Enabled = true;
                this.textBoxQuestion_07.Enabled = true;
                this.textBoxQuestion_07.Text = this.business.ActiveDataList[6].HostText;
            }
            else {
                this.labelQuestionID_07.Enabled = false;
                this.buttonSelectQuestion_07.Enabled = false;
                this.buttonSetQuestionUsed_07.Enabled = false;
                this.textBoxQuestion_07.Enabled = false;
                this.textBoxQuestion_07.Text = string.Empty;
            }
            this.setSelectedQuestion();
        }

        private void setUsedDataList() {
            this.listBoxUsedDataList.BeginUpdate();
            this.listBoxUsedDataList.Items.Clear();
            this.listBoxUsedDataList.Items.AddRange(this.business.UsedDataList.ToArray());
            this.listBoxUsedDataList.EndUpdate();
        }

        private void setSelectedQuestion() {
            if (this.business.SelectedDatasetID > 0 &&
                this.business.SelectedDatasetID <= this.business.ActiveDataList.Count) {
                this.textBoxSelectedQuestionHostText.Enabled = true;
                this.textBoxSelectedQuestionHostText.Text = this.business.ActiveDataList[this.business.SelectedDatasetID - 1].HostText;
                if (this.buttonSelectQuestion_01.Enabled) {
                    if (this.business.SelectedDatasetID == 1) this.buttonSelectQuestion_01.BackColor = Constants.ColorSelected;
                    else this.buttonSelectQuestion_01.BackColor = SystemColors.ButtonFace;
                }
                if (this.buttonSelectQuestion_02.Enabled) {
                    if (this.business.SelectedDatasetID == 2) this.buttonSelectQuestion_02.BackColor = Constants.ColorSelected;
                    else this.buttonSelectQuestion_02.BackColor = SystemColors.ButtonFace;
                }
                if (this.buttonSelectQuestion_03.Enabled) {
                    if (this.business.SelectedDatasetID == 3) this.buttonSelectQuestion_03.BackColor = Constants.ColorSelected;
                    else this.buttonSelectQuestion_03.BackColor = SystemColors.ButtonFace;
                }
                if (this.buttonSelectQuestion_04.Enabled) {
                    if (this.business.SelectedDatasetID == 4) this.buttonSelectQuestion_04.BackColor = Constants.ColorSelected;
                    else this.buttonSelectQuestion_04.BackColor = SystemColors.ButtonFace;
                }
                if (this.buttonSelectQuestion_05.Enabled) {
                    if (this.business.SelectedDatasetID == 5) this.buttonSelectQuestion_05.BackColor = Constants.ColorSelected;
                    else this.buttonSelectQuestion_05.BackColor = SystemColors.ButtonFace;
                }
                if (this.buttonSelectQuestion_06.Enabled) {
                    if (this.business.SelectedDatasetID == 6) this.buttonSelectQuestion_06.BackColor = Constants.ColorSelected;
                    else this.buttonSelectQuestion_06.BackColor = SystemColors.ButtonFace;
                }
                if (this.buttonSelectQuestion_07.Enabled) {
                    if (this.business.SelectedDatasetID == 7) this.buttonSelectQuestion_07.BackColor = Constants.ColorSelected;
                    else this.buttonSelectQuestion_07.BackColor = SystemColors.ButtonFace;
                }
            }
            else {
                this.textBoxSelectedQuestionHostText.Enabled = false;
                this.textBoxSelectedQuestionHostText.Text = string.Empty;
                if (this.buttonSelectQuestion_01.Enabled) this.buttonSelectQuestion_01.BackColor = SystemColors.ButtonFace;
                if (this.buttonSelectQuestion_02.Enabled) this.buttonSelectQuestion_02.BackColor = SystemColors.ButtonFace;
                if (this.buttonSelectQuestion_03.Enabled) this.buttonSelectQuestion_03.BackColor = SystemColors.ButtonFace;
                if (this.buttonSelectQuestion_04.Enabled) this.buttonSelectQuestion_04.BackColor = SystemColors.ButtonFace;
                if (this.buttonSelectQuestion_05.Enabled) this.buttonSelectQuestion_05.BackColor = SystemColors.ButtonFace;
                if (this.buttonSelectQuestion_06.Enabled) this.buttonSelectQuestion_06.BackColor = SystemColors.ButtonFace;
                if (this.buttonSelectQuestion_07.Enabled) this.buttonSelectQuestion_07.BackColor = SystemColors.ButtonFace;
            }
        }

        #endregion


        #region Events.Incoming

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.business_PropertyChanged(sender, e);
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "SelectedPlayer") this.setSelectedPlayer();
                else if (e.PropertyName == "IdleDataListCat1") this.setIdleDataListCat1();
                else if (e.PropertyName == "IdleDataListCat2") this.setIdleDataListCat2();
                else if (e.PropertyName == "IdleDataListCat3") this.setIdleDataListCat3();
                else if (e.PropertyName == "IdleDataListCat4") this.setIdleDataListCat4();
                else if (e.PropertyName == "IdleDataListCat5") this.setIdleDataListCat5();
                else if (e.PropertyName == "IdleDataListCat6") this.setIdleDataListCat6();
                else if (e.PropertyName == "PreselectedDatasetIndex") this.setPreselectedDatasetIndex();
                else if (e.PropertyName == "PreselectedCategory") this.setPreselectedCategory();
                else if (e.PropertyName == "ActiveDataList") this.setActiveDataList();
                else if (e.PropertyName == "UsedDataList") this.setUsedDataList();
                else if (e.PropertyName == "SelectedDatasetID") this.setSelectedQuestion();
            }
        }

        #endregion

        #region Events.Controls

        private void UserControlGame_BackColorChanged(object sender, EventArgs e) {
            Control control;
            string key;
            for (int i = 0; i < Business.TaskCounterPenaltyCountMax; i++) {
                key = "userControlGamePoolTemplatesTaskCounterSingleDot_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.BackColor = this.BackColor;
                key = "labelDotCounter_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.BackColor = this.BackColor;
            }
        }

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void buttonLeftPlayerSelected_Click(object sender, EventArgs e) { this.business.SelectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer; }
        private void buttonRightPlayerSelected_Click(object sender, EventArgs e) { this.business.SelectedPlayer = Content.Gameboard.PlayerSelection.RightPlayer; }

        private void numericUpDownTaskCounter_ValueChanged(object sender, EventArgs e) { this.business.TaskCounter = (int)this.numericUpDownTaskCounter.Value; }

        private void buttonTrue_Click(object sender, EventArgs e) { this.business.True(); }
        private void buttonFalse_Click(object sender, EventArgs e) { this.business.False(); }

        private void buttonSelectQuestion_Click(object sender, EventArgs e) {
            int id = 0;
            if (Helper.tryParseIndexFromControl(sender as Control, out id)) this.business.SelectQuestion(id);
        }
        private void buttonSetQuestionUsed_Click(object sender, EventArgs e) {
            int id = 0;
            if (Helper.tryParseIndexFromControl(sender as Control, out id)) this.business.SetActiveQuestionUsed(id);
        }

        private void buttonSetQuestionUsed_Click_1(object sender, EventArgs e) {
            if (this.tabControlIdle.SelectedTab == this.tabPageCat1) {
                this.business.SetIdleQuestionUsed(this.listBoxIdle1.SelectedItem as DatasetContent);
            }
            else if (this.tabControlIdle.SelectedTab == this.tabPageCat2) {
                this.business.SetIdleQuestionUsed(this.listBoxIdle2.SelectedItem as DatasetContent);
            }
            else if (this.tabControlIdle.SelectedTab == this.tabPageCat3) {
                this.business.SetIdleQuestionUsed(this.listBoxIdle3.SelectedItem as DatasetContent);
            }
            else if (this.tabControlIdle.SelectedTab == this.tabPageCat4) {
                this.business.SetIdleQuestionUsed(this.listBoxIdle4.SelectedItem as DatasetContent);
            }
            else if (this.tabControlIdle.SelectedTab == this.tabPageCat5) {
                this.business.SetIdleQuestionUsed(this.listBoxIdle5.SelectedItem as DatasetContent);
            }
            else if (this.tabControlIdle.SelectedTab == this.tabPageCat6) {
                this.business.SetIdleQuestionUsed(this.listBoxIdle6.SelectedItem as DatasetContent);
            }
        }

        private void buttonSetQuestionIdle_Click(object sender, EventArgs e) { this.business.SetUsedQuestionIdle(this.listBoxUsedDataList.SelectedItem as DatasetContent); }

        private void tabControlIdle_SelectedIndexChanged(object sender, EventArgs e) {
            this.business.PreselectedCategory = (DatasetContent.Categories)this.tabControlIdle.SelectedIndex;
        }
        private void listBoxIdle1_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.tabControlIdle.SelectedTab == this.tabPageCat1) this.business.PreselectedDatasetIndex = this.listBoxIdle1.SelectedIndex;
        }
        private void listBoxIdle2_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.tabControlIdle.SelectedTab == this.tabPageCat2) this.business.PreselectedDatasetIndex = this.listBoxIdle2.SelectedIndex;
        }
        private void listBoxIdle3_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.tabControlIdle.SelectedTab == this.tabPageCat3) this.business.PreselectedDatasetIndex = this.listBoxIdle3.SelectedIndex;
        }
        private void listBoxIdle4_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.tabControlIdle.SelectedTab == this.tabPageCat4) this.business.PreselectedDatasetIndex = this.listBoxIdle4.SelectedIndex;
        }
        private void listBoxIdle5_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.tabControlIdle.SelectedTab == this.tabPageCat5) this.business.PreselectedDatasetIndex = this.listBoxIdle5.SelectedIndex;
        }
        private void listBoxIdle6_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.tabControlIdle.SelectedTab == this.tabPageCat6) this.business.PreselectedDatasetIndex = this.listBoxIdle6.SelectedIndex;
        }


        private void buttonVinsert_GameIn_Click(object sender, EventArgs e) { this.business.Vinsert_GameIn(); }
        private void buttonVinsert_TaskCounterIn_Click(object sender, EventArgs e) { this.business.Vinsert_TaskCounterIn(); }
        private void buttonVinsert_StartTimeout_Click(object sender, EventArgs e) { this.business.Vinsert_StartTimeout(); }
        private void buttonVinsert_SetTaskCounter_Click(object sender, EventArgs e) { this.business.Vinsert_SetTaskCounter(); }
        private void buttonVinsert_TaskCounterOut_Click(object sender, EventArgs e) { this.business.Vinsert_TaskCounterOut(); }
        private void buttonVinsert_QuestionOut_Click(object sender, EventArgs e) { this.business.Vinsert_QuestionOut(); }
        private void buttonVinsert_TaskCounterOut_01_Click(object sender, EventArgs e) { this.business.Vinsert_TaskCounterOut(); }
        private void buttonVinsert_GameOut_Click(object sender, EventArgs e) { this.business.Vinsert_GameOut(); }

        private void buttonVhost_GameIn_Click(object sender, EventArgs e) { this.business.Vhost_GameIn(); }
        private void buttonVhost_SetGame_Click(object sender, EventArgs e) { this.business.Vhost_SetGame(); }
        private void buttonVhost_SetGame_01_Click(object sender, EventArgs e) { this.business.Vhost_SetGame(); }
        private void buttonVhost_GameOut_Click(object sender, EventArgs e) { this.business.Vhost_GameOut(); }

        private void buttonVplayers_GameIn_Click(object sender, EventArgs e) { this.business.Vplayers_GameIn(); }
        private void buttonVplayers_SetGame_Click(object sender, EventArgs e) { this.business.Vplayers_SetGame(); }
        private void buttonVplayers_GameOut_Click(object sender, EventArgs e) { this.business.Vplayers_GameOut(); }
        private void buttonVplayers_InitPlayDice_Click(object sender, EventArgs e) { this.business.Vplayers_InitPlayDice(); }
        private void buttonVplayers_GameOut_01_Click(object sender, EventArgs e) { this.business.Vplayers_GameOut(); }

        #endregion
    }
}
