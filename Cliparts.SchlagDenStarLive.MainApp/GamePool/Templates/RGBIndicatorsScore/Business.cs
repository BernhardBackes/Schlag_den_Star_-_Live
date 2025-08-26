using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.RGBIndicatorsScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.RGBIndicatorsScore {

    public class Indicator : INotifyPropertyChanged {

        public enum States { Off, Left, Right}

        #region Properties

        private States status = States.Off;
        public States Status {
            get { return this.status; }
            set {
                if (this.status != value) {
                    this.status = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public Indicator() { }
        public Indicator(States status) { this.Status = status; }

        public void Reset() {
            this.Status = States.Off;
        }

        public void Clone(
            Indicator set) {
            if (set is Indicator) {
                this.Status = set.Status;
            }
            else this.Reset();
        }

        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_PropertyChanged([CallerMemberName] string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }

    public class Business : _Base.TimerScore.Business {

        public const int IndicatorsCountMax = 12;

        #region Properties

        private int indicatorsCount = 5;
        public int IndicatorsCount {
            get { return this.indicatorsCount; }
            set {
                if (this.indicatorsCount != value) {
                    if (value < 1) this.indicatorsCount = 1;
                    else if (value > IndicatorsCountMax) this.indicatorsCount = IndicatorsCountMax;
                    else this.indicatorsCount = value;
                    this.on_PropertyChanged();
                }
            }
        }

        protected List<Indicator> indicators = new List<Indicator>();
        public Indicator[] Indicators {
            get {
                this.fillIndicators();
                return this.indicators.ToArray();
            }
            set {
                this.fillIndicators();
                for (int i = 0; i < IndicatorsCountMax; i++) {
                    if (value is Indicator[] &&
                        value.Length > i) this.indicators[i].Clone(value[i]);
                    else this.indicators[i].Reset();
                }
            }
        }

        private Tools.DMX.DMXNet dMX = new Tools.DMX.DMXNet();
        private byte[] universe = new byte[512];

        private Color offColor = Color.White;
        public Color OffColor {
            get { return this.offColor; }
            set {
                if (this.offColor != value) {
                    this.offColor = value;
                    this.on_PropertyChanged();
                    this.SetDMXValues();
                }
            }
        }

        private Color leftColor = Color.Red;
        public Color LeftColor {
            get { return this.leftColor; }
            set {
                if (this.leftColor != value) {
                    this.leftColor = value;
                    this.on_PropertyChanged();
                    this.SetDMXValues();
                }
            }
        }

        private Color rightColor = Color.Blue;
        public Color RightColor {
            get { return this.rightColor; }
            set {
                if (this.rightColor != value) {
                    this.rightColor = value;
                    this.on_PropertyChanged();
                    this.SetDMXValues();
                }
            }
        }

        private int dmxStartchannel = 1;
        public int DMXStartchannel {
            get { return this.dmxStartchannel; }
            set {
                if (this.dmxStartchannel != value) {
                    if (value < 1) this.dmxStartchannel = 1;
                    else if (value > 256) this.dmxStartchannel = 256;
                    else this.dmxStartchannel = value;
                    this.on_PropertyChanged();
                    this.SetDMXValues();
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

        private bool suppressDMXUpdate = true;

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.RGBIndicatorsScore'", typeIdentifier);
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

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);

            this.suppressDMXUpdate = false;
            this.AllLightsBlack();
        }

        public override void Dispose() {
            base.Dispose();
            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Timer.Alarm1Fired -= this.timer_Alarm1Fired;
            this.insertScene.Timer.Alarm2Fired -= this.timer_Alarm2Fired;
            this.insertScene.Timer.StopFired -= this.timer_StopFired;
            this.insertScene.Timer.PropertyChanged -= this.timer_PropertyChanged;
        }

        protected void fillIndicators() {
            while (this.indicators.Count < IndicatorsCountMax) {
                Indicator item = new Indicator();
                item.PropertyChanged += this.item_PropertyChanged;
                this.indicators.Add(item);
            }
        }

        public override void ResetData() {
            base.ResetData();
            this.ResetIndicators();
        }

        public override void Next() {
            base.Next();
            this.ResetIndicators();
        }

        public void ResetIndicators() {
            this.suppressDMXUpdate = true;
            foreach (Indicator item in this.indicators) item.Reset();
            this.suppressDMXUpdate = false;
            this.SetDMXValues();
        }

        internal void AllLightsBlack() {
            byte[] valueList = new byte[512];
            this.setDMXValues(1, valueList);
        }

        internal void SetDMXValues() {
            byte[] offValue = new byte[] { this.OffColor.R, this.OffColor.G, this.OffColor.B };
            byte[] leftValue = new byte[] { this.LeftColor.R, this.LeftColor.G, this.LeftColor.B };
            byte[] rightValue = new byte[] { this.RightColor.R, this.RightColor.G, this.RightColor.B };
            List<byte> values = new List<byte>();
            foreach (Indicator item in indicators) {
                switch (item.Status) {
                    case Indicator.States.Left:
                        values.AddRange(leftValue);
                        break;
                    case Indicator.States.Right:
                        values.AddRange(rightValue);
                        break;
                    case Indicator.States.Off:
                    default:
                        values.AddRange(offValue);
                        break;
                }
            }
            this.setDMXValues(this.DMXStartchannel, values.ToArray());
        }

        private void setDMXValues(
            int startChannel,
            byte[] values) {
            if (!this.suppressDMXUpdate) {
                try {
                    if (this.buzzerHandler is BuzzerIO.Business) {
                        this.buzzerHandler.SetDMXMode(this.IOUnitName, IOnet.IOUnit.IONbuzDX.DMXBuzzerModes.OFF);
                        this.buzzerHandler.SetDMXChannel(this.IOUnitName, startChannel, values);
                    }
                    if (this.dMX is Tools.DMX.DMXNet) {
                        byte startIndex = Convert.ToByte(startChannel - 1);
                        if (startIndex + values.Length <= this.universe.Length) Array.Copy(values, 0, this.universe, startIndex, values.Length);
                        this.dMX.SetDmxDataToEspNet(0, 0, this.universe);
                    }
                }
                catch (Exception) { }
            }
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }
        public override void Vinsert_ScoreIn() {
            this.Vinsert_SetScore();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Score.ToIn();
        }
        public override void Vinsert_SetScore() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
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
        public override void Vinsert_ScoreOut() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Score.ToOut();
        }
        public override void Vinsert_TimerIn() {
            this.Vinsert_SetTimer();
            this.Vinsert_ResetTimer();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ToIn();
        }
        public void Vinsert_TimerIn(
            int startTime) {
            this.Vinsert_SetTimer(startTime);
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ToIn();
        }
        public override void Vinsert_SetTimer() {
            if (this.RunExtraTime) this.Vinsert_SetTimer(this.TimerExtraTime);
            else this.Vinsert_SetTimer(this.TimerStartTime);
        }
        public void Vinsert_SetTimer(int startTime) {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.Timer.SetPositionX(this.TimerPositionX);
                this.insertScene.Timer.SetPositionY(this.TimerPositionY);
                this.insertScene.Timer.SetStyle(this.TimerStyle);
                this.insertScene.Timer.SetScaling(100);
                this.insertScene.Timer.SetStartTime(startTime);
                this.insertScene.Timer.SetStopTime(this.TimerStopTime);
                this.insertScene.Timer.SetAlarmTime1(this.TimerAlarmTime1);
                this.insertScene.Timer.SetAlarmTime2(this.TimerAlarmTime2);
            }
        }
        public override void Vinsert_StartTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.StartTimer(); }
        public override void Vinsert_StopTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.StopTimer(); }
        public override void Vinsert_ContinueTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ContinueTimer(); }
        public override void Vinsert_ResetTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ResetTimer(); }
        public override void Vinsert_TimerOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ToOut(); }
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
        #endregion

        #region Events.Incoming

        protected override void sync_timer_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "CurrentTime") this.TimerCurrentTime = Convert.ToInt32(this.insertScene.Timer.CurrentTime);
            }
        }

        private void item_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_item_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_item_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) this.SetDMXValues();
        }

        #endregion

    }
}
