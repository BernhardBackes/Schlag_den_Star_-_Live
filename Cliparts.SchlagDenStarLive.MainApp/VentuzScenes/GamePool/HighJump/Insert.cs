using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.HighJump {

    public class Insert : _Base {

        //	[Path]=.Position.X
        //	[Path]=.Position.Y
        //	[Path]=.Reset
        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.TopName
        //	[Path]=.BottomName
        //	[Path]=.Markers.Count
        //	[Path]=.Markers.Marker_01.Height
        //	[Path]=.Markers.Marker_01.TopFaults
        //	[Path]=.Markers.Marker_01.TopPassed
        //	[Path]=.Markers.Marker_01.BottomFaults
        //	[Path]=.Markers.Marker_01.BottomPassed
        //	[Path]=.Markers.Marker_02.Height
        //	...
        //	[Path]=.Markers.Marker_14.BottomFaults

        #region Properties     
   
        private const string sceneID = "project/gamepool/highjump/insert";

        public const int MarkersCountMax = 14;

        #endregion


        #region Funktionen

        public Insert(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Insert(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
        }


        public void SetPositionX(int value) { this.SetDataItemValue(".Position.X", value); }
        public void SetPositionY(int value) { this.SetDataItemValue(".Position.Y", value); }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void SetTopName(string value) { this.SetDataItemValue(".TopName", value); }
        public void SetBottomName(string value) { this.SetDataItemValue(".BottomName", value); }

        public void SetMarkersCount(int value) { this.SetDataItemValue(".Markers.Count", value); }

        public void SetMarkersHeight(
            int id,
            double value) {
            string name = string.Format(".Markers.Marker_{0}.Height", id.ToString("00"));
            this.SetDataItemValue(name, value);
        }
        public void SetMarkersTopFaults(
            int id,
            int value) {
            string name = string.Format(".Markers.Marker_{0}.TopFaults", id.ToString("00"));
            this.SetDataItemValue(name, value);
        }
        public void SetMarkersTopPassed(
            int id,
            bool value) {
            string name = string.Format(".Markers.Marker_{0}.TopPassed", id.ToString("00"));
            this.SetDataItemValue(name, value);
        }
        public void SetMarkersBottomFaults(
            int id,
            int value) {
            string name = string.Format(".Markers.Marker_{0}.BottomFaults", id.ToString("00"));
            this.SetDataItemValue(name, value);
        }
        public void SetMarkersBottomPassed(
            int id,
            bool value) {
            string name = string.Format(".Markers.Marker_{0}.BottomPassed", id.ToString("00"));
            this.SetDataItemValue(name, value);
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
        #endregion

        #region Events.Incoming
        #endregion

    }
}
