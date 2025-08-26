using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.DotsAndBoxes {

    public partial class FormBoardControl : Form {

        #region Properties

        private VentuzScenes.GamePool.DotsAndBoxes.Fullscreen fullscreenScene;
        private VRemote4.HandlerSi.Scene.States fullscreenSceneStatus {
            get {
                if (this.fullscreenScene is VentuzScenes.GamePool.DotsAndBoxes.Fullscreen) return this.fullscreenScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private VentuzScenes.GamePool.DotsAndBoxes.Player leftPlayerScene;
        private VRemote4.HandlerSi.Scene.States leftPlayerSceneStatus {
            get {
                if (this.leftPlayerScene is VentuzScenes.GamePool.DotsAndBoxes.Player) return this.leftPlayerScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private VentuzScenes.GamePool.DotsAndBoxes.Player rightPlayerScene;
        private VRemote4.HandlerSi.Scene.States rightPlayerSceneStatus {
            get {
                if (this.rightPlayerScene is VentuzScenes.GamePool.DotsAndBoxes.Player) return this.rightPlayerScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        #endregion


        #region Funktionen

        public FormBoardControl(
            VentuzScenes.GamePool.DotsAndBoxes.Fullscreen fullscreenScene,
            VentuzScenes.GamePool.DotsAndBoxes.Player leftPlayerScene,
            VentuzScenes.GamePool.DotsAndBoxes.Player rightPlayerScene) {
            InitializeComponent();
            this.fullscreenScene = fullscreenScene;
            this.leftPlayerScene = leftPlayerScene;
            this.rightPlayerScene = rightPlayerScene;
        }

        #endregion


        #region Events.Incoming
        #endregion

        #region Events.Controls

        private void button_MouseDown(object sender, MouseEventArgs e) {
            Control control = sender as Control;
            int firstIndex;
            int secondIndex;
            if (control is Control &&
                Helper.tryParseTwoIndicesFromControl(control, out firstIndex, out secondIndex)) {
                if (control.Name.StartsWith("buttonH")) {
                    switch (e.Button) {
                        case MouseButtons.Left:
                            // In
                            if (this.fullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.Game.SetHorizontalFrameIn(firstIndex, secondIndex);
                            if (this.leftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.Game.SetHorizontalFrameIn(firstIndex, secondIndex);
                            if (this.rightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.Game.SetHorizontalFrameIn(firstIndex, secondIndex);
                            break;
                        case MouseButtons.Right:
                            // Out
                            if (this.fullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.Game.SetHorizontalFrameOut(firstIndex, secondIndex);
                            if (this.leftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.Game.SetHorizontalFrameOut(firstIndex, secondIndex);
                            if (this.rightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.Game.SetHorizontalFrameOut(firstIndex, secondIndex);
                            break;
                    }
                }
                if (control.Name.StartsWith("buttonV")) {
                    switch (e.Button) {
                        case MouseButtons.Left:
                            // In
                            if (this.fullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.Game.SetVerticalFrameIn(firstIndex, secondIndex);
                            if (this.leftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.Game.SetVerticalFrameIn(firstIndex, secondIndex);
                            if (this.rightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.Game.SetVerticalFrameIn(firstIndex, secondIndex);
                            break;
                        case MouseButtons.Right:
                            // Out
                            if (this.fullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.Game.SetVerticalFrameOut(firstIndex, secondIndex);
                            if (this.leftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.Game.SetVerticalFrameOut(firstIndex, secondIndex);
                            if (this.rightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.Game.SetVerticalFrameOut(firstIndex, secondIndex);
                            break;
                    }
                }
            }
        }

        private void buttonSelectLeftPlayer_Click(object sender, EventArgs e) {
            if (this.fullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.Game.SelectLeftPlayer();
            if (this.leftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.Game.SelectLeftPlayer();
            if (this.rightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.Game.SelectLeftPlayer();
        }

        private void buttonSelectRightPlayer_Click(object sender, EventArgs e) {
            if (this.fullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.Game.SelectRightPlayer();
            if (this.leftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.Game.SelectRightPlayer();
            if (this.rightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.Game.SelectRightPlayer();
        }

        #endregion

    }
}
