using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

using Cliparts.Serialization;
using Cliparts.Tools.Base;
using Cliparts.SchlagDenStarLive.MainApp.Settings;

namespace Cliparts.SchlagDenStarLive.MainApp.Content.Playlist {

    public enum PlayoutChannelElements {  Insert, Fullscreen }

    public class DatasetContent : INotifyPropertyChanged {

        #region Properties

        private string name = string.Empty;
        public string Name {
            get { return this.name; }
            private set {
                if (this.name != value) {
                    if (string.IsNullOrEmpty(value)) this.name = string.Empty;
                    else this.name = value;
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        private string filename = string.Empty;
        public string Filename {
            get { return this.filename; }
            set {
                if (this.filename != value) {
                    if (string.IsNullOrEmpty(value)) this.filename = string.Empty;
                    else this.filename = value;
                    this.on_PropertyChanged();
                    if (string.IsNullOrEmpty(this.Name)) this.Name = Path.GetFileName(this.filename);
                    this.IsValid = File.Exists(this.filename);
                }
            }
        }

        private bool isValid = true;
        [XmlIgnore]
        public bool IsValid {
            get { return this.isValid; }
            set {
                if (this.isValid != value) {
                    this.isValid = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContent() { }
        public DatasetContent(
            string filename) {
            if (string.IsNullOrEmpty(filename)) {
                this.Name = "?";
                this.Filename = string.Empty;
            }
            else this.Filename = filename;
        }

        private void buildToString() { this.toString = this.Name; }

        public override string ToString() { return this.toString; }

        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void on_PropertyChanged([CallerMemberName]string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected virtual void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected virtual void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }

    public class Business : Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private SynchronizationContext syncContext;

        private List<DatasetContent> dataList = new List<DatasetContent>();
        [NotSerialized]
        public DatasetContent[] DataList {
            get { return this.dataList.ToArray(); }
            set {
                this.Eject();
                this.repressPropertyChanged = true;
                this.tryRemoveAllDatasets();
                if (value is DatasetContent[]) {
                    foreach (DatasetContent item in value) this.tryAddDataset(item, -1);
                }
                this.buildNameList();
                this.repressPropertyChanged = false;
                this.on_PropertyChanged("NameList");
            }
        }

        private List<string> names = new List<string>();
        public string[] NameList { get { return this.names.ToArray(); } }

        public int DatasetsCount { get { return this.dataList.Count; } }

        public DatasetContent SelectedDataset { get; private set; }

        public int SelectedDatasetIndex { get; private set; }

        protected VentuzScenes.Insert.Business insertMasterScene;
        protected VRemote4.HandlerSi.Scene.States insertMasterSceneStatus {
            get {
                if (this.insertMasterScene is VentuzScenes.Insert.Business) return this.insertMasterScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        protected VentuzScenes.Fullscreen.Business fullscreenMasterScene;
        protected VRemote4.HandlerSi.Scene.States fullscreenMasterSceneStatus {
            get {
                if (this.fullscreenMasterScene is VentuzScenes.Fullscreen.Business) return this.fullscreenMasterScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private PlayoutChannelElements playoutChannel = PlayoutChannelElements.Insert;
        public PlayoutChannelElements PlayoutChannel {
            get { return this.playoutChannel; }
            set {
                if (this.playoutChannel != value) {
                    this.playoutChannel = value;
                    this.on_PropertyChanged();
                    this.selectPlayer(value);
                }
            }
        }
        private VentuzScenes.MediaPlayer player;

        private bool repressPropertyChanged = false;

        #endregion

        #region Funktionen

        public Business(
            SynchronizationContext syncContext,
            VentuzScenes.Insert.Business insertMasterScene,
            VentuzScenes.Fullscreen.Business fullscreenMasterScene) {
            this.syncContext = syncContext;
            this.insertMasterScene = insertMasterScene;
            this.fullscreenMasterScene = fullscreenMasterScene;
            this.selectPlayer(this.PlayoutChannel);
        }

        private void selectPlayer(
            PlayoutChannelElements value) {
            switch (value) {
                case PlayoutChannelElements.Insert:
                    this.player = this.insertMasterScene.MediaPlayer;
                    this.fullscreenMasterScene.MediaPlayer.Clear();
                    break;
                case PlayoutChannelElements.Fullscreen:
                    this.player = this.fullscreenMasterScene.MediaPlayer;
                    this.insertMasterScene.MediaPlayer.Clear();
                    break;
            }
        }

        public void Play() {
            if (this.player is VentuzScenes.MediaPlayer &&
                this.SelectedDataset is DatasetContent &&
                this.SelectedDataset.IsValid) this.player.Play();
        }

        public void Pause() {
            if (this.player is VentuzScenes.MediaPlayer) this.player.Stop();
        }

        public void Eject() {
            if (this.player is VentuzScenes.MediaPlayer) {
                this.player.Clear();
                this.player.SetFilename(string.Empty);
                this.SelectedDatasetIndex = -1;
                this.on_PropertyChanged("SelectedDatasetIndex");
                this.SelectedDataset = null;
                this.on_PropertyChanged("SelectedDataset");
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
            if (index < 0 ||
                index >= this.dataList.Count) this.Eject();
            else {
                this.SelectedDatasetIndex = index;
                this.on_PropertyChanged("SelectedDatasetIndex");
                this.SelectedDataset = this.GetDataset(index);
                this.on_PropertyChanged("SelectedDataset");
                if (this.player is VentuzScenes.MediaPlayer &&
                    this.SelectedDataset is DatasetContent &&
                    this.SelectedDataset.IsValid) {
                    this.player.SetFilename(this.SelectedDataset.Filename);
                    this.player.Set();
                }
            }
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
            if (dataset == this.SelectedDataset) this.Eject();
            if (dataset is DatasetContent &&
                this.dataList.Contains(dataset)) {
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

        #endregion

        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_PropertyChanged([CallerMemberName]string callerName = "") { if (!this.repressPropertyChanged) Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected void on_PropertyChanged(PropertyChangedEventArgs e) { if (!this.repressPropertyChanged) Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { if (!this.repressPropertyChanged) Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        #endregion

        #region Events.Incoming

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
        }

        #endregion

    }
}
