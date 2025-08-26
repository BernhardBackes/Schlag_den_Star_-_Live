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

using Cliparts.SchlagDenStarLive.MainApp.Settings;

//#region Properties
//#endregion

//#region Funktionen
//#endregion

//#region Events.Incoming
//#endregion

//#region Events.Controls
//#endregion

namespace Cliparts.SchlagDenStarLive.MainApp {

    public partial class MainForm : Form {

        #region Properties

        private MainBusiness business;

        private bool lastKeyControl = false;

        private bool keyControl = false;

        private bool midiOutStatus = false;

        private Settings.Business settingsHandler;

        private Devantech.Controller devantechHandler;
        private Devantech.Views.WinForms.FormController devantechForm;

        private Content.Business contentHandler;
        private Content.BusinessForm contentForm;

        private VRemote4.HandlerSi.Business ventuzHandler;
        private VRemote4.HandlerSi.BusinessForm ventuzHandlerForm;

        private VentuzScenes.Insert.Business ventuzInsertScene;
        private VentuzScenes.Insert.BusinessForm ventuzInsertSceneForm;

        private VentuzScenes.Fullscreen.Business ventuzFullscreenScene;
        private VentuzScenes.Fullscreen.BusinessForm ventuzFullscreenSceneForm;

        private VentuzScenes.Host.Business ventuzHostScene;
        private VentuzScenes.Host.BusinessForm ventuzHostSceneForm;

        private VentuzScenes.Player.Business ventuzLeftPlayerScene;
        private VentuzScenes.Player.BusinessForm ventuzLeftPlayerSceneForm;

        private VentuzScenes.Player.Business ventuzRightPlayerScene;
        private VentuzScenes.Player.BusinessForm ventuzRightPlayerSceneForm;

        private VRemote4.HandlerSi.Client.Pipe.Business previewPipe;

        #endregion


        #region Funktionen

        public MainForm() {
            InitializeComponent();
            Binding bind;

            this.BackColor = ClipartsColors.DE_DARKBLUE;
            this.Text = String.Format("{0} - Version {1}", Application.ProductName, Application.ProductVersion);

            this.business = new MainBusiness(
                WindowsFormsSynchronizationContext.Current,
                out this.settingsHandler,
                out this.devantechHandler,
                out this.contentHandler,
                out this.ventuzHandler,
                out this.ventuzInsertScene,
                out this.ventuzFullscreenScene,
                out this.ventuzHostScene,
                out this.ventuzLeftPlayerScene,
                out this.ventuzRightPlayerScene);
            this.business.MessagingStatusChanged += this.business_MessagingStatusChanged;
            this.business.MidiOutStatusChanged += this.business_MidiHandler_Out_StatusChanged;
            this.settingsHandler.ContentFilenameChanged += this.settings_ContentFilenameChanged;

            bind = new Binding("Text", this.business, "Car");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxGewinnerCar.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "Gainer");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxGewinnerName.DataBindings.Add(bind);

            string key;
            string[] clientList = ventuzHandler.ClientNameList;
            int index = 0;
            foreach (string item in clientList) {
                VRemote4.HandlerSi.Client.Business client;
                if (ventuzHandler.TryGetClient(item, out client)) {
                    key = "userControlClientState_" + index.ToString("00");
                    VRemote4.HandlerSi.Client.UserControlClientState clientState = this.groupBoxVentuzServer.Controls[key] as VRemote4.HandlerSi.Client.UserControlClientState;
                    if (clientState is VRemote4.HandlerSi.Client.UserControlClientState) {
                        clientState.Pose(client);
                        clientState.BackColor = this.BackColor;
                    }
                }
                index++;
            }

            // buttonLoadInsertScene
            bind = new Binding("Text", this.business, "VentuzInsertSceneStatus");
            bind.Format += (s, e) => { e.Value = ((VRemote4.HandlerSi.Scene.States)e.Value).ToString(); };
            this.buttonLoadInsertScene.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "VentuzInsertSceneStatus");
            bind.Format += this.bind_textBoxStatus_BackColor;
            this.buttonLoadInsertScene.DataBindings.Add(bind);

