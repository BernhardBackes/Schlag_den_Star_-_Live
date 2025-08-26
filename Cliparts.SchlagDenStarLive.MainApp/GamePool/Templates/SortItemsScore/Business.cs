using System;
using System.Collections;
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

using Cliparts.Serialization;

using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SortItemsScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.SortItemsScore {

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetItem : INotifyPropertyChanged {

        #region Properties

        [XmlIgnore]
        public int Index { get; private set; }

        private string text = string.Empty;
        public string Text {
            get { return this.text; }
            set {
                if (this.text != value) {
                    if (string.IsNullOrEmpty(value)) this.text = string.Empty;
                    else this.text = value;
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        private int result = 0;
        public int Result {
            get { return this.result; }
            set {
                if (value < 0) this.result = 0;
                else this.result = value;
                this.on_PropertyChanged();
            }
        }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetItem() { }
        public DatasetItem(int index) { this.Index = index; }

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

    public class DatasetContent : Messaging.Message, INotifyPropertyChanged {

        public const int DataItemsCount = 5;

        #region Properties

        private string text = string.Empty;
        public string Text {
            get { return this.text; }
            set {
                if (this.text != value) {
                    if (string.IsNullOrEmpty(value)) this.text = string.Empty;
                    else this.text = value;
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        private DatasetItem[] items = null;
        public DatasetItem[] Items {
            get {
                if (this.items == null) this.buildItemList();
                return this.items;
             }
            set {
                if (this.items == null) this.buildItemList();
                for (int i = 0; i < this.items.Length; i++) {
                    if (value is DatasetItem[] &&
                        value.Length > i &&
                        value[i] is DatasetItem) {
                        this.items[i].Text = value[i].Text;
                        this.items[i].Result = value[i].Result;
                    }
                    else {
                        this.items[i].Text = string.Empty;
                        this.items[i].Result = 0;
                    }
                }
            }
        }

        private DatasetItem[] sortedItems = null;
        public DatasetItem[] SortedItems {
            get {
                if (this.sortedItems == null) this.buildSortedItemList();
                return this.sortedItems;
            }
        }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContent() {
            this.Text = "?";
            this.buildItemList();
        }

        private void buildItemList() {
            this.items = new DatasetItem[DataItemsCount];
            for (int i = 0; i < this.items.Length; i++) {
                DatasetItem item = new DatasetItem(i);
                item.PropertyChanged += item_PropertyChanged;
                this.items[i] = item;
            }
            this.buildSortedItemList();
        }

        private void buildSortedItemList() {
            this.sortedItems = this.Items.OrderBy(item => item.Result).ToArray();
            this.on_PropertyChanged("SortedItems");
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

        private void item_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            this.on_PropertyChanged(sender, e);
            if (e.PropertyName == "Result") this.buildSortedItemList();
        }

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

        private string filename = string.Empty;
        public string Filename {
            get { return this.filename; }
            set { if (this.filename != value) this.Load(value); }
        }

        private List<DatasetContent> dataList = new List<DatasetContent>();
        [NotSerialized]
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

        public DatasetItem SelectedDatasetItem { get; private set; }

        private List<DatasetItem> visibleItems = new List<DatasetItem>();

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

        private DatasetItem[] leftPlayerInput = new DatasetItem[DatasetContent.DataItemsCount];
        public DatasetItem[] LeftPlayerInput { get { return this.leftPlayerInput; } }

        private DatasetItem[] rightPlayerInput = new DatasetItem[DatasetContent.DataItemsCount];
        public DatasetItem[] RightPlayerInput { get { return this.rightPlayerInput; } }


        private Insert insertScene;
        public override VRemote4.HandlerSi.Scene.States InsertSceneStatus {
            get {
                if (this.insertScene is Insert) return this.insertScene.Status;
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

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.SortItemsScore'", typeIdentifier);
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

            this.leftPlayerScene = new Player(syncContext, leftPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.leftPlayerScene.StatusChanged += this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.PropertyChanged += this.leftPlayerScene_PropertyChanged;
            this.leftPlayerScene.OKPressed += this.leftPlayerScene_OKPressed;

            this.rightPlayerScene = new Player(syncContext, rightPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.rightPlayerScene.StatusChanged += this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.PropertyChanged += this.rightPlayerScene_PropertyChanged;
            this.rightPlayerScene.OKPressed += this.rightPlayerScene_OKPressed;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();

            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Dispose();
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
        }

        public void Resolve() {
            if (this.TaskCounter > 0) {
                if (this.SelectedDataset is DatasetContent) {
                    for (int i = 0; i < DatasetContent.DataItemsCount; i++) {
                        if (this.LeftPlayerInput is DatasetItem[] &&
                            this.LeftPlayerInput.Length > i &&
                            this.LeftPlayerInput[i] is DatasetItem &&
                            this.LeftPlayerInput[i] == this.SelectedDataset.SortedItems[i]) this.LeftPlayerScore++;

                        if (this.RightPlayerInput is DatasetItem[] &&
                            this.RightPlayerInput.Length > i &&
                            this.RightPlayerInput[i] is DatasetItem &&
                            this.RightPlayerInput[i] == this.SelectedDataset.SortedItems[i]) this.RightPlayerScore++;
                    }
                }
            }
        }

        public override void Next() {
            base.Next();
            this.TaskCounter++;
            this.SelectDataset(this.SelectedDatasetIndex + 1);
        }

        public void ResetLeftPlayerInput() { this.setLeftPlayerInput(null); }
        public void SetLeftPlayerInput(
            int index,
            int value) {
            int[] valueList = new int[DatasetContent.DataItemsCount];
            if (this.SelectedDataset is DatasetContent) {
                for (int i = 0; i < DatasetContent.DataItemsCount; i++) {
                    if (i == index) valueList[i] = value;
                    else if (this.leftPlayerInput[i] is DatasetItem) {
                        if (this.leftPlayerInput[i].Index == value - 1) valueList[i] = 0;
                        else valueList[i] = this.leftPlayerInput[i].Index + 1;
                    }
                    else valueList[i] = 0;
                }
            }
            this.setLeftPlayerInput(valueList);
        }
        private void setLeftPlayerInput(
            int[] value) {
            this.leftPlayerInput = new DatasetItem[DatasetContent.DataItemsCount];
            if (this.SelectedDataset is DatasetContent) {
                for (int i = 0; i < DatasetContent.DataItemsCount; i++) {
                    if (value is int[] &&
                        value.Length > i &&
                        value[i] > 0 &&
                        value[i] <= DatasetContent.DataItemsCount) this.leftPlayerInput[i] = this.SelectedDataset.Items[value[i] - 1];
                }
            }
            this.on_PropertyChanged("LeftPlayerInput");
        }

        public void ResetRightPlayerInput() { this.setRightPlayerInput(null); }
        public void SetRightPlayerInput(
            int index,
            int value) {
            int[] valueList = new int[DatasetContent.DataItemsCount];
            if (this.SelectedDataset is DatasetContent) {
                for (int i = 0; i < DatasetContent.DataItemsCount; i++) {
                    if (i == index) valueList[i] = value;
                    else if (this.rightPlayerInput[i] is DatasetItem) {
                        if (this.rightPlayerInput[i].Index == value - 1) valueList[i] = 0;
                        else valueList[i] = this.rightPlayerInput[i].Index + 1;
                    }
                    else valueList[i] = 0;
                }
            }
            this.setRightPlayerInput(valueList);
        }
        private void setRightPlayerInput(
            int[] value) {
            this.rightPlayerInput = new DatasetItem[DatasetContent.DataItemsCount];
            if (this.SelectedDataset is DatasetContent) {
                for (int i = 0; i < DatasetContent.DataItemsCount; i++) {
                    if (value is int[] &&
                        value.Length > i &&
                        value[i] > 0 &&
                        value[i] <= DatasetContent.DataItemsCount) this.rightPlayerInput[i] = this.SelectedDataset.Items[value[i] - 1];
                }
            }
            this.on_PropertyChanged("RightPlayerInput");
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
            this.SelectDatasetItem(0);
            this.ResetLeftPlayerInput();
            this.ResetRightPlayerInput();
            this.visibleItems.Clear();
        }

        public void SelectDatasetItem(
            int index) {
            if (this.SelectedDataset is DatasetContent) {
                if (index < 0) this.SelectedDatasetItem = this.SelectedDataset.SortedItems[0];
                else if (index >= DatasetContent.DataItemsCount) this.SelectedDatasetItem = this.SelectedDataset.SortedItems[DatasetContent.DataItemsCount - 1];
                else this.SelectedDatasetItem = this.SelectedDataset.SortedItems[index];
            }
            else this.SelectedDatasetItem = null;
            this.on_PropertyChanged("SelectedDatasetItem");
        }

        public void AddDataset(
            DatasetContent newDataset,
            int insertIndex) {
            if (this.tryAddDataset(newDataset, insertIndex)) this.on_PropertyChanged("NameList");
            if (this.SelectedDataset == null) this.SelectDataset(0);
            this.Save();
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
                this.Save();
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
                this.Save();
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
                this.Save();
            }
        }

        public void RemoveAllDatasets() {
            if (this.tryRemoveAllDatasets()) {
                this.SelectDataset(0);
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                this.Save();
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
                this.Save();
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
            foreach (DatasetContent item in this.dataList) {
                this.names.Add(item.ToString());
            }
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

        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.Vinsert_SetScore(this.insertScene.Score, this.LeftPlayerScore, this.RightPlayerScore);
                this.Vinsert_SetGame(this.insertScene.Game, this.LeftPlayerScore, this.RightPlayerScore);
            }
        }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }

        public void Vinsert_GameIn() {
            this.Vinsert_SetGame();
            if (this.insertScene is Insert &&
                this.insertScene.Game is Game) {
                this.insertScene.Game.ToIn();
            }
            if (this.TaskCounter > 0 &&
                this.TaskCounter <= this.TaskCounterSize) this.Vinsert_TaskCounterIn();
        }
        public void Vinsert_SetGame() { if (this.insertScene is Insert) this.Vinsert_SetGame(this.insertScene.Game, this.SelectedDataset, this.LeftPlayerInput, this.RightPlayerInput); }
        public void Vinsert_SetGame(
            Game scene,
            DatasetContent dataset,
            DatasetItem[] leftPlayerInput,
            DatasetItem[] rightPlayerInput) {
            if (scene is Game) {
                scene.SetPositionX(this.GamePositionX);
                scene.SetPositionY(this.GamePositionY);
                scene.SetLeftPlayerScore(this.LeftPlayerScore);
                scene.SetRightPlayerScore(this.RightPlayerScore);

                scene.SetLeftPlayerName(LeftPlayerName);
                scene.SetRightPlayerName(RightPlayerName);

                int counter = 0;
                for (int i = 0; i < DatasetContent.DataItemsCount; i++) {
                    if (leftPlayerInput is DatasetItem[] &&
                        leftPlayerInput.Length > i &&
                        leftPlayerInput[i] is DatasetItem) scene.SetLeftPlayerItemText(i + 1, leftPlayerInput[i].Text);
                    else scene.SetLeftPlayerItemText(i + 1, string.Empty);
                    scene.SetLeftPlayerItemColor(i + 1, Game.ItemColor.Yellow);

                    if (rightPlayerInput is DatasetItem[] &&
                        rightPlayerInput.Length > i &&
                        rightPlayerInput[i] is DatasetItem) scene.SetRightPlayerItemText(i + 1, rightPlayerInput[i].Text);
                    else scene.SetRightPlayerItemText(i + 1, string.Empty);
                    scene.SetRightPlayerItemColor(i + 1, Game.ItemColor.Yellow);

                    //if (dataset is DatasetContent) {
                    //    scene.SetSolutionItemText(i + 1, dataset.SortedItems[i].Text);
                    //    scene.SetSolutionItemResult(i + 1, dataset.SortedItems[i].Result.ToString()); //+ "g");
                    //    if (this.visibleItems.Contains(dataset.SortedItems[i])) {
                    //        counter++;
                    //        scene.SetSolutionItemIn(i + 1);
                    //    }
                    //    else scene.SetSolutionItemOut(i + 1);
                    //    scene.SetSolutionItemID(i + 1, counter);
                    //}
                    //else {
                    //    scene.SetSolutionItemOut(i + 1);
                    //    scene.SetSolutionItemID(i + 1, 0);
                    //    scene.SetSolutionItemText(i + 1, string.Empty);
                    //    scene.SetSolutionItemResult(i + 1, string.Empty);
                    //}
                }
            }
            insert_SetVisibleResultDatasetItems(visibleItems.OrderBy(x => x.Result).ToList(), scene, false);
        }
        public void Vinsert_SetGame(
            Game scene,
            int leftPlayerScore,
            int rightPlayerScore) {
            if (scene is Game) {
                scene.SetPositionX(this.GamePositionX);
                scene.SetPositionY(this.GamePositionY);
                scene.SetLeftPlayerScore(leftPlayerScore);
                scene.SetRightPlayerScore(rightPlayerScore);
                scene.SetLeftPlayerName(LeftPlayerName);
                scene.SetRightPlayerName(RightPlayerName);
            }
        }
        internal void Vinsert_SelectedItemIn() {
            if (this.insertScene is Insert &&
                this.SelectedDataset is DatasetContent &&
                this.SelectedDatasetItem is DatasetItem &&
                !this.visibleItems.Contains(this.SelectedDatasetItem)) {
                this.visibleItems.Add(this.SelectedDatasetItem);
                var orderedVisible = visibleItems.OrderBy(x => x.Result).ToList();
                int counter = 0;
                int id = 0;
                Game scene = this.insertScene.Game;

                insert_SetVisibleResultDatasetItems(orderedVisible, scene, true);

                //for (int i = 0; i < DatasetContent.DataItemsCount; i++) {
                //    scene.SetSolutionItemResult(i + 1, this.SelectedDataset.SortedItems[i].Result.ToString());// + "g");
                //    scene.SetSolutionItemText(i + 1, this.SelectedDataset.SortedItems[i].Text);



                //    if (this.visibleItems.Contains(this.SelectedDataset.SortedItems[i])) counter++;
                //    scene.SetSolutionItemID(i + 1, counter);
                //    if (this.SelectedDataset.SortedItems[i] == this.SelectedDatasetItem) id = i + 1;

                //    if (this.visibleItems.Contains(this.SelectedDataset.SortedItems[i]))
                //        scene.SolutionItemToIn(i+1);
                //}
                //scene.SolutionItemToIn(id);
                scene.PlayJingleIn();
            }
        }

        private async Task insert_SetVisibleResultDatasetItems(List<DatasetItem> orderedVisible, Game scene, bool withAnimation) {
            var newDisplayedItemIndex = int.MaxValue;
            if (SelectedDatasetItem != null && orderedVisible.Contains(SelectedDatasetItem)){
                newDisplayedItemIndex = orderedVisible.IndexOf(SelectedDatasetItem);
            }
            if (withAnimation) {
                orderedVisible.Remove(SelectedDatasetItem);
            }
            var orderedVisibleWithNew = orderedVisible.Concat(new List<DatasetItem> { SelectedDatasetItem }).OrderBy(x => x.Result);


            for (int i = 0; i < orderedVisible.Count; i++) {
                var item = orderedVisible[i];
                scene.SetSolutionItemResult(i + 1, item.Result.ToString());// + "g");
                scene.SetSolutionItemText(i + 1, item.Text);
                scene.SetSolutionItemID(i + 1, i + 1);
                scene.SetSolutionItemIn(i + 1);
                
                if (withAnimation && i >= newDisplayedItemIndex) {
                    scene.SetSolutionItemID(i + 1, i + 2);
                }
                if (withAnimation) {
                    scene.SetSolutionDampDuration(i + 1, 0.3f);
                }
                else
                    scene.SetSolutionDampDuration(i + 1, 0);
            }
            //current
            if (withAnimation) {
                var i = orderedVisible.Count;
                var item = SelectedDatasetItem;
                scene.SetSolutionItemResult(i + 1, item.Result.ToString());// + "g");
                scene.SetSolutionItemText(i + 1, item.Text);
                scene.SetSolutionItemID(i + 1, newDisplayedItemIndex+1);
                scene.SetSolutionItemOut(i + 1);
                var delay = newDisplayedItemIndex == orderedVisible.Count ? 0 : 200;
                Task.Delay(delay).ContinueWith(a => scene.SolutionItemToIn(i + 1));
                scene.SetSolutionDampDuration(i + 1, 0);
                Task.Delay(delay + 500).ContinueWith(b => insert_SetVisibleResultDatasetItems(orderedVisibleWithNew.ToList(), scene, false));

            }
            var start = orderedVisible.Count;
            if (withAnimation) start++;
            for (var i = start; i < DatasetContent.DataItemsCount; i++) {
                scene.SetSolutionItemOut(i + 1);

                scene.SetSolutionItemID(i + 1, 0);
                scene.SetSolutionItemText(i + 1, string.Empty);
                scene.SetSolutionItemResult(i + 1, string.Empty);
                scene.SetSolutionDampDuration(i + 1, 0);
            }
        }

        internal void Vinsert_SelectedItemOut() {
            if (this.SelectedDatasetItem is DatasetItem &&
                this.visibleItems.Contains(this.SelectedDatasetItem)) this.visibleItems.Remove(this.SelectedDatasetItem);
            this.Vinsert_SetGame();
        }
        public void Vinsert_GameResolve() {
            if (this.insertScene is Insert &&
                this.SelectedDataset is DatasetContent) {
                Game scene = this.insertScene.Game;
                for (int i = 0; i < DatasetContent.DataItemsCount; i++) {
                    if (this.LeftPlayerInput is DatasetItem[] &&
                        this.LeftPlayerInput.Length > i &&
                        this.LeftPlayerInput[i] is DatasetItem &&
                        this.LeftPlayerInput[i] == this.SelectedDataset.SortedItems[i]) scene.SetLeftPlayerItemColor(i + 1, Game.ItemColor.Green);
                    else scene.SetLeftPlayerItemColor(i + 1, Game.ItemColor.Yellow);

                    if (this.RightPlayerInput is DatasetItem[] &&
                        this.RightPlayerInput.Length > i &&
                        this.RightPlayerInput[i] is DatasetItem &&
                        this.RightPlayerInput[i] == this.SelectedDataset.SortedItems[i]) scene.SetRightPlayerItemColor(i + 1, Game.ItemColor.Green);
                    else scene.SetRightPlayerItemColor(i + 1, Game.ItemColor.Yellow);
                }
                scene.PlayJingleResolve();
            }
        }
        public void Vinsert_GameOut() {
            if (this.insertScene is Insert && this.insertScene.Game is Game) this.insertScene.Game.ToOut();
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
            if (this.hostMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostMasterScene.LiveVideoIn();
        }

        public override void Vleftplayer_LoadScene() { this.leftPlayerScene.Load(); }
        internal void Vleftplayer_ContentIn() {
            this.ResetLeftPlayerInput();
            this.Vplayer_SetData(this.leftPlayerScene, this.SelectedDataset);
            this.Vplayer_In(this.leftPlayerScene);
        }
        internal void Vleftplayer_ContentOut() { this.Vplayer_Out(this.leftPlayerScene); }
        internal void Vleftplayer_ReleaseInput() { this.Vplayer_ReleaseInput(this.leftPlayerScene); }
        internal void Vleftplayer_LockInput() { this.Vplayer_LockInput(this.leftPlayerScene); }
        public override void Vleftplayer_UnloadScene() { this.leftPlayerScene.Unload(); }

        public override void Vrightplayer_LoadScene() { this.rightPlayerScene.Load(); }
        internal void Vrightplayer_ContentIn() {
            this.ResetRightPlayerInput();
            this.Vplayer_SetData(this.rightPlayerScene, this.SelectedDataset);
            this.Vplayer_In(this.rightPlayerScene);
        }
        internal void Vrightplayer_ContentOut() { this.Vplayer_Out(this.rightPlayerScene); }
        internal void Vrightplayer_ReleaseInput() { this.Vplayer_ReleaseInput(this.rightPlayerScene); }
        internal void Vrightplayer_LockInput() { this.Vplayer_LockInput(this.rightPlayerScene); }
        public override void Vrightplayer_UnloadScene() { this.rightPlayerScene.Unload(); }

        public void Vplayer_In(Player scene) { if (scene is Player) scene.ToIn(); }
        public void Vplayer_SetData(
            Player scene,
            DatasetContent dataset) {
            if (scene is Player &&
                dataset is DatasetContent) {
                for (int i = 0; i < DatasetContent.DataItemsCount; i++) {
                    if (dataset.Items[i] is DatasetItem) scene.SetItemText(i + 1, dataset.Items[i].Text);
                    else scene.SetItemText(i + 1, string.Empty);
                }
            }
        }
        public void Vplayer_ReleaseInput(Player scene) { if (scene is Player) scene.EnableTouch(); }
        public void Vplayer_LockInput(Player scene) { if (scene is Player) scene.DisableTouch(); }
        public void Vplayer_Out(Player scene) { if (scene is Player) scene.ToOut(); }

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
            }
        }

        private void leftPlayerScene_OKPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerScene_OKPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerScene_OKPressed(object content) {
            this.setLeftPlayerInput(this.leftPlayerScene.ResultArray);
        }

        void rightPlayerScene_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
            }
        }

        private void rightPlayerScene_OKPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_OKPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_OKPressed(object content) {
            this.setRightPlayerInput(this.rightPlayerScene.ResultArray);
        }

        #endregion

    }
}
