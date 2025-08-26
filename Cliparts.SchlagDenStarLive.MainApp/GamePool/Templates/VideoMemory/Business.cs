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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules;
using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.VideoMemory;
using NReco.VideoConverter;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.VideoMemory {

    public class VideoDataset : INotifyPropertyChanged {

        #region Properties

        private string name = string.Empty;
        [XmlIgnore]
        public string Name {
            get { return this.name; }
            private set {
                if (this.name != value) {
                    if (string.IsNullOrEmpty(value)) this.name = string.Empty;
                    else this.name = value;
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
                    this.Thumbnail = Helper.getThumbnailFromMediaFile(this.filename, 0.25);
                    this.on_PropertyChanged("Thumbnail");
                    this.Name = Path.GetFileNameWithoutExtension(this.filename);
                }
            }
        }

        [XmlIgnore]
        public Image Thumbnail { get; private set; }

        private int firstBuzzerID = 0;
        public int FirstBuzzerID {
            get { return this.firstBuzzerID; }
            set {
                if (this.firstBuzzerID != value) {
                    this.firstBuzzerID = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool firstBuzzerIDIsValid = true;
        [XmlIgnore]
        public bool FirstBuzzerIDIsValid {
            get { return this.firstBuzzerIDIsValid; }
            set {
                if (this.firstBuzzerIDIsValid != value) {
                    this.firstBuzzerIDIsValid = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int secondBuzzerID = 0;
        public int SecondBuzzerID {
            get { return this.secondBuzzerID; }
            set {
                if (this.secondBuzzerID != value) {
                    this.secondBuzzerID = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool secondBuzzerIDIsValid = true;
        [XmlIgnore]
        public bool SecondBuzzerIDIsValid {
            get { return this.secondBuzzerIDIsValid; }
            set {
                if (this.secondBuzzerIDIsValid != value) {
                    this.secondBuzzerIDIsValid = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public VideoDataset() {}

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

    public class Data {
        public VideoDataset[] DataList;
    }

    public class SelectedBuzzer : INotifyPropertyChanged
    {

        #region Properties

        public int BuzzerID { get; private set; }

        public VideoDataset Video { get; private set; }

        #endregion


        #region Funktionen

        public SelectedBuzzer(
            int buzzerID,
            VideoDataset video) 
        { 
            this.BuzzerID = buzzerID;
            this.Video = video;
            this.buildToString();
        }

        string toString = string.Empty;

        private void buildToString()
        {
            this.toString = string.Format("{0} - {1}", this.BuzzerID.ToString("00"), this.Video.Name);
        }

        public override string ToString()
        {
            return this.toString;
        }

        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void on_PropertyChanged([CallerMemberName] string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected virtual void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected virtual void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }

    public class Business : _Base.Score.Business {

        public const int VideosCount = 21;
        public const int BuzzerCount = 42;

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

        private int insertPlayerPositionX = 0;
        public int InsertPlayerPositionX
        {
            get { return this.insertPlayerPositionX; }
            set
            {
                if (this.insertPlayerPositionX != value)
                {
                    this.insertPlayerPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetVideo();
                }
            }
        }

        private int insertPlayerPositionY = -350;
        public int InsertPlayerPositionY
        {
            get { return this.insertPlayerPositionY; }
            set
            {
                if (this.insertPlayerPositionY != value)
                {
                    this.insertPlayerPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetVideo();
                }
            }
        }

        private int insertPlayerScaling = 25;
        public int InsertPlayerScaling
        {
            get { return this.insertPlayerScaling; }
            set
            {
                if (this.insertPlayerScaling != value)
                {
                    if (value < 10) this.insertPlayerScaling = 10;
                    else this.insertPlayerScaling = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetVideo();
                }
            }
        }

        private List<VideoDataset> datalist = new List<VideoDataset>();
        public VideoDataset[] DataList {
            get { return this.datalist.ToArray(); }
            private set {
                int index = 0;
                foreach (VideoDataset item in this.datalist) {
                    if (value is VideoDataset[] && 
                        value.Length > index &&
                        value[index] is VideoDataset) {
                        item.Filename = value[index].Filename;
                        item.FirstBuzzerID = value[index].FirstBuzzerID;
                        item.SecondBuzzerID = value[index].SecondBuzzerID;
                    }
                    index++;
                }
            }
        }

        public List<int> UnusedBuzzerIDs { get; private set; } = new List<int>();

        public List<SelectedBuzzer> SelectedBuzzers { get; private set; } = new List<SelectedBuzzer>();

        private string filename = string.Empty;
        public string Filename {
            get { return this.filename; }
            set { if (this.filename != value) this.Load(value); }
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

        private VRemote4.HandlerSi.Business localVentuzHandler;

        private VRemote4.HandlerSi.Client.Business gameboardClient;
        private Game ventuzGameboardScene;
        public VRemote4.HandlerSi.Scene.States VentuzGameboardSceneStatus {
            get {
                if (this.ventuzGameboardScene is Game) return this.ventuzGameboardScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }
        private string gameboardClientHostname = string.Empty;
        public string GameboardClientHostname {
            get { return this.gameboardClientHostname; }
            set {
                if (this.gameboardClientHostname != value) {
                    if (string.IsNullOrEmpty(value)) this.gameboardClientHostname = string.Empty;
                    else this.gameboardClientHostname = value;
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

            for (int i = 0; i < VideosCount; i++) {
                VideoDataset newDataset = new VideoDataset();
                newDataset.PropertyChanged += this.songDataset_PropertyChanged;
                this.datalist.Add(newDataset);
            }

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.VideoMemory'", typeIdentifier);
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

            this.fullscreenScene = new Fullscreen(syncContext, fullscreenMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.fullscreenScene.StatusChanged += this.fullscreenScene_StatusChanged;
            this.fullscreenScene.Game.TouchPressed += this.fullscreenGame_TouchPressed;
            this.fullscreenScene.Player.Completed += this.fullscreenPlayer_Completed;

            this.localVentuzHandler = new VRemote4.HandlerSi.Business(syncContext);
            this.localVentuzHandler.Error += this.localVentuzHandler_Error;

            if (this.localVentuzHandler.TryAddClient("Gameboard", false, out this.gameboardClient)) {
                this.gameboardClient.HostnameChanged += this.gameboardClient_HostnameChanged;
                this.gameboardClient.StatusChanged += this.gameboardClient_StatusChanged;
                this.ventuzGameboardScene = new Game(syncContext, this.gameboardClient, 0);
                this.ventuzGameboardScene.StatusChanged += this.ventuzGameboardScene_StatusChanged;
                this.ventuzGameboardScene.TouchPressed += this.ventuzGameboardScene_TouchPressed;         
            }

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this, this.localVentuzHandler);

        }

        public override void Dispose() {
            base.Dispose();

            foreach (VideoDataset item in this.datalist) item.PropertyChanged -= this.songDataset_PropertyChanged;
            this.datalist.Clear();

            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Dispose();

            this.fullscreenScene.StatusChanged -= this.fullscreenScene_StatusChanged;
            this.fullscreenScene.Game.TouchPressed -= this.fullscreenGame_TouchPressed;
            this.fullscreenScene.Player.Completed -= this.fullscreenPlayer_Completed;
            this.fullscreenScene.Dispose();

            this.gameboardClient.Shutdown();
            if (this.gameboardClient is VRemote4.HandlerSi.Client.Business) {
                this.gameboardClient.HostnameChanged -= this.gameboardClient_HostnameChanged;
                this.gameboardClient.StatusChanged -= this.gameboardClient_StatusChanged;
                this.gameboardClient.Shutdown();
            }
            if (this.ventuzGameboardScene is VRemote4.HandlerSi.Scene) {
                this.ventuzGameboardScene.StatusChanged -= this.ventuzGameboardScene_StatusChanged;
                this.ventuzGameboardScene.Dispose();
            }

        }

        public override void Init()
        {
            base.Init();
            this.SelectedBuzzers.Clear();
            this.on_PropertyChanged(nameof(this.SelectedBuzzers));
        }

        internal void StartNext()
        {
            if (this.SelectedBuzzers.Count >= 2) 
            { 
                if (this.SelectedBuzzers[0].Video == this.SelectedBuzzers[1].Video)
                {
                    this.Vfullscreen_SelectionToOut();
                    this.Vgameboard_SelectionToOut();
                }
                else
                {
                    this.Vfullscreen_SelectionToIn();
                    this.Vgameboard_SelectionToIn();
                }
                this.SelectedBuzzers.Clear();
                this.on_PropertyChanged(nameof(this.SelectedBuzzers));
            }
            this.Vfullscreen_EnableTouch();
            this.Vgameboard_EnableTouch();
        }

        internal void TouchPressed(int id)
        {
            if (this.SelectedBuzzers.Count < 2)
            {
                foreach(var item in this.DataList)
                {
                    if (item.FirstBuzzerID.Equals(id) ||
                        item.SecondBuzzerID.Equals(id))
                    {
                        this.SelectedBuzzers.Add(new SelectedBuzzer(id, item));
                        this.Vfullscreen_ToSelected(id);
                        this.Vfullscreen_PlayVideo(item.Filename);
                        this.Vinsert_PlayVideo(item.Filename);
                        this.on_PropertyChanged(nameof(this.SelectedBuzzers));
                        break;
                    }
                }
            }
        }

        public bool TryGetVideoDataset(
            int index,
            out VideoDataset value) {
            value = null;
            if (index >= 0 && index < this.DataList.Length) {
                value = this.DataList[index];
                return true;
            }
            else return false;
        }

        private void validateData() {
            Dictionary<int, int> usedCards = new Dictionary<int, int>();
            foreach (VideoDataset item in this.datalist) {
                if (usedCards.ContainsKey(item.FirstBuzzerID)) usedCards[item.FirstBuzzerID]++;
                else usedCards.Add(item.FirstBuzzerID, 1);
                if (usedCards.ContainsKey(item.SecondBuzzerID)) usedCards[item.SecondBuzzerID]++;
                else usedCards.Add(item.SecondBuzzerID, 1);
            }

            foreach (VideoDataset item in this.datalist) {
                item.FirstBuzzerIDIsValid = item.FirstBuzzerID >= 1 && item.FirstBuzzerID <= BuzzerCount && usedCards.ContainsKey(item.FirstBuzzerID) && usedCards[item.FirstBuzzerID] == 1;
                item.SecondBuzzerIDIsValid = item.SecondBuzzerID >= 1 && item.SecondBuzzerID <= BuzzerCount && usedCards.ContainsKey(item.SecondBuzzerID) && usedCards[item.SecondBuzzerID] == 1;
            }

            this.UnusedBuzzerIDs.Clear();
            for (int i = 1; i <= BuzzerCount; i++) {
                if (!usedCards.ContainsKey(i)) this.UnusedBuzzerIDs.Add(i);
            }
            this.on_PropertyChanged(nameof(this.UnusedBuzzerIDs));
        }

        public void RandomizeData() {
            this.repressPropertyChanged = true;
            List<int> buzzerIDs = new List<int>();
            for (int i = 1; i <= BuzzerCount; i++) buzzerIDs.Add(i);
            VideoDataset song;
            Random rnd = new Random();
            for (int i = 0; i <= VideosCount; i++) {
                if (TryGetVideoDataset(i, out song)) {
                    int index = rnd.Next(0, buzzerIDs.Count);
                    song.FirstBuzzerID = buzzerIDs[index];
                    buzzerIDs.RemoveAt(index);
                    index = rnd.Next(0, buzzerIDs.Count);
                    song.SecondBuzzerID = buzzerIDs[index];
                    buzzerIDs.RemoveAt(index);
                }
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
            this.validateData();
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
        public void Vinsert_BorderIn() {
            this.Vinsert_SetBorder();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Border.ToIn();
        }
        public void Vinsert_SetBorder() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vinsert_SetBorder(this.insertScene.Border, this.LeftPlayerScore, this.RightPlayerScore); 
        }
        public void Vinsert_SetBorder(
            Border scene,
            int leftPlayerScore,
            int rightPlayerScore) {
            if (scene is Border) {
                scene.SetPositionX(this.BorderPositionX);
                scene.SetPositionY(this.BorderPositionY);
                scene.SetScaling(this.BorderScaling);
                scene.SetStyle(this.BorderStyle);
                scene.SetLeftName(this.LeftPlayerName);
                scene.SetLeftScore(leftPlayerScore);
                scene.SetRightName(this.RightPlayerName);
                scene.SetRightScore(rightPlayerScore);
            }
        }
        public void Vinsert_BorderOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Border.ToOut(); }
        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { 
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score);
            this.Vinsert_SetBorder();
        }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }
        public void Vinsert_PlayVideo(int id)
        {
            foreach (var item in this.DataList)
            {
                if (item.FirstBuzzerID.Equals(id))
                {
                    this.Vinsert_PlayVideo(item.Filename);
                    break;
                }
                else if (item.SecondBuzzerID.Equals(id))
                {
                    this.Vinsert_PlayVideo(item.Filename);
                    break;
                }
            }
        }
        public void Vinsert_PlayVideo(
            string filename)
        {
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_PlayVideo(this.insertScene.Player, filename);
        }
        public void Vinsert_PlayVideo(
            Player scene, 
            string filename)
        {
            this.Vinsert_SetVideo(scene, filename);
            if (scene is Player)scene.Start();
        }
        public void Vinsert_SetVideo()
        {
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetVideo(this.insertScene.Player);
        }
        public void Vinsert_SetVideo(
            Player scene)
        {
            if (scene is Player)
            {
                scene.SetShowBorder(true);
                scene.SetMuteAudio(true);
                scene.SetScaling(this.InsertPlayerScaling);
                scene.SetPositionX(this.InsertPlayerPositionX);
                scene.SetPositionY(this.InsertPlayerPositionY);
            }
        }
        public void Vinsert_SetVideo(
            Player scene,
            string filename)
        {
            this.Vinsert_SetVideo(scene);
            if (scene is Player) scene.SetFilename(filename);   
        }
        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vfullscreen_LoadScene() {
            base.Vfullscreen_LoadScene();
            this.fullscreenScene.Load(); 
        }
        public void Vfullscreen_Init() {
            this.Vfullscreen_Reset();
        }
        public void Vfullscreen_Reset() { if (this.fullscreenScene.Game is Game) this.fullscreenScene.Game.Reset(); }
        public void Vfullscreen_EnableTouch() { if (this.fullscreenScene.Game is Game) this.fullscreenScene.Game.EnableTouch(); }
        public void Vfullscreen_DisableTouch() { if (this.fullscreenScene.Game is Game) this.fullscreenScene.Game.DisableTouch(); }
        public void Vfullscreen_SelectionToIn() { if (this.fullscreenScene.Game is Game) this.fullscreenScene.Game.SelectionToIn(); }
        public void Vfullscreen_SelectionToOut() { if (this.fullscreenScene.Game is Game) this.fullscreenScene.Game.SelectionToOut(); }
        public void Vfullscreen_Reset(int buzzerID) { if (this.fullscreenScene.Game is Game) this.fullscreenScene.Game.Reset(buzzerID); }
        public void Vfullscreen_ToIn(int buzzerID) { if (this.fullscreenScene.Game is Game) this.fullscreenScene.Game.ToIn(buzzerID); }
        public void Vfullscreen_SetSelected(int buzzerID) { if (this.fullscreenScene.Game is Game) this.fullscreenScene.Game.SetSelected(buzzerID); }
        public void Vfullscreen_ToSelected(int buzzerID) { if (this.fullscreenScene.Game is Game) this.fullscreenScene.Game.ToSelected(buzzerID); }
        public void Vfullscreen_SetOut(int buzzerID) { if (this.fullscreenScene.Game is Game) this.fullscreenScene.Game.SetOut(buzzerID); }
        public void Vfullscreen_ToOut(int buzzerID) { if (this.fullscreenScene.Game is Game) this.fullscreenScene.Game.ToOut(buzzerID); }
        public void Vfullscreen_EnableTouch(int buzzerID) { if (this.fullscreenScene.Game is Game) this.fullscreenScene.Game.EnableTouch(buzzerID); }
        public void Vfullscreen_DisableTouch(int buzzerID) { if (this.fullscreenScene.Game is Game) this.fullscreenScene.Game.DisableTouch(buzzerID); }
        public void Vfullscreen_PlayVideo(int id)
        {
            foreach (var item in this.DataList)
            {
                if (item.FirstBuzzerID.Equals(id))
                {
                    this.Vfullscreen_PlayVideo(item.Filename);
                    break;
                }
                else if (item.SecondBuzzerID.Equals(id))
                {
                    this.Vfullscreen_PlayVideo(item.Filename);
                    break;
                }
            }
        }
        public void Vfullscreen_PlayVideo(string filename)
        {
            if (this.fullscreenScene.Player is Player)
            {
                this.fullscreenScene.Player.SetMuteAudio(false);
                this.fullscreenScene.Player.SetShowBorder(false);
                this.fullscreenScene.Player.SetFilename(filename);
                this.fullscreenScene.Player.Start();
            }
        }
        public override void Vfullscreen_UnloadScene() { this.fullscreenScene.Unload(); }

        public void Vgameboard_Start() { this.gameboardClient.Start(this.GameboardClientHostname); }
        public void Vgameboard_Init() {
            this.Vgameboard_Reset();
        }
        public void Vgameboard_Reset() { if (this.ventuzGameboardScene is Game) this.ventuzGameboardScene.Reset(); }
        public void Vgameboard_EnableTouch() { if (this.ventuzGameboardScene is Game) this.ventuzGameboardScene.EnableTouch(); }
        public void Vgameboard_DisableTouch() { if (this.ventuzGameboardScene is Game) this.ventuzGameboardScene.DisableTouch(); }
        public void Vgameboard_SelectionToIn() { if (this.fullscreenScene.Game is Game) this.ventuzGameboardScene.SelectionToIn(); }
        public void Vgameboard_SelectionToOut() { if (this.fullscreenScene.Game is Game) this.ventuzGameboardScene.SelectionToOut(); }
        public void Vgameboard_Reset(int buzzerID) { if (this.ventuzGameboardScene is Game) this.ventuzGameboardScene.Reset(buzzerID); }
        public void Vgameboard_ToIn(int buzzerID) { if (this.ventuzGameboardScene is Game) this.ventuzGameboardScene.ToIn(buzzerID); }
        public void Vgameboard_SetSelected(int buzzerID) { if (this.ventuzGameboardScene is Game) this.ventuzGameboardScene.SetSelected(buzzerID); }
        public void Vgameboard_ToSelected(int buzzerID) { if (this.ventuzGameboardScene is Game) this.ventuzGameboardScene.ToSelected(buzzerID); }
        public void Vgameboard_SetOut(int buzzerID) { if (this.ventuzGameboardScene is Game) this.ventuzGameboardScene.SetOut(buzzerID); }
        public void Vgameboard_ToOut(int buzzerID) { if (this.ventuzGameboardScene is Game) this.ventuzGameboardScene.ToOut(buzzerID); }
        public void Vgameboard_EnableTouch(int buzzerID) { if (this.ventuzGameboardScene is Game) this.ventuzGameboardScene.EnableTouch(buzzerID); }
        public void Vgameboard_DisableTouch(int buzzerID) { if (this.ventuzGameboardScene is Game) this.ventuzGameboardScene.DisableTouch(buzzerID); }
        public void Vgameboards_ShutDown() { this.gameboardClient.Shutdown(); }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming

        void songDataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_songDataset_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_songDataset_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (!this.repressPropertyChanged &&
                    e is PropertyChangedEventArgs &&
                    (e.PropertyName == "Filename" || e.PropertyName == "FirstCardID" || e.PropertyName == "SecondCardID")) {
                    this.Save();
                    this.validateData();
                }
            }
        }

        void localVentuzHandler_Error(object sender, ErrorEventArgs e) {
            Exception exc = e.GetException();
            this.on_Error(sender, new Messaging.ErrorEventArgs(this, sender.ToString(), DateTime.Now, exc.ToString()));
        }

        private void fullscreenPlayer_Completed(object sender, EventArgs e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_fullscreenGame_PlayerCompleted);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_fullscreenGame_PlayerCompleted(object content)
        {
            if (this.SelectedBuzzers.Count < 2)
            {
                this.Vfullscreen_EnableTouch();
                this.Vgameboard_EnableTouch();
            }
        }

        private void fullscreenGame_TouchPressed(object sender, int e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_fullscreenGame_TouchPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_fullscreenGame_TouchPressed(object content)
        {
            //if (content is Int32) this.TouchPressed(Convert.ToInt32(content));
        }

        void gameboardClient_HostnameChanged(object sender, VRemote4.HandlerSi.Client.HostnameArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_gameboardClient_HostnameChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_gameboardClient_HostnameChanged(object content) {
            VRemote4.HandlerSi.Client.HostnameArgs e = content as VRemote4.HandlerSi.Client.HostnameArgs;
            if (e is VRemote4.HandlerSi.Client.HostnameArgs) this.GameboardClientHostname = e.Name;
        }

        void gameboardClient_StatusChanged(object sender, VRemote4.HandlerSi.Client.ClientStateArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_gameboardClient_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_gameboardClient_StatusChanged(object content) {
            VRemote4.HandlerSi.Client.ClientStateArgs e = content as VRemote4.HandlerSi.Client.ClientStateArgs;
            if (e is VRemote4.HandlerSi.Client.ClientStateArgs &&
                e.Status.HasValue &&
                e.Status.Value == VRemote4.HandlerSi.Client.ClientStates.Active &&
                this.ventuzGameboardScene is VRemote4.HandlerSi.Scene) this.ventuzGameboardScene.Load();
        }

        void ventuzGameboardScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzGameboardScene_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzGameboardScene_StatusChanged(object content) {
            VRemote4.HandlerSi.Scene.StatusArgs e = content as VRemote4.HandlerSi.Scene.StatusArgs;
            if (e is VRemote4.HandlerSi.Scene.StatusArgs) {
                if (e.Status == VRemote4.HandlerSi.Scene.States.Unloaded) this.gameboardClient.Shutdown();
                else if (e.Status == VRemote4.HandlerSi.Scene.States.Available) this.Vgameboard_Init();
            }
            this.on_PropertyChanged("VentuzGameboardSceneStatus");
        }

        void ventuzGameboardScene_TouchPressed(object sender, int e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzGameboardScene_TouchPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzGameboardScene_TouchPressed(object content) {
            if (content is Int32) this.TouchPressed(Convert.ToInt32(content));
        }

        #endregion

    }
}
