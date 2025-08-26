using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.Geography {

    public class Stage : _Base {

        //	[Path]=.Reset
        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.Map.Reset
        //	[Path]=.Map.Sample
        //	[Path]=.Map.SelectedPlayer
        //	[Path]=.Map.FullText
        //	[Path]=.Map.Touch.Lock
        //	[Path]=.Map.Touch.Unlock
        //	[Path]=.Map.Touch.OKPressed
        //	[Path]=.Map.Touch.Position.X
        //	[Path]=.Map.Touch.Position.Y
        //	[Path]=.Map.Solution.Hide
        //	[Path]=.Map.Solution.Show
        //	[Path]=.Map.Solution.Name
        //	[Path]=.Map.Solution.Position.X
        //	[Path]=.Map.Solution.Position.Y
        //	[Path]=.Map.Results.Reset
        //	[Path]=.Map.Results.Show
        //	[Path]=.Map.Results.Blue.Name
        //	[Path]=.Map.Results.Blue.Distance
        //	[Path]=.Map.Results.Blue.Position.X
        //	[Path]=.Map.Results.Blue.Position.Y
        //	[Path]=.Map.Results.Blue.HidePin
        //	[Path]=.Map.Results.Blue.ResetDistance
        //	[Path]=.Map.Results.Blue.ShowPin
        //	[Path]=.Map.Results.Blue.ShowDistance
        //	[Path]=.Map.Results.Red.Name
        //	[Path]=.Map.Results.Red.Distance
        //	[Path]=.Map.Results.Red.Position.X
        //	[Path]=.Map.Results.Red.Position.Y
        //	[Path]=.Map.Results.Red.HidePin
        //	[Path]=.Map.Results.Red.ResetDistance
        //	[Path]=.Map.Results.Red.ShowPin
        //	[Path]=.Map.Results.Red.ShowDistance

        public enum PlayerSelection { NotSelected, LeftPlayer, RightPlayer }

        #region Properties

        private const string sceneID = "project/gamepool/geography/stage";

        private int? touchPositionX = null;
        public int TouchPositionX {
            get {
                if (this.touchPositionX.HasValue) return this.touchPositionX.Value;
                else return 0;
            }
            private set {
                if (!this.touchPositionX.HasValue ||
                    this.touchPositionX != value) {
                    this.touchPositionX = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? touchPositionY = null;
        public int TouchPositionY {
            get {
                if (this.touchPositionY.HasValue) return this.touchPositionY.Value;
                else return 0;
            }
            private set {
                if (!this.touchPositionY.HasValue ||
                    this.touchPositionY != value) {
                    this.touchPositionY = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public Stage(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Stage(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
        }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetMapLayout(Fullscreen.MapLayoutElements value) { this.SetDataItemValue(".Map.Layout", value); }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void ResetMap() { 
            this.SetDataItemTrigger(".Map.Reset");
            this.touchPositionX = null;
            this.touchPositionY = null;
        }

        public void SetSampleMap(bool value) { this.SetDataItemValue(".Map.Sample", value); }

        public void SelectPlayer(PlayerSelection value) { this.SetDataItemValue(".Map.SelectedPlayer", value); }

        public void SetFullText(string value) { this.SetDataItemValue(".Map.FullText", value); }

        public void LockTouch() { this.SetDataItemTrigger(".Map.Touch.Lock"); }
        public void UnlockTouch() { 
            this.SetDataItemTrigger(".Map.Touch.Unlock");
            this.touchPositionX = null;
            this.touchPositionY = null;
        }

        public void SetSolutionName(string value) { this.SetDataItemValue(".Map.Solution.Name", value); }
        public void SetSolutionPosition(
            double x,
            double y) {
            this.SetDataItemValue(".Map.Solution.Position.X", x);
            this.SetDataItemValue(".Map.Solution.Position.Y", y);
        }
        public void HideSolution() { this.SetDataItemTrigger(".Map.Solution.Hide"); }
        public void ShowSolution() { this.SetDataItemTrigger(".Map.Solution.Show"); }

        public void SetBlueName(string value) { this.SetDataItemValue(".Map.Results.Blue.Name", value); }
        public void SetBlueDistance(int value) { this.SetDataItemValue(".Map.Results.Blue.Distance", value); }
        public void SetBluePosition(
            double x,
            double y) {
            this.SetDataItemValue(".Map.Results.Blue.Position.X", x);
            this.SetDataItemValue(".Map.Results.Blue.Position.Y", y);
        }
        public void HideBluePin() { this.SetDataItemTrigger(".Map.Results.Blue.HidePin"); }
        public void ShowBluePin() { this.SetDataItemTrigger(".Map.Results.Blue.ShowPin"); }
        public void ResetBlueDistance() { this.SetDataItemTrigger(".Map.Results.Blue.ResetDistance"); }
        public void ShowBlueDistance() { this.SetDataItemTrigger(".Map.Results.Blue.ShowDistance"); }

        public void SetRedName(string value) { this.SetDataItemValue(".Map.Results.Red.Name", value); }
        public void SetRedDistance(int value) { this.SetDataItemValue(".Map.Results.Red.Distance", value); }
        public void SetRedPosition(
            double x,
            double y) {
            this.SetDataItemValue(".Map.Results.Red.Position.X", x);
            this.SetDataItemValue(".Map.Results.Red.Position.Y", y);
        }
        public void HideRedPin() { this.SetDataItemTrigger(".Map.Results.Red.HidePin"); }
        public void ShowRedPin() { this.SetDataItemTrigger(".Map.Results.Red.ShowPin"); }
        public void ResetRedDistance() { this.SetDataItemTrigger(".Map.Results.Red.ResetDistance"); }
        public void ShowRedDistance() { this.SetDataItemTrigger(".Map.Results.Red.ShowDistance"); }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Map.Touch.Position.X") this.TouchPositionX = Convert.ToInt32(e.Value);
                else if (e.Path == ".Map.Touch.Position.Y") this.TouchPositionY = Convert.ToInt32(e.Value);
                else if (e.Path == ".Map.Touch.Position.XChanged") this.TouchPositionX = Convert.ToInt32(e.Value);
                else if (e.Path == ".Map.Touch.Position.YChanged") this.TouchPositionY = Convert.ToInt32(e.Value);
                else if (e.Path == ".Map.Touch.OKPressed") {
                    this.on_OKButtonPressed(this, new EventArgs());
                }
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs &&
                e.Value != null) {
                if (e.Path == ".Map.Touch.Position.X") this.TouchPositionX = Convert.ToInt32(e.Value);
                else if (e.Path == ".Map.Touch.Position.Y") this.TouchPositionY = Convert.ToInt32(e.Value);
                else if (e.Path == ".Map.Touch.Position.XChanged") this.TouchPositionX = Convert.ToInt32(e.Value);
                else if (e.Path == ".Map.Touch.Position.YChanged") this.TouchPositionY = Convert.ToInt32(e.Value);
            }
        }

        public override void Dispose() {
            base.Dispose();
        }

        public override void Clear() {
            base.Clear();
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler OKButtonPressed;
        private void on_OKButtonPressed(object sender, EventArgs e) { Helper.raiseEvent(sender, this.OKButtonPressed, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }

}