            // buttonLoadFullscreenScene
            bind = new Binding("Text", this.business, "VentuzFullscreenSceneStatus");
            bind.Format += (s, e) => { e.Value = ((VRemote4.HandlerSi.Scene.States)e.Value).ToString(); };
            this.buttonLoadFullscreenScene.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "VentuzFullscreenSceneStatus");
            bind.Format += this.bind_textBoxStatus_BackColor;
            this.buttonLoadFullscreenScene.DataBindings.Add(bind);

            // buttonLoadHostScene
            bind = new Binding("Text", this.business, "VentuzHostSceneStatus");
            bind.Format += (s, e) => { e.Value = ((VRemote4.HandlerSi.Scene.States)e.Value).ToString(); };
            this.buttonLoadHostScene.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "VentuzHostSceneStatus");
            bind.Format += this.bind_textBoxStatus_BackColor;
            this.buttonLoadHostScene.DataBindings.Add(bind);

            // buttonLoadLeftPlayerScene
            bind = new Binding("Text", this.business, "VentuzLeftPlayerSceneStatus");
            bind.Format += (s, e) => { e.Value = ((VRemote4.HandlerSi.Scene.States)e.Value).ToString(); };
            this.buttonLoadLeftPlayerScene.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "VentuzLeftPlayerSceneStatus");
            bind.Format += this.bind_textBoxStatus_BackColor;
            this.buttonLoadLeftPlayerScene.DataBindings.Add(bind);

            // buttonLoadRightPlayerScene
            bind = new Binding("Text", this.business, "VentuzRightPlayerSceneStatus");
            bind.Format += (s, e) => { e.Value = ((VRemote4.HandlerSi.Scene.States)e.Value).ToString(); };
            this.buttonLoadRightPlayerScene.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "VentuzRightPlayerSceneStatus");
            bind.Format += this.bind_textBoxStatus_BackColor;
            this.buttonLoadRightPlayerScene.DataBindings.Add(bind);

            bind = new Binding("Text", this.ventuzInsertScene, "TimerCurrentTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerCurrentTime.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.ventuzInsertScene, "TimerIsRunning");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorEnabled : this.ForeColor; };
            this.labelTimerCurrentTime.DataBindings.Add(bind);

            this.userControlGameboard.Pose(this.contentHandler.Gameboard);

            this.userControlGameList.Pose(this.contentHandler.GameList);
            this.userControlGameList.EnterControl += this.userControl_EnterControl;
            this.userControlGameList.LeaveControl += this.userControl_LeaveControl;

            this.userControlPlaylist.Pose(this.contentHandler.Playlist);

            this.settings_ContentFilenameChanged(this, new FilenameArgs(this.settingsHandler.ContentFilename));
            this.business_MessagingStatusChanged(this, new Messaging.MessageStatusChangedEventArgs(this.business.MessagingStatus, this.business.MessagingLatestMessage));

            this.setKeyControl(false);

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);

            this.textBoxGewinnerName.DataBindings.Clear();

            this.userControlClientState_00.Dispose();
            this.userControlClientState_01.Dispose();
            this.userControlClientState_02.Dispose();
            this.userControlClientState_03.Dispose();
            this.userControlClientState_04.Dispose();

            this.business.Dispose();
            this.business.MessagingStatusChanged -= this.business_MessagingStatusChanged;
            this.business.MidiOutStatusChanged -= this.business_MidiHandler_Out_StatusChanged;
            this.settingsHandler.ContentFilenameChanged -= this.settings_ContentFilenameChanged;

            this.buttonLoadInsertScene.DataBindings.Clear();
            this.buttonLoadFullscreenScene.DataBindings.Clear();
            this.buttonLoadHostScene.DataBindings.Clear();
            this.buttonLoadLeftPlayerScene.DataBindings.Clear();
            this.buttonLoadRightPlayerScene.DataBindings.Clear();

            this.userControlGameboard.Dispose();

            this.userControlGameList.Dispose();
            this.userControlGameList.EnterControl -= this.userControl_EnterControl;
            this.userControlGameList.LeaveControl -= this.userControl_LeaveControl;

            this.userControlPlaylist.Dispose();
        }

        private void setKeyControl(
            bool keycontrol) {

            this.keyControl = keycontrol;

            this.userControlGameList.KeyControl = keycontrol;

            if (keyControl) {
                this.buttonKeyControl.BackColor = Constants.ColorEnabled;
                this.buttonMidiOut.Text = "MIDI OUT [ M ]";
            }
            else {
                this.buttonKeyControl.BackColor = Constants.ColorDisabled;
                this.buttonMidiOut.Text = "MIDI OUT";
            }
        }

        public void ToggleKeyControl() {
            this.setKeyControl(!this.keyControl);
        }

        public virtual void SetPreviewPipe(VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            this.previewPipe = previewPipe;
        }

        #endregion


        #region Events.Incoming

        private void ventuzInsertSceneForm_Disposed(object sender, EventArgs e) {
            if (this.ventuzInsertSceneForm is VentuzScenes.Insert.BusinessForm) {
                this.ventuzInsertSceneForm.Disposed -= this.ventuzInsertSceneForm_Disposed;
                this.ventuzInsertSceneForm = null;
            }
        }

        private void ventuzFullscreenSceneForm_Disposed(object sender, EventArgs e) {
            if (this.ventuzFullscreenSceneForm is VentuzScenes.Fullscreen.BusinessForm) {
                this.ventuzFullscreenSceneForm.Disposed -= this.ventuzFullscreenSceneForm_Disposed;
                this.ventuzFullscreenSceneForm = null;
            }
        }

        private void business_MidiHandler_Out_StatusChanged(object sender, MidiHandler.StatusChangedArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_MidiHandler_Out_StatusChanged(sender, e)));
            else {

                if (e is MidiHandler.StatusChangedArgs) {

                    this.midiOutStatus = e.IsEnabled && e.IsOpen;

                    if (this.midiOutStatus) this.buttonMidiOut.BackColor = Constants.ColorEnabled;
                    else this.buttonMidiOut.BackColor = Constants.ColorDisabled;

                }
            }
        }

        private void business_MessagingStatusChanged(object sender, Messaging.MessageStatusChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_MessagingStatusChanged(sender, e)));
            else {

                if (e is Messaging.MessageStatusChangedEventArgs) {
                    if (e == null || e.Status == Cliparts.Messaging.ErrorStatus.NoMessage) {
                        this.toolStripStatusLabelError.BackColor = Constants.ColorEnabled;
                        this.toolStripStatusLabelError.Text = "<no error reported>";
                    }
                    else {
                        switch (e.Status) {
                            case Cliparts.Messaging.ErrorStatus.NewMessage:
                                this.toolStripStatusLabelError.BackColor = Constants.ColorDisabled;
                                break;
                            case Cliparts.Messaging.ErrorStatus.MessageAvailable:
                                this.toolStripStatusLabelError.BackColor = Constants.ColorEnabling;
                                break;
                        }
                        this.toolStripStatusLabelError.Text = e.LatestMessage;
                    }

                }

            }
        }

        private void settings_ContentFilenameChanged(object sender, FilenameArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.settings_ContentFilenameChanged(sender, e)));
            else {

                if (e == null || string.IsNullOrEmpty(e.Filename)) {
                    this.toolStripStatusLabelContentFilename.Text = "<no content loaded>";
                }
                else {
                    this.toolStripStatusLabelContentFilename.Text = e.Filename;
                }

            }
        }

        private void devantechForm_Disposed(object sender, EventArgs e) {
            if (this.devantechForm is Devantech.Views.WinForms.FormController) {
                this.devantechForm.Disposed -= this.devantechForm_Disposed;
                this.devantechForm = null;
            }
        }

        private void contentForm_Disposed(object sender, EventArgs e) {
            if (this.contentForm is Content.BusinessForm) {
                this.contentForm.Disposed -= this.contentForm_Disposed;
                this.contentForm = null;
            }
        }

        private void ventuzHandlerForm_Disposed(object sender, EventArgs e) {
            if (this.ventuzHandlerForm is VRemote4.HandlerSi.BusinessForm) {
                this.ventuzHandlerForm.Disposed -= this.ventuzHandlerForm_Disposed;
                this.ventuzHandlerForm = null;
            }
        }

        #endregion

        #region Events.Controls

        private void userControl_EnterControl(object sender, EventArgs e) {
            this.lastKeyControl = this.keyControl;
            if (this.keyControl) this.ToggleKeyControl();
        }

        private void userControl_LeaveControl(object sender, EventArgs e) {
            if (this.keyControl != this.lastKeyControl) this.ToggleKeyControl();
        }

        private void MainForm_BackColorChanged(object sender, EventArgs e) {
            this.groupBoxVentuzServer.BackColor = this.BackColor;
            this.userControlPlaylist.BackColor = this.BackColor;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {      }

        private void MainForm_KeyDown(object sender, KeyEventArgs e) { if (this.keyControl) e.Handled = true; }
        private void MainForm_KeyPress(object sender, KeyPressEventArgs e) { if (this.keyControl) e.Handled = true; }
        private void MainForm_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Escape) this.ToggleKeyControl();
            else {
                if (this.keyControl) {
                    switch (e.KeyCode) {
                        case Keys.M:
                            this.business.ToggleMidiOutStatus();
                            //this.business.ToggleVentuzMuteStatus();
                            break;
                        default:
                            this.userControlGameList.ParseKey(e.KeyCode);
                            break;
                    }
                    e.Handled = true;
                }
            }
        }

        private void MainForm_Resize(object sender, EventArgs e) {
            double width = this.statusStrip.Width * 0.7;
            this.toolStripStatusLabelContentFilename.Width = (int)width;
            this.toolStripStatusLabelError.Width = this.statusStrip.Width - this.toolStripStatusLabelContentFilename.Width - 15;
        }

        private void menuStripFileContent_Click(object sender, EventArgs e) {
            if (this.contentForm == null ||
                this.contentForm.IsDisposed) {
                this.contentForm = new Content.BusinessForm(this, this.contentHandler);
                this.contentForm.Disposed += this.contentForm_Disposed;
            }
            this.contentForm.Show();
            this.contentForm.BringToFront();
        }
        private void menuStripFileContentNew_Click(object sender, EventArgs e) {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "New Content";
            dialog.InitialDirectory = ApplicationAttributes.ContentPath;
            if (File.Exists(this.contentHandler.Filename)) dialog.FileName = this.contentHandler.Filename;
            dialog.DefaultExt = "*.xml";
            dialog.Filter = "XML-File (*.xml)|*.xml|all files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;
            switch (dialog.ShowDialog()) {
                case DialogResult.Cancel:
                    break;
                case DialogResult.OK:
                    this.contentHandler.New(dialog.FileName);
                    break;
            }
            dialog = null;
        }
        private void menuStripFileContentLoad_Click(object sender, EventArgs e) {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Load Content";
            dialog.InitialDirectory = ApplicationAttributes.ContentPath;
            if (File.Exists(this.contentHandler.Filename)) dialog.FileName = this.contentHandler.Filename;
            dialog.DefaultExt = "*.xml";
            dialog.Filter = "XML-File (*.xml)|*.xml|all files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.Multiselect = false;
            dialog.RestoreDirectory = true;
            switch (dialog.ShowDialog()) {
                case DialogResult.Cancel:
                    break;
                case DialogResult.OK:
                    this.contentHandler.Load(dialog.FileName);
                    break;
            }
            dialog = null;
        }
        private void menuStripFileContentSave_Click(object sender, EventArgs e) {
            this.contentHandler.Save();
        }
        private void menuStripFileContentSaveAs_Click(object sender, EventArgs e) {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Save Content As...";
            dialog.InitialDirectory = ApplicationAttributes.ContentPath;
            if (File.Exists(this.contentHandler.Filename)) dialog.FileName = this.contentHandler.Filename;
            dialog.DefaultExt = "*.xml";
            dialog.Filter = "XML-File (*.xml)|*.xml|all files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;
            switch (dialog.ShowDialog()) {
                case DialogResult.Cancel:
                    break;
                case DialogResult.OK:
                    this.contentHandler.SaveAs(dialog.FileName);
                    break;
            }
            dialog = null;
        }

        private void menuStripFileExit_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void menuStripVentuzHandler_Click(object sender, EventArgs e) {
            if (this.ventuzHandlerForm == null) {
                this.ventuzHandlerForm = new VRemote4.HandlerSi.BusinessForm(this.ventuzHandler, this.BackColor);
                this.ventuzHandlerForm.Disposed += this.ventuzHandlerForm_Disposed;
            }
            this.ventuzHandlerForm.Show();
            this.ventuzHandlerForm.BringToFront();
        }

        private void menuStripVentuzScenesInsert_Click(object sender, EventArgs e) {
            if (this.ventuzInsertSceneForm == null) {
                this.ventuzInsertSceneForm = new VentuzScenes.Insert.BusinessForm(this.ventuzInsertScene);
                this.ventuzInsertSceneForm.BackColor = ClipartsColors.DE_DARKBLUE;
                this.ventuzInsertSceneForm.Text = "[V] Scenes: Insert";
                this.ventuzInsertSceneForm.Disposed += this.ventuzInsertSceneForm_Disposed;
            }
            this.ventuzInsertSceneForm.Show();
            this.ventuzInsertSceneForm.BringToFront();
        }

        private void menuStripVentuzScenesFullscreen_Click(object sender, EventArgs e) {
            if (this.ventuzFullscreenSceneForm == null) {
                this.ventuzFullscreenSceneForm = new VentuzScenes.Fullscreen.BusinessForm(this.ventuzFullscreenScene);
                this.ventuzFullscreenSceneForm.BackColor = ClipartsColors.DE_DARKBLUE;
                this.ventuzFullscreenSceneForm.Text = "[V] Scenes: Fullscreen";
                this.ventuzFullscreenSceneForm.Disposed += this.ventuzFullscreenSceneForm_Disposed;
            }
            this.ventuzFullscreenSceneForm.Show();
            this.ventuzFullscreenSceneForm.BringToFront();
        }

        private void menuStripExtrasIONet_Click(object sender, EventArgs e) {
            this.business.ShowIOnet();
        }

        private void menuStripExtrasMidi_Click(object sender, EventArgs e) {
            this.business.ShowMidiForm();
        }

        private void menuStripExtrasUpdateMidi_Click(object sender, EventArgs e) {
            this.business.UpdateMidiEventList();
        }

        private void menuStripExtrasAMB_Click(object sender, EventArgs e) {
            this.business.ShowAMBForm();
        }

        private void menuStripExtrasDevantech_Click(object sender, EventArgs e) {
            if (this.devantechForm == null ||
                this.devantechForm.IsDisposed) {
                this.devantechForm = new Devantech.Views.WinForms.FormController(this.devantechHandler);
                this.devantechForm.Disposed += this.devantechForm_Disposed;
            }
            this.devantechForm.BackColor = this.BackColor;
            this.devantechForm.Show();
            this.devantechForm.BringToFront();
        }

        private void groupBoxVentuzServer_BackColorChanged(object sender, EventArgs e) {
            foreach (Control control in this.groupBoxVentuzServer.Controls) {
                control.BackColor = this.BackColor;
            }
        }

        private void buttonKeyControl_Click(object sender, EventArgs e) {
            this.ToggleKeyControl();
        }

        private void buttonMidiOut_Click(object sender, EventArgs e) {
            this.business.ToggleMidiOutStatus();
            //this.business.ToggleVentuzMuteStatus();
        }

        private void buttonResetAll_Click(object sender, EventArgs e) { this.business.ResetAll(); }

        private void buttonSyncAll_Click(object sender, EventArgs e) { this.business.SyncAll(); }

        private void buttonClearGraphic_Click(object sender, EventArgs e) { this.business.ClearGraphic(); }

        private void buttonClearStage_Click(object sender, EventArgs e) { this.business.ClearStage(); }

        private void textBoxGewinnerCar_TextChanged(object sender, EventArgs e) { this.business.Car = this.textBoxGewinnerCar.Text; }
        private void textBoxGewinnerName_TextChanged(object sender, EventArgs e) { this.business.Gainer = this.textBoxGewinnerName.Text; }
        private void buttonGewinnerStart_Click(object sender, EventArgs e) { this.business.StartGainerInsert(); }
        private void buttonGewinnerReset_Click(object sender, EventArgs e) { this.business.ResetGainerInsert(); }

        private void buttonTimerIn_Click(object sender, EventArgs e) { this.ventuzInsertScene.TimerIn(); }
        private void buttonTimerOut_Click(object sender, EventArgs e) { this.ventuzInsertScene.TimerOut(); }
        private void buttonTimerStart_Click(object sender, EventArgs e) { this.ventuzInsertScene.StartTimer(); }
        private void buttonTimerStop_Click(object sender, EventArgs e) { this.ventuzInsertScene.StopTimer(); }
        private void buttonTimerContinue_Click(object sender, EventArgs e) { this.ventuzInsertScene.ContinueTimer(); }
        private void buttonTimerReset_Click(object sender, EventArgs e) { this.ventuzInsertScene.ResetTimer(); }

        void bind_textBoxStatus_BackColor(object sender, ConvertEventArgs e) {
            switch ((VRemote4.HandlerSi.Scene.States)e.Value) {
                case VRemote4.HandlerSi.Scene.States.Unloaded:
                    e.Value = Constants.ColorDisabled;
                    break;
                case VRemote4.HandlerSi.Scene.States.Loading:
                    e.Value = Constants.ColorEnabling;
                    break;
                case VRemote4.HandlerSi.Scene.States.Available:
                default:
                    e.Value = Constants.ColorEnabled;
                    break;
            }
        }

        private void buttonLoadInsertScene_Click(object sender, EventArgs e) { this.business.LoadVentuzInsertScene(); }
        private void buttonLoadFullscreenScene_Click(object sender, EventArgs e) { this.business.LoadVentuzFullscreenScene(); }
        private void buttonLoadHostScene_Click(object sender, EventArgs e) { this.business.LoadVentuzHostScene(); }
        private void buttonLoadLeftPlayerScene_Click(object sender, EventArgs e) { this.business.LoadVentuzLeftPlayerScene(); }
        private void buttonLoadRightPlayerScene_Click(object sender, EventArgs e) { this.business.LoadVentuzRightPlayerScene(); }

        private void toolStripStatusLabelError_Click(object sender, EventArgs e) {
            this.business.ShowMessagingForm();
        }

        private void buttonShow_Click(object sender, EventArgs e) { this.business.FadeStageOut(); }
        private void buttonFadeStageIn_Click(object sender, EventArgs e) { this.business.FadeStageIn(); }

        private void buttonLiveVideoIn_Click(object sender, EventArgs e) { this.business.LiveVideoIn(); }
        private void buttonLiveVideoOut_Click(object sender, EventArgs e) { this.business.LiveVideoOut(); }

        #endregion

    }
}
