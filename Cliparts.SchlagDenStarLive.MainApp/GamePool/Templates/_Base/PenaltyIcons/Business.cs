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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.PenaltyIcons {

    public class SingleDot : INotifyPropertyChanged {

        #region Properties

        private VentuzScenes.GamePool._Modules.PenaltyIcons.DotStates status = VentuzScenes.GamePool._Modules.PenaltyIcons.DotStates.Off;
        public VentuzScenes.GamePool._Modules.PenaltyIcons.DotStates Status {
            get { return this.status; }
            set {
                if (this.status != value) {
                    this.status = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public SingleDot() { }
        public SingleDot(VentuzScenes.GamePool._Modules.PenaltyIcons.DotStates status) { this.Status = status; }

        public void Reset() {
            this.Status = VentuzScenes.GamePool._Modules.PenaltyIcons.DotStates.Off;
        }

        public void Clone(
            SingleDot set) {
            if (set is SingleDot) {
                this.Status = set.Status;
            }
            else this.Reset();
        }

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

    public class Business : _Base.Business {

        #region Properties

        private int penaltyPositionX = 0;
        public int PenaltyPositionX {
            get { return this.penaltyPositionX; }
            set {
                if (this.penaltyPositionX != value) {
                    this.penaltyPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetPenalty();
                }
            }
        }

        private int penaltyPositionY = 0;
        public int PenaltyPositionY {
            get { return this.penaltyPositionY; }
            set {
                if (this.penaltyPositionY != value) {
                    this.penaltyPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetPenalty();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.PenaltyIcons.Styles penaltyStyle = VentuzScenes.GamePool._Modules.PenaltyIcons.Styles.Football_3x3;
        public VentuzScenes.GamePool._Modules.PenaltyIcons.Styles PenaltyStyle {
            get { return this.penaltyStyle; }
            set {
                if (this.penaltyStyle != value) {
                    this.penaltyStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetPenalty();
                }
            }
        }

        public int PenaltyDotsCount {
            get {
                switch (this.PenaltyStyle) {
                    case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.PenaltyIcons.Styles.Football_3x3:
                        return 9;
                        break;
                    case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.PenaltyIcons.Styles.Football_3x4:
                    default:
                        return 12;
                        break;
                }
            }
        }

        public const int PenaltyCountMax = 12;

        protected List<SingleDot> leftPlayerPenaltyDots = new List<SingleDot>();
        public SingleDot[] LeftPlayerPenaltyDots {
            get {
                this.fillPenaltyDots();
                return this.leftPlayerPenaltyDots.ToArray(); 
            }
            set {
                this.fillPenaltyDots();
                for (int i = 0; i < PenaltyCountMax; i++) {
                    if (value is SingleDot[] &&
                        value.Length > i) this.leftPlayerPenaltyDots[i].Clone(value[i]);
                    else this.leftPlayerPenaltyDots[i].Reset();
                }
                this.calcPenaltySums();
            }
        }

        protected int leftPlayerPenaltySum = 0;
        public int LeftPlayerPenaltySum {
            get { return this.leftPlayerPenaltySum; }
            private set {
                if (this.leftPlayerPenaltySum != value) {
                    if (value < 0) value = 0;
                    this.leftPlayerPenaltySum = value;
                    this.on_PropertyChanged();
                }
            }
        }

        protected List<SingleDot> rightPlayerPenaltyDots = new List<SingleDot>();
        public SingleDot[] RightPlayerPenaltyDots {
            get {
                this.fillPenaltyDots();
                return this.rightPlayerPenaltyDots.ToArray();
            }
            set {
                this.fillPenaltyDots();
                for (int i = 0; i < PenaltyCountMax; i++) {
                    if (value is SingleDot[] &&
                        value.Length > i) this.rightPlayerPenaltyDots[i].Clone(value[i]);
                    else this.rightPlayerPenaltyDots[i].Reset();
                }
                this.calcPenaltySums();
            }
        }

        protected int rightPlayerPenaltySum = 0;
        public int RightPlayerPenaltySum {
            get { return this.rightPlayerPenaltySum; }
            private set {
                if (this.rightPlayerPenaltySum != value) {
                    if (value < 0) value = 0;
                    this.rightPlayerPenaltySum = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool repressCalcPenaltySums = false;

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {
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
        }

        public override void Dispose() {
            base.Dispose();
            this.clearPenalty();
        }

        public override void ResetData() {
            base.ResetData();
            this.ResetPenaltyDots();
        }

        protected void fillPenaltyDots() {
            while (this.leftPlayerPenaltyDots.Count < PenaltyCountMax) {
                SingleDot item = new SingleDot();
                item.PropertyChanged += this.dot_PropertyChanged;
                this.leftPlayerPenaltyDots.Add(item);
            }
            while (this.rightPlayerPenaltyDots.Count < PenaltyCountMax) {
                SingleDot item = new SingleDot();
                item.PropertyChanged += this.dot_PropertyChanged;
                this.rightPlayerPenaltyDots.Add(item);
            }
        }

        public void ResetPenaltyDots() {
            foreach (SingleDot item in this.leftPlayerPenaltyDots) item.Reset();
            foreach (SingleDot item in this.rightPlayerPenaltyDots) item.Reset();
        }

        protected void clearPenalty() {
            foreach (SingleDot item in this.leftPlayerPenaltyDots) item.PropertyChanged -= this.dot_PropertyChanged;
            foreach (SingleDot item in this.rightPlayerPenaltyDots) item.PropertyChanged -= this.dot_PropertyChanged;
        }

        protected void calcPenaltySums() {
            this.repressCalcPenaltySums = true;
            int index = 0;
            int penaltySum = 0;
            foreach (SingleDot item in this.leftPlayerPenaltyDots) {
                if (index < this.PenaltyDotsCount &&
                    item.Status == VentuzScenes.GamePool._Modules.PenaltyIcons.DotStates.Green) penaltySum++;
                index++;
            }
            this.LeftPlayerPenaltySum = penaltySum;
            index = 0;
            penaltySum = 0;
            foreach (SingleDot item in this.rightPlayerPenaltyDots) {
                if (index < this.PenaltyDotsCount &&
                    item.Status == VentuzScenes.GamePool._Modules.PenaltyIcons.DotStates.Green) penaltySum++;
                index++;
            }
            this.RightPlayerPenaltySum = penaltySum;
            this.repressCalcPenaltySums = false;
        }

        public virtual void Vinsert_PenaltyIn() { }
        public void Vinsert_PenaltyIn(VentuzScenes.GamePool._Modules.PenaltyIcons scene) {
            this.Vinsert_SetPenalty(scene);
            if (scene is VentuzScenes.GamePool._Modules.PenaltyIcons) scene.ToIn();
        }
        public virtual void Vinsert_SetPenalty() { }
        public void Vinsert_SetPenalty(VentuzScenes.GamePool._Modules.PenaltyIcons scene) { this.Vinsert_SetPenalty(scene, this.LeftPlayerPenaltyDots, this.RightPlayerPenaltyDots); }
        public void Vinsert_SetPenalty(
            VentuzScenes.GamePool._Modules.PenaltyIcons scene,
            SingleDot[] leftPlayerPenaltyDots,
            SingleDot[] rightPlayerPenaltyDots) {
            if (scene is VentuzScenes.GamePool._Modules.PenaltyIcons) {
                scene.SetPositionX(this.PenaltyPositionX);
                scene.SetPositionY(this.PenaltyPositionY);
                scene.SetStyle(this.PenaltyStyle);
                scene.SetTopName(this.LeftPlayerName);
                scene.SetBottomName(this.RightPlayerName);
                int id = 1;
                foreach (SingleDot dot in leftPlayerPenaltyDots) {
                    scene.SetTopDot(id, dot.Status);
                    id++;
                }
                id = 1;
                foreach (SingleDot dot in rightPlayerPenaltyDots) {
                    scene.SetBottomDot(id, dot.Status);
                    id++;
                }
            }
        }

        public virtual void Vinsert_PenaltyOut() { }
        public void Vinsert_PenaltyOut(VentuzScenes.GamePool._Modules.PenaltyIcons scene) { if (scene is VentuzScenes.GamePool._Modules.PenaltyIcons) scene.ToOut(); }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming

        void dot_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_dot_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_dot_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "Status" &&
                    !this.repressCalcPenaltySums) this.calcPenaltySums();
            }
        }

        #endregion
    }
}
