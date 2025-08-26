using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.NumericInputTimerCounterScore
{
    public class Game : _Modules._InsertBase
    {
        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Top.Name
        //	[Path]=.Top.Estimated
        //	[Path]=.Top.Delivered
        //	[Path]=.Top.Score
        //	[Path]=.Bottom.Name
        //	[Path]=.Bottom.Estimated
        //	[Path]=.Bottom.Delivered
        //	[Path]=.Bottom.Score

        #region Properties

        private const string sceneID = "project/gamepool/numericinputtimercounterscore/game";

        #endregion

        #region Funktionen

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID)
        {
        }

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID)
        {
        }

        public void SetTopName(string value) { this.SetDataItemValue(".Top.Name", value); }
        public void SetTopEstimated(int value) { this.SetDataItemValue(".Top.Estimated", value); }
        public void SetTopDelivered(int value) { this.SetDataItemValue(".Top.Delivered", value); }
        public void SetTopScore(int value) { this.SetDataItemValue(".Top.Score", value); }

        public void SetBottomName(string value) { this.SetDataItemValue(".Bottom.Name", value); }
        public void SetBottomEstimated(int value) { this.SetDataItemValue(".Bottom.Estimated", value); }
        public void SetBottomDelivered(int value) { this.SetDataItemValue(".Bottom.Delivered", value); }
        public void SetBottomScore(int value) { this.SetDataItemValue(".Bottom.Score", value); }

        public override void Dispose()
        {
            base.Dispose();
            this.Reset();
        }

        public override void Clear()
        {
            base.Clear();
            this.ToOut();
        }

        #endregion

        #region Events.Outgoing

        public event EventHandler StopFired;
        private void on_StopFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.StopFired, e); }

        public event EventHandler PreciseTimeReceived;
        private void on_PreciseTimeReceived(object sender, EventArgs e) { Helper.raiseEvent(sender, this.PreciseTimeReceived, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }
}
