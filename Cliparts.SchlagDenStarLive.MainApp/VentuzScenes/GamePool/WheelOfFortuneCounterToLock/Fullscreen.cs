using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.WheelOfFortuneCounterToLock {

    //	[Path]=.Value.Reset
    //	[Path]=.Value.Set
    //	[Path]=.Audio.Stop
    //	[Path]=.Audio.StartCountdown
    //	[Path]=.Audio.PlayJingleGood
    //	[Path]=.Audio.PlayJingleBad

    public class Fullscreen : _Base {

        public const int ValueMinimum = 0;
        public const int ValueMaximum = 20;

        #region Properties

        private const string sceneID = "project/gamepool/wheeloffortunecountertolock/fullscreen";

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

        public void ResetValue() { this.SetDataItemTrigger(".Value.Reset"); }
        public void SetValue(int value) { this.SetDataItemTrigger(".Value.Set", value); }

        public void StopAudio() { this.SetDataItemTrigger(".Audio.Stop"); }
        public void StartCountdown() { this.SetDataItemTrigger(".Audio.StartCountdown"); }
        public void PlayJingleGood() { this.SetDataItemTrigger(".Audio.PlayJingleGood"); }
        public void PlayJingleBad() { this.SetDataItemTrigger(".Audio.PlayJingleBad"); }

        public override void Clear() {
            base.Clear();
            this.StopAudio();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
