using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.NumericInputTimerCounterScore
{
    public class List : _Modules._InsertBase
    {
        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y

        //	[Path]=.Reset
        //	[Path]=.Position.X
        //	[Path]=.Position.Y
        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.Item_01.Hide.Invoke
        //	[Path]=.Item_01.Show.Invoke
        //	[Path]=.Item_02.Hide.Invoke
        //	...
        //	[Path]=.Item_24.Show.Invoke
        //	[Path]=.HideAll.Invoke
        //	[Path]=.ShowAll.Invoke

        #region Properties

        private const string sceneID = "project/gamepool/numericinputtimercounterscore/list";

        #endregion

        #region Funktionen

        public List(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID)
        {
        }

        public List(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID)
        {
        }

        public void ShowAllItems() { this.SetDataItemTrigger(".ShowAll.Invoke"); }
        public void HideAllItems() { this.SetDataItemTrigger(".HideAll.Invoke"); }
        public void ShowItem(int id) { this.SetDataItemTrigger($".Item_{id.ToString("00")}.Show.Invoke"); }
        public void HideItem(int id) { this.SetDataItemTrigger($".Item_{id.ToString("00")}.Hide.Invoke"); }

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
