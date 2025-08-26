using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.PercentageDivision {

    //	[Path]=.MainFader.Reset
    //	[Path]=.MainFader.SetOut
    //	[Path]=.MainFader.ToOut
    //	[Path]=.MainFader.SetIn
    //	[Path]=.MainFader.ToIn
    //	[Path]=.Values.A
    //	[Path]=.Values.Reset
    //	[Path]=.Values.ToIn
    //	[Path]=.Values.CountUp
    //	[Path]=.Values.ToOut
    //	[Path]=.Picture.Filename
    //	[Path]=.CountDown.Start
    //	[Path]=.CountDown.Stop

    public class Fullscreen : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/percentagedivision/fullscreen";

        public _Modules.TextInsert TextInsert;


        #endregion


        #region Funktionen

        public Fullscreen(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Fullscreen(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
            this.TextInsert = new _Modules.TextInsert(syncContext, this.addPort("TextInsertLayer"));
        }

        public void Reset() { this.SetDataItemTrigger(".MainFader.Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".MainFader.SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".MainFader.ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".MainFader.SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".MainFader.ToIn"); }

        public void StartCountDown() { this.SetDataItemTrigger(".CountDown.Start"); }
        public void StopCountDown() { this.SetDataItemTrigger(".CountDown.Stop"); }

        public void ResetValues() { this.SetDataItemTrigger(".Values.Reset"); }
        public void ValuesToOut() { this.SetDataItemTrigger(".Values.ToOut"); }
        public void ValuesToIn() { this.SetDataItemTrigger(".Values.ToIn"); }
        public void CountValuesUp() { this.SetDataItemTrigger(".Values.CountUp"); }

        public void SetPictureFilename(string value) { this.SetDataItemValue(".Picture.Filename", value); }

        public void SetValueA(int value) { this.SetDataItemValue(".Values.A", value); }

        public override void Dispose() {
            base.Dispose();
            this.TextInsert.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.TextInsert.Clear();
            this.ToOut();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
