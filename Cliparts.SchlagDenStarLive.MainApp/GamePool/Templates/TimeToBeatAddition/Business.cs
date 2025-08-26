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

using Cliparts.Serialization;

using Cliparts.Tools.Base;

using Cliparts.IOnet.IOUnit.IONbase;
using Cliparts.IOnet.IOUnit.IONbuz;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;
using Cliparts.SchlagDenStarLive.MainApp.Settings;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimeToBeatAddition;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimeToBeatAddition
{

    public class Business : _Base.Business {

        #region Properties

        private float leftPlayerFirstRun = 0;
        public float LeftPlayerFirstRun
        {
            get { return this.leftPlayerFirstRun; }
            set
            {
                if (this.leftPlayerFirstRun != value)
                {
                    this.leftPlayerFirstRun = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private float leftPlayerSecondRun = 0;
        public float LeftPlayerSecondRun
        {
            get { return this.leftPlayerSecondRun; }
            set
            {
                if (this.leftPlayerSecondRun != value)
                {
                    this.leftPlayerSecondRun = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private float rightPlayerFirstRun = 0;
        public float RightPlayerFirstRun
        {
            get { return this.rightPlayerFirstRun; }
            set
            {
                if (this.rightPlayerFirstRun != value)
                {
                    this.rightPlayerFirstRun = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private float rightPlayerSecondRun = 0;
        public float RightPlayerSecondRun
        {
            get { return this.rightPlayerSecondRun; }
            set
            {
                if (this.rightPlayerSecondRun != value)
                {
                    this.rightPlayerSecondRun = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int timeToBeatPositionX = 0;
        public int TimeToBeatPositionX {
            get { return this.timeToBeatPositionX; }
            set {
                if (this.timeToBeatPositionX != value) {
                    this.timeToBeatPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeToBeat();
                }
            }
        }

        private int timeToBeatPositionY = 0;
        public int TimeToBeatPositionY {
            get { return this.timeToBeatPositionY; }
            set {
                if (this.timeToBeatPositionY != value) {
                    this.timeToBeatPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeToBeat();
                }
            }
        }

        private Game.StyleElements timeToBeatStyle = Game.StyleElements.TwoRuns;
        public Game.StyleElements TimeToBeatStyle {
            get { return this.timeToBeatStyle; }
            set {
                if (this.timeToBeatStyle != value) {
                    this.timeToBeatStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeToBeat();
                }
            }
        }

        private int timeToBeatStopTime = 5999;
        public int TimeToBeatStopTime {
            get { return this.timeToBeatStopTime; }
            set {
                if (this.timeToBeatStopTime != value) {
                    this.timeToBeatStopTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeToBeat();
                }
            }
        }

        private int timeToBeatSentenceTime = 0;
        public int TimeToBeatSentenceTime {
            get { return this.timeToBeatSentenceTime; }
            set {
                if (this.timeToBeatSentenceTime != value) {
                    this.timeToBeatSentenceTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeToBeatSentenceTime();
                }
            }
        }

        private double timeToBeatCurrentTime = -1;
        public double TimeToBeatCurrentTime {
            get { return this.timeToBeatCurrentTime; }
            protected set {
                if (this.timeToBeatCurrentTime != value) {
                    this.timeToBeatCurrentTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Content.Gameboard.PlayerSelection selectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
        public Content.Gameboard.PlayerSelection SelectedPlayer {
            get { return this.selectedPlayer; }
            set {
                if (this.selectedPlayer != value) {
                    if (value == Content.Gameboard.PlayerSelection.NotSelected) value = Content.Gameboard.PlayerSelection.LeftPlayer;
                    this.selectedPlayer = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int sentenceTime = 10;
        public int SentenceTime {
            get { return this.sentenceTime; }
            set {
                if (this.sentenceTime != value) {
                    this.sentenceTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private BuzzerIO.Business buzzerHandler;

        private List<string> ioUnitNameList = new List<string>();
        public string[] IOUnitNameList { get { return this.ioUnitNameList.ToArray(); } }

        private InfoParamArray_EventArgs ioUnitInfo;

        private string ioUnitName = string.Empty;
        public string IOUnitName {
            get { return this.ioUnitName; }
            set {
                if (this.ioUnitName != value) {
                    if (value == null) value = string.Empty;
                    this.ioUnitName = value;
                    this.on_PropertyChanged();
                    this.ioUnitConnectionStatus = Tools.NetContact.ClientStates.Missing;
                    this.ioUnitWorkMode = WorkModes.NA;
                    this.checkIOUnitStatus();
                    this.requestIOUnitStates(value);
                }
            }
        }

        private Tools.NetContact.ClientStates ioUnitConnectionStatus = Tools.NetContact.ClientStates.Missing;

        private WorkModes ioUnitWorkMode = WorkModes.NA;

        public BuzzerUnitStates IOUnitStatus { get; private set; }

        private int leftBuzzerChannel = 1;
        public int LeftBuzzerChannel
        {
            get { return this.leftBuzzerChannel; }
            set
            {
                if (this.leftBuzzerChannel != value)
                {
                    if (value < 1) value = 1;
                    if (value > 8) value = 8;
                    this.leftBuzzerChannel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightBuzzerChannel = 2;
        public int RightBuzzerChannel
        {
            get { return this.rightBuzzerChannel; }
            set
            {
                if (this.rightBuzzerChannel != value)
                {
                    if (value < 1) value = 1;
                    if (value > 8) value = 8;
                    this.rightBuzzerChannel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int stopBuzzerChannel = 3;
        public int StopBuzzerChannel
        {
            get { return this.stopBuzzerChannel; }
            set
            {
                if (this.stopBuzzerChannel != value)
                {
                    if (value < 1) value = 1;
                    if (value > 8) value = 8;
                    this.stopBuzzerChannel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool buzzerSound = false;
        public bool BuzzerSound {
            get { return this.buzzerSound; }
            set {
                if (this.buzzerSound != value) {
                    this.buzzerSound = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Color leftTeamOffColor = Color.Red;
        public Color LeftTeamOffColor
        {
            get { return this.leftTeamOffColor; }
            set
            {
                if (this.leftTeamOffColor != value)
                {
                    this.leftTeamOffColor = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Color leftTeamOnColor = Color.White;
        public Color LeftTeamOnColor
        {
            get { return this.leftTeamOnColor; }
            set
            {
                if (this.leftTeamOnColor != value)
                {
                    this.leftTeamOnColor = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Color rightTeamOffColor = Color.Blue;
        public Color RightTeamOffColor
        {
            get { return this.rightTeamOffColor; }
            set
            {
                if (this.rightTeamOffColor != value)
                {
                    this.rightTeamOffColor = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Color rightTeamOnColor = Color.Red;
        public Color RightTeamOnColor
        {
            get { return this.rightTeamOnColor; }
            set
            {
                if (this.rightTeamOnColor != value)
                {
                    this.rightTeamOnColor = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Tools.DMX.DMXNet dMX = new Tools.DMX.DMXNet();
        private byte[] universe = new byte[256];

        private int leftBuzzerDMXStartchannel = 1;
        public int LeftBuzzerDMXStartchannel
        {
            get { return this.leftBuzzerDMXStartchannel; }
            set
            {
                if (this.leftBuzzerDMXStartchannel != value)
                {
                    if (value < 1) this.leftBuzzerDMXStartchannel = 1;
                    else if (value > 256) this.leftBuzzerDMXStartchannel = 256;
                    else this.leftBuzzerDMXStartchannel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightBuzzerDMXStartchannel = 4;
        public int RightBuzzerDMXStartchannel
        {
            get { return this.rightBuzzerDMXStartchannel; }
            set
            {
                if (this.rightBuzzerDMXStartchannel != value)
                {
                    if (value < 1) this.rightBuzzerDMXStartchannel = 1;
                    else if (value > 256) this.rightBuzzerDMXStartchannel = 256;
                    else this.rightBuzzerDMXStartchannel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int flashCounter = 0;
        private System.Timers.Timer flashTimer;
        private Content.Gameboard.PlayerSelection buzzeredChannel;

        private Insert insertScene;
        public override VRemote4.HandlerSi.Scene.States InsertSceneStatus {
            get {
                if (this.insertScene is Insert) return this.insertScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private bool timeIsRunning = false;

        #endregion


        #region Funktionen

        public Business() {}
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.TimeToBeatAddition'", typeIdentifier);
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

            this.buzzerHandler = buzzerHandler;
            this.buzzerHandler.BuzUnit_Buzzered += this.buzzerHandler_BuzUnit_Buzzered;
            this.buzzerHandler.Unit_ConnectionStatusChanged += this.buzzerHandler_UnitConnectionStatusChanged;
            this.buzzerHandler.Unit_ConnectionStatusRequest += this.buzzerHandler_UnitConnectionStatusRequest;
            this.buzzerHandler.Unit_InfoListChanged += this.buzzerHandler_UnitInfoListChanged;
            this.buzzerHandler.BuzUnit_WorkmodeChanged += this.buzzerHandler_BuzUnit_WorkmodeChanged;
            this.buzzerHandler.BuzUnit_WorkmodeRequest += this.buzzerHandler_BuzUnit_WorkmodeRequest;
            this.fillIOUnitList(this.buzzerHandler.UnitInfoList);
            this.buzzerHandler.RequestUnitConnectionStatus(this.IOUnitName);
            this.buzzerHandler.RequestUnitWorkMode(this.IOUnitName);

            this.flashTimer = new System.Timers.Timer(100);
            this.flashTimer.AutoReset = false;
            this.flashTimer.Elapsed += this.FlashTimer_Elapsed;

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;
            this.insertScene.Game.StopFired += this.timeToBeat_StopFired;
            this.insertScene.Game.PropertyChanged += this.timeToBeat_PropertyChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);

            this.AllLightsBlack();
        }

        public override void Dispose() {
            base.Dispose();

            this.buzzerHandler.BuzUnit_Buzzered -= this.buzzerHandler_BuzUnit_Buzzered;
            this.buzzerHandler.Unit_ConnectionStatusChanged -= this.buzzerHandler_UnitConnectionStatusChanged;
            this.buzzerHandler.Unit_ConnectionStatusRequest -= this.buzzerHandler_UnitConnectionStatusRequest;
            this.buzzerHandler.Unit_InfoListChanged -= this.buzzerHandler_UnitInfoListChanged;
            this.buzzerHandler.BuzUnit_WorkmodeChanged -= this.buzzerHandler_BuzUnit_WorkmodeChanged;
            this.buzzerHandler.BuzUnit_WorkmodeRequest -= this.buzzerHandler_BuzUnit_WorkmodeRequest;
            this.buzzerHandler = null;

            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Game.StopFired -= this.timeToBeat_StopFired;
            this.insertScene.Game.PropertyChanged -= this.timeToBeat_PropertyChanged;
        }

        public override void ResetData() {
            base.ResetData();
            this.timeIsRunning = false;
            this.TimeToBeatSentenceTime = 0;
            this.LeftPlayerFirstRun = 0;
            this.LeftPlayerSecondRun = 0;
            this.RightPlayerFirstRun = 0;
            this.RightPlayerSecondRun = 0;
        }

        public void AddSentence() {
            this.TimeToBeatSentenceTime += this.SentenceTime;
        }

        public void NextPlayer() {
            if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) this.SelectedPlayer = Content.Gameboard.PlayerSelection.RightPlayer;
            else this.SelectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
            this.TimeToBeatSentenceTime = 0;
            this.Vinsert_SetTimeToBeat();
        }

        private void fillIOUnitList(
            InfoParam[] unitInfoList) {
            this.ioUnitNameList.Clear();
            if (unitInfoList is InfoParam[]) {
                foreach (InfoParam item in unitInfoList) {
                    if (item is InfoParam) this.ioUnitNameList.Add(item.Name);
                }
            }
            this.on_PropertyChanged("IOUnitNameList");
        }

        private void checkIOUnitStatus() {
            BuzzerIO.BuzzerUnitStates ioUnitStatus = BuzzerIO.BuzzerUnitStates.NotAvailable;
            if (this.ioUnitNameList.Contains(this.IOUnitName)) {
                switch (this.ioUnitConnectionStatus) {
                    case Cliparts.Tools.NetContact.ClientStates.Connected:
                        ioUnitStatus = BuzzerIO.BuzzerUnitStates.Connected;
                        break;
                    case Cliparts.Tools.NetContact.ClientStates.Connecting:
                        ioUnitStatus = BuzzerIO.BuzzerUnitStates.Connecting;
                        break;
                    case Cliparts.Tools.NetContact.ClientStates.Disconnected:
                        ioUnitStatus = BuzzerIO.BuzzerUnitStates.Disconnected;
                        break;
                    case Cliparts.Tools.NetContact.ClientStates.Missing:
                    default:
                        ioUnitStatus = BuzzerIO.BuzzerUnitStates.Missing;
                        break;
                }
                if (ioUnitStatus == BuzzerIO.BuzzerUnitStates.Connected) {
                    switch (this.ioUnitWorkMode) {
                        case WorkModes.BUZZER:
                            ioUnitStatus = BuzzerIO.BuzzerUnitStates.BuzzerMode;
                            break;
                        case WorkModes.EVENT:
                            ioUnitStatus = BuzzerIO.BuzzerUnitStates.EventMode;
                            break;
                        case WorkModes.LOCK:
                            ioUnitStatus = BuzzerIO.BuzzerUnitStates.Locked;
                            break;
                        case WorkModes.NA:
                        default:
                            break;
                    }
                }
            }
            if (this.IOUnitStatus != ioUnitStatus) {
                this.IOUnitStatus = ioUnitStatus;
                this.on_PropertyChanged("IOUnitStatus");
            }
        }

        private void requestIOUnitStates(
            string unitName) {
            if (this.buzzerHandler is BuzzerIO.Business) {
                this.buzzerHandler.RequestUnitConnectionStatus(unitName);
                this.buzzerHandler.RequestUnitWorkMode(unitName);
            }
        }

        public void DoBuzzer(
            int channel)
        {
            if (channel == this.StopBuzzerChannel)
            { 
                this.PassFinishLine();
            }
            else
            {
                if (this.BuzzerSound) this.Vinsert_PlayBuzzerSound();
                if (channel == this.LeftBuzzerChannel) this.startFlashing(Content.Gameboard.PlayerSelection.LeftPlayer);
                else if (channel == this.RightBuzzerChannel) this.startFlashing(Content.Gameboard.PlayerSelection.RightPlayer);
                this.AddSentence();
            }
            this.LockBuzzer();
        }

        public virtual void PassFinishLine() {
            if (this.timeIsRunning)
            {
                this.Vinsert_StopTimeToBeat();
                if (this.BuzzerSound) this.Vinsert_PlayStopSound();
            }
            else this.Vinsert_StartTimeToBeat();
        }

        private void checkWinner()
        {
            if (this.LeftPlayerFirstRun > 0 &&
                this.LeftPlayerSecondRun > 0 &&
                this.RightPlayerFirstRun > 0 &&
                this.RightPlayerSecondRun > 0)
            {
                float leftAddition = this.leftPlayerFirstRun + this.leftPlayerSecondRun;
                float rightAddition = this.rightPlayerFirstRun + this.rightPlayerSecondRun;
                if (this.insertScene is VRemote4.HandlerSi.Scene)
                {
                    if (leftAddition < rightAddition) this.insertScene.Game.StartTopFlashing();
                    else if (leftAddition > rightAddition) this.insertScene.Game.StartBottomFlashing();
                    else
                    {
                        this.insertScene.Game.StartTopFlashing();
                        this.insertScene.Game.StartBottomFlashing();
                    }
                }
            }
        }

        public virtual void ReleaseBuzzer() {
            this.flashTimer.Stop();
            bool[] inputMask = new bool[8];
            if (this.LeftBuzzerChannel > 0 &&
                this.LeftBuzzerChannel <= inputMask.Length) inputMask[this.LeftBuzzerChannel - 1] = true;
            if (this.RightBuzzerChannel > 0 &&
                this.RightBuzzerChannel <= inputMask.Length) inputMask[this.RightBuzzerChannel - 1] = true;
            if (this.LeftBuzzerChannel > 0 &&
                this.LeftBuzzerChannel <= inputMask.Length) inputMask[this.LeftBuzzerChannel - 1] = true;
            if (this.StopBuzzerChannel > 0 &&
                this.StopBuzzerChannel <= inputMask.Length) inputMask[this.StopBuzzerChannel - 1] = true;
            this.buzzerHandler.SetInputMask(this.IOUnitName, inputMask);
            this.buzzerHandler.ReleaseBuzzer(this.IOUnitName, WorkModes.BUZZER);
            this.SetLeftBuzzerOff(this.SelectedPlayer);
            this.SetRightBuzzerOff(this.SelectedPlayer);
        }

        public virtual void LockBuzzer() {
            this.buzzerHandler.LockBuzzer(this.IOUnitName);
        }

        internal void AllLightsBlack()
        {
            byte[] valueList = new byte[256];
            this.setDMXValues(1, valueList);
        }

        internal void SetLeftBuzzerOn(
            Content.Gameboard.PlayerSelection player)
        {
            byte[] valueList = new byte[] { 0, 0, 0 };
            switch (player)
            {
                case Content.Gameboard.PlayerSelection.LeftPlayer:
                    valueList = new byte[] { this.LeftTeamOnColor.R, this.LeftTeamOnColor.G, this.LeftTeamOnColor.B };
                    break;
                case Content.Gameboard.PlayerSelection.RightPlayer:
                    valueList = new byte[] { this.RightTeamOnColor.R, this.RightTeamOnColor.G, this.RightTeamOnColor.B };
                    break;
            }
            this.setDMXValues(this.LeftBuzzerDMXStartchannel, valueList);
        }
        internal void SetLeftBuzzerOff(
            Content.Gameboard.PlayerSelection player)
        {
            byte[] valueList = new byte[] { 0, 0, 0 };
            switch (player)
            {
                case Content.Gameboard.PlayerSelection.LeftPlayer:
                    valueList = new byte[] { this.LeftTeamOffColor.R, this.LeftTeamOffColor.G, this.LeftTeamOffColor.B };
                    break;
                case Content.Gameboard.PlayerSelection.RightPlayer:
                    valueList = new byte[] { this.RightTeamOffColor.R, this.RightTeamOffColor.G, this.RightTeamOffColor.B };
                    break;
            }
            this.setDMXValues(this.LeftBuzzerDMXStartchannel, valueList);
        }

        internal void SetRightBuzzerOn(
            Content.Gameboard.PlayerSelection player)
        {
            byte[] valueList = new byte[] { 0, 0, 0 };
            switch (player)
            {
                case Content.Gameboard.PlayerSelection.LeftPlayer:
                    valueList = new byte[] { this.LeftTeamOnColor.R, this.LeftTeamOnColor.G, this.LeftTeamOnColor.B };
                    break;
                case Content.Gameboard.PlayerSelection.RightPlayer:
                    valueList = new byte[] { this.RightTeamOnColor.R, this.RightTeamOnColor.G, this.RightTeamOnColor.B };
                    break;
            }
            this.setDMXValues(this.RightBuzzerDMXStartchannel, valueList);
        }
        internal void SetRightBuzzerOff(
            Content.Gameboard.PlayerSelection player)
        {
            byte[] valueList = new byte[] { 0, 0, 0 };
            switch (player)
            {
                case Content.Gameboard.PlayerSelection.LeftPlayer:
                    valueList = new byte[] { this.LeftTeamOffColor.R, this.LeftTeamOffColor.G, this.LeftTeamOffColor.B };
                    break;
                case Content.Gameboard.PlayerSelection.RightPlayer:
                    valueList = new byte[] { this.RightTeamOffColor.R, this.RightTeamOffColor.G, this.RightTeamOffColor.B };
                    break;
            }
            this.setDMXValues(this.RightBuzzerDMXStartchannel, valueList);
        }

        private void setDMXValues(
            int startChannel,
            byte[] values)
        {
            try
            {
                if (this.buzzerHandler is BuzzerIO.Business) this.buzzerHandler.SetDMXChannel(this.IOUnitName, 1, values);
                if (this.dMX is Tools.DMX.DMXNet)
                {
                    byte startIndex = Convert.ToByte(startChannel - 1);
                    if (startIndex + values.Length <= this.universe.Length) Array.Copy(values, 0, this.universe, startIndex, values.Length);
                    this.dMX.SetDmxDataToEspNet(0, 0, this.universe);
                }
            }
            catch (Exception) { }
        }

        private void startFlashing(
            Content.Gameboard.PlayerSelection buzzer)
        {
            this.buzzeredChannel = buzzer;
            this.flashCounter = 0;
            switch (buzzer)
            {
                case Content.Gameboard.PlayerSelection.LeftPlayer:
                    this.SetLeftBuzzerOn(this.SelectedPlayer);
                    break;
                case Content.Gameboard.PlayerSelection.RightPlayer:
                    this.SetRightBuzzerOn(this.SelectedPlayer);
                    break;
            }
            this.flashTimer.Start();
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }

        public void Vinsert_TimeToBeatIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_TimeToBeatIn(this.insertScene.Game); }
        public void Vinsert_TimeToBeatIn(
            Game scene) {
            if (scene is Game) {
                this.Vinsert_SetTimeToBeat(scene, this.SelectedPlayer);
                scene.ToIn();
            }
            this.Vinsert_ResetTimeToBeat(scene);
        }
        public void Vinsert_SetTimeToBeat() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetTimeToBeat(this.insertScene.Game, this.SelectedPlayer); }
        public void Vinsert_SetTimeToBeat(
            Game scene,
            Content.Gameboard.PlayerSelection selectedPlayer) {
            if (scene is Game) {
                scene.SetPositionX(this.TimeToBeatPositionX);
                scene.SetPositionY(this.TimeToBeatPositionY);
                scene.SetStopTime(this.TimeToBeatStopTime);
                scene.SetSentenceTime(this.TimeToBeatSentenceTime);
                scene.SetStyle(this.TimeToBeatStyle);
                scene.SetTopName(this.LeftPlayerName);
                scene.SetTopFirstRun(this.LeftPlayerFirstRun);
                scene.SetTopSecondRun(this.LeftPlayerSecondRun);
                scene.SetBottomName(this.RightPlayerName);
                scene.SetBottomFirstRun(this.RightPlayerFirstRun);
                scene.SetBottomSecondRun(this.RightPlayerSecondRun);
                switch (selectedPlayer)
                {
                    case Content.Gameboard.PlayerSelection.LeftPlayer:
                        scene.SetSelectedTeam(Game.SelectedTeamElements.Top);
                        if (this.LeftPlayerFirstRun > 0) scene.SetRun(Game.RunElements.SecondRun);
                        else scene.SetRun(Game.RunElements.FirstRun);
                        break;
                    case Content.Gameboard.PlayerSelection.RightPlayer:
                        scene.SetSelectedTeam(Game.SelectedTeamElements.Bottom);
                        if (this.RightPlayerFirstRun > 0) scene.SetRun(Game.RunElements.SecondRun);
                        else scene.SetRun(Game.RunElements.FirstRun);
                        break;
                }
            }
        }
        public void Vinsert_SetTimeToBeatSentenceTime() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetTimeToBeatSentenceTime(this.insertScene.Game); }
        public void Vinsert_SetTimeToBeatSentenceTime(Game scene) { if (scene is Game) scene.SetSentenceTime(this.TimeToBeatSentenceTime); }
        public void Vinsert_StartTimeToBeat() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_StartTimeToBeat(this.insertScene.Game); }
        public void Vinsert_StartTimeToBeat(Game scene) {
            if (scene is Game) {
                scene.StartTimer();
                this.timeIsRunning = true;
            }
        }
        public void Vinsert_StopTimeToBeat() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_StopTimeToBeat(this.insertScene.Game); }
        public void Vinsert_StopTimeToBeat(Game scene) { if (scene is Game) { scene.StopTimer(); } }
        public void Vinsert_ContinueTimeToBeat() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ContinueTimeToBeat(this.insertScene.Game); }
        public void Vinsert_ContinueTimeToBeat(Game scene) {
            if (scene is Game) {
                scene.ContinueTimer();
                this.timeIsRunning = true;
            }
        }
        public void Vinsert_ResetTimeToBeat() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ResetTimeToBeat(this.insertScene.Game); }
        public void Vinsert_ResetTimeToBeat(Game scene) { if (scene is Game) scene.ResetTimer(); }
        public void Vinsert_TimeToBeatOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_TimeToBeatOut(this.insertScene.Game); }
        public void Vinsert_TimeToBeatOut(
            Game scene) {
            if (scene is Game) {
                scene.ToOut();
            }
        }

        public void Vinsert_PlayBuzzerSound() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlayBuzzerJingle(); }
        public void Vinsert_PlayStopSound() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlayStopJingle(); }

        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler TimeToBeatStopFired;
        protected void on_TimeToBeatStopFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TimeToBeatStopFired, e); }

        #endregion

        #region Events.Incoming

        void buzzerHandler_BuzUnit_Buzzered(object sender, BuzzerIDParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_Buzzered);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_BuzUnit_Buzzered(object content) {
            BuzzerIDParam_EventArgs e = content as BuzzerIDParam_EventArgs;
            if (this.isActive &&
                e is BuzzerIDParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                this.DoBuzzer(e.Arg.BuzzerID);
            }
        }

        void buzzerHandler_UnitConnectionStatusChanged(object sender, ConnectionStatusParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_UnitConnectionStatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        void buzzerHandler_UnitConnectionStatusRequest(object sender, ConnectionStatusParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_UnitConnectionStatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_UnitConnectionStatusChanged(object content) {
            ConnectionStatusParam_EventArgs e = content as ConnectionStatusParam_EventArgs;
            if (e is ConnectionStatusParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                this.ioUnitConnectionStatus = e.Arg.ConnectionStatus;
                this.checkIOUnitStatus();
            }
        }

        void buzzerHandler_UnitInfoListChanged(object sender, InfoParamArray_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_UnitInfoListChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_UnitInfoListChanged(object content) {
            InfoParamArray_EventArgs e = content as InfoParamArray_EventArgs;
            if (e is InfoParamArray_EventArgs) {
                this.ioUnitInfo = e;
                this.fillIOUnitList(e.Arg);
            }
        }

        void buzzerHandler_BuzUnit_WorkmodeChanged(object sender, WorkModeParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_WorkmodeChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        void buzzerHandler_BuzUnit_WorkmodeRequest(object sender, WorkModeParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_WorkmodeChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_BuzUnit_WorkmodeChanged(object content) {
            WorkModeParam_EventArgs e = content as WorkModeParam_EventArgs;
            if (e is WorkModeParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                this.ioUnitWorkMode = e.Arg.WorkMode;
                this.checkIOUnitStatus();
            }
        }

        protected void timeToBeat_StopFired(object sender, EventArgs e) {
            this.on_TimeToBeatStopFired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timeToBeat_StopFired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timeToBeat_StopFired(object content) {
            if (this.timeIsRunning) {
                switch (this.SelectedPlayer)
                {
                    case Content.Gameboard.PlayerSelection.LeftPlayer:
                        if (this.LeftPlayerFirstRun <= 0) this.LeftPlayerFirstRun = this.insertScene.Game.CurrentTime;
                        else if (this.LeftPlayerSecondRun <= 0) this.LeftPlayerSecondRun = this.insertScene.Game.CurrentTime;
                        break;
                    case Content.Gameboard.PlayerSelection.RightPlayer:
                        if (this.RightPlayerFirstRun <= 0) this.RightPlayerFirstRun = this.insertScene.Game.CurrentTime;
                        else if (this.RightPlayerSecondRun <= 0) this.RightPlayerSecondRun = this.insertScene.Game.CurrentTime;
                        break;
                }
                this.checkWinner();
            }
            this.timeIsRunning = false;
        }

        protected void timeToBeat_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timeToBeat_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timeToBeat_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "CurrentTime") this.TimeToBeatCurrentTime = Convert.ToDouble(this.insertScene.Game.CurrentTime);
            }
        }

        private void FlashTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_FlashTimer_Elapsed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_FlashTimer_Elapsed(object content)
        {
            if (this.flashCounter < 19)
            {
                if (this.flashCounter % 2 == 1) 
                {
                    switch (this.buzzeredChannel)
                    {
                        case Content.Gameboard.PlayerSelection.LeftPlayer:
                            this.SetLeftBuzzerOn(this.SelectedPlayer);
                            break;
                        case Content.Gameboard.PlayerSelection.RightPlayer:
                            this.SetRightBuzzerOn(this.SelectedPlayer);
                            break;
                    }
                }
                else {
                    switch (this.buzzeredChannel)
                    {
                        case Content.Gameboard.PlayerSelection.LeftPlayer:
                            this.SetLeftBuzzerOff(this.SelectedPlayer);
                            break;
                        case Content.Gameboard.PlayerSelection.RightPlayer:
                            this.SetRightBuzzerOff(this.SelectedPlayer);
                            break;
                    }
                }
                this.flashCounter++;
                this.flashTimer.Start();
            }
        }
    }


    #endregion
}
