using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.WeightLifting;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.WeightLifting {

    public class MarkerSet : INotifyPropertyChanged {

        #region Properties

        private int weight = 0;
        public int Weight {
            get { return this.weight; }
            set {
                if (this.weight != value) {
                    this.weight = value;
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        private Insert.DotStates leftPlayerStatus = Insert.DotStates.Off;
        public Insert.DotStates LeftPlayerStatus {
            get { return this.leftPlayerStatus; }
            set {
                if (this.leftPlayerStatus != value) {
                    this.leftPlayerStatus = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Insert.DotStates rightPlayerStatus = Insert.DotStates.Off;
        public Insert.DotStates RightPlayerStatus {
            get { return this.rightPlayerStatus; }
            set {
                if (this.rightPlayerStatus != value) {
                    this.rightPlayerStatus = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public MarkerSet() { }
        public MarkerSet(int weight) { this.Weight = weight; }

        public void Reset() {
            this.LeftPlayerStatus = VentuzScenes.GamePool.WeightLifting.Insert.DotStates.Off;
            this.RightPlayerStatus = VentuzScenes.GamePool.WeightLifting.Insert.DotStates.Off;
        }

        public void Clone(
            MarkerSet set) {
            if (set is MarkerSet) {
                this.Weight = set.Weight;
                this.LeftPlayerStatus = set.LeftPlayerStatus;
                this.RightPlayerStatus = set.RightPlayerStatus;
            }
            else this.Reset();
        }

        private void buildToString() { this.toString = this.Weight.ToString("0") + "kg"; }

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

    public class Business : _Base.Timer.Business {

        #region Properties

        private int markerSetsPositionX = 0;
        public int MarkerSetsPositionX {
            get { return this.markerSetsPositionX; }
            set {
                if (this.markerSetsPositionX != value) {
                    this.markerSetsPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetMarkerSets();
                }
            }
        }

        private int markerSetsPositionY = 0;
        public int MarkerSetsPositionY {
            get { return this.markerSetsPositionY; }
            set {
                if (this.markerSetsPositionY != value) {
                    this.markerSetsPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetMarkerSets();
                }
            }
        }

        private int markerBeginning = 45;
        public int MarkerBeginning {
            get { return this.markerBeginning; }
            set {
                if (this.markerBeginning != value) {
                    this.markerBeginning = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int markerExpanse = 10;
        public int MarkerExpanse {
            get { return this.markerExpanse; }
            set {
                if (this.markerExpanse != value) {
                    this.markerExpanse = value;
                    this.on_PropertyChanged();
                }
            }
        }

        protected List<MarkerSet> markerSetList = new List<MarkerSet>();
        public MarkerSet[] MarkerSetList {
            get { return this.markerSetList.ToArray(); }
            set {
                this.tryRemoveAllMarkerSets();
                if (value is MarkerSet[]) {
                    foreach (MarkerSet item in value) this.tryAddMarkerSet(item, -1);
                }
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                this.SelectMarker(0);
            }
        }

        private List<string> names = new List<string>();
        public string[] NameList { get { return this.names.ToArray(); } }

        public int MarkerSetsCount { get { return this.markerSetList.Count; } }

        public MarkerSet SelectedMarker { get; private set; }

        public int SelectedMarkerIndex { get; private set; }

        private Insert insertScene;
        public override VRemote4.HandlerSi.Scene.States InsertSceneStatus {
            get {
                if (this.insertScene is Insert) return this.insertScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private bool repressCheckBestValue = false;

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.WeightLifting'", typeIdentifier);
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

            //wenn die MarkerSets einen Synccontext benötigen, dann wird er hierdurch zugeordnet
            this.MarkerSetList = this.markerSetList.ToArray();

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;
            this.insertScene.Timer.PropertyChanged += this.insertTimer_PropertyChanged;

            this.Vinsert_Timer.Pose(syncContext, this.insertScene.Timer);
            this.Vinsert_Timer.PropertyChanged += this.on_PropertyChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);

        }

        public override void Dispose() {
            base.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Timer.PropertyChanged -= this.insertTimer_PropertyChanged;
            this.insertScene.Dispose();

            this.Vinsert_Timer.PropertyChanged -= this.on_PropertyChanged;
            this.Vinsert_Timer.Dispose();

            foreach (MarkerSet item in this.markerSetList) item.PropertyChanged -= this.markerSet_PropertyChanged;
        }

        public override void ResetData() {
            base.ResetData();
            this.RemoveAllMarkerSets();
        }

        public MarkerSet GetMarkerSet(
            int index) {
            if (index >= 0 &&
                index < this.markerSetList.Count) return this.markerSetList[index];
            else return null;
        }

        public int GetMarkerSetIndex(
            MarkerSet dataset) {
            int index = -1;
            int datasetIndex = 0;
            foreach (MarkerSet item in this.markerSetList) {
                if (item == dataset) {
                    index = datasetIndex;
                    break;
                }
                datasetIndex++;
            }
            return index;
        }

        public void SelectMarker(
            int index) {
            this.checkMarkerSetList();
            if (index < 0) index = 0;
            if (index >= this.markerSetList.Count) index = this.markerSetList.Count - 1;
            this.SelectedMarkerIndex = index;
            this.on_PropertyChanged("SelectedMarkerIndex");
            this.SelectedMarker = this.GetMarkerSet(index);
            this.on_PropertyChanged("SelectedMarker");
        }

        private void checkMarkerSetList() {
            if (this.MarkerSetsCount == 0) this.AddNewMarkerSet();
        }

        public void AddNewMarkerSet() {
            int height = this.MarkerBeginning;
            MarkerSet set = this.GetMarkerSet(this.MarkerSetsCount - 1);
            if (set is MarkerSet) height = set.Weight + this.MarkerExpanse;
            if (this.tryAddMarkerSet(new MarkerSet(height), -1)) this.on_PropertyChanged("NameList");
            this.SelectMarker(this.MarkerSetsCount - 1);
            this.Vinsert_SetMarkerSets();
        }
        private bool tryAddMarkerSet(
            MarkerSet markerSet,
            int insertIndex) {
            if (markerSet is MarkerSet) {
                MarkerSet newMarkerSet = new MarkerSet();
                newMarkerSet.Clone(markerSet);
                newMarkerSet.PropertyChanged += this.markerSet_PropertyChanged;
                if (insertIndex >= 0 &&
                    insertIndex < this.MarkerSetsCount) {
                    this.markerSetList.Insert(insertIndex, newMarkerSet);
                    this.names.Insert(insertIndex, newMarkerSet.ToString());
                }
                else {
                    this.markerSetList.Add(newMarkerSet);
                    this.names.Add(newMarkerSet.ToString());
                }
                return true;
            }
            else return false;
        }

        public void RemoveAllMarkerSets() {
            if (this.tryRemoveAllMarkerSets()) {
                this.AddNewMarkerSet();
                this.SelectMarker(0);
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                this.Vinsert_SetMarkerSets();
            }
        }
        private bool tryRemoveAllMarkerSets() {
            bool datasetRemoved = false;
            MarkerSet[] datasetList = this.markerSetList.ToArray();
            foreach (MarkerSet item in datasetList) datasetRemoved = this.removeMarkerSet(item) | datasetRemoved;
            return datasetRemoved;
        }
        public bool TryRemoveMarkerSet(
            int index) {
            if (this.removeMarkerSet(this.GetMarkerSet(index))) {
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                if (index <= this.SelectedMarkerIndex) this.SelectMarker(this.SelectedMarkerIndex);
                this.Vinsert_SetMarkerSets();
                return true;
            }
            else return false;
        }
        private bool removeMarkerSet(
            MarkerSet dataset) {
            if (dataset is MarkerSet &&
                this.markerSetList.Contains(dataset)) {
                dataset.PropertyChanged -= this.markerSet_PropertyChanged;
                this.markerSetList.Remove(dataset);
                return true;
            }
            else return false;
        }

        private void buildNameList() {
            this.names.Clear();
            foreach (MarkerSet item in this.markerSetList) {
                this.names.Add(item.ToString());
            }
        }

        public override void Vinsert_LoadScene() { this.insertScene.Load(); }
        public void Vinsert_MarkerSetsIn() {
            this.Vinsert_SetMarkerSets();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.ToIn();
        }
        public void Vinsert_SetMarkerSets() { this.Vinsert_SetMarkerSets(this.insertScene, this.MarkerSetsPositionX, this.MarkerSetsPositionY, this.LeftPlayerName, this.RightPlayerName, this.SelectedMarkerIndex, this.MarkerSetList); }
        public void Vinsert_SetMarkerSets(
            Insert scene,
            int positionX,
            int positionY,
            string topPlayerName,
            string bottomPlayerName,
            int selectedMarkerIndex,
            MarkerSet[] markerSetList) {
            if (scene is Insert) {
                scene.SetPositionX(positionX);
                scene.SetPositionY(positionY);
                scene.SetMarkersCount(selectedMarkerIndex + 1);
                scene.SetTopName(topPlayerName);
                scene.SetBottomName(bottomPlayerName);
                for (int i = 0; i <= selectedMarkerIndex; i++) {
                    if (markerSetList is MarkerSet[] &&
                        i < markerSetList.Length) {
                        scene.SetMarkersWeight(i + 1, markerSetList[i].Weight);
                        scene.SetMarkersTopStatus(i + 1, markerSetList[i].LeftPlayerStatus);
                        scene.SetMarkersBottomStatus(i + 1, markerSetList[i].RightPlayerStatus);
                    }
                }
            }
        }
        public void Vinsert_MarkerSetsOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.ToIn(); }
        public override void Vinsert_UnloadScene() { this.insertScene.Unload(); }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming

        void markerSet_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            this.on_PropertyChanged(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_markerSet_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_markerSet_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "Height") {
                    this.buildNameList();
                    this.on_PropertyChanged("NameList");
                }
            }
        }

        #endregion
    }
}
