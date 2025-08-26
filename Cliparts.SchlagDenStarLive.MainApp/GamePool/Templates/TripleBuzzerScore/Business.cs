using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.IOnet.IOUnit.IONbase;
using Cliparts.IOnet.IOUnit.IONbuz;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TripleBuzzerScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TripleBuzzerScore {

    public class Business : _Base.Score.Business {

        #region Properties

        private BuzzerIO.Business buzzerHandler;

        private List<string> ioUnitNameList = new List<string>();
        public string[] IOUnitNameList { get { return this.ioUnitNameList.ToArray(); } }

        private Cliparts.IOnet.IOUnit.IONbase.InfoParamArray_EventArgs ioUnitInfo;

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

        private Cliparts.Tools.NetContact.ClientStates ioUnitConnectionStatus = Tools.NetContact.ClientStates.Missing;

        private WorkModes ioUnitWorkMode = WorkModes.NA;

        public BuzzerUnitStates IOUnitStatus { get; private set; }

        private int leftPlayerBuzzerChannel1 = 1;
        public int LeftPlayerBuzzerChannel1 {
            get { return this.leftPlayerBuzzerChannel1; }
            set {
                if (this.leftPlayerBuzzerChannel1 != value) {
                    if (value < 1) value = 1;
                    if (value > 8) value = 8;
                    this.leftPlayerBuzzerChannel1 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int leftPlayerBuzzerChannel2 = 2;
        public int LeftPlayerBuzzerChannel2 {
            get { return this.leftPlayerBuzzerChannel2; }
            set {
                if (this.leftPlayerBuzzerChannel2 != value) {
                    if (value < 1) value = 1;
                    if (value > 8) value = 8;
                    this.leftPlayerBuzzerChannel2 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int leftPlayerBuzzerChannel3 = 3;
        public int LeftPlayerBuzzerChannel3 {
            get { return this.leftPlayerBuzzerChannel3; }
            set {
                if (this.leftPlayerBuzzerChannel3 != value) {
                    if (value < 1) value = 1;
                    if (value > 8) value = 8;
                    this.leftPlayerBuzzerChannel3 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool leftPlayerBuzzerIdle1 = true;
        public bool LeftPlayerBuzzerIdle1 {
            get { return this.leftPlayerBuzzerIdle1; }
            set {
                if (this.leftPlayerBuzzerIdle1 != value) {
                    this.leftPlayerBuzzerIdle1 = value;
                    if (!value) this.Vinsert_PlayJingleBuzzerLeft();
                    this.on_PropertyChanged();
                    this.SetDMXValues();
                }
            }
        }

        private bool leftPlayerBuzzerIdle2 = true;
        public bool LeftPlayerBuzzerIdle2 {
            get { return this.leftPlayerBuzzerIdle2; }
            set {
                if (this.leftPlayerBuzzerIdle2 != value) {
                    this.leftPlayerBuzzerIdle2 = value;
                    if (!value) this.Vinsert_PlayJingleBuzzerLeft();
                    this.on_PropertyChanged();
                    this.SetDMXValues();
                }
            }
        }

        private bool leftPlayerBuzzerIdle3 = true;
        public bool LeftPlayerBuzzerIdle3 {
            get { return this.leftPlayerBuzzerIdle3; }
            set {
                if (this.leftPlayerBuzzerIdle3 != value) {
                    if (!value) this.Vinsert_PlayJingleBuzzerLeft();
                    this.leftPlayerBuzzerIdle3 = value;
                    this.on_PropertyChanged();
                    this.SetDMXValues();
                }
            }
        }

        private int rightPlayerBuzzerChannel1 = 5;
        public int RightPlayerBuzzerChannel1 {
            get { return this.rightPlayerBuzzerChannel1; }
            set {
                if (this.rightPlayerBuzzerChannel1 != value) {
                    if (value < 1) value = 1;
                    if (value > 8) value = 8;
                    this.rightPlayerBuzzerChannel1 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerBuzzerChannel2 = 6;
        public int RightPlayerBuzzerChannel2 {
            get { return this.rightPlayerBuzzerChannel2; }
            set {
                if (this.rightPlayerBuzzerChannel2 != value) {
                    if (value < 1) value = 1;
                    if (value > 8) value = 8;
                    this.rightPlayerBuzzerChannel2 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerBuzzerChannel3 = 7;
        public int RightPlayerBuzzerChannel3 {
            get { return this.rightPlayerBuzzerChannel3; }
            set {
                if (this.rightPlayerBuzzerChannel3 != value) {
                    if (value < 1) value = 1;
                    if (value > 8) value = 8;
                    this.rightPlayerBuzzerChannel3 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool rightPlayerBuzzerIdle1 = true;
        public bool RightPlayerBuzzerIdle1 {
            get { return this.rightPlayerBuzzerIdle1; }
            set {
                if (this.rightPlayerBuzzerIdle1 != value) {
                    this.rightPlayerBuzzerIdle1 = value;
                    if (!value) this.Vinsert_PlayJingleBuzzerRight();
                    this.on_PropertyChanged();
                    this.SetDMXValues();
                }
            }
        }

        private bool rightPlayerBuzzerIdle2 = true;
        public bool RightPlayerBuzzerIdle2 {
            get { return this.rightPlayerBuzzerIdle2; }
            set {
                if (this.rightPlayerBuzzerIdle2 != value) {
                    this.rightPlayerBuzzerIdle2 = value;
                    if (!value) this.Vinsert_PlayJingleBuzzerRight();
                    this.on_PropertyChanged();
                    this.SetDMXValues();
                }
            }
        }

        private bool rightPlayerBuzzerIdle3 = true;
        public bool RightPlayerBuzzerIdle3 {
            get { return this.rightPlayerBuzzerIdle3; }
            set {
                if (this.rightPlayerBuzzerIdle3 != value) {
                    this.rightPlayerBuzzerIdle3 = value;
                    if (!value) this.Vinsert_PlayJingleBuzzerRight();
                    this.on_PropertyChanged();
                    this.SetDMXValues();
                }
            }
        }

        private Tools.DMX.DMXNet dMX = new Tools.DMX.DMXNet();
        private byte[] universe = new byte[512];

        private Color offColor = Color.Black;
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

        private Color leftPlayerColor = Color.Red;
        public Color LeftPlayerColor {
            get { return this.leftPlayerColor; }
            set {
                if (this.leftPlayerColor != value) {
                    this.leftPlayerColor = value;
                    this.on_PropertyChanged();
                    this.SetDMXValues();
                }
            }
        }

        private Color rightPlayerColor = Color.Blue;
        public Color RightPlayerColor {
            get { return this.rightPlayerColor; }
            set {
                if (this.rightPlayerColor != value) {
                    this.rightPlayerColor = value;
                    this.on_PropertyChanged();
                    this.SetDMXValues();
                }
            }
        }

        private int leftPlayerDMXStart1 = 1;
        public int LeftPlayerDMXStart1 {
            get { return this.leftPlayerDMXStart1; }
            set {
                if (this.leftPlayerDMXStart1 != value) {
                    if (value < 1) this.leftPlayerDMXStart1 = 1;
                    else if (value > 256) this.leftPlayerDMXStart1 = 256;
                    else this.leftPlayerDMXStart1 = value;
                    this.on_PropertyChanged();
                    this.SetDMXValues();
                }
            }
        }

        private int leftPlayerDMXStart2 = 4;
        public int LeftPlayerDMXStart2 {
            get { return this.leftPlayerDMXStart2; }
            set {
                if (this.leftPlayerDMXStart2 != value) {
                    if (value < 1) this.leftPlayerDMXStart2 = 1;
                    else if (value > 256) this.leftPlayerDMXStart2 = 256;
                    else this.leftPlayerDMXStart2 = value;
                    this.on_PropertyChanged();
                    this.SetDMXValues();
                }
            }
        }

        private int leftPlayerDMXStart3 = 7;
        public int LeftPlayerDMXStart3 {
            get { return this.leftPlayerDMXStart3; }
            set {
                if (this.leftPlayerDMXStart3 != value) {
                    if (value < 1) this.leftPlayerDMXStart3 = 1;
                    else if (value > 256) this.leftPlayerDMXStart3 = 256;
                    else this.leftPlayerDMXStart3 = value;
                    this.on_PropertyChanged();
                    this.SetDMXValues();
                }
            }
        }

        private int rightPlayerDMXStart1 = 10;
        public int RightPlayerDMXStart1 {
            get { return this.rightPlayerDMXStart1; }
            set {
                if (this.rightPlayerDMXStart1 != value) {
                    if (value < 1) this.rightPlayerDMXStart1 = 1;
                    else if (value > 256) this.rightPlayerDMXStart1 = 256;
                    else this.rightPlayerDMXStart1 = value;
                    this.on_PropertyChanged();
                    this.SetDMXValues();
                }
            }
        }

        private int rightPlayerDMXStart2 = 13;
        public int RightPlayerDMXStart2 {
            get { return this.rightPlayerDMXStart2; }
            set {
                if (this.rightPlayerDMXStart2 != value) {
                    if (value < 1) this.rightPlayerDMXStart2 = 1;
                    else if (value > 256) this.rightPlayerDMXStart2 = 256;
                    else this.rightPlayerDMXStart2 = value;
                    this.on_PropertyChanged();
                    this.SetDMXValues();
                }
            }
        }

        private int rightPlayerDMXStart3 = 16;
        public int RightPlayerDMXStart3 {
            get { return this.rightPlayerDMXStart3; }
            set {
                if (this.rightPlayerDMXStart3 != value) {
                    if (value < 1) this.rightPlayerDMXStart3 = 1;
                    else if (value > 256) this.rightPlayerDMXStart3 = 256;
                    else this.rightPlayerDMXStart3 = value;
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

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.TripleBuzzerScore'", typeIdentifier);
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

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;

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
        }

        public override void ResetData() {
            base.ResetData();
            this.LeftPlayerBuzzerIdle1 = true;
            this.LeftPlayerBuzzerIdle2 = true;
            this.LeftPlayerBuzzerIdle3 = true;
            this.RightPlayerBuzzerIdle1 = true;
            this.RightPlayerBuzzerIdle2 = true;
            this.RightPlayerBuzzerIdle3 = true;
        }

        public override void Next() {
            this.LeftPlayerBuzzerIdle1 = true;
            this.LeftPlayerBuzzerIdle2 = true;
            this.LeftPlayerBuzzerIdle3 = true;
            this.RightPlayerBuzzerIdle1 = true;
            this.RightPlayerBuzzerIdle2 = true;
            this.RightPlayerBuzzerIdle3 = true;
        }

        private void fillIOUnitList(
            InfoParam[] unitInfoList) {
            this.ioUnitNameList.Clear();
            if (unitInfoList is InfoParam[]) {
                foreach (InfoParam item in unitInfoList) {
                    if (item is InfoParam) this.ioUnitNameList.Add(item.Name);
                }
            }
            this.on_PropertyChanged("IOUnitList");
        }

        private void checkIOUnitStatus() {
            BuzzerUnitStates ioUnitStatus = BuzzerUnitStates.NotAvailable;
            if (this.ioUnitNameList.Contains(this.IOUnitName)) {
                switch (this.ioUnitConnectionStatus) {
                    case Tools.NetContact.ClientStates.Connected:
                        ioUnitStatus = BuzzerUnitStates.Connected;
                        break;
                    case Tools.NetContact.ClientStates.Connecting:
                        ioUnitStatus = BuzzerUnitStates.Connecting;
                        break;
                    case Tools.NetContact.ClientStates.Disconnected:
                        ioUnitStatus = BuzzerUnitStates.Disconnected;
                        break;
                    case Tools.NetContact.ClientStates.Missing:
                    default:
                        ioUnitStatus = BuzzerUnitStates.Missing;
                        break;
                }
                if (ioUnitStatus == BuzzerUnitStates.Connected) {
                    switch (this.ioUnitWorkMode) {
                        case WorkModes.BUZZER:
                            ioUnitStatus = BuzzerUnitStates.BuzzerMode;
                            break;
                        case WorkModes.EVENT:
                            ioUnitStatus = BuzzerUnitStates.EventMode;
                            break;
                        case WorkModes.LOCK:
                            ioUnitStatus = BuzzerUnitStates.Locked;
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
            if (buzzerHandler is BuzzerIO.Business) {
                this.buzzerHandler.RequestUnitConnectionStatus(unitName);
                this.buzzerHandler.RequestUnitWorkMode(unitName);
            }
        }

        internal void LeftPlayerDMXOn(int v)
        {
            byte[] onValue = new byte[] { this.LeftPlayerColor.R, this.LeftPlayerColor.G, this.LeftPlayerColor.B };
            if (v == 1) this.SetDMXValues(this.LeftPlayerDMXStart1, onValue);
            else if (v == 2) this.SetDMXValues(this.LeftPlayerDMXStart2, onValue);
            else if (v == 3) this.SetDMXValues(this.LeftPlayerDMXStart3, onValue);
        }

        internal void LeftPlayerDMXOff(int v)
        {
            byte[] offValue = new byte[] { this.OffColor.R, this.OffColor.G, this.OffColor.B };
            if (v == 1) this.SetDMXValues(this.LeftPlayerDMXStart1, offValue);
            else if (v == 2) this.SetDMXValues(this.LeftPlayerDMXStart2, offValue);
            else if (v == 3) this.SetDMXValues(this.LeftPlayerDMXStart3, offValue);
        }

        internal void RightPlayerDMXOn(int v)
        {
            byte[] onValue = new byte[] { this.RightPlayerColor.R, this.RightPlayerColor.G, this.RightPlayerColor.B };
            if (v == 1) this.SetDMXValues(this.RightPlayerDMXStart1, onValue);
            else if (v == 2) this.SetDMXValues(this.RightPlayerDMXStart2, onValue);
            else if (v == 3) this.SetDMXValues(this.RightPlayerDMXStart3, onValue);
        }

        internal void RightPlayerDMXOff(int v)
        {
            byte[] offValue = new byte[] { this.OffColor.R, this.OffColor.G, this.OffColor.B };
            if (v == 1) this.SetDMXValues(this.RightPlayerDMXStart1, offValue);
            else if (v == 2) this.SetDMXValues(this.RightPlayerDMXStart2, offValue);
            else if (v == 3) this.SetDMXValues(this.RightPlayerDMXStart3, offValue);
        }

        internal void AllLightsBlack()
        {
            byte[] valueList = new byte[512];
            this.SetDMXValues(1, valueList);
        }

        private void SetDMXValues() {
            if (this.LeftPlayerBuzzerIdle1) this.LeftPlayerDMXOff(1);
            else this.LeftPlayerDMXOn(1);
            if (this.LeftPlayerBuzzerIdle2) this.LeftPlayerDMXOff(2);
            else this.LeftPlayerDMXOn(2);
            if (this.LeftPlayerBuzzerIdle3) this.LeftPlayerDMXOff(3);
            else this.LeftPlayerDMXOn(3);
            if (this.RightPlayerBuzzerIdle1) this.RightPlayerDMXOff(1);
            else this.RightPlayerDMXOn(1);
            if (this.RightPlayerBuzzerIdle2) this.RightPlayerDMXOff(2);
            else this.RightPlayerDMXOn(2);
            if (this.RightPlayerBuzzerIdle3) this.RightPlayerDMXOff(3);
            else this.RightPlayerDMXOn(3);
        }

        internal void SetDMXValues(
            int startChannel,
            byte[] values)
        {
            try
            {
                if (this.buzzerHandler is BuzzerIO.Business)
                {
                    this.buzzerHandler.SetDMXMode(this.IOUnitName, IOnet.IOUnit.IONbuzDX.DMXBuzzerModes.OFF);
                    this.buzzerHandler.SetDMXChannel(this.IOUnitName, startChannel, values);
                }
                if (this.dMX is Tools.DMX.DMXNet)
                {
                    byte startIndex = Convert.ToByte(startChannel - 1);
                    if (startIndex + values.Length <= this.universe.Length) Array.Copy(values, 0, this.universe, startIndex, values.Length);
                    this.dMX.SetDmxDataToEspNet(0, 0, this.universe);
                }
            }
            catch (Exception) { }
        }

        public virtual void ReleaseBuzzer() {
            bool[] inputMask = new bool[8];
            if (this.LeftPlayerBuzzerChannel1 > 0 &&
                this.LeftPlayerBuzzerChannel1 <= inputMask.Length) inputMask[this.LeftPlayerBuzzerChannel1 - 1] = true;
            if (this.LeftPlayerBuzzerChannel2 > 0 &&
                this.LeftPlayerBuzzerChannel2 <= inputMask.Length) inputMask[this.LeftPlayerBuzzerChannel2 - 1] = true;
            if (this.LeftPlayerBuzzerChannel3 > 0 &&
                this.LeftPlayerBuzzerChannel3 <= inputMask.Length) inputMask[this.LeftPlayerBuzzerChannel3 - 1] = true;
            if (this.RightPlayerBuzzerChannel1 > 0 &&
                this.RightPlayerBuzzerChannel1 <= inputMask.Length) inputMask[this.RightPlayerBuzzerChannel1 - 1] = true;
            if (this.RightPlayerBuzzerChannel2 > 0 &&
                this.RightPlayerBuzzerChannel2 <= inputMask.Length) inputMask[this.RightPlayerBuzzerChannel2 - 1] = true;
            if (this.RightPlayerBuzzerChannel3 > 0 &&
                this.RightPlayerBuzzerChannel3 <= inputMask.Length) inputMask[this.RightPlayerBuzzerChannel3 - 1] = true;
            this.buzzerHandler.SetInputMask(this.IOUnitName, inputMask);
            this.buzzerHandler.ReleaseBuzzer(this.IOUnitName, WorkModes.EVENT);
        }

        public virtual void LockBuzzer() {
            this.buzzerHandler.LockBuzzer(this.IOUnitName);
        }

        private void checkFinished() {
            if (!this.LeftPlayerBuzzerIdle1 &&
                !this.LeftPlayerBuzzerIdle2 &&
                !this.LeftPlayerBuzzerIdle3) {
                this.LockBuzzer();
                this.Vinsert_PlayJingleBuzzerLeft();
            }
            else if (!this.RightPlayerBuzzerIdle1 &&
                !this.RightPlayerBuzzerIdle2 &&
                !this.RightPlayerBuzzerIdle3) {
                this.LockBuzzer();
                this.Vinsert_PlayJingleBuzzerRight();
            }
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }
        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }
        public void Vinsert_PlayJingleBuzzerLeft() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlayJingleBuzzerLeft(); }
        public void Vinsert_PlayJingleBuzzerRight() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlayJingleBuzzerRight(); }
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

        void buzzerHandler_BuzUnit_Buzzered(object sender, BuzzerIDParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_Buzzered);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_BuzUnit_Buzzered(object content) {
            BuzzerIDParam_EventArgs e = content as BuzzerIDParam_EventArgs;
            if (this.isActive &&
                e is BuzzerIDParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                if (e.Arg.BuzzerID == this.LeftPlayerBuzzerChannel1) this.LeftPlayerBuzzerIdle1 = false;
                else if (e.Arg.BuzzerID == this.LeftPlayerBuzzerChannel2) this.LeftPlayerBuzzerIdle2 = false;
                else if (e.Arg.BuzzerID == this.LeftPlayerBuzzerChannel3) this.LeftPlayerBuzzerIdle3 = false;
                else if (e.Arg.BuzzerID == this.RightPlayerBuzzerChannel1) this.RightPlayerBuzzerIdle1 = false;
                else if (e.Arg.BuzzerID == this.RightPlayerBuzzerChannel2) this.RightPlayerBuzzerIdle2 = false;
                else if (e.Arg.BuzzerID == this.RightPlayerBuzzerChannel3) this.RightPlayerBuzzerIdle3 = false;
                this.checkFinished();
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
                if (e.Arg.ConnectionStatus == Tools.NetContact.ClientStates.Connected) this.SetDMXValues();
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

        #endregion

    }
}
