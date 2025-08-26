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

using Cliparts.SchlagDenStarLive.MainApp.Settings;
using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TargetValueCounterScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TargetValueCounterScore {

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetContent : Cliparts.Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private int targetValue = 0;
        public int TargetValue {
            get { return this.targetValue; }
            set {
                if (this.targetValue != value) {
                    this.targetValue = value;
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContent() { }
        public DatasetContent(
            int targetValue) {
            this.TargetValue = targetValue;
            this.buildToString();
        }

        private void buildToString() { this.toString = this.TargetValue.ToString(); }

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

        private int leftPlayerCounter = 0;
        public int LeftPlayerCounter {
            get { return this.leftPlayerCounter; }
            set {
                if (this.leftPlayerCounter != value) {
                    this.leftPlayerCounter = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int leftPlayerValue = 0;
        public int LeftPlayerValue {
            get { return this.leftPlayerValue; }
            set {
                if (this.leftPlayerValue != value) {
                    this.leftPlayerValue = value;
                    this.on_PropertyChanged();
                    this.calcOffsets();
                }
            }
        }

        private int leftPlayerOffset = 0;
        public int LeftPlayerOffset {
            get { return this.leftPlayerOffset; }
            private set {
                if (this.leftPlayerOffset != value) {
                    this.leftPlayerOffset = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerCounter = 0;
        public int RightPlayerCounter {
            get { return this.rightPlayerCounter; }
            set {
                if (this.rightPlayerCounter != value) {
                    this.rightPlayerCounter = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerValue = 0;
        public int RightPlayerValue {
            get { return this.rightPlayerValue; }
            set {
                if (this.rightPlayerValue != value) {
                    this.rightPlayerValue = value;
                    this.on_PropertyChanged();
                    this.calcOffsets();
                }
            }
        }

        private int rightPlayerOffset = 0;
        public int RightPlayerOffset {
            get { return this.rightPlayerOffset; }
            private set {
                if (this.rightPlayerOffset != value) {
                    this.rightPlayerOffset = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int targetValueCounterPositionX = 0;
        public int TargetValueCounterPositionX {
            get { return this.targetValueCounterPositionX; }
            set {
                if (this.targetValueCounterPositionX != value) {
                    this.targetValueCounterPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTargetValueCounter();
                }
            }
        }

        private int targetValueCounterPositionY = 0;
        public int TargetValueCounterPositionY {
            get { return this.targetValueCounterPositionY; }
            set {
                if (this.targetValueCounterPositionY != value) {
                    this.targetValueCounterPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTargetValueCounter();
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
                    foreach (DatasetContent item in value)
                        this.tryAddDataset(item, -1);
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

        private Insert insertScene;
        public override VRemote4.HandlerSi.Scene.States InsertSceneStatus {
            get {
                if (this.insertScene is Insert) return this.insertScene.Status;
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.TargetValueCounterScore'", typeIdentifier);
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
        }

        public override void Dispose() {
            base.Dispose();

            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
        }

        public override void Init() {
            base.Init();
            this.SelectDataset(0);
            this.LeftPlayerCounter = 0;
            this.RightPlayerCounter = 0;
            if (this.SelectedDataset is DatasetContent) {
                this.LeftPlayerValue = this.SelectedDataset.TargetValue;
                this.RightPlayerValue = this.SelectedDataset.TargetValue;
            }
        }

        public void SubtractValueToSelectedPlayer(
            int value) {
            if (value >= 0 &&
                this.SelectedDataset is DatasetContent) {
                switch (this.SelectedPlayer) {
                    case PlayerSelection.LeftPlayer:
                        if (this.LeftPlayerValue >= value) this.LeftPlayerValue -= value;
                        this.LeftPlayerCounter++;
                        break;
                    case PlayerSelection.RightPlayer:
                        if (this.RightPlayerValue >= value) this.RightPlayerValue -= value;
                        this.RightPlayerCounter++;
                        break;
                }
                this.Vinsert_SetTargetValueCounter();
            }
        }

        public override void Next() {
            this.SelectDataset(this.SelectedDatasetIndex + 1);
            this.LeftPlayerCounter = 0;
            this.RightPlayerCounter = 0;
            if (this.SelectedDataset is DatasetContent) {
                this.LeftPlayerValue = this.SelectedDataset.TargetValue;
                this.RightPlayerValue = this.SelectedDataset.TargetValue;
            }
        }

        private void calcOffsets() {
            if (this.SelectedDataset is DatasetContent) {
                this.LeftPlayerOffset = this.SelectedDataset.TargetValue - this.LeftPlayerValue;
                this.RightPlayerOffset = this.SelectedDataset.TargetValue - this.RightPlayerValue;
            }
            else {
                this.LeftPlayerOffset = 0;
                this.RightPlayerOffset = 0;
            }
        }

        public DatasetContent GetDataset(
            int index) {
            if (index >= 0 &&
                index < this.dataList.Count)
                return this.dataList[index];
            else
                return null;
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
            if (index < 0)
                index = 0;
            if (index >= this.dataList.Count)
                index = this.dataList.Count - 1;
            this.SelectedDatasetIndex = index;
            this.on_PropertyChanged("SelectedDatasetIndex");
            this.SelectedDataset = this.GetDataset(index);
            this.on_PropertyChanged("SelectedDataset");
            this.calcOffsets();
        }

        public void AddDataset(
            DatasetContent newDataset,
            int insertIndex) {
            if (this.tryAddDataset(newDataset, insertIndex))
                this.on_PropertyChanged("NameList");
            if (this.SelectedDataset == null)
                this.SelectDataset(0);
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
            else
                return false;
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
            else
                return false;
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
            else
                return false;
        }

        public void ResortAllDatasets() {
            if (this.DatasetsCount > 1) {
                List<DatasetContent> dataList = new List<DatasetContent>();
                for (int i = this.dataList.Count - 1; i >= 0; i--) dataList.Add(this.dataList[i]);
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
            foreach (DatasetContent item in datasetList)
                datasetRemoved = this.removeDataset(item) | datasetRemoved;
            return datasetRemoved;
        }
        public bool TryRemoveDataset(
            int index) {
            if (this.removeDataset(this.GetDataset(index))) {
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                if (index <= this.SelectedDatasetIndex)
                    this.SelectDataset(this.SelectedDatasetIndex);
                return true;
            }
            else
                return false;
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
            else
                return false;
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
                    using (StreamReader reader = new StreamReader(filename))
                        data = (Data)serializer.Deserialize(reader);
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
                using (StreamWriter writer = new StreamWriter(filename))
                    serializer.Serialize(writer, data);
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
        public void Vinsert_TargetValueCounterIn() {
            this.Vinsert_SetTargetValueCounter();
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.ToIn();
        }
        public void Vinsert_SetTargetValueCounter() { this.Vinsert_SetTargetValueCounter(this.insertScene); }
        public void Vinsert_SetTargetValueCounter(Insert scene) {
            if (this.SelectedDataset is DatasetContent) this.Vinsert_SetTargetValueCounter(this.insertScene, this.SelectedDataset.TargetValue, this.LeftPlayerCounter, this.LeftPlayerValue, this.RightPlayerCounter, this.RightPlayerValue); 
        }
        public void Vinsert_SetTargetValueCounter(
            Insert scene,
            int targetValue,
            int leftPlayerCounter,
            int leftPlayerValue,
            int rightPlayerCounter,
            int rightPlayerValue) {
            if (scene is Insert) {
                scene.SetPositionX(this.TargetValueCounterPositionX);
                scene.SetPositionY(this.TargetValueCounterPositionY);
                scene.SetTarget(targetValue);
                scene.SetTopName(this.LeftPlayerName);
                scene.SetTopCounter(leftPlayerCounter);
                scene.SetTopValue(leftPlayerValue);
                scene.SetBottomName(this.RightPlayerName);
                scene.SetBottomCounter(rightPlayerCounter);
                scene.SetBottomValue(rightPlayerValue);
            }
        }
        public void Vinsert_TargetValueCounterOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.ToOut(); }
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

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming

        void dataset_Error(object sender, Messaging.ErrorEventArgs e) { this.on_Error(sender, e); }

        void dataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_dataset_PropertyChanged);
            if (this.syncContext != null)
                this.syncContext.Post(callback, e);
        }
        private void sync_dataset_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "TargetValue") {
                    this.buildNameList();
                    this.on_PropertyChanged("NameList");
                    this.calcOffsets();
                }
            }
            this.Save();
        }

        #endregion

    }
}
