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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.AudioMemory;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.AudioMemory {

    public class SongDataset : INotifyPropertyChanged {

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
                    this.Name = Path.GetFileNameWithoutExtension(this.filename);
                }
            }
        }

        private int firstCardID = 0;
        public int FirstCardID {
            get { return this.firstCardID; }
            set {
                if (this.firstCardID != value) {
                    this.firstCardID = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool firstCardIDIsValid = true;
        [XmlIgnore]
        public bool FirstCardIDIsValid {
            get { return this.firstCardIDIsValid; }
            set {
                if (this.firstCardIDIsValid != value) {
                    this.firstCardIDIsValid = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int secondCardID = 0;
        public int SecondCardID {
            get { return this.secondCardID; }
            set {
                if (this.secondCardID != value) {
                    this.secondCardID = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool secondCardIDIsValid = true;
        [XmlIgnore]
        public bool SecondCardIDIsValid {
            get { return this.secondCardIDIsValid; }
            set {
                if (this.secondCardIDIsValid != value) {
                    this.secondCardIDIsValid = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public SongDataset() {}

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
        public SongDataset[] DataList;
    }

    public class Business : _Base.Score.Business {

        public const int SongsCount = 21;
        public const int CardsCount = 42;

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

        private List<SongDataset> datalist = new List<SongDataset>();
        public SongDataset[] DataList {
            get { return this.datalist.ToArray(); }
            private set {
                int index = 0;
                foreach (SongDataset item in this.datalist) {
                    if (value is SongDataset[] && 
                        value.Length > index &&
                        value[index] is SongDataset) {
                        item.Filename = value[index].Filename;
                        item.FirstCardID = value[index].FirstCardID;
                        item.SecondCardID = value[index].SecondCardID;
                    }
                    index++;
                }
            }
        }

        public List<int> UnusedCardIDs { get; private set; }

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

            this.UnusedCardIDs = new List<int>();

            for (int i = 0; i < SongsCount; i++) {
                SongDataset newDataset = new SongDataset();
                newDataset.PropertyChanged += this.songDataset_PropertyChanged;
                this.datalist.Add(newDataset);
            }

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.AudioMemory'", typeIdentifier);
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

            this.localVentuzHandler = new VRemote4.HandlerSi.Business(syncContext);
            this.localVentuzHandler.Error += this.localVentuzHandler_Error;

            if (this.localVentuzHandler.TryAddClient("Gameboard", false, out this.gameboardClient)) {
                this.gameboardClient.HostnameChanged += this.gameboardClient_HostnameChanged;
                this.gameboardClient.StatusChanged += this.gameboardClient_StatusChanged;
                this.ventuzGameboardScene = new Game(syncContext, this.gameboardClient, 0);
                this.ventuzGameboardScene.StatusChanged += this.ventuzGameboardScene_StatusChanged;
                this.ventuzGameboardScene.TouchPressed += this.ventuzGameboardScene_TouchPressed;         
            }

            ((AudioMemory.UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((AudioMemory.UserControlGame)this.GameControl).Pose(this, this.localVentuzHandler);

        }

        public override void Dispose() {
            base.Dispose();

            foreach (SongDataset item in this.datalist) item.PropertyChanged -= this.songDataset_PropertyChanged;
            this.datalist.Clear();

            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Dispose();

            this.fullscreenScene.StatusChanged -= this.fullscreenScene_StatusChanged;
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

        public bool TryGetSongDataset(
            int index,
            out SongDataset value) {
            value = null;
            if (index >= 0 && index < this.DataList.Length) {
                value = this.DataList[index];
                return true;
            }
            else return false;
        }

        private void validateData() {
            Dictionary<int, int> usedCards = new Dictionary<int, int>();
            foreach (SongDataset item in this.datalist) {
                if (usedCards.ContainsKey(item.FirstCardID)) usedCards[item.FirstCardID]++;
                else usedCards.Add(item.FirstCardID, 1);
                if (usedCards.ContainsKey(item.SecondCardID)) usedCards[item.SecondCardID]++;
                else usedCards.Add(item.SecondCardID, 1);
            }

            foreach (SongDataset item in this.datalist) {
                item.FirstCardIDIsValid = item.FirstCardID >= 1 && item.FirstCardID <= CardsCount && usedCards.ContainsKey(item.FirstCardID) && usedCards[item.FirstCardID] == 1;
                item.SecondCardIDIsValid = item.SecondCardID >= 1 && item.SecondCardID <= CardsCount && usedCards.ContainsKey(item.SecondCardID) && usedCards[item.SecondCardID] == 1;
            }

            this.UnusedCardIDs.Clear();
            for (int i = 1; i <= CardsCount; i++) {
                if (!usedCards.ContainsKey(i)) this.UnusedCardIDs.Add(i);
            }
            this.on_PropertyChanged("UnusedCardIDs");
        }

        public void RandomizeData() {
            this.repressPropertyChanged = true;
            List<int> cardIDs = new List<int>();
            for (int i = 1; i <= CardsCount; i++) cardIDs.Add(i);
            SongDataset song;
            Random rnd = new Random();
            for (int i = 0; i <= SongsCount; i++) {
                if (TryGetSongDataset(i, out song)) {
                    int index = rnd.Next(0, cardIDs.Count);
                    song.FirstCardID = cardIDs[index];
                    cardIDs.RemoveAt(index);
                    index = rnd.Next(0, cardIDs.Count);
                    song.SecondCardID = cardIDs[index];
                    cardIDs.RemoveAt(index);
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
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.Border.SetPositionX(this.BorderPositionX);
                this.insertScene.Border.SetPositionY(this.BorderPositionY);
                this.insertScene.Border.SetScaling(this.BorderScaling);
                this.insertScene.Border.SetStyle(this.BorderStyle);
                this.insertScene.Border.SetLeftName(this.LeftPlayerName);
                this.insertScene.Border.SetLeftScore(this.LeftPlayerScore);
                this.insertScene.Border.SetRightName(this.RightPlayerName);
                this.insertScene.Border.SetRightScore(this.RightPlayerScore);
            }
        }
        public void Vinsert_BorderOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Border.ToOut(); }
        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { 
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score);
            this.Vinsert_SetBorder();
        }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }
        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vfullscreen_LoadScene() {
            base.Vfullscreen_LoadScene();
            this.fullscreenScene.Load(); 
        }
        public void Vfullscreen_Init() {
            this.Vfullscreen_SetContent();
            this.Vfullscreen_Reset();
        }
        public void Vfullscreen_SetContent() {
            if (this.fullscreenScene.Game is Game) {
                Game game = this.fullscreenScene.Game;
                foreach (SongDataset item in this.datalist) {
                    game.SetAudiofile(item.FirstCardID, item.Filename);
                    game.SetAudiofile(item.SecondCardID, item.Filename);
                }
            }
        }
        public void Vfullscreen_Reset() { if (this.fullscreenScene.Game is Game) this.fullscreenScene.Game.Reset(); }
        public void Vfullscreen_Next() { if (this.fullscreenScene.Game is Game) this.fullscreenScene.Game.Next(); }
        public void Vfullscreen_EnableTouch() { if (this.fullscreenScene.Game is Game) this.fullscreenScene.Game.EnableTouch(); }
        public void Vfullscreen_DisableTouch() { if (this.fullscreenScene.Game is Game) this.fullscreenScene.Game.DisableTouch(); }
        public void Vfullscreen_Reset(int cardID) { if (this.fullscreenScene.Game is Game) this.fullscreenScene.Game.Reset(cardID); }
        public void Vfullscreen_StartAudio(int cardID) { if (this.fullscreenScene.Game is Game) this.fullscreenScene.Game.StartAudio(cardID); }
        public void Vfullscreen_ToIn(int cardID) { if (this.fullscreenScene.Game is Game) this.fullscreenScene.Game.ToIn(cardID); }
        public void Vfullscreen_SetSelected(int cardID) { if (this.fullscreenScene.Game is Game) this.fullscreenScene.Game.SetSelected(cardID); }
        public void Vfullscreen_ToSelected(int cardID) { if (this.fullscreenScene.Game is Game) this.fullscreenScene.Game.ToSelected(cardID); }
        public void Vfullscreen_SetOut(int cardID) { if (this.fullscreenScene.Game is Game) this.fullscreenScene.Game.SetOut(cardID); }
        public void Vfullscreen_ToOut(int cardID) { if (this.fullscreenScene.Game is Game) this.fullscreenScene.Game.ToOut(cardID); }
        public void Vfullscreen_EnableTouch(int cardID) { if (this.fullscreenScene.Game is Game) this.fullscreenScene.Game.EnableTouch(cardID); }
        public void Vfullscreen_DisableTouch(int cardID) { if (this.fullscreenScene.Game is Game) this.fullscreenScene.Game.DisableTouch(cardID); }
        public override void Vfullscreen_UnloadScene() { this.fullscreenScene.Unload(); }

        public void Vgameboard_Start() { this.gameboardClient.Start(this.GameboardClientHostname); }
        public void Vgameboard_Init() {
            this.Vgameboard_SetContent();
            this.Vgameboard_Reset();
        }
        public void Vgameboard_SetContent() {
            if (this.ventuzGameboardScene is Game) {
                Game game = ventuzGameboardScene;
                foreach (SongDataset item in this.datalist) {
                    game.SetAudiofile(item.FirstCardID, item.Filename);
                    game.SetAudiofile(item.SecondCardID, item.Filename);
                }
            }
        }
        public void Vgameboard_Reset() { if (this.ventuzGameboardScene is Game) this.ventuzGameboardScene.Reset(); }
        public void Vgameboard_Next() { if (this.ventuzGameboardScene is Game) this.ventuzGameboardScene.Next(); }
        public void Vgameboard_EnableTouch() { if (this.ventuzGameboardScene is Game) this.ventuzGameboardScene.EnableTouch(); }
        public void Vgameboard_DisableTouch() { if (this.ventuzGameboardScene is Game) this.ventuzGameboardScene.DisableTouch(); }
        public void Vgameboard_Reset(int cardID) { if (this.ventuzGameboardScene is Game) this.ventuzGameboardScene.Reset(cardID); }
        public void Vgameboard_StartAudio(int cardID) { if (this.ventuzGameboardScene is Game) this.ventuzGameboardScene.StartAudio(cardID); }
        public void Vgameboard_ToIn(int cardID) { if (this.ventuzGameboardScene is Game) this.ventuzGameboardScene.ToIn(cardID); }
        public void Vgameboard_SetSelected(int cardID) { if (this.ventuzGameboardScene is Game) this.ventuzGameboardScene.SetSelected(cardID); }
        public void Vgameboard_ToSelected(int cardID) { if (this.ventuzGameboardScene is Game) this.ventuzGameboardScene.ToSelected(cardID); }
        public void Vgameboard_SetOut(int cardID) { if (this.ventuzGameboardScene is Game) this.ventuzGameboardScene.SetOut(cardID); }
        public void Vgameboard_ToOut(int cardID) { if (this.ventuzGameboardScene is Game) this.ventuzGameboardScene.ToOut(cardID); }
        public void Vgameboard_EnableTouch(int cardID) { if (this.ventuzGameboardScene is Game) this.ventuzGameboardScene.EnableTouch(cardID); }
        public void Vgameboard_DisableTouch(int cardID) { if (this.ventuzGameboardScene is Game) this.ventuzGameboardScene.DisableTouch(cardID); }
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
                    this.Vfullscreen_SetContent();
                    this.Vgameboard_SetContent();
                }
            }
        }

        void localVentuzHandler_Error(object sender, System.IO.ErrorEventArgs e) {
            Exception exc = e.GetException();
            this.on_Error(sender, new Messaging.ErrorEventArgs(this, sender.ToString(), DateTime.Now, exc.ToString()));
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
            if (content is int) this.Vfullscreen_ToSelected((int)content);
        }

        #endregion

    }
}
