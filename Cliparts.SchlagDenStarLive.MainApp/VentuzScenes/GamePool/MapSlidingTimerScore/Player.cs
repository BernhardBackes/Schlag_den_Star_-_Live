using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MapSlidingTimerScore {

    public class Player : _Base {

        //	[Path]=.Reset
        //	[Path]=.ToOut
        //	[Path]=.ToIn
        //	[Path]=.SetIn
        //	[Path]=.Headline
        //	[Path]=.Navigation.URL
        //	[Path]=.Navigation.Load
        //	[Path]=.Navigation.Scaling
        //	[Path]=.Input.Lock
        //	[Path]=.Input.Release
        //	[Path]=.Marker.Lock
        //	[Path]=.Marker.Release

        public enum TypeItems { Numerary, Time }

        #region Properties

        private const string sceneID = "project/gamepool/mapslidingtimerscore/player";

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

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void SetText(string value) { this.SetDataItemValue(".Headline", value); }
        public void SetURL(string value) { this.SetDataItemValue(".Navigation.URL", value); }
        public void SetScaling(UInt16 value) { this.SetDataItemValue(".Navigation.Scaling", value); }

        public void LoadURL() { this.SetDataItemTrigger(".Navigation.Load"); }
        public void LockInput() { this.SetDataItemTrigger(".Input.Lock"); }
        public void ReleaseInput() { this.SetDataItemTrigger(".Input.Release"); }
        public void LockMarker() { this.SetDataItemTrigger(".Marker.Lock"); }
        public void ReleaseMarker() { this.SetDataItemTrigger(".Marker.Release"); }

        public override void Dispose() {
            base.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.ToOut();
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
