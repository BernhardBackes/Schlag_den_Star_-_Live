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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.LevelsChecked {

    public class SingleDot : INotifyPropertyChanged {

        #region Properties

        private bool isChecked = false;
        public bool IsChecked {
            get { return this.isChecked; }
            set {
                if (this.isChecked != value) {
                    this.isChecked = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public SingleDot() { }
        public SingleDot(bool isChecked) { this.IsChecked = isChecked; }

        public void Reset() {
            this.IsChecked = false;
        }

        public void Clone(
            SingleDot set) {
            if (set is SingleDot) {
                this.IsChecked = set.IsChecked;
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
        public int LevelsCheckedPositionX {
            get { return this.penaltyPositionX; }
            set {
                if (this.penaltyPositionX != value) {
                    this.penaltyPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetLevelsChecked();
                }
            }
        }

        private int penaltyPositionY = 0;
        public int LevelsCheckedPositionY {
            get { return this.penaltyPositionY; }
            set {
                if (this.penaltyPositionY != value) {
                    this.penaltyPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetLevelsChecked();
                }
            }
        }

        public const int LevelsCheckedCountMax = 10;

        protected List<SingleDot> leftPlayerLevelsCheckedDots = new List<SingleDot>();
        public SingleDot[] LeftPlayerLevelsCheckedDots {
            get {
                this.fillLevelsCheckedDots();
                return this.leftPlayerLevelsCheckedDots.ToArray(); 
            }
            set {
                this.fillLevelsCheckedDots();
                for (int i = 0; i < LevelsCheckedCountMax; i++) {
                    if (value is SingleDot[] &&
                        value.Length > i) this.leftPlayerLevelsCheckedDots[i].Clone(value[i]);
                    else this.leftPlayerLevelsCheckedDots[i].Reset();
                }
            }
        }

        protected List<SingleDot> rightPlayerLevelsCheckedDots = new List<SingleDot>();
        public SingleDot[] RightPlayerLevelsCheckedDots {
            get {
                this.fillLevelsCheckedDots();
                return this.rightPlayerLevelsCheckedDots.ToArray();
            }
            set {
                this.fillLevelsCheckedDots();
                for (int i = 0; i < LevelsCheckedCountMax; i++) {
                    if (value is SingleDot[] &&
                        value.Length > i) this.rightPlayerLevelsCheckedDots[i].Clone(value[i]);
                    else this.rightPlayerLevelsCheckedDots[i].Reset();
                }
            }
        }

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
            this.clearLevelsChecked();
        }

        public override void ResetData() {
            base.ResetData();
            this.ResetLevelsCheckedDots();
        }

        protected void fillLevelsCheckedDots() {
            while (this.leftPlayerLevelsCheckedDots.Count < LevelsCheckedCountMax) {
                SingleDot item = new SingleDot();
                item.PropertyChanged += this.dot_PropertyChanged;
                this.leftPlayerLevelsCheckedDots.Add(item);
            }
            while (this.rightPlayerLevelsCheckedDots.Count < LevelsCheckedCountMax) {
                SingleDot item = new SingleDot();
                item.PropertyChanged += this.dot_PropertyChanged;
                this.rightPlayerLevelsCheckedDots.Add(item);
            }
        }

        public void ResetLevelsCheckedDots() {
            foreach (SingleDot item in this.leftPlayerLevelsCheckedDots) item.Reset();
            foreach (SingleDot item in this.rightPlayerLevelsCheckedDots) item.Reset();
        }

        protected void clearLevelsChecked() {
            foreach (SingleDot item in this.leftPlayerLevelsCheckedDots) item.PropertyChanged -= this.dot_PropertyChanged;
            foreach (SingleDot item in this.rightPlayerLevelsCheckedDots) item.PropertyChanged -= this.dot_PropertyChanged;
        }

        public virtual void Vinsert_LevelsCheckedIn() { }
        public void Vinsert_LevelsCheckedIn(VentuzScenes.GamePool._Modules.LevelsChecked scene) {
            this.Vinsert_SetLevelsChecked(scene);
            if (scene is VentuzScenes.GamePool._Modules.LevelsChecked) scene.ToIn();
        }
        public virtual void Vinsert_SetLevelsChecked() { }
        public void Vinsert_SetLevelsChecked(VentuzScenes.GamePool._Modules.LevelsChecked scene) { this.Vinsert_SetLevelsChecked(scene, this.LeftPlayerLevelsCheckedDots, this.RightPlayerLevelsCheckedDots); }
        public void Vinsert_SetLevelsChecked(
            VentuzScenes.GamePool._Modules.LevelsChecked scene,
            SingleDot[] leftPlayerLevelsCheckedDots,
            SingleDot[] rightPlayerLevelsCheckedDots) {
            if (scene is VentuzScenes.GamePool._Modules.LevelsChecked) {
                scene.SetPositionX(this.LevelsCheckedPositionX);
                scene.SetPositionY(this.LevelsCheckedPositionY);
                scene.SetTopName(this.LeftPlayerName);
                scene.SetBottomName(this.RightPlayerName);
                int id = 1;
                foreach (SingleDot dot in leftPlayerLevelsCheckedDots) {
                    scene.SetTopDot(id, !dot.IsChecked);
                    id++;
                }
                id = 1;
                foreach (SingleDot dot in rightPlayerLevelsCheckedDots) {
                    scene.SetBottomDot(id, !dot.IsChecked);
                    id++;
                }
            }
        }
        public virtual void Vinsert_LevelsCheckedOut() { }
        public void Vinsert_LevelsCheckedOut(VentuzScenes.GamePool._Modules.LevelsChecked scene) { if (scene is VentuzScenes.GamePool._Modules.LevelsChecked) scene.ToOut(); }

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
            }
        }

        #endregion
    }
}
