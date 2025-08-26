using System.Threading;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class Shooting : _InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        /*
        [Path]= .Style (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Style (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= FourHeats (the default value for this DataItem)
            [Elements]= TwoHeats,ThreeHeats,FourHeats,FiveHeats (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.HitsCountMax
        //	[Path]=.LeftTop.Name
        //	[Path]=.LeftTop.Heats
        //	[Path]=.LeftTop.Hits.SetOut
        //	[Path]=.LeftTop.Hits.ToOut
        //	[Path]=.LeftTop.Hits.SetIn
        //	[Path]=.LeftTop.Hits.ToIn
        //	[Path]=.LeftTop.Hits.Miss
        //	[Path]=.LeftTop.Hits.Count
        //	[Path]=.RightBottom.Name
        //	[Path]=.RightBottom.Heats
        //	[Path]=.RightBottom.Hits.SetOut
        //	[Path]=.RightBottom.Hits.ToOut
        //	[Path]=.RightBottom.Hits.SetIn
        //	[Path]=.RightBottom.Hits.ToIn
        //	[Path]=.RightBottom.Hits.Miss
        //	[Path]=.RightBottom.Hits.Count

        public enum Styles { TwoHeats, ThreeHeats, FourHeats, FiveHeats }

        #region Properties     

        private const string sceneID = "project/gamepool/_modules/shooting";

        #endregion


        #region Funktionen

        public Shooting(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public Shooting(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetStyle(Styles value) { this.SetDataItemValue(".Style", value); }
        public void SetHitsCountMax(int value) { this.SetDataItemValue(".HitsCountMax", value); }

        public void SetLeftTopName(string value) { this.SetDataItemValue(".LeftTop.Name", value); }
        public void SetLeftTopHeats(int value) { this.SetDataItemValue(".LeftTop.Heats", value); }
        public void SetLeftTopHits(int value) { this.SetDataItemValue(".LeftTop.Hits.Count", value); }
        public void SetLeftTopHitsOut() { this.SetDataItemTrigger(".LeftTop.Hits.SetOut"); }
        public void LeftTopHitsToOut() { this.SetDataItemTrigger(".LeftTop.Hits.ToOut"); }
        public void SetLeftTopHitsIn() { this.SetDataItemTrigger(".LeftTop.Hits.SetIn"); }
        public void LeftTopHitsToIn() { this.SetDataItemTrigger(".LeftTop.Hits.ToIn"); }
        public void LeftTopHitsMiss() { this.SetDataItemTrigger(".LeftTop.Hits.Miss"); }

        public void SetRightBottomName(string value) { this.SetDataItemValue(".RightBottom.Name", value); }
        public void SetRightBottomHeats(int value) { this.SetDataItemValue(".RightBottom.Heats", value); }
        public void SetRightBottomHits(int value) { this.SetDataItemValue(".RightBottom.Hits.Count", value); }
        public void SetRightBottomHitsOut() { this.SetDataItemTrigger(".RightBottom.Hits.SetOut"); }
        public void RightBottomHitsToOut() { this.SetDataItemTrigger(".RightBottom.Hits.ToOut"); }
        public void SetRightBottomHitsIn() { this.SetDataItemTrigger(".RightBottom.Hits.SetIn"); }
        public void RightBottomHitsToIn() { this.SetDataItemTrigger(".RightBottom.Hits.ToIn"); }
        public void RightBottomHitsMiss() { this.SetDataItemTrigger(".RightBottom.Hits.Miss"); }

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
