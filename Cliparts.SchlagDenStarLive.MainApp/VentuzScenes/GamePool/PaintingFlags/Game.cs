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

    public enum OrientationStates { Horizontal, Vertical }
    public enum ColorStates { Neutral, Blue, Green, Red, Brown, Orange, Yellow, White, Black }

    public class Game : _Modules._InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Solution.Filename
        //	[Path]=.Solution.SetOut
        //	[Path]=.Solution.ToOut
        //	[Path]=.Solution.SetIn
        //	[Path]=.Solution.ToIn
        /*
        [Path]= .Orientation (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Orientation (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= Vertical (the default value for this DataItem)
            [Elements]= Horizontal,Vertical (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.LeftPlayer.FlagColor1
        //	[Path]=.LeftPlayer.FlagColor2
        //	[Path]=.LeftPlayer.FlagColor3
        //	[Path]=.RightPlayer.FlagColor1
        //	[Path]=.RightPlayer.FlagColor2
        //	[Path]=.RightPlayer.FlagColor3
        //	[Path]=.PlaySound.BuzzerLeft
        //	[Path]=.PlaySound.BuzzerRight

        #region Properties

        private const string sceneID = "project/gamepool/paintingflags/game";

        #endregion


        #region Funktionen

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetSolutionFilename(string value) { this.SetDataItemValue(".Solution.Filename", value); }
        public void SetSolutionOut() { this.SetDataItemTrigger(".Solution.SetOut"); }
        public void SolutionToOut() { this.SetDataItemTrigger(".Solution.ToOut"); }
        public void SetSolutionIn() { this.SetDataItemTrigger(".Solution.SetIn"); }
        public void SolutionToIn() { this.SetDataItemTrigger(".Solution.ToIn"); }

        public void SetOrientation(OrientationStates value) { this.SetDataItemValue(".Orientation", value); }

        public void SetLeftPlayerFlagColor(int id, ColorStates value) { this.SetDataItemValue($".LeftPlayer.FlagColor{id}", Convert.ToInt32(value)); }

        public void SetRightPlayerFlagColor(int id, ColorStates value) { this.SetDataItemValue($".RightPlayer.FlagColor{id}", Convert.ToInt32(value)); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">BuzzerLeft, BuzzerRight</param>
        public void PlaySound(string name) { this.SetDataItemTrigger($".PlaySound.{name}"); }

        public override void Dispose() {
            base.Dispose();
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
