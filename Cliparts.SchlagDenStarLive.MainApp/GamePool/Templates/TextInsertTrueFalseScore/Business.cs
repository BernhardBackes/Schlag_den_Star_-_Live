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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TextInsertTrueFalseScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TextInsertTrueFalseScore {

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetContent : Cliparts.Messaging.Message, INotifyPropertyChanged {

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
        public string HostText {
            get { return this.hostText; }
            set {
                if (this.hostText != value) {
                    if (value == null) value = string.Empty;
                    this.hostText = Regex.Replace(value, "(?<!\r)\n", "\r\n");
                    this.on_PropertyChanged();
                }
            }
        }

        private string playerText = string.Empty;
        public string PlayerText {
            get { return this.playerText; }
            set {
                if (this.playerText != value) {
                    if (value == null) value = string.Empty;
                    this.playerText = Regex.Replace(value, "(?<!\r)\n", "\r\n");
                    this.on_PropertyChanged();
                }
            }
        }

        private bool taskIsTrue = true;
        public bool TaskIsTrue {
            get { return taskIsTrue; }
            set {
                if (this.taskIsTrue != value) {
                    this.taskIsTrue = value;
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

        private int timeoutPositionX = 0;
        public int TimeoutPositionX {
            get { return this.timeoutPositionX; }
            set {
                if (this.timeoutPositionX != value) {
                    this.timeoutPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeout();
                }
            }
        }

        private int timeoutPositionY = 0;
        public int TimeoutPositionY {
            get { return this.timeoutPositionY; }
            set {
                if (this.timeoutPositionY != value) {
                    this.timeoutPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeout();
                }
            }
        }

        private bool timeoutIsVisible;
        public bool TimeoutIsVisible {
            get { return this.timeoutIsVisible; }
            set {
                if (this.timeoutIsVisible != value) {
                    this.timeoutIsVisible = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeout();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.Timeout.Duration timeoutDuration = VentuzScenes.GamePool._Modules.Timeout.Duration.FiveSeconds;
        public VentuzScenes.GamePool._Modules.Timeout.Duration TimeoutDuration {
            get { return this.timeoutDuration; }
            set {
                if (this.timeoutDuration != value) {
                    this.timeoutDuration = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int textInsertPositionX = 0;
        public int TextInsertPositionX {
            get { return this.textInsertPositionX; }
            set {
                if (this.textInsertPositionX != value) {
                    this.textInsertPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetContent();
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
                    this.Vinsert_SetContent();
                }
            }
        }

        private SelectionValues leftPlayerSelection = SelectionValues.NotAvailable;
        public SelectionValues LeftPlayerSelection {
            get { return this.leftPlayerSelection; }
            set {
                if (this.leftPlayerSelection != value) {
                    this.leftPlayerSelection = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private SelectionValues rightPlayerSelection = SelectionValues.NotAvailable;
        public SelectionValues RightPlayerSelection {
            get { return this.rightPlayerSelection; }
            set {
                if (this.rightPlayerSelection != value) {
                    this.rightPlayerSelection = value;
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

        private bool repressPropertyChanged = false;

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.TextInsertTrueFalseScore'", typeIdentifier);
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
            this.leftPlayerScene.PropertyChanged += this.leftPlayerScene_PropertyChanged;

            this.rightPlayerScene = new Player(syncContext, rightPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.rightPlayerScene.StatusChanged += this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.PropertyChanged += this.rightPlayerScene_PropertyChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();

            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Dispose();
            this.hostScene.StatusChanged -= this.hostScene_StatusChanged;
            this.hostScene.Dispose();
            this.leftPlayerScene.StatusChanged -= this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.PropertyChanged -= this.leftPlayerScene_PropertyChanged;
            this.leftPlayerScene.Dispose();
            this.rightPlayerScene.StatusChanged -= this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.PropertyChanged -= this.rightPlayerScene_PropertyChanged;
            this.rightPlayerScene.Dispose();
        }

        public override void ResetData() {
            base.ResetData();
            this.TaskCounter = 0;
            this.SelectDataset(0);
            this.resetInput();
        }

        public void Resolve() {
            if (this.SelectedDataset is DatasetContent) {
                if (this.LeftPlayerSelection == SelectionValues.NotAvailable ||
                    (this.LeftPlayerSelection == SelectionValues.True && !this.SelectedDataset.TaskIsTrue) ||
                    (this.LeftPlayerSelection == SelectionValues.False && this.SelectedDataset.TaskIsTrue)) this.LeftPlayerScore++;
                if (this.RightPlayerSelection == SelectionValues.NotAvailable ||
                    (this.RightPlayerSelection == SelectionValues.True && !this.SelectedDataset.TaskIsTrue) ||
                    (this.RightPlayerSelection == SelectionValues.False && this.SelectedDataset.TaskIsTrue)) this.RightPlayerScore++;
            }
        }

        public override void Next() {
            base.Next();
            this.TaskCounter++;
            this.SelectDataset(this.SelectedDatasetIndex + 1);
            this.resetInput();
        }

        private void resetInput() {
            this.LeftPlayerSelection = SelectionValues.NotAvailable;
            this.RightPlayerSelection = SelectionValues.NotAvailable;
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
                    string trimLine;
                    int lineCounter = 0;
                    DatasetContent newDataset = null;
                    file = new System.IO.StreamReader(filename, Encoding.UTF7);
                    while ((line = file.ReadLine()) != null) {
                        trimLine = line.Trim();
                        if (!string.IsNullOrEmpty(trimLine)) {
                            lineCounter++;
                            if (newDataset is DatasetContent) {
                                if (lineCounter == 2) {
                                    newDataset.Text = trimLine;
                                    newDataset.HostText += "\n" + trimLine;
                                    newDataset.PlayerText += "\n" + trimLine;
                                }
                                else if (lineCounter == 3) {
                                    newDataset.HostText += "\nFALSCH: " + trimLine;
                                    newDataset.TaskIsTrue = false;
                                }
                            }
                            else {
                                newDataset = new DatasetContent();
                                newDataset.HostText = trimLine;
                                newDataset.PlayerText = trimLine;
                            }
                        }
                        else if (newDataset is DatasetContent) {
                            this.tryAddDataset(newDataset, -1);
                            newDataset = null;
                            lineCounter = 0;
                        }
                    }
                    file.Close();
                    if (newDataset is DatasetContent) this.tryAddDataset(newDataset, -1);
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
                string[] sourceArray = sourceText.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string target;
                int length;
                foreach (string item in sourceArray) {
                    length = item.Length;
                    if (length > 0) {
                        if (length == 1) target = item.ToUpper();
                        else target = item.Substring(0, 1).ToUpper() + item.Substring(1);
                        targetText += target + " ";
                    }
                }
            }
            return targetText.Trim();
        }

        public override void Vinsert_LoadScene() { this.insertScene.Load(); }
        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score, this.LeftPlayerScore, this.RightPlayerScore); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }

        public void Vinsert_SetTimeout() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetTimeout(this.insertScene.Timeout); }
        public void Vinsert_SetTimeout(
            VentuzScenes.GamePool._Modules.Timeout scene) {
            if (scene is VentuzScenes.GamePool._Modules.Timeout) {
                scene.SetPositionX(this.TimeoutPositionX);
                scene.SetPositionY(this.TimeoutPositionY);
                scene.SetIsVisible(this.TimeoutIsVisible);
            }
        }
        public void Vinsert_StartTimeout() {
            this.Vinsert_SetTimeout();
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timeout.StartCenter(this.TimeoutDuration, this.TimeoutIsVisible);
        }
        public void Vinsert_StopTimeout() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timeout.Clear(); }

        internal void Vinsert_ContentIn() {
            this.Vinsert_SetContent();
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.TextInsert.ToIn();
        }
        internal void Vinsert_SetContent() {
            if (this.insertScene is VRemote4.HandlerSi.Scene && this.SelectedDataset is DatasetContent) this.Vinsert_SetContent(this.insertScene.TextInsert, this.SelectedDataset);
        }
        internal void Vinsert_SetContent(
            VentuzScenes.GamePool._Modules.TextInsert scene,
            DatasetContent dataset) {
            if (scene is VentuzScenes.GamePool._Modules.TextInsert &&
            dataset is DatasetContent) {
                scene.SetPositionX(this.TextInsertPositionX);
                scene.SetPositionY(this.TextInsertPositionY);
                scene.SetStyle(VentuzScenes.GamePool._Modules.TextInsert.Styles.OneRow);
                scene.SetText(dataset.Text);
            }
        }
        internal void Vinsert_ContentOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.TextInsert.ToOut(); }
        public override void Vinsert_UnloadScene() { this.insertScene.Unload(); }

        public override void Vhost_LoadScene() { this.hostScene.Load(); }
        public void Vhost_SetContent() {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                this.hostScene.SetHeadline(this.SelectedDataset.HostText);
                this.hostScene.SetIsTrue(this.SelectedDataset.TaskIsTrue);
                this.hostScene.SetLeftPlayerSelection(this.LeftPlayerSelection);
                this.hostScene.SetRightPlayerSelection(this.RightPlayerSelection);
            }
        }
        public override void Vhost_UnloadScene() { this.hostScene.Unload(); }

        public override void Vleftplayer_LoadScene() { this.leftPlayerScene.Load(); }
        internal void Vleftplayer_SetContent() {
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                this.leftPlayerScene.SetHeadline(this.SelectedDataset.PlayerText);
            }
        }
        internal void Vleftplayer_EnableInput() { if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.EnableTouch(); }
        internal void Vleftplayer_DisableInput() { if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.DisableTouch(); }
        public override void Vleftplayer_UnloadScene() { this.leftPlayerScene.Unload(); }

        public override void Vrightplayer_LoadScene() { this.rightPlayerScene.Load(); }
        internal void Vrightplayer_SetContent() {
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) this.rightPlayerScene.SetHeadline(this.SelectedDataset.PlayerText);
        }
        internal void Vrightplayer_EnableInput() { if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.EnableTouch(); }
        internal void Vrightplayer_DisableInput() { if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.DisableTouch(); }
        public override void Vrightplayer_UnloadScene() { this.rightPlayerScene.Unload(); }

        internal void Vplayers_EnableInput() {
            this.Vleftplayer_EnableInput();
            this.Vrightplayer_EnableInput();
        }
        internal void Vplayers_DisableInput() {
            this.Vleftplayer_DisableInput();
            this.Vrightplayer_DisableInput();
        }
        internal void Vplayers_ShowInput() {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                switch (this.LeftPlayerSelection) {
                    case SelectionValues.True:
                        this.hostMasterScene.SetLeftDisplayToGreen();
                        break;
                    case SelectionValues.False:
                        this.hostMasterScene.SetLeftDisplayToRed();
                        break;
                    case SelectionValues.NotAvailable:
                    default:
                        this.hostMasterScene.SetLeftDisplayToNeutral();
                        break;
                }
                switch (this.RightPlayerSelection) {
                    case SelectionValues.True:
                        this.hostMasterScene.SetRightDisplayToGreen();
                        break;
                    case SelectionValues.False:
                        this.hostMasterScene.SetRightDisplayToRed();
                        break;
                    case SelectionValues.NotAvailable:
                    default:
                        this.hostMasterScene.SetRightDisplayToNeutral();
                        break;
                }
            }
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                switch (this.LeftPlayerSelection) {
                    case SelectionValues.True:
                        this.leftPlayerScene.SetSelectionTrue();
                        break;
                    case SelectionValues.False:
                        this.leftPlayerScene.SetSelectionFalse();
                        break;
                    case SelectionValues.NotAvailable:
                    default:
                        this.leftPlayerScene.ResetSelection();
                        break;
                }
            }
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                switch (this.RightPlayerSelection) {
                    case SelectionValues.True:
                        this.rightPlayerScene.SetSelectionTrue();
                        break;
                    case SelectionValues.False:
                        this.rightPlayerScene.SetSelectionFalse();
                        break;
                    case SelectionValues.NotAvailable:
                    default:
                        this.rightPlayerScene.ResetSelection();
                        break;
                }
            }
        }

        internal void Vstage_ContentIn() {
            this.Vhost_SetContent();
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ToIn();
            this.Vleftplayer_SetContent();
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.ToIn();
            this.Vrightplayer_SetContent();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.ToIn();
        }

        internal void Vstage_ContentOut() {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ToOut();
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.ToOut();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.ToOut();
            this.Vstage_Init();
        }

        public override void Vstage_Init() {
            base.Vstage_Init();
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.hostMasterScene.SetLeftDisplayToNeutral();
                this.hostMasterScene.SetRightDisplayToNeutral();
            }
            //if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.ResetSelection();
            //if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.ResetSelection();
        }

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

        void leftPlayerScene_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerScene_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerScene_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "Selection") {
                    this.LeftPlayerSelection = this.leftPlayerScene.Selection;
                    this.Vhost_SetContent();
                }
            }
        }

        void rightPlayerScene_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "Selection") {
                    this.RightPlayerSelection = this.rightPlayerScene.Selection;
                    this.Vhost_SetContent();
                }
            }
        }

        #endregion
    }
}
