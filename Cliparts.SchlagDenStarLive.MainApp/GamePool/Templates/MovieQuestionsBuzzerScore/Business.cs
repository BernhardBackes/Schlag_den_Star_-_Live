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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MovieQuestionsBuzzerScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.MovieQuestionsBuzzerScore {

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetQuestion : INotifyPropertyChanged {

        #region Properties

        private string text = string.Empty;
        public string Text {
            get { return this.text; }
            set {
                if (this.text != value) {
                    if (value == null) value = string.Empty;
                    this.text = Regex.Replace(value, "(?<!\r)\n", "\r\n"); ;
                    this.on_PropertyChanged();
                }
            }
        }

        private string answer = string.Empty;
        public string Answer {
            get { return this.answer; }
            set {
                if (this.answer != value) {
                    if (value == null) value = string.Empty;
                    this.answer = Regex.Replace(value, "(?<!\r)\n", "\r\n"); ;
                    this.on_PropertyChanged();
                }
            }
        }


        #endregion


        #region Funktionen

        public DatasetQuestion() {}

        public DatasetQuestion(
            string text) {
            this.Text = text;
        }
        public DatasetQuestion(
            string text,
            string answer) {
            this.Text = text;
            this.Answer = answer;
        }

        public void Clone(
            DatasetQuestion source) {
            if (source is DatasetQuestion) {
                this.Text = source.Text;
                this.Answer = source.Answer;
            }
            else {
                this.Text = string.Empty;
                this.Answer = string.Empty;
            }
        }

        public override string ToString() { return this.Text; }

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

    public class DatasetContent : Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private string name = string.Empty;
        public string Name {
            get { return this.name; }
            set {
                if (this.name != value) {
                    if (value == null) value = string.Empty;
                    this.name = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string movieFilename = string.Empty;
        public string MovieFilename {
            get { return this.movieFilename; }
            set {
                if (this.movieFilename != value) {
                    if (value == null) value = string.Empty;
                    this.movieFilename = value;
                    this.on_PropertyChanged();
                    this.Movie = Helper.getThumbnailFromMediaFile(value, 2.0f);
                }
            }
        }

        [XmlIgnore]
        public Image Movie { get; private set; }

        private List<DatasetQuestion> questionList = new List<DatasetQuestion>();
        public DatasetQuestion[] QuestionList {
            get { return this.questionList.ToArray(); }
            set {
                this.tryRemoveAllQuestions();
                if (value is DatasetQuestion[]) {
                    foreach(DatasetQuestion item in value) this.tryAddQuestion(item, -1);
                }
                this.on_PropertyChanged("QuestionList");
            }
        }

        public int QuestionsCount { get { return this.questionList.Count; } }

        [XmlIgnore]
        public DatasetQuestion SelectedQuestion { get; private set; }

        [XmlIgnore]
        public int SelectedQuestionIndex { get; private set; }

        #endregion


        #region Funktionen

        public DatasetContent() { }
        public DatasetContent(
            string pictureFilename) {
            if (string.IsNullOrEmpty(pictureFilename)) {
                this.Name = "?";
                this.MovieFilename = string.Empty;
            }
            else {
                this.Name = Path.GetFileNameWithoutExtension(pictureFilename);
                this.MovieFilename = pictureFilename;
            }
        }

        public DatasetQuestion GetQuestion(
            int index) {
            if (index >= 0 &&
                index < this.questionList.Count) return this.questionList[index];
            else return null;
        }

        public int GetQuestionIndex(
            DatasetQuestion question) {
            if (question is DatasetQuestion) return this.questionList.IndexOf(question);
            else return -1;
        }

        public void SelectQuestion(
            int index) {
            if (index < 0) index = 0;
            if (index >= this.questionList.Count) index = this.questionList.Count - 1;
            if (this.SelectedQuestionIndex != index) {
                this.SelectedQuestionIndex = index;
                this.on_PropertyChanged("SelectedQuestionIndex");
            }
            DatasetQuestion selectedQuestion = this.GetQuestion(index);
            if (this.SelectedQuestion != selectedQuestion) {
                this.SelectedQuestion = selectedQuestion;
                this.on_PropertyChanged("SelectedQuestion");
            }
        }

        public void AddQuestion(
            DatasetQuestion newQuestion,
            int insertIndex) {
            if (this.tryAddQuestion(newQuestion, insertIndex)) this.on_PropertyChanged("QuestionList");
            if (this.SelectedQuestion == null) this.SelectQuestion(0);
        }
        private bool tryAddQuestion(
            DatasetQuestion newQuestion,
            int insertIndex) {
            if (newQuestion is DatasetQuestion &&
                !this.questionList.Contains(newQuestion)) {
                newQuestion.PropertyChanged += this.on_PropertyChanged;
                if (insertIndex >= 0 &&
                    insertIndex < this.QuestionsCount) {
                    this.questionList.Insert(insertIndex, newQuestion);
                }
                else {
                    this.questionList.Add(newQuestion);
                }
                return true;
            }
            else return false;
        }

        public bool TryMoveQuestionUp(
            int index) {
            if (index > 0 &&
                index < this.QuestionsCount) {
                DatasetQuestion question = this.GetQuestion(index);
                this.questionList.RemoveAt(index);
                this.questionList.Insert(index - 1, question);
                this.on_PropertyChanged("QuestionList");
                return true;
            }
            else return false;
        }
        public bool TryMoveQuestionDown(
            int index) {
            if (index >= 0 &&
                index < this.QuestionsCount - 1) {
                DatasetQuestion question = this.GetQuestion(index);
                this.questionList.RemoveAt(index);
                this.questionList.Insert(index + 1, question);
                this.on_PropertyChanged("QuestionList");
                return true;
            }
            else return false;
        }

        public void RemoveAllQuestions() {
            if (this.tryRemoveAllQuestions()) {
                this.SelectQuestion(0);
                this.on_PropertyChanged("QuestionList");
            }
        }
        private bool tryRemoveAllQuestions() {
            bool questionRemoved = false;
            DatasetQuestion[] questionList = this.questionList.ToArray();
            foreach (DatasetQuestion item in questionList) questionRemoved = this.removeQuestion(item) | questionRemoved;
            return questionRemoved;
        }
        public bool TryRemoveQuestion(
            int index) {
            if (this.removeQuestion(this.GetQuestion(index))) {
                this.on_PropertyChanged("QuestionList");
                if (index <= this.SelectedQuestionIndex) this.SelectQuestion(this.SelectedQuestionIndex);
                return true;
            }
            else return false;
        }
        private bool removeQuestion(
            DatasetQuestion question) {
            if (question is DatasetQuestion &&
                this.questionList.Contains(question)) {
                question.PropertyChanged -= this.on_PropertyChanged;
                this.questionList.Remove(question);
                return true;
            }
            else return false;
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
        #endregion

    }

    public class Business : _Base.BuzzerScore.Business {

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

        private VentuzScenes.GamePool._Modules.Score.Styles setsStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
        public VentuzScenes.GamePool._Modules.Score.Styles CounterStyle {
            get { return this.setsStyle; }
            set {
                if (this.setsStyle != value) {
                    this.setsStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounter();
                }
            }
        }

        private int counterPositionX = 0;
        public int CounterPositionX {
            get { return this.counterPositionX; }
            set {
                if (this.counterPositionX != value) {
                    this.counterPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounter();
                }
            }
        }

        private int counterPositionY = 0;
        public int CounterPositionY {
            get { return this.counterPositionY; }
            set {
                if (this.counterPositionY != value) {
                    this.counterPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounter();
                }
            }
        }

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

        public bool LastQuestion {
            get {
                if (this.SelectedDataset is DatasetContent) return this.SelectedDataset.QuestionsCount <= this.SelectedDataset.SelectedQuestionIndex + 1;
                else return true;
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

        private bool repressPropertyChanged = false;

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.MovieQuestionsBuzzerScore'", typeIdentifier);
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

            this.fullscreenScene = new Fullscreen(syncContext, fullscreenMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.fullscreenScene.StatusChanged += this.fullscreenScene_StatusChanged;

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

            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Dispose();
            this.fullscreenScene.StatusChanged -= this.fullscreenScene_StatusChanged;
            this.fullscreenScene.Dispose();
            this.hostScene.StatusChanged -= this.hostScene_StatusChanged;
            this.hostScene.Dispose();
            this.leftPlayerScene.StatusChanged -= this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.Dispose();
            this.rightPlayerScene.StatusChanged -= this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.Dispose();
        }

        public override void ResetData() {
            base.ResetData();
            this.TaskCounter = 0;
            this.LeftPlayerCounter = 0;
            this.RightPlayerCounter = 0;
            this.SelectDataset(0);
        }

        public void True() {
            switch (this.BuzzeredPlayer) {
                case PlayerSelection.LeftPlayer:
                    this.LeftPlayerCounter++;
                    break;
                case PlayerSelection.RightPlayer:
                    this.RightPlayerCounter++;
                    break;
            }
            this.Vinsert_SetCounter();
        }

        public void False() {
            switch (this.BuzzeredPlayer) {
                case PlayerSelection.LeftPlayer:
                    this.RightPlayerCounter++;
                    break;
                case PlayerSelection.RightPlayer:
                    this.LeftPlayerCounter++;
                    break;
            }
            this.Vinsert_SetCounter();
        }

        public void NextQuestion() {
            this.BuzzeredPlayer = PlayerSelection.NotSelected;
            if (this.SelectedDataset is DatasetContent) this.SelectedDataset.SelectQuestion(this.SelectedDataset.SelectedQuestionIndex + 1);
        }

        public void Resolve() {
            if (this.TaskCounter > 0) {
                if (this.LeftPlayerCounter > this.RightPlayerCounter) this.LeftPlayerScore++;
                else if (this.LeftPlayerCounter < this.RightPlayerCounter) this.RightPlayerScore++;
                else {
                    this.LeftPlayerScore++;
                    this.RightPlayerScore++;
                }
            }
            this.Vinsert_SetScore();
            this.Vstage_SetScore();
        }

        public override void Next() {
            base.Next();
            this.LeftPlayerCounter = 0;
            this.RightPlayerCounter = 0;
            this.TaskCounter++;
            this.SelectDataset(this.SelectedDatasetIndex + 1);
        }

        public override void ReleaseBuzzer() {
            base.ReleaseBuzzer();
            this.Vinsert_SetBorderBuzzer();
        }

        public override void DoBuzzer(PlayerSelection buzzeredPlayer) {
            base.DoBuzzer(buzzeredPlayer);
            this.Vinsert_SetBorderBuzzer();
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
            if (this.SelectedDataset is DatasetContent) this.SelectedDataset.SelectQuestion(0);
            this.on_PropertyChanged("SelectedDataset");
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

        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score, this.LeftPlayerScore, this.RightPlayerScore); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }

        public override void Vinsert_Buzzer(PlayerSelection buzzeredPlayer) { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_Buzzer(this.insertScene.Timeout, buzzeredPlayer); }
        public override void Vinsert_SetTimeout() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetTimeout(this.insertScene.Timeout); }
        public override void Vinsert_StopTimeout() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_StopTimeout(this.insertScene.Timeout); }

        public void Vinsert_CounterIn() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.Vinsert_SetCounter(this.insertScene.Counter);
                this.insertScene.Counter.ToIn();
            }
        }
        public void Vinsert_SetCounter() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetCounter(this.insertScene.Counter); }
        public void Vinsert_SetCounter(VentuzScenes.GamePool._Modules.Score scene) { this.Vinsert_SetCounter(scene, this.LeftPlayerCounter, this.RightPlayerCounter); }
        public void Vinsert_SetCounter(
            VentuzScenes.GamePool._Modules.Score scene,
            int leftPlayerCounter,
            int rightPlayerCounter) {
            if (scene is VentuzScenes.GamePool._Modules.Score) {
                scene.SetPositionX(this.CounterPositionX);
                scene.SetPositionY(this.CounterPositionY);
                scene.SetStyle(this.CounterStyle);
                scene.SetFlipPosition(this.FlipPlayers);
                scene.SetLeftTopName(this.LeftPlayerName);
                scene.SetLeftTopScore(leftPlayerCounter);
                scene.SetRightBottomName(this.RightPlayerName);
                scene.SetRightBottomScore(rightPlayerCounter);
            }
        }
        public void Vinsert_CounterOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Counter.ToOut(); }

        public void Vinsert_TextInsertIn() {
            this.Vinsert_SetTextInsert();
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.TextInsert.ToIn();
        }
        public void Vinsert_TextInsertSolutionIn() {
            this.Vinsert_SetTextInsert();
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.TextInsert.SolutionToIn();
        }
        public void Vinsert_SetTextInsert() {
            if (this.insertScene is VRemote4.HandlerSi.Scene && 
                this.SelectedDataset is DatasetContent &&
                this.SelectedDataset.SelectedQuestion is DatasetQuestion) {
                this.Vinsert_SetTextInsert(
                    this.insertScene.TextInsert,
                    this.SelectedDataset.SelectedQuestion.Text,
                    this.SelectedDataset.SelectedQuestion.Answer);
            }
        }
        public void Vinsert_SetTextInsert(
            TextInsert scene,
            string text,
            string answer) {
            if (scene is TextInsert) {
                scene.SetPositionX(this.TextInsertPositionX);
                scene.SetPositionY(this.TextInsertPositionY);
                scene.SetText(text);
                scene.SetSolution(answer);
            }
        }
        public void Vinsert_TextInsertOut() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.TextInsert.ToOut();
        }

        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vfullscreen_LoadScene() { this.fullscreenScene.Load(); }

        public void Vfullscreen_ContentIn() {
            if (this.fullscreenScene is VRemote4.HandlerSi.Scene &&
                this.SelectedDataset is DatasetContent) {
                this.Vfullscreen_SetContent(this.fullscreenScene, this.SelectedDataset.MovieFilename);
                this.fullscreenScene.ToIn();
            }
        }
        public void Vfullscreen_SetContent(
            Fullscreen scene,
            string movieFilename) {
            if (scene is Fullscreen) scene.SetMovieFilename(movieFilename);
        }
        public void Vfullscreen_StartContent() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.Start(); }
        public void Vfullscreen_ContentOut() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.ToOut(); }

        public override void Vfullscreen_UnloadScene() { this.fullscreenScene.Unload(); }

        public override void Vstage_Init() {
            base.Vstage_Init();
            if (this.hostMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostMasterScene.LiveVideoIn();
            if (this.leftPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerMasterScene.LiveVideoIn();
            if (this.rightPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerMasterScene.LiveVideoIn();
        }

        public override void Vhost_LoadScene() { this.hostScene.Load(); }
        public void Vhost_In() {
            this.Vhost_Set();
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ToIn();
        }
        public void Vhost_Set() { 
            if (this.hostScene is VRemote4.HandlerSi.Scene && 
                this.SelectedDataset is DatasetContent &&
                this.SelectedDataset.SelectedQuestion is DatasetQuestion) { 
                this.Vhost_Set(
                    this.hostScene, 
                    this.SelectedDataset.SelectedQuestionIndex + 1, 
                    this.SelectedDataset.QuestionsCount, 
                    this.SelectedDataset.SelectedQuestion.Text, 
                    this.SelectedDataset.SelectedQuestion.Answer); 
            } 
        }
        public void Vhost_Set(
            Stage scene,
            int counter,
            int length,
            string text,
            string answer) {
            string hostText = string.Format("{0}/{1}: {2}\n{3}", counter.ToString(), length.ToString(), text, answer);
            if (scene is Stage) scene.SetText(hostText);
        }
        public void Vhost_Out() { if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ToOut(); }
        public override void Vhost_UnloadScene() { this.hostScene.Unload(); }

        public override void Vleftplayer_LoadScene() { this.leftPlayerScene.Load(); }
        public void Vleftplayer_TextInsertIn() {
            this.Vleftplayer_SetTextInsert();
            if (this.leftPlayerScene is VRemote4.HandlerSi.Scene) this.leftPlayerScene.ToIn();
        }
        public void Vleftplayer_SetTextInsert() {
            if (this.leftPlayerScene is VRemote4.HandlerSi.Scene &&
                this.SelectedDataset is DatasetContent &&
                this.SelectedDataset.SelectedQuestion is DatasetQuestion) {
                this.Vplayer_SetTextInsert(
                    this.leftPlayerScene,
                    this.SelectedDataset.SelectedQuestion.Text);
            }
        }
        public void Vleftplayer_TextInsertOut() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.leftPlayerScene.ToOut();
        }
        public override void Vleftplayer_UnloadScene() { this.leftPlayerScene.Unload(); }

        public override void Vrightplayer_LoadScene() { this.rightPlayerScene.Load(); }
        public void Vrightplayer_TextInsertIn() {
            this.Vrightplayer_SetTextInsert();
            if (this.rightPlayerScene is VRemote4.HandlerSi.Scene) this.rightPlayerScene.ToIn();
        }
        public void Vrightplayer_SetTextInsert() {
            if (this.rightPlayerScene is VRemote4.HandlerSi.Scene &&
                this.SelectedDataset is DatasetContent &&
                this.SelectedDataset.SelectedQuestion is DatasetQuestion) {
                this.Vplayer_SetTextInsert(
                    this.rightPlayerScene,
                    this.SelectedDataset.SelectedQuestion.Text);
            }
        }
        public void Vrightplayer_TextInsertOut() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.rightPlayerScene.ToOut();
        }
        public override void Vrightplayer_UnloadScene() { this.rightPlayerScene.Unload(); }

        public void Vplayer_SetTextInsert(
            Stage scene,
            string text) {
            if (scene is Stage) scene.SetText(text);
        }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
            this.fullscreenScene.Clear();
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

        #endregion

    }
}
