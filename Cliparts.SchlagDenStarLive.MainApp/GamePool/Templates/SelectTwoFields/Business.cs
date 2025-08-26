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
using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;
using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SelectTwoFields;
using static Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SelectTwoFields.Game;
using Cliparts.VRemote4.HandlerSi;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.SelectTwoFields {

    public class Data
    {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetContent : Cliparts.Messaging.Message, INotifyPropertyChanged
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
                    if (value == null) value = string.Empty;
                    this.name = value;
                    this.buildToString();
                    this.on_PropertyChanged();
                    if (string.IsNullOrEmpty(this.hostText)) this.HostText = value; 
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
                    if (value == null) value = string.Empty;
                    this.hostText = Regex.Replace(value, "(?<!\r)\n", "\r\n"); ;
                    this.on_PropertyChanged();
                }
            }
        }

        private string taskFilename = string.Empty;
        public string TaskFilename
        {
            get { return this.taskFilename; }
            set
            {
                if (this.taskFilename != value)
                {
                    if (value == null) value = string.Empty;
                    this.taskFilename = value;
                    this.on_PropertyChanged();
                    this.Task = Helper.getThumbnailFromMediaFile(value);
                }
            }
        }

        [XmlIgnore]
        public Image Task { get; private set; }

        private Game.FieldIDElements targetField1 = Game.FieldIDElements.A1;
        public Game.FieldIDElements TargetField1
        {
            get { return this.targetField1; }
            set
            {
                if (this.targetField1 != value)
                {
                    this.targetField1 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Game.FieldIDElements targetField2 = Game.FieldIDElements.A2;
        public Game.FieldIDElements TargetField2
        {
            get { return this.targetField2; }
            set
            {
                if (this.targetField2 != value)
                {
                    this.targetField2 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContent() { }
        public DatasetContent(
            string pictureFilename)
        {
            if (string.IsNullOrEmpty(pictureFilename))
            {
                this.Name = "?";
                this.TaskFilename = string.Empty;
            }
            else
            {
                this.Name = Path.GetFileNameWithoutExtension(pictureFilename);
                this.TaskFilename = pictureFilename;
            }
        }

        private void buildToString() { this.toString = this.Name; }

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

    public class Business : _Base.Score.Business {

        #region Properties

        private int borderPositionX = 0;
        public int BorderPositionX
        {
            get { return this.borderPositionX; }
            set
            {
                if (this.borderPositionX != value)
                {
                    this.borderPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetBorder();
                }
            }
        }

        private int borderPositionY = 0;
        public int BorderPositionY
        {
            get { return this.borderPositionY; }
            set
            {
                if (this.borderPositionY != value)
                {
                    this.borderPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetBorder();
                }
            }
        }

        private int borderScaling = 100;
        public int BorderScaling
        {
            get { return this.borderScaling; }
            set
            {
                if (this.borderScaling != value)
                {
                    if (value < 10) this.borderScaling = 10;
                    else this.borderScaling = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetBorder();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.Border.Styles setsStyle = VentuzScenes.GamePool._Modules.Border.Styles.ThreeDotsCounter;
        public VentuzScenes.GamePool._Modules.Border.Styles BorderStyle
        {
            get { return this.setsStyle; }
            set
            {
                if (this.setsStyle != value)
                {
                    this.setsStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetBorder();
                }
            }
        }


        private int taskCounterPositionX = 0;
        public int TaskCounterPositionX
        {
            get { return this.taskCounterPositionX; }
            set
            {
                if (this.taskCounterPositionX != value)
                {
                    this.taskCounterPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTaskCounter();
                }
            }
        }

        private int taskCounterPositionY = 0;
        public int TaskCounterPositionY
        {
            get { return this.taskCounterPositionY; }
            set
            {
                if (this.taskCounterPositionY != value)
                {
                    this.taskCounterPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTaskCounter();
                }
            }
        }

        public const int TaskCounterPenaltyCountMax = 13;

        private int taskCounterSize = 13;
        public int TaskCounterSize
        {
            get { return this.taskCounterSize; }
            set
            {
                if (this.taskCounterSize != value)
                {
                    if (value < 0) value = 0;
                    this.taskCounterSize = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTaskCounter();
                }
            }
        }

        private FieldIDElements? leftTeamInputDesk = null;
        public FieldIDElements? LeftTeamInputDesk
        {
            get { return this.leftTeamInputDesk; }
            set
            {
                if (this.leftTeamInputDesk != value)
                {
                    this.leftTeamInputDesk = (value);
                    this.on_PropertyChanged();
                }
            }
        }

        private FieldIDElements? leftTeamInputTablet = null;
        public FieldIDElements? LeftTeamInputTablet
        {
            get { return this.leftTeamInputTablet; }
            set
            {
                if (this.leftTeamInputTablet != value)
                {
                    this.leftTeamInputTablet = (value);
                    this.on_PropertyChanged();
                }
            }
        }

        private FieldIDElements? rightTeamInputDesk = null;
        public FieldIDElements? RightTeamInputDesk
        {
            get { return this.rightTeamInputDesk; }
            set
            {
                if (this.rightTeamInputDesk != value)
                {
                    this.rightTeamInputDesk = (value);
                    this.on_PropertyChanged();
                }
            }
        }

        private FieldIDElements? rightTeamInputTablet = null;
        public FieldIDElements? RightTeamInputTablet
        {
            get { return this.rightTeamInputTablet; }
            set
            {
                if (this.rightTeamInputTablet != value)
                {
                    this.rightTeamInputTablet = (value);
                    this.on_PropertyChanged();
                }
            }
        }

        private PlayerSelection selectedTeam = PlayerSelection.NotSelected;
        [NotSerialized]
        public PlayerSelection SelectedTeam
        {
            get { return this.selectedTeam; }
            set
            {
                if (this.selectedTeam != value)
                {
                    this.selectedTeam = value;
                    this.on_PropertyChanged();
                }
                this.Vinsert_SetBorderBuzzer();
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

        private Insert insertScene;
        public override VRemote4.HandlerSi.Scene.States InsertSceneStatus
        {
            get
            {
                if (this.insertScene is Insert) return this.insertScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private Fullscreen fullscreenScene;
        public override VRemote4.HandlerSi.Scene.States FullscreenSceneStatus
        {
            get
            {
                if (this.fullscreenScene is Fullscreen) return this.fullscreenScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private Stage hostScene;
        public override VRemote4.HandlerSi.Scene.States HostSceneStatus
        {
            get
            {
                if (this.hostScene is Game) return this.hostScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private Stage leftPlayerScene;
        public override VRemote4.HandlerSi.Scene.States LeftPlayerSceneStatus
        {
            get
            {
                if (this.leftPlayerScene is Stage) return this.leftPlayerScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private Stage rightPlayerScene;
        public override VRemote4.HandlerSi.Scene.States RightPlayerSceneStatus
        {
            get
            {
                if (this.rightPlayerScene is Stage) return this.rightPlayerScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private VRemote4.HandlerSi.Business localVentuzHandler;

        private VRemote4.HandlerSi.Client.Business leftTeamTabletClient;
        private PlayerTablet ventuzLeftTeamTabletScene;
        public VRemote4.HandlerSi.Scene.States VentuzLeftTeamTabletSceneStatus
        {
            get
            {
                if (this.ventuzLeftTeamTabletScene is PlayerTablet) return this.ventuzLeftTeamTabletScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }
        private string leftTeamTabletHostname = string.Empty;
        public string LeftTeamTabletHostname
        {
            get { return this.leftTeamTabletHostname; }
            set
            {
                if (this.leftTeamTabletHostname != value)
                {
                    if (value == null) value = string.Empty;
                    this.leftTeamTabletHostname = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private VRemote4.HandlerSi.Client.Business rightTeamTabletClient;
        private PlayerTablet ventuzRightTeamTabletScene;
        public VRemote4.HandlerSi.Scene.States VentuzRightTeamTabletSceneStatus
        {
            get
            {
                if (this.ventuzRightTeamTabletScene is PlayerTablet) return this.ventuzRightTeamTabletScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }
        private string rightTeamTabletHostname = string.Empty;
        public string RightTeamTabletHostname
        {
            get { return this.rightTeamTabletHostname; }
            set
            {
                if (this.rightTeamTabletHostname != value)
                {
                    if (value == null) value = string.Empty;
                    this.rightTeamTabletHostname = value;
                    this.on_PropertyChanged();
                }
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.SelectTwoFields'", typeIdentifier);
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
            this.leftPlayerScene.TouchPressed += this.leftPlayerScene_TouchPressed;

            this.rightPlayerScene = new Stage(syncContext, rightPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.rightPlayerScene.StatusChanged += this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.TouchPressed += this.rightPlayerScene_TouchPressed;

            this.localVentuzHandler = new VRemote4.HandlerSi.Business(syncContext);
            this.localVentuzHandler.Error += this.localVentuzHandler_Error;

            if (this.localVentuzHandler.TryAddClient("Left Tablet", false, out this.leftTeamTabletClient))
            {
                this.leftTeamTabletClient.HostnameChanged += this.leftTeamTabletClient_HostnameChanged;
                this.leftTeamTabletClient.StatusChanged += this.leftTeamTabletClient_StatusChanged;
                this.ventuzLeftTeamTabletScene = new PlayerTablet(syncContext, this.leftTeamTabletClient, 0);
                this.ventuzLeftTeamTabletScene.StatusChanged += this.ventuzLeftTeamTabletScene_StatusChanged;
                this.ventuzLeftTeamTabletScene.PropertyChanged += this.ventuzLeftTeamTabletScene_PropertyChanged;
                this.ventuzLeftTeamTabletScene.TouchPressed += this.ventuzLeftTeamTabletScene_TouchPressed;
            }

            if (this.localVentuzHandler.TryAddClient("Right Tablet", false, out this.rightTeamTabletClient))
            {
                this.rightTeamTabletClient.HostnameChanged += this.rightTeamTabletClient_HostnameChanged;
                this.rightTeamTabletClient.StatusChanged += this.rightTeamTabletClient_StatusChanged;
                this.ventuzRightTeamTabletScene = new PlayerTablet(syncContext, this.rightTeamTabletClient, 0);
                this.ventuzRightTeamTabletScene.StatusChanged += this.ventuzRightTeamTabletScene_StatusChanged;
                this.ventuzRightTeamTabletScene.PropertyChanged += this.ventuzRightTeamTabletScene_PropertyChanged;
                this.ventuzRightTeamTabletScene.TouchPressed += this.ventuzRightTeamTabletScene_TouchPressed;
            }

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this, this.localVentuzHandler);
        }

        public override void Dispose()
        {
            base.Dispose();

            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Dispose();

            this.fullscreenScene.StatusChanged -= this.fullscreenScene_StatusChanged;
            this.fullscreenScene.Dispose();

            this.hostScene.StatusChanged -= this.hostScene_StatusChanged;
            this.hostScene.Dispose();

            this.hostScene.StatusChanged -= this.hostScene_StatusChanged;
            this.hostScene.Dispose();

            this.leftPlayerScene.StatusChanged -= this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.TouchPressed -= this.leftPlayerScene_TouchPressed;
            this.leftPlayerScene.Dispose();

            this.rightPlayerScene.StatusChanged -= this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.TouchPressed -= this.rightPlayerScene_TouchPressed;
            this.rightPlayerScene.Dispose();

            this.localVentuzHandler.Error -= this.localVentuzHandler_Error;
            this.localVentuzHandler.Dispose();

            this.leftTeamTabletClient.HostnameChanged -= this.leftTeamTabletClient_HostnameChanged;
            this.leftTeamTabletClient.StatusChanged -= this.leftTeamTabletClient_StatusChanged;
            this.ventuzLeftTeamTabletScene.StatusChanged -= this.ventuzLeftTeamTabletScene_StatusChanged;
            this.ventuzLeftTeamTabletScene.PropertyChanged -= this.ventuzLeftTeamTabletScene_PropertyChanged;
            this.ventuzLeftTeamTabletScene.TouchPressed -= this.ventuzLeftTeamTabletScene_TouchPressed;
            this.ventuzLeftTeamTabletScene.Dispose();

            this.rightTeamTabletClient.HostnameChanged -= this.rightTeamTabletClient_HostnameChanged;
            this.rightTeamTabletClient.StatusChanged -= this.rightTeamTabletClient_StatusChanged;
            this.ventuzRightTeamTabletScene.StatusChanged -= this.ventuzRightTeamTabletScene_StatusChanged;
            this.ventuzRightTeamTabletScene.PropertyChanged -= this.ventuzRightTeamTabletScene_PropertyChanged;
            this.ventuzRightTeamTabletScene.TouchPressed -= this.ventuzRightTeamTabletScene_TouchPressed;
            this.ventuzRightTeamTabletScene.Dispose();

        }

        public override void ResetData()
        {
            base.ResetData();
            this.LeftTeamInputDesk = null;
            this.LeftTeamInputTablet = null;
            this.RightTeamInputDesk = null;
            this.RightTeamInputTablet = null;
            this.SelectedTeam = PlayerSelection.NotSelected;
            this.TaskCounter = 0;
            this.SelectDataset(0);
        }

        public override void Next()
        {
            base.Next();
            this.LeftTeamInputDesk = null;
            this.LeftTeamInputTablet = null;
            this.RightTeamInputDesk = null;
            this.RightTeamInputTablet = null;
            this.SelectedTeam = PlayerSelection.NotSelected;
            this.TaskCounter++;
            this.SelectDataset(this.SelectedDatasetIndex + 1);
        }

        private void playerTouchPressed(
            Game scene,
            FieldIDElements e)
        {
            if (this.SelectedTeam == PlayerSelection.NotSelected)
            {
                if (scene == this.leftPlayerScene &&
                    this.LeftTeamInputTablet != e)
                {
                    this.LeftTeamInputDesk = e;
                    this.Vgame_DisableTouch(scene);
                }
                else if (scene == this.ventuzLeftTeamTabletScene &&
                    this.LeftTeamInputDesk != e)
                {
                    this.LeftTeamInputTablet = e;
                    this.Vgame_DisableTouch(scene);
                }
                else if (scene == this.rightPlayerScene &&
                    this.RightTeamInputTablet != e)
                {
                    this.RightTeamInputDesk = e;
                    this.Vgame_DisableTouch(scene);
                }
                else if (scene == this.ventuzRightTeamTabletScene &&
                    this.RightTeamInputDesk != e)
                {
                    this.RightTeamInputTablet = e;
                    this.Vgame_DisableTouch(scene);
                }
                this.Vplayers_SetInput();
                if (this.LeftTeamInputDesk.HasValue &&
                    this.LeftTeamInputTablet.HasValue) this.SelectedTeam = PlayerSelection.LeftPlayer;
                else if (this.RightTeamInputDesk.HasValue &&
                    this.RightTeamInputTablet.HasValue) this.SelectedTeam = PlayerSelection.RightPlayer;
                if (this.SelectedTeam != PlayerSelection.NotSelected)
                {
                    switch (this.SelectedTeam)
                    {
                        case PlayerSelection.LeftPlayer:
                            this.insertMasterScene.Sampler.Play(VentuzScenes.Insert.Sampler.SampleElements.BuzzerLeft);
                            break;
                        case PlayerSelection.RightPlayer:
                            this.insertMasterScene.Sampler.Play(VentuzScenes.Insert.Sampler.SampleElements.BuzzerRight);
                            break;
                    }
                    this.Vinsert_SetBorderBuzzer();
                    this.Vplayers_DisableInput();
                    this.Vhost_SetInput();
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

        public void Vinsert_BorderIn()
        {
            if (this.insertScene is VRemote4.HandlerSi.Scene)
            {
                this.Vinsert_SetBorder();
                this.insertScene.Border.ToIn();
            }
            if (this.TaskCounter > 0 && this.TaskCounter <= this.TaskCounterSize) this.Vinsert_TaskCounterIn();
        }
        public void Vinsert_SetBorder()
        {
            if (this.insertScene is VRemote4.HandlerSi.Scene)
            {
                this.Vinsert_SetBorder(this.insertScene.Border, this.LeftPlayerScore, this.RightPlayerScore);
                this.Vinsert_SetBorderBuzzer(this.insertScene.Border, this.SelectedTeam);
            }
        }
        public void Vinsert_SetBorder(
            VentuzScenes.GamePool._Modules.Border scene,
            int leftPlayerScore,
            int rightPlayerScore)
        {
            if (scene is VentuzScenes.GamePool._Modules.Border)
            {
                scene.SetPositionX(this.BorderPositionX);
                scene.SetPositionY(this.BorderPositionY);
                scene.SetScaling(this.BorderScaling);
                scene.SetStyle(this.BorderStyle);
                scene.SetLeftScore(leftPlayerScore);
                scene.SetRightScore(rightPlayerScore);
            }
        }
        public void Vinsert_SetBorderBuzzer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetBorderBuzzer(this.insertScene.Border, this.SelectedTeam); }
        public void Vinsert_SetBorderBuzzer(PlayerSelection buzzeredPlayer) { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetBorderBuzzer(this.insertScene.Border, buzzeredPlayer); }
        public void Vinsert_SetBorderBuzzer(
            VentuzScenes.GamePool._Modules.Border scene,
            PlayerSelection buzzeredPlayer)
        {
            if (scene is VentuzScenes.GamePool._Modules.Border)
            {
                switch (buzzeredPlayer)
                {
                    case PlayerSelection.LeftPlayer:
                        scene.SetBuzzerLeft();
                        break;
                    case PlayerSelection.RightPlayer:
                        scene.SetBuzzerRight();
                        break;
                    case PlayerSelection.NotSelected:
                    default:
                        scene.ResetBuzzer();
                        break;
                }
            }
        }
        public void Vinsert_BorderOut()
        {
            if (this.insertScene is VRemote4.HandlerSi.Scene &&
                this.insertScene.Border is VentuzScenes.GamePool._Modules.Border) this.insertScene.Border.ToOut();
            this.Vinsert_TaskCounterOut();
        }

        public void Vinsert_TaskCounterIn()
        {
            this.Vinsert_SetTaskCounter();
            if (this.insertScene is Insert &&
                this.insertScene.TaskCounter is VentuzScenes.GamePool._Modules.TaskCounter) this.insertScene.TaskCounter.ToIn();
        }
        public void Vinsert_SetTaskCounter() { if (this.insertScene is Insert) this.Vinsert_SetTaskCounter(this.insertScene.TaskCounter, this.TaskCounter); }
        public void Vinsert_SetTaskCounter(
            VentuzScenes.GamePool._Modules.TaskCounter scene,
            int counter)
        {
            if (scene is VentuzScenes.GamePool._Modules.TaskCounter)
            {
                scene.SetPositionX(this.TaskCounterPositionX);
                scene.SetPositionY(this.TaskCounterPositionY);
                scene.SetStyle(VentuzScenes.GamePool._Modules.TaskCounter.Styles.Numeric);
                scene.SetSize(this.TaskCounterSize);
                scene.SetCounter(counter);
            }
        }
        public void Vinsert_TaskCounterOut()
        {
            if (this.insertScene is Insert &&
                this.insertScene.TaskCounter is VentuzScenes.GamePool._Modules.TaskCounter) this.insertScene.TaskCounter.ToOut();
        }

        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vfullscreen_LoadScene() { this.fullscreenScene.Load(); }
        internal void Vfullscreen_ContentIn()
        {
            this.Vgame_SetContent(this.fullscreenScene, this.SelectedDataset);
            if (this.fullscreenScene is Scene) this.fullscreenScene.ToIn();
        }
        internal void Vfullscreen_SetInput()
        {
            switch (this.SelectedTeam)
            {
                case PlayerSelection.LeftPlayer:
                    if (this.LeftTeamInputDesk.HasValue) this.Vgame_SetFieldStatus(this.fullscreenScene, this.LeftTeamInputDesk, FieldStatusElements.Red);
                    if (this.LeftTeamInputTablet.HasValue) this.Vgame_SetFieldStatus(this.fullscreenScene, this.LeftTeamInputTablet, FieldStatusElements.Red);
                    this.insertMasterScene.Sampler.Play(VentuzScenes.Insert.Sampler.SampleElements.Select);
                    break;
                case PlayerSelection.RightPlayer:
                    if (this.RightTeamInputDesk.HasValue) this.Vgame_SetFieldStatus(this.fullscreenScene, this.RightTeamInputDesk, FieldStatusElements.Blue);
                    if (this.RightTeamInputTablet.HasValue) this.Vgame_SetFieldStatus(this.fullscreenScene, this.RightTeamInputTablet, FieldStatusElements.Blue);
                    this.insertMasterScene.Sampler.Play(VentuzScenes.Insert.Sampler.SampleElements.Select);
                    break;
                case PlayerSelection.NotSelected:
                default:
                    this.Vhost_ContentIn();
                    break;
            }
        }
        internal void Vfullscreen_Resolve() { 
            this.Vgame_SetSolution(this.fullscreenScene, this.SelectedDataset);
            this.insertMasterScene.Sampler.Play(VentuzScenes.Insert.Sampler.SampleElements.Resolve);
        }
        internal void Vfullscreen_ContentOut()
        {
            if (this.fullscreenScene is Scene) this.fullscreenScene.ToOut();
        }
        public override void Vfullscreen_UnloadScene() { this.fullscreenScene.Unload(); }

        public override void Vhost_LoadScene() { this.hostScene.Load(); }
        internal void Vhost_ContentIn()
        {
            this.Vgame_SetContent(this.hostScene, this.SelectedDataset);
            this.Vgame_ToIn(this.hostScene);
            this.Vhost_SetSolution();
        }
        internal void Vhost_SetSolution() { this.Vgame_SetSolution(this.hostScene, this.SelectedDataset); }
        internal void Vhost_SetInput()
        {
            switch (this.SelectedTeam)
            {
                case PlayerSelection.LeftPlayer:
                    if (this.LeftTeamInputDesk.HasValue) this.Vgame_SetFieldStatus(this.hostScene, this.LeftTeamInputDesk, FieldStatusElements.Red);
                    if (this.LeftTeamInputTablet.HasValue) this.Vgame_SetFieldStatus(this.hostScene, this.LeftTeamInputTablet, FieldStatusElements.Red);
                    break;
                case PlayerSelection.RightPlayer:
                    if (this.RightTeamInputDesk.HasValue) this.Vgame_SetFieldStatus(this.hostScene, this.RightTeamInputDesk, FieldStatusElements.Blue);
                    if (this.RightTeamInputTablet.HasValue) this.Vgame_SetFieldStatus(this.hostScene, this.RightTeamInputTablet, FieldStatusElements.Blue);
                    break;
                case PlayerSelection.NotSelected:
                default:
                    this.Vhost_ContentIn();
                    break;
            }
        }
        public override void Vhost_UnloadScene() { this.hostScene.Unload(); }

        public override void Vleftplayer_LoadScene() { this.leftPlayerScene.Load(); }
        public override void Vleftplayer_UnloadScene() { this.leftPlayerScene.Unload(); }

        public override void Vrightplayer_LoadScene() { this.rightPlayerScene.Load(); }
        public override void Vrightplayer_UnloadScene() { this.rightPlayerScene.Unload(); }

        internal void Vplayers_ContentIn()
        {
            this.Vgame_SetContent(this.leftPlayerScene, this.SelectedDataset);
            this.Vgame_SetContent(this.rightPlayerScene, this.SelectedDataset);
            this.Vgame_SetContent(this.ventuzLeftTeamTabletScene, this.SelectedDataset);
            this.Vgame_SetContent(this.ventuzRightTeamTabletScene, this.SelectedDataset);
            this.Vgame_ToIn(this.leftPlayerScene);
            this.Vgame_ToIn(this.rightPlayerScene);
            this.Vgame_ToIn(this.ventuzLeftTeamTabletScene);
            this.Vgame_ToIn(this.ventuzRightTeamTabletScene);
            this.Vplayers_EnableInput();
        }
        internal void Vplayers_EnableInput()
        {
            this.Vgame_EnableTouch(this.leftPlayerScene);
            this.Vgame_EnableTouch(this.rightPlayerScene);
            this.Vgame_EnableTouch(this.ventuzLeftTeamTabletScene);
            this.Vgame_EnableTouch(this.ventuzRightTeamTabletScene);
        }
        internal void Vplayers_SetInput()
        {
            if (this.LeftTeamInputDesk.HasValue)
            {
                this.Vgame_SetFieldStatus(this.leftPlayerScene, this.LeftTeamInputDesk, FieldStatusElements.Red);
                this.Vgame_SetFieldStatus(this.ventuzLeftTeamTabletScene, this.LeftTeamInputDesk, FieldStatusElements.Red_Dashed);
            }
            if (this.LeftTeamInputTablet.HasValue)
            {
                this.Vgame_SetFieldStatus(this.leftPlayerScene, this.LeftTeamInputTablet, FieldStatusElements.Red_Dashed);
                this.Vgame_SetFieldStatus(this.ventuzLeftTeamTabletScene, this.LeftTeamInputTablet, FieldStatusElements.Red);
            }
            if (this.RightTeamInputDesk.HasValue)
            {
                this.Vgame_SetFieldStatus(this.rightPlayerScene, this.RightTeamInputDesk, FieldStatusElements.Blue);
                this.Vgame_SetFieldStatus(this.ventuzRightTeamTabletScene, this.RightTeamInputDesk, FieldStatusElements.Blue_Dashed);
            }
            if (this.RightTeamInputTablet.HasValue)
            {
                this.Vgame_SetFieldStatus(this.rightPlayerScene, this.RightTeamInputTablet, FieldStatusElements.Blue_Dashed);
                this.Vgame_SetFieldStatus(this.ventuzRightTeamTabletScene, this.RightTeamInputTablet, FieldStatusElements.Blue);
            }
        }
        internal void Vplayers_DisableInput()
        {
            this.Vgame_DisableTouch(this.leftPlayerScene);
            this.Vgame_DisableTouch(this.rightPlayerScene);
            this.Vgame_DisableTouch(this.ventuzLeftTeamTabletScene);
            this.Vgame_DisableTouch(this.ventuzRightTeamTabletScene);
        }

        internal void Vstage_ContentOut()
        {
            this.Vgame_ToOut(this.hostScene);
            this.Vgame_ToOut(this.leftPlayerScene);
            this.Vgame_ToOut(this.rightPlayerScene);
            this.Vgame_ToOut(this.ventuzLeftTeamTabletScene);
            this.Vgame_ToOut(this.ventuzRightTeamTabletScene);
        }

        public void Vgame_Reset(Game scene) { if (scene is Game) scene.Reset(); }
        public void Vgame_ToIn(Game scene) { if (scene is Game) scene.ToIn(); }
        public void Vgame_ToOut(Game scene) { if (scene is Game) scene.ToOut(); }
        public void Vgame_SetAllIDsOut(Game scene) { if (scene is Game) scene.SetAllIDsOut(); }
        public void Vgame_SetImageFilename(Game scene, string value) { if (scene is Game) scene.SetImageFilename(value); }
        public void Vgame_SetFieldStatus(Game scene, FieldIDElements? id, FieldStatusElements value) 
        {
            if (scene is Game)
            {
                 if (id is FieldIDElements) scene.SetFieldStatus((FieldIDElements)id, value);
            }
        }
        public void Vgame_EnableTouch(Game scene) { if (scene is Game) scene.EnableTouch(); }
        public void Vgame_DisableTouch(Game scene) { if (scene is Game) scene.DisableTouch(); }
        public void Vgame_ResetAllFields(Game scene) { if (scene is Game) scene.ResetAllFields(); }
        public void Vgame_Init(
            Game scene)
        {
            this.Vgame_Reset(scene);
            this.Vgame_SetAllIDsOut(scene);
        }
        internal void Vgame_SetContent(
            Game scene,
            DatasetContent content,
            bool allIDsOut = true)
        {
            this.Vgame_ResetAllFields(scene);
            if (allIDsOut) this.Vgame_SetAllIDsOut(scene);
            if (content is DatasetContent) this.Vgame_SetImageFilename(scene, content.TaskFilename);
            else this.Vgame_SetImageFilename(scene, string.Empty);
        }
        internal void Vgame_SetSolution(
            Game scene,
            DatasetContent content)
        {
            if (content is DatasetContent)
            {
                this.Vgame_SetFieldStatus(scene, content.TargetField1, FieldStatusElements.Yellow);
                this.Vgame_SetFieldStatus(scene, content.TargetField2, FieldStatusElements.Yellow);
            }
            else Vgame_ResetAllFields(scene);
        }

        public void Vlefttablet_Start() { this.leftTeamTabletClient.Start(this.LeftTeamTabletHostname); }
        public void Vlefttablet_Init() { if (this.VentuzLeftTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vgame_Init(this.ventuzLeftTeamTabletScene); }
        internal void Vlefttablet_SetContent()
        {
            if (this.VentuzLeftTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vgame_SetContent(this.ventuzLeftTeamTabletScene, this.SelectedDataset);
        }
        internal void Vlefttablet_EnableInput() { if (this.VentuzLeftTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vgame_EnableTouch(this.ventuzLeftTeamTabletScene); }
        internal void Vlefttablet_DisableInput() { if (this.VentuzLeftTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vgame_DisableTouch(this.ventuzLeftTeamTabletScene); }
        public void Vlefttablet_ShutDown() { this.leftTeamTabletClient.Shutdown(); }

        public void Vrighttablet_Start() { this.rightTeamTabletClient.Start(this.RightTeamTabletHostname); }
        public void Vrighttablet_Init() { if (this.VentuzRightTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vgame_Init(this.ventuzRightTeamTabletScene); }
        internal void Vrighttablet_SetContent()
        {
            if (this.VentuzRightTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vgame_SetContent(this.ventuzRightTeamTabletScene, this.SelectedDataset);
        }
        internal void Vrighttablet_EnableInput() { if (this.VentuzRightTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vgame_EnableTouch(this.ventuzRightTeamTabletScene); }
        internal void Vrighttablet_DisableInput() { if (this.VentuzRightTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vgame_DisableTouch(this.ventuzRightTeamTabletScene); }
        public void Vrighttablet_ShutDown() { this.rightTeamTabletClient.Shutdown(); }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
        }

        #endregion


        #region Events.Outgoing
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

        protected override void sync_hostScene_StatusChanged(object content)
        {
            base.sync_hostScene_StatusChanged(content);
            VRemote4.HandlerSi.Scene.StatusArgs e = content as VRemote4.HandlerSi.Scene.StatusArgs;
            if (e is VRemote4.HandlerSi.Scene.StatusArgs)
            {
                if (e.Status == VRemote4.HandlerSi.Scene.States.Available) this.Vgame_Init(this.hostScene);
            }
        }

        protected override void sync_leftPlayerScene_StatusChanged(object content)
        {
            base.sync_leftPlayerScene_StatusChanged(content);
            VRemote4.HandlerSi.Scene.StatusArgs e = content as VRemote4.HandlerSi.Scene.StatusArgs;
            if (e is VRemote4.HandlerSi.Scene.StatusArgs)
            {
                if (e.Status == VRemote4.HandlerSi.Scene.States.Available) this.Vgame_Init(this.leftPlayerScene);
            }
        }

        private void leftPlayerScene_TouchPressed(object sender, Game.FieldIDElements e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerScene_TouchPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerScene_TouchPressed(object content)
        {
            if (content is FieldIDElements) this.playerTouchPressed(this.leftPlayerScene, (FieldIDElements)content);
        }

        protected override void sync_rightPlayerScene_StatusChanged(object content)
        {
            base.sync_rightPlayerScene_StatusChanged(content);
            VRemote4.HandlerSi.Scene.StatusArgs e = content as VRemote4.HandlerSi.Scene.StatusArgs;
            if (e is VRemote4.HandlerSi.Scene.StatusArgs)
            {
                if (e.Status == VRemote4.HandlerSi.Scene.States.Available) this.Vgame_Init(this.leftPlayerScene);
            }
        }

        private void rightPlayerScene_TouchPressed(object sender, Game.FieldIDElements e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_TouchPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_TouchPressed(object content)
        {
            if (content is FieldIDElements) this.playerTouchPressed(this.rightPlayerScene, (FieldIDElements)content);
        }

        void localVentuzHandler_Error(object sender, ErrorEventArgs e)
        {
            Exception exc = e.GetException();
            this.on_Error(sender, new Messaging.ErrorEventArgs(this, sender.ToString(), DateTime.Now, exc.ToString()));
        }

        void leftTeamTabletClient_HostnameChanged(object sender, VRemote4.HandlerSi.Client.HostnameArgs e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftTeamTabletClient_HostnameChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftTeamTabletClient_HostnameChanged(object content)
        {
            VRemote4.HandlerSi.Client.HostnameArgs e = content as VRemote4.HandlerSi.Client.HostnameArgs;
            if (e is VRemote4.HandlerSi.Client.HostnameArgs) this.LeftTeamTabletHostname = e.Name;
        }

        void leftTeamTabletClient_StatusChanged(object sender, VRemote4.HandlerSi.Client.ClientStateArgs e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftTeamTabletClient_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftTeamTabletClient_StatusChanged(object content)
        {
            VRemote4.HandlerSi.Client.ClientStateArgs e = content as VRemote4.HandlerSi.Client.ClientStateArgs;
            if (e is VRemote4.HandlerSi.Client.ClientStateArgs &&
                e.Status.HasValue &&
                e.Status.Value == VRemote4.HandlerSi.Client.ClientStates.Active &&
                this.ventuzLeftTeamTabletScene is VRemote4.HandlerSi.Scene) this.ventuzLeftTeamTabletScene.Load();
        }

        void ventuzLeftTeamTabletScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzLeftTeamTabletScene_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzLeftTeamTabletScene_StatusChanged(object content)
        {
            VRemote4.HandlerSi.Scene.StatusArgs e = content as VRemote4.HandlerSi.Scene.StatusArgs;
            if (e is VRemote4.HandlerSi.Scene.StatusArgs)
            {
                if (e.Status == VRemote4.HandlerSi.Scene.States.Unloaded) this.leftTeamTabletClient.Shutdown();
                else if (e.Status == VRemote4.HandlerSi.Scene.States.Available) this.Vlefttablet_Init();
            }
            this.on_PropertyChanged("VentuzLeftTeamTabletSceneStatus");
        }

        void ventuzLeftTeamTabletScene_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzLeftTeamTabletScene_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzLeftTeamTabletScene_PropertyChanged(object content)
        {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs)
            {
            }
        }

        void ventuzLeftTeamTabletScene_TouchPressed(object sender, Game.FieldIDElements e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzLeftTeamTabletScene_TouchPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzLeftTeamTabletScene_TouchPressed(object content)
        {
            if (content is FieldIDElements) this.playerTouchPressed(this.ventuzLeftTeamTabletScene, (FieldIDElements)content);
        }

        void rightTeamTabletClient_HostnameChanged(object sender, VRemote4.HandlerSi.Client.HostnameArgs e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightTeamTabletClient_HostnameChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightTeamTabletClient_HostnameChanged(object content)
        {
            VRemote4.HandlerSi.Client.HostnameArgs e = content as VRemote4.HandlerSi.Client.HostnameArgs;
            if (e is VRemote4.HandlerSi.Client.HostnameArgs) this.RightTeamTabletHostname = e.Name;
        }

        void rightTeamTabletClient_StatusChanged(object sender, VRemote4.HandlerSi.Client.ClientStateArgs e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightTeamTabletClient_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightTeamTabletClient_StatusChanged(object content)
        {
            VRemote4.HandlerSi.Client.ClientStateArgs e = content as VRemote4.HandlerSi.Client.ClientStateArgs;
            if (e is VRemote4.HandlerSi.Client.ClientStateArgs &&
                e.Status.HasValue &&
                e.Status.Value == VRemote4.HandlerSi.Client.ClientStates.Active &&
                this.ventuzRightTeamTabletScene is VRemote4.HandlerSi.Scene) this.ventuzRightTeamTabletScene.Load();
        }

        void ventuzRightTeamTabletScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzRightTeamTabletScene_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzRightTeamTabletScene_StatusChanged(object content)
        {
            VRemote4.HandlerSi.Scene.StatusArgs e = content as VRemote4.HandlerSi.Scene.StatusArgs;
            if (e is VRemote4.HandlerSi.Scene.StatusArgs)
            {
                if (e.Status == VRemote4.HandlerSi.Scene.States.Unloaded) this.rightTeamTabletClient.Shutdown();
                else if (e.Status == VRemote4.HandlerSi.Scene.States.Available) this.Vrighttablet_Init();
            }
            this.on_PropertyChanged("VentuzRightTeamTabletSceneStatus");
        }

        void ventuzRightTeamTabletScene_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzRightTeamTabletScene_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzRightTeamTabletScene_PropertyChanged(object content)
        {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs)
            {
            }
        }

        void ventuzRightTeamTabletScene_TouchPressed(object sender, Game.FieldIDElements e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzRightTeamTabletScene_TouchPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzRightTeamTabletScene_TouchPressed(object content)
        {
            if (content is FieldIDElements) this.playerTouchPressed(this.ventuzRightTeamTabletScene, (FieldIDElements)content);
        }

        #endregion

    }
}
