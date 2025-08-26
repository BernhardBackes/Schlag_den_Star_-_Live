using System.Threading;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class ScorePointer: _InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //  [Path]=.Style
        //	[Path]=.Pointer
        //	[Path]=.FlipPosition
        //	[Path]=.LeftTop.Name
        //	[Path]=.LeftTop.Score
        //	[Path]=.RightBottom.Name
        //	[Path]=.RightBottom.Score

        public enum Styles { Points10, Points14, Points15, Points16, Points18, Points20 }

        #region Properties     
   
        private const string sceneID = "project/gamepool/_modules/scorepointer";

        #endregion


        #region Funktionen

        public ScorePointer(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public ScorePointer(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetStyle(Styles value) { this.SetDataItemValue(".Style", value); }

        public void SetPointer(int value) { this.SetDataItemValue(".Pointer", value); }

        public void SetLeftTopName(string value) { this.SetDataItemValue(".LeftTop.Name", value); }
        public void SetLeftTopScore(int value) { this.SetDataItemValue(".LeftTop.Score", value); }

        public void SetRightBottomName(string value) { this.SetDataItemValue(".RightBottom.Name", value); }
        public void SetRightBottomScore(int value) { this.SetDataItemValue(".RightBottom.Score", value); }

        public void SetFlipPosition(bool value) { this.SetDataItemValue(".FlipPosition", value); }

        public override void Dispose() {
            base.Dispose();
            this.Reset();
        }

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
