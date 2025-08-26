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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.BondedDots {

    public class Business : _Base.Business {

        const int DotsBondedMax = 3;

        #region Properties

        private int bondedDotsPositionX = 0;
        public int BondedDotsPositionX {
            get { return this.bondedDotsPositionX; }
            set {
                if (this.bondedDotsPositionX != value) {
                    this.bondedDotsPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetBondedDots();
                }
            }
        }

        private int bondedDotsPositionY = 0;
        public int BondedDotsPositionY {
            get { return this.bondedDotsPositionY; }
            set {
                if (this.bondedDotsPositionY != value) {
                    this.bondedDotsPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetBondedDots();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.BondedDots.Styles setsStyle = VentuzScenes.GamePool._Modules.BondedDots.Styles.FifteenDots;
        public VentuzScenes.GamePool._Modules.BondedDots.Styles BondedDotsStyle {
            get { return this.setsStyle; }
            set {
                if (this.setsStyle != value) {
                    this.setsStyle = value;
                    this.on_PropertyChanged();
                    this.on_PropertyChanged("BondedDotsCount");
                    this.calcDotsSums();
                    this.Vinsert_SetBondedDots();
                }
            }
        }

        public int BondedDotsCount {
            get {
                int count = 0;
                switch (this.BondedDotsStyle) {
                    case VentuzScenes.GamePool._Modules.BondedDots.Styles.TenDots:
                        count = 10;
                        break;
                    case VentuzScenes.GamePool._Modules.BondedDots.Styles.ElevenDots:
                        count = 11;
                        break;
                    case VentuzScenes.GamePool._Modules.BondedDots.Styles.ThirteenDots:
                        count = 13;
                        break;
                    case VentuzScenes.GamePool._Modules.BondedDots.Styles.FifteenDots:
                    default:
                        count = 15;
                        break;
                }
                return count;
            }
        }

        protected List<int> leftPlayerDots = new List<int>();
        public int[] LeftPlayerDots {
            get { return this.leftPlayerDots.ToArray(); }
            set {
                if (value is int[]) this.leftPlayerDots = new List<int>(value);
                else this.leftPlayerDots.Clear();
                this.calcDotsSums();
                this.on_PropertyChanged();
                this.Vinsert_SetBondedDots();
            }
        }

        protected List<int> rightPlayerDots = new List<int>();
        public int[] RightPlayerDots {
            get { return this.rightPlayerDots.ToArray(); }
            set {
                if (value is int[]) this.rightPlayerDots = new List<int>(value);
                else this.rightPlayerDots.Clear();
                this.calcDotsSums();
                this.on_PropertyChanged();
                this.Vinsert_SetBondedDots();
            }
        }

        protected int leftPlayerDotsSum = 0;
        public int LeftPlayerDotsSum {
            get { return this.leftPlayerDotsSum; }
            private set {
                if (this.leftPlayerDotsSum != value) {
                    if (value < 0) this.leftPlayerDotsSum = 0;
                    else this.leftPlayerDotsSum = value;
                    this.on_PropertyChanged();
                }
            }
        }

        protected int rightPlayerDotsSum = 0;
        public int RightPlayerDotsSum {
            get { return this.rightPlayerDotsSum; }
            private set {
                if (this.rightPlayerDotsSum != value) {
                    if (value < 0) this.rightPlayerDotsSum = 0;
                    else this.rightPlayerDotsSum = value;
                    this.on_PropertyChanged();
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
        }

        public override void ResetData() {
            base.ResetData();
            this.LeftPlayerDots = null;
            this.RightPlayerDots = null;
        }

        public bool TryAddDotsToTop(
            int value) {
            if (value > 0 &&
                value <= DotsBondedMax &&
                this.LeftPlayerDotsSum + value <= this.BondedDotsCount) {
                this.leftPlayerDots.Add(value);
                this.calcDotsSums();
                this.on_PropertyChanged("LeftPlayerDots");
                this.Vinsert_AddBondedDotsToTop(this.leftPlayerDots.Count - 1, value);
                return true;
            }
            else return false;
        }
        public bool TryRemoveDotsFromTop(
            int index) {
            if (index >= 0 &&
                index < this.leftPlayerDots.Count) {
                this.leftPlayerDots.RemoveAt(index);
                this.calcDotsSums();
                this.on_PropertyChanged("LeftPlayerDots");
                this.Vinsert_SetBondedDots();
                return true;
            }
            else return false;
        }

        public bool TryAddDotsToBottom(
            int value) {
            if (value > 0 &&
                value <= DotsBondedMax &&
                this.RightPlayerDotsSum + value <= this.BondedDotsCount) {
                this.rightPlayerDots.Add(value);
                this.calcDotsSums();
                this.on_PropertyChanged("RightPlayerDots");
                this.Vinsert_AddBondedDotsToBottom(this.rightPlayerDots.Count - 1, value);
                return true;
            }
            else return false;
        }
        public bool TryRemoveDotsFromBottom(
            int index) {
            if (index >= 0 &&
                index < this.rightPlayerDots.Count) {
                this.rightPlayerDots.RemoveAt(index);
                this.calcDotsSums();
                this.on_PropertyChanged("RightPlayerDots");
                this.Vinsert_SetBondedDots();
                return true;
            }
            else return false;
        }

        protected void calcDotsSums() {
            int dotsSum = 0;
            bool changed = false;
            foreach (int item in this.LeftPlayerDots) dotsSum += item;
            while (dotsSum > this.BondedDotsCount) {
                dotsSum -= this.leftPlayerDots.Last();
                this.leftPlayerDots.RemoveAt(this.leftPlayerDots.Count - 1);
                changed = true;
            }
            this.LeftPlayerDotsSum = dotsSum;
            if (changed) this.on_PropertyChanged("LeftPlayerDots");

            dotsSum = 0;
            changed = false;
            foreach (int item in this.RightPlayerDots) dotsSum += item;
            while (dotsSum > this.BondedDotsCount) {
                dotsSum -= this.rightPlayerDots.Last();
                this.rightPlayerDots.RemoveAt(this.rightPlayerDots.Count - 1);
                changed = true;
            }
            this.RightPlayerDotsSum = dotsSum;
            if (changed) this.on_PropertyChanged("RightPlayerDots");
        }

        public virtual void Vinsert_BondedDotsIn() { }
        public void Vinsert_BondedDotsIn(
            VentuzScenes.GamePool._Modules.BondedDots scene) {
            if (scene is VentuzScenes.GamePool._Modules.BondedDots) {
                this.Vinsert_SetBondedDots(scene);
                scene.ToIn();
            }
        }
        public virtual void Vinsert_SetBondedDots() { }
        public void Vinsert_SetBondedDots(VentuzScenes.GamePool._Modules.BondedDots scene) {this.Vinsert_SetBondedDots(scene, this.LeftPlayerDots, this.RightPlayerDots); }
        public void Vinsert_SetBondedDots(
            VentuzScenes.GamePool._Modules.BondedDots scene,
            int[] leftPlayerDots,
            int[] rightPlayerDots) {
            if (scene is VentuzScenes.GamePool._Modules.BondedDots) {
                scene.SetPositionX(this.BondedDotsPositionX);
                scene.SetPositionY(this.BondedDotsPositionY);
                scene.SetStyle(this.BondedDotsStyle);
                scene.SetTopName(this.LeftPlayerName);
                scene.SetBottomName(this.RightPlayerName);
                scene.ResetTopPointer();
                scene.ResetBottomPointer();
                for (int i = 0; i < this.BondedDotsCount; i++) {
                    if (leftPlayerDots.Length > i) {
                        scene.SetTopValue(leftPlayerDots[i]);
                        scene.SetTopDotIn(i);
                    }
                    else {
                        scene.ResetTopDot(i);
                        scene.SetTopValue(0);
                    }
                    if (rightPlayerDots.Length > i) {
                        scene.SetBottomValue(rightPlayerDots[i]);
                        scene.SetBottomDotIn(i);
                    }
                    else {
                        scene.ResetBottomDot(i);
                        scene.SetBottomValue(0);
                    }
                }
            }
        }
        public virtual void Vinsert_AddBondedDotsToTop(int index, int value) { }
        public void Vinsert_AddBondedDotsToTop(
            VentuzScenes.GamePool._Modules.BondedDots scene,
            int index,
            int value) {
            if (scene is VentuzScenes.GamePool._Modules.BondedDots) {
                scene.SetTopPointer(index);
                scene.SetTopValue(value);
                scene.TopDotToIn(index);
            }
        }
        public virtual void Vinsert_AddBondedDotsToBottom(int index, int value) { }
        public void Vinsert_AddBondedDotsToBottom(
            VentuzScenes.GamePool._Modules.BondedDots scene,
            int index,
            int value) {
            if (scene is VentuzScenes.GamePool._Modules.BondedDots) {
                scene.SetBottomPointer(index);
                scene.SetBottomValue(value);
                scene.BottomDotToIn(index);
            }
        }
        public virtual void Vinsert_BondedDotsOut() { }
        public void Vinsert_BondedDotsOut(
            VentuzScenes.GamePool._Modules.BondedDots scene) {
            if (scene is VentuzScenes.GamePool._Modules.BondedDots) scene.ToOut();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion
    }
}
