using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.VideoMemory {

    public partial class UserControlGame : _Base.Score.UserControlGame {

        #region Properties

        private Business business;

        private VRemote4.HandlerSi.Business localVentuzHandler;
        private VRemote4.HandlerSi.BusinessForm localVentuzHandlerForm;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Business localVentuzHandler) {
            base.Pose(business);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged; 

            //Binding bind;

            this.localVentuzHandler = localVentuzHandler;

            VRemote4.HandlerSi.Client.Business gameboardClient;
            if (this.localVentuzHandler.TryGetClient("Gameboard", out gameboardClient)) this.userControlClientStateGameboard.Pose(gameboardClient);
            this.userControlClientStateGameboard.BackColor = this.BackColor;

            object[] items = new object[] { "reset", "start video", "to in", "set selected", "to selected", "set out", "to out" };
            this.comboBoxGameboardCard_00.BeginUpdate();
            this.comboBoxGameboardCard_00.Items.Add("01");
            this.comboBoxGameboardCard_00.Items.AddRange(items);
            this.comboBoxGameboardCard_00.SelectedIndex = 0;
            this.comboBoxGameboardCard_00.EndUpdate();
            for (int i = 1; i < Business.BuzzerCount; i++) {
                int id = i + 1;
                int column = i % 7;
                int row = i / 7;
                ComboBox comboBoxGameboardCard = new ComboBox();
                comboBoxGameboardCard.Height = this.comboBoxGameboardCard_00.Height;
                comboBoxGameboardCard.Items.Add(id.ToString("00"));
                comboBoxGameboardCard.Items.AddRange(items);
                comboBoxGameboardCard.Left = this.comboBoxGameboardCard_00.Left + (this.comboBoxGameboardCard_00.Width + this.comboBoxGameboardCard_00.Margin.Left + this.comboBoxGameboardCard_00.Margin.Right) * column;
                comboBoxGameboardCard.Name = string.Format("comboBoxGameboardCard_{0}", i.ToString("00"));
                comboBoxGameboardCard.Top = this.comboBoxGameboardCard_00.Top + (this.comboBoxGameboardCard_00.Height + this.comboBoxGameboardCard_00.Margin.Top + this.comboBoxGameboardCard_00.Margin.Bottom) * row;
                comboBoxGameboardCard.Width = this.comboBoxGameboardCard_00.Width;
                comboBoxGameboardCard.SelectedIndex = 0;
                comboBoxGameboardCard.SelectedIndexChanged += this.comboBoxGameboardCard_SelectedIndexChanged;
                this.panelGameboard.Controls.Add(comboBoxGameboardCard);
            }

            this.labelGameClass.Text = this.business.ClassInfo;

            this.SetListBoxSelectedBuzzers(this.business.SelectedBuzzers);
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

            this.userControlClientStateGameboard.Dispose();

        }

        protected override void buildStepList() {

            int index = 0;
            stepAction step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_LoadScene);
            step.AddButton(this.buttonVfullscreen_LoadScene);
            step.AddButton(this.buttonVgameboard_Start);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_Init);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVfullscreen_Init);
            step.AddButton(this.buttonVstage_Clear);
            step.AddButton(this.buttonVstage_Init);
            step.AddButton(this.buttonVgameboard_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_ShowGame);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_BorderIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex);
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVstage_SetScore);
            step.AddButton(this.buttonGame_StartNext);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_BorderOut);
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
            step.AddButton(this.buttonVfullscreen_UnloadScene);
            step.AddButton(this.buttonVfullscreen_ShowGameboard);
            step.AddButton(this.buttonVgameboard_ShutDown);
            step.AddButton(this.buttonGame_SetWinner);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_UpdateGameboard);
            this.stepList.Add(step);

            if (index < this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to short");
            if (index > this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to long");
        }

        private void SetListBoxSelectedBuzzers(
            List<SelectedBuzzer> value)
        {
            this.listBoxSelectedBuzzers.BeginUpdate();
            this.listBoxSelectedBuzzers.Items.Clear();
            if (value is List<SelectedBuzzer> &&
                value.Count > 0) this.listBoxSelectedBuzzers.Items.AddRange(value.ToArray());
            this.listBoxSelectedBuzzers.EndUpdate();
        }

        #endregion


        #region Events.Incoming

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.business_PropertyChanged(sender, e);
            if (e.PropertyName == nameof(this.business.SelectedBuzzers)) this.SetListBoxSelectedBuzzers(this.business.SelectedBuzzers);
        }

        private void localVentuzHandlerForm_Disposed(object sender, EventArgs e) {
            if (this.localVentuzHandlerForm is VRemote4.HandlerSi.BusinessForm) {
                this.localVentuzHandlerForm.Disposed -= this.localVentuzHandlerForm_Disposed;
                this.localVentuzHandlerForm = null;
            }
        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void UserControlGame_BackColorChanged(object sender, EventArgs e) {
            this.userControlClientStateGameboard.BackColor = this.BackColor;
        }

        private void buttonVinsert_BorderIn_Click(object sender, EventArgs e) { this.business.Vinsert_BorderIn(); }
        private void buttonVinsert_BorderOut_Click(object sender, EventArgs e) { this.business.Vinsert_BorderOut(); }

        private void buttonVfullscreen_Init_Click(object sender, EventArgs e) { this.business.Vfullscreen_Init(); }

        private void buttonVgameboard_Start_Click(object sender, EventArgs e) { this.business.Vgameboard_Start(); }
        private void buttonVgameboard_Init_Click(object sender, EventArgs e) { this.business.Vgameboard_Init(); }
        private void buttonVgameboard_ShutDown_Click(object sender, EventArgs e) { this.business.Vgameboards_ShutDown(); }

        private void buttonGame_StartNext_Click(object sender, EventArgs e) { this.business.StartNext(); }

        private void buttonShowVentuzHandler_Click(object sender, EventArgs e) {
            if (this.localVentuzHandlerForm == null) {
                this.localVentuzHandlerForm = new VRemote4.HandlerSi.BusinessForm(this.localVentuzHandler, this.BackColor);
                this.localVentuzHandlerForm.Disposed += this.localVentuzHandlerForm_Disposed;
            }
            this.localVentuzHandlerForm.Show();
            this.localVentuzHandlerForm.BringToFront();
        }

        private void comboBoxGameboardCard_SelectedIndexChanged(object sender, EventArgs e) {
            ComboBox comboBoxGameboardCard = sender as ComboBox;
            if (comboBoxGameboardCard is ComboBox &&
                comboBoxGameboardCard.SelectedIndex > 0) {
                int index;
                if (Helper.tryParseIndexFromControl(comboBoxGameboardCard, out index)) {
                    if (comboBoxGameboardCard.Text == "reset") {
                        this.business.Vfullscreen_Reset(index + 1);
                        this.business.Vgameboard_Reset(index + 1);
                    }
                    else if (comboBoxGameboardCard.Text == "start video") {
                        this.business.Vfullscreen_PlayVideo(index + 1);
                        this.business.Vinsert_PlayVideo(index + 1);
                    }
                    else if (comboBoxGameboardCard.Text == "to in") {
                        this.business.Vfullscreen_ToIn(index + 1);
                        this.business.Vgameboard_ToIn(index + 1);
                    }
                    else if (comboBoxGameboardCard.Text == "set selected") {
                        this.business.Vfullscreen_SetSelected(index + 1);
                        this.business.Vgameboard_SetSelected(index + 1);
                    }
                    else if (comboBoxGameboardCard.Text == "to selected") {
                        this.business.Vfullscreen_ToSelected(index + 1);
                        this.business.Vgameboard_ToSelected(index + 1);
                    }
                    else if (comboBoxGameboardCard.Text == "set out") {
                        this.business.Vfullscreen_SetOut(index + 1);
                        this.business.Vgameboard_SetOut(index + 1);
                    }
                    else if (comboBoxGameboardCard.Text == "to out") {
                        this.business.Vfullscreen_ToOut(index + 1);
                        this.business.Vgameboard_ToOut(index + 1);
                    }
                }
                comboBoxGameboardCard.SelectedIndex = 0;
            }
        }

        #endregion

    }

}
