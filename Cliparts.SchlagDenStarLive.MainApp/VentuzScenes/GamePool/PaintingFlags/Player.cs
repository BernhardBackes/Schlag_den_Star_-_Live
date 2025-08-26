using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.PaintingFlags
{

    public class Player : _Base {

        //	[Path]=.Reset
        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.Task
        //	[Path]=.Orientation
        //	[Path]=.Lock
        //	[Path]=.Unlock
        //	[Path]=.OKPressed
        //	[Path]=.FlagColor1
        //	[Path]=.FlagColor2
        //	[Path]=.FlagColor3

        #region Properties

        private ColorStates flagColor1 = 0;
        public ColorStates FlagColor1
        {
            get { return this.flagColor1; }
            set
            {
                if (this.flagColor1 != value)
                {
                    this.flagColor1 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private ColorStates flagColor2 = 0;
        public ColorStates FlagColor2
        {
            get { return this.flagColor2; }
            set
            {
                if (this.flagColor2 != value)
                {
                    this.flagColor2 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private ColorStates flagColor3 = 0;
        public ColorStates FlagColor3
        {
            get { return this.flagColor3; }
            set
            {
                if (this.flagColor3 != value)
                {
                    this.flagColor3 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private const string sceneID = "project/gamepool/paintingflags/player";

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

        public void SetTask(string value) { this.SetDataItemValue(".Task", value); }
        public void SetOrientation(OrientationStates value) { this.SetDataItemValue(".Orientation", value); }

        public void Lock() { this.SetDataItemTrigger(".Lock"); }
        public void Unlock() { this.SetDataItemTrigger(".Unlock"); }

        public override void Dispose() {
            base.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.ToOut();
        }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".FlagColor1Changed" &&
                    e.Value is int) this.FlagColor1 = (ColorStates)e.Value;
                else if (e.Path == ".FlagColor2Changed" &&
                    e.Value is int) this.FlagColor2 = (ColorStates)e.Value;
                else if (e.Path == ".FlagColor3Changed" &&
                    e.Value is int) this.FlagColor3 = (ColorStates)e.Value;
                else if (e.Path == ".OKPressed") this.on_OKButtonPressed(this, new EventArgs());
            }
        }
        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs)
            {
                if (e.Path == ".FlagColor1" &&
                    e.Value is int) this.FlagColor1 = (ColorStates)e.Value;
                else if (e.Path == ".FlagColor2" &&
                    e.Value is int) this.FlagColor2 = (ColorStates)e.Value;
                else if (e.Path == ".FlagColor3" &&
                    e.Value is int) this.FlagColor3 = (ColorStates)e.Value;
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
