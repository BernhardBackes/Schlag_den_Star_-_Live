using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SetsPenalty {

    public class Insert : _Modules._InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Top.Name
        //	[Path]=.Top.Value_01
        //	[Path]=.Top.Status_01
        //	[Path]=.Top.Value_02
        //	...
        //	[Path]=.Top.Status_10
        //	[Path]=.Bottom.Name
        //	[Path]=.Bottom.Value_01
        //	[Path]=.Bottom.Status_01
        //	[Path]=.Bottom.Value_02
        //	...
        //	[Path]=.Bottom.Status_10
        /*
        [Path]= .Penalty.Shots (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Shots (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= FiveShots (the default value for this DataItem)
            [Elements]= FiveShots,EightShots,TenShots (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.Penalty.Position
        //	[Path]=.Penalty.SetOut
        //	[Path]=.Penalty.ToOut
        //	[Path]=.Penalty.SetIn
        //	[Path]=.Penalty.ToIn
        //	[Path]=.Penalty.Dot_01
        //	[Path]=.Penalty.Dot_02
        //	[Path]=.Penalty.Dot_03
        //	[Path]=.Penalty.Dot_04
        //	[Path]=.Penalty.Dot_05

        public enum Styles { TwoSets, ThreeSets, FourSets, FiveSets, SixSets, SevenSets, EightSets, NineSets, TenSets }

        public enum PenaltyShots { FiveShots, EightShots, TenShots }
        public enum PenaltyPositions { Top, Bottom }

        #region Properties

        private const string sceneID = "project/gamepool/setspenalty/insert";

        #endregion


        #region Funktionen

        public Insert(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Insert(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
        }

        public void SetStyle(Styles value) { this.SetDataItemValue(".Style", value); }

        public void SetTopName(string value) { this.SetDataItemValue(".Top.Name", value); }
        public void SetTopValue(
            int id,
            int value) {
            string name = string.Format(".Top.Value_{0}", id.ToString("00"));
            this.SetDataItemValue(name, value);
        }
        public void SetTopStatus(
            int id,
            _Modules.Sets.ValueStatusElements status) {
            string name = string.Format(".Top.Status_{0}", id.ToString("00"));
            this.SetDataItemValue(name, status);
        }

        public void SetBottomName(string value) { this.SetDataItemValue(".Bottom.Name", value); }
        public void SetBottomValue(
            int id,
            int value) {
            string name = string.Format(".Bottom.Value_{0}", id.ToString("00"));
            this.SetDataItemValue(name, value);
        }
        public void SetBottomStatus(
            int id,
            _Modules.Sets.ValueStatusElements status) {
            string name = string.Format(".Bottom.Status_{0}", id.ToString("00"));
            this.SetDataItemValue(name, status);
        }

        public void SetPenaltyOut() { this.SetDataItemTrigger(".Penalty.SetOut"); }
        public void PenaltyToOut() { this.SetDataItemTrigger(".Penalty.ToOut"); }
        public void SetPenaltyIn() { this.SetDataItemTrigger(".Penalty.SetIn"); }
        public void PenaltyToIn() { this.SetDataItemTrigger(".Penalty.ToIn"); }
        public void SetPenaltyShots(PenaltyShots value) { this.SetDataItemValue(".Penalty.Shots", value); }
        public void SetPenaltyPosition(PenaltyPositions value) { this.SetDataItemValue(".Penalty.Position", value); }
        public void SetDot(
            int id,
            VentuzScenes.GamePool._Modules.Penalty.DotStates status) {
            string name = string.Format(".Penalty.Dot_{0}", id.ToString("00"));
            this.SetDataItemValue(name, status);
        }

 
        public override void Dispose() {
            this.Reset();
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
