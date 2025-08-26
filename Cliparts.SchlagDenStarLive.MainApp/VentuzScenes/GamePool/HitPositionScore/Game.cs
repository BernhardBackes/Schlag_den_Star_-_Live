using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.HitPositionScore {

    //	[Path]=.Reset
    //	[Path]=.ToOut
    //	[Path]=.ToIn
    //	[Path]=.SetIn
    //	[Path]=.Filename
    //	[Path]=.ResetZoom
    //	[Path]=.Marker.Red.Show
    //	[Path]=.Marker.Red.Hide
    //	[Path]=.Marker.Red.Play
    //	[Path]=.Marker.Red.SetPosition.X
    //	[Path]=.Marker.Red.SetPosition.Y
    //	[Path]=.Marker.Red.ZoomIn
    //	[Path]=.Marker.Red.Buzzer
    //	[Path]=.Marker.Blue.Show
    //	[Path]=.Marker.Blue.Hide
    //	[Path]=.Marker.Blue.Play
    //	[Path]=.Marker.Blue.SetPosition.X
    //	[Path]=.Marker.Blue.SetPosition.Y
    //	[Path]=.Marker.Blue.ZoomIn
    //	[Path]=.Marker.Blue.Buzzer

    public class Game : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/hitpositionscore/game";

        #endregion


        #region Funktionen

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID, Modes.Static) {
        }

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void ContentToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetContentIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ContentToIn() { this.SetDataItemTrigger(".ToIn"); }
        public void ResetZoom() { this.SetDataItemTrigger(".ResetZoom"); }

        public void SetFilename(string value) { this.SetDataItemValue(".Filename", value); }

        public void SetRedMarkerPositionX(int value) { this.SetDataItemTrigger(".Marker.Red.SetPosition.X", value); }
        public void SetRedMarkerPositionY(int value) { this.SetDataItemTrigger(".Marker.Red.SetPosition.Y", value); }
        public void ShowRedMarker() { this.SetDataItemTrigger(".Marker.Red.Show"); }
        public void HideRedMarker() { this.SetDataItemTrigger(".Marker.Red.Hide"); }
        public void PlayRedMarker() { this.SetDataItemTrigger(".Marker.Red.Play"); }
        public void ZoomInRedMarker() { this.SetDataItemTrigger(".Marker.Red.ZoomIn"); }
        public void BuzzerRedMarker() { this.SetDataItemTrigger(".Marker.Red.Buzzer"); }

        public void SetBlueMarkerPositionX(int value) { this.SetDataItemTrigger(".Marker.Blue.SetPosition.X", value); }
        public void SetBlueMarkerPositionY(int value) { this.SetDataItemTrigger(".Marker.Blue.SetPosition.Y", value); }
        public void ShowBlueMarker() { this.SetDataItemTrigger(".Marker.Blue.Show"); }
        public void HideBlueMarker() { this.SetDataItemTrigger(".Marker.Blue.Hide"); }
        public void PlayBlueMarker() { this.SetDataItemTrigger(".Marker.Blue.Play"); }
        public void ZoomInBlueMarker() { this.SetDataItemTrigger(".Marker.Blue.ZoomIn"); }
        public void BuzzerBlueMarker() { this.SetDataItemTrigger(".Marker.Blue.Buzzer"); }

        public override void Dispose() {
            base.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.Reset();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
