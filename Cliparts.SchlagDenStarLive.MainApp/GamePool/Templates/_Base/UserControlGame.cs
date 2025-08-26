using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.Settings;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base {

    public partial class UserControlGame : UserControl {

        public static Delegate[] ControlHasEventHandler(Control control, string eventName) {
            EventHandlerList events =
                (EventHandlerList)
                typeof(Component)
                 .GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance)
                 .GetValue(control, null);

            object key = typeof(Control)
                .GetField(eventName, BindingFlags.NonPublic | BindingFlags.Static)
                .GetValue(null);

            Delegate handlers = events[key];

            if (handlers != null) return handlers.GetInvocationList();
            else return null;
        }

        public delegate int NextStep(int stepIndex);

        internal class stepAction {
            int index;
            NextStep nextStep;
            object[] parameter = new object[] { null, null };
            List<Button> buttonList = new List<Button>();
            public stepAction(
                int index,
                NextStep nextStep) {
                this.index = index;
                this.nextStep = nextStep;
            }
            public void AddButton(
                Button clickButton) {
                if (clickButton is Button) this.buttonList.Add(clickButton);
            }
            public int RunAction(
                Control parent) {
                foreach (Button item in this.buttonList) {
                    if (parent != null &&
                        item is Button &&
                        item.Enabled) {
                        Delegate[] handlerList = ControlHasEventHandler(item, "EventClick");
                        if (handlerList is Delegate[]) {
                            foreach (Delegate handler in handlerList) parent.BeginInvoke(handler, parameter);
                        }
                    }
                }
                return this.nextStep(this.index);
            }
        }

        #region Properties

        private Business business;

        protected bool adjustingGUI;

        protected bool keyControl = false;
        public bool KeyControl { set { if (this.keyControl != value) this.adjustKeyControl(value); } }

        internal List<stepAction> stepList = new List<stepAction>();

        protected int stepsMaxIndex = 0;
        private int nextStepIndex_hlp = -1;
        protected int nextStepIndex {
            get { return this.nextStepIndex_hlp; }
            set {
                if (value < 0) value = 0;
                if (value > this.stepsMaxIndex) value = this.stepsMaxIndex;
                if (this.nextStepIndex_hlp != value) {
                    this.nextStepIndex_hlp = value;
                    this.adjustStepButtons(this.keyControl, value);
                }
            }
        }

        protected int nextContestantStepIndex;
        protected int winnerStepIndex;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();
        }

        public virtual void Pose(
            Business business) {

            Binding bind;

            this.buildStepList();

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            bind = new Binding("Text", this.business, "LeftPlayerName");
            this.textBoxLeftPlayerName.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerName");
            this.textBoxRightPlayerName.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "InsertSceneStatus");
            bind.Format += this.bind_labelScene_BackColor;
            this.labelVinsert.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "FullscreenSceneStatus");
            bind.Format += this.bind_labelScene_BackColor;
            this.labelVfullscreen.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "HostSceneStatus");
            bind.Format += this.bind_labelScene_BackColor;
            this.labelVhost.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "LeftPlayerSceneStatus");
            bind.Format += this.bind_labelScene_BackColor;
            this.labelVleftplayer.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "RightPlayerSceneStatus");
            bind.Format += this.bind_labelScene_BackColor;
            this.labelVrightplayer.DataBindings.Add(bind);

            this.panelStepMarker.SendToBack();

            this.labelGameClass.Text = this.business.TypeIdentifier.ToString();

            this.setWinner();
        }

        public virtual new void Dispose() {
            this.business.PropertyChanged -= this.business_PropertyChanged;
            this.labelVinsert.DataBindings.Clear();
            this.labelVfullscreen.DataBindings.Clear();
        }

        protected virtual void buildStepList() {
        }

        protected int patchStepButtons() {
            int index = 0;
            int value;
            foreach (Control item in this.Controls) {
                if (item.Name.StartsWith("buttonStep_")) {
                    if (Helper.tryParseIndexFromControl(item, out value) &&
                        index < value) index = value;
                    if (!Helper.controlHasEventHandler(item, "EventClick")) item.Click += new EventHandler(this.buttonStep_Click);
                }
            }
            return index;
        }

        protected virtual void adjustKeyControl(bool isEnabled) {
            this.keyControl = isEnabled;
            this.adjustStepButtons(this.keyControl, this.nextStepIndex);
        }

        protected virtual void adjustStepButtons(
            bool keyControl,
            int nextStep) {
            int index;
            for (int i = 0; i <= this.stepsMaxIndex; i++) {
                string key = "buttonStep_" + i.ToString("00");
                Button button = this.Controls[key] as Button;
                if (button is Button) {
                    if (Helper.tryParseIndexFromControl(button, out index)) {
                        if (index == nextStep) {
                            this.panelStepMarker.Height = button.Height;
                            this.panelStepMarker.Top = button.Top;
                            this.panelStepMarker.Visible = keyControl;
                            if (keyControl) button.BackColor = Color.OrangeRed;
                            else button.BackColor = Color.Gold;
                        }
                        else {
                            button.UseVisualStyleBackColor = true;
                        }
                    }
                }
            }
        }

        protected void doStep(int stepIndex) {
            if (stepIndex >= 0 &&
                stepIndex < this.stepList.Count) this.nextStepIndex = this.stepList[stepIndex].RunAction(this);
            else this.on_Error("doStep", string.Format("stepIndex (={0}) out of stepList range (0-{1})", stepIndex.ToString(), ((int)this.stepList.Count - 1).ToString()));
        }

        public virtual void Activate(
            bool keycontrol) {
            this.KeyControl = keycontrol;
            this.nextStepIndex = 0;
        }

        public virtual void ParseKey(
            Keys keycode) {
            if (this.keyControl) {
                switch (keycode) {
                    case Keys.Space:
                        this.doStep(this.nextStepIndex);
                        break;
                    case Keys.Up:
                        this.nextStepIndex--;
                        break;
                    case Keys.Down:
                        this.nextStepIndex++;
                        break;
                    case Keys.PageUp:
                        this.nextStepIndex = 0;
                        break;
                    case Keys.PageDown:
                        this.nextStepIndex = this.stepsMaxIndex;
                        break;
                }
            }
        }

        public void SetStep(int stepIndex) { this.nextStepIndex = stepIndex; }
        public void SetToNextStep() { this.nextStepIndex++; }

        private void setWinner() {
            switch (this.business.Winner) {
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.LeftPlayer:
                    this.buttonLeftPlayerWinner.BackColor = Constants.ColorWinner;
                    this.buttonRightPlayerWinner.UseVisualStyleBackColor = true;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.RightPlayer:
                    this.buttonLeftPlayerWinner.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerWinner.BackColor = Constants.ColorWinner;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.NotSelected:
                default:
                    this.buttonLeftPlayerWinner.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerWinner.UseVisualStyleBackColor = true;
                    break;
            }
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler EnterControl;
        protected void on_EnterControl(object sender, EventArgs e) { if (this.EnterControl != null) this.EnterControl(sender, e); }

        public event EventHandler LeaveControl;
        protected void on_LeaveControl(object sender, EventArgs e) { if (this.LeaveControl != null) this.LeaveControl(sender, e); }

        public event EventHandler<Cliparts.Messaging.ErrorEventArgs> Error;
        protected void on_Error(object subSender, string errorMsg) { this.on_Error(this, new Cliparts.Messaging.ErrorEventArgs(this, subSender, DateTime.Now, errorMsg)); }
        protected void on_Error(object sender, Cliparts.Messaging.ErrorEventArgs e) {
            if (this.Error == null) Console.WriteLine(e.ConsoleText);
            else this.Error(this, e);
        }

        #endregion

        #region Events.Incoming

        protected void error(object sender, Cliparts.Messaging.ErrorEventArgs e) { this.on_Error(sender, e); }

        protected virtual void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "Winner") this.setWinner();
            }
        }

        #endregion

        #region Events.Controls

        protected void control_Enter(object sender, EventArgs e) { this.on_EnterControl(sender, e); }
        protected void control_Leave(object sender, EventArgs e) { this.on_LeaveControl(sender, e); }
        protected void control_EnabledChanged(object sender, EventArgs e) { Helper.setControlBackColor(sender as Control); }

        protected void buttonLeftPlayerWinner_Click(object sender, EventArgs e) {
            this.business.ToggleWinner(Content.Gameboard.PlayerSelection.LeftPlayer);
            if (this.business.Winner == Content.Gameboard.PlayerSelection.LeftPlayer) this.nextStepIndex = this.winnerStepIndex;
        }

        protected void buttonRightPlayerWinner_Click(object sender, EventArgs e) {
            this.business.ToggleWinner(Content.Gameboard.PlayerSelection.RightPlayer);
            if (this.business.Winner == Content.Gameboard.PlayerSelection.RightPlayer) this.nextStepIndex = this.winnerStepIndex;
        }

        protected void bind_labelScene_BackColor(object sender, ConvertEventArgs e) {
            switch ((VRemote4.HandlerSi.Scene.States)e.Value) {
                case VRemote4.HandlerSi.Scene.States.Unloaded:
                    e.Value = this.BackColor;
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

        protected void buttonVgraphic_Clear_Click(object sender, EventArgs e) { this.business.V_ClearGraphic(); }

        protected void buttonVinsert_LoadScene_Click(object sender, EventArgs e) { this.business.Vinsert_LoadScene(); }
        protected void buttonVinsert_UnloadScene_Click(object sender, EventArgs e) { this.business.Vinsert_UnloadScene(); }

        protected void buttonVfullscreen_LoadScene_Click(object sender, EventArgs e) { this.business.Vfullscreen_LoadScene(); }
        protected void buttonVfullscreen_ShowGame_Click(object sender, EventArgs e) { this.business.Vfullscreen_ShowGame(); }
        protected void buttonVfullscreen_ShowTimer_Click(object sender, EventArgs e) { this.business.Vfullscreen_ShowTimer(); }
        protected void buttonVfullscreen_ShowScore_Click(object sender, EventArgs e) { this.business.Vfullscreen_ShowScore(); }
        protected virtual void buttonVfullscreen_ShowFreetext_Click(object sender, EventArgs e) { this.business.Vfullscreen_ShowFreetext(); }
        protected void buttonVfullscreen_UnloadScene_Click(object sender, EventArgs e) { this.business.Vfullscreen_UnloadScene(); }
        protected void buttonVfullscreen_ShowGameboard_Click(object sender, EventArgs e) { this.business.Vfullscreen_ShowGameboard(); }
        protected void buttonVfullscreen_UpdateGameboard_Click(object sender, EventArgs e) { this.business.Vfullscreen_UpdateGameboard(); }

        protected void buttonVstage_Clear_Click(object sender, EventArgs e) { this.business.V_ClearStage(); }

        protected void buttonVhost_LoadScene_Click(object sender, EventArgs e) { this.business.Vhost_LoadScene(); }
        protected void buttonVhost_UnloadScene_Click(object sender, EventArgs e) { this.business.Vhost_UnloadScene(); }

        protected void buttonVleftplayer_LoadScene_Click(object sender, EventArgs e) { this.business.Vleftplayer_LoadScene(); }
        protected void buttonVleftplayer_UnloadScene_Click(object sender, EventArgs e) { this.business.Vleftplayer_UnloadScene(); }

        protected void buttonVrightplayer_LoadScene_Click(object sender, EventArgs e) { this.business.Vrightplayer_LoadScene(); }
        protected void buttonVrightplayer_UnloadScene_Click(object sender, EventArgs e) { this.business.Vrightplayer_UnloadScene(); }

        protected void buttonGame_Init_Click(object sender, EventArgs e) { this.business.Init(); }
        protected void buttonGame_SetWinner_Click(object sender, EventArgs e) { this.business.SetWinner(); }

        protected void buttonStep_Click(object sender, EventArgs e) {
            Button button = sender as Button;
            int index;
            if (button is Button &&
                Helper.tryParseIndexFromControl(button, out index)) this.doStep(index);
        }

        #endregion
    }

}
