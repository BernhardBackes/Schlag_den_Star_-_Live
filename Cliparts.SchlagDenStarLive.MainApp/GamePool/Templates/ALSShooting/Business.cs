using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Cliparts.ALSShooting;

using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.ALSShooting;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.ALSShooting {

    public class TargetNumbers {
        public List<int> Numbers { get; private set; }
        private string numbersText = string.Empty;
        public TargetNumbers() { this.Numbers = new List<int>(); }
        public String Get() { return this.numbersText; }
        public bool SetNew(string value) {
            List<int> newNumbers;
            string newText = this.buildText(value, out newNumbers);
            if (this.numbersText.Equals(newText)) return false;
            else {
                this.numbersText = newText;
                this.Numbers = newNumbers;
                return true;
            }
        }
        private string buildText(
            string value,
            out List<int> numbers) {
            numbers = new List<int>();
            if (string.IsNullOrEmpty(value)) return string.Empty;
            else {
                string result = string.Empty;
                string[] valueArray = value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                int number;
                foreach (string item in valueArray) {
                    if (int.TryParse(item.Trim(), out number)) {
                        if (result.Length > 0) result += "; ";
                        result += number.ToString();
                        numbers.Add(number);
                    }
                }
                return result;
            }
        }
    }

    public class Business : _Base.ShootingScore.Business {

        #region Properties

        internal Cliparts.ALSShooting.Controls.Client alsClient;

        private string alsHost = string.Empty;
        public string ALSHost {
            get { return this.alsHost; }
            set {
                if (this.alsHost != value) {
                    if (string.IsNullOrEmpty(value)) this.alsHost = string.Empty;
                    else this.alsHost = value;
                    this.on_PropertyChanged();
                }
            }
        }

        public ConnectionStates ALSStatus { get { return this.alsClient.Status; } }

        private TargetNumbers leftPlayerTargets = new TargetNumbers();
        public string LeftPlayerTargets {
            get { return this.leftPlayerTargets.Get(); }
            set {
                if (this.leftPlayerTargets.SetNew(value)) this.on_PropertyChanged();
            }
        }

        private TargetNumbers rightPlayerTargets = new TargetNumbers();
        public string RightPlayerTargets {
            get { return this.rightPlayerTargets.Get(); }
            set {
                if (this.rightPlayerTargets.SetNew(value)) this.on_PropertyChanged();
            }
        }

        private int hitsMaxCount = 3;

        private int heatsMaxCount {
            get {
                int result = 2;
                switch (this.ShootingStyle) {
                    case VentuzScenes.GamePool._Modules.Shooting.Styles.TwoHeats:
                        result = 2;
                        break;
                    case VentuzScenes.GamePool._Modules.Shooting.Styles.ThreeHeats:
                        result = 3;
                        break;
                    case VentuzScenes.GamePool._Modules.Shooting.Styles.FourHeats:
                        result = 4;
                        break;
                    case VentuzScenes.GamePool._Modules.Shooting.Styles.FiveHeats:
                        result = 5;
                        break;
                    default:
                        result = 2;
                        break;
                }
                return result;
            }
        }

        private bool swapTracks = false;
        [XmlIgnore]
        public bool SwapTracks {
            get { return this.swapTracks; }
            set {
                if (this.swapTracks != value) {
                    this.swapTracks = value;
                    this.on_PropertyChanged();
                }
            }
        }        

        private bool lockTargets = false;
        [XmlIgnore]
        public bool LockTargets {
            get { return this.lockTargets; }
            set {
                if (this.lockTargets != value) {
                    this.lockTargets = value;
                    this.on_PropertyChanged();
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

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.ALSShooting'", typeIdentifier);
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

            this.alsClient = new Cliparts.ALSShooting.Controls.Client(syncContext);
            this.alsClient.ExeptionRaised += this.AlsClient_ExeptionRaised;
            this.alsClient.HeatConfirmationPacketReceived += this.AlsClient_HeatConfirmationPacketReceived;
            this.alsClient.TargetActionPacketReceived += this.AlsClient_TargetActionPacketReceived;
            this.alsClient.StatusChanged += this.AlsClient_StatusChanged;

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();

            this.alsClient.ExeptionRaised -= this.AlsClient_ExeptionRaised;
            this.alsClient.Dispose();

            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
        }

        public override void ResetData() {
            base.ResetData();
            this.SwapTracks = false;
            this.LockTargets = false;
        }

        public override void Next() {
            base.Next();
            this.LockTargets = false;
        }

        internal void DisconnectALS() { this.alsClient.CloseConnection(); }

        internal void ConnectALS() { this.alsClient.Connect(this.ALSHost); }

        private void parseTAPPacket(
            Cliparts.ALSShooting.Models.TAP_Telegram e) {
            if (!this.LockTargets) {
                PlayerSelection shotPlayer = PlayerSelection.NotSelected;
                if (this.SwapTracks) {
                    if (this.leftPlayerTargets.Numbers.Contains(e.TargetNumber)) shotPlayer = PlayerSelection.RightPlayer;
                    else if (this.rightPlayerTargets.Numbers.Contains(e.TargetNumber)) shotPlayer = PlayerSelection.LeftPlayer;
                }
                else {
                    if (this.leftPlayerTargets.Numbers.Contains(e.TargetNumber)) shotPlayer = PlayerSelection.LeftPlayer;
                    else if (this.rightPlayerTargets.Numbers.Contains(e.TargetNumber)) shotPlayer = PlayerSelection.RightPlayer;
                }
                if (shotPlayer != PlayerSelection.NotSelected) {
                    if (shotPlayer == PlayerSelection.LeftPlayer) this.Vinsert_ShootingLeftPlayerHitsIn();
                    else if (shotPlayer == PlayerSelection.RightPlayer) this.Vinsert_ShootingRightPlayerHitsIn();
                    if (e.ShotHit) {
                        if (shotPlayer == PlayerSelection.LeftPlayer) {
                            this.LeftPlayerHits = e.HeatShotHit;
                            if (this.LeftPlayerHits == hitsMaxCount) this.LeftPlayerHeats++;
                        }
                        else if (shotPlayer == PlayerSelection.RightPlayer) {
                            this.RightPlayerHits = e.HeatShotHit;
                            if (this.RightPlayerHits == hitsMaxCount) this.RightPlayerHeats++;
                        }
                        //if (this.LeftPlayerHeats == heatsMaxCount ||
                        //    this.RightPlayerHeats == heatsMaxCount) this.LockTargets = true;
                        this.Vinsert_SetShooting();
                    }
                    else {
                        if (shotPlayer == PlayerSelection.LeftPlayer) this.Vinsert_ShootingLeftPlayerHitMiss();
                        else if (shotPlayer == PlayerSelection.RightPlayer) this.Vinsert_ShootingRightPlayerHitMiss();
                    }
                }
            }
        }

        private void parseHCPPacket(
            Cliparts.ALSShooting.Models.HCP_Telegram e) {
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }
        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }
        public override void Vinsert_ShootingIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ShootingIn(this.insertScene.Shooting); }
        public override void Vinsert_SetShooting() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetShooting(this.insertScene.Shooting); }
        public override void Vinsert_ShootingLeftPlayerHitsIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ShootingLeftPlayerHitsIn(this.insertScene.Shooting); }
        public override void Vinsert_ShootingLeftPlayerHitsOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ShootingLeftPlayerHitsOut(this.insertScene.Shooting); }
        public override void Vinsert_ShootingLeftPlayerHitMiss() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ShootingLeftPlayerHitMiss(this.insertScene.Shooting); }
        public override void Vinsert_ShootingRightPlayerHitsIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ShootingRightPlayerHitsIn(this.insertScene.Shooting); }
        public override void Vinsert_ShootingRightPlayerHitsOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ShootingRightPlayerHitsOut(this.insertScene.Shooting); }
        public override void Vinsert_ShootingRightPlayerHitMiss() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ShootingRightPlayerHitMiss(this.insertScene.Shooting); }
        public override void Vinsert_ShootingOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ShootingOut(this.insertScene.Shooting); }
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

        private void AlsClient_ExeptionRaised(object sender, Exception e) { this.on_Error(sender, e.Message); }

        private void AlsClient_TargetActionPacketReceived(object sender, Cliparts.ALSShooting.Models.TAP_Telegram e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_AlsClient_TargetActionPacketReceived);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_AlsClient_TargetActionPacketReceived(object content) {
            Cliparts.ALSShooting.Models.TAP_Telegram e = content as Cliparts.ALSShooting.Models.TAP_Telegram;
            if (e is Cliparts.ALSShooting.Models.TAP_Telegram) this.parseTAPPacket(e); 
        }

        private void AlsClient_HeatConfirmationPacketReceived(object sender, Cliparts.ALSShooting.Models.HCP_Telegram e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_AlsClient_HeatConfirmationPacketReceived);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_AlsClient_HeatConfirmationPacketReceived(object content) {
            Cliparts.ALSShooting.Models.HCP_Telegram e = content as Cliparts.ALSShooting.Models.HCP_Telegram;
            if (e is Cliparts.ALSShooting.Models.HCP_Telegram) this.parseHCPPacket(e); 
        }

        private void AlsClient_StatusChanged(object sender, EventArgs e) { this.on_PropertyChanged("ALSStatus"); }
         
        #endregion

    }
}
