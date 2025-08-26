using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.DotsAndBoxes {

    //	[Path]=.ResetAll
    //	[Path]=.AutoLockEnabled
    //	[Path]=.Player.LeftPlayer.Select
    //	[Path]=.Player.LeftPlayer.Score
    //	[Path]=.Player.LeftPlayer.ScoreChanged
    //	[Path]=.Player.RightPlayer.Select
    //	[Path]=.Player.RightPlayer.Score
    //	[Path]=.Player.RightPlayer.ScoreChanged
    //	[Path]=.Touch.Enable
    //	[Path]=.Touch.Disable
    //	[Path]=.Touch.Enabled
    //	[Path]=.Touch.EnabledChanged
    //	[Path]=.Frames.AllOut
    //	[Path]=.Frames.AllIn
    //	[Path]=.Frames.BoxAdded
    //	[Path]=.Frames.Horizontal.H_1_1.Frame.Selected
    //	[Path]=.Frames.Horizontal.H_1_1.Frame.Reset
    //	[Path]=.Frames.Horizontal.H_1_1.Frame.In
    //	[Path]=.Frames.Horizontal.H_1_1.Frame.Out
    //  ...
    //	[Path]=.Frames.Horizontal.H_1_5.Frame.Selected
    //  ...
    //	[Path]=.Frames.Horizontal.H_2_1.Frame.Selected
    //  ...
    //	[Path]=.Frames.Horizontal.H_2_1.Frame.Reset
    //  ...
    //	[Path]=.Frames.Horizontal.H_3_1.Frame.Selected
    //  ...
    //	[Path]=.Frames.Horizontal.H_4_1.Frame.Selected
    //  ...
    //	[Path]=.Frames.Horizontal.H_5_1.Frame.Reset
    //  ...
    //	[Path]=.Frames.Horizontal.H_5_1.Frame.In
    //  ...
    //	[Path]=.Frames.Horizontal.H_6_1.Frame.Selected
    //  ...
    //	[Path]=.Frames.Vertical.V_1_1.Frame.Selected
    //	[Path]=.Frames.Vertical.V_1_1.Frame.Reset
    //	[Path]=.Frames.Vertical.V_1_1.Frame.In
    //	[Path]=.Frames.Vertical.V_1_1.Frame.Out
    //  ...
    //	[Path]=.Frames.Vertical.V_1_6.Frame.Selected
    //  ...
    //	[Path]=.Frames.Vertical.V_1_6.Frame.Reset
    //  ...
    //	[Path]=.Frames.Vertical.V_2_1.Frame.Selected
    //  ...
    //	[Path]=.Frames.Vertical.V_3_1.Frame.Selected
    //  ...
    //	[Path]=.Frames.Vertical.V_4_1.Frame.Selected
    //  ...
    //	[Path]=.Frames.Vertical.V_5_1.Frame.Selected
    //  ...

    public class Game : VentuzScenes.GamePool._Modules._InsertBase {

        #region Properties

        private const string sceneID = "project/gamepool/dotsandboxes/game";

        private int? leftPlayerCounter = null;
        public int LeftPlayerCounter {
            get {
                if (this.leftPlayerCounter.HasValue) return this.leftPlayerCounter.Value;
                else return 0;
            }
            private set {
                if (!this.leftPlayerCounter.HasValue ||
                    this.leftPlayerCounter != value) {
                    this.leftPlayerCounter = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? rightPlayerCounter = null;
        public int RightPlayerCounter {
            get {
                if (this.rightPlayerCounter.HasValue) return this.rightPlayerCounter.Value;
                else return 0;
            }
            private set {
                if (!this.rightPlayerCounter.HasValue ||
                    this.rightPlayerCounter != value) {
                    this.rightPlayerCounter = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool? touchEnabled = null;
        public bool TouchEnabled {
            get {
                if (this.touchEnabled.HasValue) return this.touchEnabled.Value;
                else return false;
            }
            private set {
                if (!this.touchEnabled.HasValue ||
                    this.touchEnabled.Value != value) {
                    this.touchEnabled = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string lastSelectedFrame = null;
        public string LastSelectedFrame {
            get {
                if (string.IsNullOrEmpty(this.lastSelectedFrame)) return string.Empty;
                else return lastSelectedFrame;
            }
            private set {
                if (string.IsNullOrEmpty(this.lastSelectedFrame) ||
                    this.lastSelectedFrame != value) {
                    this.lastSelectedFrame = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetAutoLock(bool enabled) { this.SetDataItemValue(".AutoLockEnabled", enabled); }
        public void SetTouch(bool enabled) {
            if (enabled) {
                this.LastSelectedFrame = string.Empty;
                this.SetDataItemTrigger(".Touch.Enable");
            }
            else this.SetDataItemTrigger(".Touch.Disable");
        }

        public void Reset() { 
            this.SetDataItemTrigger(".ResetAll");
            this.LastSelectedFrame = string.Empty;
        }
        public void SelectLeftPlayer() { this.SetDataItemTrigger(".Player.LeftPlayer.Select"); }
        public void SelectRightPlayer() { this.SetDataItemTrigger(".Player.RightPlayer.Select"); }

        public void SetAllFramesOut() { this.SetDataItemTrigger(".Frames.AllOut"); }
        public void SetAllFramesIn() { this.SetDataItemTrigger(".Frames.AllIn"); }

        public void ResetHorizontalFrame(
            int xPosition,
            int yPosition) {
            string name = string.Format(".Frames.Horizontal.H_{0}_{1}.Frame.Reset", xPosition.ToString(), yPosition.ToString());
            this.SetDataItemTrigger(name);
        }
        public void SetHorizontalFrameIn(
            int xPosition,
            int yPosition) {
            string name = string.Format(".Frames.Horizontal.H_{0}_{1}.Frame.In", xPosition.ToString(), yPosition.ToString());
            this.SetDataItemTrigger(name);
        }
        public void SetHorizontalFrameOut(
            int xPosition,
            int yPosition) {
            string name = string.Format(".Frames.Horizontal.H_{0}_{1}.Frame.Out", xPosition.ToString(), yPosition.ToString());
            this.SetDataItemTrigger(name);
        }

        public void ResetVerticalFrame(
            int xPosition,
            int yPosition) {
            string name = string.Format(".Frames.Vertical.V_{0}_{1}.Frame.Reset", xPosition.ToString(), yPosition.ToString());
            this.SetDataItemTrigger(name);
        }
        public void SetVerticalFrameIn(
            int xPosition,
            int yPosition) {
            string name = string.Format(".Frames.Vertical.V_{0}_{1}.Frame.In", xPosition.ToString(), yPosition.ToString());
            this.SetDataItemTrigger(name);
        }
        public void SetVerticalFrameOut(
            int xPosition,
            int yPosition) {
            string name = string.Format(".Frames.Vertical.V_{0}_{1}.Frame.Out", xPosition.ToString(), yPosition.ToString());
            this.SetDataItemTrigger(name);
        }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Frames.BoxAdded") this.on_BoxAdded(this, new EventArgs());
                else if (e.Path.EndsWith(".Selected")) {
                    string[] nameArray = e.Path.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                    if (nameArray.Length >= 3) this.LastSelectedFrame = nameArray[2];
                }
                //else if (e.Path == ".Player.LeftPlayer.ScoreChanged") this.LeftPlayerCounter = Convert.ToInt32(e.Value);
                //else if (e.Path == ".Player.RightPlayer.ScoreChanged") this.RightPlayerCounter = Convert.ToInt32(e.Value);
                else if (e.Path == ".Touch.EnabledChanged") this.TouchEnabled = Convert.ToBoolean(e.Value);
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs &&
                e.Value != null) {
                if (e.Path == ".AutoLockEnabled") { }
                //else if (e.Path == ".Player.LeftPlayer.Score") this.LeftPlayerCounter = Convert.ToInt32(e.Value);
                //else if (e.Path == ".Player.RightPlayer.Score") this.RightPlayerCounter = Convert.ToInt32(e.Value);
                else if (e.Path == ".Touch.Enabled") this.TouchEnabled = Convert.ToBoolean(e.Value);
            }
        }

        public override void Dispose() {
            base.Dispose();
            this.Reset();
        }

        public override void Clear() {
            base.Clear();
            this.ToOut();
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler BoxAdded;
        private void on_BoxAdded(object sender, EventArgs e) { Helper.raiseEvent(sender, this.BoxAdded, e); }

        //public event EventHandler FrameSelected;
        //private void on_FrameSelected(object sender, EventArgs e) { Helper.RaiseEvent(sender, this.FrameSelected, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }

}
