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

using Cliparts.Serialization;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TrueOrFalseMultiple;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TrueOrFalseMultiple {

    public enum SolutionValues { True, False }

    public class Data
    {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetItem : INotifyPropertyChanged
    {

        #region Properties

        private string text = "?";
        public string Text
        {
            get { return this.text; }
            set
            {
                if (this.text != value)
                {
                    if (string.IsNullOrEmpty(value)) this.text = string.Empty;
                    else this.text = value;
                    this.on_PropertyChanged();
                    this.on_PropertyChanged("ItemList");
                    if (string.IsNullOrEmpty(this.HostText)) this.HostText = this.Text;
                }
            }
        }

        private string hostText = string.Empty;
        public string HostText
        {
            get { return this.hostText; }
            set
            {
                if (this.hostText != value)
                {
                    if (string.IsNullOrEmpty(value)) this.hostText = string.Empty;
                    else this.hostText = Regex.Replace(value, "(?<!\r)\n", "\r\n");
                    this.on_PropertyChanged();
                }
            }
        }

        private SolutionValues solution = SolutionValues.True;
        public SolutionValues Solution
        {
            get { return this.solution; }
            set
            {
                if (this.solution != value)
                {
                    this.solution = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private SelectionValues selectionLeft = SelectionValues.NotAvailable;
        public SelectionValues SelectionLeft
        {
            get { return this.selectionLeft; }
            set
            {
                if (this.selectionLeft != value)
                {
                    this.selectionLeft = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private SelectionValues selectionRight = SelectionValues.NotAvailable;
        public SelectionValues SelectionRight
        {
            get { return this.selectionRight; }
            set
            {
                if (this.selectionRight != value)
                {
                    this.selectionRight = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetItem() { }

        public void Clone(
            DatasetItem source)
        {
            if (source is DatasetItem)
            {
                this.Text = source.Text;
                this.HostText = source.HostText;
                this.Solution = source.Solution;
                this.SelectionLeft = source.SelectionLeft;
                this.SelectionRight = source.SelectionRight;
            }
            else
            {
                this.Text = "?";
                this.HostText = string.Empty;
                this.Solution = SolutionValues.True;
                this.SelectionLeft = SelectionValues.NotAvailable;
                this.SelectionRight = SelectionValues.NotAvailable;
            }
        }

        public void Reset()
        {
            this.SelectionLeft = SelectionValues.NotAvailable;
            this.SelectionRight = SelectionValues.NotAvailable;
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

    public class DatasetContent : Messaging.Message, INotifyPropertyChanged
    {

        #region Properties

        private string name = string.Empty;
        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    if (string.IsNullOrEmpty(value)) value = "?";
                    else this.name = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string text = string.Empty;
        public string Text
        {
            get { return this.text; }
            set
            {
                if (this.text != value)
                {
                    if (string.IsNullOrEmpty(value)) this.text = string.Empty;
                    else this.text = Regex.Replace(value, "(?<!\r)\n", "\r\n"); ;
                    this.on_PropertyChanged();
                    if (string.IsNullOrEmpty(this.HostText)) this.HostText = this.Text;
                }
            }
        }

        private string hostText = string.Empty;
        public string HostText
        {
            get { return this.hostText; }
            set
            {
                if (this.hostText != value)
                {
                    if (string.IsNullOrEmpty(value)) this.hostText = string.Empty;
                    else this.hostText = Regex.Replace(value, "(?<!\r)\n", "\r\n");
                    this.on_PropertyChanged();
                }
            }
        }

        private List<DatasetItem> itemList = new List<DatasetItem>();
        public DatasetItem[] ItemList
        {
            get { return this.itemList.ToArray(); }
            set
            {
                this.RemoveAllItems();
                if (value is DatasetItem[]) foreach (DatasetItem item in value) this.tryAddItem(item);
                this.on_PropertyChanged("ItemList");
            }
        }

        public int ItemsCount { get { return this.itemList.Count; } }

        #endregion


        #region Funktionen

        public DatasetContent()
        {
            this.Name = "?";
        }

        public DatasetItem GetItem(
            int index)
        {
            if (index >= 0 &&
                index < this.itemList.Count) return this.itemList[index];
            else return null;
        }

        public void AddItem(
            DatasetItem newItem)
        {
            if (this.tryAddItem(newItem)) this.on_PropertyChanged("ItemList");
        }
        private bool tryAddItem(
            DatasetItem newItem)
        {
            if (newItem is DatasetItem &&
                !this.itemList.Contains(newItem))
            {
                newItem.PropertyChanged += this.on_PropertyChanged;
                this.itemList.Add(newItem);
                return true;
            }
            else return false;
        }

        public bool TryMoveItemUp(
            int index)
        {
            if (index > 0 &&
                index < this.ItemsCount)
            {
                DatasetItem item = this.GetItem(index);
                this.itemList.RemoveAt(index);
                this.itemList.Insert(index - 1, item);
                this.on_PropertyChanged("ItemList");
                return true;
            }
            else return false;
        }
        public bool TryMoveItemDown(
            int index)
        {
            if (index >= 0 &&
                index < this.ItemsCount - 1)
            {
                DatasetItem item = this.GetItem(index);
                this.itemList.RemoveAt(index);
                this.itemList.Insert(index + 1, item);
                this.on_PropertyChanged("ItemList");
                return true;
            }
            else return false;
        }


        public void RemoveAllItems()
        {
            if (this.tryRemoveAllItems()) this.on_PropertyChanged("ItemList");
        }
        private bool tryRemoveAllItems()
        {
            bool itemRemoved = false;
            DatasetItem[] itemList = this.itemList.ToArray();
            foreach (DatasetItem item in itemList) itemRemoved = this.removeItem(item) | itemRemoved;
            return itemRemoved;
        }
        public bool TryRemoveItem(
            int index)
        {
            if (this.removeItem(this.GetItem(index)))
            {
                this.on_PropertyChanged("ItemList");
                return true;
            }
            else return false;
        }
        private bool removeItem(
            DatasetItem dataset)
        {
            if (dataset is DatasetItem &&
            this.itemList.Contains(dataset))
            {
                dataset.PropertyChanged -= this.on_PropertyChanged;
                this.itemList.Remove(dataset);
                return true;
            }
            else return false;
        }

        public void Reset()
        {
            foreach (var item in this.itemList) item.Reset();
        }

        public override string ToString() { return this.name; }

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

    public class Business : _Base.Score.Business {

        #region Properties

        private int gamePositionX = 0;
        public int GamePositionX
        {
            get { return this.gamePositionX; }
            set
            {
                if (this.gamePositionX != value)
                {
                    this.gamePositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetGame();
                }
            }
        }

        private int gamePositionY = 0;
        public int GamePositionY
        {
            get { return this.gamePositionY; }
            set
            {
                if (this.gamePositionY != value)
                {
                    this.gamePositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetGame();
                }
            }
        }

        private int countDownDuration = 4;
        public int CountDownDuration
        {
            get { return this.countDownDuration; }
            set
            {
                if (this.countDownDuration != value)
                {
                    if (value < 3) this.countDownDuration = 3;
                    else if (value > 5) this.countDownDuration = 5;
                    else this.countDownDuration = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string filename = string.Empty;
        public string Filename
        {
            get { return this.filename; }
            set { if (this.filename != value) this.Load(value); }
        }

        private List<DatasetContent> dataList = new List<DatasetContent>();
        [NotSerialized]
        public DatasetContent[] DataList
        {
            get { return this.dataList.ToArray(); }
            set
            {
                this.tryRemoveAllDatasets();
                if (value is DatasetContent[])
                {
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
        public int SelectedDatasetItemIndex { get; private set; }

        public bool IsLastItem
        {
            get
            {
                return (this.SelectedDataset is DatasetContent &&
                    this.SelectedDatasetItemIndex == this.SelectedDataset.ItemsCount - 1);
            }
        }

        private int taskCounter = 0;
        public int TaskCounter
        {
            get { return this.taskCounter; }
            set
            {
                if (this.taskCounter != value)
                {
                    if (value < 0) value = 0;
                    if (!this.SampleIncluded &&
                        value < 1) value = 1;
                    this.taskCounter = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool sampleIncluded = true;
        public bool SampleIncluded
        {
            get { return this.sampleIncluded; }
            set
            {
                if (this.sampleIncluded != value)
                {
                    this.sampleIncluded = value;
                    this.on_PropertyChanged();
                    // TaskCounter validieren
                    int id = this.TaskCounter;
                    this.TaskCounter = -1;
                    this.TaskCounter = id;
                }
            }
        }

        private SelectionValues leftPlayerInput;
        [NotSerialized]
        public SelectionValues LeftPlayerInput
        {
            get { return this.leftPlayerInput; }
            set
            {
                if (this.leftPlayerInput != value)
                {
                    this.leftPlayerInput = value;
                    this.on_PropertyChanged();
                    this.rateLeftPlayerInput();
                    this.Vhost_SetInput();
                    this.Vleftplayer_SetInput();
                }
            }
        }
        private bool? leftPlayerCorrectInput = null;
        public bool? LeftPlayerCorrectInput
        {
            get { return this.leftPlayerCorrectInput; }
            private set
            {
                if (this.leftPlayerCorrectInput != value)
                {
                    this.leftPlayerCorrectInput = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private SelectionValues rightPlayerInput;
        [NotSerialized]
        public SelectionValues RightPlayerInput
        {
            get { return this.rightPlayerInput; }
            set
            {
                if (this.rightPlayerInput != value)
                {
                    this.rightPlayerInput = value;
                    this.on_PropertyChanged();
                    this.rateRightPlayerInput();
                    this.Vhost_SetInput();
                    this.Vrightplayer_SetInput();
                }
            }
        }
        private bool? rightPlayerCorrectInput = null;
        public bool? RightPlayerCorrectInput
        {
            get { return this.rightPlayerCorrectInput; }
            private set
            {
                if (this.rightPlayerCorrectInput != value)
                {
                    this.rightPlayerCorrectInput = value;
                    this.on_PropertyChanged();
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
                    this.Vhost_SetCounter();
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
                    this.Vhost_SetCounter();
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

        private VentuzScenes.GamePool._Modules.Score.Styles setsStyle = VentuzScenes.GamePool._Modules.Score.Styles.CounterLeft;
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

        private Insert insertScene;
        public override VRemote4.HandlerSi.Scene.States InsertSceneStatus {
            get {
                if (this.insertScene is Insert) return this.insertScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private Host hostScene;
        public override VRemote4.HandlerSi.Scene.States HostSceneStatus
        {
            get
            {
                if (this.hostScene is Host) return this.hostScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private Player leftPlayerScene;
        public override VRemote4.HandlerSi.Scene.States LeftPlayerSceneStatus
        {
            get
            {
                if (this.leftPlayerScene is Player) return this.leftPlayerScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private Player rightPlayerScene;
        public override VRemote4.HandlerSi.Scene.States RightPlayerSceneStatus
        {
            get
            {
                if (this.rightPlayerScene is Player) return this.rightPlayerScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private bool resolving = false;

        private bool repressPropertyChanged = false;

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.TrueOrFalseMultiple'", typeIdentifier);
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

            this.hostScene = new Host(syncContext, hostMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.hostScene.StatusChanged += this.hostScene_StatusChanged;

            this.leftPlayerScene = new Player(syncContext, leftPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.leftPlayerScene.StatusChanged += this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.FalseFired += this.LeftPlayerScene_FalseFired;
            this.leftPlayerScene.TrueFired += this.LeftPlayerScene_TrueFired;

            this.rightPlayerScene = new Player(syncContext, rightPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.rightPlayerScene.StatusChanged += this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.FalseFired += this.RightPlayerScene_FalseFired;
            this.rightPlayerScene.TrueFired += this.RightPlayerScene_TrueFired;

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
            this.leftPlayerScene.FalseFired -= this.LeftPlayerScene_FalseFired;
            this.leftPlayerScene.TrueFired -= this.LeftPlayerScene_TrueFired;
            this.leftPlayerScene.Dispose();

            this.rightPlayerScene.StatusChanged -= this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.FalseFired -= this.RightPlayerScene_FalseFired;
            this.rightPlayerScene.TrueFired -= this.RightPlayerScene_TrueFired;
            this.rightPlayerScene.Dispose();
        }

        public override void ResetData() {
            base.ResetData();
            this.resolving = false;
            foreach (var item in this.dataList) item.Reset();
            this.LeftPlayerCounter = 0;
            this.LeftPlayerInput = SelectionValues.NotAvailable;
            this.RightPlayerCounter = 0;
            this.RightPlayerInput = SelectionValues.NotAvailable;
            this.SelectDataset(0);
        }

        public override void Next() {
            base.Next();
            if (this.SelectedDatasetIndex < this.dataList.Count - 1)
            {
                this.resolving = false;
                this.SelectDataset(this.SelectedDatasetIndex + 1);
                this.LeftPlayerCounter = 0;
                this.LeftPlayerInput = SelectionValues.NotAvailable;
                this.RightPlayerCounter = 0;
                this.RightPlayerInput = SelectionValues.NotAvailable;
            }
        }

        public void NextItem(
            bool resolving)
        {
            this.resolving = resolving;
            if (this.SelectedDataset is DatasetContent &&
                this.SelectedDatasetItemIndex < this.SelectedDataset.ItemsCount - 1)
            {
                this.SelectDatasetItem(this.SelectedDatasetItemIndex + 1);
                this.SetInput(resolving);
            }
        }

        private void SetInput(
            bool resolving)
        {
            this.resolving = resolving;
            if (this.resolving &&
                this.SelectedDatasetItem is DatasetItem)
            {
                //this.LeftPlayerInput = this.SelectedDatasetItem.SelectionLeft;
                //this.RightPlayerInput = this.SelectedDatasetItem.SelectionRight;
            }
            else
            {
                this.LeftPlayerInput = SelectionValues.NotAvailable;
                this.RightPlayerInput = SelectionValues.NotAvailable;
            }
        }

        internal void InitResolve()
        {
            this.resolving = true;
            this.LeftPlayerCounter = 0;
            this.RightPlayerCounter = 0;
            this.SelectDatasetItem(0);
            this.SetInput(true);
            this.LeftPlayerInput = SelectionValues.NotAvailable;
            this.RightPlayerInput = SelectionValues.NotAvailable;
            this.Vinsert_AllItemsIn(false);
            this.Vhost_AllItemsIn(false);
            this.Vleftplayer_AllItemsIn(false);
            this.Vrightplayer_AllItemsIn(false);
        }

        internal void ResolveItem()
        {
            if (this.SelectedDatasetItem is DatasetItem)
            {
                if (this.SelectedDatasetItem.Solution.ToString().Equals(this.SelectedDatasetItem.SelectionLeft.ToString())) this.LeftPlayerCounter++;
                if (this.SelectedDatasetItem.Solution.ToString().Equals(this.SelectedDatasetItem.SelectionRight.ToString())) this.RightPlayerCounter++;
                //switch (this.LeftPlayerInput)
                //{
                //    case SelectionValues.True:
                //        if (this.SelectedDatasetItem.Solution == SolutionValues.True) this.LeftPlayerCounter++;
                //        break;
                //    case SelectionValues.False:
                //        if (this.SelectedDatasetItem.Solution == SolutionValues.False) this.LeftPlayerCounter++;
                //        break;
                //}
                //switch (this.RightPlayerInput)
                //{
                //    case SelectionValues.True:
                //        if (this.SelectedDatasetItem.Solution == SolutionValues.True) this.RightPlayerCounter++;
                //        break;
                //    case SelectionValues.False:
                //        if (this.SelectedDatasetItem.Solution == SolutionValues.False) this.RightPlayerCounter++;
                //        break;
                //}
            }
            //if (this.LeftPlayerCorrectInput.HasValue &&
            //    this.LeftPlayerCorrectInput.Value == true) this.LeftPlayerCounter++;
            //else this.LeftPlayerCounter--;
            //if (this.RightPlayerCorrectInput.HasValue &&
            //    this.RightPlayerCorrectInput.Value == true) this.RightPlayerCounter++;
            //else this.RightPlayerCounter--;
        }

        internal void ResolveSet()
        {
            if (this.SelectedDatasetIndex > 0)
            {
                if (this.LeftPlayerCounter > this.RightPlayerCounter) this.LeftPlayerScore++;
                else if (this.RightPlayerCounter > this.LeftPlayerCounter) this.RightPlayerScore++;
                else
                {
                    this.LeftPlayerScore++;
                    this.RightPlayerScore++;
                }
            }
        }

        public DatasetContent GetDataset(
            int index)
        {
            if (index >= 0 &&
                index < this.dataList.Count) return this.dataList[index];
            else return null;
        }

        public int GetDatasetIndex(
            DatasetContent dataset)
        {
            int index = -1;
            int datasetIndex = 0;
            foreach (DatasetContent item in this.dataList)
            {
                if (item == dataset)
                {
                    index = datasetIndex;
                    break;
                }
                datasetIndex++;
            }
            return index;
        }

        public void SelectDataset(
            int index)
        {
            if (index < 0) index = 0;
            if (index >= this.dataList.Count) index = this.dataList.Count - 1;
            this.SelectedDatasetIndex = index;
            this.on_PropertyChanged("SelectedDatasetIndex");
            this.SelectedDataset = this.GetDataset(index);
            this.on_PropertyChanged("SelectedDataset");
            this.SelectDatasetItem(0);
        }

        public void SelectDatasetItem(
            int index)
        {
            this.SelectedDatasetItem = null;
            this.SelectedDatasetItemIndex = -1;
            if (this.SelectedDataset is DatasetContent)
            {
                this.SelectedDatasetItem = this.SelectedDataset.GetItem(index);
                if (this.SelectedDatasetItem is DatasetItem) this.SelectedDatasetItemIndex = index;
            }
            this.on_PropertyChanged("SelectedDatasetItem");
            this.rateLeftPlayerInput();
            this.rateRightPlayerInput();
        }


        public void AddDataset(
            DatasetContent newDataset,
            int insertIndex)
        {
            if (this.tryAddDataset(newDataset, insertIndex)) this.on_PropertyChanged("NameList");
            if (this.SelectedDataset == null) this.SelectDataset(0);
            this.Save();
        }
        private bool tryAddDataset(
            DatasetContent newDataset,
            int insertIndex)
        {
            if (newDataset is DatasetContent &&
                !this.dataList.Contains(newDataset))
            {
                newDataset.Error += this.dataset_Error;
                newDataset.PropertyChanged += this.dataset_PropertyChanged;
                if (insertIndex >= 0 &&
                    insertIndex < this.DatasetsCount)
                {
                    this.dataList.Insert(insertIndex, newDataset);
                    this.names.Insert(insertIndex, newDataset.ToString());
                }
                else
                {
                    this.dataList.Add(newDataset);
                    this.names.Add(newDataset.ToString());
                }
                return true;
            }
            else return false;
        }

        public bool TryMoveDatasetUp(
            int index)
        {
            if (index > 0 &&
                index < this.DatasetsCount)
            {
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
            int index)
        {
            if (index >= 0 &&
                index < this.DatasetsCount - 1)
            {
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

        public void ResortAllDatasets()
        {
            if (this.DatasetsCount > 1)
            {
                List<DatasetContent> dataList = new List<DatasetContent>();
                if (this.SampleIncluded)
                {
                    dataList.Add(this.dataList[0]);
                    for (int i = this.dataList.Count - 1; i > 0; i--) dataList.Add(this.dataList[i]);
                }
                else
                {
                    for (int i = this.dataList.Count - 1; i >= 0; i--) dataList.Add(this.dataList[i]);
                }
                this.dataList.Clear();
                foreach (DatasetContent item in dataList) this.dataList.Add(item);
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                this.Save();
            }
        }

        public void RemoveAllDatasets()
        {
            if (this.tryRemoveAllDatasets())
            {
                this.SelectDataset(0);
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                this.Save();
            }
        }
        private bool tryRemoveAllDatasets()
        {
            bool datasetRemoved = false;
            DatasetContent[] datasetList = this.dataList.ToArray();
            foreach (DatasetContent item in datasetList) datasetRemoved = this.removeDataset(item) | datasetRemoved;
            return datasetRemoved;
        }
        public bool TryRemoveDataset(
            int index)
        {
            if (this.removeDataset(this.GetDataset(index)))
            {
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                if (index <= this.SelectedDatasetIndex) this.SelectDataset(this.SelectedDatasetIndex);
                this.Save();
                return true;
            }
            else return false;
        }
        private bool removeDataset(
            DatasetContent dataset)
        {
            if (dataset is DatasetContent &&
                this.dataList.Contains(dataset))
            {
                dataset.Error -= this.dataset_Error;
                dataset.PropertyChanged -= this.dataset_PropertyChanged;
                this.dataList.Remove(dataset);
                return true;
            }
            else return false;
        }

        private void buildNameList()
        {
            this.names.Clear();
            this.repressPropertyChanged = true;
            foreach (DatasetContent item in this.dataList)
            {
                this.names.Add(item.ToString());
            }
            this.repressPropertyChanged = false;
        }

        private void rateLeftPlayerInput()
        {
            if (this.SelectedDatasetItem is DatasetItem)
            {
                if (!resolving) this.SelectedDatasetItem.SelectionLeft = this.LeftPlayerInput;
                switch (this.LeftPlayerInput)
                {
                    case SelectionValues.True:
                        if (this.SelectedDatasetItem.Solution == SolutionValues.True) this.LeftPlayerCorrectInput = true;
                        else this.LeftPlayerCorrectInput = false;
                        break;
                    case SelectionValues.False:
                        if (this.SelectedDatasetItem.Solution == SolutionValues.False) this.LeftPlayerCorrectInput = true;
                        else this.LeftPlayerCorrectInput = false;
                        break;
                    case SelectionValues.NotAvailable:
                    default:
                        this.LeftPlayerCorrectInput = null;
                        break;
                }
            }
            else this.LeftPlayerCorrectInput = null;
        }

        private void rateRightPlayerInput()
        {
            if (this.SelectedDatasetItem is DatasetItem)
            {
                if (!resolving) this.SelectedDatasetItem.SelectionRight = this.RightPlayerInput;
                switch (this.RightPlayerInput)
                {
                    case SelectionValues.True:
                        if (this.SelectedDatasetItem.Solution == SolutionValues.True) this.RightPlayerCorrectInput = true;
                        else this.RightPlayerCorrectInput = false;
                        break;
                    case SelectionValues.False:
                        if (this.SelectedDatasetItem.Solution == SolutionValues.False) this.RightPlayerCorrectInput = true;
                        else this.RightPlayerCorrectInput = false;
                        break;
                    case SelectionValues.NotAvailable:
                    default:
                        this.RightPlayerCorrectInput = null;
                        break;
                }
            }
            else this.RightPlayerCorrectInput = null;
        }


        public void Load(
            string filename)
        {
            string subSender = "Load";
            if (File.Exists(filename))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Data));
                    Data data;
                    using (StreamReader reader = new StreamReader(filename)) data = (Data)serializer.Deserialize(reader);
                    this.DataList = data.DataList;
                    this.SelectDataset(data.SelectedIndex);
                    this.filename = filename;
                    this.on_PropertyChanged("Filename");
                }
                catch (Exception exc)
                {
                    // Fehler weitergeben
                    this.on_Error(subSender, exc.Message);
                }
            }
            else
            {
                // Fehler weitergeben
                this.on_Error(subSender, "Datei nicht vorhanden - '" + filename + "'");
            }
        }
        public void Save() { if (File.Exists(this.Filename)) this.SaveAs(this.Filename); }
        public void SaveAs(
            string filename)
        {
            string subSender = "SaveAs";
            try
            {
                // Dokument speichern
                Data data = new Data();
                data.DataList = this.DataList;
                data.SelectedIndex = this.SelectedDatasetIndex;
                XmlSerializer serializer = new XmlSerializer(typeof(Data));
                using (StreamWriter writer = new StreamWriter(filename)) serializer.Serialize(writer, data);
                this.filename = filename;
                this.on_PropertyChanged("Filename");
            }
            catch (Exception exc)
            {
                // Fehler weitergeben
                this.on_Error(subSender, exc.Message);
            }
        }


        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }

        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }

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

        public void Vinsert_GameIn()
        {
            this.Vinsert_SetGame(this.insertScene.Game, this.SelectedDataset);
            if (this.insertScene is Insert &&
                this.insertScene.Game is Game)
            {
                this.insertScene.Game.ResetAnswers();
                this.insertScene.Game.ToIn();
            }
        }
        public void Vinsert_SetGame() { if (this.insertScene is Insert) this.Vinsert_SetGame(this.insertScene.Game, this.SelectedDataset); }
        public void Vinsert_SetGame(
            Game scene,
            DatasetContent dataset)
        {
            if (scene is Game)
            {
                scene.SetPositionX(this.GamePositionX);
                scene.SetPositionY(this.GamePositionY);
                if (dataset is DatasetContent)
                {
                    scene.SetText(dataset.Text);
                    for (int i = 0; i< dataset.ItemsCount; i++)
                    {
                        var item = dataset.GetItem(i);
                        if (item is DatasetItem)
                        {
                            scene.SetAnswerText(i + 1, item.Text);
                        }
                        else
                        {
                            scene.SetAnswerText(i + 1, string.Empty);
                        }
                    }
                }
                else scene.SetText(string.Empty);
            }
        }
        internal void Vinsert_ItemIn()
        {
            if (this.insertScene is Insert)
            {
                this.Vinsert_SetItemsSelection(this.insertScene.Game, this.SelectedDataset, this.SelectedDatasetItemIndex - 1);
                this.Vinsert_SetItemsSolution(this.insertScene.Game, this.SelectedDataset, - 1);
                this.insertScene.Game.SetAnswerTextColor(this.SelectedDatasetItemIndex + 1, Game.ColorValues.Countdown);
                this.insertScene.Game.ResetCountDown();
                this.insertScene.Game.PlaySound(Game.SoundValues.Wusch);
                this.insertScene.Game.AnswerToIn(this.SelectedDatasetItemIndex + 1);
            }
        }
        internal void Vinsert_AllItemsIn(
            bool resolved)
        {
            if (this.insertScene is Insert)
            {
                this.Vinsert_SetItemsSelection(this.insertScene.Game, this.SelectedDataset, this.SelectedDataset.ItemsCount);
                if (resolved) this.Vinsert_SetItemsSolution(this.insertScene.Game, this.SelectedDataset, this.SelectedDataset.ItemsCount);
                else
                {
                    this.Vinsert_SetItemsSolution(this.insertScene.Game, this.SelectedDataset, -1);
                    this.insertScene.Game.SetAnswerTextColor(this.SelectedDatasetItemIndex + 1, Game.ColorValues.Neutral);
                }
                this.insertScene.Game.AnswerToIn(this.SelectedDataset.ItemsCount);
            }
        }
        public void Vinsert_SetItemsSelection(
            Game scene,
            DatasetContent dataset,
            int lastItemIndex)
        {
            if (scene is Game &&
                dataset is DatasetContent)
            {
                for (int i = 0; i < dataset.ItemsCount; i++)
                {
                    DatasetItem item = null;
                    if (i <= lastItemIndex) item = dataset.GetItem(i);
                    if (item is DatasetItem)
                    {
                        switch (item.SelectionLeft)
                        {
                            case SelectionValues.True:
                                scene.SetLeftSelectionColor(i + 1, Game.ColorValues.Green);
                                break;
                            case SelectionValues.False:
                                scene.SetLeftSelectionColor(i + 1, Game.ColorValues.Red);
                                break;
                            case SelectionValues.NotAvailable:
                            default:
                                scene.SetLeftSelectionColor(i + 1, Game.ColorValues.Neutral);
                                break;
                        }
                        switch (item.SelectionRight)
                        {
                            case SelectionValues.True:
                                scene.SetRightSelectionColor(i + 1, Game.ColorValues.Green);
                                break;
                            case SelectionValues.False:
                                scene.SetRightSelectionColor(i + 1, Game.ColorValues.Red);
                                break;
                            case SelectionValues.NotAvailable:
                            default:
                                scene.SetRightSelectionColor(i + 1, Game.ColorValues.Neutral);
                                break;
                        }
                    }
                    else
                    {
                        scene.SetLeftSelectionColor(i + 1, Game.ColorValues.Neutral);
                        scene.SetRightSelectionColor(i + 1, Game.ColorValues.Neutral);
                    }
                }
            }
        }
        public void Vinsert_SetItemsSolution(
            Game scene,
            DatasetContent dataset,
            int lastItemIndex)
        {
            if (scene is Game &&
                dataset is DatasetContent)
            {
                for (int i = 0; i < dataset.ItemsCount; i++)
                {
                    DatasetItem item = null;
                    if (i <= lastItemIndex) item = dataset.GetItem(i);
                    if (item is DatasetItem)
                    {
                        switch (item.Solution)
                        {
                            case SolutionValues.True:
                                scene.SetAnswerTextColor(i + 1, Game.ColorValues.Green);
                                break;
                            case SolutionValues.False:
                                scene.SetAnswerTextColor(i + 1, Game.ColorValues.Red);
                                break;
                            default:
                                scene.SetAnswerTextColor(i + 1, Game.ColorValues.Neutral);
                                break;
                        }
                    }
                    else
                    {
                        scene.SetAnswerTextColor(i + 1, Game.ColorValues.Neutral);
                    }
                }
            }
        }
        internal void Vinsert_StartCountdown()
        {
            if (this.insertScene is Insert) this.insertScene.Game.StartCountDown(this.CountDownDuration);
        }
        internal void Vinsert_SetInput()
        {
            if (this.insertScene is Insert)
            {
                this.Vinsert_SetItemsSelection(this.insertScene.Game, this.SelectedDataset, this.SelectedDatasetItemIndex);
                this.insertScene.Game.PlaySound(Game.SoundValues.Plopp);
            }

        }
        internal void Vinsert_SolutionIn()
        {
            if (this.insertScene is Insert)
            {
                this.Vinsert_SetItemsSolution(this.insertScene.Game, this.SelectedDataset, this.SelectedDatasetItemIndex);
                this.insertScene.Game.PlaySound(Game.SoundValues.Pling);
            }
        }
        internal void Vinsert_GameOut()
        {
            if (this.insertScene is Insert) this.insertScene.Game.ToOut();
        }

        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            if (this.insertScene is Insert) this.insertScene.Unload();
        }

        public override void ClearGraphic() {
            base.ClearGraphic();
            if (this.insertScene is Insert) this.insertScene.Clear();
        }

        public override void Vhost_LoadScene() { this.hostScene.Load(); }
        internal void Vhost_GameIn()
        {
            if (this.hostScene is Host)
            {
                this.Vhost_SetCounter();
                this.Vhost_SetGame(this.hostScene, this.SelectedDataset);
                this.Vhost_SetItemsSelection(this.hostScene, this.SelectedDataset, -1);
                this.Vhost_SetItemsSolution(this.hostScene, this.SelectedDataset, -1);
                this.hostScene.ToIn();
            }
        }
        public void Vhost_SetGame(
            Host scene,
            DatasetContent dataset)
        {
            if (scene is Host)
            {
                if (dataset is DatasetContent) scene.SetText(dataset.HostText);
                else scene.SetText(string.Empty);
                for (int i = 0; i < Game.ItemsCount ; i++)
                {
                    if (dataset is DatasetContent &&
                        dataset.ItemsCount > i)
                    {
                        DatasetItem item = dataset.GetItem(i);
                        if (item is DatasetItem) {
                            scene.SetAnswerText(i + 1, item.HostText);
                        }
                        else
                        {
                            scene.SetAnswerText(i + 1, string.Empty);
                        }
                        scene.SetAnswerBlocked(i + 1, false);
                    }
                    else scene.SetAnswerBlocked(i + 1, true);
                }
            }
        }
        public void Vhost_SetItemsSelection(
            Host scene,
            DatasetContent dataset,
            int lastItemIndex)
        {
            if (scene is Host &&
                dataset is DatasetContent)
            {
                for (int i = 0; i < dataset.ItemsCount; i++)
                {
                    DatasetItem item = null;
                    if (i <= lastItemIndex) item = dataset.GetItem(i);
                    if (item is DatasetItem)
                    {
                        switch (item.SelectionLeft)
                        {
                            case SelectionValues.True:
                                scene.SetLeftSelectionColor(i + 1, Host.ColorValues.Green);
                                break;
                            case SelectionValues.False:
                                scene.SetLeftSelectionColor(i + 1, Host.ColorValues.Red);
                                break;
                            case SelectionValues.NotAvailable:
                            default:
                                scene.SetLeftSelectionColor(i + 1, Host.ColorValues.Neutral);
                                break;
                        }
                        switch (item.SelectionRight)
                        {
                            case SelectionValues.True:
                                scene.SetRightSelectionColor(i + 1, Host.ColorValues.Green);
                                break;
                            case SelectionValues.False:
                                scene.SetRightSelectionColor(i + 1, Host.ColorValues.Red);
                                break;
                            case SelectionValues.NotAvailable:
                            default:
                                scene.SetRightSelectionColor(i + 1, Host.ColorValues.Neutral);
                                break;
                        }
                    }
                    else
                    {
                        scene.SetLeftSelectionColor(i + 1, Host.ColorValues.Neutral);
                        scene.SetRightSelectionColor(i + 1, Host.ColorValues.Neutral);
                    }
                }
            }
        }
        public void Vhost_SetItemsSolution(
            Host scene,
            DatasetContent dataset,
            int lastItemIndex)
        {
            if (scene is Host &&
                dataset is DatasetContent)
            {
                for (int i = 0; i < dataset.ItemsCount; i++)
                {
                    DatasetItem item = null;
                    if (i <= lastItemIndex) item = dataset.GetItem(i);
                    if (item is DatasetItem)
                    {
                        switch (item.Solution)
                        {
                            case SolutionValues.True:
                                scene.SetAnswerTextColor(i + 1, Host.ColorValues.Green);
                                break;
                            case SolutionValues.False:
                                scene.SetAnswerTextColor(i + 1, Host.ColorValues.Red);
                                break;
                            default:
                                scene.SetAnswerTextColor(i + 1, Host.ColorValues.Neutral);
                                break;
                        }
                    }
                    else
                    {
                        scene.SetAnswerTextColor(i + 1, Host.ColorValues.Neutral);
                    }
                }
            }
        }
        internal void Vhost_SetInput()
        {
            this.Vhost_SetItemsSelection(this.hostScene, this.SelectedDataset, this.SelectedDatasetItemIndex);
        }
        internal void Vhost_SolutionIn()
        {
            this.Vhost_SetItemsSolution(this.hostScene, this.SelectedDataset, this.SelectedDatasetItemIndex);
        }
        internal void Vhost_AllItemsIn(
            bool resolved)
        {
            if (this.hostScene is Host)
            {
                this.Vhost_SetItemsSelection(this.hostScene, this.SelectedDataset, this.SelectedDataset.ItemsCount);
                if (resolved) this.Vhost_SetItemsSolution(this.hostScene, this.SelectedDataset, this.SelectedDataset.ItemsCount);
                else
                {
                    this.Vhost_SetItemsSolution(this.hostScene, this.SelectedDataset, -1);
                }
            }
        }
        internal void Vhost_GameOut()
        {
            if (this.hostScene is Host) this.hostScene.ToOut();
        }
        internal void Vhost_SetCounter()
        {
            if (this.hostScene is Host)
            {
                this.hostScene.SetLeftCounter(this.LeftPlayerCounter);
                this.hostScene.SetRightCounter(this.RightPlayerCounter);
            }
        }
        public override void Vhost_UnloadScene() { if (this.hostScene is Host) this.hostScene.Unload(); }

        public override void Vleftplayer_LoadScene() { if (this.leftPlayerScene is Player) this.leftPlayerScene.Load(); }
        internal void Vleftplayer_GameIn()
        {
            this.Vplayer_SetGame(this.leftPlayerScene, this.SelectedDataset);
            this.Vplayer_SetItem(this.leftPlayerScene, this.SelectedDataset, -1);
            this.Vplayer_SetInput(this.leftPlayerScene, this.SelectedDataset, -1);
            this.Vplayer_SetSolution(this.leftPlayerScene, this.SelectedDataset, -1);
            this.Vplayer_ResetCountDown(this.leftPlayerScene, this.CountDownDuration);
            if (this.leftPlayerScene is Player) this.leftPlayerScene.ToIn();
        }
        internal void Vleftplayer_ItemIn()
        {
            this.Vplayer_SetItem(this.leftPlayerScene, this.SelectedDataset, this.SelectedDatasetItemIndex);
            this.Vplayer_SetInput(this.leftPlayerScene, this.SelectedDataset, this.SelectedDatasetItemIndex - 1);
            this.Vplayer_SetSolution(this.leftPlayerScene, this.SelectedDataset, -1);
            this.Vplayer_ResetCountDown(this.leftPlayerScene, this.CountDownDuration);
        }
        internal void VleftPlayer_StartCountDown()
        {
            this.Vplayer_StartCountDown(this.leftPlayerScene);
        }

        internal void Vleftplayer_SetInput()
        {
            this.Vplayer_SetInput(this.leftPlayerScene, this.SelectedDataset, this.SelectedDatasetItemIndex);
            if (this.resolving) this.Vplayer_SetSolution(this.leftPlayerScene, this.SelectedDataset, this.SelectedDatasetItemIndex - 1);
            else this.Vplayer_SetSolution(this.leftPlayerScene, this.SelectedDataset, -1);
        }
        internal void Vleftplayer_SolutionIn()
        {
            this.Vplayer_SetSolution(this.leftPlayerScene, this.SelectedDataset, this.SelectedDatasetItemIndex);
        }
        internal void Vleftplayer_LockInput()
        {
            this.Vplayer_LockInput(this.leftPlayerScene, this.SelectedDatasetItemIndex);
        }
        internal void Vleftplayer_AllItemsIn(bool resolved)
        {
            if (this.rightPlayerScene is Player) this.Vplayer_AllItemsIn(this.leftPlayerScene, resolved);
        }
        internal void Vleftplayer_GameOut()
        {
            if (this.leftPlayerScene is Player) this.leftPlayerScene.ToOut();
        }
        public override void Vleftplayer_UnloadScene() { if (this.leftPlayerScene is Player) this.leftPlayerScene.Unload(); }

        public override void Vrightplayer_LoadScene() { if (this.rightPlayerScene is Player) this.rightPlayerScene.Load(); }
        internal void Vrightplayer_GameIn()
        {
            this.Vplayer_SetGame(this.rightPlayerScene, this.SelectedDataset);
            this.Vplayer_SetItem(this.rightPlayerScene, this.SelectedDataset, -1);
            this.Vplayer_SetInput(this.rightPlayerScene, this.SelectedDataset, -1);
            this.Vplayer_SetSolution(this.rightPlayerScene, this.SelectedDataset, -1);
            this.Vplayer_ResetCountDown(this.rightPlayerScene, this.CountDownDuration);
            if (this.rightPlayerScene is Player) this.rightPlayerScene.ToIn();
        }
        internal void Vrightplayer_ItemIn()
        {
            this.Vplayer_SetItem(this.rightPlayerScene, this.SelectedDataset, this.SelectedDatasetItemIndex);
            this.Vplayer_SetInput(this.rightPlayerScene, this.SelectedDataset, this.SelectedDatasetItemIndex - 1);
            this.Vplayer_SetSolution(this.rightPlayerScene, this.SelectedDataset, -1);
            this.Vplayer_ResetCountDown(this.rightPlayerScene, this.CountDownDuration);
        }
        internal void Vrightplayer_StartCountDown()
        {
            this.Vplayer_StartCountDown(this.rightPlayerScene);
        }
        internal void Vrightplayer_SetInput()
        {
            this.Vplayer_SetInput(this.rightPlayerScene, this.SelectedDataset, this.SelectedDatasetItemIndex);
            if (this.resolving) this.Vplayer_SetSolution(this.rightPlayerScene, this.SelectedDataset, this.SelectedDatasetItemIndex - 1);
            else this.Vplayer_SetSolution(this.rightPlayerScene, this.SelectedDataset, -1);
        }
        internal void Vrightplayer_SolutionIn()
        {
            this.Vplayer_SetSolution(this.rightPlayerScene, this.SelectedDataset, this.SelectedDatasetItemIndex);
        }
        internal void Vrightplayer_LockInput()
        {
            this.Vplayer_LockInput(this.rightPlayerScene, this.SelectedDatasetItemIndex);
        }
        internal void Vrightplayer_AllItemsIn(bool resolved)
        {
            if (this.rightPlayerScene is Player) this.Vplayer_AllItemsIn(this.rightPlayerScene, resolved);
        }
        internal void Vrightplayer_GameOut()
        {
            if (this.rightPlayerScene is Player) this.rightPlayerScene.ToOut();
        }
        public override void Vrightplayer_UnloadScene() { if (this.rightPlayerScene is Player) this.rightPlayerScene.Unload(); }

        internal void Vplayer_SetGame(
            Player scene,
            DatasetContent dataset)
        {
            if (scene is Player)
            {
                if (dataset is DatasetContent) scene.SetText(dataset.Text);
                else scene.SetText(string.Empty);
            }
        }
        internal void Vplayer_SetItem(
            Player scene,
            DatasetContent dataset,
            int lastItemIndex)
        {
            if (scene is Player)
            {
                for (int i = 0; i < Game.ItemsCount; i++)
                {
                    DatasetItem item = null;
                    if (dataset is DatasetContent && i <= lastItemIndex) item = dataset.GetItem(i);
                    if (item is DatasetItem)
                    {
                        scene.SetAnswerText(i + 1, item.Text);
                        scene.SetAnswerBlocked(i + 1, false);
                    }
                    else
                    {
                        scene.SetAnswerText(i + 1, string.Empty);
                        scene.SetAnswerBlocked(i + 1, true);
                    }
                }
            }
        }
        internal void Vplayer_ResetCountDown(
            Player scene,
            int duration)
        {
            if (scene is Player) scene.ResetCountDown(duration);
        }
        internal void Vplayer_StartCountDown(
            Player scene)
        {
            if (scene is Player) scene.StartCountDown();
        }
        internal void Vplayer_SetInput(
            Player scene,
            DatasetContent dataset,
            int lastItemIndex)
        {
            if (scene is Player)
            {
                for (int i = 0; i < Game.ItemsCount; i++)
                {
                    DatasetItem item = null;
                    if (dataset is DatasetContent && i <= lastItemIndex) item = dataset.GetItem(i);
                    if (item is DatasetItem)
                    {
                        if (scene == this.leftPlayerScene)
                        {
                            switch (item.SelectionLeft)
                            {
                                case SelectionValues.True:
                                    scene.SetAnswerTrue(i + 1);
                                    break;
                                case SelectionValues.False:
                                    scene.SetAnswerFalse(i + 1);
                                    break;
                                case SelectionValues.NotAvailable:
                                default:
                                    //scene.ResetAnswer(i + 1);
                                    scene.LockAnswer(i + 1);
                                    break;
                            }
                        }
                        else
                        {
                            switch (item.SelectionRight)
                            {
                                case SelectionValues.True:
                                    scene.SetAnswerTrue(i + 1);
                                    break;
                                case SelectionValues.False:
                                    scene.SetAnswerFalse(i + 1);
                                    break;
                                case SelectionValues.NotAvailable:
                                default:
                                    //scene.ResetAnswer(i + 1);
                                    scene.LockAnswer(i + 1);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        scene.ResetAnswer(i + 1);
                    }
                }
            }
        }
        internal void Vplayer_SetSolution(
            Player scene,
            DatasetContent dataset, 
            int lastItemIndex)
        {
            if (scene is Player)
            {
                for (int i = 0; i < Game.ItemsCount; i++)
                {
                    DatasetItem item = null;
                    if (dataset is DatasetContent && i <= lastItemIndex) item = dataset.GetItem(i);
                    if (item is DatasetItem)
                    {
                        switch (item.Solution)
                        {
                            case SolutionValues.True:
                                scene.SetAnswerTextColor(i + 1, Player.ColorValues.Green);
                                break;
                            case SolutionValues.False:
                            default:
                                scene.SetAnswerTextColor(i + 1, Player.ColorValues.Red);
                                break;
                        }
                    }
                    else
                    {
                        scene.SetAnswerTextColor(i + 1, Player.ColorValues.Neutral);
                    }
                }
            }
        }
        internal void Vplayer_LockInput(
            Player scene,
            int itemIndex)
        {
            if (scene is Player) scene.LockAnswer(itemIndex + 1);
        }
        internal void Vplayer_AllItemsIn(
            Player scene,
            bool resolved)
        {
            if (scene is Player)
            {
                this.Vplayer_SetItem(scene, this.SelectedDataset, this.SelectedDataset.ItemsCount);
                this.Vplayer_SetInput(scene, this.SelectedDataset, this.SelectedDataset.ItemsCount);
                if (resolved) this.Vplayer_SetSolution(scene, this.SelectedDataset, this.SelectedDataset.ItemsCount);
                else
                {
                    this.Vplayer_SetSolution(scene, this.SelectedDataset, -1);
                }
            }
        }


        #endregion


        #region Events.Outgoing

        protected override void on_PropertyChanged([CallerMemberName] string callerName = "") {
            base.on_PropertyChanged(callerName);
            if (callerName == "FlipPlayers") this.Vinsert_SetCounter();
        }

        #endregion

        #region Events.Incoming

        void dataset_Error(object sender, Messaging.ErrorEventArgs e) { this.on_Error(sender, e); }

        void dataset_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_dataset_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_dataset_PropertyChanged(object content)
        {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs)
            {
                if (e.PropertyName == "Name")
                {
                    this.buildNameList();
                    this.on_PropertyChanged("NameList");
                }
            }
            this.Save();
        }

        private void LeftPlayerScene_FalseFired(object sender, string e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_LeftPlayerScene_FalseFired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_LeftPlayerScene_FalseFired(object content)
        {
            this.LeftPlayerInput = SelectionValues.False;
        }

        private void LeftPlayerScene_TrueFired(object sender, string e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_LeftPlayerScene_TrueFired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_LeftPlayerScene_TrueFired(object content)
        {
            this.LeftPlayerInput = SelectionValues.True;
        }

        private void RightPlayerScene_FalseFired(object sender, string e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_RightPlayerScene_FalseFired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_RightPlayerScene_FalseFired(object content)
        {
            this.RightPlayerInput = SelectionValues.False;
        }

        private void RightPlayerScene_TrueFired(object sender, string e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_RightPlayerScene_TrueFired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_RightPlayerScene_TrueFired(object content)
        {
            this.RightPlayerInput = SelectionValues.True;
        }

        #endregion

    }

}
