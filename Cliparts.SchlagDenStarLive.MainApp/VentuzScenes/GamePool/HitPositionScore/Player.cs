using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.HitPositionScore {

    public class Player : _Base {

        //	[Path]=.Reset
        //	[Path]=.Player
        //	[Path]=.Content.ToOut
        //	[Path]=.Content.ToIn
        //	[Path]=.Content.SetIn
        //	[Path]=.Content.Filename
        //	[Path]=.Marker.Show
        //	[Path]=.Marker.Hide
        //	[Path]=.Marker.Play
        //	[Path]=.Marker.Position.X
        //	[Path]=.Marker.Position.Y
        //	[Path]=.Marker.PositionChanged.X
        //	[Path]=.Marker.PositionChanged.Y
        //	[Path]=.Touch.Disable
        //	[Path]=.Touch.Enable
        //	[Path]=.Touch.Enabled
        //	[Path]=.OK.Disable
        //	[Path]=.OK.Enable
        //	[Path]=.OK.Enabled
        //	[Path]=.OK.Pressed

        public enum PlayerValues { Red, Blue }

        #region Properties

        private const string sceneID = "project/gamepool/hitpositionscore/player";

        private Point selection = new Point();
        public Point Selection {
            get { return this.selection; }
            private set {
                if (this.selection != value) {
                    this.selection = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public Player(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Player(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
        }

        public void SetPlayer(PlayerValues value) { this.SetDataItemValue(".Player", value); }

        public void Reset() { 
            this.SetDataItemTrigger(".Reset");
            this.Selection = new Point();
        }

        public void SetContentFilename(string value) { this.SetDataItemValue(".Content.Filename", value); }
        public void ContentToOut() { this.SetDataItemTrigger(".Content.ToOut"); }
        public void SetContentIn() { this.SetDataItemTrigger(".Content.SetIn"); }
        public void ContentToIn() { this.SetDataItemTrigger(".Content.ToIn"); }

        public void EnableTouch() { 
            this.SetDataItemTrigger(".Touch.Enable");
            this.Selection = new Point();
        }
        public void DisableTouch() { this.SetDataItemTrigger(".Touch.Disable"); }


        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".OK.Pressed") this.on_OKPressed(this, this.Selection);
                else if (e.Value != null) {
                    if (e.Path == ".Marker.PositionChanged.X") this.Selection = new Point(Convert.ToInt32(e.Value), this.Selection.Y);
                    else if (e.Path == ".Marker.PositionChanged.Y") this.Selection = new Point(this.Selection.X, Convert.ToInt32(e.Value));
                }
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs &&
                e.Value != null) {
                if (e.Path == ".Marker.Position.X") this.Selection = new Point(Convert.ToInt32(e.Value), this.Selection.Y);
                else if (e.Path == ".Marker.Position.Y")this.Selection = new Point(this.Selection.X, Convert.ToInt32(e.Value));
            }
        }

        public override void Dispose() {
            base.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.ContentToOut();
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler<Point> OKPressed;
        private void on_OKPressed(object sender, Point e) { Helper.raiseEvent(sender, this.OKPressed, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }
}
