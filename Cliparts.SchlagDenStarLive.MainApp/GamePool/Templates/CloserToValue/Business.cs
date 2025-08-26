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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.CloserToValue;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.CloserToValue {

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

    public class Business : _Base.TimerScore.Business {

        #region Properties

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
        public string LeftPlayerOffsetText { get; private set; }

        private bool leftPlayerIsCloser = false;
        public bool LeftPlayerIsCloser {
            get { return this.leftPlayerIsCloser; }
            private set {
                if (this.leftPlayerIsCloser != value) {
                    this.leftPlayerIsCloser = value;
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
        public string RightPlayerOffsetText { get; private set; }

        private bool rightPlayerIsCloser = false;
        public bool RightPlayerIsCloser {
            get { return this.rightPlayerIsCloser; }
            private set {
                if (this.rightPlayerIsCloser != value) {
                    this.rightPlayerIsCloser = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int contentInsertPositionX = 0;
        public int ContentInsertPositionX {
            get { return this.contentInsertPositionX; }
            set {
                if (this.contentInsertPositionX != value) {
                    this.contentInsertPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetContent();
                }
            }
        }

        private int contentInsertPositionY = 0;
        public int ContentInsertPositionY {
            get { return this.contentInsertPositionY; }
            set {
                if (this.contentInsertPositionY != value) {
                    this.contentInsertPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetContent();
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

        private string unit = "g";
        public string Unit {
            get { return this.unit; }
            set {
                if (this.unit != value) {
                    if (value == null)
                        value = string.Empty;
                    else
                        this.unit = value;
                    this.on_PropertyChanged();
                    this.calcOffsets();
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

        private bool repressPropertyChanged = false;

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.CloserToValue'", typeIdentifier);
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

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);

            this.calcOffsets();
        }

        public override void Dispose() {
            base.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Timer.Alarm1Fired -= this.timer_Alarm1Fired;
            this.insertScene.Timer.Alarm2Fired -= this.timer_Alarm2Fired;
            this.insertScene.Timer.StopFired -= this.timer_StopFired;
            this.insertScene.Timer.PropertyChanged -= this.timer_PropertyChanged;
            this.insertScene.Dispose();
        }

        public override void ResetData() {
            base.ResetData();
            this.SelectDataset(0);
            this.LeftPlayerValue = 0;
            this.RightPlayerValue = 0;
        }

        internal void Resolve() {
            this.calcOffsets();
            if (this.LeftPlayerIsCloser) this.LeftPlayerScore++;
            if (this.RightPlayerIsCloser) this.RightPlayerScore++;

            if (Math.Abs(LeftPlayerOffset) == Math.Abs(RightPlayerOffset)) {
                this.LeftPlayerScore++; 
                this.RightPlayerScore++;
            }
        }

        public override void Next() {
            base.Next();
            this.LeftPlayerValue = 0;
            this.RightPlayerValue = 0;
            this.SelectDataset(this.SelectedDatasetIndex + 1);
        }

        private void calcOffsets() {
            if (this.LeftPlayerValue > 0 &&
                this.RightPlayerValue > 0 &&
                this.SelectedDataset is DatasetContent) {
                this.LeftPlayerOffset = this.LeftPlayerValue - this.SelectedDataset.TargetValue;
                if (this.LeftPlayerOffset > 0) this.LeftPlayerOffsetText = "+" + this.LeftPlayerOffset.ToString("") + this.Unit;
                else if (this.LeftPlayerOffset < 0) this.LeftPlayerOffsetText = this.LeftPlayerOffset.ToString("") + this.Unit;
                else this.LeftPlayerOffsetText = this.LeftPlayerOffset.ToString("") + this.Unit;
                this.RightPlayerOffset = this.RightPlayerValue - this.SelectedDataset.TargetValue;
                if (this.RightPlayerOffset > 0) this.RightPlayerOffsetText = "+" + this.RightPlayerOffset.ToString("") + this.Unit;
                else if (this.RightPlayerOffset < 0) this.RightPlayerOffsetText = this.RightPlayerOffset.ToString("") + this.Unit;
                else this.RightPlayerOffsetText = this.RightPlayerOffset.ToString("") + this.Unit;
                this.LeftPlayerIsCloser = Math.Abs(this.LeftPlayerOffset) < Math.Abs(this.RightPlayerOffset);
                this.RightPlayerIsCloser = Math.Abs(this.LeftPlayerOffset) > Math.Abs(this.RightPlayerOffset);
            }
            else {
                this.LeftPlayerOffset = 0;
                this.LeftPlayerOffsetText = string.Empty;
                this.LeftPlayerIsCloser = false;
                this.RightPlayerOffset = 0;
                this.RightPlayerOffsetText = string.Empty;
                this.RightPlayerIsCloser = false;
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
        public void Vinsert_SetContent() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.SetPositionX(this.ContentInsertPositionX);
                this.insertScene.SetPositionY(this.ContentInsertPositionY);
            }
        }
        public void Vinsert_TargetValueIn() {
            this.Vinsert_SetContent();
            this.Vinsert_SetTargetValue();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.TargetToIn();
        }
        public void Vinsert_SetTargetValue() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                string value = string.Format("{0}{1}", this.SelectedDataset.TargetValue.ToString(), this.Unit);
                this.insertScene.SetTargetValue(value);
            }
        }
        public void Vinsert_TargetValueOut() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.TargetToOut();
        }
        public void Vinsert_PlayerIn() {
            this.Vinsert_SetContent();
            this.Vinsert_SetPlayer();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.SetOffsetOut();
                this.insertScene.PlayerToIn();
            }
        }
        public void Vinsert_SetPlayer() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.SetLeftName(this.LeftPlayerName);
                string value = string.Format("{0}{1}", this.LeftPlayerValue.ToString(), this.Unit);
                this.insertScene.SetLeftValue(value);
                this.insertScene.SetRightName(this.RightPlayerName);
                value = string.Format("{0}{1}", this.RightPlayerValue.ToString(), this.Unit);
                this.insertScene.SetRightValue(value);
                this.insertScene.SetFlipPosition(this.FlipPlayers);
            }
        }
        public void Vinsert_ResolvePlayer() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.SetLeftOffsetValue(this.LeftPlayerOffsetText);
                this.insertScene.SetLeftOffsetIsWinner(this.LeftPlayerIsCloser);
                this.insertScene.SetRightOffsetValue(this.RightPlayerOffsetText);
                this.insertScene.SetRightOffsetIsWinner(this.RightPlayerIsCloser);
                this.insertScene.OffsetToIn();
            }
        }
        public void Vinsert_PlayerOut() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.PlayerToOut();
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
        public override void Vinsert_TimerIn() {
            this.Vinsert_SetTimer();
            this.Vinsert_ResetTimer();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ToIn();
        }
        public override void Vinsert_SetTimer() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.Timer.SetPositionX(this.TimerPositionX);
                this.insertScene.Timer.SetPositionY(this.TimerPositionY);
                this.insertScene.Timer.SetStyle(this.TimerStyle);
                this.insertScene.Timer.SetScaling(100);
                if (this.RunExtraTime) this.insertScene.Timer.SetStartTime(this.TimerExtraTime);
                else this.insertScene.Timer.SetStartTime(this.TimerStartTime);
                this.insertScene.Timer.SetStopTime(this.TimerStopTime);
                this.insertScene.Timer.SetAlarmTime1(this.TimerAlarmTime1);
                this.insertScene.Timer.SetAlarmTime2(this.TimerAlarmTime2);
            }
        }
        public override void Vinsert_StartTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.StartTimer(); }
        public override void Vinsert_StopTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.StopTimer(); }
        public override void Vinsert_ContinueTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ContinueTimer(); }
        public override void Vinsert_ResetTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ResetTimer(); }
        public override void Vinsert_TimerOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ToOut(); }
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

        protected override void sync_timer_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "CurrentTime") this.TimerCurrentTime = this.insertScene.Timer.CurrentTime;
                else if (e.PropertyName == "IsRunning") this.TimerIsRunning = this.insertScene.Timer.IsRunning;
            }
        }

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
