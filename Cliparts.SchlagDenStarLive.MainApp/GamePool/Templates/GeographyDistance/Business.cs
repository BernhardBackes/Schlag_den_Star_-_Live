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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.GeographyDistance;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.GeographyDistance {

    public class DatasetContent : Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private string name = string.Empty;
        [XmlIgnore]
        public string Name {
            get { return this.name; }
            private set {
                if (this.name != value) {
                    if (string.IsNullOrEmpty(value)) this.name = string.Empty;
                    else this.name = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Fullscreen.MapLayoutElements mapLayout;
        public Fullscreen.MapLayoutElements MapLayout {
            get { return this.mapLayout; }
            set {
                if (this.mapLayout != value) {
                    this.mapLayout = value;
                    this.on_PropertyChanged();
                }
                if (this.firstLocationCoordinates is CoordinateSet) this.firstLocationCoordinates.MapLayout = value;
                if (this.secondLocationCoordinates is CoordinateSet) this.secondLocationCoordinates.MapLayout = value;
            }
        }

        private int distance = 0;
        public int Distance
        {
            get { return this.distance; }
            set { 
                if (this.distance != value)
                {
                    if (value <  0) this.distance = 0;
                    else this.distance = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string firstLocationName = "?";
        public string FirstLocationName
        {
            get { return this.firstLocationName; }
            set
            {
                if (this.firstLocationName != value)
                {
                    if (string.IsNullOrEmpty(value)) this.firstLocationName = string.Empty;
                    else this.firstLocationName = value;
                    this.on_PropertyChanged();
                    this.buildToString();
                }
            }
        }

        private CoordinateSet firstLocationCoordinates = new CoordinateSet(Fullscreen.MapLayoutElements.Africa);
        public CoordinateSet FirstLocationCoordinates
        {
            get { return this.firstLocationCoordinates; }
            set
            {
                if (!(value is CoordinateSet)) value = new CoordinateSet(this.MapLayout);
                if (this.firstLocationCoordinates.Longitude.Text != value.Longitude.Text ||
                    this.firstLocationCoordinates.Latitude.Text != value.Latitude.Text)
                {
                    this.firstLocationCoordinates.Longitude.Text = value.Longitude.Text;
                    this.firstLocationCoordinates.Latitude.Text = value.Latitude.Text;
                    this.on_PropertyChanged();
                }
            }
        }

        private string secondLocationName = "?";
        public string SecondLocationName
        {
            get { return this.secondLocationName; }
            set
            {
                if (this.secondLocationName != value)
                {
                    if (string.IsNullOrEmpty(value)) this.secondLocationName = string.Empty;
                    else this.secondLocationName = value;
                    this.on_PropertyChanged();
                    this.buildToString();
                }
            }
        }

        private CoordinateSet secondLocationCoordinates = new CoordinateSet(Fullscreen.MapLayoutElements.Africa);
        public CoordinateSet SecondLocationCoordinates
        {
            get { return this.secondLocationCoordinates; }
            set
            {
                if (!(value is CoordinateSet)) value = new CoordinateSet(this.MapLayout);
                if (this.secondLocationCoordinates.Longitude.Text != value.Longitude.Text ||
                    this.secondLocationCoordinates.Latitude.Text != value.Latitude.Text)
                {
                    this.secondLocationCoordinates.Longitude.Text = value.Longitude.Text;
                    this.secondLocationCoordinates.Latitude.Text = value.Latitude.Text;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public DatasetContent() { this.pose(); }
        public DatasetContent(
            Fullscreen.MapLayoutElements mapLayout) {
            this.MapLayout = mapLayout;
            this.Distance = 478;
            this.FirstLocationName = "Köln";
            this.FirstLocationCoordinates = new CoordinateSet(this.MapLayout, new Coordinate("50° 56' 17\" N"), new Coordinate("6° 57' 25\" E"));
            this.SecondLocationName = "Berlin";
            this.SecondLocationCoordinates = new CoordinateSet(this.MapLayout, new Coordinate("52° 31' 7\" N"), new Coordinate("13° 24' 30\" E"));
            this.pose();
        }
        public DatasetContent(
            Fullscreen.MapLayoutElements mapLayout,
            int distance,
            string firstLocationName,
            CoordinateSet firstLocationCoordinates,
            string secondLocationName,
            CoordinateSet secondLocationCoordinates) {
            this.MapLayout = mapLayout;
            this.Distance = distance;
            this.firstLocationName = firstLocationName;
            this.FirstLocationCoordinates = firstLocationCoordinates;
            this.secondLocationName = secondLocationName;
            this.SecondLocationCoordinates = secondLocationCoordinates;
            this.buildToString();
            this.pose();
        }
        private void pose() {
            this.firstLocationCoordinates.PropertyChanged += this.coordinates_PropertyChanged;
            this.secondLocationCoordinates.PropertyChanged += this.coordinates_PropertyChanged;
        }

        public void Dispose() {
            this.firstLocationCoordinates.PropertyChanged -= this.coordinates_PropertyChanged;
            this.secondLocationCoordinates.PropertyChanged -= this.coordinates_PropertyChanged;
        }

        private void buildToString() {
            this.Name = string.Format("{0} - {1}", this.FirstLocationName, this.SecondLocationName);
        }

        public override string ToString() { return this.Name; }

        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_PropertyChanged([CallerMemberName]string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        #endregion

        #region Events.Incoming

        void coordinates_PropertyChanged(object sender, PropertyChangedEventArgs e) { this.on_PropertyChanged(sender, e); }

        #endregion
    }

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class Business : _Base.TimerScore.Business {

        #region Properties

        private int textInsertPositionX = 0;
        public int TextInsertPositionX {
            get { return this.textInsertPositionX; }
            set {
                if (this.textInsertPositionX != value) {
                    this.textInsertPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTextInsert();
                }
            }
        }

        private int textInsertPositionY = 0;
        public int TextInsertPositionY {
            get { return this.textInsertPositionY; }
            set {
                if (this.textInsertPositionY != value) {
                    this.textInsertPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTextInsert();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.TextInsert.Styles textInsertStyle = VentuzScenes.GamePool._Modules.TextInsert.Styles.TwoRows;
        public VentuzScenes.GamePool._Modules.TextInsert.Styles TextInsertStyle {
            get { return this.textInsertStyle; }
            set {
                if (this.textInsertStyle != value) {
                    this.textInsertStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTextInsert();
                }
            }
        }

        private CoordinateSet leftPlayerInput1 = null;
        public String LeftPlayerInput1 {
            get {
                if (this.leftPlayerInput1 is CoordinateSet) return this.leftPlayerInput1.Text;
                else return string.Empty;
            }
            private set {
                if (this.LeftPlayerInput1 != value) {
                    if (string.IsNullOrEmpty(value)) this.leftPlayerInput1 = null;
                    else {
                        if (this.leftPlayerInput1 is CoordinateSet) this.leftPlayerInput1.Text = value;
                        else this.leftPlayerInput1 = new CoordinateSet(this.mapLayout, value);
                    }
                }
                this.on_PropertyChanged();
                this.calcPlayerDistances();
            }
        }

        private CoordinateSet leftPlayerInput2 = null;
        public String LeftPlayerInput2
        {
            get
            {
                if (this.leftPlayerInput2 is CoordinateSet) return this.leftPlayerInput2.Text;
                else return string.Empty;
            }
            private set
            {
                if (this.LeftPlayerInput2 != value)
                {
                    if (string.IsNullOrEmpty(value)) this.leftPlayerInput2 = null;
                    else
                    {
                        if (this.leftPlayerInput2 is CoordinateSet) this.leftPlayerInput2.Text = value;
                        else this.leftPlayerInput2 = new CoordinateSet(this.mapLayout, value);
                    }
                }
                this.on_PropertyChanged();
                this.calcPlayerDistances();
            }
        }

        private int? leftPlayerDistance = null;
        public String LeftPlayerDistance
        {
            get
            {
                if (this.leftPlayerDistance.HasValue) return string.Format("{0} km", this.leftPlayerDistance.Value.ToString("#,##0"));
                else return string.Empty;
            }
        }

        private int? leftPlayerOffset = null;

        private CoordinateSet rightPlayerInput1 = null;
        public String RightPlayerInput1 {
            get {
                if (this.rightPlayerInput1 is CoordinateSet) return this.rightPlayerInput1.Text;
                else return string.Empty;
            }
            private set {
                if (this.RightPlayerInput1 != value) {
                    if (string.IsNullOrEmpty(value)) this.rightPlayerInput1 = null;
                    else {
                        if (this.rightPlayerInput1 is CoordinateSet) this.rightPlayerInput1.Text = value;
                        else this.rightPlayerInput1 = new CoordinateSet(this.mapLayout, value);
                    }
                }
                this.on_PropertyChanged();
                this.calcPlayerDistances();
            }
        }

        private CoordinateSet rightPlayerInput2 = null;
        public String RightPlayerInput2
        {
            get
            {
                if (this.rightPlayerInput2 is CoordinateSet) return this.rightPlayerInput2.Text;
                else return string.Empty;
            }
            private set
            {
                if (this.RightPlayerInput2 != value)
                {
                    if (string.IsNullOrEmpty(value)) this.rightPlayerInput2 = null;
                    else
                    {
                        if (this.rightPlayerInput2 is CoordinateSet) this.rightPlayerInput2.Text = value;
                        else this.rightPlayerInput2 = new CoordinateSet(this.mapLayout, value);
                    }
                }
                this.on_PropertyChanged();
                this.calcPlayerDistances();
            }
        }

        private int? rightPlayerDistance = null;
        public String RightPlayerDistance
        {
            get
            {
                if (this.rightPlayerDistance.HasValue) return string.Format("{0} km", this.rightPlayerDistance.Value.ToString("#,##0"));
                else return string.Empty;
            }
        }

        private int? rightPlayerOffset= null;

        private Content.Gameboard.PlayerSelection closerPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
        public Content.Gameboard.PlayerSelection CloserPlayer {
            get { return this.closerPlayer; }
            private set {
                if (this.closerPlayer != value) {
                    this.closerPlayer = value;
                    this.on_PropertyChanged();
                }
            }
        }
        private string filename = string.Empty;
        public string Filename {
            get { return this.filename; }
            set { if (this.filename != value) this.Load(value); }
        }

        private List<DatasetContent> dataList = new List<DatasetContent>();
        [Cliparts.Serialization.NotSerialized]
        public DatasetContent[] DataList {
            get { return this.dataList.ToArray(); }
            set {
                this.tryRemoveAllDatasets();
                if (value is DatasetContent[]) {
                    foreach (DatasetContent item in value) this.tryAddDataset(item, -1);
                }
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                this.SelectDataset(0);
            }
        }

        private List<string> names = new List<string>();
        public string[] NameList { get { return this.names.ToArray(); } }

        public int DatasetsCount { get { return this.dataList.Count; } }

        public DatasetContent SelectedDataset { get; private set; }

        public int SelectedDatasetIndex { get; private set; }

        private Fullscreen.MapLayoutElements mapLayout {
            get {
                if (this.SelectedDataset is DatasetContent) return this.SelectedDataset.MapLayout;
                else return Fullscreen.MapLayoutElements.Africa;
            }
        }

        private int taskCounter = 0;
        public int TaskCounter {
            get { return this.taskCounter; }
            set {
                if (this.taskCounter != value) {
                    if (value < 0) value = 0;
                    if (!this.SampleIncluded &&
                        value < 1) value = 1;
                    this.taskCounter = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool sampleIncluded = true;
        public bool SampleIncluded {
            get { return this.sampleIncluded; }
            set {
                if (this.sampleIncluded != value) {
                    this.sampleIncluded = value;
                    this.on_PropertyChanged();
                    // TaskCounter validieren
                    int id = this.TaskCounter;
                    this.TaskCounter = -1;
                    this.TaskCounter = id;
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

        private Fullscreen fullscreenScene;
        public override VRemote4.HandlerSi.Scene.States FullscreenSceneStatus {
            get {
                if (this.fullscreenScene is Fullscreen) return this.fullscreenScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private Stage hostScene;
        public override VRemote4.HandlerSi.Scene.States HostSceneStatus {
            get {
                if (this.hostScene is Stage) return this.hostScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private Stage leftPlayerScene;
        public override VRemote4.HandlerSi.Scene.States LeftPlayerSceneStatus {
            get {
                if (this.leftPlayerScene is Stage) return this.leftPlayerScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private Stage rightPlayerScene;
        public override VRemote4.HandlerSi.Scene.States RightPlayerSceneStatus {
            get {
                if (this.rightPlayerScene is Stage) return this.rightPlayerScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private VRemote4.HandlerSi.Business localVentuzHandler;

        private VRemote4.HandlerSi.Client.Business leftTeamTabletClient;
        private PlayerTablet ventuzLeftTeamTabletScene;
        public VRemote4.HandlerSi.Scene.States VentuzLeftTeamTabletSceneStatus
        {
            get
            {
                if (this.ventuzLeftTeamTabletScene is PlayerTablet) return this.ventuzLeftTeamTabletScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }
        private string leftTeamTabletHostname = string.Empty;
        public string LeftTeamTabletHostname
        {
            get { return this.leftTeamTabletHostname; }
            set
            {
                if (this.leftTeamTabletHostname != value)
                {
                    if (value == null) value = string.Empty;
                    this.leftTeamTabletHostname = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private VRemote4.HandlerSi.Client.Business rightTeamTabletClient;
        private PlayerTablet ventuzRightTeamTabletScene;
        public VRemote4.HandlerSi.Scene.States VentuzRightTeamTabletSceneStatus
        {
            get
            {
                if (this.ventuzRightTeamTabletScene is PlayerTablet) return this.ventuzRightTeamTabletScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }
        private string rightTeamTabletHostname = string.Empty;
        public string RightTeamTabletHostname
        {
            get { return this.rightTeamTabletHostname; }
            set
            {
                if (this.rightTeamTabletHostname != value)
                {
                    if (value == null) value = string.Empty;
                    this.rightTeamTabletHostname = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool repressPropertyChanged = false;

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.GeographyDistance'", typeIdentifier);
        }

        public override void New() {
            base.New();
            this.Filename = string.Empty;
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

            //wenn die Datasets einen Synccontext benötigen, dann wird er hierdurch zugeordnet
            this.DataList = this.dataList.ToArray();

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;
            this.insertScene.Timer.Alarm1Fired += this.timer_Alarm1Fired;
            this.insertScene.Timer.Alarm2Fired += this.timer_Alarm2Fired;
            this.insertScene.Timer.StopFired += this.timer_StopFired;
            this.insertScene.Timer.PropertyChanged += this.timer_PropertyChanged;

            this.fullscreenScene = new Fullscreen(syncContext, fullscreenMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.fullscreenScene.StatusChanged += this.fullscreenScene_StatusChanged;

            this.hostScene = new Stage(syncContext, hostMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.hostScene.StatusChanged += this.hostScene_StatusChanged;

            this.leftPlayerScene = new Stage(syncContext, leftPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.leftPlayerScene.StatusChanged += this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.OKButtonPressed += this.leftPlayerScene_OKButtonPressed;

            this.rightPlayerScene = new Stage(syncContext, rightPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.rightPlayerScene.StatusChanged += this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.OKButtonPressed += this.rightPlayerScene_OKButtonPressed;

            this.localVentuzHandler = new VRemote4.HandlerSi.Business(syncContext);
            this.localVentuzHandler.Error += this.localVentuzHandler_Error;

            if (this.localVentuzHandler.TryAddClient("Left Tablet", false, out this.leftTeamTabletClient))
            {
                this.leftTeamTabletClient.HostnameChanged += this.leftTeamTabletClient_HostnameChanged;
                this.leftTeamTabletClient.StatusChanged += this.leftTeamTabletClient_StatusChanged;
                this.ventuzLeftTeamTabletScene = new PlayerTablet(syncContext, this.leftTeamTabletClient, 0);
                this.ventuzLeftTeamTabletScene.StatusChanged += this.ventuzLeftTeamTabletScene_StatusChanged;
                this.ventuzLeftTeamTabletScene.PropertyChanged += this.ventuzLeftTeamTabletScene_PropertyChanged;
                this.ventuzLeftTeamTabletScene.OKButtonPressed += this.ventuzLeftTeamTabletScene_OKButtonPressed;
            }

            if (this.localVentuzHandler.TryAddClient("Right Tablet", false, out this.rightTeamTabletClient))
            {
                this.rightTeamTabletClient.HostnameChanged += this.rightTeamTabletClient_HostnameChanged;
                this.rightTeamTabletClient.StatusChanged += this.rightTeamTabletClient_StatusChanged;
                this.ventuzRightTeamTabletScene = new PlayerTablet(syncContext, this.rightTeamTabletClient, 0);
                this.ventuzRightTeamTabletScene.StatusChanged += this.ventuzRightTeamTabletScene_StatusChanged;
                this.ventuzRightTeamTabletScene.PropertyChanged += this.ventuzRightTeamTabletScene_PropertyChanged;
                this.ventuzRightTeamTabletScene.OKButtonPressed += this.ventuzRightTeamTabletScene_OKButtonPressed;
            }

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this, this.localVentuzHandler);

        }

        public override void Dispose() {
            base.Dispose();

            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Timer.Alarm1Fired -= this.timer_Alarm1Fired;
            this.insertScene.Timer.Alarm2Fired -= this.timer_Alarm2Fired;
            this.insertScene.Timer.StopFired -= this.timer_StopFired;
            this.insertScene.Timer.PropertyChanged -= this.timer_PropertyChanged;
            this.insertScene.Dispose();

            this.fullscreenScene.StatusChanged -= this.fullscreenScene_StatusChanged;
            this.fullscreenScene.Dispose();
            this.hostScene.StatusChanged -= this.hostScene_StatusChanged;
            this.hostScene.Dispose();
            this.leftPlayerScene.StatusChanged -= this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.OKButtonPressed -= this.leftPlayerScene_OKButtonPressed;
            this.leftPlayerScene.Dispose();
            this.rightPlayerScene.StatusChanged -= this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.OKButtonPressed -= this.rightPlayerScene_OKButtonPressed;
            this.rightPlayerScene.Dispose();
            this.localVentuzHandler.Error -= this.localVentuzHandler_Error;
            this.localVentuzHandler.Dispose();
            this.leftTeamTabletClient.HostnameChanged -= this.leftTeamTabletClient_HostnameChanged;
            this.leftTeamTabletClient.StatusChanged -= this.leftTeamTabletClient_StatusChanged;
            this.ventuzLeftTeamTabletScene.StatusChanged -= this.ventuzLeftTeamTabletScene_StatusChanged;
            this.ventuzLeftTeamTabletScene.PropertyChanged -= this.ventuzLeftTeamTabletScene_PropertyChanged;
            this.ventuzLeftTeamTabletScene.OKButtonPressed -= this.ventuzLeftTeamTabletScene_OKButtonPressed;
            this.ventuzLeftTeamTabletScene.Dispose();
            this.rightTeamTabletClient.HostnameChanged -= this.rightTeamTabletClient_HostnameChanged;
            this.rightTeamTabletClient.StatusChanged -= this.rightTeamTabletClient_StatusChanged;
            this.ventuzRightTeamTabletScene.StatusChanged -= this.ventuzRightTeamTabletScene_StatusChanged;
            this.ventuzRightTeamTabletScene.PropertyChanged -= this.ventuzRightTeamTabletScene_PropertyChanged;
            this.ventuzRightTeamTabletScene.OKButtonPressed -= this.ventuzRightTeamTabletScene_OKButtonPressed;
            this.ventuzRightTeamTabletScene.Dispose();

        }

        public override void ResetData() {
            base.ResetData();
            this.TaskCounter = 0;
            this.SelectDataset(0);
            this.resetInput();
        }

        public override void Next() {
            base.Next();
            this.TaskCounter++;
            this.resetInput();
            this.SelectDataset(this.SelectedDatasetIndex + 1);
        }

        public void Resolve() {
            if (this.CloserPlayer != Content.Gameboard.PlayerSelection.NotSelected &&
                (!this.SampleIncluded || (this.SampleIncluded && this.SelectedDatasetIndex > 0))) {
                if (this.CloserPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) this.LeftPlayerScore++;
                else if (this.CloserPlayer == Content.Gameboard.PlayerSelection.RightPlayer) this.RightPlayerScore++;
            }
        }

        public void SetLeftPlayerInput1(string text)
        {
            this.LeftPlayerInput1 = text;
            this.Vhost_SetMap();
        }
        public void SetLeftPlayerInput2(string text)
        {
            this.LeftPlayerInput2 = text;
            this.Vhost_SetMap();
        }

        public void SetRightPlayerInput1(string text) { 
            this.RightPlayerInput1 = text;
            this.Vhost_SetMap();
        }
        public void SetRightPlayerInput2(string text)
        {
            this.RightPlayerInput2 = text;
            this.Vhost_SetMap();
        }

        private void resetInput() {
            this.LeftPlayerInput1 = null;
            this.LeftPlayerInput2 = null;
            this.leftPlayerDistance = null;
            this.leftPlayerOffset = null;
            this.RightPlayerInput1 = null;
            this.RightPlayerInput2 = null;
            this.rightPlayerDistance = null;
            this.rightPlayerOffset = null;
        }

        private void calcPlayerDistances() {
            int? leftPlayerDistance = null;
            int? rightPlayerDistance = null;
            if (this.leftPlayerInput1 is CoordinateSet &&
                this.leftPlayerInput2 is CoordinateSet) leftPlayerDistance = Convert.ToInt32(Math.Truncate(Transformation.GetDistance(this.leftPlayerInput1, this.leftPlayerInput2)));
            if (this.rightPlayerInput1 is CoordinateSet &&
                this.rightPlayerInput2 is CoordinateSet) rightPlayerDistance = Convert.ToInt32(Math.Truncate(Transformation.GetDistance(this.rightPlayerInput1, this.rightPlayerInput2)));
            if (this.leftPlayerDistance != leftPlayerDistance) {
                this.leftPlayerDistance = leftPlayerDistance;
                this.on_PropertyChanged("LeftPlayerDistance");
            }
            if (this.rightPlayerDistance != rightPlayerDistance) {
                this.rightPlayerDistance = rightPlayerDistance;
                this.on_PropertyChanged("RightPlayerDistance");
            }
            if (leftPlayerDistance.HasValue &&
                this.SelectedDataset is DatasetContent) this.leftPlayerOffset = Convert.ToInt32(Math.Abs(leftPlayerDistance.Value - this.SelectedDataset.Distance));
            else this.leftPlayerOffset = null;
            if (rightPlayerDistance.HasValue &&
                this.SelectedDataset is DatasetContent) this.rightPlayerOffset = Convert.ToInt32(Math.Abs(rightPlayerDistance.Value - this.SelectedDataset.Distance));
            else this.rightPlayerOffset = null;

            if (this.leftPlayerOffset.HasValue &&
                this.rightPlayerOffset.HasValue) {
                if (this.leftPlayerOffset.Value < this.rightPlayerOffset.Value) this.CloserPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
                else if (this.rightPlayerOffset.Value < this.leftPlayerOffset.Value) this.CloserPlayer = Content.Gameboard.PlayerSelection.RightPlayer;
                else this.CloserPlayer = Content.Gameboard.PlayerSelection.NotSelected;
            }
            else this.CloserPlayer = Content.Gameboard.PlayerSelection.NotSelected;
        }

        public DatasetContent GetDataset(
            int index) {
            if (index >= 0 &&
                index < this.dataList.Count) return this.dataList[index];
            else return null;
        }

        public int GetDatasetIndex(
            DatasetContent dataset) {
            int index = -1;
            int datasetIndex = 0;
            foreach (DatasetContent item in this.dataList) {
                if (item == dataset) {
                    index = datasetIndex;
                    break;
                }
                datasetIndex++;
            }
            return index;
        }

        public void SelectDataset(
            int index) {
            if (index < 0) index = 0;
            if (index >= this.dataList.Count) index = this.dataList.Count - 1;
            this.SelectedDatasetIndex = index;
            this.on_PropertyChanged("SelectedDatasetIndex");
            this.SelectedDataset = this.GetDataset(index);
            this.on_PropertyChanged("SelectedDataset");
            this.calcPlayerDistances();
        }

        public void AddDataset(
            DatasetContent newDataset,
            int insertIndex) {
            if (this.tryAddDataset(newDataset, insertIndex)) this.on_PropertyChanged("NameList");
            if (this.SelectedDataset == null) this.SelectDataset(0);
        }
        private bool tryAddDataset(
            DatasetContent newDataset,
            int insertIndex) {
            if (newDataset is DatasetContent &&
                !this.dataList.Contains(newDataset)) {
                newDataset.Error += this.dataset_Error;
                newDataset.PropertyChanged += this.dataset_PropertyChanged;
                if (insertIndex >= 0 &&
                    insertIndex < this.DatasetsCount) {
                    this.dataList.Insert(insertIndex, newDataset);
                    this.names.Insert(insertIndex, newDataset.ToString());
                }
                else {
                    this.dataList.Add(newDataset);
                    this.names.Add(newDataset.ToString());
                }
                return true;
            }
            else return false;
        }

        public bool TryMoveDatasetUp(
            int index) {
            if (index > 0 &&
                index < this.DatasetsCount) {
                DatasetContent dataset = this.GetDataset(index);
                this.dataList.RemoveAt(index);
                this.dataList.Insert(index - 1, dataset);
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                return true;
            }
            else return false;
        }
        public bool TryMoveDatasetDown(
            int index) {
            if (index >= 0 &&
                index < this.DatasetsCount - 1) {
                DatasetContent dataset = this.GetDataset(index);
                this.dataList.RemoveAt(index);
                this.dataList.Insert(index + 1, dataset);
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                return true;
            }
            else return false;
        }

        public void ResortAllDatasets() {
            if (this.DatasetsCount > 1) {
                List<DatasetContent> dataList = new List<DatasetContent>();
                if (this.SampleIncluded) {
                    dataList.Add(this.dataList[0]);
                    for (int i = this.dataList.Count - 1; i > 0; i--) dataList.Add(this.dataList[i]);
                }
                else {
                    for (int i = this.dataList.Count - 1; i >= 0; i--) dataList.Add(this.dataList[i]);
                }
                this.dataList.Clear();
                foreach (DatasetContent item in dataList) this.dataList.Add(item);
                this.buildNameList();
                this.on_PropertyChanged("NameList");
            }
        }

        public void RemoveAllDatasets() {
            if (this.tryRemoveAllDatasets()) {
                this.SelectDataset(0);
                this.buildNameList();
                this.on_PropertyChanged("NameList");
            }
        }
        private bool tryRemoveAllDatasets() {
            bool datasetRemoved = false;
            DatasetContent[] datasetList = this.dataList.ToArray();
            foreach (DatasetContent item in datasetList) datasetRemoved = this.removeDataset(item) | datasetRemoved;
            return datasetRemoved;
        }
        public bool TryRemoveDataset(
            int index) {
            if (this.removeDataset(this.GetDataset(index))) {
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                if (index <= this.SelectedDatasetIndex) this.SelectDataset(this.SelectedDatasetIndex);
                return true;
            }
            else return false;
        }
        private bool removeDataset(
            DatasetContent dataset) {
            if (dataset is DatasetContent &&
                this.dataList.Contains(dataset)) {
                dataset.Error -= this.dataset_Error;
                dataset.PropertyChanged -= this.dataset_PropertyChanged;
                this.dataList.Remove(dataset);
                return true;
            }
            else return false;
        }

        private void buildNameList() {
            this.names.Clear();
            this.repressPropertyChanged = true;
            foreach (DatasetContent item in this.dataList) {
                this.names.Add(item.ToString());
            }
            this.repressPropertyChanged = false;
        }

        public void Load(
            string filename) {
            string subSender = "Load";
            if (File.Exists(filename)) {
                try {
                    XmlSerializer serializer = new XmlSerializer(typeof(Data));
                    Data data;
                    using (StreamReader reader = new StreamReader(filename)) data = (Data)serializer.Deserialize(reader);
                    this.DataList = data.DataList;
                    this.SelectDataset(data.SelectedIndex);
                    this.filename = filename;
                    this.on_PropertyChanged("Filename");
                }
                catch (Exception exc) {
                    // Fehler weitergeben
                    this.on_Error(subSender, exc.Message);
                }
            }
            else {
                // Fehler weitergeben
                this.on_Error(subSender, "Datei nicht vorhanden - '" + filename + "'");
            }
        }
        public void Save() { if (File.Exists(this.Filename)) this.SaveAs(this.Filename); }
        public void SaveAs(
            string filename) {
            string subSender = "SaveAs";
            try {
                // Dokument speichern
                Data data = new Data();
                data.DataList = this.DataList;
                data.SelectedIndex = this.SelectedDatasetIndex;
                XmlSerializer serializer = new XmlSerializer(typeof(Data));
                using (StreamWriter writer = new StreamWriter(filename)) serializer.Serialize(writer, data);
                this.filename = filename;
                this.on_PropertyChanged("Filename");
            }
            catch (Exception exc) {
                // Fehler weitergeben
                this.on_Error(subSender, exc.Message);
            }
        }

        public override void Vinsert_LoadScene() {
            this.insertScene.Load();
        }

        public void Vinsert_TextInsertIn() {
            this.Vinsert_SetTextInsert();
            if (this.insertScene is VRemote4.HandlerSi.Scene &&
                this.insertScene.TextInsert is VentuzScenes.GamePool._Modules.TextInsert) this.insertScene.TextInsert.ToIn();
        }
        public void Vinsert_SetTextInsert() { if (this.insertScene is VRemote4.HandlerSi.Scene && this.SelectedDataset is DatasetContent) this.Vinsert_SetTextInsert(this.insertScene.TextInsert, this.SelectedDataset.Name); }
        public void Vinsert_SetTextInsert(
            VentuzScenes.GamePool._Modules.TextInsert scene,
            string text) {
            if (scene is VentuzScenes.GamePool._Modules.TextInsert) {
                scene.SetPositionX(this.TextInsertPositionX);
                scene.SetPositionY(this.TextInsertPositionY);
                scene.SetStyle(this.TextInsertStyle);
                scene.SetText(text);
            }
        }
        public void Vinsert_TextInsertOut() {
            if (this.insertScene is VRemote4.HandlerSi.Scene &&
                this.insertScene.TextInsert is VentuzScenes.GamePool._Modules.TextInsert) this.insertScene.TextInsert.ToOut();
        }

        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }

        public override void Vinsert_TimerIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_TimerIn(this.insertScene.Timer); }
        public override void Vinsert_SetTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetTimer(this.insertScene.Timer); }
        public override void Vinsert_StartTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timer.StartTimer(); }
        public override void Vinsert_StopTimer()
        {
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timer.StopTimer();
            this.Vfullscreen_StopTimer();
            this.Vfullscreen_SetTimer(this.TimerCurrentTime);
        }
        public override void Vinsert_ContinueTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timer.ContinueTimer(); }
        public override void Vinsert_ResetTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timer.ResetTimer(); }
        public override void Vinsert_TimerOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timer.ToOut(); }

        public override void Vinsert_UnloadScene() {
            this.insertScene.Unload();
        }

        public override void Vfullscreen_LoadScene() { this.fullscreenScene.Load(); }
        public override void Vfullscreen_ShowGame() {
            this.Vfullscreen_Reset();
            base.Vfullscreen_ShowGame();
        }
        public void Vfullscreen_TextIn() {
            this.Vfullscreen_SetMapLayout();
            this.Vfullscreen_SetText();
            //if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.ShowTask();
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.ShowResults();
        }
        public void Vfullscreen_SetMapLayout() {
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) this.fullscreenScene.SetMapLayout(this.SelectedDataset.MapLayout);
        }
        public void Vfullscreen_SetText() {
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                this.fullscreenScene.SetTaskText(this.SelectedDataset.Name);
                this.fullscreenScene.SetResultsSolution(this.SelectedDataset.Name);
                this.fullscreenScene.SetResultsBlueName(this.RightPlayerName);
                this.fullscreenScene.SetResultsRedName(this.LeftPlayerName);
            }
        }
        public void Vfullscreen_BluePinsIn() {
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.rightPlayerInput1 is CoordinateSet &&
                this.rightPlayerInput2 is CoordinateSet &&
                this.rightPlayerDistance.HasValue) {
                this.fullscreenScene.SetResultsBlueName(this.RightPlayerName);
                this.fullscreenScene.SetResultsBlueDistance(this.rightPlayerDistance.Value);
                this.fullscreenScene.ShowBlueDistance();
                this.fullscreenScene.SetBluePin1Position(this.rightPlayerInput1.HD_PositionX, this.rightPlayerInput1.HD_PositionY);
                this.fullscreenScene.SetBluePin2Position(this.rightPlayerInput2.HD_PositionX, this.rightPlayerInput2.HD_PositionY);
                this.fullscreenScene.ShowBluePins();
            }
        }
        public void Vfullscreen_RedPinsIn() {
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.leftPlayerInput1 is CoordinateSet &&
                this.leftPlayerInput2 is CoordinateSet &&
                this.leftPlayerDistance.HasValue) {
                this.fullscreenScene.SetResultsRedName(this.LeftPlayerName);
                this.fullscreenScene.SetResultsRedDistance(this.leftPlayerDistance.Value);
                this.fullscreenScene.SetRedPin1Position(this.leftPlayerInput1.HD_PositionX, this.leftPlayerInput1.HD_PositionY);
                this.fullscreenScene.SetRedPin2Position(this.leftPlayerInput2.HD_PositionX, this.leftPlayerInput2.HD_PositionY);
                this.fullscreenScene.ShowRedDistance();
                this.fullscreenScene.ShowRedPins();
            }
        }
        public void Vfullscreen_SolutionIn() {
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                this.fullscreenScene.SetResultsSolution(this.SelectedDataset.Name);
                this.fullscreenScene.SetResultsSolutionDistance(this.SelectedDataset.Distance);
                this.fullscreenScene.SetYellowPin1Position(this.SelectedDataset.FirstLocationCoordinates.HD_PositionX, this.SelectedDataset.FirstLocationCoordinates.HD_PositionY);
                this.fullscreenScene.SetYellowPin2Position(this.SelectedDataset.SecondLocationCoordinates.HD_PositionX, this.SelectedDataset.SecondLocationCoordinates.HD_PositionY);
                this.fullscreenScene.ShowYellowPins();
                //this.fullscreenScene.ShowBlueDistance();
                //this.fullscreenScene.ShowRedDistance();
                this.fullscreenScene.ShowSolutionDistance();
            }
        }
        internal void Vfullscreen_OffsetIn()
        {
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent)
            {
                if (this.leftPlayerOffset.HasValue &&
                    this.rightPlayerOffset.HasValue)
                {
                    this.fullscreenScene.SetResultsRedOffset(this.leftPlayerOffset.Value);
                    this.fullscreenScene.SetResultsBlueOffset(this.rightPlayerOffset.Value);
                    this.fullscreenScene.ShowOffsets();
                    this.insertMasterScene.Sampler.Play(VentuzScenes.Insert.Sampler.SampleElements.Select);
                }
                else this.fullscreenScene.ResetOffsets();

            }
        }
        public void Vfullscreen_Reset() {
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.Reset();
        }
        public override void Vfullscreen_UnloadScene() { this.fullscreenScene.Unload(); }

        public override void Vhost_LoadScene() { this.hostScene.Load(); }
        public void Vhost_SetMap() {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                this.hostScene.SetMapLayout(this.SelectedDataset.MapLayout);
                this.hostScene.SetFullText(string.Empty);
                this.hostScene.SetSolutionName(this.SelectedDataset.Name);
                this.hostScene.SetSolutionDistance(this.SelectedDataset.Distance);
                this.hostScene.ShowSolutionDistance();
                this.hostScene.SetBlueName(this.RightPlayerName);
                this.hostScene.SetRedName(this.LeftPlayerName);
                this.hostScene.SetSolutionPin1Position(this.SelectedDataset.FirstLocationCoordinates.SXGA_PositionX, this.SelectedDataset.FirstLocationCoordinates.SXGA_PositionY);
                this.hostScene.SetSolutionPin2Position(this.SelectedDataset.SecondLocationCoordinates.SXGA_PositionX, this.SelectedDataset.SecondLocationCoordinates.SXGA_PositionY);

                if (this.rightPlayerInput1 is CoordinateSet)
                {
                    this.hostScene.SetBluePin1Position(this.rightPlayerInput1.SXGA_PositionX, this.rightPlayerInput1.SXGA_PositionY);
                    this.hostScene.ShowBluePin1();
                }
                else this.hostScene.HideBluePin1();
                if (this.rightPlayerInput2 is CoordinateSet)
                {
                    this.hostScene.SetBluePin2Position(this.rightPlayerInput2.SXGA_PositionX, this.rightPlayerInput2.SXGA_PositionY);
                    this.hostScene.ShowBluePin2();
                }
                else this.hostScene.HideBluePin2();
                if (this.rightPlayerDistance.HasValue) {
                    this.hostScene.SetBlueDistance(this.rightPlayerDistance.Value);
                    this.hostScene.ShowBlueDistance();
                }
                else {
                    this.hostScene.ResetBlueDistance();
                }

                if (this.leftPlayerInput1 is CoordinateSet)
                {
                    this.hostScene.SetRedPin1Position(this.leftPlayerInput1.SXGA_PositionX, this.leftPlayerInput1.SXGA_PositionY);
                    this.hostScene.ShowRedPin1();
                }
                else this.hostScene.HideRedPin1();
                if (this.leftPlayerInput2 is CoordinateSet)
                {
                    this.hostScene.SetRedPin2Position(this.leftPlayerInput2.SXGA_PositionX, this.leftPlayerInput2.SXGA_PositionY);
                    this.hostScene.ShowRedPin2();
                }
                else this.hostScene.HideRedPin2();
                if (this.leftPlayerDistance.HasValue) {
                    this.hostScene.SetRedDistance(this.leftPlayerDistance.Value);
                    this.hostScene.ShowRedDistance();
                }
                else {
                    this.hostScene.ResetRedDistance();
                }

                if (this.leftPlayerOffset.HasValue &&
                    this.rightPlayerOffset.HasValue)
                {
                    this.hostScene.SetBlueOffset(this.rightPlayerOffset.Value);
                    this.hostScene.SetRedOffset(this.leftPlayerOffset.Value);
                    this.hostScene.SetOInffsetsIn();
                }
                else this.hostScene.ResetOffsets();
            }
        }
        public override void Vhost_UnloadScene() { this.hostScene.Unload(); }

        public override void Vleftplayer_LoadScene() { this.leftPlayerScene.Load(); }
        public override void Vleftplayer_UnloadScene() { this.leftPlayerScene.Unload(); }

        public override void Vrightplayer_LoadScene() { this.rightPlayerScene.Load(); }
        public override void Vrightplayer_UnloadScene() { this.rightPlayerScene.Unload(); }

        public override void Vstage_Init() {
            base.Vstage_Init();
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.hostScene.ResetMap();
                this.hostScene.SetFullText(string.Empty);
                this.hostScene.SelectPlayer(Stage.PlayerSelection.NotSelected);
                this.hostScene.ToIn();
            }
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.leftPlayerScene.ResetMap();
                this.leftPlayerScene.SetSampleMap(false);
                this.leftPlayerScene.SelectPlayer(Stage.PlayerSelection.LeftPlayer);
                this.leftPlayerScene.SetFullText(string.Empty);
                this.leftPlayerScene.ToIn();
            }
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.rightPlayerScene.ResetMap();
                this.rightPlayerScene.SetSampleMap(false);
                this.rightPlayerScene.SelectPlayer(Stage.PlayerSelection.RightPlayer);
                this.rightPlayerScene.SetFullText(string.Empty);
                this.rightPlayerScene.ToIn();
            }
            if (this.VentuzLeftTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available)
            {
                this.ventuzLeftTeamTabletScene.ResetMap();
                this.ventuzLeftTeamTabletScene.SetSampleMap(false);
                this.ventuzLeftTeamTabletScene.SelectPlayer(Stage.PlayerSelection.LeftPlayer);
                this.ventuzLeftTeamTabletScene.SetFullText(string.Empty);
                this.ventuzLeftTeamTabletScene.ToIn();
            }
            if (this.VentuzRightTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available)
            {
                this.ventuzRightTeamTabletScene.ResetMap();
                this.ventuzRightTeamTabletScene.SetSampleMap(false);
                this.ventuzRightTeamTabletScene.SelectPlayer(Stage.PlayerSelection.RightPlayer);
                this.ventuzRightTeamTabletScene.SetFullText(string.Empty);
                this.ventuzRightTeamTabletScene.ToIn();
            }
        }
        public void Vplayer_UnlockTouch() {
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.UnlockTouch();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.UnlockTouch();
            if (this.VentuzLeftTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzLeftTeamTabletScene.UnlockTouch();
            if (this.VentuzRightTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzRightTeamTabletScene.UnlockTouch();
        }
        public void Vplayer_LockTouch() {
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.LockTouch();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.LockTouch();
            if (this.VentuzLeftTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzLeftTeamTabletScene.LockTouch();
            if (this.VentuzRightTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzRightTeamTabletScene.LockTouch();
        }
        public void Vstage_MapIn(
            bool showSample) {
            this.Vhost_SetMap();
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                if (showSample) this.hostScene.ToOut();
                else
                {
                    this.hostScene.ToIn();
                    Helper.invokeActionAfterDelay(this.hostScene.ShowSolution, 300, this.syncContext);
                    Helper.invokeActionAfterDelay(this.hostScene.ShowSolutionDistance, 500, this.syncContext);
                }
            }
            if (showSample) {
                if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available)
                {
                    this.leftPlayerScene.ResetMap();
                    this.leftPlayerScene.SetSampleMap(true);
                    this.leftPlayerScene.ToIn();
                }
                if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available)
                {
                    this.rightPlayerScene.ResetMap();
                    this.rightPlayerScene.SetSampleMap(true);
                    this.rightPlayerScene.ToIn();
                }
                if (this.VentuzLeftTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available)
                {
                    this.ventuzLeftTeamTabletScene.ResetMap();
                    this.ventuzLeftTeamTabletScene.SetSampleMap(true);
                    this.ventuzLeftTeamTabletScene.ToIn();
                }
                if (this.VentuzRightTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available)
                {
                    this.ventuzRightTeamTabletScene.ResetMap();
                    this.ventuzRightTeamTabletScene.SetSampleMap(true);
                    this.ventuzRightTeamTabletScene.ToIn();
                }
                this.Vplayer_UnlockTouch();
            }
            else if (this.SelectedDataset is DatasetContent) {
                if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available)
                {
                    this.leftPlayerScene.SetSampleMap(false);
                    this.leftPlayerScene.SetMapLayout(this.SelectedDataset.MapLayout);
                    if (this.SelectedDataset is DatasetContent) this.leftPlayerScene.SetFullText(this.SelectedDataset.FirstLocationName);
                    else this.leftPlayerScene.SetFullText(string.Empty);
                    this.leftPlayerScene.ToIn();
                }
                if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available)
                {
                    this.rightPlayerScene.SetMapLayout(this.SelectedDataset.MapLayout);
                    this.rightPlayerScene.SetSampleMap(false);
                    if (this.SelectedDataset is DatasetContent) this.rightPlayerScene.SetFullText(this.SelectedDataset.FirstLocationName);
                    else this.rightPlayerScene.SetFullText(string.Empty);
                    this.rightPlayerScene.ToIn();
                }
                if (this.VentuzLeftTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available)
                {
                    this.ventuzLeftTeamTabletScene.SetSampleMap(false);
                    this.ventuzLeftTeamTabletScene.SetMapLayout(this.SelectedDataset.MapLayout);
                    if (this.SelectedDataset is DatasetContent) this.ventuzLeftTeamTabletScene.SetFullText(this.SelectedDataset.SecondLocationName);
                    else this.ventuzLeftTeamTabletScene.SetFullText(string.Empty);
                    this.ventuzLeftTeamTabletScene.ToIn();
                }
                if (this.VentuzRightTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available)
                {
                    this.ventuzRightTeamTabletScene.SetMapLayout(this.SelectedDataset.MapLayout);
                    this.ventuzRightTeamTabletScene.SetSampleMap(false);
                    if (this.SelectedDataset is DatasetContent) this.ventuzRightTeamTabletScene.SetFullText(this.SelectedDataset.SecondLocationName);
                    else this.ventuzRightTeamTabletScene.SetFullText(string.Empty);
                    this.ventuzRightTeamTabletScene.ToIn();
                }
            }
        }
        public void Vstage_MapOut() {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.hostScene.ToOut();
                this.hostScene.ResetMap();
            }
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available)
            {
                this.leftPlayerScene.ToOut();
                this.leftPlayerScene.ResetMap();
                this.leftPlayerScene.SetFullText(string.Empty);
            }
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available)
            {
                this.rightPlayerScene.ToOut();
                this.rightPlayerScene.ResetMap();
                this.rightPlayerScene.SetFullText(string.Empty);
            }
            if (this.VentuzLeftTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available)
            {
                this.ventuzLeftTeamTabletScene.ToOut();
                this.ventuzLeftTeamTabletScene.ResetMap();
                this.ventuzLeftTeamTabletScene.SetFullText(string.Empty);
            }
            if (this.VentuzRightTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available)
            {
                this.ventuzRightTeamTabletScene.ToOut();
                this.ventuzRightTeamTabletScene.ResetMap();
                this.ventuzRightTeamTabletScene.SetFullText(string.Empty);
            }
        }

        public void Vlefttablet_Start() { this.leftTeamTabletClient.Start(this.LeftTeamTabletHostname); }
        public void Vlefttablet_ShutDown() { this.leftTeamTabletClient.Shutdown(); }

        public void Vrighttablet_Start() { this.rightTeamTabletClient.Start(this.RightTeamTabletHostname); }
        public void Vrighttablet_ShutDown() { this.rightTeamTabletClient.Shutdown(); }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
            this.fullscreenScene.Clear();
            this.hostScene.Clear();
            this.leftPlayerScene.Clear();
            this.rightPlayerScene.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming

        void dataset_Error(object sender, Messaging.ErrorEventArgs e) { this.on_Error(sender, e); }

        void dataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_dataset_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_dataset_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "Name") {
                    this.buildNameList();
                    this.on_PropertyChanged("NameList");
                }
            }
            this.Save();
        }

        void leftPlayerScene_OKButtonPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerScene_OKButtonPressed(object content) {
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                CoordinateSet touchCoordinate = new CoordinateSet(this.mapLayout, this.leftPlayerScene.TouchPositionX, this.leftPlayerScene.TouchPositionY);
                this.SetLeftPlayerInput1(touchCoordinate.Text);
                this.Vhost_SetMap();
            }
        }

        void rightPlayerScene_OKButtonPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_OKButtonPressed(object content) {
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                CoordinateSet touchCoordinate = new CoordinateSet(this.mapLayout, this.rightPlayerScene.TouchPositionX, this.rightPlayerScene.TouchPositionY);
                this.SetRightPlayerInput1(touchCoordinate.Text);
                this.Vhost_SetMap();
            }
        }

        void localVentuzHandler_Error(object sender, ErrorEventArgs e)
        {
            Exception exc = e.GetException();
            this.on_Error(sender, new Messaging.ErrorEventArgs(this, sender.ToString(), DateTime.Now, exc.ToString()));
        }

        void leftTeamTabletClient_HostnameChanged(object sender, VRemote4.HandlerSi.Client.HostnameArgs e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftTeamTabletClient_HostnameChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftTeamTabletClient_HostnameChanged(object content)
        {
            VRemote4.HandlerSi.Client.HostnameArgs e = content as VRemote4.HandlerSi.Client.HostnameArgs;
            if (e is VRemote4.HandlerSi.Client.HostnameArgs) this.LeftTeamTabletHostname = e.Name;
        }

        void leftTeamTabletClient_StatusChanged(object sender, VRemote4.HandlerSi.Client.ClientStateArgs e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftTeamTabletClient_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftTeamTabletClient_StatusChanged(object content)
        {
            VRemote4.HandlerSi.Client.ClientStateArgs e = content as VRemote4.HandlerSi.Client.ClientStateArgs;
            if (e is VRemote4.HandlerSi.Client.ClientStateArgs &&
                e.Status.HasValue &&
                e.Status.Value == VRemote4.HandlerSi.Client.ClientStates.Active &&
                this.ventuzLeftTeamTabletScene is VRemote4.HandlerSi.Scene) this.ventuzLeftTeamTabletScene.Load();
        }

        void ventuzLeftTeamTabletScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzLeftTeamTabletScene_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzLeftTeamTabletScene_StatusChanged(object content)
        {
            VRemote4.HandlerSi.Scene.StatusArgs e = content as VRemote4.HandlerSi.Scene.StatusArgs;
            if (e is VRemote4.HandlerSi.Scene.StatusArgs)
            {
                //if (e.Status == VRemote4.HandlerSi.Scene.States.Unloaded) this.leftTeamTabletClient.Shutdown();
                //else if (e.Status == VRemote4.HandlerSi.Scene.States.Available) this.Vlefttablet_Init();
            }
            this.on_PropertyChanged("VentuzLeftTeamTabletSceneStatus");
        }

        void ventuzLeftTeamTabletScene_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzLeftTeamTabletScene_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzLeftTeamTabletScene_PropertyChanged(object content)
        {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs)
            {
                if (e.PropertyName == "Selection")
                {
                    //this.LeftTeamInput = this.ventuzTerminalLeftScene.Controller.Selection;
                }
            }
        }

        void ventuzLeftTeamTabletScene_OKButtonPressed(object sender, EventArgs e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzLeftTeamTabletScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzLeftTeamTabletScene_OKButtonPressed(object content)
        {
            CoordinateSet touchCoordinate = new CoordinateSet(this.mapLayout, this.ventuzLeftTeamTabletScene.TouchPositionX, this.ventuzLeftTeamTabletScene.TouchPositionY);
            this.SetLeftPlayerInput2(touchCoordinate.Text);
            this.Vhost_SetMap();
        }

        void rightTeamTabletClient_HostnameChanged(object sender, VRemote4.HandlerSi.Client.HostnameArgs e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightTeamTabletClient_HostnameChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightTeamTabletClient_HostnameChanged(object content)
        {
            VRemote4.HandlerSi.Client.HostnameArgs e = content as VRemote4.HandlerSi.Client.HostnameArgs;
            if (e is VRemote4.HandlerSi.Client.HostnameArgs) this.RightTeamTabletHostname = e.Name;
        }

        void rightTeamTabletClient_StatusChanged(object sender, VRemote4.HandlerSi.Client.ClientStateArgs e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightTeamTabletClient_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightTeamTabletClient_StatusChanged(object content)
        {
            VRemote4.HandlerSi.Client.ClientStateArgs e = content as VRemote4.HandlerSi.Client.ClientStateArgs;
            if (e is VRemote4.HandlerSi.Client.ClientStateArgs &&
                e.Status.HasValue &&
                e.Status.Value == VRemote4.HandlerSi.Client.ClientStates.Active &&
                this.ventuzRightTeamTabletScene is VRemote4.HandlerSi.Scene) this.ventuzRightTeamTabletScene.Load();
        }

        void ventuzRightTeamTabletScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzRightTeamTabletScene_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzRightTeamTabletScene_StatusChanged(object content)
        {
            VRemote4.HandlerSi.Scene.StatusArgs e = content as VRemote4.HandlerSi.Scene.StatusArgs;
            if (e is VRemote4.HandlerSi.Scene.StatusArgs)
            {
                //if (e.Status == VRemote4.HandlerSi.Scene.States.Unloaded) this.rightTeamTabletClient.Shutdown();
                //else if (e.Status == VRemote4.HandlerSi.Scene.States.Available) this.Vrighttablet_Init();
            }
            this.on_PropertyChanged("VentuzRightTeamTabletSceneStatus");
        }

        void ventuzRightTeamTabletScene_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzRightTeamTabletScene_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzRightTeamTabletScene_PropertyChanged(object content)
        {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs)
            {
                if (e.PropertyName == "Selection")
                {
                    //this.RightTeamInput = this.ventuzTerminalRightScene.Controller.Selection;
                }
            }
        }

        void ventuzRightTeamTabletScene_OKButtonPressed(object sender, EventArgs e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzRightTeamTabletScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzRightTeamTabletScene_OKButtonPressed(object content)
        {
            CoordinateSet touchCoordinate = new CoordinateSet(this.mapLayout, this.ventuzRightTeamTabletScene.TouchPositionX, this.ventuzRightTeamTabletScene.TouchPositionY);
            this.SetRightPlayerInput2(touchCoordinate.Text);
            this.Vhost_SetMap();
        }

        protected override void sync_timer_PropertyChanged(object content)
        {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs)
            {
                if (e.PropertyName == "CurrentTime") this.TimerCurrentTime = this.insertScene.Timer.CurrentTime;
                else if (e.PropertyName == "IsRunning") this.TimerIsRunning = this.insertScene.Timer.IsRunning;
            }
        }

        #endregion

    }
}
