using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.DoubleRGBIndicators;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.DoubleRGBIndicators {

    public class Indicator : INotifyPropertyChanged {

        #region Properties

        public int Index { get; private set; }

        public int ID { get { return this.Index + 1; } }

        private int leftPlayerStartAddress = 1;
        public int LeftPlayerStartAddress {
            get { return this.leftPlayerStartAddress; }
            set {
                if (this.leftPlayerStartAddress != value) {
                    if (value < 1) this.leftPlayerStartAddress = 1;
                    else if (value > 512) this.leftPlayerStartAddress = 512;
                    else this.leftPlayerStartAddress = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool leftPlayerOn = false;
        public bool LeftPlayerOn {
            get { return this.leftPlayerOn; }
            set {
                if (this.leftPlayerOn != value) {
                    this.leftPlayerOn = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerStartAddress = 1;
        public int RightPlayerStartAddress {
            get { return this.rightPlayerStartAddress; }
            set {
                if (this.rightPlayerStartAddress != value) {
                    if (value < 1) this.rightPlayerStartAddress = 1;
                    else if (value > 512) this.rightPlayerStartAddress = 512;
                    else this.rightPlayerStartAddress = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool rightPlayerOn = false;
        public bool RightPlayerOn {
            get { return this.rightPlayerOn; }
            set {
                if (this.rightPlayerOn != value) {
                    this.rightPlayerOn = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public Indicator() { }
        public Indicator(
            int index) {
            this.Index = index;
            this.leftPlayerStartAddress = index * 6 + 1;
            this.rightPlayerStartAddress = leftPlayerStartAddress + 3;
        }

        public void Reset() {
            this.LeftPlayerOn = false;
            this.RightPlayerOn = false;
        }

        public void Clone(
            Indicator set) {
            if (set is Indicator) {
                this.LeftPlayerStartAddress = set.LeftPlayerStartAddress;
                this.RightPlayerStartAddress = set.RightPlayerStartAddress;
                this.LeftPlayerOn = set.LeftPlayerOn;
                this.RightPlayerOn = set.RightPlayerOn;
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

    public class Business : _Base.Score.Business {

        public const int IndicatorsCountMax = 11;

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
                    this.SetAllDMXValues();
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
                    this.SetAllDMXValues();
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
                    this.SetAllDMXValues();
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.DoubleRGBIndicators'", typeIdentifier);
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

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);

            this.suppressDMXUpdate = false;
            this.AllLightsBlack();
        }

        public override void Dispose() {
            base.Dispose();
            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
        }

        protected void fillIndicators() {
            while (this.indicators.Count < IndicatorsCountMax) {
                Indicator item = new Indicator(this.indicators.Count);
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
            this.SetAllDMXValues();
        }

        internal void AllLightsBlack() {
            byte[] valueList = new byte[512];
            this.SetDMXValues(1, valueList);
        }

        internal void AllLightsOff() {
            this.suppressDMXUpdate = true;
            foreach (Indicator item in this.indicators) item.Reset();
            this.suppressDMXUpdate = false;
            this.SetAllDMXValues();
        }

        internal void LeftOff() {
            this.suppressDMXUpdate = true;
            foreach (Indicator item in this.indicators) item.LeftPlayerOn = false;
            this.suppressDMXUpdate = false;
            this.SetAllDMXValues();
        }

        internal void LeftOn(
            int index) {
            this.suppressDMXUpdate = true;
            foreach (Indicator item in this.indicators) {
                if (item.Index == index) item.LeftPlayerOn = true;
                else item.LeftPlayerOn = false;
            }
            this.suppressDMXUpdate = false;
            this.SetAllDMXValues();
        }

        internal void RightOff() {
            this.suppressDMXUpdate = true;
            foreach (Indicator item in this.indicators) item.RightPlayerOn = false;
            this.suppressDMXUpdate = false;
            this.SetAllDMXValues();
        }

        internal void RightOn(
            int index) {
            this.suppressDMXUpdate = true;
            foreach (Indicator item in this.indicators) {
                if (item.Index == index) item.RightPlayerOn = true;
                else item.RightPlayerOn = false;
            }
            this.suppressDMXUpdate = false;
            this.SetAllDMXValues();
        }

        internal void SetAllDMXValues() {
            foreach (Indicator item in indicators) {
                if (item.Index < this.IndicatorsCount) {
                    if (item.LeftPlayerOn) this.SetLeftColor(item.LeftPlayerStartAddress);
                    else this.SetOffColor(item.LeftPlayerStartAddress);
                    if (item.RightPlayerOn) this.SetRightColor(item.RightPlayerStartAddress);
                    else this.SetOffColor(item.RightPlayerStartAddress);
                }
            }
        }

        internal void SetLeftColor(
            int startChannel) { this.SetColor(startChannel, this.LeftColor); }

        internal void SetRightColor(
            int startChannel) { this.SetColor(startChannel, this.RightColor); }

        internal void SetOffColor(
            int startChannel) { this.SetColor(startChannel, this.OffColor); }

        internal void SetBlack(
            int startChannel) { this.SetDMXValues(startChannel, new byte[3]); }

        internal void SetColor(
            int startChannel,
            Color color) {
            if (!this.suppressDMXUpdate) {
                try {
                    byte[] values = new byte[] { color.R, color.G, color.B };
                    if (this.dMX is Tools.DMX.DMXNet) {
                        byte startIndex = Convert.ToByte(startChannel - 1);
                        if (startIndex + values.Length <= this.universe.Length) Array.Copy(values, 0, this.universe, startIndex, values.Length);
                        this.dMX.SetDmxDataToEspNet(0, 0, this.universe);
                    }
                }
                catch (Exception) { }
            }
        }

        internal void SetDMXValues(
            int startChannel,
            byte[] values) {
            if (!this.suppressDMXUpdate) {
                try {
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

        private void item_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_item_PropertyChanged);
            PropertyChangedEventSyncArgs psea = new PropertyChangedEventSyncArgs(sender, e);
            if (this.syncContext != null) this.syncContext.Post(callback, psea);
        }
        private void sync_item_PropertyChanged(object content) {
            PropertyChangedEventSyncArgs psea = content as PropertyChangedEventSyncArgs;
            if (psea is PropertyChangedEventSyncArgs) {
                Indicator item = psea.Sender as Indicator;
                if (item is Indicator &&
                    psea.EventArgs is PropertyChangedEventArgs) {
                    if (psea.EventArgs.PropertyName == "LeftPlayerOn") {
                        if (item.LeftPlayerOn) this.LeftOn(item.Index);
                        else if (!this.suppressDMXUpdate) this.SetAllDMXValues(); 
                    }
                    else if (psea.EventArgs.PropertyName == "RightPlayerOn") {
                        if (item.RightPlayerOn) this.RightOn(item.Index);
                        else if (!this.suppressDMXUpdate) this.SetAllDMXValues();
                    }
                }
            }
        }

        #endregion

    }
}
