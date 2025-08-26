using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class Score : _InsertBase {

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
            [Default]= ThreeDots (the default value for this DataItem)
            [Elements]= Names, TwoDots, ThreeDots, FourDots, FiveDots, FiveDotsWhite, FiveDotsWhiteLeft, SixDots, SevenDots, ThreeCrosses, FourCrosses, FiveCrosses, SixCrosses, SevenCrosses, Counter, CounterLeft, CounterLeftLarge, CounterIsolatedOneDigit, CounterIsolatedTwoDigits, Sport, SportTeamColors, ThreeBasketballsLeft, Ballons, FourColoredDots, SevenColoredDots (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.FlipPosition
        //	[Path]=.LeftTop.Name
        //	[Path]=.LeftTop.Score
        //	[Path]=.RightBottom.Name
        //	[Path]=.RightBottom.Score

        public enum Styles { Names, TwoDots, ThreeDots, FourDots, FiveDots, FiveDotsWhite, FiveDotsWhiteLeft, SixDots, SevenDots, ThreeCrosses, FourCrosses, FiveCrosses, SixCrosses, SevenCrosses, Counter, CounterLeft, CounterLeftLarge, CounterIsolatedOneDigit, CounterIsolatedTwoDigits, Sport, SportTeamColors, ThreeBasketballsLeft, Ballons, FourColoredDots, SevenColoredDots }     

        #region Properties     

        private const string sceneID = "project/gamepool/_modules/score";

        #endregion


        #region Funktionen

        public Score(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public Score(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetStyle(Styles value) { this.SetDataItemValue(".Style", value); }

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
