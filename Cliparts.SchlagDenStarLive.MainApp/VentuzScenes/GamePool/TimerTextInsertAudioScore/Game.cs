using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimerTextInsertAudioScore
{

    public class Game : _Base
    {

        //	[Path]=.Audio.Start
        //	[Path]=.Audio.Pause
        //	[Path]=.Audio.Play
        //	[Path]=.Audio.Stop
        //	[Path]=.Audio.Filename
        //	[Path]=.Audio.MaxVolume
        //	[Path]=.Audio.Duration
        //	[Path]=.Audio.DurationChanged
        //	[Path]=.Audio.Remaining
        //	[Path]=.Audio.RemainingChanged

        #region Properties

        private const string sceneID = "project/gamepool/timertextinsertaudioscore/game";

        #endregion


        #region Funktionen

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID, Modes.Static)
        {
        }

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID)
        {
        }

        public void SetAudioFilename(string value) { this.SetDataItemValue(".Audio.Filename", value); }
        public void PlayAudio() { this.SetDataItemTrigger(".Audio.Start"); }

        public override void Dispose() {
            base.Dispose();
        }

        public override void Clear() {
            base.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
