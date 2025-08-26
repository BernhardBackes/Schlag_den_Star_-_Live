using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TeamCorrelation
{

    public class Player : _Base {

        //	[Path]=.Reset
        //	[Path]=.ToOut
        //	[Path]=.ToIn
        //	[Path]=.SetIn
        //	[Path]=.Lock
        //	[Path]=.Unlock
        //	[Path]=.OKPressed
        //	[Path]=.Result
        //	[Path]=.Headline
        //	[Path]=.Tasks.Task1.Value
        //	[Path]=.Tasks.Task2.Value
        //	[Path]=.Tasks.Task3.Value
        //	[Path]=.Tasks.Task4.Value

        #region Properties

        private string input = string.Empty;
        public string Input
        {
            get { return this.input; }
            private set
            {
                if (this.input != value)
                {
                    if (string.IsNullOrEmpty(value)) this.input = string.Empty;
                    else this.input = (value);
                    this.on_PropertyChanged();
                }
            }
        }

        private const string sceneID = "project/gamepool/teamcorrelation/player";

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

        public void Lock() { this.SetDataItemTrigger(".Lock"); }
        public void Unlock() { this.SetDataItemTrigger(".Unlock"); }

        public void SetHeadline(string value) { this.SetDataItemValue(".Headline", value); }

        public void SetTask(
            int id,
            string value)
        {
            this.SetDataItemValue($".Tasks.Task{id.ToString()}.Value", value);
        }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e)
        {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs)
            {
                if (e.Path == ".OKPressed")
                {
                    if (e.Value == null) this.Input = string.Empty;
                    else this.Input = e.Value.ToString();
                    this.on_OKButtonPressed(this, new EventArgs());
                }
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e)
        {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs)
            {
                if (e.Path == ".Result")
                {
                    object o = this.GetDataItemValue(".Result");
                    if (o == null) this.Input = string.Empty;
                    else this.Input = o.ToString();
                }
            }
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
