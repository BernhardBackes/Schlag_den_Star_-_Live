using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class TwoTimersPassiv : _InsertBase {

        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        //	[Path]=.LeftTimer
        //	[Path]=.RightTimer

        #region Properties     
   
        private const string sceneID = "project/gamepool/_modules/twotimerspassiv";

        #endregion


        #region Funktionen

        public TwoTimersPassiv(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public TwoTimersPassiv(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode = Modes.Static,
            string scene = sceneID)
            : base(syncContext, port, scene, mode) {
        }

        public TwoTimersPassiv(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetLeftTimer(int value) { this.SetDataItemValue(".LeftTimer", value); }
        public void SetRightTimer(int value) { this.SetDataItemValue(".RightTimer", value); }

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

        public event EventHandler Alarm1Fired;
        private void on_Alarm1Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.Alarm1Fired, e); }

        public event EventHandler Alarm2Fired;
        private void on_Alarm2Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.Alarm2Fired, e); }

        public event EventHandler StopFired;
        private void on_StopFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.StopFired, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }
}
