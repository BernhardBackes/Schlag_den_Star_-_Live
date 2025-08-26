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


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.SetLarge {

    public class Business : _Base.Business {

        #region Properties

        private int setsPositionX = 0;
        public int SetLargePositionX {
            get { return this.setsPositionX; }
            set {
                if (this.setsPositionX != value) {
                    this.setsPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetSetLarge();
                }
            }
        }

        private int setsPositionY = 0;
        public int SetLargePositionY {
            get { return this.setsPositionY; }
            set {
                if (this.setsPositionY != value) {
                    this.setsPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetSetLarge();
                }
            }
        }

        private string leftPlayerSetValue = string.Empty;
        public string LeftPlayerSetValue {
            get { return this.leftPlayerSetValue; }
            set {
                if (this.leftPlayerSetValue != value) {
                    if (string.IsNullOrEmpty(value)) this.leftPlayerSetValue = string.Empty;
                    else this.leftPlayerSetValue = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.SetLarge.ValueStates leftPlayerStatus = VentuzScenes.GamePool._Modules.SetLarge.ValueStates.Idle;
        public VentuzScenes.GamePool._Modules.SetLarge.ValueStates LeftPlayerSetStatus {
            get { return this.leftPlayerStatus; }
            set {
                if (this.leftPlayerStatus != value) {
                    this.leftPlayerStatus = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetSetLarge();
                }
            }
        }

        private string rightPlayerSetValue = string.Empty;
        public string RightPlayerSetValue {
            get { return this.rightPlayerSetValue; }
            set {
                if (this.rightPlayerSetValue != value) {
                    if (string.IsNullOrEmpty(value)) this.rightPlayerSetValue = string.Empty;
                    else this.rightPlayerSetValue = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.SetLarge.ValueStates rightPlayerSetStatus = VentuzScenes.GamePool._Modules.SetLarge.ValueStates.Idle;
        public VentuzScenes.GamePool._Modules.SetLarge.ValueStates RightPlayerSetStatus {
            get { return this.rightPlayerSetStatus; }
            set {
                if (this.rightPlayerSetStatus != value) {
                    this.rightPlayerSetStatus = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetSetLarge();
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
            this.LeftPlayerSetValue = string.Empty;
            this.LeftPlayerSetStatus = VentuzScenes.GamePool._Modules.SetLarge.ValueStates.Idle;
            this.RightPlayerSetValue = string.Empty;
            this.RightPlayerSetStatus = VentuzScenes.GamePool._Modules.SetLarge.ValueStates.Idle;
        }

        public virtual void Vinsert_SetLargeIn() { }
        public void Vinsert_SetLargeIn(
            VentuzScenes.GamePool._Modules.SetLarge scene) {
            if (scene is VentuzScenes.GamePool._Modules.SetLarge) {
                this.Vinsert_SetSetLarge(scene);
                scene.ToIn();
            }
        }
        public virtual void Vinsert_SetSetLarge() { }
        public void Vinsert_SetSetLarge(VentuzScenes.GamePool._Modules.SetLarge scene) {
            this.Vinsert_SetSetLarge(scene, this.LeftPlayerSetStatus, this.LeftPlayerSetValue, this.RightPlayerSetStatus, this.RightPlayerSetValue);
        }
        public void Vinsert_SetSetLarge(
            VentuzScenes.GamePool._Modules.SetLarge scene,
            VentuzScenes.GamePool._Modules.SetLarge.ValueStates leftPlayerSetStatus,
            string leftPlayerSetValue,
            VentuzScenes.GamePool._Modules.SetLarge.ValueStates rightPlayerSetStatus,
            string rightPlayerSetValue) {
            if (scene is VentuzScenes.GamePool._Modules.SetLarge) {
                scene.SetPositionX(this.SetLargePositionX);
                scene.SetPositionY(this.SetLargePositionY);
                scene.SetTopName(this.LeftPlayerName);
                scene.SetTopStatus(leftPlayerSetStatus);
                scene.SetTopValue(leftPlayerSetValue);
                scene.SetBottomName(this.RightPlayerName);
                scene.SetBottomStatus(rightPlayerSetStatus);
                scene.SetBottomValue(rightPlayerSetValue);
            }
        }
        public virtual void Vinsert_SetLargeOut() { }
        public void Vinsert_SetLargeOut(VentuzScenes.GamePool._Modules.SetLarge scene) { if (scene is VentuzScenes.GamePool._Modules.SetLarge) scene.ToOut(); }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion
    }
}
