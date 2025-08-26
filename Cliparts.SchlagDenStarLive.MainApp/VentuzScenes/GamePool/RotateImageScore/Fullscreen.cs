using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.RotateImageScore {

    //	[Path]=.Reset
    //	[Path]=.SetIn
    //	[Path]=.ToIn
    //	[Path]=.ToOut
    //	[Path]=.SetOut
    //	[Path]=.Task.MovieFilename
    //	[Path]=.Task.Angle
    //	[Path]=.Solution.Reset
    //	[Path]=.Solution.ToOut
    //	[Path]=.Solution.SetIn
    //	[Path]=.Solution.ToIn
    //	[Path]=.Solution.ImageFilename
    //	[Path]=.Solution.PlayerIn
    //	[Path]=.Solution.SetPlayerIn
    //	[Path]=.Solution.Start
    //	[Path]=.Solution.LeftPlayer.Angle
    //	[Path]=.Solution.RightPlayer.Angle

    public class Fullscreen : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/rotateimagescore/fullscreen";

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
            this.TextInsert = new _Modules.TextInsert(this.syncContext, this.addPort("TextInsertLayer"));
        }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }
        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void SetTaskMovieFilename(string value) { this.SetDataItemValue(".Task.MovieFilename", value); }
        public void SetTaskAngle(int value) { this.SetDataItemValue(".Task.Angle", value); }

        public void ResetSolution() { this.SetDataItemTrigger(".Solution.Reset"); }
        public void SolutionToOut() { this.SetDataItemTrigger(".Solution.ToOut"); }
        public void SetSolutionIn() { this.SetDataItemTrigger(".Solution.SetIn"); }
        public void SolutionToIn() { this.SetDataItemTrigger(".Solution.ToIn"); }
        public void StartSolution() { this.SetDataItemTrigger(".Solution.Start"); }

        public void SetSolutionImageFilename(string value) { this.SetDataItemValue(".Solution.ImageFilename", value); }
        public void SetLeftPlayerAngle(int value) { this.SetDataItemValue(".Solution.LeftPlayer.Angle", value); }
        public void SetRightPlayerAngle(int value) { this.SetDataItemValue(".Solution.RightPlayer.Angle", value); }

        public void PlayerIn() { this.SetDataItemTrigger(".Solution.PlayerIn"); }
        public void SetPlayerIn() { this.SetDataItemTrigger(".Solution.SetPlayerIn"); }

        public override void Clear() {
            base.Clear();
            this.ToOut();
            this.TextInsert.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
