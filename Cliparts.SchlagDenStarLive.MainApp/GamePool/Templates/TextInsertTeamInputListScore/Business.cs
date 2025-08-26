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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TextInsertTeamInputListScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TextInsertTeamInputListScore {

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetContent : Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private string text = string.Empty;
        public string Text {
            get { return this.text; }
            set {
                if (this.text != value) {
                    if (value == null) value = string.Empty;
                    else this.text = value;
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        private string hostText = string.Empty;
        public string HostText
        {
            get { return this.hostText; }
            set
            {
                if (this.hostText != value)
                {
                    if (value == null) value = string.Empty;
                    this.hostText = Regex.Replace(value, "(?<!\r)\n", "\r\n");
                    this.on_PropertyChanged();
                }
            }
        }

        private string infoText = string.Empty;
        public string InfoText
        {
            get { return this.infoText; }
            set
            {
                if (this.infoText != value)
                {
                    if (value == null) value = string.Empty;
                    this.infoText = Regex.Replace(value, "(?<!\r)\n", "\r\n");
                    this.on_PropertyChanged();
                }
            }
        }

        private string leftTeamInputTop = string.Empty;
        public string LeftTeamInputTop
        {
            get { return this.leftTeamInputTop; }
            set
            {
                if (this.leftTeamInputTop != value)
                {
                    if (string.IsNullOrEmpty(value)) this.leftTeamInputTop = string.Empty;
                    else this.leftTeamInputTop = (value);
                    this.on_PropertyChanged();
                }
            }
        }

        private string leftTeamInputBottom = string.Empty;
        public string LeftTeamInputBottom
        {
            get { return this.leftTeamInputBottom; }
            set
            {
                if (this.leftTeamInputBottom != value)
                {
                    if (string.IsNullOrEmpty(value)) this.leftTeamInputBottom = string.Empty;
                    else this.leftTeamInputBottom = (value);
                    this.on_PropertyChanged();
                }
            }
        }

        private string rightTeamInputTop = string.Empty;
        public string RightTeamInputTop
        {
            get { return this.rightTeamInputTop; }
            set
            {
                if (this.rightTeamInputTop != value)
                {
                    if (string.IsNullOrEmpty(value)) this.rightTeamInputTop = string.Empty;
                    else this.rightTeamInputTop = (value);
                    this.on_PropertyChanged();
                }
            }
        }

        private string rightTeamInputBottom = string.Empty;
        public string RightTeamInputBottom
        {
            get { return this.rightTeamInputBottom; }
            set
            {
                if (this.rightTeamInputBottom != value)
                {
                    if (string.IsNullOrEmpty(value)) this.rightTeamInputBottom = string.Empty;
                    else this.rightTeamInputBottom = (value);
                    this.on_PropertyChanged();
                }
            }
        }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContent() { }
        public DatasetContent(
            string text) {
            if (string.IsNullOrEmpty(text)) this.Text = "?";
            else this.Text = text;
            this.HostText = text;
        }

        public void ResetInput()
        {
            this.LeftTeamInputTop = string.Empty;
            this.LeftTeamInputBottom = string.Empty;
            this.RightTeamInputTop = string.Empty;
            this.RightTeamInputBottom = string.Empty;
        }

        private void buildToString() { this.toString = this.Text; }

        public override string ToString() { return this.toString; }

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

        private int textInsertPositionX = 0;
        public int TextInsertPositionX {
            get { return this.textInsertPositionX; }
            set {
                if (this.textInsertPositionX != value) {
                    this.textInsertPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetContent(false);
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
                    this.Vinsert_SetContent(false);
                }
            }
        }

        private int taskCounterPositionX = 0;
        public int TaskCounterPositionX {
            get { return this.taskCounterPositionX; }
            set {
                if (this.taskCounterPositionX != value) {
                    this.taskCounterPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTaskCounter();
                }
            }
        }

        private int taskCounterPositionY = 0;
        public int TaskCounterPositionY {
            get { return this.taskCounterPositionY; }
            set {
                if (this.taskCounterPositionY != value) {
                    this.taskCounterPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTaskCounter();
                }
            }
        }

        public const int TaskCounterPenaltyCountMax = 13;

        private int taskCounterSize = 10;
        public int TaskCounterSize {
            get { return this.taskCounterSize; }
            set {
                if (this.taskCounterSize != value) {
                    if (value < 0) value = 0;
                    this.taskCounterSize = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTaskCounter();
                }
            }
        }

        public string LeftTeamInputTop {
            get { 
                if (this.SelectedDataset is DatasetContent) return this.SelectedDataset.LeftTeamInputTop;
                else return string.Empty; 
            }
            set {
                if (this.SelectedDataset is DatasetContent)
                {
                    if (this.SelectedDataset.LeftTeamInputTop != value)
                    {
                        if (string.IsNullOrEmpty(value)) this.SelectedDataset.LeftTeamInputTop = string.Empty;
                        else this.SelectedDataset.LeftTeamInputTop = value;
                        this.on_PropertyChanged();
                    }
                }
                else this.on_PropertyChanged();
            }
        }

        public string LeftTeamInputBottom
        {
            get
            {
                if (this.SelectedDataset is DatasetContent) return this.SelectedDataset.LeftTeamInputBottom;
                else return string.Empty;
            }
            set
            {
                if (this.SelectedDataset is DatasetContent)
                {
                    if (this.SelectedDataset.LeftTeamInputBottom != value)
                    {
                        if (string.IsNullOrEmpty(value)) this.SelectedDataset.LeftTeamInputBottom = string.Empty;
                        else this.SelectedDataset.LeftTeamInputBottom = value;
                        this.on_PropertyChanged();
                    }
                }
                else this.on_PropertyChanged();
            }
        }

        private bool leftTeamIsTrue = false;
        public bool LeftTeamIsTrue {
            get { return this.leftTeamIsTrue; }
            set {
                if (this.leftTeamIsTrue != value) {
                    this.leftTeamIsTrue = (value);
                    this.on_PropertyChanged();
                }
            }
        }

        public string RightTeamInputTop
        {
            get
            {
                if (this.SelectedDataset is DatasetContent) return this.SelectedDataset.RightTeamInputTop;
                else return string.Empty;
            }
            set
            {
                if (this.SelectedDataset is DatasetContent)
                {
                    if (this.SelectedDataset.RightTeamInputTop != value)
                    {
                        if (string.IsNullOrEmpty(value)) this.SelectedDataset.RightTeamInputTop = string.Empty;
                        else this.SelectedDataset.RightTeamInputTop = value;
                        this.on_PropertyChanged();
                    }
                }
                else this.on_PropertyChanged();
            }
        }

        public string RightTeamInputBottom
        {
            get
            {
                if (this.SelectedDataset is DatasetContent) return this.SelectedDataset.RightTeamInputBottom;
                else return string.Empty;
            }
            set
            {
                if (this.SelectedDataset is DatasetContent)
                {
                    if (this.SelectedDataset.RightTeamInputBottom != value)
                    {
                        if (string.IsNullOrEmpty(value)) this.SelectedDataset.RightTeamInputBottom = string.Empty;
                        else this.SelectedDataset.RightTeamInputBottom = value;
                        this.on_PropertyChanged();
                    }
                }
                else this.on_PropertyChanged();
            }
        }

        private bool rightTeamIsTrue = false;
        public bool RightTeamIsTrue {
            get { return this.rightTeamIsTrue; }
            set {
                if (this.rightTeamIsTrue != value) {
                    this.rightTeamIsTrue = (value);
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
        [Serialization.NotSerialized]
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

        private Host hostScene;
        public override VRemote4.HandlerSi.Scene.States HostSceneStatus {
            get {
                if (this.hostScene is Host) return this.hostScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private Player leftPlayerScene;
        public override VRemote4.HandlerSi.Scene.States LeftPlayerSceneStatus {
            get {
                if (this.leftPlayerScene is Player) return this.leftPlayerScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private Player rightPlayerScene;
        public override VRemote4.HandlerSi.Scene.States RightPlayerSceneStatus {
            get {
                if (this.rightPlayerScene is Player) return this.rightPlayerScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private VRemote4.HandlerSi.Business localVentuzHandler;

        private VRemote4.HandlerSi.Client.Business leftTeamTabletClient;
        private PlayerTablet ventuzLeftTeamTabletScene;
        public VRemote4.HandlerSi.Scene.States VentuzLeftTeamTabletSceneStatus
        {
            get {
                if (this.ventuzLeftTeamTabletScene is PlayerTablet ) return this.ventuzLeftTeamTabletScene.Status;
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
            get {
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
                if (this.rightTeamTabletHostname != value) {
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.TextInsertTeamInputListScore'", typeIdentifier);
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

            this.hostScene = new Host(syncContext, hostMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.hostScene.StatusChanged += this.hostScene_StatusChanged;

            this.leftPlayerScene = new Player(syncContext, leftPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.leftPlayerScene.StatusChanged += this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.OKButtonPressed += this.leftPlayerScene_OKButtonPressed;

            this.rightPlayerScene = new Player(syncContext, rightPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.rightPlayerScene.StatusChanged += this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.OKButtonPressed += this.rightPlayerScene_OKButtonPressed;

            this.localVentuzHandler = new VRemote4.HandlerSi.Business(syncContext);
            this.localVentuzHandler.Error += this.localVentuzHandler_Error;

            if (this.localVentuzHandler.TryAddClient("Left Tablet", false, out this.leftTeamTabletClient)) {
                this.leftTeamTabletClient.HostnameChanged += this.leftTeamTabletClient_HostnameChanged;
                this.leftTeamTabletClient.StatusChanged += this.leftTeamTabletClient_StatusChanged;
                this.ventuzLeftTeamTabletScene = new PlayerTablet(syncContext, this.leftTeamTabletClient, 0);
                this.ventuzLeftTeamTabletScene.StatusChanged += this.ventuzLeftTeamTabletScene_StatusChanged;
                this.ventuzLeftTeamTabletScene.PropertyChanged += this.ventuzLeftTeamTabletScene_PropertyChanged;
                this.ventuzLeftTeamTabletScene.OKButtonPressed += this.ventuzLeftTeamTabletScene_OKButtonPressed;
            }

            if (this.localVentuzHandler.TryAddClient("Right Tablet", false, out this.rightTeamTabletClient)) {
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
            this.insertScene.Dispose();

            this.hostScene.StatusChanged -= this.hostScene_StatusChanged;
            this.hostScene.Dispose();

            this.leftPlayerScene.StatusChanged -= this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.OKButtonPressed -= this.leftPlayerScene_OKButtonPressed;
            this.leftPlayerScene.Dispose();

            this.rightPlayerScene.StatusChanged -= this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.OKButtonPressed -= this.rightPlayerScene_OKButtonPressed;
            this.rightPlayerScene.Dispose();

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
            foreach (var item in this.DataList) item.ResetInput();
            this.TaskCounter = 0;
            this.SelectDataset(0);
            this.resetInput();
        }

        public void NextInput() {
            base.Next();
            this.TaskCounter++;
            this.SelectDataset(this.SelectedDatasetIndex + 1);
            this.resetInput();
        }

        internal void InitResolve()
        {
            this.TaskCounter = 1;
            if (this.SampleIncluded)
            {
                this.SelectDataset(1);
            }
            else
            {
                this.SelectDataset(0);
            }
        }

        public void Resolve()
        {
            if (this.LeftTeamIsTrue) this.LeftPlayerScore++;
            if (this.RightTeamIsTrue) this.RightPlayerScore++;
        }

        public override void Next()
        {
            base.Next();
            this.TaskCounter++;
            this.SelectDataset(this.SelectedDatasetIndex + 1);
            this.LeftTeamIsTrue = false;
            this.RightTeamIsTrue = false;
        }

        private void resetInput() {
            this.LeftTeamInputTop = string.Empty;
            this.LeftTeamInputBottom = string.Empty;
            this.LeftTeamIsTrue = false;
            this.Vleftplayer_ClearInput();
            this.Vlefttablet_ClearInput();
            this.RightTeamInputTop = string.Empty;
            this.RightTeamInputBottom = string.Empty;
            this.RightTeamIsTrue = false;
            this.Vrightplayer_ClearInput();
            this.Vrighttablet_ClearInput();
            this.Vinsert_ClearInputs();
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
            this.on_PropertyChanged(nameof(this.LeftTeamInputTop));
            this.on_PropertyChanged(nameof(this.LeftTeamInputBottom));
            this.on_PropertyChanged(nameof(this.RightTeamInputTop));
            this.on_PropertyChanged(nameof(this.RightTeamInputBottom));
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

        public override void New() {
            base.New();
            this.Filename = string.Empty;
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
        public void Import(
            string filename) {
            string subSender = "Import";
            if (File.Exists(filename)) {
                System.IO.StreamReader file = null;
                try {
                    string line;
                    file = new System.IO.StreamReader(filename, Encoding.UTF7);
                    while ((line = file.ReadLine()) != null) {
                        if (!string.IsNullOrEmpty(line)) this.tryAddDataset(new DatasetContent(line), -1);
                    }
                    file.Close();
                }
                catch (Exception exc) {
                    if (file != null) file.Close();
                    this.on_Error(subSender, exc.Message);
                }
                this.on_PropertyChanged("NameList");
                this.Save();
            }
            else {
                // Fehler weitergeben
                this.on_Error(subSender, "Datei nicht vorhanden - '" + filename + "'");
            }
        }

        private string textToCapitals(
            string sourceText) {
            string targetText = string.Empty;
            if (!string.IsNullOrEmpty(sourceText)) {

                targetText = sourceText;

                //    string[] sourceArray = sourceText.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                //    string target;
                //    int length;
                //    foreach (string item in sourceArray) {
                //        length = item.Length;
                //        if (length > 0) {
                //            if (length == 1) target = item.ToUpper();
                //            else target = item.Substring(0, 1).ToUpper() + item.Substring(1);
                //            targetText += target + " ";
                //        }
                //    }
                //    sourceText = targetText.Trim();
                //    targetText = string.Empty;
                //    sourceArray = sourceText.ToLower().Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                //    foreach (string item in sourceArray)
                //    {
                //        length = item.Length;
                //        if (length > 0)
                //        {
                //            if (length == 1) target = item.ToUpper();
                //            else target = item.Substring(0, 1).ToUpper() + item.Substring(1);
                //            targetText += target + "-";
                //        }
                //    }
            }
            return targetText.Trim();
        }

        public override void Vinsert_LoadScene() { this.insertScene.Load(); }
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
        public override void Vinsert_ScoreOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Score.ToOut(); }
        internal void Vinsert_TaskIn() {
            this.Vinsert_SetContent(false);
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.TaskToIn();
            if (this.TaskCounter > 0 && this.TaskCounter <= this.TaskCounterSize) this.Vinsert_TaskCounterIn();
        }
        internal void Vinsert_InputIn_LeftTop() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.SetInputLeftTextTop(this.LeftTeamInputTop);
                this.insertScene.InputLeftToIn();
            }
        }
        internal void Vinsert_InputIn_LeftBottom() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.SetInputLeftTextBottom(this.LeftTeamInputBottom);
                this.insertScene.InputLeftToIn();
            }
        }
        internal void Vinsert_InputIn_Left(
            bool resolved) {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.SetInputLeftTextTop(this.LeftTeamInputTop);
                this.insertScene.SetInputLeftTextBottom(this.LeftTeamInputBottom);
                if (resolved) {
                    if (this.LeftTeamIsTrue)
                    {
                        this.insertScene.SetInputLeftStatus(Insert.InputStates.True);
                        this.LeftPlayerScore++;
                        this.Vstage_SetScore();
                    }
                    else this.insertScene.SetInputLeftStatus(Insert.InputStates.False);
                }
                else {
                    this.insertScene.InputLeftToIn();
                    this.insertScene.SetInputLeftStatus(Insert.InputStates.Idle);
                }
            }
        }
        internal void Vinsert_InputIn_RightTop() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.SetInputRightTextTop(this.RightTeamInputTop);
                this.insertScene.InputRightToIn();
            }
        }
        internal void Vinsert_InputIn_RightBottom() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.SetInputRightTextBottom(this.RightTeamInputBottom);
                this.insertScene.InputRightToIn();
            }
        }
        internal void Vinsert_InputIn_Right(
            bool resolved) {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.SetInputRightTextTop(this.RightTeamInputTop);
                this.insertScene.SetInputRightTextBottom(this.RightTeamInputBottom);
                if (resolved) {
                    if (this.RightTeamIsTrue)
                    {
                        this.insertScene.SetInputRightStatus(Insert.InputStates.True);
                        this.RightPlayerScore++;
                        this.Vstage_SetScore();
                    }
                    else this.insertScene.SetInputRightStatus(Insert.InputStates.False);
                }
                else {
                    this.insertScene.InputRightToIn();
                    this.insertScene.SetInputRightStatus(Insert.InputStates.Idle);
                }
            }
        }
        internal void Vinsert_InputIn() {
            this.Vinsert_SetContent(false);
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.InputLeftToIn();
                this.insertScene.InputRightToIn();
            }
        }
        internal void Vinsert_SetContent(
            bool resolved) {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                this.insertScene.SetPositionX(this.TextInsertPositionX);
                this.insertScene.SetPositionY(this.TextInsertPositionY);
                this.insertScene.SetTaskText(this.SelectedDataset.Text);
                this.insertScene.SetInputLeftTextTop(this.LeftTeamInputTop);
                this.insertScene.SetInputLeftTextBottom(this.LeftTeamInputBottom);
                this.insertScene.SetInputRightTextTop(this.RightTeamInputTop);
                this.insertScene.SetInputRightTextBottom(this.RightTeamInputBottom);
                if (resolved) {
                    if (this.LeftTeamIsTrue) this.insertScene.SetInputLeftStatus(Insert.InputStates.True);
                    else this.insertScene.SetInputLeftStatus(Insert.InputStates.False);
                    if (this.RightTeamIsTrue) this.insertScene.SetInputRightStatus(Insert.InputStates.True);
                    else this.insertScene.SetInputRightStatus(Insert.InputStates.False);
                    if (this.LeftTeamIsTrue ||
                        this.RightTeamIsTrue)
                        this.insertScene.PlayJingleTrue();
                    else 
                        this.insertScene.PlayJingleFalse();
                }
                else {
                    this.insertScene.SetInputLeftStatus(Insert.InputStates.Idle);
                    this.insertScene.SetInputRightStatus(Insert.InputStates.Idle);
                }
            }
        }
        internal void Vinsert_ClearInputs() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.SetInputLeftTextTop(string.Empty);
                this.insertScene.SetInputLeftTextBottom(string.Empty);
                this.insertScene.SetInputRightTextTop(string.Empty);
                this.insertScene.SetInputRightTextBottom(string.Empty);
            }
        }
        internal void Vinsert_ContentOut() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.InputLeftToOut();
                this.insertScene.InputRightToOut();
                this.insertScene.TaskToOut();
            }
            this.Vinsert_TaskCounterOut();
        }

        public void Vinsert_TaskCounterIn() {
            this.Vinsert_SetTaskCounter();
            if (this.insertScene is Insert &&
                this.insertScene.TaskCounter is VentuzScenes.GamePool._Modules.TaskCounter) this.insertScene.TaskCounter.ToIn();
        }
        public void Vinsert_SetTaskCounter() { if (this.insertScene is Insert) this.Vinsert_SetTaskCounter(this.insertScene.TaskCounter, this.TaskCounter); }
        public void Vinsert_SetTaskCounter(
            VentuzScenes.GamePool._Modules.TaskCounter scene,
            int counter) {
            if (scene is VentuzScenes.GamePool._Modules.TaskCounter) {
                scene.SetPositionX(this.TaskCounterPositionX);
                scene.SetPositionY(this.TaskCounterPositionY);
                scene.SetStyle(VentuzScenes.GamePool._Modules.TaskCounter.Styles.Numeric);
                scene.SetSize(this.TaskCounterSize);
                scene.SetCounter(counter);
            }
        }
        public void Vinsert_TaskCounterOut() {
            if (this.insertScene is Insert &&
                this.insertScene.TaskCounter is VentuzScenes.GamePool._Modules.TaskCounter) this.insertScene.TaskCounter.ToOut();
        }

        public override void Vinsert_UnloadScene() { this.insertScene.Unload(); }

        public override void Vhost_LoadScene() { this.hostScene.Load(); }
        public void Vhost_SetContent() {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                string hostText = this.SelectedDataset.HostText;
                if (this.TaskCounter == 0) hostText = string.Format("Beispiel: {0}", hostText);
                else if (this.TaskCounter > 0 && this.TaskCounter <= this.TaskCounterSize) hostText = string.Format("{0}/{1}: {2}", taskCounter.ToString(), this.TaskCounterSize.ToString(), hostText);
                this.hostScene.SetHeadline(hostText);
                this.hostScene.SetInfo(this.SelectedDataset.InfoText);
                this.hostScene.SetLeftTeamInputBottom(this.LeftTeamInputTop);
                this.hostScene.SetLeftTeamInputTop(this.LeftTeamInputBottom);
                this.hostScene.SetRightTeamInputBottom(this.RightTeamInputTop);
                this.hostScene.SetRightTeamInputTop(this.RightTeamInputBottom);
            }
        }
        public override void Vhost_UnloadScene() { this.hostScene.Unload(); }

        public override void Vleftplayer_LoadScene() { this.leftPlayerScene.Load(); }
        internal void Vleftplayer_SetContent() {
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) this.leftPlayerScene.SetHeadline(this.SelectedDataset.Text);
        }
        internal void Vleftplayer_EnableInput() { if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.EnableInput(); }
        internal void Vleftplayer_DisableInput() { if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.DisableInput(); }
        internal void Vleftplayer_ClearInput() { 
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.ClearInput();
            this.LeftTeamInputTop = string.Empty;
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.SetLeftTeamInputBottom(this.LeftTeamInputTop);
        }
        public override void Vleftplayer_UnloadScene() { this.leftPlayerScene.Unload(); }

        public override void Vrightplayer_LoadScene() { this.rightPlayerScene.Load(); }
        internal void Vrightplayer_SetContent() {
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) this.rightPlayerScene.SetHeadline(this.SelectedDataset.Text);
        }
        internal void Vrightplayer_EnableInput() { if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.EnableInput(); }
        internal void Vrightplayer_DisableInput() { if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.DisableInput(); }
        internal void Vrightplayer_ClearInput() { 
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.ClearInput();
            this.RightTeamInputTop = string.Empty;
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.SetRightTeamInputBottom(this.RightTeamInputTop);
        }
        public override void Vrightplayer_UnloadScene() { this.rightPlayerScene.Unload(); }

        internal void Vplayers_EnableInput() {
            this.Vleftplayer_EnableInput();
            this.Vrightplayer_EnableInput();
            this.Vlefttablet_EnableInput();
            this.Vrighttablet_EnableInput();
        }

        internal void Vstage_ContentIn() {
            this.Vhost_SetContent();
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ToIn();
            this.Vleftplayer_SetContent();
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.ToIn();
            this.Vrightplayer_SetContent();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.ToIn();
            this.Vlefttablet_SetContent();
            this.Vrighttablet_SetContent();
        }

        internal void Vstage_ContentOut() {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ToOut();
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.ToOut();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.ToOut();
            this.Vlefttablet_Init();
            this.Vrighttablet_Init();
        }

        public override void Vstage_Init() {
            base.Vstage_Init();
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.ClearInput();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.ClearInput();
            this.Vlefttablet_Init();
            this.Vrighttablet_Init();
        }

        public void Vlefttablet_Start() { this.leftTeamTabletClient.Start(this.LeftTeamTabletHostname); }
        public void Vlefttablet_Init() {
            if (this.VentuzLeftTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.ventuzLeftTeamTabletScene.SetHeadline(string.Empty);
                this.ventuzLeftTeamTabletScene.ClearInput();
                this.ventuzLeftTeamTabletScene.DisableInput();
            }
        }
        internal void Vlefttablet_SetContent() {
            if (this.VentuzLeftTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) this.ventuzLeftTeamTabletScene.SetHeadline(this.SelectedDataset.Text);
        }
        internal void Vlefttablet_EnableInput() {
            if (this.VentuzLeftTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzLeftTeamTabletScene.EnableInput();
        }
        internal void Vlefttablet_DisableInput() {
            if (this.VentuzLeftTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzLeftTeamTabletScene.DisableInput();
        }
        internal void Vlefttablet_ClearInput() {
            if (this.VentuzLeftTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzLeftTeamTabletScene.ClearInput();
            this.LeftTeamInputBottom = string.Empty;
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.SetLeftTeamInputTop(this.LeftTeamInputBottom);

        }
        public void Vlefttablet_ShutDown() { this.leftTeamTabletClient.Shutdown(); }

        public void Vrighttablet_Start() { this.rightTeamTabletClient.Start(this.RightTeamTabletHostname); }
        public void Vrighttablet_Init() {
            if (this.VentuzRightTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.ventuzRightTeamTabletScene.SetHeadline(string.Empty);
                this.ventuzRightTeamTabletScene.ClearInput();
                this.ventuzRightTeamTabletScene.DisableInput();
            }
        }
        internal void Vrighttablet_SetContent() {
            if (this.VentuzRightTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) this.ventuzRightTeamTabletScene.SetHeadline(this.SelectedDataset.Text);
        }
        internal void Vrighttablet_EnableInput() {
            if (this.VentuzRightTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzRightTeamTabletScene.EnableInput();
        }
        internal void Vrighttablet_DisableInput() {
            if (this.VentuzRightTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzRightTeamTabletScene.DisableInput();
        }
        internal void Vrighttablet_ClearInput() {
            if (this.VentuzRightTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzRightTeamTabletScene.ClearInput();
            this.RightTeamInputBottom = string.Empty;
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.SetRightTeamInputTop(this.RightTeamInputBottom);
        }
        public void Vrighttablet_ShutDown() { this.rightTeamTabletClient.Shutdown(); }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
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
                if (e.PropertyName == "Text") {
                    this.buildNameList();
                    this.on_PropertyChanged("NameList");
                }
            }
            this.Save();
        }

        void localVentuzHandler_Error(object sender, ErrorEventArgs e) {
            Exception exc = e.GetException();
            this.on_Error(sender, new Messaging.ErrorEventArgs(this, sender.ToString(), DateTime.Now, exc.ToString()));
        }

        void leftPlayerScene_OKButtonPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerScene_OKButtonPressed(object content) {
            this.LeftTeamInputTop = this.textToCapitals(this.leftPlayerScene.Input);
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.SetLeftTeamInputBottom(this.LeftTeamInputTop);
        }

        void rightPlayerScene_OKButtonPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_OKButtonPressed(object content) {
            this.RightTeamInputTop = this.textToCapitals(this.rightPlayerScene.Input);
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.SetRightTeamInputBottom(this.RightTeamInputTop);
        }

        void leftTeamTabletClient_HostnameChanged(object sender, VRemote4.HandlerSi.Client.HostnameArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftTeamTabletClient_HostnameChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftTeamTabletClient_HostnameChanged(object content) {
            VRemote4.HandlerSi.Client.HostnameArgs e = content as VRemote4.HandlerSi.Client.HostnameArgs;
            if (e is VRemote4.HandlerSi.Client.HostnameArgs) this.LeftTeamTabletHostname = e.Name;
        }

        void leftTeamTabletClient_StatusChanged(object sender, VRemote4.HandlerSi.Client.ClientStateArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftTeamTabletClient_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftTeamTabletClient_StatusChanged(object content) {
            VRemote4.HandlerSi.Client.ClientStateArgs e = content as VRemote4.HandlerSi.Client.ClientStateArgs;
            if (e is VRemote4.HandlerSi.Client.ClientStateArgs &&
                e.Status.HasValue &&
                e.Status.Value == VRemote4.HandlerSi.Client.ClientStates.Active &&
                this.ventuzLeftTeamTabletScene is VRemote4.HandlerSi.Scene) this.ventuzLeftTeamTabletScene.Load();
        }

        void ventuzLeftTeamTabletScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzLeftTeamTabletScene_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzLeftTeamTabletScene_StatusChanged(object content) {
            VRemote4.HandlerSi.Scene.StatusArgs e = content as VRemote4.HandlerSi.Scene.StatusArgs;
            if (e is VRemote4.HandlerSi.Scene.StatusArgs) {
                if (e.Status == VRemote4.HandlerSi.Scene.States.Unloaded) this.leftTeamTabletClient.Shutdown();
                else if (e.Status == VRemote4.HandlerSi.Scene.States.Available) this.Vlefttablet_Init();
            }
            this.on_PropertyChanged("VentuzLeftTeamTabletSceneStatus");
        }

        void ventuzLeftTeamTabletScene_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzLeftTeamTabletScene_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzLeftTeamTabletScene_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "Selection") {
                    //this.LeftTeamInput = this.ventuzTerminalLeftScene.Controller.Selection;
                }
            }
        }

        void ventuzLeftTeamTabletScene_OKButtonPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzLeftTeamTabletScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzLeftTeamTabletScene_OKButtonPressed(object content) {
            this.LeftTeamInputBottom = this.textToCapitals(this.ventuzLeftTeamTabletScene.Input);
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.SetLeftTeamInputTop(this.LeftTeamInputBottom);
        }

        void rightTeamTabletClient_HostnameChanged(object sender, VRemote4.HandlerSi.Client.HostnameArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightTeamTabletClient_HostnameChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightTeamTabletClient_HostnameChanged(object content) {
            VRemote4.HandlerSi.Client.HostnameArgs e = content as VRemote4.HandlerSi.Client.HostnameArgs;
            if (e is VRemote4.HandlerSi.Client.HostnameArgs) this.RightTeamTabletHostname = e.Name;
        }

        void rightTeamTabletClient_StatusChanged(object sender, VRemote4.HandlerSi.Client.ClientStateArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightTeamTabletClient_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightTeamTabletClient_StatusChanged(object content) {
            VRemote4.HandlerSi.Client.ClientStateArgs e = content as VRemote4.HandlerSi.Client.ClientStateArgs;
            if (e is VRemote4.HandlerSi.Client.ClientStateArgs &&
                e.Status.HasValue &&
                e.Status.Value == VRemote4.HandlerSi.Client.ClientStates.Active &&
                this.ventuzRightTeamTabletScene is VRemote4.HandlerSi.Scene) this.ventuzRightTeamTabletScene.Load();
        }

        void ventuzRightTeamTabletScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzRightTeamTabletScene_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzRightTeamTabletScene_StatusChanged(object content) {
            VRemote4.HandlerSi.Scene.StatusArgs e = content as VRemote4.HandlerSi.Scene.StatusArgs;
            if (e is VRemote4.HandlerSi.Scene.StatusArgs) {
                if (e.Status == VRemote4.HandlerSi.Scene.States.Unloaded) this.rightTeamTabletClient.Shutdown();
                else if (e.Status == VRemote4.HandlerSi.Scene.States.Available) this.Vrighttablet_Init();
            }
            this.on_PropertyChanged("VentuzRightTeamTabletSceneStatus");
        }

        void ventuzRightTeamTabletScene_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzRightTeamTabletScene_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzRightTeamTabletScene_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "Selection") {
                    //this.RightTeamInput = this.ventuzTerminalRightScene.Controller.Selection;
                }
            }
        }

        void ventuzRightTeamTabletScene_OKButtonPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzRightTeamTabletScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzRightTeamTabletScene_OKButtonPressed(object content) {
            this.RightTeamInputBottom = this.textToCapitals(this.ventuzRightTeamTabletScene.Input);
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.SetRightTeamInputTop(this.RightTeamInputBottom);
        }

        #endregion

    }
}
