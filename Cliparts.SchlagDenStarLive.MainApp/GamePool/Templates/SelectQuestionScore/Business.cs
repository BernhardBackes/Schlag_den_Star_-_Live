using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;
using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SelectQuestionScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.SelectQuestionScore {

    public class Data {
        public DatasetContent[] DataList;
    }

    public class DatasetContent : Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private string text = string.Empty;
        public string Text {
            get { return this.text; }
            set {
                if (this.text != value) {
                    if (string.IsNullOrEmpty(value)) this.text = string.Empty;
                    else this.text = Regex.Replace(value, "(?<!\r)\n", "\r\n");
                    this.buildToString();
                    this.on_PropertyChanged();
                    if (string.IsNullOrEmpty(this.HostText)) this.HostText = Text;
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

        public enum Categories { Cat1, Cat2, Cat3, Cat4, Cat5, Cat6 }

        private Categories category = Categories.Cat1;
        public Categories Category {
            get { return this.category; }
            set {
                if (this.category != value) {
                    this.category = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContent() {
            this.Text = "?";
            this.HostText = string.Empty;
        }

        private void buildToString() { 
            this.toString = this.Text; 
        }

        public override string ToString() { return this.toString; }

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

    public class Business : _Base.BuzzerScore.Business {

        #region Properties

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

        public const int TaskCounterPenaltyCountMax = 10;

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

        private int taskCounter = 1;
        public int TaskCounter {
            get { return this.taskCounter; }
            set {
                if (this.taskCounter != value) {
                    if (value < 1) value = 1;
                    this.taskCounter = value;
                    this.on_PropertyChanged();
                }
            }
        }

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


        private PlayerSelection selectedPlayer = PlayerSelection.LeftPlayer;
        public PlayerSelection SelectedPlayer {
            get { return this.selectedPlayer; }
            set {
                if (this.selectedPlayer != value) {
                    if (value == PlayerSelection.NotSelected) value = PlayerSelection.LeftPlayer;
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
        public DatasetContent[] DataList {
            get { return this.dataList.ToArray(); }
            private set {
                this.tryRemoveAllDatasets();
                if (value is DatasetContent[]) {
                    foreach (DatasetContent item in value) this.tryAddDataset(item, -1);
                }
                this.buildNameList();
                this.on_PropertyChanged("NameList");
            }
        }

        private List<string> names = new List<string>();
        public string[] NameList { get { return this.names.ToArray(); } }

        public int DatasetsCount { get { return this.dataList.Count; } }

        public List<DatasetContent> IdleDataListCat1 { get; private set; }
        public List<DatasetContent> IdleDataListCat2 { get; private set; }
        public List<DatasetContent> IdleDataListCat3 { get; private set; }
        public List<DatasetContent> IdleDataListCat4 { get; private set; }
        public List<DatasetContent> IdleDataListCat5 { get; private set; }
        public List<DatasetContent> IdleDataListCat6 { get; private set; }

        private DatasetContent.Categories preselectedCategory;
        public DatasetContent.Categories PreselectedCategory {
            get { return this.preselectedCategory; }
            set {
                if (this.preselectedCategory != value) {
                    this.preselectedCategory = value;
                    this.on_PropertyChanged();
                    this.PreselectedDatasetIndex = 0;
                }
            }
        }

        private int idleDataListCount {
            get {
                int count = 0;
                switch (this.PreselectedCategory) {
                    case DatasetContent.Categories.Cat1:
                        count = this.IdleDataListCat1.Count;
                        break;
                    case DatasetContent.Categories.Cat2:
                        count = this.IdleDataListCat2.Count;
                        break;
                    case DatasetContent.Categories.Cat3:
                        count = this.IdleDataListCat3.Count;
                        break;
                    case DatasetContent.Categories.Cat4:
                        count = this.IdleDataListCat4.Count;
                        break;
                    case DatasetContent.Categories.Cat5:
                        count = this.IdleDataListCat5.Count;
                        break;
                    case DatasetContent.Categories.Cat6:
                        count = this.IdleDataListCat6.Count;
                        break;
                }
                return count;
            }
        }

        private int preselectedDatasetIndex = 0;
        public int PreselectedDatasetIndex {
            get { return this.preselectedDatasetIndex; }
            set {
                if (this.preselectedDatasetIndex != value) {
                    if (value < 0) this.preselectedDatasetIndex = -1;
                    else if (value >= this.idleDataListCount) value = this.idleDataListCount - 1;
                    else this.preselectedDatasetIndex = value;
                    this.on_PropertyChanged();
                }
            }
        }

        public List<DatasetContent> ActiveDataList { get; private set; }

        private int selectedDatasetID = 0;
        public int SelectedDatasetID {
            get { return this.selectedDatasetID; }
            private set {
                if (this.selectedDatasetID != value) {
                    if (value < 0) this.selectedDatasetID = 0;
                    else if (value > this.ActiveDataList.Count) value = this.ActiveDataList.Count;
                    else this.selectedDatasetID = value;
                    this.on_PropertyChanged();
                }
            }
        }

        public List<DatasetContent> UsedDataList { get; private set; }

        private int diceID = 0;

        private Insert insertScene;
        public override VRemote4.HandlerSi.Scene.States InsertSceneStatus {
            get {
                if (this.insertScene is Insert) return this.insertScene.Status;
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

        private bool repressPropertyChanged = false;

        public const int QuestionsCount = 6;

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.IdleDataListCat1 = new List<DatasetContent>();
            this.IdleDataListCat2 = new List<DatasetContent>();
            this.IdleDataListCat3 = new List<DatasetContent>();
            this.IdleDataListCat4 = new List<DatasetContent>();
            this.IdleDataListCat5 = new List<DatasetContent>();
            this.IdleDataListCat6 = new List<DatasetContent>();
            this.ActiveDataList = new List<DatasetContent>();
            this.UsedDataList = new List<DatasetContent>();

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.SelectQuestionScore'", typeIdentifier);
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

            this.hostScene = new Stage(syncContext, hostMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.hostScene.StatusChanged += this.hostScene_StatusChanged;

            this.leftPlayerScene = new Stage(syncContext, leftPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.leftPlayerScene.StatusChanged += this.leftPlayerScene_StatusChanged;

            this.rightPlayerScene = new Stage(syncContext, rightPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.rightPlayerScene.StatusChanged += this.rightPlayerScene_StatusChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();
            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.hostScene.StatusChanged -= this.hostScene_StatusChanged;
            this.hostScene.Dispose();
            this.leftPlayerScene.StatusChanged -= this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.Dispose();
            this.rightPlayerScene.StatusChanged -= this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.Dispose();
        }

        public override void ResetData() {
            base.ResetData();
            this.TaskCounter = 1;
            this.SelectedDatasetID = 0;
            this.diceID = 0;
            this.buildGameLists();
        }
        public override void Next() {
            base.Next();
            if (this.SelectedDatasetID > 0 &&
                this.SelectedDatasetID <= this.ActiveDataList.Count &&
                this.PreselectedDatasetIndex >= 0 &&
                this.PreselectedDatasetIndex < this.idleDataListCount) {
                this.UsedDataList.Add(this.ActiveDataList[this.SelectedDatasetID - 1]);
                switch (this.PreselectedCategory) {
                    case DatasetContent.Categories.Cat1:
                        this.ActiveDataList[this.SelectedDatasetID - 1] = this.IdleDataListCat1[this.PreselectedDatasetIndex];
                        this.IdleDataListCat1.RemoveAt(this.PreselectedDatasetIndex);
                        this.on_PropertyChanged("IdleDataListCat1");
                        break;
                    case DatasetContent.Categories.Cat2:
                        this.ActiveDataList[this.SelectedDatasetID - 1] = this.IdleDataListCat2[this.PreselectedDatasetIndex];
                        this.IdleDataListCat2.RemoveAt(this.PreselectedDatasetIndex);
                        this.on_PropertyChanged("IdleDataListCat2");
                        break;
                    case DatasetContent.Categories.Cat3:
                        this.ActiveDataList[this.SelectedDatasetID - 1] = this.IdleDataListCat3[this.PreselectedDatasetIndex];
                        this.IdleDataListCat3.RemoveAt(this.PreselectedDatasetIndex);
                        this.on_PropertyChanged("IdleDataListCat3");
                        break;
                    case DatasetContent.Categories.Cat4:
                        this.ActiveDataList[this.SelectedDatasetID - 1] = this.IdleDataListCat4[this.PreselectedDatasetIndex];
                        this.IdleDataListCat4.RemoveAt(this.PreselectedDatasetIndex);
                        this.on_PropertyChanged("IdleDataListCat4");
                        break;
                    case DatasetContent.Categories.Cat5:
                        this.ActiveDataList[this.SelectedDatasetID - 1] = this.IdleDataListCat5[this.PreselectedDatasetIndex];
                        this.IdleDataListCat5.RemoveAt(this.PreselectedDatasetIndex);
                        this.on_PropertyChanged("IdleDataListCat5");
                        break;
                    case DatasetContent.Categories.Cat6:
                        this.ActiveDataList[this.SelectedDatasetID - 1] = this.IdleDataListCat6[this.PreselectedDatasetIndex];
                        this.IdleDataListCat6.RemoveAt(this.PreselectedDatasetIndex);
                        this.on_PropertyChanged("IdleDataListCat6");
                        break;
                }
                this.Vinsert_NextQuestion(this.SelectedDatasetID, this.ActiveDataList[this.SelectedDatasetID - 1].Text);
                this.on_PropertyChanged("ActiveDataList");
                this.on_PropertyChanged("UsedDataList");
            }
            this.PreselectedDatasetIndex = 0;
            this.SelectedDatasetID = 0;
            if (this.diceID == 0) {
                this.TaskCounter++;
                this.Vinsert_SetTaskCounter();
                if (this.SelectedPlayer == PlayerSelection.LeftPlayer) this.SelectedPlayer = PlayerSelection.RightPlayer;
                else this.SelectedPlayer = PlayerSelection.LeftPlayer;
            }
            this.Vhost_SetGame();
            this.Vplayers_SetGame();
        }
        internal void SelectQuestion(
            int id) {
            if (this.SelectedDatasetID == 0) {
                this.SelectedDatasetID = id;
                this.Vinsert_SelectQuestion(this.SelectedDatasetID);
            }
            else {
                if (this.SelectedDatasetID == id) {
                    if (this.SelectedDatasetID > 0) this.Vinsert_QuestionIn(this.SelectedDatasetID);
                    this.SelectedDatasetID = 0;
                }
                else {
                    if (this.SelectedDatasetID > 0) this.Vinsert_QuestionIn(this.SelectedDatasetID);
                    this.SelectedDatasetID = id;
                    this.Vinsert_SelectQuestion(this.SelectedDatasetID);
                }
            }
            if (this.SelectedDatasetID > 0 &&
                this.SelectedDatasetID <= this.ActiveDataList.Count) this.PreselectedCategory = this.ActiveDataList[this.SelectedDatasetID - 1].Category;
            this.Vhost_SetGame();
            this.Vplayers_SetGame();
        }

        //internal void PlayDice() {
        //    if (this.SelectedDatasetID > 0 &&
        //        this.SelectedDatasetID <= this.ActiveDataList.Count) {
        //        this.diceID = this.SelectedDatasetID;
        //        this.UsedDataList.Add(this.ActiveDataList[this.SelectedDatasetID - 1]);
        //        this.ActiveDataList.RemoveAt(this.SelectedDatasetID - 1);
        //        this.on_PropertyChanged("ActiveDataList");
        //        this.on_PropertyChanged("UsedDataList");
        //        this.SelectedDatasetID = 0;
        //    }
        //    this.Vhost_SetGame();
        //    this.Vplayers_SetGame();
        //}

        public override void ReleaseBuzzer() {
            base.ReleaseBuzzer();
            this.Vstage_SetBuzzer(PlayerSelection.NotSelected);
        }

        public override void DoBuzzer(PlayerSelection buzzeredPlayer) {
            base.DoBuzzer(buzzeredPlayer);
            this.SelectedPlayer = this.BuzzeredPlayer;
        }

        public void True() {
            if (this.SelectedDatasetID > 0) {
                this.Vinsert_PlayJingleTrue();
                this.Vinsert_QuestionToGreen(this.SelectedDatasetID);
                switch (this.SelectedPlayer) {
                    case PlayerSelection.LeftPlayer:
                        this.LeftPlayerScore++;
                        break;
                    case PlayerSelection.RightPlayer:
                        this.RightPlayerScore++;
                        break;
                }
                this.Vstage_SetScore();
            }
        }

        public void False() {
            if (this.SelectedDatasetID > 0) {
                this.Vinsert_PlayJingleFalse();
                this.Vinsert_QuestionToRed(this.SelectedDatasetID);
                switch (this.SelectedPlayer) {
                    case PlayerSelection.LeftPlayer:
                        this.RightPlayerScore++;
                        break;
                    case PlayerSelection.RightPlayer:
                        this.LeftPlayerScore++;
                        break;
                }
                this.Vstage_SetScore();
            }
        }

        public void SetActiveQuestionUsed(
            int id) {
            if (id > 0 &&
                id <= this.ActiveDataList.Count &&
                !this.UsedDataList.Contains(this.ActiveDataList[id - 1])) {
                this.UsedDataList.Add(this.ActiveDataList[id - 1]);
                this.ActiveDataList[id - 1] = null;
                if (this.idleDataListCount > 0) {
                    int sourceIndex;
                    if (this.PreselectedDatasetIndex >= 1 &&
                        this.PreselectedDatasetIndex <= this.idleDataListCount) sourceIndex = this.PreselectedDatasetIndex;
                    else sourceIndex = 0;
                    switch (this.PreselectedCategory) {
                        case DatasetContent.Categories.Cat1:
                            this.ActiveDataList[id - 1] = this.IdleDataListCat1[sourceIndex];
                            this.IdleDataListCat1.RemoveAt(sourceIndex);
                            this.on_PropertyChanged("IdleDataListCat1");
                            break;
                        case DatasetContent.Categories.Cat2:
                            this.ActiveDataList[id - 1] = this.IdleDataListCat2[sourceIndex];
                            this.IdleDataListCat2.RemoveAt(sourceIndex);
                            this.on_PropertyChanged("IdleDataListCat2");
                            break;
                        case DatasetContent.Categories.Cat3:
                            this.ActiveDataList[id - 1] = this.IdleDataListCat3[sourceIndex];
                            this.IdleDataListCat3.RemoveAt(sourceIndex);
                            this.on_PropertyChanged("IdleDataListCat3");
                            break;
                        case DatasetContent.Categories.Cat4:
                            this.ActiveDataList[id - 1] = this.IdleDataListCat4[sourceIndex];
                            this.IdleDataListCat4.RemoveAt(sourceIndex);
                            this.on_PropertyChanged("IdleDataListCat4");
                            break;
                        case DatasetContent.Categories.Cat5:
                            this.ActiveDataList[id - 1] = this.IdleDataListCat5[sourceIndex];
                            this.IdleDataListCat5.RemoveAt(sourceIndex);
                            this.on_PropertyChanged("IdleDataListCat5");
                            break;
                        case DatasetContent.Categories.Cat6:
                            this.ActiveDataList[id - 1] = this.IdleDataListCat6[sourceIndex];
                            this.IdleDataListCat6.RemoveAt(sourceIndex);
                            this.on_PropertyChanged("IdleDataListCat6");
                            break;
                    }
                }
            }
            this.validateActiveDataList();
            this.on_PropertyChanged("ActiveDataList");
            this.on_PropertyChanged("UsedDataList");
            this.PreselectedDatasetIndex = 0;
        }

        public void SetIdleQuestionUsed(
            DatasetContent value) {
            if (value is DatasetContent) {
                if (this.IdleDataListCat1.Contains(value)) {
                    this.IdleDataListCat1.Remove(value);
                    this.on_PropertyChanged("IdleDataListCat1");
                }
                else if (this.IdleDataListCat2.Contains(value)) {
                    this.IdleDataListCat2.Remove(value);
                    this.on_PropertyChanged("IdleDataListCat2");
                }
                else if (this.IdleDataListCat3.Contains(value)) {
                    this.IdleDataListCat3.Remove(value);
                    this.on_PropertyChanged("IdleDataListCat3");
                }
                else if (this.IdleDataListCat4.Contains(value)) {
                    this.IdleDataListCat4.Remove(value);
                    this.on_PropertyChanged("IdleDataListCat4");
                }
                else if (this.IdleDataListCat5.Contains(value)) {
                    this.IdleDataListCat5.Remove(value);
                    this.on_PropertyChanged("IdleDataListCat5");
                }
                else if (this.IdleDataListCat6.Contains(value)) {
                    this.IdleDataListCat6.Remove(value);
                    this.on_PropertyChanged("IdleDataListCat6");
                }
                if (!this.UsedDataList.Contains(value)) {
                    this.UsedDataList.Add(value);
                    this.on_PropertyChanged("UsedDataList");
                }
            }
        }

        public void SetUsedQuestionIdle(
            DatasetContent value) {
            if (value is DatasetContent) {
                if (this.UsedDataList.Contains(value)) {
                    this.UsedDataList.Remove(value);
                    this.on_PropertyChanged("UsedDataList");
                }
                switch (value.Category) {
                    case DatasetContent.Categories.Cat1:
                        if (!this.IdleDataListCat1.Contains(value)) {
                            this.IdleDataListCat1.Add(value);
                            this.on_PropertyChanged("IdleDataListCat1");
                        }
                        break;
                    case DatasetContent.Categories.Cat2:
                        if (!this.IdleDataListCat2.Contains(value)) {
                            this.IdleDataListCat2.Add(value);
                            this.on_PropertyChanged("IdleDataListCat2");
                        }
                        break;
                    case DatasetContent.Categories.Cat3:
                        if (!this.IdleDataListCat3.Contains(value)) {
                            this.IdleDataListCat3.Add(value);
                            this.on_PropertyChanged("IdleDataListCat3");
                        }
                        break;
                    case DatasetContent.Categories.Cat4:
                        if (!this.IdleDataListCat4.Contains(value)) {
                            this.IdleDataListCat4.Add(value);
                            this.on_PropertyChanged("IdleDataListCat4");
                        }
                        break;
                    case DatasetContent.Categories.Cat5:
                        if (!this.IdleDataListCat5.Contains(value)) {
                            this.IdleDataListCat5.Add(value);
                            this.on_PropertyChanged("IdleDataListCat5");
                        }
                        break;
                    case DatasetContent.Categories.Cat6:
                        if (!this.IdleDataListCat6.Contains(value)) {
                            this.IdleDataListCat6.Add(value);
                            this.on_PropertyChanged("IdleDataListCat6");
                        }
                        break;
                }
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

        public void AddDataset(
            DatasetContent newDataset,
            int insertIndex) {
            if (this.tryAddDataset(newDataset, insertIndex)) {
                this.on_PropertyChanged("NameList");
                this.buildGameLists();
            }
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
                this.buildGameLists();
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
                this.buildGameLists();
                this.Save();
                return true;
            }
            else return false;
        }

        public void ResortAllDatasets() {
            if (this.DatasetsCount > 1) {
                List<DatasetContent> dataList = new List<DatasetContent>();
                for (int i = this.dataList.Count - 1; i >= 0; i--) dataList.Add(this.dataList[i]);
                this.dataList.Clear();
                foreach (DatasetContent item in dataList) this.dataList.Add(item);
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                this.buildGameLists();
                this.Save();
            }
        }

        public void RemoveAllDatasets() {
            if (this.tryRemoveAllDatasets()) {
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                this.buildGameLists();
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
                this.buildGameLists();
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
            this.repressPropertyChanged = true;
            foreach (DatasetContent item in this.dataList) {
                this.names.Add(item.ToString());
            }
            this.repressPropertyChanged = false;
        }

        private void buildGameLists() {
            this.IdleDataListCat1.Clear();
            this.IdleDataListCat2.Clear();
            this.IdleDataListCat3.Clear();
            this.IdleDataListCat4.Clear();
            this.IdleDataListCat5.Clear();
            this.IdleDataListCat6.Clear();
            this.ActiveDataList.Clear();
            this.UsedDataList.Clear();
            int size = QuestionsCount;
            foreach (DatasetContent item in this.dataList) {
                if (this.ActiveDataList.Count < size) this.ActiveDataList.Add(item);
                else {
                    switch (item.Category) {
                        case DatasetContent.Categories.Cat1:
                            this.IdleDataListCat1.Add(item);
                            break;
                        case DatasetContent.Categories.Cat2:
                            this.IdleDataListCat2.Add(item);
                            break;
                        case DatasetContent.Categories.Cat3:
                            this.IdleDataListCat3.Add(item);
                            break;
                        case DatasetContent.Categories.Cat4:
                            this.IdleDataListCat4.Add(item);
                            break;
                        case DatasetContent.Categories.Cat5:
                            this.IdleDataListCat5.Add(item);
                            break;
                        case DatasetContent.Categories.Cat6:
                            this.IdleDataListCat6.Add(item);
                            break;
                    }
                }
            }
            this.on_PropertyChanged("IdleDataListCat1");
            this.on_PropertyChanged("IdleDataListCat2");
            this.on_PropertyChanged("IdleDataListCat3");
            this.on_PropertyChanged("IdleDataListCat4");
            this.on_PropertyChanged("IdleDataListCat5");
            this.on_PropertyChanged("IdleDataListCat6");
            this.on_PropertyChanged("ActiveDataList");
            this.on_PropertyChanged("UsedDataList");
            this.PreselectedDatasetIndex = 0;
        }

        private void validateActiveDataList() {
            DatasetContent[] target = this.ActiveDataList.ToArray();
            this.ActiveDataList.Clear();
            foreach (DatasetContent item in target) this.ActiveDataList.Add(item);
            this.on_PropertyChanged("ActiveDataList");
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
                    this.filename = filename;
                    this.on_PropertyChanged("Filename");
                    this.buildGameLists();
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

        public void Vinsert_GameIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_GameIn(this.insertScene.Game); }
        public void Vinsert_GameIn(
            Game scene) {
            this.Vinsert_SetGame();
            if (scene is Game) scene.ToIn();
        }

        public void Vinsert_SetGame() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetGame(this.insertScene.Game, this.ActiveDataList.ToArray(), this.SelectedDatasetID, false); }
        public void Vinsert_SetGame(
            Game scene,
            DatasetContent[] datasetList,
            int selectedID,
            bool preview) {
            if (scene is Game) {
                scene.SetPositionX(this.GamePositionX);
                scene.SetPositionY(this.GamePositionY);
                scene.SetPreview(preview);
                for (int i = 1; i <= QuestionsCount; i++) {
                    if (datasetList is DatasetContent[] && 
                        datasetList.Length >= i) {
                        scene.SetQuestionText(i, datasetList[i - 1].Text);
                        if (selectedID == i) scene.SetQuestionSelected(i);
                        else scene.SetQuestionIn(i);
                    }
                    else {
                        scene.SetQuestionText(i, string.Empty);
                        scene.ResetQuestion(i);
                    }
                }
            }
        }

        public void Vinsert_SelectQuestion(int id) {
            if (this.diceID > 0 && id >= this.diceID) id++;
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SelectQuestion(this.insertScene.Game, id); 
        }
        public void Vinsert_SelectQuestion(
            Game scene,
            int id) {
            if (scene is Game) scene.QuestionToSelected(id);
        }

        public void Vinsert_QuestionIn(int id) {
            if (this.diceID > 0 && id >= this.diceID) id++;
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_QuestionIn(this.insertScene.Game, id); 
        }
        public void Vinsert_QuestionIn(
            Game scene,
            int id) {
            if (scene is Game) scene.QuestionToIn(id);
        }

        public void Vinsert_QuestionToGreen(int id) {
            if (this.diceID > 0 && id >= this.diceID) id++;
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_QuestionToGreen(this.insertScene.Game, id); 
        }
        public void Vinsert_QuestionToGreen(
            Game scene,
            int id) {
            if (scene is Game) scene.QuestionToGreen(id);
        }

        public void Vinsert_QuestionToRed(int id) {
            if (this.diceID > 0 && id >= this.diceID) id++;
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_QuestionToRed(this.insertScene.Game, id); 
        }
        public void Vinsert_QuestionToRed(
            Game scene,
            int id) {
            if (scene is Game) scene.QuestionToRed(id);
        }

        public void Vinsert_NextQuestion(int id, string text) {
            if (this.diceID > 0 && id >= this.diceID) id++;
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_NextQuestion(this.insertScene.Game, id, text); 
        }
        public void Vinsert_NextQuestion(
            Game scene,
            int id,
            string text) {
            if (scene is Game) scene.QuestionToNext(id, text);
        }

        public void Vinsert_QuestionOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_QuestionOut(this.insertScene.Game, this.SelectedDatasetID); }
        public void Vinsert_QuestionOut(int id) { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_QuestionOut(this.insertScene.Game, id); }
        public void Vinsert_QuestionOut(
            Game scene,
            int id) {
            if (scene is Game) scene.QuestionToOut(id);
        }

        public void Vinsert_GameOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_GameOut(this.insertScene.Game); }
        public void Vinsert_GameOut(Game scene) { if (scene is Game) { scene.ToOut(); } }

        public void Vinsert_PlayJingleFalse() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Game.PlayJingleFalse(); }
        public void Vinsert_PlayJingleTrue() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Game.PlayJingleTrue(); }

        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score, this.LeftPlayerScore, this.RightPlayerScore); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }

        internal void Vinsert_StartTimeout() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timeout.StartCenter(this.TimeoutDuration, false); }
        public override void Vinsert_Buzzer(PlayerSelection buzzeredPlayer) { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_Buzzer(this.insertScene.Timeout, buzzeredPlayer); }
        public override void Vinsert_SetTimeout() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetTimeout(this.insertScene.Timeout); }
        public override void Vinsert_StopTimeout() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_StopTimeout(this.insertScene.Timeout); }

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
                int id = 1;
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
            //base.Vstage_Init();
            this.Vstage_GamescoreIn();
            if (this.hostScene is VRemote4.HandlerSi.Scene) this.hostScene.Reset();
            if (this.leftPlayerScene is VRemote4.HandlerSi.Scene) this.leftPlayerScene.Reset();
            if (this.rightPlayerScene is VRemote4.HandlerSi.Scene) this.rightPlayerScene.Reset();
        }

        public override void Vhost_LoadScene() { this.hostScene.Load(); }
        internal void Vhost_GameIn() {
            this.Vhost_SetGame();
            if (this.hostScene is VRemote4.HandlerSi.Scene) this.hostScene.ToIn();
        }
        internal void Vhost_SetGame() { if (this.hostScene is VRemote4.HandlerSi.Scene) this.Vhost_SetGame(this.hostScene, this.ActiveDataList.ToArray(), this.SelectedDatasetID); }
        internal void Vhost_SetGame(
            Stage scene,
            DatasetContent[] datasetList,
            int selectedID) {
            if (scene is Stage) {
                for (int i = 1; i <= 7; i++) {
                    if (datasetList is DatasetContent[] &&
                        datasetList.Length >= i) {
                        scene.SetQuestionText(i, datasetList[i - 1].HostText);
                        if (selectedID == i) scene.SelectQuestion(i);
                        else scene.ResetQuestion(i);
                    }
                    else {
                        scene.SetQuestionText(i, string.Empty);
                        scene.BlockQuestion(i);
                    }
                }
            }
        }
        internal void Vhost_GameOut() { if (this.hostScene is VRemote4.HandlerSi.Scene) this.hostScene.ToOut(); }
        public override void Vhost_UnloadScene() { this.hostScene.Unload(); }

        public override void Vleftplayer_LoadScene() { this.leftPlayerScene.Load(); }
        public override void Vleftplayer_UnloadScene() { this.leftPlayerScene.Unload(); }

        public override void Vrightplayer_LoadScene() { this.rightPlayerScene.Load(); }
        public override void Vrightplayer_UnloadScene() { this.rightPlayerScene.Unload(); }

        public void Vplayers_GameIn() {
            this.Vplayers_SetGame();
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.ToIn();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.ToIn();
        }
        internal void Vplayers_SetGame() { if (this.hostScene is VRemote4.HandlerSi.Scene) this.Vplayers_SetGame(this.leftPlayerScene, this.rightPlayerScene, this.ActiveDataList.ToArray(), this.SelectedDatasetID); }
        public void Vplayers_SetGame(
            Stage leftPlayerScene,
            Stage rightPlayerScene,
            DatasetContent[] datasetList,
            int selectedID) {
            for (int i = 1; i <= QuestionsCount; i++) {
                if (datasetList is DatasetContent[] &&
                    datasetList.Length >= i) {
                    if (leftPlayerScene is Stage) {
                        leftPlayerScene.SetQuestionText(i, datasetList[i - 1].Text);
                        if (selectedID == i) leftPlayerScene.SelectQuestion(i);
                        else leftPlayerScene.ResetQuestion(i);
                    }
                    if (rightPlayerScene is Stage) {
                        rightPlayerScene.SetQuestionText(i, datasetList[i - 1].Text);
                        if (selectedID == i) rightPlayerScene.SelectQuestion(i);
                        else rightPlayerScene.ResetQuestion(i);
                    }
                }
                else {
                    if (leftPlayerScene is Stage) {
                        leftPlayerScene.SetQuestionText(i, string.Empty);
                        leftPlayerScene.BlockQuestion(i);
                    }
                    if (rightPlayerScene is Stage) {
                        rightPlayerScene.SetQuestionText(i, string.Empty);
                        rightPlayerScene.BlockQuestion(i);
                    }
                }
            }
        }
        internal void Vplayers_InitPlayDice() {
            this.Vstage_SetBuzzer(PlayerSelection.NotSelected);
        }
        public void Vplayers_GameOut() {
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.ToOut();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.ToOut();
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
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_dataset_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                this.buildGameLists();
            }
            this.Save();
        }

        #endregion

    }
}
