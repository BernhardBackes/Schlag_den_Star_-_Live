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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.CashRegister;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.CashRegister {

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetContent : Cliparts.Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private string name = string.Empty;
        public string Name {
            get { return this.name; }
            set {
                if (this.name != value) {
                    if (value == null) value = string.Empty;
                    this.name = value;
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        private string pictureFilename = string.Empty;
        public string PictureFilename {
            get { return this.pictureFilename; }
            set {
                if (this.pictureFilename != value) {
                    if (value == null) value = string.Empty;
                    this.pictureFilename = value;
                    this.Picture = Helper.getThumbnailFromMediaFile(value);
                    this.on_PropertyChanged();
                }
            }
        }

        [XmlIgnore]
        public Image Picture { get; private set; }

        private string solutionFilename = string.Empty;
        public string SolutionFilename {
            get { return this.solutionFilename; }
            set {
                if (this.solutionFilename != value) {
                    if (value == null) value = string.Empty;
                    this.solutionFilename = value;
                    this.Solution = Helper.getThumbnailFromMediaFile(value);
                    this.on_PropertyChanged();
                }
            }
        }

        [XmlIgnore]
        public Image Solution { get; private set; }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContent() { }
        public DatasetContent(
            string pictureFilename) {
            if (string.IsNullOrEmpty(pictureFilename)) {
                this.Name = "?";
                this.PictureFilename = string.Empty;
                this.SolutionFilename = string.Empty;
            }
            else {
                this.Name = Path.GetFileNameWithoutExtension(pictureFilename);
                this.PictureFilename = pictureFilename;
                this.SolutionFilename = string.Empty;
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

        private double leftPlayerInput = 0;
        public double LeftPlayerInput {
            get { return this.leftPlayerInput; }
            set {
                if (this.leftPlayerInput != value) {
                    this.leftPlayerInput = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double rightPlayerInput = 0;
        public double RightPlayerInput {
            get { return this.rightPlayerInput; }
            set {
                if (this.rightPlayerInput != value) {
                    this.rightPlayerInput = value;
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

        private bool repressPropertyChanged = false;

        private VRemote4.HandlerSi.Business localVentuzHandler;

        private VRemote4.HandlerSi.Client.Business displayClient;
        private Displays ventuzDisplayScene;
        public VRemote4.HandlerSi.Scene.States VentuzDisplaySceneStatus {
            get {
                if (this.ventuzDisplayScene is Displays) return this.ventuzDisplayScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }
        private string displayClientHostname = string.Empty;
        public string DisplayClientHostname {
            get { return this.displayClientHostname; }
            set {
                if (this.displayClientHostname != value) {
                    if (value == null) value = string.Empty;
                    this.displayClientHostname = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private VRemote4.HandlerSi.Client.Business terminalLeftClient;
        private Terminal ventuzTerminalLeftScene;
        public VRemote4.HandlerSi.Scene.States VentuzTerminalLeftSceneStatus {
            get {
                if (this.ventuzTerminalLeftScene is Terminal) return this.ventuzTerminalLeftScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }
        private string terminalLeftClientHostname = string.Empty;
        public string TerminalLeftClientHostname {
            get { return this.terminalLeftClientHostname; }
            set {
                if (this.terminalLeftClientHostname != value) {
                    if (value == null) value = string.Empty;
                    this.terminalLeftClientHostname = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private VRemote4.HandlerSi.Client.Business terminalRightClient;
        private Terminal ventuzTerminalRightScene;
        public VRemote4.HandlerSi.Scene.States VentuzTerminalRightSceneStatus {
            get {
                if (this.ventuzTerminalRightScene is Terminal) return this.ventuzTerminalRightScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }
        private string terminalRightClientHostname = string.Empty;
        public string TerminalRightClientHostname {
            get { return this.terminalRightClientHostname; }
            set {
                if (this.terminalRightClientHostname != value) {
                    if (value == null) value = string.Empty;
                    this.terminalRightClientHostname = value;
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

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.CashRegister'", typeIdentifier);
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

            this.localVentuzHandler = new VRemote4.HandlerSi.Business(syncContext);
            this.localVentuzHandler.Error += this.localVentuzHandler_Error;

            if (this.localVentuzHandler.TryAddClient("Displays", false, out this.displayClient)) {
                this.displayClient.HostnameChanged += this.displayClient_HostnameChanged;
                this.displayClient.StatusChanged += this.displayClient_StatusChanged;
                this.ventuzDisplayScene = new Displays(syncContext, this.displayClient, 0);
                this.ventuzDisplayScene.StatusChanged += this.ventuzDisplayScene_StatusChanged;
            }

            if (this.localVentuzHandler.TryAddClient("Left Terminal", false, out this.terminalLeftClient)) {
                this.terminalLeftClient.HostnameChanged += this.terminalLeftClient_HostnameChanged;
                this.terminalLeftClient.StatusChanged += this.terminalLeftClient_StatusChanged;
                this.ventuzTerminalLeftScene = new Terminal(syncContext, this.terminalLeftClient, 0);
                this.ventuzTerminalLeftScene.StatusChanged += this.ventuzTerminalLeftScene_StatusChanged;
                this.ventuzTerminalLeftScene.PropertyChanged += this.ventuzTerminalLeftScene_PropertyChanged;
                this.ventuzTerminalLeftScene.OKButtonPressed += this.ventuzTerminalLeftScene_OKButtonPressed;
            }

            if (this.localVentuzHandler.TryAddClient("Right Terminal", false, out this.terminalRightClient)) {
                this.terminalRightClient.HostnameChanged += this.terminalRightClient_HostnameChanged;
                this.terminalRightClient.StatusChanged += this.terminalRightClient_StatusChanged;
                this.ventuzTerminalRightScene = new Terminal(syncContext, this.terminalRightClient, 0);
                this.ventuzTerminalRightScene.StatusChanged += this.ventuzTerminalRightScene_StatusChanged;
                this.ventuzTerminalRightScene.PropertyChanged += this.ventuzTerminalRightScene_PropertyChanged;
                this.ventuzTerminalRightScene.OKButtonPressed += this.ventuzTerminalRightScene_OKButtonPressed;
            }

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this, this.localVentuzHandler);
        }

        public override void Dispose() {
            base.Dispose();

            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;

            this.displayClient.Shutdown();
            if (this.displayClient is VRemote4.HandlerSi.Client.Business) {
                this.displayClient.HostnameChanged -= this.displayClient_HostnameChanged;
                this.displayClient.StatusChanged -= this.displayClient_StatusChanged;
                this.displayClient.Shutdown();
            }
            if (this.ventuzDisplayScene is VRemote4.HandlerSi.Scene) this.ventuzDisplayScene.StatusChanged -= this.ventuzDisplayScene_StatusChanged;

            this.terminalLeftClient.Shutdown();
            if (this.terminalLeftClient is VRemote4.HandlerSi.Client.Business) {
                this.terminalLeftClient.HostnameChanged -= this.terminalLeftClient_HostnameChanged;
                this.terminalLeftClient.StatusChanged -= this.terminalLeftClient_StatusChanged;
                this.terminalLeftClient.Shutdown();
            }
            if (this.ventuzTerminalLeftScene is VRemote4.HandlerSi.Scene) {
                this.ventuzTerminalLeftScene.StatusChanged -= this.ventuzTerminalLeftScene_StatusChanged;
                this.ventuzTerminalLeftScene.PropertyChanged -= this.ventuzTerminalLeftScene_PropertyChanged;
                this.ventuzTerminalLeftScene.OKButtonPressed -= this.ventuzTerminalLeftScene_OKButtonPressed;
            }

            this.terminalRightClient.Shutdown();
            if (this.terminalRightClient is VRemote4.HandlerSi.Client.Business) {
                this.terminalRightClient.HostnameChanged -= this.terminalRightClient_HostnameChanged;
                this.terminalRightClient.StatusChanged -= this.terminalRightClient_StatusChanged;
                this.terminalRightClient.Shutdown();
            }
            if (this.ventuzTerminalRightScene is VRemote4.HandlerSi.Scene) {
                this.ventuzTerminalRightScene.StatusChanged -= this.ventuzTerminalRightScene_StatusChanged;
                this.ventuzTerminalRightScene.PropertyChanged -= this.ventuzTerminalRightScene_PropertyChanged;
                this.ventuzTerminalRightScene.OKButtonPressed -= this.ventuzTerminalRightScene_OKButtonPressed;
            }

            this.localVentuzHandler.Dispose();

        }

        public override void ResetData() {
            base.ResetData();
            this.TaskCounter = 0;
            this.SelectDataset(0);
            this.LeftPlayerInput = 0;
            this.RightPlayerInput = 0;
        }

        internal void Resolve() {
            if (this.SelectedDataset is DatasetContent) {
                int value = (int)(Convert.ToDouble(this.SelectedDataset.Name) * 100);
                int leftInput = (int)(this.LeftPlayerInput * 100);
                int righInput = (int)(this.RightPlayerInput * 100);
                if (leftInput > 0) {
                    if (leftInput == value) this.LeftPlayerScore++;
                    else this.RightPlayerScore++;
                }
                else if (righInput > 0) {
                    if (righInput == value) this.RightPlayerScore++;
                    else this.LeftPlayerScore++;
                }
            }
        }

        public override void Next() {
            base.Next();
            this.TaskCounter++;
            this.SelectDataset(this.SelectedDatasetIndex + 1);
            this.LeftPlayerInput = 0;
            this.RightPlayerInput = 0;
        }

        private void parseInput(
            double leftPlayerInput,
            double rightPlayerInput) {
            if (leftPlayerInput > 0 ||
                rightPlayerInput > 0) {
                this.Vleftterminal_Lock();
                this.Vrightterminal_Lock();
                if (this.LeftPlayerInput == 0 && this.RightPlayerInput == 0) this.LeftPlayerInput = leftPlayerInput;
                if (this.LeftPlayerInput == 0 && this.RightPlayerInput == 0) this.RightPlayerInput = rightPlayerInput;
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
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }
        public void Vinsert_ContentIn() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                this.insertScene.ResetSolution();
                this.insertScene.SetPictureFilename(this.SelectedDataset.PictureFilename);
                this.insertScene.SetSolutionFilename(this.SelectedDataset.SolutionFilename);
                this.insertScene.ToIn();
            }
        }
        public void Vinsert_ShowSolution() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.SolutionToIn(); }
        public void Vinsert_ContentOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.ToOut(); }
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

        public void Vdisplays_Start() { this.displayClient.Start(this.DisplayClientHostname); }
        public void Vdisplays_Init() {
            if (this.VentuzDisplaySceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.ventuzDisplayScene.SetLeftDisplayValue(0);
                this.ventuzDisplayScene.ShowLeftDisplayLogo();
                this.ventuzDisplayScene.SetRightDisplayValue(0);
                this.ventuzDisplayScene.ShowRightDisplayLogo();
            }
        }
        public void Vdisplays_HideLogo() {
            if (this.VentuzDisplaySceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.ventuzDisplayScene.HideLeftDisplayLogo();
                this.ventuzDisplayScene.HideRightDisplayLogo();
            }
        }
        internal void Vdisplays_ShowInput() {
            this.Vdisplays_HideLogo();
            if (this.VentuzDisplaySceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.ventuzDisplayScene.SetLeftDisplayValue(this.LeftPlayerInput);
                this.ventuzDisplayScene.SetRightDisplayValue(this.RightPlayerInput);
            }
        }
        internal void Vdisplays_ClearInput() {
            this.Vdisplays_HideLogo();
            if (this.VentuzDisplaySceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.ventuzDisplayScene.SetLeftDisplayValue(0);
                this.ventuzDisplayScene.SetRightDisplayValue(0);
            }
        }
        public void Vdisplays_ShutDown() { this.displayClient.Shutdown(); }

        public void Vleftterminal_Start() { this.terminalLeftClient.Start(this.TerminalLeftClientHostname); }
        public void Vleftterminal_Init() {
            if (this.VentuzTerminalLeftSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.ventuzTerminalLeftScene.Reset();
                this.ventuzTerminalLeftScene.Lock();
            }
        }
        internal void Vleftterminal_Unlock() {
            if (this.VentuzTerminalLeftSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzTerminalLeftScene.Unlock();
        }
        internal void Vleftterminal_Lock() {
            if (this.VentuzTerminalLeftSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzTerminalLeftScene.Lock();
        }
        internal void Vleftterminal_Reset() {
            if (this.VentuzTerminalLeftSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzTerminalLeftScene.Reset();
        }
        public void Vleftterminal_ShutDown() { this.terminalLeftClient.Shutdown(); }

        public void Vrightterminal_Start() { this.terminalRightClient.Start(this.TerminalRightClientHostname); }
        public void Vrightterminal_Init() {
            if (this.VentuzTerminalRightSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.ventuzTerminalRightScene.Reset();
                this.ventuzTerminalRightScene.Lock();
            }
        }
        internal void Vrightterminal_Unlock() {
            if (this.VentuzTerminalRightSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzTerminalRightScene.Unlock();
        }
        internal void Vrightterminal_Lock() {
            if (this.VentuzTerminalRightSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzTerminalRightScene.Lock();
        }
        internal void Vrightterminal_Reset() {
            if (this.VentuzTerminalRightSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzTerminalRightScene.Reset();
        }
        public void Vrightterminal_ShutDown() { this.terminalRightClient.Shutdown(); }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
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

        void localVentuzHandler_Error(object sender, System.IO.ErrorEventArgs e) {
            Exception exc = e.GetException();
            this.on_Error(sender, new Messaging.ErrorEventArgs(this, sender.ToString(), DateTime.Now, exc.ToString()));
        }

        void displayClient_HostnameChanged(object sender, VRemote4.HandlerSi.Client.HostnameArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_displayClient_HostnameChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_displayClient_HostnameChanged(object content) {
            VRemote4.HandlerSi.Client.HostnameArgs e = content as VRemote4.HandlerSi.Client.HostnameArgs;
            if (e is VRemote4.HandlerSi.Client.HostnameArgs) this.DisplayClientHostname = e.Name;
        }

        void displayClient_StatusChanged(object sender, VRemote4.HandlerSi.Client.ClientStateArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_displayClient_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_displayClient_StatusChanged(object content) {
            VRemote4.HandlerSi.Client.ClientStateArgs e = content as VRemote4.HandlerSi.Client.ClientStateArgs;
            if (e is VRemote4.HandlerSi.Client.ClientStateArgs &&
                e.Status.HasValue &&
                e.Status.Value == VRemote4.HandlerSi.Client.ClientStates.Active &&
                this.ventuzDisplayScene is VRemote4.HandlerSi.Scene) this.ventuzDisplayScene.Load();
        }

        void ventuzDisplayScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzDisplayScene_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzDisplayScene_StatusChanged(object content) {
            VRemote4.HandlerSi.Scene.StatusArgs e = content as VRemote4.HandlerSi.Scene.StatusArgs;
            if (e is VRemote4.HandlerSi.Scene.StatusArgs) {
                if (e.Status == VRemote4.HandlerSi.Scene.States.Unloaded) this.displayClient.Shutdown();
                else if (e.Status == VRemote4.HandlerSi.Scene.States.Available) this.Vdisplays_Init();
            }
            this.on_PropertyChanged("VentuzDisplaySceneStatus");
        }

        void terminalLeftClient_HostnameChanged(object sender, VRemote4.HandlerSi.Client.HostnameArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_terminalLeftClient_HostnameChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_terminalLeftClient_HostnameChanged(object content) {
            VRemote4.HandlerSi.Client.HostnameArgs e = content as VRemote4.HandlerSi.Client.HostnameArgs;
            if (e is VRemote4.HandlerSi.Client.HostnameArgs) this.TerminalLeftClientHostname = e.Name;
        }

        void terminalLeftClient_StatusChanged(object sender, VRemote4.HandlerSi.Client.ClientStateArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_terminalLeftClient_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_terminalLeftClient_StatusChanged(object content) {
            VRemote4.HandlerSi.Client.ClientStateArgs e = content as VRemote4.HandlerSi.Client.ClientStateArgs;
            if (e is VRemote4.HandlerSi.Client.ClientStateArgs &&
                e.Status.HasValue &&
                e.Status.Value == VRemote4.HandlerSi.Client.ClientStates.Active &&
                this.ventuzTerminalLeftScene is VRemote4.HandlerSi.Scene) this.ventuzTerminalLeftScene.Load();
        }

        void ventuzTerminalLeftScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzTerminalLeftScene_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzTerminalLeftScene_StatusChanged(object content) {
            VRemote4.HandlerSi.Scene.StatusArgs e = content as VRemote4.HandlerSi.Scene.StatusArgs;
            if (e is VRemote4.HandlerSi.Scene.StatusArgs) {
                if (e.Status == VRemote4.HandlerSi.Scene.States.Unloaded) this.terminalLeftClient.Shutdown();
                else if (e.Status == VRemote4.HandlerSi.Scene.States.Available) this.Vleftterminal_Init();
            }
            this.on_PropertyChanged("VentuzTerminalLeftSceneStatus");
        }

        void ventuzTerminalLeftScene_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzTerminalLeftScene_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzTerminalLeftScene_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "Selection") {
                    //this.LeftTeamInput = this.ventuzTerminalLeftScene.Controller.Selection;
                }
            }
        }

        void ventuzTerminalLeftScene_OKButtonPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzTerminalLeftScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzTerminalLeftScene_OKButtonPressed(object content) {
            this.parseInput(this.ventuzTerminalLeftScene.Value, 0);
        }


        void terminalRightClient_HostnameChanged(object sender, VRemote4.HandlerSi.Client.HostnameArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_terminalRightClient_HostnameChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_terminalRightClient_HostnameChanged(object content) {
            VRemote4.HandlerSi.Client.HostnameArgs e = content as VRemote4.HandlerSi.Client.HostnameArgs;
            if (e is VRemote4.HandlerSi.Client.HostnameArgs) this.TerminalRightClientHostname = e.Name;
        }

        void terminalRightClient_StatusChanged(object sender, VRemote4.HandlerSi.Client.ClientStateArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_terminalRightClient_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_terminalRightClient_StatusChanged(object content) {
            VRemote4.HandlerSi.Client.ClientStateArgs e = content as VRemote4.HandlerSi.Client.ClientStateArgs;
            if (e is VRemote4.HandlerSi.Client.ClientStateArgs &&
                e.Status.HasValue &&
                e.Status.Value == VRemote4.HandlerSi.Client.ClientStates.Active &&
                this.ventuzTerminalRightScene is VRemote4.HandlerSi.Scene) this.ventuzTerminalRightScene.Load();
        }

        void ventuzTerminalRightScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzTerminalRightScene_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzTerminalRightScene_StatusChanged(object content) {
            VRemote4.HandlerSi.Scene.StatusArgs e = content as VRemote4.HandlerSi.Scene.StatusArgs;
            if (e is VRemote4.HandlerSi.Scene.StatusArgs) {
                if (e.Status == VRemote4.HandlerSi.Scene.States.Unloaded) this.terminalRightClient.Shutdown();
                else if (e.Status == VRemote4.HandlerSi.Scene.States.Available) this.Vrightterminal_Init();
            }
            this.on_PropertyChanged("VentuzTerminalRightSceneStatus");
        }

        void ventuzTerminalRightScene_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzTerminalRightScene_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzTerminalRightScene_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                //if (e.PropertyName == "Selection") { this.RightTeamInput = this.ventuzTerminalRightScene.Controller.Selection; }
            }
        }

        void ventuzTerminalRightScene_OKButtonPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzTerminalRightScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzTerminalRightScene_OKButtonPressed(object content) {
            this.parseInput(0, this.ventuzTerminalRightScene.Value);
        }

        #endregion


    }
}
