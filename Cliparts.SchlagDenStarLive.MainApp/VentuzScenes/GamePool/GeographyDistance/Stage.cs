using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.GeographyDistance {

    public class Stage : _Base {

        //	[Path]=.Reset
        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.Map.Layout
        //	[Path]=.Map.Reset
        //	[Path]=.Map.Sample
        //	[Path]=.Map.SelectedPlayer
        //	[Path]=.Map.FullText
        //	[Path]=.Map.Touch.Lock
        //	[Path]=.Map.Touch.Unlock
        //	[Path]=.Map.Touch.OKPressed
        //	[Path]=.Map.Touch.Position.X
        //	[Path]=.Map.Touch.Position.XChanged
        //	[Path]=.Map.Touch.Position.Y
        //	[Path]=.Map.Touch.Position.YChanged
        //	[Path]=.Map.Solution.Hide
        //	[Path]=.Map.Solution.Show
        //	[Path]=.Map.Solution.Name
        //	[Path]=.Map.Solution.Distance
        //	[Path]=.Map.Solution.ResetDistance
        //	[Path]=.Map.Solution.ShowDistance
        //	[Path]=.Map.Solution.SetSolutionDistanceIn
        //	[Path]=.Map.Solution.Pin1.Position.X
        //	[Path]=.Map.Solution.Pin1.Position.Y
        //	[Path]=.Map.Solution.Pin2.Position.X
        //	[Path]=.Map.Solution.Pin2.Position.Y
        //	[Path]=.Map.Results.Reset
        //	[Path]=.Map.Results.Show
        //	[Path]=.Map.Results.SetIn
        //	[Path]=.Map.Results.ResetOffsets
        //	[Path]=.Map.Results.ShowOffsets
        //	[Path]=.Map.Results.SetOInffsetsIn
        //	[Path]=.Map.Results.Blue.Name
        //	[Path]=.Map.Results.Blue.Distance
        //	[Path]=.Map.Results.Blue.ResetDistance
        //	[Path]=.Map.Results.Blue.ShowDistance
        //	[Path]=.Map.Results.Blue.SetBlueDistanceIn
        //	[Path]=.Map.Results.Blue.Offset
        //	[Path]=.Map.Results.Blue.Pin1.Position.X
        //	[Path]=.Map.Results.Blue.Pin1.Position.Y
        //	[Path]=.Map.Results.Blue.Pin1.Show
        //	[Path]=.Map.Results.Blue.Pin1.Hide
        //	[Path]=.Map.Results.Blue.Pin2.Position.X
        //	[Path]=.Map.Results.Blue.Pin2.Position.Y
        //	[Path]=.Map.Results.Blue.Pin2.Show
        //	[Path]=.Map.Results.Blue.Pin2.Hide
        //	[Path]=.Map.Results.Red.Name
        //	[Path]=.Map.Results.Red.Distance
        //	[Path]=.Map.Results.Red.ResetDistance
        //	[Path]=.Map.Results.Red.ShowDistance
        //	[Path]=.Map.Results.Red.SetRedDistanceIn
        //	[Path]=.Map.Results.Red.Offset
        //	[Path]=.Map.Results.Red.Pin1.Position.X
        //	[Path]=.Map.Results.Red.Pin1.Position.Y
        //	[Path]=.Map.Results.Red.Pin1.Show
        //	[Path]=.Map.Results.Red.Pin1.Hide
        //	[Path]=.Map.Results.Red.Pin2.Position.X
        //	[Path]=.Map.Results.Red.Pin2.Position.Y
        //	[Path]=.Map.Results.Red.Pin2.Show
        //	[Path]=.Map.Results.Red.Pin2.Hide

        public enum PlayerSelection { NotSelected, LeftPlayer, RightPlayer }

        #region Properties

        private const string sceneID = "project/gamepool/geographydistance/stage";

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
        public void SetSolutionDistance(int value) { this.SetDataItemValue(".Map.Solution.Distance", value); }
        public void SetSolutionPin1Position(
            double x,
            double y)
        {
            this.SetDataItemValue(".Map.Solution.Pin1.Position.X", x);
            this.SetDataItemValue(".Map.Solution.Pin1.Position.Y", y);
        }
        public void SetSolutionPin2Position(
            double x,
            double y)
        {
            this.SetDataItemValue(".Map.Solution.Pin2.Position.X", x);
            this.SetDataItemValue(".Map.Solution.Pin2.Position.Y", y);
        }
        public void HideSolution() { this.SetDataItemTrigger(".Map.Solution.Hide"); }
        public void ShowSolution() { this.SetDataItemTrigger(".Map.Solution.Show"); }
        public void ResetSolutionDistance() { this.SetDataItemTrigger(".Map.Solution.ResetDistance"); }
        public void ShowSolutionDistance() { this.SetDataItemTrigger(".Map.Solution.ShowDistance"); }
        public void SetSolutionDistanceIn() { this.SetDataItemTrigger(".Map.Solution.SetSolutionDistanceIn"); }

        public void SetBlueName(string value) { this.SetDataItemValue(".Map.Results.Blue.Name", value); }
        public void SetBlueDistance(int value) { this.SetDataItemValue(".Map.Results.Blue.Distance", value); }
        public void SetBlueOffset(int value) { this.SetDataItemValue(".Map.Results.Blue.Offset", value); }
        public void SetBluePin1Position(
            double x,
            double y)
        {
            this.SetDataItemValue(".Map.Results.Blue.Pin1.Position.X", x);
            this.SetDataItemValue(".Map.Results.Blue.Pin1.Position.Y", y);
        }
        public void SetBluePin2Position(
            double x,
            double y)
        {
            this.SetDataItemValue(".Map.Results.Blue.Pin2.Position.X", x);
            this.SetDataItemValue(".Map.Results.Blue.Pin2.Position.Y", y);
        }
        public void HideBluePin1() { this.SetDataItemTrigger(".Map.Results.Blue.Pin1.Hide"); }
        public void ShowBluePin1() { this.SetDataItemTrigger(".Map.Results.Blue.Pin1.Show"); }
        public void HideBluePin2() { this.SetDataItemTrigger(".Map.Results.Blue.Pin2.Hide"); }
        public void ShowBluePin2() { this.SetDataItemTrigger(".Map.Results.Blue.Pin2.Show"); }
        public void ResetBlueDistance() { this.SetDataItemTrigger(".Map.Results.Blue.ResetDistance"); }
        public void ShowBlueDistance() { this.SetDataItemTrigger(".Map.Results.Blue.ShowDistance"); }
        public void SetBlueDistanceIn() { this.SetDataItemTrigger(".Map.Results.Blue.SetBlueDistanceIn"); }

        public void SetRedName(string value) { this.SetDataItemValue(".Map.Results.Red.Name", value); }
        public void SetRedDistance(int value) { this.SetDataItemValue(".Map.Results.Red.Distance", value); }
        public void SetRedOffset(int value) { this.SetDataItemValue(".Map.Results.Red.Offset", value); }
        public void SetRedPin1Position(
            double x,
            double y)
        {
            this.SetDataItemValue(".Map.Results.Red.Pin1.Position.X", x);
            this.SetDataItemValue(".Map.Results.Red.Pin1.Position.Y", y);
        }
        public void SetRedPin2Position(
            double x,
            double y)
        {
            this.SetDataItemValue(".Map.Results.Red.Pin2.Position.X", x);
            this.SetDataItemValue(".Map.Results.Red.Pin2.Position.Y", y);
        }
        public void HideRedPin1() { this.SetDataItemTrigger(".Map.Results.Red.Pin1.Hide"); }
        public void ShowRedPin1() { this.SetDataItemTrigger(".Map.Results.Red.Pin1.Show"); }
        public void HideRedPin2() { this.SetDataItemTrigger(".Map.Results.Red.Pin2.Hide"); }
        public void ShowRedPin2() { this.SetDataItemTrigger(".Map.Results.Red.Pin2.Show"); }
        public void ResetRedDistance() { this.SetDataItemTrigger(".Map.Results.Red.ResetDistance"); }
        public void ShowRedDistance() { this.SetDataItemTrigger(".Map.Results.Red.ShowDistance"); }
        public void SetRedDistanceIn() { this.SetDataItemTrigger(".Map.Results.Red.SetRedDistanceIn"); }

        public void ResetOffsets() { this.SetDataItemTrigger(".Map.Results.ResetOffsets"); }
        public void ShowOffsets() { this.SetDataItemTrigger(".Map.Results.ShowOffsets"); }
        public void SetOInffsetsIn() { this.SetDataItemTrigger(".Map.Results.SetOInffsetsIn"); }

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
