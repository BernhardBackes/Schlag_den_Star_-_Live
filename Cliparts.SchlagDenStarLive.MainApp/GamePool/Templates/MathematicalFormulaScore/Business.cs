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
using System.Xml.XPath;
using System.Xml.Serialization;

using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MathematicalFormulaScore;
using Cliparts.VRemote4.HandlerSi;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.MathematicalFormulaScore {

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetContent : Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private Game.Sizes size = Game.Sizes.TwoOperations;
        public Game.Sizes Size {
            get { return this.size; }
            set { 
                if (this.size != value) {
                    this.size = value;
                    this.calcSolution();
                    this.on_PropertyChanged();
                }
            }
        }

        private int number_01 = 0;
        public int Number_01 {
            get { return this.number_01; }
            set {
                if (this.number_01 != value) {
                    if (value < 0) this.number_01 = 0;
                    else this.number_01 = value;
                    this.calcSolution();
                    this.on_PropertyChanged();
                }
            }
        }

        private int number_02 = 0;
        public int Number_02 {
            get { return this.number_02; }
            set {
                if (this.number_02 != value) {
                    if (value < 0) this.number_02 = 0;
                    else this.number_02 = value;
                    this.calcSolution();
                    this.on_PropertyChanged();
                }
            }
        }

        private int number_03 = 0;
        public int Number_03 {
            get { return this.number_03; }
            set {
                if (this.number_03 != value) {
                    if (value < 0) this.number_03 = 0;
                    else this.number_03 = value;
                    this.calcSolution();
                    this.on_PropertyChanged();
                }
            }
        }

        private int number_04 = 0;
        public int Number_04 {
            get { return this.number_04; }
            set {
                if (this.number_04 != value) {
                    if (value < 0) this.number_04 = 0;
                    else this.number_04 = value;
                    this.calcSolution();
                    this.on_PropertyChanged();
                }
            }
        }

        private Game.Operations operations_01 = Game.Operations.Plus;
        public Game.Operations Operation_01 {
            get { return this.operations_01; }
            set {
                if (this.operations_01 != value) {
                    this.operations_01 = value;
                    this.calcSolution();
                    this.on_PropertyChanged();
                }
            }
        }

        private Game.Operations operations_02 = Game.Operations.Plus;
        public Game.Operations Operation_02 {
            get { return this.operations_02; }
            set {
                if (this.operations_02 != value) {
                    this.operations_02 = value;
                    this.calcSolution();
                    this.on_PropertyChanged();
                }
            }
        }

        private Game.Operations operations_03 = Game.Operations.Plus;
        public Game.Operations Operation_03 {
            get { return this.operations_03; }
            set {
                if (this.operations_03 != value) {
                    this.operations_03 = value;
                    this.calcSolution();
                    this.on_PropertyChanged();
                }
            }
        }

        private int solution = 0;
        [XmlIgnore]
        public int Solution {
            get { return this.solution; }
            private set {
                if (this.solution != value) {
                    if (value < 0) this.solution = 0;
                    else this.solution = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string hostText;
        [XmlIgnore]
        public string HostText {
            get { return this.hostText; }
            private set { 
                if (this.hostText != value) {
                    if (string.IsNullOrEmpty(value)) this.hostText = string.Empty;
                    else this.hostText = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContent() {
            this.calcSolution();
        }

        public void Clone(
            DatasetContent source) {
            if (source is DatasetContent) {
                this.Size = source.Size;
                this.Number_01 = source.Number_01;
                this.Number_02 = source.Number_02;
                this.Number_03 = source.Number_03;
                this.Number_04 = source.Number_04;
                this.Operation_01 = source.Operation_01;
                this.Operation_02 = source.Operation_02;
                this.Operation_03 = source.Operation_03;
            }
            else {
                this.Size = Game.Sizes.TwoOperations;
                this.Number_01 = 0;
                this.Number_02 = 0;
                this.Number_03 = 0;
                this.Number_04 = 0;
                this.Operation_01 = Game.Operations.Plus;
                this.Operation_02 = Game.Operations.Plus;
                this.Operation_03 = Game.Operations.Plus;
            }
        }

        public static string GetOperationsText(
            Game.Operations value) {
            string result = string.Empty;
            switch (value) {
                case Game.Operations.Plus:
                    result = "+";
                    break;
                case Game.Operations.Minus:
                    result = "-";
                    break;
                case Game.Operations.Mal:
                    result = "x";
                    break;
                case Game.Operations.Geteilt:
                    result = ":";
                    break;
            }
            return result;
        }

        private void calcSolution() {
            string formula = string.Empty;
            string hostText = string.Empty;
            if (this.Size == Game.Sizes.TwoOperations) {
                formula = string.Format("{0} {1} {2} {3} {4}",
                    this.Number_01.ToString(),
                    GetOperationsText(this.Operation_01),
                    this.Number_02.ToString(),
                    GetOperationsText(this.Operation_02),
                    this.Number_03.ToString());
                hostText = string.Format("{0} ? {1} ? {2} = ",
                    this.Number_01.ToString(),
                    this.Number_02.ToString(),
                    this.Number_03.ToString());
            }
            else {
                formula = string.Format("{0} {1} {2} {3} {4} {5} {6}",
                    this.Number_01.ToString(),
                    GetOperationsText(this.Operation_01),
                    this.Number_02.ToString(),
                    GetOperationsText(this.Operation_02),
                    this.Number_03.ToString(),
                    GetOperationsText(this.Operation_03),
                    this.Number_04.ToString());
                hostText = string.Format("{0} ? {1} ? {2} ? {3} = ",
                    this.Number_01.ToString(),
                    this.Number_02.ToString(),
                    this.Number_03.ToString(),
                    this.Number_04.ToString());
            }
            this.Solution = EvaluteExpression(formula);
            this.toString = string.Format("{0} = {1}", formula, this.Solution.ToString());
            this.HostText = string.Format("{0}{1}\r\n{2}", hostText, this.Solution.ToString(), this.toString);
            this.on_PropertyChanged("Formula");
        }

        public static int EvaluteExpression(
            string value) {
            string XML = "<?xml version='1.0'?><Frank></Frank>";
            int result = 0;
            try {
                using (MemoryStream ms = new
                MemoryStream(Encoding.Default.GetBytes(XML))) {
                    XPathDocument XDoc = new XPathDocument(ms);
                    XPathNavigator XNavi = XDoc.CreateNavigator();
                    result = Convert.ToInt32(XNavi.Evaluate(value.Replace(":", "div").Replace("x", "*")));
                }
            }
            catch (Exception) {
                result = 0;
            }
            return result;
        }

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

        private int taskCounterSize = 13;
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


        private int gameInsertPositionX = 0;
        public int GameInsertPositionX {
            get { return this.gameInsertPositionX; }
            set {
                if (this.gameInsertPositionX != value) {
                    this.gameInsertPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetGameInsert();
                }
            }
        }

        private int gameInsertPositionY = 0;
        public int GameInsertPositionY {
            get { return this.gameInsertPositionY; }
            set {
                if (this.gameInsertPositionY != value) {
                    this.gameInsertPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetGameInsert();
                }
            }
        }

        private PlayerSelection buzzeredPlayer = PlayerSelection.NotSelected;
        public PlayerSelection BuzzeredPlayer {
            get { return this.buzzeredPlayer; }
            private set {
                if (this.buzzeredPlayer != value) {
                    this.buzzeredPlayer = value;
                    this.Vstage_SetBuzzer(value);
                    this.on_PropertyChanged();
                }
            }
        }

        private string playerInput = string.Empty;
        [Serialization.NotSerialized]
        public string PlayerInput {
            get { return this.playerInput; }
            set {
                if (this.playerInput != value) {
                    if (string.IsNullOrEmpty(value)) this.playerInput = string.Empty;
                    else this.playerInput = value;
                    this.on_PropertyChanged();
                    this.calcPlayerResult();
                }
            }
        }

        private string playerFulltext = string.Empty;
        [Serialization.NotSerialized]
        public string PlayerFulltext {
            get { return this.playerFulltext; }
            private set {
                if (this.playerFulltext != value) {
                    if (string.IsNullOrEmpty(value)) this.playerFulltext = string.Empty;
                    else this.playerFulltext = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int playerResult = 0;
        [Serialization.NotSerialized]
        public int PlayerResult {
            get { return this.playerResult; }
            private set {
                if (this.playerResult != value) {
                    this.playerResult = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool playerResultIsCorrect = false;
        public bool PlayerResultIsCorrect {
            get { return this.playerResultIsCorrect; }
            set {
                if (this.playerResultIsCorrect != value) {
                    this.playerResultIsCorrect = value;
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

        private bool repressPropertyChanged = false;

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.MathematicalFormulaScore'", typeIdentifier);
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

            this.hostScene = new Host(syncContext, hostMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.hostScene.StatusChanged += this.hostScene_StatusChanged;

            this.leftPlayerScene = new Player(syncContext, leftPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.leftPlayerScene.PropertyChanged += this.leftPlayerScene_PropertyChanged;
            this.leftPlayerScene.StatusChanged += this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.OKButtonPressed += this.leftPlayerScene_OKButtonPressed;

            this.rightPlayerScene = new Player(syncContext, rightPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.rightPlayerScene.PropertyChanged += this.rightPlayerScene_PropertyChanged;
            this.rightPlayerScene.StatusChanged += this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.OKButtonPressed += this.rightPlayerScene_OKButtonPressed;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();
            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;

            this.hostScene.StatusChanged -= this.hostScene_StatusChanged;
            this.hostScene.Dispose();

            this.rightPlayerScene.PropertyChanged += this.rightPlayerScene_PropertyChanged;
            this.leftPlayerScene.StatusChanged -= this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.OKButtonPressed -= this.leftPlayerScene_OKButtonPressed;
            this.leftPlayerScene.Dispose();

            this.rightPlayerScene.PropertyChanged -= this.rightPlayerScene_PropertyChanged;
            this.rightPlayerScene.StatusChanged -= this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.OKButtonPressed -= this.rightPlayerScene_OKButtonPressed;
            this.rightPlayerScene.Dispose();

        }

        public override void ResetData() {
            base.ResetData();
            this.TaskCounter = 0;
            this.SelectDataset(0);
            this.BuzzeredPlayer = PlayerSelection.NotSelected;
            this.PlayerInput = string.Empty;
        }

        public virtual void DoBuzzer(
            PlayerSelection buzzeredPlayer) {
            if (this.BuzzeredPlayer == PlayerSelection.NotSelected &&
                this.BuzzeredPlayer != buzzeredPlayer) {
                this.BuzzeredPlayer = buzzeredPlayer;
                this.Vleftplayer_LockInput();
                this.Vrightplayer_LockInput();
                switch (this.BuzzeredPlayer) {
                    case PlayerSelection.LeftPlayer:
                        if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Game.BuzzerLeft();
                        this.Vrightplayer_ResetInput();
                        break;
                    case PlayerSelection.RightPlayer:
                        if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Game.BuzzerRight();
                        this.Vleftplayer_ResetInput();
                        break;
                }
                this.Vhost_SetPlayerInput();
            }
        }

        private void calcPlayerResult() {
            string playerInput = this.PlayerInput.Replace(" ", "");
            string fulltext = string.Empty;
            int result = 0;
            bool isCorrect = false;
            if (this.SelectedDataset is DatasetContent &&
                !string.IsNullOrEmpty(playerInput)) {
                int size = 2;
                if (this.SelectedDataset.Size == Game.Sizes.ThreeOperations) size = 3;
                if (playerInput.Length == size) {
                    if (size == 2) fulltext = string.Format("{0} {1} {2} {3} {4}",
                        this.SelectedDataset.Number_01.ToString(),
                        playerInput.Substring(0, 1),
                        this.SelectedDataset.Number_02.ToString(),
                        playerInput.Substring(1, 1),
                        this.SelectedDataset.Number_03.ToString());
                    if (size == 3) fulltext = string.Format("{0} {1} {2} {3} {4} {5} {6}",
                        this.SelectedDataset.Number_01.ToString(),
                        playerInput.Substring(0, 1),
                        this.SelectedDataset.Number_02.ToString(),
                        playerInput.Substring(1, 1),
                        this.SelectedDataset.Number_03.ToString(),
                        playerInput.Substring(2, 1),
                        this.SelectedDataset.Number_04.ToString());
                    if (!string.IsNullOrEmpty(fulltext)) {
                        result = DatasetContent.EvaluteExpression(fulltext);
                        fulltext += " = " + result.ToString();
                        isCorrect = this.SelectedDataset.Solution == result;
                    }
                }
            }
            this.PlayerFulltext = fulltext;
            this.PlayerResult = result;
            this.PlayerResultIsCorrect = isCorrect;
        }

        internal void Resolve() {
            if (this.SelectedDatasetIndex > 0 ||
                !this.SampleIncluded) {
                switch (this.BuzzeredPlayer) {
                    case PlayerSelection.LeftPlayer:
                        if (this.PlayerResultIsCorrect) this.LeftPlayerScore++;
                        else this.RightPlayerScore++;
                        break;
                    case PlayerSelection.RightPlayer:
                        if (this.PlayerResultIsCorrect) this.RightPlayerScore++;
                        else this.LeftPlayerScore++;
                        break;
                }
            }
        }

        public override void Next() {
            base.Next();
            this.TaskCounter++;
            this.SelectDataset(this.SelectedDatasetIndex + 1);
            this.BuzzeredPlayer = PlayerSelection.NotSelected;
            this.PlayerInput = string.Empty;
        }

        private List<Game.Operations> playerOperations {
            get {
                List<Game.Operations> result = new List<Game.Operations>();
                if (!string.IsNullOrEmpty(this.PlayerInput)) {
                    for (int i = 0; i < this.PlayerInput.Length; i++) {
                        string input = this.PlayerInput.Substring(i, 1);
                        if (input == "+") result.Add(Game.Operations.Plus);
                        else if (input == "-") result.Add(Game.Operations.Minus);
                        else if (input == "x") result.Add(Game.Operations.Mal);
                        else if (input == ":") result.Add(Game.Operations.Geteilt);
                    }
                }
                return result;
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
            this.calcPlayerResult();
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

        internal void Vinsert_GameInsertIn() {
            this.Vinsert_SetGameInsert();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Game.ToIn();
            if (this.TaskCounter > 0 && this.TaskCounter <= this.TaskCounterSize) this.Vinsert_TaskCounterIn();

        }
        internal void Vinsert_SetGameInsert(
            Game scene,
            DatasetContent data,
            Game.Operations? playerOperation_01,
            Game.Operations? playerOperation_02,
            Game.Operations? playerOperation_03,
            int playerResult,
            bool resultIsCorrect) {
            if (scene is Game) {
                scene.SetPositionX(this.GameInsertPositionX);
                scene.SetPositionY(this.GameInsertPositionY);
                if (data is DatasetContent) {
                    scene.SetSize(data.Size);
                    scene.SetNumber(1, data.Number_01);
                    scene.SetNumber(2, data.Number_02);
                    scene.SetNumber(3, data.Number_03);
                    scene.SetNumber(4, data.Number_04);
                    if (playerOperation_01.HasValue) {
                        scene.SetOperation(1, playerOperation_01.Value);
                    }
                    else scene.SetOperation(1, data.Operation_01);
                    if (playerOperation_02.HasValue) {
                        scene.SetOperation(2, playerOperation_02.Value);
                    }
                    else scene.SetOperation(2, data.Operation_02);
                    if (playerOperation_03.HasValue) {
                        scene.SetOperation(3, playerOperation_03.Value);
                    }
                    else scene.SetOperation(3, data.Operation_03);
                    scene.SetSolution(data.Solution);
                }
                scene.SetResult(playerResult);
                scene.SetIsTrue(resultIsCorrect);
            }
        }
        internal void Vinsert_SetGameInsert() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                Game.Operations? playerOperation_01 = null;
                Game.Operations? playerOperation_02 = null;
                Game.Operations? playerOperation_03 = null;
                List<Game.Operations> playerOperations = this.playerOperations;
                if (playerOperations.Count > 0) playerOperation_01 = playerOperations[0];
                if (playerOperations.Count > 1) playerOperation_02 = playerOperations[1];
                if (playerOperations.Count > 2) playerOperation_03 = playerOperations[2];
                this.Vinsert_SetGameInsert(
                    this.insertScene.Game,
                    this.SelectedDataset,
                    playerOperation_01,
                    playerOperation_02,
                    playerOperation_03,
                    this.PlayerResult,
                    this.PlayerResultIsCorrect);
            }
        }
        internal void Vinsert_Expand() {
            this.Vinsert_SetGameInsert();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Game.ToFormulaIn();
        }
        internal void Vinsert_InputIn_() {
            this.Vinsert_SetGameInsert();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Game.ToOperationIn();
        }
        internal void Vinsert_ResultIn() {
            this.Vinsert_SetGameInsert();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Game.ToResultIn();
        }
        internal void Vinsert_Resolve() {
            this.Vinsert_SetGameInsert();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Game.ToBorderIn();
        }
        internal void Vinsert_SolutionIn() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                this.Vinsert_SetGameInsert(
                    this.insertScene.Game,
                    this.SelectedDataset,
                    null,
                    null,
                    null,
                    this.PlayerResult,
                    this.PlayerResultIsCorrect);
                this.insertScene.Game.SetToBorderIn();
                this.insertScene.Game.JingleChange();
            }
        }
        internal void Vinsert_GameInsertOut() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Game.ToOut();
            this.Vinsert_TaskCounterOut();
        }
        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vstage_Init() {
            base.Vstage_Init();
            this.Vstage_SetBuzzer(PlayerSelection.NotSelected);
        }
        public virtual void Vstage_SetBuzzer(
            PlayerSelection buzzeredPlayer) {
            switch (buzzeredPlayer) {
                case PlayerSelection.LeftPlayer:
                    this.hostMasterScene.SetBuzzerLeft();
                    this.leftPlayerMasterScene.SetBuzzerLeft();
                    this.rightPlayerMasterScene.SetBuzzerLeft();
                    break;
                case PlayerSelection.RightPlayer:
                    this.hostMasterScene.SetBuzzerRight();
                    this.leftPlayerMasterScene.SetBuzzerRight();
                    this.rightPlayerMasterScene.SetBuzzerRight();
                    break;
                case PlayerSelection.NotSelected:
                default:
                    this.hostMasterScene.SetBuzzerOut();
                    this.leftPlayerMasterScene.SetBuzzerOut();
                    this.rightPlayerMasterScene.SetBuzzerOut();
                    break;
            }
        }
        internal void Vstage_ContentIn() {
            this.Vhost_ContentIn();
            this.Vleftplayer_ResetInput();
            this.Vrightplayer_ResetInput();
            this.Vleftplayer_ContentIn();
            this.Vrightplayer_ContentIn();
        }
        internal void Vstage_ContentOut() {
            this.Vhost_ContentOut();
            this.Vleftplayer_ContentOut();
            this.Vrightplayer_ContentOut();
        }

        public override void Vhost_LoadScene() { this.hostScene.Load(); }
        internal void Vhost_ContentIn() {
            this.Vhost_Set();
            if (this.hostScene is Host) this.hostScene.ToIn();
        }
        internal void Vhost_Set() {
            this.Vhost_Set(this.hostScene, this.SelectedDataset, this.PlayerFulltext, this.PlayerResultIsCorrect);
        }
        internal void Vhost_Set(
            Host scene,
            DatasetContent data,
            string playerFulltext,
            bool isCorrect) {
            if (scene is Host) {
                if (data is DatasetContent) {
                    scene.SetText(data.HostText);
                }
                scene.SetInputValue(playerFulltext);
                scene.SetInputIsCorrect(isCorrect);
            }
        }
        internal void Vhost_SetPlayerInput() {
            this.Vhost_Set();
            if (this.hostScene is Host) {
                switch (this.BuzzeredPlayer) {
                    case PlayerSelection.LeftPlayer:
                        this.hostScene.SetInputPosition(Host.InputPositions.Left);
                        this.hostScene.SetInputIn();
                        break;
                    case PlayerSelection.RightPlayer:
                        this.hostScene.SetInputPosition(Host.InputPositions.Right);
                        this.hostScene.SetInputIn();
                        break;
                    case PlayerSelection.NotSelected:
                    default:
                        this.hostScene.SetInputOut();
                        break;
                }
            }
        }
        internal void Vhost_ContentOut() {
            if (this.hostScene is Host) this.hostScene.ToOut();
            this.hostMasterScene.RightDisplayTextOut();
            this.hostMasterScene.LeftDisplayTextOut();
        }
        public override void Vhost_UnloadScene() { this.hostScene.Unload(); }

        internal void Vplayers_Set(
            Player scene,
            DatasetContent data) {
            if (scene is Player) {
                if (data is DatasetContent) {
                    scene.SetSize(data.Size);
                    scene.SetNumber(1, data.Number_01);
                    scene.SetNumber(2, data.Number_02);
                    scene.SetNumber(3, data.Number_03);
                    scene.SetNumber(4, data.Number_04);
                    scene.SetSolution(data.Solution);
                }
                else {
                    scene.SetSize(Game.Sizes.TwoOperations);
                    scene.SetNumber(1, 0);
                    scene.SetNumber(2, 0);
                    scene.SetNumber(3, 0);
                    scene.SetNumber(4, 0);
                    scene.SetSolution(0);
                }
            }
        }
        internal void Vplayers_Unlock() {
            this.BuzzeredPlayer = PlayerSelection.NotSelected;
            this.Vleftplayer_UnlockInput();
            this.Vrightplayer_UnlockInput();
        }

        public override void Vleftplayer_LoadScene() { this.leftPlayerScene.Load(); }
        internal void Vleftplayer_ContentIn() {
            this.Vplayers_Set(this.leftPlayerScene, this.SelectedDataset);
            if (this.leftPlayerScene is Player) this.leftPlayerScene.ToIn();
        }
        internal void Vleftplayer_ContentOut() { if (this.leftPlayerScene is Player) this.leftPlayerScene.ToOut(); }
        internal void Vleftplayer_ResetInput() { if (this.leftPlayerScene is Player) this.leftPlayerScene.ToIn(); }
        internal void Vleftplayer_UnlockInput() { if (this.leftPlayerScene is Player) this.leftPlayerScene.UnlockTouch(); }
        internal void Vleftplayer_ReleaseInput() { if (this.leftPlayerScene is Player) this.leftPlayerScene.UnlockTouch(); }
        internal void Vleftplayer_LockInput() { if (this.leftPlayerScene is Player) this.leftPlayerScene.LockTouch(); }
        public override void Vleftplayer_UnloadScene() { this.leftPlayerScene.Unload(); }

        public override void Vrightplayer_LoadScene() { this.rightPlayerScene.Load(); }
        internal void Vrightplayer_ContentIn() {
            this.Vplayers_Set(this.rightPlayerScene, this.SelectedDataset);
            if (this.rightPlayerScene is Player) this.rightPlayerScene.ToIn();
        }
        internal void Vrightplayer_ContentOut() { if (this.rightPlayerScene is Player) this.rightPlayerScene.ToOut(); }
        internal void Vrightplayer_ResetInput() { if (this.rightPlayerScene is Player) this.rightPlayerScene.ToIn(); }
        internal void Vrightplayer_UnlockInput() { if (this.rightPlayerScene is Player) this.rightPlayerScene.UnlockTouch(); }
        internal void Vrightplayer_ReleaseInput() { if (this.rightPlayerScene is Player) this.rightPlayerScene.UnlockTouch(); }
        internal void Vrightplayer_LockInput() { if (this.rightPlayerScene is Player) this.rightPlayerScene.LockTouch(); }
        public override void Vrightplayer_UnloadScene() { this.rightPlayerScene.Unload(); }

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
                if (e.PropertyName == "Formula") {
                    this.buildNameList();
                    this.on_PropertyChanged("NameList");
                    this.calcPlayerResult();
                }
            }
            this.Save();
        }

        private void leftPlayerScene_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerScene_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerScene_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs &&
                e.PropertyName == "Input" &&
                this.BuzzeredPlayer == PlayerSelection.LeftPlayer) {
                this.PlayerInput = this.leftPlayerScene.Input;
                this.Vhost_SetPlayerInput();
            }
        }

        void leftPlayerScene_OKButtonPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerScene_OKButtonPressed(object content) {
            if (this.BuzzeredPlayer == PlayerSelection.NotSelected) {
                if (this.leftPlayerScene is Player) this.PlayerInput = this.leftPlayerScene.Input;
                this.DoBuzzer(PlayerSelection.LeftPlayer);
            }
        }

        private void rightPlayerScene_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs &&
                e.PropertyName == "Input" &&
                this.BuzzeredPlayer == PlayerSelection.RightPlayer) {
                this.PlayerInput = this.rightPlayerScene.Input;
                this.Vhost_SetPlayerInput();
            }
        }

        void rightPlayerScene_OKButtonPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_OKButtonPressed(object content) {
            if (this.BuzzeredPlayer == PlayerSelection.NotSelected) {
                if (this.rightPlayerScene is Player) this.PlayerInput = this.rightPlayerScene.Input;
                this.DoBuzzer(PlayerSelection.RightPlayer);
            }
        }

        #endregion
    }
}
