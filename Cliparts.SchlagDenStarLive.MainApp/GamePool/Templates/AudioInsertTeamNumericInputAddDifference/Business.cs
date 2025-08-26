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

using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.AudioInsertTeamNumericInputAddDifference;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.AudioInsertTeamNumericInputAddDifference {

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetContent : Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private string name = string.Empty;
        public string Name {
            get { return this.name; }
            set {
                if (this.name != value) {
                    if (string.IsNullOrEmpty(value)) this.name = string.Empty;
                    else this.name = value;
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        private int solution;
        public int Solution {
            get { return this.solution; }
            set {
                if (this.solution != value) {
                    this.solution = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string hostText = string.Empty;
        public string HostText {
            get { return this.hostText; }
            set {
                if (this.hostText != value) {
                    if (string.IsNullOrEmpty(value)) this.hostText = string.Empty;
                    else this.hostText = Regex.Replace(value, "(?<!\r)\n", "\r\n");
                    this.on_PropertyChanged();
                }
            }
        }

        private string audioFilename = string.Empty;
        public string AudioFilename {
            get { return this.audioFilename; }
            set {
                if (this.audioFilename != value) {
                    if (value == null) value = string.Empty;
                    this.audioFilename = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContent() { }
        public DatasetContent(
            string audioFilename) {
            if (string.IsNullOrEmpty(audioFilename)) {
                this.Name = "?";
                this.AudioFilename = string.Empty;
                this.HostText = string.Empty;
            }
            else {
                this.Name = Path.GetFileNameWithoutExtension(audioFilename);
                this.AudioFilename = audioFilename;
                this.HostText = this.Name;
            }
        }

        public void Clone(
            DatasetContent source) {
            if (source is DatasetContent) {
                this.Name = source.Name;
                this.Solution = source.Solution;
                this.HostText = source.HostText;
                this.AudioFilename = source.AudioFilename;                
            }
            else {
                this.Name = string.Empty;
                this.Solution = 0;
                this.HostText = string.Empty;
                this.AudioFilename = string.Empty;
            }
        }

        private void buildToString() { this.toString = this.Name; }

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

        private int gamePositionX = 0;
        public int GamePositionX {
            get { return this.gamePositionX; }
            set {
                if (this.gamePositionX != value) {
                    this.gamePositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetGame();
                }
            }
        }

        private int gamePositionY = 0;
        public int GamePositionY {
            get { return this.gamePositionY; }
            set {
                if (this.gamePositionY != value) {
                    this.gamePositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetGame();
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

        private int taskCounterSize = 13;
        public int TaskCounterSize {
            get { return this.taskCounterSize; }
            set {
                if (this.taskCounterSize != value) {
                    this.taskCounterSize = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTaskCounter();
                }
            }
        }

        private int leftPlayerTopValue = 0;
        public int LeftPlayerTopValue
        {
            get { return this.leftPlayerTopValue; }
            set
            {
                if (this.leftPlayerTopValue != value)
                {
                    this.leftPlayerTopValue = value;
                    this.on_PropertyChanged();
                    this.calcDifferences();
                }
            }
        }

        private int leftPlayerTopDifference = 0;
        public int LeftPlayerTopDifference
        {
            get { return this.leftPlayerTopDifference; }
            private set
            {
                if (this.leftPlayerTopDifference != value)
                {
                    this.leftPlayerTopDifference = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int leftPlayerBottomValue = 0;
        public int LeftPlayerBottomValue
        {
            get { return this.leftPlayerBottomValue; }
            set
            {
                if (this.leftPlayerBottomValue != value)
                {
                    this.leftPlayerBottomValue = value;
                    this.on_PropertyChanged();
                    this.calcDifferences();
                }
            }
        }

        private int leftPlayerBottomDifference = 0;
        public int LeftPlayerBottomDifference
        {
            get { return this.leftPlayerBottomDifference; }
            private set
            {
                if (this.leftPlayerBottomDifference != value)
                {
                    this.leftPlayerBottomDifference = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerTopValue = 0;
        public int RightPlayerTopValue
        {
            get { return this.rightPlayerTopValue; }
            set
            {
                if (this.rightPlayerTopValue != value)
                {
                    this.rightPlayerTopValue = value;
                    this.on_PropertyChanged();
                    this.calcDifferences();
                }
            }
        }

        private int rightPlayerTopDifference = 0;
        public int RightPlayerTopDifference
        {
            get { return this.rightPlayerTopDifference; }
            private set
            {
                if (this.rightPlayerTopDifference != value)
                {
                    this.rightPlayerTopDifference = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerBottomValue = 0;
        public int RightPlayerBottomValue
        {
            get { return this.rightPlayerBottomValue; }
            set
            {
                if (this.rightPlayerBottomValue != value)
                {
                    this.rightPlayerBottomValue = value;
                    this.on_PropertyChanged();
                    this.calcDifferences();
                }
            }
        }

        private int rightPlayerBottomDifference = 0;
        public int RightPlayerBottomDifference
        {
            get { return this.rightPlayerBottomDifference; }
            private set
            {
                if (this.rightPlayerBottomDifference != value)
                {
                    this.rightPlayerBottomDifference = value;
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
                this.repressPropertyChanged = true;
                this.tryRemoveAllDatasets();
                if (value is DatasetContent[]) {
                    foreach (DatasetContent item in value) this.tryAddDataset(item, -1);
                }
                this.repressPropertyChanged = false;
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.AudioInsertTeamNumericInputAddDifference'", typeIdentifier);
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
            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;

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
            this.LeftPlayerTopValue = 0;
            this.RightPlayerTopValue = 0;
            this.LeftPlayerBottomValue = 0;
            this.RightPlayerBottomValue = 0;
            this.SelectDataset(0);
        }

        internal void Resolve() {
            if (this.TaskCounter > 0) {
                this.LeftPlayerScore += this.LeftPlayerTopDifference + this.LeftPlayerBottomDifference;
                this.RightPlayerScore += this.RightPlayerTopDifference + this.RightPlayerBottomDifference;
            }
        }

        public override void Next() {
            base.Next();
            this.TaskCounter++;
            this.LeftPlayerTopValue = 0;
            this.RightPlayerTopValue = 0;
            this.LeftPlayerBottomValue = 0;
            this.RightPlayerBottomValue = 0;
            this.SelectDataset(this.SelectedDatasetIndex + 1);
        }

        private void calcDifferences() {
            if (this.SelectedDataset is DatasetContent &&
                this.LeftPlayerTopValue > 0)
            {
                this.LeftPlayerTopDifference = Math.Abs(this.LeftPlayerTopValue - this.SelectedDataset.Solution);
            }
            else
            {
                this.LeftPlayerTopDifference = 0;
            }

            if (this.SelectedDataset is DatasetContent &&
                this.LeftPlayerBottomValue > 0)
            {
                this.LeftPlayerBottomDifference = Math.Abs(this.LeftPlayerBottomValue - this.SelectedDataset.Solution);
            }
            else
            {
                this.LeftPlayerBottomDifference = 0;
            }

            if (this.SelectedDataset is DatasetContent &&
                this.RightPlayerTopValue > 0)
            {
                this.RightPlayerTopDifference = Math.Abs(this.RightPlayerTopValue - this.SelectedDataset.Solution);
            }
            else
            {
                this.RightPlayerTopDifference = 0;
            }

            if (this.SelectedDataset is DatasetContent &&
                this.RightPlayerBottomValue > 0)
            {
                this.RightPlayerBottomDifference = Math.Abs(this.RightPlayerBottomValue - this.SelectedDataset.Solution);
            }
            else
            {
                this.RightPlayerBottomDifference = 0;
            }
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
                DatasetContent dataset = new DatasetContent();
                dataset.Clone(newDataset);
                dataset.Error += this.dataset_Error;
                dataset.PropertyChanged += this.dataset_PropertyChanged;
                if (insertIndex >= 0 &&
                    insertIndex < this.DatasetsCount) {
                    this.dataList.Insert(insertIndex, dataset);
                    this.names.Insert(insertIndex, dataset.ToString());
                }
                else {
                    this.dataList.Add(dataset);
                    this.names.Add(dataset.ToString());
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

        public override void New()
        {
            base.New();
            this.Filename = string.Empty;
        }

        public void Load(
            string filename) {
            string subSender = "Load";
            this.repressPropertyChanged = true;
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
            this.repressPropertyChanged = false;
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
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }

        public override void Vinsert_SetScore() {
            base.Vinsert_SetScore();
            this.Vinsert_SetGame();
        }

        internal void Vinsert_GameIn() {
            this.Vinsert_SetGame();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Game.ToIn();
        }
        internal void Vinsert_SetGame(
            Insert scene) {
            if (scene is Insert) {
                scene.Game.SetPositionX(this.GamePositionX);
                scene.Game.SetPositionY(this.GamePositionY);
                scene.Game.SetLeftPlayerName(this.LeftPlayerName);
                scene.Game.SetLeftPlayerScore(this.LeftPlayerScore);
                scene.Game.SetRightPlayerName(this.RightPlayerName);
                scene.Game.SetRightPlayerScore(this.RightPlayerScore);
            }
        }
        internal void Vinsert_SetGame(
            Insert scene,
            string filename) {
            if (scene is Insert) {
                scene.Game.SetAudioFilename(filename);
            }
        }
        internal void Vinsert_SetGame(
            Insert scene,
            string taskText,
            int solution) {
            if (scene is Insert) {
                scene.Game.SetTaskText(taskText);
                scene.Game.SetTaskSolution(solution);
            }
        }
        internal void Vinsert_SetGame(
            Insert scene,
            int leftPlayerTopInput,
            int leftPlayerTopDifference,
            int leftPlayerBottomInput,
            int leftPlayerBottomDifference,
            int rightPlayerTopInput,
            int rightPlayerTopDifference,
            int rightPlayerBottomInput,
            int rightPlayerBottomDifference) {
            if (scene is Insert) {
                scene.Game.SetLeftPlayerTopInput(leftPlayerTopInput);
                scene.Game.SetLeftPlayerTopDifference(leftPlayerTopDifference);
                scene.Game.SetLeftPlayerBottomInput(leftPlayerBottomInput);
                scene.Game.SetLeftPlayerBottomDifference(leftPlayerBottomDifference);
                scene.Game.SetRightPlayerTopInput(rightPlayerTopInput);
                scene.Game.SetRightPlayerTopDifference(rightPlayerTopDifference);
                scene.Game.SetRightPlayerBottomInput(rightPlayerBottomInput);
                scene.Game.SetRightPlayerBottomDifference(rightPlayerBottomDifference);
            }
        }
        internal void Vinsert_SetGame() {
            if (this.insertScene is Insert) this.Vinsert_SetGame(this.insertScene);
        }
        internal void Vinsert_PlayAudio() {
            if (this.insertScene is Insert) {
                if (this.SelectedDataset is DatasetContent) this.Vinsert_SetGame(this.insertScene, this.SelectedDataset.AudioFilename);
                this.insertScene.Game.PlayAudio();
            }
        }
        internal void Vinsert_TaskIn() {
            if (this.insertScene is Insert) {
                if (this.SelectedDataset is DatasetContent) this.Vinsert_SetGame(this.insertScene, this.SelectedDataset.Name, this.SelectedDataset.Solution);
                this.insertScene.Game.TaskToIn();
            }
            if (this.TaskCounter > 0 &&
                this.TaskCounter <= this.TaskCounterSize) this.Vinsert_TaskCounterIn();
        }
        internal void Vinsert_SolutionIn() {
            if (this.insertScene is Insert) {
                if (this.SelectedDataset is DatasetContent) this.Vinsert_SetGame(this.insertScene, this.SelectedDataset.Name, this.SelectedDataset.Solution);
                this.insertScene.Game.SolutionToIn();
            }
        }
        internal void Vinsert_TaskOut() {
            if (this.insertScene is Insert) this.insertScene.Game.TaskToOut();
            this.Vinsert_TaskCounterOut();
        }
        internal void Vinsert_InputIn() {
            if (this.insertScene is Insert) {
                this.Vinsert_SetGame(
                    this.insertScene,
                    this.LeftPlayerTopValue,
                    this.LeftPlayerTopDifference,
                    this.LeftPlayerBottomValue,
                    this.LeftPlayerBottomDifference,
                    this.RightPlayerTopValue,
                    this.RightPlayerTopDifference,
                    this.RightPlayerBottomValue,
                    this.RightPlayerBottomDifference);
                this.insertScene.Game.InputToIn();
            }
        }
        internal void Vinsert_DifferencesIn() {
            if (this.insertScene is Insert) {
                this.Vinsert_SetGame(
                    this.insertScene,
                    this.LeftPlayerTopValue,
                    this.LeftPlayerTopDifference,
                    this.LeftPlayerBottomValue,
                    this.LeftPlayerBottomDifference,
                    this.RightPlayerTopValue,
                    this.RightPlayerTopDifference,
                    this.RightPlayerBottomValue,
                    this.RightPlayerBottomDifference);
                this.insertScene.Game.ResolvedToIn();
            }
        }
        internal void Vinsert_InputOut() {
            if (this.insertScene is Insert) this.insertScene.Game.InputToOut();
        }

        internal void Vinsert_GameOut() { 
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Game.ToOut();
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

        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vstage_Init() {
            base.Vstage_Init();
        }
        internal void Vstage_ContentIn() {
            this.Vhost_ContentIn();
            this.Vleftplayer_ResetInput();
            this.Vrightplayer_ResetInput();
            this.Vleftplayer_ContentIn();
            this.Vrightplayer_ContentIn();
            this.Vlefttablet_ClearInput();
            this.Vlefttablet_SetContent();
            this.Vrighttablet_ClearInput();
            this.Vrighttablet_SetContent();
        }
        internal void Vstage_ContentOut() {
            this.Vhost_ContentOut();
            this.Vleftplayer_ContentOut();
            this.Vrightplayer_ContentOut();
            this.Vlefttablet_ClearInput();
            this.Vrighttablet_ClearInput();
        }

        public override void Vhost_LoadScene() { this.hostScene.Load(); }
        internal void Vhost_ContentIn() {
            this.Vhost_SetPlayerInput();
            if (this.hostScene is Host &&
                this.SelectedDataset is DatasetContent) {
                this.hostScene.SetHeadline(this.SelectedDataset.HostText);
                this.hostScene.SetInfo(string.Empty);
                this.hostScene.ToIn();
            }
        }
        internal void Vhost_SetPlayerInput() {
            string unit = string.Empty;
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vhost_SetPlayerInput(
                    this.hostScene,
                    this.LeftPlayerTopValue,
                    this.LeftPlayerTopDifference,
                    this.LeftPlayerBottomValue,
                    this.LeftPlayerBottomDifference,
                    this.RightPlayerTopValue,
                    this.RightPlayerTopDifference,
                    this.RightPlayerBottomValue,
                    this.RightPlayerBottomDifference);
        }
        internal void Vhost_SetPlayerInput(
            Host scene,
            int leftPlayerTopInput,
            int leftPlayerTopDifference,
            int leftPlayerBottomInput,
            int leftPlayerBottomDifference,
            int rightPlayerTopInput,
            int rightPlayerTopDifference,
            int rightPlayerBottomInput,
            int rightPlayerBottomDifference)
        {
            if (scene is Host) {
                if (leftPlayerTopInput > 0) scene.SetLeftTeamInputBottom(string.Format("{0} ({1})", leftPlayerTopInput.ToString(), leftPlayerTopDifference.ToString()));
                else scene.SetLeftTeamInputBottom(string.Empty);

                if (leftPlayerBottomInput > 0) scene.SetLeftTeamInputTop(string.Format("{0} ({1})", leftPlayerBottomInput.ToString(), leftPlayerBottomDifference.ToString()));
                else scene.SetLeftTeamInputTop(string.Empty);

                if (rightPlayerTopInput > 0) scene.SetRightTeamInputBottom(string.Format("{0} ({1})", rightPlayerTopInput.ToString(), rightPlayerTopDifference.ToString()));
                else scene.SetRightTeamInputBottom(string.Empty);

                if (rightPlayerBottomInput > 0) scene.SetRightTeamInputTop(string.Format("{0} ({1})", rightPlayerBottomInput.ToString(), rightPlayerBottomDifference.ToString()));
                else scene.SetRightTeamInputTop(string.Empty);
            }
        }
        internal void Vhost_ContentOut() {
            if (this.hostScene is Host) this.hostScene.ToOut();
        }
        public override void Vhost_UnloadScene() { this.hostScene.Unload(); }

        internal void Vplayers_EnableInput()
        {
            this.Vleftplayer_UnlockInput();
            this.Vrightplayer_UnlockInput();
            this.Vlefttablet_EnableInput();
            this.Vrighttablet_EnableInput();
        }

        public override void Vleftplayer_LoadScene() { this.leftPlayerScene.Load(); }
        internal void Vleftplayer_ContentIn() {
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                this.leftPlayerScene.SetTask(this.SelectedDataset.Name);
                this.leftPlayerScene.ToIn();
            }
        }
        internal void Vleftplayer_ContentOut() { if (this.leftPlayerScene is Player) this.leftPlayerScene.ToOut(); }
        internal void Vleftplayer_ResetInput() { if (this.leftPlayerScene is Player) this.leftPlayerScene.Reset(); }
        internal void Vleftplayer_UnlockInput() { if (this.leftPlayerScene is Player) this.leftPlayerScene.UnlockTouch(); }
        internal void Vleftplayer_LockInput() { if (this.leftPlayerScene is Player) this.leftPlayerScene.LockTouch(); }
        public override void Vleftplayer_UnloadScene() { this.leftPlayerScene.Unload(); }

        public override void Vrightplayer_LoadScene() { this.rightPlayerScene.Load(); }
        internal void Vrightplayer_ContentIn() {
            if (this.rightPlayerScene is Player &&
                this.SelectedDataset is DatasetContent) {
                this.rightPlayerScene.SetTask(this.SelectedDataset.Name); 
                this.rightPlayerScene.ToIn();
            }
        }
        internal void Vrightplayer_ContentOut() { if (this.rightPlayerScene is Player) this.rightPlayerScene.ToOut(); }
        internal void Vrightplayer_ResetInput() { if (this.rightPlayerScene is Player) this.rightPlayerScene.Reset(); }
        internal void Vrightplayer_UnlockInput() { if (this.rightPlayerScene is Player) this.rightPlayerScene.UnlockTouch(); }
        internal void Vrightplayer_LockInput() { if (this.rightPlayerScene is Player) this.rightPlayerScene.LockTouch(); }
        public override void Vrightplayer_UnloadScene() { this.rightPlayerScene.Unload(); }

        public void Vlefttablet_Start() { this.leftTeamTabletClient.Start(this.LeftTeamTabletHostname); }
        public void Vlefttablet_Init()
        {
            if (this.VentuzLeftTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available)
            {
                this.ventuzLeftTeamTabletScene.SetTask(string.Empty);
                this.ventuzLeftTeamTabletScene.Reset();
                this.ventuzLeftTeamTabletScene.LockTouch();
            }
        }
        internal void Vlefttablet_SetContent()
        {
            if (this.VentuzLeftTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) this.ventuzLeftTeamTabletScene.SetTask(this.SelectedDataset.Name);
        }
        internal void Vlefttablet_EnableInput()
        {
            if (this.VentuzLeftTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzLeftTeamTabletScene.UnlockTouch();
        }
        internal void Vlefttablet_DisableInput()
        {
            if (this.VentuzLeftTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzLeftTeamTabletScene.LockTouch();
        }
        internal void Vlefttablet_ClearInput()
        {
            if (this.VentuzLeftTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzLeftTeamTabletScene.Reset();
            this.LeftPlayerBottomValue = 0;
            this.Vhost_SetPlayerInput();
        }
        public void Vlefttablet_ShutDown() { this.leftTeamTabletClient.Shutdown(); }

        public void Vrighttablet_Start() { this.rightTeamTabletClient.Start(this.RightTeamTabletHostname); }
        public void Vrighttablet_Init()
        {
            if (this.VentuzRightTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available)
            {
                this.ventuzRightTeamTabletScene.SetTask(string.Empty);
                this.ventuzRightTeamTabletScene.Reset();
                this.ventuzRightTeamTabletScene.LockTouch();
            }
        }
        internal void Vrighttablet_SetContent()
        {
            if (this.VentuzRightTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) this.ventuzRightTeamTabletScene.SetTask(this.SelectedDataset.Name);
        }
        internal void Vrighttablet_EnableInput()
        {
            if (this.VentuzRightTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzRightTeamTabletScene.UnlockTouch();
        }
        internal void Vrighttablet_DisableInput()
        {
            if (this.VentuzRightTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzRightTeamTabletScene.LockTouch();
        }
        internal void Vrighttablet_ClearInput()
        {
            if (this.VentuzRightTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzRightTeamTabletScene.Reset();
            this.RightPlayerBottomValue = 0;
            this.Vhost_SetPlayerInput();
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
            if (e is PropertyChangedEventArgs &&
                !this.repressPropertyChanged) {
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
            this.LeftPlayerTopValue = Convert.ToInt32(this.leftPlayerScene.Text);
            this.Vhost_SetPlayerInput();
        }

        void rightPlayerScene_OKButtonPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_OKButtonPressed(object content) {
            this.RightPlayerTopValue = Convert.ToInt32(this.rightPlayerScene.Text);
            this.Vhost_SetPlayerInput();
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
                if (e.Status == VRemote4.HandlerSi.Scene.States.Unloaded) this.leftTeamTabletClient.Shutdown();
                else if (e.Status == VRemote4.HandlerSi.Scene.States.Available) this.Vlefttablet_Init();
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
            this.LeftPlayerBottomValue = Convert.ToInt32(this.ventuzLeftTeamTabletScene.Text);
            this.Vhost_SetPlayerInput();
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
                if (e.Status == VRemote4.HandlerSi.Scene.States.Unloaded) this.rightTeamTabletClient.Shutdown();
                else if (e.Status == VRemote4.HandlerSi.Scene.States.Available) this.Vrighttablet_Init();
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
            this.RightPlayerBottomValue = Convert.ToInt32(this.ventuzRightTeamTabletScene.Text);
            this.Vhost_SetPlayerInput();
        }

        #endregion
    }
}
