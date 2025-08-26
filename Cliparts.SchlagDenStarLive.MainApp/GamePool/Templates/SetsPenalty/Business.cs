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

using Cliparts.Tools.Base;

using Cliparts.IOnet.IOUnit.IONbase;
using Cliparts.IOnet.IOUnit.IONbuz;

using Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.Sets;
using Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.Penalty;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SetsPenalty;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.SetsPenalty {

    public class Business : _Base.Business {

        #region Properties

        private Content.Gameboard.PlayerSelection selectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
        public Content.Gameboard.PlayerSelection SelectedPlayer {
            get { return this.selectedPlayer; }
            set {
                if (this.selectedPlayer != value) {
                    if (value == Content.Gameboard.PlayerSelection.NotSelected) value = Content.Gameboard.PlayerSelection.LeftPlayer;
                    this.selectedPlayer = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int selectedSet = 0;
        public int SelectedSet {
            get { return this.selectedSet; }
            set {
                if (this.selectedSet != value) {
                    if (value < 0) this.SelectedSet = 0;
                    else if (value >= this.SetsCount) this.SelectedSet = this.SetsCount - 1;
                    else this.selectedSet = value;
                    this.on_PropertyChanged();
                }
            }
        }


        private int setsPositionX = 0;
        public int SetsPositionX {
            get { return this.setsPositionX; }
            set {
                if (this.setsPositionX != value) {
                    this.setsPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetSets();
                }
            }
        }

        private int setsPositionY = 0;
        public int SetsPositionY {
            get { return this.setsPositionY; }
            set {
                if (this.setsPositionY != value) {
                    this.setsPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetSets();
                }
            }
        }

        private Insert.Styles setsStyle = Insert.Styles.ThreeSets;
        public Insert.Styles SetsStyle {
            get { return this.setsStyle; }
            set {
                if (this.setsStyle != value) {
                    this.setsStyle = value;
                    this.on_PropertyChanged();
                    this.on_PropertyChanged("SetsCount");
                    this.Vinsert_SetSets();
                    this.calcSetSums();
                    this.SelectedSet = this.SelectedSet;
                }
            }
        }

        public int SetsCount {
            get {
                int count = 0;
                switch (this.SetsStyle) {
                    case Insert.Styles.ThreeSets:
                        count = 3;
                        break;
                    case Insert.Styles.FourSets:
                        count = 4;
                        break;
                    case Insert.Styles.FiveSets:
                        count = 5;
                        break;
                    case Insert.Styles.SixSets:
                        count = 6;
                        break;
                    case Insert.Styles.SevenSets:
                        count = 7;
                        break;
                    case Insert.Styles.EightSets:
                        count = 8;
                        break;
                    case Insert.Styles.NineSets:
                        count = 9;
                        break;
                    case Insert.Styles.TenSets:
                        count = 10;
                        break;
                    case Insert.Styles.TwoSets:
                    default:
                        count = 2;
                        break;
                }
                return count;
            }
        }

        public const int SetsCountMax = 10;

        protected List<SingleSet> leftPlayerSets = new List<SingleSet>();
        public SingleSet[] LeftPlayerSets {
            get {
                this.fillSets();
                return this.leftPlayerSets.ToArray(); 
            }
            set {
                this.fillSets();
                for (int i = 0; i < SetsCountMax; i++) {
                    if (value is SingleSet[] &&
                        value.Length > i) this.leftPlayerSets[i].Clone(value[i]);
                    else this.leftPlayerSets[i].Reset();
                }
                this.calcSetSums();
            }
        }

        protected int leftPlayerSetSum = 0;
        public int LeftPlayerSetSum {
            get { return this.leftPlayerSetSum; }
            private set {
                if (this.leftPlayerSetSum != value) {
                    if (value < 0) value = 0;
                    this.leftPlayerSetSum = value;
                    this.on_PropertyChanged();
                }
            }
        }

        protected List<SingleSet> rightPlayerSets = new List<SingleSet>();
        public SingleSet[] RightPlayerSets {
            get {
                this.fillSets();
                return this.rightPlayerSets.ToArray();
            }
            set {
                this.fillSets();
                for (int i = 0; i < SetsCountMax; i++) {
                    if (value is SingleSet[] &&
                        value.Length > i) this.rightPlayerSets[i].Clone(value[i]);
                    else this.rightPlayerSets[i].Reset();
                }
                this.calcSetSums();
            }
        }

        protected int rightPlayerSetSum = 0;
        public int RightPlayerSetSum {
            get { return this.rightPlayerSetSum; }
            private set {
                if (this.rightPlayerSetSum != value) {
                    if (value < 0) value = 0;
                    this.rightPlayerSetSum = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool repressCalcSetSums = false;

        private Insert.PenaltyShots penaltyShots = Insert.PenaltyShots.FiveShots;
        public Insert.PenaltyShots PenaltyShots {
            get { return this.penaltyShots; }
            set {
                if (this.penaltyShots != value) {
                    this.penaltyShots = value;
                    this.on_PropertyChanged();
                    this.on_PropertyChanged("PenaltyShots");
                    this.calcPenaltySum();
                    this.Vinsert_SetPenalty();
                }
            }
        }
        public int PenaltyCount {
            get {
                int count = 0;
                switch (this.PenaltyShots) {
                    case Insert.PenaltyShots.EightShots:
                        count = 8;
                        break;
                    case Insert.PenaltyShots.TenShots:
                        count = 10;
                        break;
                    case Insert.PenaltyShots.FiveShots:
                    default:
                        count = 5;
                        break;
                }
                return count;
            }
        }
        public const int PenaltyCountMax = 10;

        protected List<SingleDot> penaltyDots = new List<SingleDot>();
        public SingleDot[] PenaltyDots {
            get {
                this.fillPenaltyDots();
                return this.penaltyDots.ToArray();
            }
            set {
                this.fillPenaltyDots();
                for (int i = 0; i < PenaltyCount; i++) {
                    if (value is SingleDot[] &&
                        value.Length > i) this.penaltyDots[i].Clone(value[i]);
                    else this.penaltyDots[i].Reset();
                }
                this.calcPenaltySum();
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.SetsPenalty'", typeIdentifier);
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

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;
        }

        public override void Dispose() {
            base.Dispose();
            this.clearSets();
            this.clearPenalty();
            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
        }

        public override void ResetData() {
            base.ResetData();
            this.SelectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
            this.SelectedSet = 0;
            foreach (SingleSet item in this.leftPlayerSets) item.Reset();
            foreach (SingleSet item in this.rightPlayerSets) item.Reset();
            this.resetPenalty();
        }

        public void ActivatePlayer() {
            switch (this.SelectedPlayer) {
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.LeftPlayer:
                    this.LeftPlayerSets[this.SelectedSet].Status = VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.RightPlayer:
                    this.RightPlayerSets[this.SelectedSet].Status = VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid;
                    break;
            }
            this.Vinsert_SetSets();
        }

        public override void Next() {
            base.Next();
            bool nextSet = false;
            switch (this.SelectedPlayer) {
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.LeftPlayer:
                    this.SelectedPlayer = Content.Gameboard.PlayerSelection.RightPlayer;
                    nextSet = this.RightPlayerSets[this.SelectedSet].Status == VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.RightPlayer:
                    this.SelectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
                    nextSet = this.LeftPlayerSets[this.SelectedSet].Status == VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid;
                    break;
            }
            if (!nextSet) this.resetPenalty();
            else if (this.SelectedSet < this.SetsCount - 1) {
                this.SelectedSet++;
                this.resetPenalty();
            }
        }

        protected void fillSets() {
            while (this.leftPlayerSets.Count < SetsCountMax) {
                SingleSet item = new SingleSet();
                item.PropertyChanged += this.set_PropertyChanged;
                this.leftPlayerSets.Add(item);
            }
            while (this.rightPlayerSets.Count < SetsCountMax) {
                SingleSet item = new SingleSet();
                item.PropertyChanged += this.set_PropertyChanged;
                this.rightPlayerSets.Add(item);
            }
        }

        protected void clearSets() {
            foreach (SingleSet item in this.leftPlayerSets) item.PropertyChanged -= this.set_PropertyChanged;
            foreach (SingleSet item in this.rightPlayerSets) item.PropertyChanged -= this.set_PropertyChanged;
        }

        protected void calcSetSums() {
            this.repressCalcSetSums = true;
            int index = 0;
            int setSum = 0;
            foreach (SingleSet item in this.leftPlayerSets) {
                if (index < this.SetsCount) setSum += item.ValidValue;
                index++;
            }
            this.LeftPlayerSetSum = setSum;
            index = 0;
            setSum = 0;
            foreach (SingleSet item in this.rightPlayerSets) {
                if (index < this.SetsCount) setSum += item.ValidValue;
                index++;
            }
            this.RightPlayerSetSum = setSum;
            this.repressCalcSetSums = false;
        }

        protected void fillPenaltyDots() {
            while (this.penaltyDots.Count < PenaltyCountMax) {
                SingleDot item = new SingleDot();
                item.PropertyChanged += this.dot_PropertyChanged;
                this.penaltyDots.Add(item);
            }
        }

        protected void clearPenalty() {
            foreach (SingleDot item in this.penaltyDots) item.PropertyChanged -= this.dot_PropertyChanged;
        }

        protected void resetPenalty() {
            foreach (SingleDot item in this.penaltyDots) item.Reset();
        }

        protected void calcPenaltySum() {
            int index = 0;
            int penaltySum = 0;
            foreach (SingleDot item in this.PenaltyDots) {
                if (index < PenaltyCount &&
                    item.Status == VentuzScenes.GamePool._Modules.Penalty.DotStates.Green) penaltySum++;
                index++;
            }
            switch (this.SelectedPlayer) {
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.LeftPlayer:
                    this.LeftPlayerSets[this.SelectedSet].Value = penaltySum;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.RightPlayer:
                    this.RightPlayerSets[this.SelectedSet].Value = penaltySum;
                    break;
            }
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }

        public void Vinsert_SetsIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetsIn(this.insertScene); }
        public void Vinsert_SetsIn(
            Insert scene) {
            if (scene is Insert) {
                this.Vinsert_SetSets(scene);
                scene.ToIn();
            }
        }
        public virtual void Vinsert_SetSets() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetSets(this.insertScene); }
        public void Vinsert_SetSets(VentuzScenes.GamePool.SetsPenalty.Insert scene) {
            this.Vinsert_SetSets(scene, this.LeftPlayerSets, this.RightPlayerSets);
        }
        public void Vinsert_SetSets(
            Insert scene,
            SingleSet[] leftPlayerSets,
            SingleSet[] rightPlayerSets) {
            if (scene is Insert) {
                scene.SetPositionX(this.SetsPositionX);
                scene.SetPositionY(this.SetsPositionY);
                scene.SetStyle(this.SetsStyle);
                scene.SetTopName(this.LeftPlayerName);
                scene.SetBottomName(this.RightPlayerName);
                int id = 1;
                foreach (_Base.Sets.SingleSet set in leftPlayerSets) {
                    scene.SetTopStatus(id, set.Status);
                    scene.SetTopValue(id, set.Value);
                    id++;
                }
                id = 1;
                foreach (_Base.Sets.SingleSet set in rightPlayerSets) {
                    scene.SetBottomStatus(id, set.Status);
                    scene.SetBottomValue(id, set.Value);
                    id++;
                }
            }
        }
        public virtual void Vinsert_SetsOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetsOut(this.insertScene); }
        public void Vinsert_SetsOut(Insert scene) { if (scene is Insert) scene.ToOut(); }

        public void Vinsert_PenaltyIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_PenaltyIn(this.insertScene); }
        public void Vinsert_PenaltyIn(
            Insert scene) {
            if (scene is Insert) {
                this.Vinsert_SetPenalty(scene);
                scene.PenaltyToIn();
            }
        }
        public virtual void Vinsert_SetPenalty() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetPenalty(this.insertScene); }
        public void Vinsert_SetPenalty(
            Insert scene) {
            Insert.PenaltyPositions dotsPosition = Insert.PenaltyPositions.Top;
            if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.RightPlayer) dotsPosition = Insert.PenaltyPositions.Bottom;
            this.Vinsert_SetPenalty(scene, this.PenaltyShots, dotsPosition, this.PenaltyDots);
        }
        public void Vinsert_SetPenalty(
            Insert scene,
            Insert.PenaltyShots penaltyShots,
            Insert.PenaltyPositions dotsPosition,
            SingleDot[] penaltyDots) {
            if (scene is Insert) {
                scene.SetPenaltyShots(penaltyShots);
                scene.SetPenaltyPosition(dotsPosition);
                int id = 1;
                foreach (SingleDot dot in penaltyDots) {
                    scene.SetDot(id, dot.Status);
                    id++;
                }
            }
        }
        public virtual void Vinsert_PenaltyOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_PenaltyOut(this.insertScene); }
        public void Vinsert_PenaltyOut(Insert scene) { if (scene is Insert) scene.PenaltyToOut(); }
        
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

        void set_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_set_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_set_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "ValidValue" &&
                    !this.repressCalcSetSums) this.calcSetSums();
            }
        }

        void dot_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_dot_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_dot_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "Status") this.calcPenaltySum();
            }
        }

        #endregion

    }
}
