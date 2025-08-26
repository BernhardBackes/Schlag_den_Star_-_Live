using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.VideoMemory
{
    public class Player : VRemote4.HandlerSi.Scene, INotifyPropertyChanged
    {
        //	[Path]=.Position.X
        //	[Path]=.Position.Y
        //	[Path]=.Scaling
        //	[Path]=.MuteAudio
        //	[Path]=.ShowBorder
        //	[Path]=.Filename
        //	[Path]=.Reset
        //	[Path]=.SetIn
        //	[Path]=.Start
        //	[Path]=.Completed

        #region Properties

        private const string sceneID = "project/gamepool/videomemory/player";

        #endregion


        #region Funktionen

        public Player(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID, Modes.Static)
        {
            this.init();
        }

        public Player(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Business client,
            int pipeIndex)
            : base(syncContext, client, pipeIndex, sceneID)
        {
            this.init();
        }

        private void init()
        {
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public void SetPositionX(int value) { this.SetDataItemValue(".Position.X", value); }
        public void SetPositionY(int value) { this.SetDataItemValue(".Position.Y", value); }
        public void SetScaling(double value) { this.SetDataItemValue(".Scaling", value); }
        public void SetMuteAudio(bool value) { this.SetDataItemValue(".MuteAudio", value); }
        public void SetShowBorder(bool value) { this.SetDataItemValue(".ShowBorder", value); }
        public void SetFilename(string value) { this.SetDataItemValue(".Filename", value); }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void Start() { this.SetDataItemTrigger(".Start"); }

        #endregion


        #region Events.Outgoing

        public event EventHandler Completed;
        private void on_Completed(object sender, EventArgs e) { Helper.raiseEvent(sender, this.Completed, e); }

        #endregion

        #region Events.Incoming

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e)
        {
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs)
            {
                if (e.Path == ".Completed") { this.on_Completed(sender, new EventArgs()); }
            }
        }

        #endregion

    }
}
