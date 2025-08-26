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

using Cliparts.Tools.Base;

using Cliparts.IOnet.IOUnit.IONbase;
using Cliparts.IOnet.IOUnit.IONbuz;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;
using Cliparts.SchlagDenStarLive.MainApp.Settings;
using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.Remembory;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.Remembory {

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetContent : Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private string pictureFilename = string.Empty;
        public string PictureFilename {
            get { return this.pictureFilename; }
            set {
                if (this.pictureFilename != value) {
                    if (value == null) value = string.Empty;
                    this.pictureFilename = value;
                    this.Picture = Helper.getThumbnailFromMediaFile(value);
                    this.on_PropertyChanged();
                }
            }
        }

        [XmlIgnore]
        public Image Picture { get; private set; }

        private string name = string.Empty;
        public string Name {
            get { return this.name; }
            set {
                if (this.name != value) {
                    if (value == null) value = string.Empty;
                    this.name = value;
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        private string image1Filename = string.Empty;
        public string Image1Filename {
            get { return this.image1Filename; }
            set {
                if (this.image1Filename != value) {
                    if (value == null) value = string.Empty;
                    this.image1Filename = value;
                    this.Image1 = Helper.getThumbnailFromMediaFile(value);
                    this.on_PropertyChanged();
                    this.setImagesCount();
                }
            }
        }
        [XmlIgnore]
        public Image Image1 { get; private set; }

        private string image1Text = string.Empty;
        public string Image1Text {
            get { return this.image1Text; }
            set {
                if (this.image1Text != value) {
                    if (value == null) value = string.Empty;
                    this.image1Text = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string image2Filename = string.Empty;
        public string Image2Filename {
            get { return this.image2Filename; }
            set {
                if (this.image2Filename != value) {
                    if (value == null) value = string.Empty;
                    this.image2Filename = value;
                    this.Image2 = Helper.getThumbnailFromMediaFile(value);
                    this.on_PropertyChanged();
                    this.setImagesCount();
                }
            }
        }
        [XmlIgnore]
        public Image Image2 { get; private set; }

        private string image2Text = string.Empty;
        public string Image2Text {
            get { return this.image2Text; }
            set {
                if (this.image2Text != value) {
                    if (value == null) value = string.Empty;
                    this.image2Text = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string image3Filename = string.Empty;
        public string Image3Filename {
            get { return this.image3Filename; }
            set {
                if (this.image3Filename != value) {
                    if (value == null) value = string.Empty;
                    this.image3Filename = value;
                    this.Image3 = Helper.getThumbnailFromMediaFile(value);
                    this.on_PropertyChanged();
                    this.setImagesCount();
                }
            }
        }
        [XmlIgnore]
        public Image Image3 { get; private set; }

        private string image3Text = string.Empty;
        public string Image3Text {
            get { return this.image3Text; }
            set {
                if (this.image3Text != value) {
                    if (value == null) value = string.Empty;
                    this.image3Text = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string image4Filename = string.Empty;
        public string Image4Filename {
            get { return this.image4Filename; }
            set {
                if (this.image4Filename != value) {
                    if (value == null) value = string.Empty;
                    this.image4Filename = value;
                    this.Image4 = Helper.getThumbnailFromMediaFile(value);
                    this.on_PropertyChanged();
                    this.setImagesCount();
                }
            }
        }
        [XmlIgnore]
        public Image Image4 { get; private set; }

        private string image4Text = string.Empty;
        public string Image4Text {
            get { return this.image4Text; }
            set {
                if (this.image4Text != value) {
                    if (value == null) value = string.Empty;
                    this.image4Text = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string image5Filename = string.Empty;
        public string Image5Filename {
            get { return this.image5Filename; }
            set {
                if (this.image5Filename != value) {
                    if (value == null) value = string.Empty;
                    this.image5Filename = value;
                    this.Image5 = Helper.getThumbnailFromMediaFile(value);
                    this.on_PropertyChanged();
                    this.setImagesCount();
                }
            }
        }
        [XmlIgnore]
        public Image Image5 { get; private set; }

        private string image5Text = string.Empty;
        public string Image5Text {
            get { return this.image5Text; }
            set {
                if (this.image5Text != value) {
                    if (value == null) value = string.Empty;
                    this.image5Text = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string image6Filename = string.Empty;
        public string Image6Filename {
            get { return this.image6Filename; }
            set {
                if (this.image6Filename != value) {
                    if (value == null) value = string.Empty;
                    this.image6Filename = value;
                    this.Image6 = Helper.getThumbnailFromMediaFile(value);
                    this.on_PropertyChanged();
                    this.setImagesCount();
                }
            }
        }
        [XmlIgnore]
        public Image Image6 { get; private set; }

        private string image6Text = string.Empty;
        public string Image6Text {
            get { return this.image6Text; }
            set {
                if (this.image6Text != value) {
                    if (value == null) value = string.Empty;
                    this.image6Text = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string image7Filename = string.Empty;
        public string Image7Filename {
            get { return this.image7Filename; }
            set {
                if (this.image7Filename != value) {
                    if (value == null) value = string.Empty;
                    this.image7Filename = value;
                    this.Image7 = Helper.getThumbnailFromMediaFile(value);
                    this.on_PropertyChanged();
                    this.setImagesCount();
                }
            }
        }
        [XmlIgnore]
        public Image Image7 { get; private set; }

        private string image7Text = string.Empty;
        public string Image7Text {
            get { return this.image7Text; }
            set {
                if (this.image7Text != value) {
                    if (value == null) value = string.Empty;
                    this.image7Text = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string image8Filename = string.Empty;
        public string Image8Filename {
            get { return this.image8Filename; }
            set {
                if (this.image8Filename != value) {
                    if (value == null) value = string.Empty;
                    this.image8Filename = value;
                    this.Image8 = Helper.getThumbnailFromMediaFile(value);
                    this.on_PropertyChanged();
                    this.setImagesCount();
                }
            }
        }
        [XmlIgnore]
        public Image Image8 { get; private set; }

        private string image8Text = string.Empty;
        public string Image8Text {
            get { return this.image8Text; }
            set {
                if (this.image8Text != value) {
                    if (value == null) value = string.Empty;
                    this.image8Text = value;
                    this.on_PropertyChanged();
                }
            }
        }

        [XmlIgnore]
        public int ImagesCount { get; private set; }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContent() { this.setImagesCount(); }
        public DatasetContent(
            string pictureFilename) {
            if (string.IsNullOrEmpty(pictureFilename)) {
                this.Name = "?";
                this.PictureFilename = string.Empty;
            }
            else {
                this.Name = Path.GetFileNameWithoutExtension(pictureFilename);
                this.PictureFilename = pictureFilename;
            }
            this.setImagesCount();
        }

        public void SetAllImages(
            List<string> filenames) {
            if (filenames is List<string>) filenames.Sort();
            if (filenames is List<string>
                && filenames.Count >= 1) {
                this.Image1Filename = filenames[0];
                this.Image1Text = Path.GetFileNameWithoutExtension(this.Image1Filename);
            }
            else {
                this.Image1Filename = string.Empty;
                this.Image1Text = string.Empty;
            }
            if (filenames is List<string>
                && filenames.Count >= 2) {
                this.Image2Filename = filenames[1];
                this.Image2Text = Path.GetFileNameWithoutExtension(this.Image2Filename);
            }
            else {
                this.Image2Filename = string.Empty;
                this.Image2Text = string.Empty;
            }
            if (filenames is List<string>
                && filenames.Count >= 3) {
                this.Image3Filename = filenames[2];
                this.Image3Text = Path.GetFileNameWithoutExtension(this.Image3Filename);
            }
            else {
                this.Image3Filename = string.Empty;
                this.Image3Text = string.Empty;
            }
            if (filenames is List<string>
                && filenames.Count >= 4) {
                this.Image4Filename = filenames[3];
                this.Image4Text = Path.GetFileNameWithoutExtension(this.Image4Filename);
            }
            else {
                this.Image4Filename = string.Empty;
                this.Image4Text = string.Empty;
            }
            if (filenames is List<string>
                && filenames.Count >= 5) {
                this.Image5Filename = filenames[4];
                this.Image5Text = Path.GetFileNameWithoutExtension(this.Image5Filename);
            }
            else {
                this.Image5Filename = string.Empty;
                this.Image5Text = string.Empty;
            }
            if (filenames is List<string>
                && filenames.Count >= 6) {
                this.Image6Filename = filenames[5];
                this.Image6Text = Path.GetFileNameWithoutExtension(this.Image6Filename);
            }
            else {
                this.Image6Filename = string.Empty;
                this.Image6Text = string.Empty;
            }
            if (filenames is List<string>
                && filenames.Count >= 7) {
                this.Image7Filename = filenames[6];
                this.Image7Text = Path.GetFileNameWithoutExtension(this.Image7Filename);
            }
            else {
                this.Image7Filename = string.Empty;
                this.Image7Text = string.Empty;
            }
            if (filenames is List<string>
                && filenames.Count >= 8) {
                this.Image8Filename = filenames[7];
                this.Image8Text = Path.GetFileNameWithoutExtension(this.Image8Filename);
            }
            else {
                this.Image8Filename = string.Empty;
                this.Image8Text = string.Empty;
            }
        }

        public string GetImageFilename(
            int id) {
            if (id == 1) return this.Image1Filename;
            else if (id == 2) return this.Image2Filename;
            else if (id == 3) return this.Image3Filename;
            else if (id == 4) return this.Image4Filename;
            else if (id == 5) return this.Image5Filename;
            else if (id == 6) return this.Image6Filename;
            else if (id == 7) return this.Image7Filename;
            else if (id == 8) return this.Image8Filename;
            else return string.Empty;
        }

        private void setImagesCount() {
            this.ImagesCount = 0;
            if (this.Image1 is Image) this.ImagesCount++;
            if (this.Image2 is Image) this.ImagesCount++;
            if (this.Image3 is Image) this.ImagesCount++;
            if (this.Image4 is Image) this.ImagesCount++;
            if (this.Image5 is Image) this.ImagesCount++;
            if (this.Image6 is Image) this.ImagesCount++;
            if (this.Image7 is Image) this.ImagesCount++;
            if (this.Image8 is Image) this.ImagesCount++;
        }

        private void buildToString() { this.toString = this.Name; }

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

        private int borderPositionX = 0;
        public int BorderPositionX {
            get { return this.borderPositionX; }
            set {
                if (this.borderPositionX != value) {
                    this.borderPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetBorder();
                }
            }
        }

        private int borderPositionY = 0;
        public int BorderPositionY {
            get { return this.borderPositionY; }
            set {
                if (this.borderPositionY != value) {
                    this.borderPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetBorder();
                }
            }
        }

        private int borderScaling = 100;
        public int BorderScaling {
            get { return this.borderScaling; }
            set {
                if (this.borderScaling != value) {
                    if (value < 10) this.borderScaling = 10;
                    else this.borderScaling = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetBorder();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.Border.Styles setsStyle = VentuzScenes.GamePool._Modules.Border.Styles.ThreeDotsCounter;
        public VentuzScenes.GamePool._Modules.Border.Styles BorderStyle {
            get { return this.setsStyle; }
            set {
                if (this.setsStyle != value) {
                    this.setsStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetBorder();
                }
            }
        }

        private int insertPositionX = 0;
        public int InsertPositionX {
            get { return this.insertPositionX; }
            set {
                if (this.insertPositionX != value) {
                    this.insertPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetContent();
                }
            }
        }

        private int insertPositionY = 0;
        public int InsertPositionY {
            get { return this.insertPositionY; }
            set {
                if (this.insertPositionY != value) {
                    this.insertPositionY = value;
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

        private List<string> foundImages = new List<string>();
        public List<bool> FoundImages { get; private set; }

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

        private PlayerSelection buzzeredPlayer = PlayerSelection.NotSelected;
        public PlayerSelection BuzzeredPlayer {
            get { return this.buzzeredPlayer; }
            private set {
                if (this.buzzeredPlayer != value) {
                    this.buzzeredPlayer = value;
                    this.Vinsert_SetBorderBuzzer(value);
                    this.Vstage_SetBuzzer(value);
                    this.on_PropertyChanged();
                    switch (this.BuzzeredPlayer) {
                        case PlayerSelection.LeftPlayer:
                            this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.LeftPlayerBuzzerChannel, new byte[] { 255 });
                            break;
                        case PlayerSelection.RightPlayer:
                            this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.RightPlayerBuzzerChannel, new byte[] { 255 });
                            break;
                        case PlayerSelection.NotSelected:
                        default:
                            this.buzzerHandler.SetDMXChannel(this.IOUnitName, IOnet.IOUnit.IONbuzDX.DMXStates.OFF);
                            break;
                    }
                }
            }
        }

        private bool fault = false;

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

            this.ClassInfo = string.Format("'{0}' of 'Templates.Remembory'", typeIdentifier);
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
            this.insertScene.Timer.Alarm1Fired += this.timer_Alarm1Fired;
            this.insertScene.Timer.Alarm2Fired += this.timer_Alarm2Fired;
            this.insertScene.Timer.StopFired += this.timer_StopFired;
            this.insertScene.Timer.PropertyChanged += this.timer_PropertyChanged;

            this.fullscreenScene = new Fullscreen(syncContext, fullscreenMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.fullscreenScene.StatusChanged += this.fullscreenScene_StatusChanged;

            this.hostScene = new Host(syncContext, hostMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.hostScene.StatusChanged += this.hostScene_StatusChanged;

            this.leftPlayerScene = new Player(syncContext, leftPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.leftPlayerScene.StatusChanged += this.leftPlayerScene_StatusChanged;

            this.rightPlayerScene = new Player(syncContext, rightPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.rightPlayerScene.StatusChanged += this.rightPlayerScene_StatusChanged;

            this.FoundImages = new List<bool>();

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);

        }

        public override void Dispose() {
            base.Dispose();

            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Timer.Alarm1Fired -= this.timer_Alarm1Fired;
            this.insertScene.Timer.Alarm2Fired -= this.timer_Alarm2Fired;
            this.insertScene.Timer.StopFired -= this.timer_StopFired;
            this.insertScene.Timer.PropertyChanged -= this.timer_PropertyChanged;
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
            this.SelectDataset(0);
            this.BuzzeredPlayer = PlayerSelection.NotSelected;
            this.foundImages.Clear();
            this.setFoundImages();
            this.fault = false;
        }

        public override void Next() {
            base.Next();
            this.TaskCounter++;
            this.SelectDataset(this.SelectedDatasetIndex + 1);
            this.BuzzeredPlayer = PlayerSelection.NotSelected;
            this.foundImages.Clear();
            this.setFoundImages();
            this.LockBuzzer();
            this.fault = false;
        }       

        public override void DoBuzzer(
            PlayerSelection buzzeredPlayer) {
            if (this.BuzzeredPlayer == PlayerSelection.NotSelected &&
                this.BuzzeredPlayer != buzzeredPlayer) {
                this.BuzzeredPlayer = buzzeredPlayer;
                switch (this.BuzzeredPlayer) {
                    case PlayerSelection.LeftPlayer:
                        if (this.insertMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.PlayJingleBuzzerLeft();
                        break;
                    case PlayerSelection.RightPlayer:
                        if (this.insertMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.PlayJingleBuzzerRight();
                        break;
                }
                this.Vinsert_SetBorderBuzzer();
                this.Vplayers_LiveVideoOut();
            }
        }

        public void RemoveFoundImage(
            int id) {
            if (this.SelectedDataset is DatasetContent) {
                string filename = this.SelectedDataset.GetImageFilename(id);
                if (!string.IsNullOrEmpty(filename) &&
                    this.foundImages.Contains(filename)) {
                    this.foundImages.Remove(filename);
                    this.Vinsert_SetContent();
                    this.Vhost_Set();
                    this.Vplayers_SetContent();
                }
            }
            this.setFoundImages();
        }
        public void AddFoundImage(
            int id) {
            if (this.SelectedDataset is DatasetContent) {
                string filename = this.SelectedDataset.GetImageFilename(id);
                if (!string.IsNullOrEmpty(filename) &&
                    !this.foundImages.Contains(filename)) {
                    this.foundImages.Add(filename);
                    this.Vinsert_SetContent();
                    if (this.insertMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                        !this.fault) {
                        if (this.foundImages.Count == this.SelectedDataset.ImagesCount) this.insertScene.PlayJingleTrue();
                        else this.insertScene.PlayJingleSet();
                    }
                    this.Vhost_Set();
                    this.Vplayers_SetContent();
                    if (this.foundImages.Count == this.SelectedDataset.ImagesCount) {
                        this.Vinsert_StopTimer();
                        this.Vinsert_TimerOut();
                        this.Vplayers_StopTimer();
                        this.Vplayers_TimerOut();
                    }
                }
            }
            this.setFoundImages();
        }
        private void setFoundImages() {
            this.FoundImages.Clear();
            if (this.SelectedDataset is DatasetContent) {
                if (this.foundImages.Contains(this.SelectedDataset.Image1Filename)) this.FoundImages.Add(true);
                else this.FoundImages.Add(false);
                if (this.foundImages.Contains(this.SelectedDataset.Image2Filename)) this.FoundImages.Add(true);
                else this.FoundImages.Add(false);
                if (this.foundImages.Contains(this.SelectedDataset.Image3Filename)) this.FoundImages.Add(true);
                else this.FoundImages.Add(false);
                if (this.foundImages.Contains(this.SelectedDataset.Image4Filename)) this.FoundImages.Add(true);
                else this.FoundImages.Add(false);
                if (this.foundImages.Contains(this.SelectedDataset.Image5Filename)) this.FoundImages.Add(true);
                else this.FoundImages.Add(false);
                if (this.foundImages.Contains(this.SelectedDataset.Image6Filename)) this.FoundImages.Add(true);
                else this.FoundImages.Add(false);
                if (this.foundImages.Contains(this.SelectedDataset.Image7Filename)) this.FoundImages.Add(true);
                else this.FoundImages.Add(false);
                if (this.foundImages.Contains(this.SelectedDataset.Image8Filename)) this.FoundImages.Add(true);
                else this.FoundImages.Add(false);
            }
            this.on_PropertyChanged("FoundImages");
        }

        public void Fault() {
            if (this.insertMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.PlayJingleFalse();
            this.Vinsert_TimerOut();
            this.fault = true;
        }

        public void CalcScore() {
            if (this.TaskCounter > 0) {
                switch (this.BuzzeredPlayer) {
                    case PlayerSelection.LeftPlayer:
                        if (this.fault) this.RightPlayerScore++;
                        else this.LeftPlayerScore++;
                        break;
                    case PlayerSelection.RightPlayer:
                        if (this.fault) this.LeftPlayerScore++;
                        else this.RightPlayerScore++;
                        break;
                }
                this.Vinsert_SetScore();
                this.Vstage_SetScore();
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

        public void Vinsert_BorderIn(VentuzScenes.GamePool._Modules.Border scene) {
            this.Vinsert_SetBorder();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Border.ToIn();
        }
        public void Vinsert_BorderIn() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.Vinsert_SetBorder(this.insertScene.Border, this.LeftPlayerScore, this.RightPlayerScore);
                this.insertScene.Border.ToIn();
            }
        }
        public void Vinsert_SetBorder() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.Vinsert_SetBorder(this.insertScene.Border, this.LeftPlayerScore, this.RightPlayerScore);
                this.Vinsert_SetBorderBuzzer(this.insertScene.Border, this.BuzzeredPlayer);
            }
        }
        public void Vinsert_SetBorder(
            VentuzScenes.GamePool._Modules.Border scene,
            int leftPlayerScore,
            int rightPlayerScore) {
            if (scene is VentuzScenes.GamePool._Modules.Border) {
                scene.SetPositionX(this.BorderPositionX);
                scene.SetPositionY(this.BorderPositionY);
                scene.SetScaling(this.BorderScaling);
                scene.SetStyle(this.BorderStyle);
                scene.SetLeftScore(leftPlayerScore);
                scene.SetRightScore(rightPlayerScore);
            }
        }
        public void Vinsert_SetBorderBuzzer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetBorderBuzzer(this.insertScene.Border, this.BuzzeredPlayer); }
        public void Vinsert_SetBorderBuzzer(PlayerSelection buzzeredPlayer) { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetBorderBuzzer(this.insertScene.Border, buzzeredPlayer); }
        public void Vinsert_SetBorderBuzzer(
            VentuzScenes.GamePool._Modules.Border scene,
            PlayerSelection buzzeredPlayer) {
            if (scene is VentuzScenes.GamePool._Modules.Border) {
                switch (buzzeredPlayer) {
                    case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.LeftPlayer:
                        scene.SetBuzzerLeft();
                        break;
                    case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.RightPlayer:
                        scene.SetBuzzerRight();
                        break;
                    case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.NotSelected:
                    default:
                        scene.ResetBuzzer();
                        break;
                }
            }
        }
        public void Vinsert_BorderOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Border.ToOut(); }

        public override void Vinsert_TimerIn() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.Vinsert_ResetTimer(this.insertScene.Timer);
                this.Vinsert_TimerIn(this.insertScene.Timer);
            }
        }
        public override void Vinsert_SetTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetTimer(this.insertScene.Timer); }
        public override void Vinsert_StartTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_StartTimer(this.insertScene.Timer); }
        public override void Vinsert_StopTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_StopTimer(this.insertScene.Timer); }
        public override void Vinsert_ContinueTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ContinueTimer(this.insertScene.Timer); }
        public override void Vinsert_ResetTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ResetTimer(this.insertScene.Timer); }
        public override void Vinsert_TimerOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_TimerOut(this.insertScene.Timer); }

        public void Vinsert_ContentIn() {
            this.fault = false;
            this.Vinsert_SetContent();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.ImagesToIn();
        }
        public void Vinsert_SetContent() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                int imagesCount = 0;
                if (this.SelectedDataset is DatasetContent) imagesCount = this.SelectedDataset.ImagesCount;
                this.Vinsert_SetContent(this.insertScene, imagesCount, this.foundImages);
            }
        }
        public void Vinsert_SetContent(
            Insert scene,
            int imagesCount,
            List<string> imagesFilename) {
            if (scene is Insert) {
                scene.SetImagesPositionX(this.InsertPositionX);
                scene.SetImagesPositionY(this.InsertPositionY);
                scene.SetImagesCount(imagesCount);
                scene.SetImagesFilename(imagesFilename);
            }
        }
        public void Vinsert_ContentOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.ImagesToOut(); }

        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }
        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload(); 
        }

        public override void Vfullscreen_LoadScene() { this.fullscreenScene.Load(); }
        public void Vfullscreen_ContentIn() {
            if (this.fullscreenScene is VRemote4.HandlerSi.Scene &&
                this.SelectedDataset is DatasetContent) {
                this.Vfullscreen_SetContent(this.fullscreenScene, this.SelectedDataset.PictureFilename);
                this.fullscreenScene.Deselect();
                this.fullscreenScene.ToIn();
            }
        }
        public void Vfullscreen_SetContent(
            Fullscreen scene,
            string pictureFilename) {
            if (scene is Fullscreen) scene.SetPictureFilename(pictureFilename);
        }
        public void Vfullscreen_ContentOut() {
            if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.ToOut();
        }
        public override void Vfullscreen_UnloadScene() { this.fullscreenScene.Unload(); }

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

        public override void Vhost_LoadScene() { this.hostScene.Load(); }
        public void Vhost_In() {
            if (this.hostScene is Host) {
                this.Vhost_Set();
                this.hostScene.ToIn();
            }
        }
        public void Vhost_Set() {
            List<bool> resolved = new List<bool>();
            if (this.SelectedDataset is DatasetContent) {
                resolved.Add(this.foundImages.Contains(this.SelectedDataset.Image1Filename));
                resolved.Add(this.foundImages.Contains(this.SelectedDataset.Image2Filename));
                resolved.Add(this.foundImages.Contains(this.SelectedDataset.Image3Filename));
                resolved.Add(this.foundImages.Contains(this.SelectedDataset.Image4Filename));
                resolved.Add(this.foundImages.Contains(this.SelectedDataset.Image5Filename));
                resolved.Add(this.foundImages.Contains(this.SelectedDataset.Image6Filename));
                resolved.Add(this.foundImages.Contains(this.SelectedDataset.Image7Filename));
                resolved.Add(this.foundImages.Contains(this.SelectedDataset.Image8Filename));
            }
            this.Vhost_Set(this.hostScene, this.SelectedDataset, resolved.ToArray());
        }
        public void Vhost_Set(
            Host scene,
            DatasetContent dataset,
            bool[] resolved) {
            if (scene is Host) {
                if (dataset is DatasetContent) {
                    scene.SetTitle(dataset.Name);
                    scene.SetImagesFilename(1, dataset.Image1Filename);
                    scene.SetImagesFilename(2, dataset.Image2Filename);
                    scene.SetImagesFilename(3, dataset.Image3Filename);
                    scene.SetImagesFilename(4, dataset.Image4Filename);
                    scene.SetImagesFilename(5, dataset.Image5Filename);
                    scene.SetImagesFilename(6, dataset.Image6Filename);
                    scene.SetImagesFilename(7, dataset.Image7Filename);
                    scene.SetImagesFilename(8, dataset.Image8Filename);
                    scene.SetImagesText(1, dataset.Image1Text);
                    scene.SetImagesText(2, dataset.Image2Text);
                    scene.SetImagesText(3, dataset.Image3Text);
                    scene.SetImagesText(4, dataset.Image4Text);
                    scene.SetImagesText(5, dataset.Image5Text);
                    scene.SetImagesText(6, dataset.Image6Text);
                    scene.SetImagesText(7, dataset.Image7Text);
                    scene.SetImagesText(8, dataset.Image8Text);
                    for (int i = 0; i < 8; i++) {
                        if (resolved is bool[] &&
                            resolved.Length > i) scene.SetImagesResolved(i + 1, resolved[i]);
                        else scene.SetImagesResolved(i + 1, false);
                    }
                }
                else {
                    scene.SetTitle(string.Empty);
                    for (int i = 1; i < 8; i++) {
                        scene.SetImagesFilename(i, string.Empty);
                        scene.SetImagesText(i, string.Empty);
                        scene.SetImagesResolved(i, false);
                    }
                }
            }
        }
        public void Vhost_Out() { if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ToOut(); }
        public override void Vhost_UnloadScene() { this.hostScene.Unload(); }

        public override void Vleftplayer_LoadScene() { this.leftPlayerScene.Load(); }
        public override void Vleftplayer_UnloadScene() { this.leftPlayerScene.Unload(); }

        public override void Vrightplayer_LoadScene() { this.rightPlayerScene.Load(); }
        public override void Vrightplayer_UnloadScene() { this.rightPlayerScene.Unload(); }

        public void Vplayers_LiveVideoIn() {
            if (this.leftPlayerMasterScene is VRemote4.HandlerSi.Scene) this.leftPlayerMasterScene.LiveVideoIn();
            if (this.rightPlayerMasterScene is VRemote4.HandlerSi.Scene) this.rightPlayerMasterScene.LiveVideoIn();
        }
        public void Vplayers_LiveVideoOut() {
            if (this.leftPlayerMasterScene is VRemote4.HandlerSi.Scene) this.leftPlayerMasterScene.LiveVideoOut();
            if (this.rightPlayerMasterScene is VRemote4.HandlerSi.Scene) this.rightPlayerMasterScene.LiveVideoOut();
        }
        internal void Vplayers_ContentIn() {
            if (this.leftPlayerScene is VRemote4.HandlerSi.Scene) {
                if (this.SelectedDataset is DatasetContent) this.Vplayers_SetContent(this.leftPlayerScene, this.SelectedDataset.ImagesCount, this.foundImages);
                this.leftPlayerScene.ImagesToIn();
            }
            if (this.leftPlayerScene is VRemote4.HandlerSi.Scene) {
                if (this.SelectedDataset is DatasetContent) this.Vplayers_SetContent(this.rightPlayerScene, this.SelectedDataset.ImagesCount, this.foundImages);
                this.rightPlayerScene.ImagesToIn();
            }
        }
        public void Vplayers_SetContent() {
            if (this.SelectedDataset is DatasetContent) {
                this.Vplayers_SetContent(this.leftPlayerScene, this.SelectedDataset.ImagesCount, this.foundImages);
                this.Vplayers_SetContent(this.rightPlayerScene, this.SelectedDataset.ImagesCount, this.foundImages);
            }
        }
        public void Vplayers_SetContent(
            VentuzScenes.GamePool.Remembory.Player scene,
            int imagesCount,
            List<string> imagesFilename) {
            if (scene is VRemote4.HandlerSi.Scene) {
                scene.SetImagesCount(imagesCount);
                scene.SetImagesFilename(imagesFilename);
            }
        }
        internal void Vplayers_ContentOut() {
            if (this.leftPlayerScene is VRemote4.HandlerSi.Scene) this.leftPlayerScene.ImagesToOut();
            if (this.rightPlayerScene is VRemote4.HandlerSi.Scene) this.rightPlayerScene.ImagesToOut();
        }

        public void Vplayers_TimerIn() {
            if (this.leftPlayerScene is VRemote4.HandlerSi.Scene) this.Vplayers_TimerIn(this.leftPlayerScene.Timer);
            if (this.rightPlayerScene is VRemote4.HandlerSi.Scene) this.Vplayers_TimerIn(this.rightPlayerScene.Timer);
        }
        public void Vplayers_TimerIn(
            VentuzScenes.GamePool._Modules.Timer scene) {
            if (scene is VentuzScenes.GamePool._Modules.Timer) {
                this.Vplayers_SetTimer(scene, this.TimerStartTime);
                this.Vplayers_ResetTimer(this.leftPlayerScene.Timer);
                scene.ToIn();
            }
        }
        public void Vplayers_SetTimer() {
            if (this.leftPlayerScene is VRemote4.HandlerSi.Scene) this.Vplayers_SetTimer(this.leftPlayerScene.Timer, this.TimerStartTime);
            if (this.rightPlayerScene is VRemote4.HandlerSi.Scene) this.Vplayers_SetTimer(this.rightPlayerScene.Timer, this.TimerStartTime);
        }
        public void Vplayers_SetTimer(
            VentuzScenes.GamePool._Modules.Timer scene,
            int startTime) {
            if (scene is VentuzScenes.GamePool._Modules.Timer) {
                scene.SetPositionX(0);
                scene.SetPositionY(0);
                scene.SetStyle(this.TimerStyle);
                scene.SetScaling(100);
                scene.SetStartTime(startTime);
                scene.SetStopTime(this.TimerStopTime);
                scene.SetAlarmTime1(-1);
                scene.SetAlarmTime2(-1);
            }
        }
        public void Vplayers_StartTimer() {
            if (this.leftPlayerScene is VRemote4.HandlerSi.Scene) this.Vplayers_StartTimer(this.leftPlayerScene.Timer);
            if (this.rightPlayerScene is VRemote4.HandlerSi.Scene) this.Vplayers_StartTimer(this.rightPlayerScene.Timer);
        }
        public void Vplayers_StartTimer(VentuzScenes.GamePool._Modules.Timer scene) { if (scene is VentuzScenes.GamePool._Modules.Timer) scene.StartTimer(); }
        public void Vplayers_StopTimer() {
            if (this.leftPlayerScene is VRemote4.HandlerSi.Scene) this.Vplayers_StopTimer(this.leftPlayerScene.Timer);
            if (this.rightPlayerScene is VRemote4.HandlerSi.Scene) this.Vplayers_StopTimer(this.rightPlayerScene.Timer);
        }
        public void Vplayers_StopTimer(VentuzScenes.GamePool._Modules.Timer scene) { if (scene is VentuzScenes.GamePool._Modules.Timer) scene.StopTimer(); }
        public  void Vplayers_ContinueTimer() {
            if (this.leftPlayerScene is VRemote4.HandlerSi.Scene) this.Vplayers_ContinueTimer(this.leftPlayerScene.Timer);
            if (this.rightPlayerScene is VRemote4.HandlerSi.Scene) this.Vplayers_ContinueTimer(this.rightPlayerScene.Timer);
        }
        public void Vplayers_ContinueTimer(VentuzScenes.GamePool._Modules.Timer scene) { if (scene is VentuzScenes.GamePool._Modules.Timer) scene.ContinueTimer(); }
        public void Vplayers_ResetTimer() {
            if (this.leftPlayerScene is VRemote4.HandlerSi.Scene) this.Vplayers_ResetTimer(this.leftPlayerScene.Timer);
            if (this.rightPlayerScene is VRemote4.HandlerSi.Scene) this.Vplayers_ResetTimer(this.rightPlayerScene.Timer);
        }
        public void Vplayers_ResetTimer(VentuzScenes.GamePool._Modules.Timer scene) { if (scene is VentuzScenes.GamePool._Modules.Timer) scene.ResetTimer(); }
        public void Vplayers_TimerOut() {
            if (this.leftPlayerScene is VRemote4.HandlerSi.Scene) this.Vplayers_TimerOut(this.leftPlayerScene.Timer);
            if (this.rightPlayerScene is VRemote4.HandlerSi.Scene) this.Vplayers_TimerOut(this.rightPlayerScene.Timer);
        }
        public void Vplayers_TimerOut(VentuzScenes.GamePool._Modules.Timer scene) { if (scene is VentuzScenes.GamePool._Modules.Timer) scene.ToOut(); }

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
        
        protected override void sync_timer_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "CurrentTime") this.TimerCurrentTime = this.insertScene.Timer.CurrentTime;
                else if (e.PropertyName == "IsRunning") this.TimerIsRunning = this.insertScene.Timer.IsRunning;
            }
        }

        #endregion

    }
}
