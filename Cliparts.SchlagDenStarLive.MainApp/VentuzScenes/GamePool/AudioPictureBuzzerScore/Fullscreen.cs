using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.AudioPictureBuzzerScore {

    public class Fullscreen : _Base {

        //	[Path]=.Audio.Start
        //	[Path]=.Audio.Pause
        //	[Path]=.Audio.Play        
        //	[Path]=.Audio.Stop
        //	[Path]=.Audio.Filename
        //	[Path]=.Audio.MaxVolume
        //	[Path]=.Audio.Duration
        //	[Path]=.Audio.Remaining
        //	[Path]=.Audio.RemainingChanged

        #region Properties

        private const string sceneID = "project/gamepool/audiopicturebuzzerscore/fullscreen";

        private int duration = 0;
        public int Duration {
            get { return this.duration; }
            set {
                if (this.duration != value) {
                    this.duration = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int remaining = 0;
        public int Remaining {
            get { return this.remaining; }
            set {
                if (this.remaining != value) {
                    this.remaining = value;
                    this.on_PropertyChanged();
                }
            }
        }

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
        }

        public override void Dispose() {
            base.Dispose();
        }

        public void SetAudioFilename(string value) { this.SetDataItemValue(".Audio.Filename", value); }
        public void SetMaxVolume(int value) { this.SetDataItemValue(".Audio.MaxVolume", value); }
        public void StartAudio() { this.SetDataItemTrigger(".Audio.Start"); }
        public void PauseAudio() { this.SetDataItemTrigger(".Audio.Pause"); }
        public void PlayAudio() { this.SetDataItemTrigger(".Audio.Play"); }
        public void StopAudio() { this.SetDataItemTrigger(".Audio.Stop"); }

        public override void Clear() {
            base.Clear();
            this.PauseAudio();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Audio.DurationChanged") { this.Duration = Convert.ToInt32(e.Value); }
                else if (e.Path == ".Audio.RemainingChanged") { this.Remaining = Convert.ToInt32(e.Value); }
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs &&
                e.Value != null) {
                if (e.Path == ".Audio.Duration") { this.Duration = Convert.ToInt32(e.Value); }
                else if (e.Path == ".Audio.Remaining") { this.Remaining = Convert.ToInt32(e.Value); }
            }
        }

        #endregion

    }

}
