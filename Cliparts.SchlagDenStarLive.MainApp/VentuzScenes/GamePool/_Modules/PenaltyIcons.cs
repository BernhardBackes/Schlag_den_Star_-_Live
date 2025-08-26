using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class PenaltyIcons : _InsertBase {

        //	[Path]=.Reset
        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.Position.X
        //	[Path]=.Position.Y
        /*
        [Path]= .Style (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Style (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= Football_3x3 (the default value for this DataItem)
            [Elements]= Football_3x3,Football_3x4 (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.Top.Name
        //	[Path]=.Top.Dot_01
        //	...
        //	[Path]=.Top.Dot_12
        //	[Path]=.Bottom.Name
        //	[Path]=.Bottom.Dot_01
        //	...
        //	[Path]=.Bottom.Dot_12

        public enum DotStates { Off, Green, Red }

        public enum Styles { Football_3x3, Football_3x4 }

        #region Properties

        private const string sceneID = "project/gamepool/_modules/penaltyicons";

        #endregion


        #region Funktionen

        public PenaltyIcons(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public PenaltyIcons(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetStyle(Styles value) { this.SetDataItemValue(".Style", value); }

        public void SetTopName(string value) { this.SetDataItemValue(".Top.Name", value); }
        public void SetTopDot(
            int id,
            DotStates status) {
            string name = string.Format(".Top.Dot_{0}", id.ToString("00"));
            this.SetDataItemValue(name, status);
        }

        public void SetBottomName(string value) { this.SetDataItemValue(".Bottom.Name", value); }
        public void SetBottomDot(
            int id,
            DotStates status) {
            string name = string.Format(".Bottom.Dot_{0}", id.ToString("00"));
            this.SetDataItemValue(name, status);
        }

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
