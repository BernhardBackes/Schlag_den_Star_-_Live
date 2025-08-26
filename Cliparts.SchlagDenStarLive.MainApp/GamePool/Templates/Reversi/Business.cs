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

using Cliparts.IOnet.IOUnit.IONbase;
using Cliparts.IOnet.IOUnit.IONbuz;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.Reversi;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.Reversi {

    public class Field : INotifyPropertyChanged {

        #region Properties

        private int index;
        public int Index { get { return this.index; } }

        public int ID { get { return this.index + 1; } }

        public int Row { get; private set; }
        public int Column { get; private set; }

        public string Name { get; private set; }

        private Content.Gameboard.PlayerSelection selectedPlayer = Content.Gameboard.PlayerSelection.NotSelected;
        public Content.Gameboard.PlayerSelection SelectedPlayer {
            get { return this.selectedPlayer; }
            set {
                if (this.selectedPlayer != value) {
                    this.selectedPlayer = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int dmxStartAddress = 1;
        public int DMXStartAddress {
            get { return this.dmxStartAddress; }
            set {
                if (this.dmxStartAddress != value) {
                    if (value < 1) value = 1;
                    if (value > 512) value = 512;
                    this.dmxStartAddress = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public Field() { }
        public Field(
            int index) {
            this.index = index;
            this.dmxStartAddress = index * 3 + 1;
            this.buildName();
        }

        public void Dispose() {
        }

        public void SelectField() { this.on_Select(this); }

        public void Reset() { this.SelectedPlayer = Content.Gameboard.PlayerSelection.NotSelected; }

        public void Clone(
            Field field) {
            if (field is Field) {
                this.SelectedPlayer = field.SelectedPlayer;
                this.DMXStartAddress = field.DMXStartAddress;
            }
            else this.Reset();
        }

        private void buildName() {
            this.Row = (this.Index / Business.MatrixDimension) + 1;
            this.Column = (this.Index % Business.MatrixDimension) + 1;
            this.Name = Char.ConvertFromUtf32(64 + this.Row) + this.Column.ToString();
        }

        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void on_PropertyChanged([CallerMemberName]string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected virtual void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected virtual void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        public event EventHandler Select;
        protected virtual void on_Select(object sender) { Helper.raiseEvent(sender, this.Select, new EventArgs()); }

        #endregion

        #region Events.Incoming
        #endregion
    }

    public class Business : _Base.Score.Business {

        public const int FieldsCount = 36;
        public const int MatrixDimension = 6;

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

        private int scorePointerPositionX = 0;
        public int ScorePointerPositionX {
            get { return this.scorePointerPositionX; }
            set {
                if (this.scorePointerPositionX != value) {
                    this.scorePointerPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetScorePointer();
                }
            }
        }

        private int scorePointerPositionY = 0;
        public int ScorePointerPositionY {
            get { return this.scorePointerPositionY; }
            set {
                if (this.scorePointerPositionY != value) {
                    this.scorePointerPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetScorePointer();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.ScorePointer.Styles scorePointerStyle = VentuzScenes.GamePool._Modules.ScorePointer.Styles.Points20;
        public VentuzScenes.GamePool._Modules.ScorePointer.Styles ScorePointerStyle {
            get { return this.scorePointerStyle; }
            set {
                if (this.scorePointerStyle != value) {
                    this.scorePointerStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetScorePointer();
                }
            }
        }

        private int pointer = 0;
        public int Pointer {
            get { return this.pointer; }
            set {
                if (this.pointer != value) {
                    if (value < 0) value = 0;
                    this.pointer = value;
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

        private List<Field> fieldList = new List<Field>();
        public Field[] FieldList {
            get {
                this.fillFieldList();
                return this.fieldList.ToArray();
            }
            set {
                this.fillFieldList();
                for (int i = 0; i < FieldsCount; i++) {
                    if (value is Field[] &&
                        value.Length > i) this.fieldList[i].Clone(value[i]);
                }
                this.countFields();
            }
        }

        private List<Content.Gameboard.PlayerSelection> undoList = new List<Content.Gameboard.PlayerSelection>();

        private Color notSelectedColor = Color.Red;
        [Cliparts.Serialization.NotSerialized]
        public Color NotSelectedColor {
            get { return this.notSelectedColor; }
            set {
                if (this.notSelectedColor != value) {
                    this.notSelectedColor = value;
                    this.on_PropertyChanged();
                }
            }
        }
        public int NotSelectedColorARGB {
            get { return this.notSelectedColor.ToArgb(); }
            set { this.notSelectedColor = Color.FromArgb(value); }
        }

        private Color leftPlayerColor = Color.Red;
        [Cliparts.Serialization.NotSerialized]
        public Color LeftPlayerColor {
            get { return this.leftPlayerColor; }
            set {
                if (this.leftPlayerColor != value) {
                    this.leftPlayerColor = value;
                    this.on_PropertyChanged();
                }
            }
        }
        public int LeftPlayerColorARGB {
            get { return this.leftPlayerColor.ToArgb(); }
            set { this.leftPlayerColor = Color.FromArgb(value); }
        }

        private Color rightPlayerColor = Color.Blue;
        [Cliparts.Serialization.NotSerialized]
        public Color RightPlayerColor {
            get { return this.rightPlayerColor; }
            set {
                if (this.rightPlayerColor != value) {
                    this.rightPlayerColor = value;
                    this.on_PropertyChanged();
                }
            }
        }
        public int RightPlayerColorARGB {
            get { return this.rightPlayerColor.ToArgb(); }
            set { this.rightPlayerColor = Color.FromArgb(value); }
        }

        private int leftPlayerFieldsCount = 0;
        public int LeftPlayerFieldsCount {
            get { return this.leftPlayerFieldsCount; }
            private set {
                if (this.leftPlayerFieldsCount != value) {
                    this.leftPlayerFieldsCount = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerFieldsCount = 0;
        public int RightPlayerFieldsCount {
            get { return this.rightPlayerFieldsCount; }
            private set {
                if (this.rightPlayerFieldsCount != value) {
                    this.rightPlayerFieldsCount = value;
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

        private VRemote4.HandlerSi.Business localVentuzHandler;

        private VRemote4.HandlerSi.Client.Business tabletClient;
        private Tablet ventuzTabletScene;
        public VRemote4.HandlerSi.Scene.States VentuzTabletSceneStatus {
            get {
                if (this.ventuzTabletScene is Tablet) return this.ventuzTabletScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }
        private string tabletClientHostname = string.Empty;
        public string TabletClientHostname {
            get { return this.tabletClientHostname; }
            set {
                if (this.tabletClientHostname != value) {
                    if (value == null) value = string.Empty;
                    this.tabletClientHostname = value;
                    this.on_PropertyChanged();
                }
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.Reversi'", typeIdentifier);
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

            this.fillFieldList();

            this.buzzerHandler = buzzerHandler;
            this.buzzerHandler.Unit_ConnectionStatusChanged += this.buzzerHandler_UnitConnectionStatusChanged;
            this.buzzerHandler.Unit_ConnectionStatusRequest += this.buzzerHandler_UnitConnectionStatusRequest;
            this.buzzerHandler.Unit_InfoListChanged += this.buzzerHandler_UnitInfoListChanged;
            this.fillIOUnitList(this.buzzerHandler.UnitInfoList);
            this.buzzerHandler.RequestUnitConnectionStatus(this.IOUnitName);
            this.buzzerHandler.RequestUnitWorkMode(this.IOUnitName);

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;

            this.localVentuzHandler = new VRemote4.HandlerSi.Business(syncContext);
            this.localVentuzHandler.Error += this.localVentuzHandler_Error;

            if (this.localVentuzHandler.TryAddClient("Tablet", false, out this.tabletClient)) {
                this.tabletClient.HostnameChanged += this.tabletClient_HostnameChanged;
                this.tabletClient.StatusChanged += this.tabletClient_StatusChanged;
                this.ventuzTabletScene = new Tablet(syncContext, this.tabletClient, 0);
                this.ventuzTabletScene.StatusChanged += this.ventuzTabletScene_StatusChanged;
                this.ventuzTabletScene.FieldPressed += this.ventuzTabletScene_FieldPressed;
            }

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this, this.localVentuzHandler);
        }

        public override void Dispose() {
            base.Dispose();
            foreach (Field field in this.fieldList) {
                field.PropertyChanged -= this.field_PropertyChanged;
                field.Select -= this.field_Select;
            }
            this.fieldList.Clear();

            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
        }

        public override void Init() {
            base.Init();
            this.LeftPlayerScore = 0;
            this.RightPlayerScore = 0;
            this.Pointer = 0;
            this.SelectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
            foreach (Field item in this.fieldList) item.Reset();
            this.SetDMXValues();
        }

        protected void fillFieldList() {
            bool fieldAdded = false;
            while (this.fieldList.Count < FieldsCount) {
                Field field = new Field(this.fieldList.Count);
                field.PropertyChanged += this.field_PropertyChanged;
                field.Select += this.field_Select;
                this.fieldList.Add(field);
                fieldAdded = true;
            }
            if (fieldAdded) this.countFields();
        }

        private void fillIOUnitList(
            Cliparts.IOnet.IOUnit.IONbase.InfoParam[] unitInfoList) {
            this.ioUnitNameList.Clear();
            if (unitInfoList is Cliparts.IOnet.IOUnit.IONbase.InfoParam[]) {
                foreach (Cliparts.IOnet.IOUnit.IONbase.InfoParam item in unitInfoList) {
                    if (item is Cliparts.IOnet.IOUnit.IONbase.InfoParam) this.ioUnitNameList.Add(item.Name);
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

        internal void SetDMXValues() {
            foreach (Field item in this.fieldList) {
                Color fieldColor;
                switch (item.SelectedPlayer) {
                    case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.LeftPlayer:
                        fieldColor = this.LeftPlayerColor;
                        break;
                    case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.RightPlayer:
                        fieldColor = this.RightPlayerColor;
                        break;
                    case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.NotSelected:
                    default:
                        fieldColor = this.NotSelectedColor;
                        break;
                }
                this.buzzerHandler.SetDMXChannel(this.IOUnitName, item.DMXStartAddress, new byte[] {fieldColor.R, fieldColor.G, fieldColor.B});
            }
        }

        public void NextPlayer() {
            if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) this.SelectedPlayer = Content.Gameboard.PlayerSelection.RightPlayer;
            else this.SelectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
        }

        public override void Next() {
            this.Pointer = 0;
            foreach (Field item in this.fieldList) item.Reset();
            this.countFields();
            this.SetDMXValues();
        }

        public void SelectField(
            int index) {
            if (index >= 0 &&
                index < this.FieldList.Length) this.SelectField(this.FieldList[index]);
        }
        public void SelectField(
            Field field) {
            this.buildUnDoList();
            if (field is Field &&
                field.SelectedPlayer == Content.Gameboard.PlayerSelection.NotSelected) {
                field.SelectedPlayer = this.SelectedPlayer;
                this.SetDMXValues();
                this.checkContraryNeighbourField(field, this.selectedPlayer);
            }
            this.countFields();
        }

        private void checkContraryNeighbourField(
            Field field,
            Content.Gameboard.PlayerSelection selectedPlayer) {
            Content.Gameboard.PlayerSelection contraryPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
            if (selectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) contraryPlayer = Content.Gameboard.PlayerSelection.RightPlayer;
            int[] checkArray = new int[] { // umlaufende Indices
                -MatrixDimension - 1,
                -MatrixDimension,
                -MatrixDimension + 1,
                1,
                MatrixDimension + 1,
                MatrixDimension,
                MatrixDimension - 1,
                -1};
            for (int rowOrientation = 0; rowOrientation < checkArray.Length; rowOrientation++) {
                int checkIndex = field.Index + checkArray[rowOrientation];
                if (checkIndex >= 0 &&
                    checkIndex < this.FieldList.Length &&
                    this.fieldIsInOrientation(field, this.FieldList[checkIndex], rowOrientation) &&
                    this.FieldList[checkIndex].SelectedPlayer == contraryPlayer &&
                    this.validFieldInRow(this.FieldList[checkIndex], selectedPlayer, checkArray, rowOrientation)) {
                    this.FieldList[checkIndex].SelectedPlayer = selectedPlayer;
                }
            }
        }

        private bool validFieldInRow(
            Field field,
            Content.Gameboard.PlayerSelection selectedPlayer,
            int[] checkArray,
            int rowOrientation) {
            int checkIndex = field.Index + checkArray[rowOrientation];
            if (checkIndex >= 0 &&
                checkIndex < this.FieldList.Length &&
                this.FieldList[checkIndex].SelectedPlayer != Content.Gameboard.PlayerSelection.NotSelected &&
                this.fieldIsInOrientation(field, this.FieldList[checkIndex], rowOrientation)) {
                if (this.FieldList[checkIndex].SelectedPlayer == selectedPlayer) {
                    //gültiges Ende erreicht
                    field.SelectedPlayer = selectedPlayer;
                    return true;
                }
                else {
                    //weiter suchen
                    if (this.validFieldInRow(this.FieldList[checkIndex], selectedPlayer, checkArray, rowOrientation)) {
                        field.SelectedPlayer = selectedPlayer;
                        return true;
                    }
                    else return false;
                }
            }
            else return false;
        }
        private bool fieldIsInOrientation(
            Field field,
            Field checkField,
            int rowOrientation) {
            if (rowOrientation == 0) return field.Row > checkField.Row;
            else if (rowOrientation == 1) return field.Row > checkField.Row;
            else if (rowOrientation == 2) return field.Row > checkField.Row;
            else if (rowOrientation == 3) return field.Row == checkField.Row;
            else if (rowOrientation == 4) return field.Row < checkField.Row;
            else if (rowOrientation == 5) return field.Row < checkField.Row;
            else if (rowOrientation == 6) return field.Row < checkField.Row;
            else return field.Row == checkField.Row;
        }

        private void buildUnDoList() {
            this.undoList.Clear();
            foreach (Field item in this.FieldList) this.undoList.Add(item.SelectedPlayer);
        }
        public void UnDo() {
            int index = 0;
            foreach (Content.Gameboard.PlayerSelection item in this.undoList) {
                if (index >= 0 &&
                    index < this.fieldList.Count) this.fieldList[index].SelectedPlayer = item;
                index++;
            }
            this.SetDMXValues();
        }

        private void countFields() {
            int leftPlayerFieldsCount = 0;
            int rightPlayerFieldsCount = 0;
            foreach (Field item in this.FieldList) {
                switch (item.SelectedPlayer) {
                    case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.LeftPlayer:
                        leftPlayerFieldsCount++;
                        break;
                    case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.RightPlayer:
                        rightPlayerFieldsCount++;
                        break;
                }
            }
            this.LeftPlayerFieldsCount = leftPlayerFieldsCount;
            this.RightPlayerFieldsCount = rightPlayerFieldsCount;
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }
        public void Vinsert_ScorePointerIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScorePointerIn(this.insertScene.ScorePointer); }
        public void Vinsert_ScorePointerIn(
            VentuzScenes.GamePool._Modules.ScorePointer scene) {
            if (scene is VentuzScenes.GamePool._Modules.ScorePointer) {
                this.Vinsert_SetScorePointer();
                scene.ToIn();
            }
        }
        public void Vinsert_SetScorePointer() { if (this.insertScene is VRemote4.HandlerSi.Scene)  this.Vinsert_SetScorePointer(this.insertScene.ScorePointer, this.Pointer, this.LeftPlayerFieldsCount, this.RightPlayerFieldsCount); }
        public void Vinsert_SetScorePointer(
            VentuzScenes.GamePool._Modules.ScorePointer scene,
            int pointer,
            int leftPlayerScore,
            int rightPlayerScore) {
            if (scene is VentuzScenes.GamePool._Modules.ScorePointer) {
                scene.SetPositionX(this.ScorePointerPositionX);
                scene.SetPositionY(this.ScorePointerPositionY);
                scene.SetFlipPosition(this.FlipPlayers);
                scene.SetStyle(this.ScorePointerStyle);
                scene.SetPointer(pointer);
                scene.SetLeftTopName(this.LeftPlayerName);
                scene.SetLeftTopScore(leftPlayerScore);
                scene.SetRightBottomName(this.RightPlayerName);
                scene.SetRightBottomScore(rightPlayerScore);
            }
        }
        public void Vinsert_ScorePointerOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScorePointerOut(this.insertScene.ScorePointer); }
        public void Vinsert_ScorePointerOut(
            VentuzScenes.GamePool._Modules.ScorePointer scene) {
            if (scene is VentuzScenes.GamePool._Modules.ScorePointer) scene.ToOut();
        }

        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }
        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
        }

        public void Vtablet_Start() { this.tabletClient.Start(this.TabletClientHostname); }
        public void Vtablet_ShutDown() { this.tabletClient.Shutdown(); }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming

        void buzzerHandler_UnitConnectionStatusChanged(object sender, IOnet.IOUnit.IONbase.ConnectionStatusParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_UnitConnectionStatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        void buzzerHandler_UnitConnectionStatusRequest(object sender, IOnet.IOUnit.IONbase.ConnectionStatusParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_UnitConnectionStatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_UnitConnectionStatusChanged(object content) {
            IOnet.IOUnit.IONbase.ConnectionStatusParam_EventArgs e = content as IOnet.IOUnit.IONbase.ConnectionStatusParam_EventArgs;
            if (e is IOnet.IOUnit.IONbase.ConnectionStatusParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                this.ioUnitConnectionStatus = e.Arg.ConnectionStatus;
                this.checkIOUnitStatus();
                if (e.Arg.ConnectionStatus == Tools.NetContact.ClientStates.Connected) this.SetDMXValues();
            }
        }

        void buzzerHandler_UnitInfoListChanged(object sender, IOnet.IOUnit.IONbase.InfoParamArray_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_UnitInfoListChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_UnitInfoListChanged(object content) {
            IOnet.IOUnit.IONbase.InfoParamArray_EventArgs e = content as IOnet.IOUnit.IONbase.InfoParamArray_EventArgs;
            if (e is IOnet.IOUnit.IONbase.InfoParamArray_EventArgs) {
                this.ioUnitInfo = e;
                this.fillIOUnitList(e.Arg);
            }
        }

        void field_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            this.on_PropertyChanged(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_field_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_field_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "SelectedPlayer") this.countFields();
            }
        }

        void field_Select(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_field_Select);
            if (this.syncContext != null) this.syncContext.Post(callback, sender);
        }
        private void sync_field_Select(object content) {
            Field field = content as Field;
            if (field is Field) this.SelectField(field);
        }

        void localVentuzHandler_Error(object sender, System.IO.ErrorEventArgs e) {
            Exception exc = e.GetException();
            this.on_Error(sender, new Messaging.ErrorEventArgs(this, sender.ToString(), DateTime.Now, exc.ToString()));
        }

        void tabletClient_HostnameChanged(object sender, VRemote4.HandlerSi.Client.HostnameArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_tabletClient_HostnameChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_tabletClient_HostnameChanged(object content) {
            VRemote4.HandlerSi.Client.HostnameArgs e = content as VRemote4.HandlerSi.Client.HostnameArgs;
            if (e is VRemote4.HandlerSi.Client.HostnameArgs) this.TabletClientHostname = e.Name;
        }

        void tabletClient_StatusChanged(object sender, VRemote4.HandlerSi.Client.ClientStateArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_tabletClient_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_tabletClient_StatusChanged(object content) {
            VRemote4.HandlerSi.Client.ClientStateArgs e = content as VRemote4.HandlerSi.Client.ClientStateArgs;
            if (e is VRemote4.HandlerSi.Client.ClientStateArgs &&
                e.Status.HasValue &&
                e.Status.Value == VRemote4.HandlerSi.Client.ClientStates.Active &&
                this.ventuzTabletScene is VRemote4.HandlerSi.Scene) this.ventuzTabletScene.Load();
        }

        void ventuzTabletScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzTabletScene_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzTabletScene_StatusChanged(object content) {
            VRemote4.HandlerSi.Scene.StatusArgs e = content as VRemote4.HandlerSi.Scene.StatusArgs;
            if (e is VRemote4.HandlerSi.Scene.StatusArgs) {
                if (e.Status == VRemote4.HandlerSi.Scene.States.Unloaded) this.tabletClient.Shutdown();
                //else if (e.Status == VRemote4.HandlerSi.Scene.States.Available) this.Vtablets_Init();
            }
            this.on_PropertyChanged("VentuzTabletSceneStatus");
        }

        void ventuzTabletScene_FieldPressed(object sender, int e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzTabletScene_FieldPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzTabletScene_FieldPressed(object content) {
            if (content is int) {
                int id = (int)content;
                this.SelectField(id - 1);
            }
        }

        #endregion

    }
}
