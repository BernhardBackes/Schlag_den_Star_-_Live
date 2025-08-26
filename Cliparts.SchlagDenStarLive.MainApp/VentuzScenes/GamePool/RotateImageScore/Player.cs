using Cliparts.VRemote4.HandlerSi.DataItem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.RotateImageScore {

    public class Player : _Base {

        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.Country.MovieFilename
        //	[Path]=.Country.Name
        //	[Path]=.Country.Enable
        //	[Path]=.Country.Disable
        //	[Path]=.Country.Angle
        //	[Path]=.Country.SetAngle
        //	[Path]=.Country.OKPressed

        #region Properties

        private const string sceneID = "project/gamepool/rotateimagescore/player";

        private int angle = 0;
        public int Angle {
            get { return this.angle; }
            private set {
                if (this.angle != value) {
                    this.angle = value;
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

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void SetAngle(int value) { this.SetDataItemTrigger(".Country.SetAngle", value); }
        public void SetMovieFilename(string value) { this.SetDataItemValue(".Country.MovieFilename", value); }
        public void SetName(string value) { this.SetDataItemValue(".Country.Name", value); }

        public void EnableTouch() { this.SetDataItemTrigger(".Country.Enable"); }
        public void DisableTouch() { this.SetDataItemTrigger(".Country.Disable"); }

        public override void Dispose() {
            base.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.ToOut();
        }

        protected override void dataItem_TriggerReceived(object sender, ValueArgs e) {
            if (e is ValueArgs) {
                if (e.Path == ".Country.OKPressed") {
                    this.Angle = Convert.ToInt32(e.Value);
                    this.on_OKPressed(this, this.Angle);
                }
            }
        }

        protected override void dataItem_ValueChanged(object sender, ValueArgs e) {
            if (e is ValueArgs) {
                if (e.Path == "") this.Angle = Convert.ToInt32(e.Value);
            }
        }


        #endregion


        #region Events.Outgoing

        public event EventHandler<int> OKPressed;
        private void on_OKPressed(object sender, int e) { Helper.raiseEvent(sender, this.OKPressed, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }

}
