using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MovieSolutionScore {

    //	[Path]=.MovieFilename
    //	[Path]=.Reset
    //	[Path]=.SetIn
    //	[Path]=.ToIn
    //	[Path]=.Start
    //	[Path]=.ToLastFrame
    //	[Path]=.ToOut
    //	[Path]=.SetOut
    //	[Path]=.Jingle.PlayResolve
    //	[Path]=.Insert.Position.X
    //	[Path]=.Insert.Position.Y
    //	[Path]=.Insert.SetIn
    //	[Path]=.Insert.ToIn
    //	[Path]=.Insert.ToOut
    //	[Path]=.Insert.SetOut
    //	[Path]=.Insert.LeftName
    //	[Path]=.Insert.Time
    //	[Path]=.Insert.RightName

    public class Fullscreen : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/moviesolutionscore/fullscreen";

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

        public void SetMovieFilename(string value) { this.SetDataItemValue(".MovieFilename", value); }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void Start() { this.SetDataItemTrigger(".Start"); }

        public void ToLastFrame() { this.SetDataItemTrigger(".ToLastFrame"); }

        public void SetInsertPositionX(int value) { this.SetDataItemValue(".Insert.Position.X", value); }
        public void SetInsertPositionY(int value) { this.SetDataItemValue(".Insert.Position.Y", value); }
        public void SetInsertLeftName(string value) { this.SetDataItemValue(".Insert.LeftName", value); }
        public void SetInsertTime(string value) { this.SetDataItemValue(".Insert.Time", value); }
        public void SetInsertRightName(string value) { this.SetDataItemValue(".Insert.RightName", value); }
        public void SetInsertOut() { this.SetDataItemTrigger(".Insert.SetOut"); }
        public void InsertToOut() { this.SetDataItemTrigger(".Insert.ToOut"); }
        public void SetInsertIn() { this.SetDataItemTrigger(".Insert.SetIn"); }
        public void InsertToIn() { this.SetDataItemTrigger(".Insert.ToIn"); }

        public void PlayJingleResolve() { this.SetDataItemTrigger(".Jingle.PlayResolve"); }

        public override void Clear() {
            base.Clear();
            this.ToOut();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
