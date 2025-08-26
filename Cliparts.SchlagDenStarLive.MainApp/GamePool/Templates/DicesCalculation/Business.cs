using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Cliparts.Serialization;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.DicesCalculation;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.DicesCalculation {

    public class Input : INotifyPropertyChanged {

        #region Properties

        private string text = string.Empty;
        public string Text {
            get { return this.text; }
            private set {
                if (this.text != value) {
                    this.text = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int dice_1 = 0;
        public int Dice_1 {
            get { return this.dice_1; }
            private set {
                if (this.dice_1 != value) {
                    this.dice_1 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int dice_2 = 0;
        public int Dice_2 {
            get { return this.dice_2; }
            private set {
                if (this.dice_2 != value) {
                    this.dice_2 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int dice_3 = 0;
        public int Dice_3 {
            get { return this.dice_3; }
            private set {
                if (this.dice_3 != value) {
                    this.dice_3 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Tablet.Operations operation_1 = Tablet.Operations.Clear;
        public Tablet.Operations Operation_1 {
            get { return this.operation_1; }
            private set {
                if (this.operation_1 != value) {
                    this.operation_1 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Tablet.Operations operation_2 = Tablet.Operations.Clear;
        public Tablet.Operations Operation_2 {
            get { return this.operation_2; }
            private set {
                if (this.operation_2 != value) {
                    this.operation_2 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? result = null;
        public int? Result {
            get { return this.result; }
            private set {
                if (this.result != value) {
                    this.result = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public void SetText(
            string text) {
            int dice_1 = 0;
            Tablet.Operations operation_1 = Tablet.Operations.Clear;
            int dice_2 = 0;
            Tablet.Operations operation_2 = Tablet.Operations.Clear;
            int dice_3 = 0;

            text = text.ToLower().Trim().Replace(" ", "");
            if (text.Length >= 5) {
                if (int.TryParse(text.Substring(0, 1), out dice_1)) {
                    if (text.Substring(1, 1) == "+") operation_1 = Tablet.Operations.Add;
                    else if (text.Substring(1, 1) == "-") operation_1 = Tablet.Operations.Subtract;
                    else if (text.Substring(1, 1) == "*") operation_1 = Tablet.Operations.Multiply;
                    else if (text.Substring(1, 1) == "x") operation_1 = Tablet.Operations.Multiply;
                    else if (text.Substring(1, 1) == "/") operation_1 = Tablet.Operations.Divide;
                    else if (text.Substring(1, 1) == "÷") operation_1 = Tablet.Operations.Divide;
                    if (operation_1 != Tablet.Operations.Clear) {
                        if (int.TryParse(text.Substring(2, 1), out dice_2)) {
                            if (text.Substring(3, 1) == "+") operation_2 = Tablet.Operations.Add;
                            else if (text.Substring(3, 1) == "-") operation_2 = Tablet.Operations.Subtract;
                            else if (text.Substring(3, 1) == "*") operation_2 = Tablet.Operations.Multiply;
                            else if (text.Substring(3, 1) == "x") operation_2 = Tablet.Operations.Multiply;
                            else if (text.Substring(3, 1) == "/") operation_2 = Tablet.Operations.Divide;
                            else if (text.Substring(3, 1) == "÷") operation_2 = Tablet.Operations.Divide;
                        }
                        if (operation_2 != Tablet.Operations.Clear) int.TryParse(text.Substring(4, 1), out dice_3);
                    }
                }
            }

            this.SetValues(dice_1, operation_1, dice_2, operation_2, dice_3);
        }

        public void SetValues(
            int dice_1,
            Tablet.Operations operation_1,
            int dice_2,
            Tablet.Operations operation_2,
            int dice_3) {
            int? result = null;
            string text = string.Empty;
            if (dice_1 > 0) {
                text = dice_1.ToString();
                if (operation_1 != Tablet.Operations.Clear) {
                    switch (operation_1) {
                        case Tablet.Operations.Add:
                            text += "+";
                            break;
                        case Tablet.Operations.Subtract:
                            text += "-";
                            break;
                        case Tablet.Operations.Multiply:
                            text += "x";
                            break;
                        case Tablet.Operations.Divide:
                            text += "÷";
                            break;
                    }
                    if (dice_2 > 0) {
                        text += dice_2.ToString();
                        if (operation_2 != Tablet.Operations.Clear) {
                            switch (operation_2) {
                                case Tablet.Operations.Add:
                                    text += "+";
                                    break;
                                case Tablet.Operations.Subtract:
                                    text += "-";
                                    break;
                                case Tablet.Operations.Multiply:
                                    text += "x";
                                    break;
                                case Tablet.Operations.Divide:
                                    text += "÷";
                                    break;
                            }
                            if (dice_3 > 0) {
                                text += dice_3.ToString();
                                result = this.calcResult(dice_1, operation_1, dice_2, operation_2, dice_3);
                                if (result.HasValue) text += "=" + result.Value.ToString();
                            }
                        }

                    }
                }
            }

            this.Dice_1 = dice_1;
            this.Operation_1 = operation_1;
            this.Dice_2 = dice_2;
            this.Operation_2 = operation_2;
            this.Dice_3 = dice_3;

            this.Result = result;

            this.Text = text;
        }

        private int? calcResult(
            int dice_1,
            Tablet.Operations operation_1,
            int dice_2,
            Tablet.Operations operation_2,
            int dice_3) {
            double value = 0;

            if (dice_1 > 0 &&
                operation_1 != Tablet.Operations.Clear &&
                dice_2 > 0 &&
                operation_2 != Tablet.Operations.Clear &&
                dice_3 > 0) {
                switch (operation_1) {
                    case Tablet.Operations.Add:
                        value = dice_1 + dice_2;
                        break;
                    case Tablet.Operations.Subtract:
                        value = dice_1 - dice_2;
                        break;
                    case Tablet.Operations.Multiply:
                        value = dice_1 * dice_2;
                        break;
                    case Tablet.Operations.Divide:
                        value = (double)dice_1 / (double)dice_2;
                        break;
                }
                switch (operation_2) {
                    case Tablet.Operations.Add:
                        value += dice_3;
                        break;
                    case Tablet.Operations.Subtract:
                        value -= dice_3;
                        break;
                    case Tablet.Operations.Multiply:
                        value = value * dice_3;
                        break;
                    case Tablet.Operations.Divide:
                        value = value / (double)dice_3;
                        break;
                }
            }

            if (value.ToString().Contains(",")) {
                //ungültig weil Kommazahl
                return null;
            }
            else {
                try { return Convert.ToInt32(value); }
                catch (Exception) { return null; }         
            }
        }

        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_PropertyChanged([CallerMemberName]string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }

    public class Business : _Base.Score.Business {

        #region Properties

        private int timerPositionX = 0;
        public int TimerPositionX {
            get { return this.timerPositionX; }
            set {
                if (this.timerPositionX != value) {
                    this.timerPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                }
            }
        }

        private int timerPositionY = 0;
        public int TimerPositionY {
            get { return this.timerPositionY; }
            set {
                if (this.timerPositionY != value) {
                    this.timerPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.Timer.Styles timerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
        public VentuzScenes.GamePool._Modules.Timer.Styles TimerStyle {
            get { return this.timerStyle; }
            set {
                if (this.timerStyle != value) {
                    this.timerStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                    this.Vfullscreen_SetTimer();
                }
            }
        }

        private int timerStartTime = 8;
        public int TimerStartTime {
            get { return this.timerStartTime; }
            set {
                if (this.timerStartTime != value) {
                    this.timerStartTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                    this.Vfullscreen_SetTimer();
                }
            }
        }

        private int timerExtraTime = 60;
        public int TimerExtraTime {
            get { return this.timerExtraTime; }
            set {
                if (this.timerExtraTime != value) {
                    this.timerExtraTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                    this.Vfullscreen_SetTimer();
                }
            }
        }

        private int timerAlarmTime1 = 3;
        public int TimerAlarmTime1 {
            get { return this.timerAlarmTime1; }
            set {
                if (this.timerAlarmTime1 != value) {
                    this.timerAlarmTime1 = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                    this.Vfullscreen_SetTimer();
                }
            }
        }

        private int timerAlarmTime2 = -1;
        public int TimerAlarmTime2 {
            get { return this.timerAlarmTime2; }
            set {
                if (this.timerAlarmTime2 != value) {
                    this.timerAlarmTime2 = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                    this.Vfullscreen_SetTimer();
                }
            }
        }

        private int timerStopTime = 0;
        public int TimerStopTime {
            get { return this.timerStopTime; }
            set {
                if (this.timerStopTime != value) {
                    this.timerStopTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                    this.Vfullscreen_SetTimer();
                }
            }
        }

        private bool runExtraTime = false;
        [NotSerialized]
        public bool RunExtraTime {
            get { return this.runExtraTime; }
            set {
                if (this.runExtraTime != value) {
                    this.runExtraTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                    this.Vfullscreen_SetTimer();
                }
            }
        }

        private int timerCurrentTime = -1;
        public int TimerCurrentTime {
            get { return this.timerCurrentTime; }
            protected set {
                if (this.timerCurrentTime != value) {
                    this.timerCurrentTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool timerIsRunning = false;
        public bool TimerIsRunning {
            get { return this.timerIsRunning; }
            protected set {
                if (this.timerIsRunning != value) {
                    this.timerIsRunning = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int contentPositionX = 0;
        public int ContentPositionX {
            get { return this.contentPositionX; }
            set {
                if (this.contentPositionX != value) {
                    this.contentPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetContent();
                }
            }
        }

        private int contentPositionY = 0;
        public int ContentPositionY {
            get { return this.contentPositionY; }
            set {
                if (this.contentPositionY != value) {
                    this.contentPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetContent();
                }
            }
        }

        private Input leftPlayerInput = new Input();
        public string LeftPlayerInput {
            get { return this.leftPlayerInput.Text; }
            set {
                if (this.leftPlayerInput.Text != value) {
                    this.leftPlayerInput.SetText(value);
                    this.on_PropertyChanged();
                }
            }
        }

        private Input rightPlayerInput = new Input();
        public string RightPlayerInput {
            get { return this.rightPlayerInput.Text; }
            set {
                if (this.rightPlayerInput.Text != value) {
                    this.rightPlayerInput.SetText(value);
                    this.on_PropertyChanged();
                }
            }
        }

        private int dice_1 = 0;
        public int Dice_1 {
            get { return this.dice_1; }
            set {
                if (this.dice_1 != value) {
                    this.dice_1 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int dice_2 = 0;
        public int Dice_2 {
            get { return this.dice_2; }
            set {
                if (this.dice_2 != value) {
                    this.dice_2 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int dice_3 = 0;
        public int Dice_3 {
            get { return this.dice_3; }
            set {
                if (this.dice_3 != value) {
                    this.dice_3 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int targetResult = 15;
        public int TargetResult {
            get { return this.targetResult; }
            set {
                if (this.targetResult != value) {
                    this.targetResult = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Input solution = new Input();
        public string Solution {
            get { return this.solution.Text; }
            private set {
                if (this.solution.Text != value) {
                    this.solution.SetText(value);
                    this.on_PropertyChanged();
                }
            }
        }

        private Content.Gameboard.PlayerSelection firstResolved = Content.Gameboard.PlayerSelection.NotSelected;
        public Content.Gameboard.PlayerSelection FirstResolved {
            get { return this.firstResolved; }
            set {
                if (this.firstResolved != value) {
                    this.firstResolved = value;
                    this.on_PropertyChanged();
                    this.Vinsert_Buzzer(value);
                }
            }
        }

        private VRemote4.HandlerSi.Business localVentuzHandler;

        private VRemote4.HandlerSi.Client.Business tabletLeftClient;
        private Tablet ventuzTabletLeftScene;
        public VRemote4.HandlerSi.Scene.States VentuzTabletLeftSceneStatus {
            get {
                if (this.ventuzTabletLeftScene is Tablet) return this.ventuzTabletLeftScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }
        private string tabletLeftClientHostname = string.Empty;
        public string TabletLeftClientHostname {
            get { return this.tabletLeftClientHostname; }
            set {
                if (this.tabletLeftClientHostname != value) {
                    if (value == null) value = string.Empty;
                    this.tabletLeftClientHostname = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private VRemote4.HandlerSi.Client.Business tabletRightClient;
        private Tablet ventuzTabletRightScene;
        public VRemote4.HandlerSi.Scene.States VentuzTabletRightSceneStatus {
            get {
                if (this.ventuzTabletRightScene is Tablet) return this.ventuzTabletRightScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }
        private string tabletRightClientHostname = string.Empty;
        public string TabletRightClientHostname {
            get { return this.tabletRightClientHostname; }
            set {
                if (this.tabletRightClientHostname != value) {
                    if (value == null) value = string.Empty;
                    this.tabletRightClientHostname = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Insert insertScene;
        public override VRemote4.HandlerSi.Scene.States InsertSceneStatus {
            get {
                if (this.insertScene is Insert) return this.insertScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private int[,] dicesIndexArray = new int[,] { { 0, 1, 2 }, { 2, 0, 1 }, { 1, 2, 0 }, { 2, 1, 0 }, { 0, 2, 1 }, { 1, 0, 2 } };

        private Tablet.Operations[,] operationArray = new Tablet.Operations[,] {
            {Tablet.Operations.Add, Tablet.Operations.Add},
            {Tablet.Operations.Add, Tablet.Operations.Subtract},
            {Tablet.Operations.Add, Tablet.Operations.Multiply},
            {Tablet.Operations.Add, Tablet.Operations.Divide},
            {Tablet.Operations.Subtract, Tablet.Operations.Add},
            {Tablet.Operations.Subtract, Tablet.Operations.Subtract},
            {Tablet.Operations.Subtract, Tablet.Operations.Multiply},
            {Tablet.Operations.Subtract, Tablet.Operations.Divide},
            {Tablet.Operations.Multiply, Tablet.Operations.Add},
            {Tablet.Operations.Multiply, Tablet.Operations.Subtract},
            {Tablet.Operations.Multiply, Tablet.Operations.Multiply},
            {Tablet.Operations.Multiply, Tablet.Operations.Divide},
            {Tablet.Operations.Divide, Tablet.Operations.Add},
            {Tablet.Operations.Divide, Tablet.Operations.Subtract},
            {Tablet.Operations.Divide, Tablet.Operations.Multiply},
            {Tablet.Operations.Divide, Tablet.Operations.Divide}
        };

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.DicesCalculation'", typeIdentifier);
        }

        public override void New() {
            base.New();
        }

        public override void Pose(
            SynchronizationContext syncContext,
            MidiHandler.Business midiHandler,
            BuzzerIO.Business buzzerHandler,
            AMB.Business ambHandler,
            Devantech.Controller devantechHandler,
            Content.Gameboard.Business gameboard,
            VentuzScenes.Insert.Business insertMasterScene,
            VentuzScenes.Fullscreen.Business fullscreenMasterScene,
            VentuzScenes.Host.Business hostMasterScene,
            VentuzScenes.Player.Business leftPlayerMasterScene,
            VentuzScenes.Player.Business rightPlayerMasterScene,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {

            base.Pose(syncContext,
                midiHandler, buzzerHandler, ambHandler, devantechHandler, gameboard,
                insertMasterScene, fullscreenMasterScene, hostMasterScene, leftPlayerMasterScene, rightPlayerMasterScene, previewPipe);

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;
            this.insertScene.Timer.Alarm1Fired += this.timer_Alarm1Fired;
            this.insertScene.Timer.Alarm2Fired += this.timer_Alarm2Fired;
            this.insertScene.Timer.StopFired += this.timer_StopFired;
            this.insertScene.Timer.PropertyChanged += this.timer_PropertyChanged;

            this.localVentuzHandler = new VRemote4.HandlerSi.Business(syncContext);
            this.localVentuzHandler.Error += this.localVentuzHandler_Error;

            if (this.localVentuzHandler.TryAddClient("Left Tablet", false, out this.tabletLeftClient)) {
                this.tabletLeftClient.HostnameChanged += this.tabletLeftClient_HostnameChanged;
                this.tabletLeftClient.StatusChanged += this.tabletLeftClient_StatusChanged;
                this.ventuzTabletLeftScene = new Tablet(syncContext, this.tabletLeftClient, 0);
                this.ventuzTabletLeftScene.StatusChanged += this.ventuzTabletLeftScene_StatusChanged;
                this.ventuzTabletLeftScene.PropertyChanged += this.ventuzTabletLeftScene_PropertyChanged;
                this.ventuzTabletLeftScene.ResolvePressed += this.ventuzTabletLeftScene_ResolvePressed;
            }

            if (this.localVentuzHandler.TryAddClient("Right Tablet", false, out this.tabletRightClient)) {
                this.tabletRightClient.HostnameChanged += this.tabletRightClient_HostnameChanged;
                this.tabletRightClient.StatusChanged += this.tabletRightClient_StatusChanged;
                this.ventuzTabletRightScene = new Tablet(syncContext, this.tabletRightClient, 0);
                this.ventuzTabletRightScene.StatusChanged += this.ventuzTabletRightScene_StatusChanged;
                this.ventuzTabletRightScene.PropertyChanged += this.ventuzTabletRightScene_PropertyChanged;
                this.ventuzTabletRightScene.ResolvePressed += this.ventuzTabletRightScene_ResolvePressed;
            }

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this, this.localVentuzHandler);
        }

        public override void Dispose() {
            base.Dispose();

            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Timer.Alarm1Fired -= this.timer_Alarm1Fired;
            this.insertScene.Timer.Alarm2Fired -= this.timer_Alarm2Fired;
            this.insertScene.Timer.StopFired -= this.timer_StopFired;
            this.insertScene.Timer.PropertyChanged -= this.timer_PropertyChanged;

            if (this.tabletLeftClient is VRemote4.HandlerSi.Client.Business) {
                this.tabletLeftClient.HostnameChanged -= this.tabletLeftClient_HostnameChanged;
                this.tabletLeftClient.StatusChanged -= this.tabletLeftClient_StatusChanged;
                this.tabletLeftClient.Shutdown();
            }
            if (this.ventuzTabletLeftScene is VRemote4.HandlerSi.Scene) {
                this.ventuzTabletLeftScene.StatusChanged -= this.ventuzTabletLeftScene_StatusChanged;
                this.ventuzTabletLeftScene.PropertyChanged -= this.ventuzTabletLeftScene_PropertyChanged;
                this.ventuzTabletLeftScene.ResolvePressed -= this.ventuzTabletLeftScene_ResolvePressed;
            }

            if (this.tabletRightClient is VRemote4.HandlerSi.Client.Business) {
                this.tabletRightClient.HostnameChanged -= this.tabletRightClient_HostnameChanged;
                this.tabletRightClient.StatusChanged -= this.tabletRightClient_StatusChanged;
                this.tabletRightClient.Shutdown();
            }
            if (this.ventuzTabletRightScene is VRemote4.HandlerSi.Scene) {
                this.ventuzTabletRightScene.StatusChanged -= this.ventuzTabletRightScene_StatusChanged;
                this.ventuzTabletRightScene.PropertyChanged -= this.ventuzTabletRightScene_PropertyChanged;
                this.ventuzTabletRightScene.ResolvePressed -= this.ventuzTabletRightScene_ResolvePressed;
            }

            this.localVentuzHandler.Error -= this.localVentuzHandler_Error;
            this.localVentuzHandler.Dispose();

        }

        public override void ResetData() {
            base.ResetData();
            this.Dice_1 = 0;
            this.Dice_2 = 0;
            this.Dice_3 = 0;
            this.Solution = string.Empty;
            this.LeftPlayerInput = string.Empty;
            this.RightPlayerInput = string.Empty;
            this.FirstResolved = Content.Gameboard.PlayerSelection.NotSelected;
        }

        internal void SetNextDice(
            int value) {
            if (value > 0 && value <= 6) {
                if (this.Dice_1 == 0) this.Dice_1 = value;
                else if (this.Dice_2 == 0) this.Dice_2 = value;
                else if (this.Dice_3 == 0) this.Dice_3 = value;
            }
        }

        internal void SortDices() {
            int[] dices = new int[] { this.Dice_1, this.Dice_2, this.Dice_3 };
            //bubblesort
            for (int i = 0; i < dices.Length - 1; i++) {
                for (int j = i + 1; j < dices.Length; j++) {
                    if (dices[i] > dices[j]) {
                        int buffer = dices[i];
                        dices[i] = dices[j];
                        dices[j] = buffer;
                    }
                }
            }
            this.Dice_1 = dices[0];
            this.Dice_2 = dices[1];
            this.Dice_3 = dices[2];
        }

        public void CalculateSolution() {
            string solution = string.Empty;
            if (this.Dice_1 > 0 &&
                this.Dice_2 > 0 &&
                this.Dice_3 > 0) {
                int delta = int.MaxValue;
                int[] dices = new int[3];
                dices[0] = this.Dice_1;
                dices[1] = this.Dice_2;
                dices[2] = this.Dice_3;
                Input input = new Input();
                for (int d = 0; d < dicesIndexArray.GetLength(0); d++) {
                    for (int o = 0; o < operationArray.GetLength(0); o++ ) {
                        input.SetValues(dices[dicesIndexArray[d, 0]], operationArray[o, 0], dices[dicesIndexArray[d, 1]], operationArray[o, 1], dices[dicesIndexArray[d, 2]]);
                        if (input.Result.HasValue) {
                            if (Math.Abs(input.Result.Value - 15) < delta) {
                                delta = Math.Abs(input.Result.Value - 15);
                                solution = input.Text;
                                if (delta == 0) break;
                            }
                        }
                    }
                }
            }
            this.Solution = solution;
        }

        public override void Next() {
            base.Next();
            this.Dice_1 = 0;
            this.Dice_2 = 0;
            this.Dice_3 = 0;
            this.Solution = string.Empty;
            this.LeftPlayerInput = string.Empty;
            this.RightPlayerInput = string.Empty;
            this.FirstResolved = Content.Gameboard.PlayerSelection.NotSelected;
        }

        internal void Resolve() {
            if (this.leftPlayerInput.Result.HasValue &&
                this.rightPlayerInput.Result.HasValue) {
                int leftDelta = Math.Abs(15 - this.leftPlayerInput.Result.Value);
                int rightDelta = Math.Abs(15 - this.rightPlayerInput.Result.Value);
                if (leftDelta == rightDelta) {
                    if (this.FirstResolved == Content.Gameboard.PlayerSelection.LeftPlayer) this.LeftPlayerScore++;
                    else if (this.FirstResolved == Content.Gameboard.PlayerSelection.RightPlayer) this.RightPlayerScore++;
                }
                else if (leftDelta < rightDelta) this.LeftPlayerScore++;
                else if (rightDelta < leftDelta) this.RightPlayerScore++;
            }
            else if (this.leftPlayerInput.Result.HasValue) this.LeftPlayerScore++;
            else if (this.rightPlayerInput.Result.HasValue) this.RightPlayerScore++;
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }
        public void Vinsert_DicesIn() {
            this.Vinsert_SetContent();
            if (this.Dice_1 > 0 &&
                this.Dice_2 > 0 &&
                this.Dice_3 > 0) {
                this.insertScene.DicesToIn();
            }
        }
        public void Vinsert_DicesOut() { this.insertScene.DicesToOut(); }
        public void Vinsert_SolutionIn() {
            this.Vinsert_SetContent();
            if (this.Dice_1 > 0 &&
                this.Dice_2 > 0 &&
                this.Dice_3 > 0) {
                this.insertScene.SolutionToIn();
            }
        }
        public void Vinsert_SolutionOut() { this.insertScene.SolutionToOut(); }
        public void Vinsert_LeftInputIn() {
            this.Vinsert_SetContent();
            if (this.FlipPlayers) {
                if (this.leftPlayerInput.Result.HasValue) this.insertScene.RightToIn();
                else this.insertScene.RightToOut();
            }
            else {
                if (this.leftPlayerInput.Result.HasValue) this.insertScene.LeftToIn();
                else this.insertScene.LeftToOut();
            }
        }
        public void Vinsert_LeftInputOut() {
            if (this.FlipPlayers) this.insertScene.RightToOut();
            else this.insertScene.LeftToOut();
        }
        public void Vinsert_RightInputIn() {
            this.Vinsert_SetContent();
            if (this.FlipPlayers) {
                if (this.rightPlayerInput.Result.HasValue) this.insertScene.LeftToIn();
                else this.insertScene.LeftToOut();
            }
            else {
                if (this.rightPlayerInput.Result.HasValue) this.insertScene.RightToIn();
                else this.insertScene.RightToOut();
            }
        }
        public void Vinsert_RightInputOut() {
            if (this.FlipPlayers) this.insertScene.LeftToOut();
            else this.insertScene.RightToOut();
        }
        internal void Vinsert_ShowBorder() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                if (this.leftPlayerInput.Result.HasValue &&
                    this.rightPlayerInput.Result.HasValue) {
                    // es liegt von jedem Spieler eine Eingabe vor
                    int leftDelta = Math.Abs(15 - this.leftPlayerInput.Result.Value);
                    int rightDelta = Math.Abs(15 - this.rightPlayerInput.Result.Value);
                    if (leftDelta == rightDelta) {
                        // Gleichstand, es gewinnt der schnellere Spieler
                        if (this.FirstResolved == Content.Gameboard.PlayerSelection.LeftPlayer) {
                            this.insertScene.LeftBorderToIn();
                            this.insertScene.ResetRightBorder();
                        }
                        else if (this.FirstResolved == Content.Gameboard.PlayerSelection.RightPlayer) {
                            this.insertScene.ResetLeftBorder();
                            this.insertScene.RightBorderToIn();
                        }
                        else {
                            this.insertScene.ResetLeftBorder();
                            this.insertScene.ResetRightBorder();
                        }
                    }
                    else if (leftDelta < rightDelta) {
                        this.insertScene.LeftBorderToIn();
                        this.insertScene.ResetRightBorder();
                    }
                    else if (rightDelta < leftDelta) {
                        this.insertScene.ResetLeftBorder();
                        this.insertScene.RightBorderToIn();
                    }
                }
                else {
                    // es liegt nicht von jedem Spieler eine Eingabe vor
                    this.insertScene.ResetLeftBorder();
                    this.insertScene.ResetRightBorder();
                }
            }
        }
        public void Vinsert_SetContent() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.insertScene.SetPositionX(this.ContentPositionX);
                this.insertScene.SetPositionY(this.ContentPositionY);

                this.insertScene.SetDice(1, this.Dice_1);
                this.insertScene.SetDice(2, this.Dice_2);
                this.insertScene.SetDice(3, this.Dice_3);

                if (this.FlipPlayers) {
                    this.insertScene.SetLeftDice(1, this.rightPlayerInput.Dice_1);
                    this.insertScene.SetLeftDice(2, this.rightPlayerInput.Dice_2);
                    this.insertScene.SetLeftDice(3, this.rightPlayerInput.Dice_3);
                    this.insertScene.SetLeftOperation(1, this.rightPlayerInput.Operation_1);
                    this.insertScene.SetLeftOperation(2, this.rightPlayerInput.Operation_2);
                    if (this.rightPlayerInput.Result.HasValue) this.insertScene.SetLeftResult(this.rightPlayerInput.Result.Value);
                    this.insertScene.SetRightDice(1, this.leftPlayerInput.Dice_1);
                    this.insertScene.SetRightDice(2, this.leftPlayerInput.Dice_2);
                    this.insertScene.SetRightDice(3, this.leftPlayerInput.Dice_3);
                    this.insertScene.SetRightOperation(1, this.leftPlayerInput.Operation_1);
                    this.insertScene.SetRightOperation(2, this.leftPlayerInput.Operation_2);
                    if (this.leftPlayerInput.Result.HasValue) this.insertScene.SetRightResult(this.leftPlayerInput.Result.Value);
                }
                else {
                    this.insertScene.SetLeftDice(1, this.leftPlayerInput.Dice_1);
                    this.insertScene.SetLeftDice(2, this.leftPlayerInput.Dice_2);
                    this.insertScene.SetLeftDice(3, this.leftPlayerInput.Dice_3);
                    this.insertScene.SetLeftOperation(1, this.leftPlayerInput.Operation_1);
                    this.insertScene.SetLeftOperation(2, this.leftPlayerInput.Operation_2);
                    if (this.leftPlayerInput.Result.HasValue) this.insertScene.SetLeftResult(this.leftPlayerInput.Result.Value);
                    this.insertScene.SetRightDice(1, this.rightPlayerInput.Dice_1);
                    this.insertScene.SetRightDice(2, this.rightPlayerInput.Dice_2);
                    this.insertScene.SetRightDice(3, this.rightPlayerInput.Dice_3);
                    this.insertScene.SetRightOperation(1, this.rightPlayerInput.Operation_1);
                    this.insertScene.SetRightOperation(2, this.rightPlayerInput.Operation_2);
                    if (this.rightPlayerInput.Result.HasValue) this.insertScene.SetRightResult(this.rightPlayerInput.Result.Value);
                }

                this.insertScene.SetSolutionDice(1, this.solution.Dice_1);
                this.insertScene.SetSolutionDice(2, this.solution.Dice_2);
                this.insertScene.SetSolutionDice(3, this.solution.Dice_3);
                this.insertScene.SetSolutionOperation(1, this.solution.Operation_1);
                this.insertScene.SetSolutionOperation(2, this.solution.Operation_2);
                if (this.solution.Result.HasValue) this.insertScene.SetSolutionResult(this.solution.Result.Value);
            }
        }
        public void Vinsert_ContentOut() { this.insertScene.ToOut(); }
        public void Vinsert_Buzzer(
            Content.Gameboard.PlayerSelection player) {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                switch (player) {
                    case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.LeftPlayer:
                        this.insertScene.LeftBuzzer();
                        break;
                    case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.RightPlayer:
                        this.insertScene.RightBuzzer();
                        break;
                }
            }
        }
        internal void Vinsert_FirstInputIn() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                switch (this.FirstResolved) {
                    case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.LeftPlayer:
                        this.insertScene.LeftBorderToIn();
                        this.insertScene.ResetRightBorder();
                        break;
                    case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.RightPlayer:
                        this.insertScene.ResetLeftBorder();
                        this.insertScene.RightBorderToIn();
                        break;
                    case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.NotSelected:
                    default:
                        this.insertScene.ResetLeftBorder();
                        this.insertScene.ResetRightBorder();
                        break;
                }
            }
        }
        internal void Vinsert_FirstInputOut() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.insertScene.ResetLeftBorder();
                this.insertScene.ResetRightBorder();
            }
        }
        public override void Vinsert_ScoreIn() {
            this.Vinsert_SetScore();
            this.insertScene.Score.ToIn();
        }
        public override void Vinsert_SetScore() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.insertScene.Score.SetPositionX(this.ScorePositionX);
                this.insertScene.Score.SetPositionY(this.ScorePositionY);
                this.insertScene.Score.SetFlipPosition(this.FlipPlayers);
                this.insertScene.Score.SetStyle(this.ScoreStyle);
                this.insertScene.Score.SetLeftTopName(this.LeftPlayerName);
                this.insertScene.Score.SetLeftTopScore(this.LeftPlayerScore);
                this.insertScene.Score.SetRightBottomName(this.RightPlayerName);
                this.insertScene.Score.SetRightBottomScore(this.RightPlayerScore);
            }
        }
        public override void Vinsert_ScoreOut() { this.insertScene.Score.ToOut(); }
        public void Vinsert_TimerIn() {
            this.Vinsert_SetTimer();
            this.Vinsert_ResetTimer();
            this.insertScene.Timer.ToIn();
        }
        public void Vinsert_SetTimer() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.insertScene.Timer.SetPositionX(this.TimerPositionX);
                this.insertScene.Timer.SetPositionY(this.TimerPositionY);
                this.insertScene.Timer.SetStyle(this.TimerStyle);
                this.insertScene.Timer.SetScaling(100);
                if (this.RunExtraTime) this.insertScene.Timer.SetStartTime(this.TimerExtraTime);
                else this.insertScene.Timer.SetStartTime(this.TimerStartTime);
                this.insertScene.Timer.SetStopTime(this.TimerStopTime);
                this.insertScene.Timer.SetAlarmTime1(this.TimerAlarmTime1);
                this.insertScene.Timer.SetAlarmTime2(this.TimerAlarmTime2);
            }
        }        
        public void Vinsert_StartTimer() { this.insertScene.Timer.StartTimer(); }
        public void Vinsert_StopTimer() { this.insertScene.Timer.StopTimer(); }
        public void Vinsert_ContinueTimer() { this.insertScene.Timer.ContinueTimer(); }
        public void Vinsert_ResetTimer() { this.insertScene.Timer.ResetTimer(); }
        public void Vinsert_TimerOut() { this.insertScene.Timer.ToOut(); }
        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vfullscreen_SetTimer() {
            if (this.RunExtraTime) this.Vfullscreen_SetTimer(this.TimerExtraTime);
            else this.Vfullscreen_SetTimer(this.TimerStartTime);
        }
        public void Vfullscreen_SetTimer(int startTime) {
            base.Vfullscreen_SetTimer();
            if (this.fullscreenMasterScene is VRemote4.HandlerSi.Scene) {
                switch (this.TimerStyle) {
                    case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.Timer.Styles.Sec:
                        this.fullscreenMasterScene.Timer.SetStyle(VentuzScenes.Fullscreen.Clock.Styles.Sec);
                        break;
                    case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.Timer.Styles.MinSec:
                    default:
                        this.fullscreenMasterScene.Timer.SetStyle(VentuzScenes.Fullscreen.Clock.Styles.MinSec);
                        break;
                }
                this.fullscreenMasterScene.Timer.SetStartTime(startTime);
                this.fullscreenMasterScene.Timer.SetStopTime(this.TimerStopTime);
                this.fullscreenMasterScene.Timer.SetAlarmTime1(-1);
                this.fullscreenMasterScene.Timer.SetAlarmTime2(-1);
            }
        }

        public void Vlefttablet_StartClient() { this.tabletLeftClient.Start(this.TabletLeftClientHostname); }
        public void Vlefttablet_Init() {
            if (this.FlipPlayers) {
                this.ventuzTabletLeftScene.SetSelectedPlayer(Tablet.Players.Right);
                this.ventuzTabletLeftScene.SetPlayerName(this.RightPlayerName);
            }
            else {
                this.ventuzTabletLeftScene.SetSelectedPlayer(Tablet.Players.Left);
                this.ventuzTabletLeftScene.SetPlayerName(this.LeftPlayerName);
            }
            this.ventuzTabletLeftScene.Reset();
        }
        internal void Vlefttablet_Start() {
            this.ventuzTabletLeftScene.SetDiceValue(1, this.Dice_1);
            this.ventuzTabletLeftScene.SetDiceValue(2, this.Dice_2);
            this.ventuzTabletLeftScene.SetDiceValue(3, this.Dice_3);
            this.ventuzTabletLeftScene.Start();
        }
        internal void Vlefttablet_StartCountdown() { this.ventuzTabletLeftScene.StartTimeout(this.TimerStartTime); }
        internal void Vlefttablet_Stop() { this.ventuzTabletLeftScene.Stop(); }
        internal void Vlefttablet_Reset() { this.ventuzTabletLeftScene.Reset(); }
        public void Vlefttablet_ShutDown() { this.tabletLeftClient.Shutdown(); }

        public void Vrighttablet_StartClient() { this.tabletRightClient.Start(this.TabletRightClientHostname); }
        public void Vrighttablet_Init() {
            if (this.FlipPlayers) {
                this.ventuzTabletRightScene.SetSelectedPlayer(Tablet.Players.Left);
                this.ventuzTabletRightScene.SetPlayerName(this.LeftPlayerName);
            }
            else {
                this.ventuzTabletRightScene.SetSelectedPlayer(Tablet.Players.Right);
                this.ventuzTabletRightScene.SetPlayerName(this.RightPlayerName);
            }
            this.ventuzTabletRightScene.Reset();
        }
        internal void Vrighttablet_Start() {
            this.ventuzTabletRightScene.SetDiceValue(1, this.Dice_1);
            this.ventuzTabletRightScene.SetDiceValue(2, this.Dice_2);
            this.ventuzTabletRightScene.SetDiceValue(3, this.Dice_3);
            this.ventuzTabletRightScene.Start();
        }
        internal void Vrighttablet_StartCountdown() { this.ventuzTabletRightScene.StartTimeout(this.TimerStartTime); }
        internal void Vrighttablet_Stop() { this.ventuzTabletRightScene.Stop(); }
        internal void Vrighttablet_Reset() { this.ventuzTabletRightScene.Reset(); }
        public void Vrighttablet_ShutDown() { this.tabletRightClient.Shutdown(); }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler TimerAlarm1Fired;
        protected void on_TimerAlarm1Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TimerAlarm1Fired, e); }

        public event EventHandler TimerAlarm2Fired;
        protected void on_TimerAlarm2Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TimerAlarm2Fired, e); }

        public event EventHandler TimerStopFired;
        protected void on_TimerStopFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TimerStopFired, e); }

        #endregion

        #region Events.Incoming

        protected void timer_Alarm1Fired(object sender, EventArgs e) {
            this.on_TimerAlarm1Fired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_Alarm1Fired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_Alarm1Fired(object content) {
        }

        protected void timer_Alarm2Fired(object sender, EventArgs e) {
            this.on_TimerAlarm2Fired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_Alarm2Fired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_Alarm2Fired(object content) {
        }

        protected void timer_StopFired(object sender, EventArgs e) {
            this.on_TimerStopFired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_StopFired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_StopFired(object content) {
        }

        protected void timer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "CurrentTime") this.TimerCurrentTime = this.insertScene.Timer.CurrentTime;
                else if (e.PropertyName == "IsRunning") this.TimerIsRunning = this.insertScene.Timer.IsRunning;
            }
        }

        void localVentuzHandler_Error(object sender, System.IO.ErrorEventArgs e) {
            Exception exc = e.GetException();
            this.on_Error(sender, new Messaging.ErrorEventArgs(this, sender.ToString(), DateTime.Now, exc.ToString()));
        }

        void tabletLeftClient_HostnameChanged(object sender, VRemote4.HandlerSi.Client.HostnameArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_tabletLeftClient_HostnameChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_tabletLeftClient_HostnameChanged(object content) {
            VRemote4.HandlerSi.Client.HostnameArgs e = content as VRemote4.HandlerSi.Client.HostnameArgs;
            if (e is VRemote4.HandlerSi.Client.HostnameArgs) this.TabletLeftClientHostname = e.Name;
        }

        void tabletLeftClient_StatusChanged(object sender, VRemote4.HandlerSi.Client.ClientStateArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_tabletLeftClient_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_tabletLeftClient_StatusChanged(object content) {
            VRemote4.HandlerSi.Client.ClientStateArgs e = content as VRemote4.HandlerSi.Client.ClientStateArgs;
            if (e is VRemote4.HandlerSi.Client.ClientStateArgs &&
                e.Status.HasValue &&
                e.Status.Value == VRemote4.HandlerSi.Client.ClientStates.Active &&
                this.ventuzTabletLeftScene is VRemote4.HandlerSi.Scene) this.ventuzTabletLeftScene.Load();
        }

        void ventuzTabletLeftScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzTabletLeftScene_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzTabletLeftScene_StatusChanged(object content) {
            VRemote4.HandlerSi.Scene.StatusArgs e = content as VRemote4.HandlerSi.Scene.StatusArgs;
            if (e is VRemote4.HandlerSi.Scene.StatusArgs) {
                if (e.Status == VRemote4.HandlerSi.Scene.States.Unloaded) this.tabletLeftClient.Shutdown();
                else if (e.Status == VRemote4.HandlerSi.Scene.States.Available) this.Vlefttablet_Init();
            }
            this.on_PropertyChanged("VentuzTabletLeftSceneStatus");
        }

        void ventuzTabletLeftScene_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzTabletLeftScene_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzTabletLeftScene_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "Selection") {
                    //this.LeftTeamInput = this.ventuzTabletLeftScene.Controller.Selection;
                }
            }
        }

        void ventuzTabletLeftScene_ResolvePressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzTabletLeftScene_ResolvePressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzTabletLeftScene_ResolvePressed(object content) {
            if (this.FlipPlayers) {
                this.rightPlayerInput.SetValues(
                    this.ventuzTabletLeftScene.Dice_1,
                    this.ventuzTabletLeftScene.Operation_1,
                    this.ventuzTabletLeftScene.Dice_2,
                    this.ventuzTabletLeftScene.Operation_2,
                    this.ventuzTabletLeftScene.Dice_3);
                this.on_PropertyChanged("RightPlayerInput");
                if (this.FirstResolved == Content.Gameboard.PlayerSelection.NotSelected) this.FirstResolved = Content.Gameboard.PlayerSelection.RightPlayer;
            }
            else {
                this.leftPlayerInput.SetValues(
                    this.ventuzTabletLeftScene.Dice_1,
                    this.ventuzTabletLeftScene.Operation_1,
                    this.ventuzTabletLeftScene.Dice_2,
                    this.ventuzTabletLeftScene.Operation_2,
                    this.ventuzTabletLeftScene.Dice_3);
                this.on_PropertyChanged("LeftPlayerInput");
                if (this.FirstResolved == Content.Gameboard.PlayerSelection.NotSelected) this.FirstResolved = Content.Gameboard.PlayerSelection.LeftPlayer;
            }
        }


        void tabletRightClient_HostnameChanged(object sender, VRemote4.HandlerSi.Client.HostnameArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_tabletRightClient_HostnameChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_tabletRightClient_HostnameChanged(object content) {
            VRemote4.HandlerSi.Client.HostnameArgs e = content as VRemote4.HandlerSi.Client.HostnameArgs;
            if (e is VRemote4.HandlerSi.Client.HostnameArgs) this.TabletRightClientHostname = e.Name;
        }

        void tabletRightClient_StatusChanged(object sender, VRemote4.HandlerSi.Client.ClientStateArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_tabletRightClient_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_tabletRightClient_StatusChanged(object content) {
            VRemote4.HandlerSi.Client.ClientStateArgs e = content as VRemote4.HandlerSi.Client.ClientStateArgs;
            if (e is VRemote4.HandlerSi.Client.ClientStateArgs &&
                e.Status.HasValue &&
                e.Status.Value == VRemote4.HandlerSi.Client.ClientStates.Active &&
                this.ventuzTabletRightScene is VRemote4.HandlerSi.Scene) this.ventuzTabletRightScene.Load();
        }

        void ventuzTabletRightScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzTabletRightScene_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzTabletRightScene_StatusChanged(object content) {
            VRemote4.HandlerSi.Scene.StatusArgs e = content as VRemote4.HandlerSi.Scene.StatusArgs;
            if (e is VRemote4.HandlerSi.Scene.StatusArgs) {
                if (e.Status == VRemote4.HandlerSi.Scene.States.Unloaded) this.tabletRightClient.Shutdown();
                else if (e.Status == VRemote4.HandlerSi.Scene.States.Available) this.Vrighttablet_Init();
            }
            this.on_PropertyChanged("VentuzTabletRightSceneStatus");
        }

        void ventuzTabletRightScene_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzTabletRightScene_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzTabletRightScene_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                //if (e.PropertyName == "Selection") { this.RightTeamInput = this.ventuzTabletRightScene.Controller.Selection; }
            }
        }

        void ventuzTabletRightScene_ResolvePressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzTabletRightScene_ResolvePressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzTabletRightScene_ResolvePressed(object content) {
            if (this.FlipPlayers) {
                this.leftPlayerInput.SetValues(
                    this.ventuzTabletRightScene.Dice_1,
                    this.ventuzTabletRightScene.Operation_1,
                    this.ventuzTabletRightScene.Dice_2,
                    this.ventuzTabletRightScene.Operation_2,
                    this.ventuzTabletRightScene.Dice_3);
                this.on_PropertyChanged("LeftPlayerInput");
                if (this.FirstResolved == Content.Gameboard.PlayerSelection.NotSelected) this.FirstResolved = Content.Gameboard.PlayerSelection.LeftPlayer;
            }
            else {
                this.rightPlayerInput.SetValues(
                    this.ventuzTabletRightScene.Dice_1,
                    this.ventuzTabletRightScene.Operation_1,
                    this.ventuzTabletRightScene.Dice_2,
                    this.ventuzTabletRightScene.Operation_2,
                    this.ventuzTabletRightScene.Dice_3);
                this.on_PropertyChanged("RightPlayerInput");
                if (this.FirstResolved == Content.Gameboard.PlayerSelection.NotSelected) this.FirstResolved = Content.Gameboard.PlayerSelection.RightPlayer;
            }
        }

        #endregion

    }
}
