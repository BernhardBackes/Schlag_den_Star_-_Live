using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class TaskCounter : _InsertBase {

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
            [Default]= Numeric (the default value for this DataItem)
            [Elements]= Numeric,Penalty (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.Size
        //	[Path]=.Counter
        /*
        [Path]= .Dots.Dot_01 (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Dot_01 (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= Off (the default value for this DataItem)
            [Elements]= Off,Fail,Red,Blue (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.Dots.Dot_02
        //	[Path]=.Dots.Dot_03
        //	[Path]=.Dots.Dot_04
        //	[Path]=.Dots.Dot_05
        //	[Path]=.Dots.Dot_06
        //	[Path]=.Dots.Dot_07
        //	[Path]=.Dots.Dot_08
        //	[Path]=.Dots.Dot_09
        //	[Path]=.Dots.Dot_10
        //	[Path]=.Dots.Dot_11
        //	[Path]=.Dots.Dot_12
        //	[Path]=.Dots.Dot_13

        #region Properties

        private const string sceneID = "project/gamepool/_modules/taskcounter";

        public enum Styles { Numeric, Penalty, Coaster }

        public enum DotStates { Off, Fail, Red, Blue }

        #endregion


        #region Funktionen

        public TaskCounter(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public TaskCounter(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetStyle(Styles value) { this.SetDataItemValue(".Style", value); }
        public void SetSize(int value) { this.SetDataItemValue(".Size", value); }

        public void SetCounter(int value) { this.SetDataItemValue(".Counter", value); }

        public void SetDot(int id, DotStates value) { this.SetDataItemValue(string.Format(".Dots.Dot_{0}", id.ToString("00")), value); }

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
