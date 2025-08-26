using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class Sets : _InsertBase {


        /*
        [Path]= .Style (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Style (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= SixSetsSum (the default value for this DataItem)
            [Elements]= OneSet,TwoSets,TwoSetsSum,ThreeSets,ThreeSetsSum,FourSetsSum,FiveSetsSum,SixSetsSum,SevenSetsSum,EightSetsSum,NineSetsSum,TenSetsSum,TenSetsSumSmall (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        //	[Path]=.Top.Name
        //	[Path]=.Top.Value_01
        //	[Path]=.Top.Status_01
        //	[Path]=.Top.Value_02
        //	[Path]=.Top.Status_02
        //	[Path]=.Top.Value_03
        //	[Path]=.Top.Status_03
        //	[Path]=.Top.Value_04
        //	[Path]=.Top.Status_04
        //	[Path]=.Top.Value_05
        //	[Path]=.Top.Status_05
        //	[Path]=.Top.Value_06
        //	[Path]=.Top.Status_06
        //	[Path]=.Top.Value_07
        //	[Path]=.Top.Status_07
        //	[Path]=.Top.Value_08
        //	[Path]=.Top.Status_08
        //	[Path]=.Top.Value_09
        //	[Path]=.Top.Status_09
        //	[Path]=.Top.Value_10
        //	[Path]=.Top.Status_10
        //	[Path]=.Top.ScoreOffset
        //	[Path]=.Bottom.Name
        //	[Path]=.Bottom.Value_01
        //	[Path]=.Bottom.Status_01
        //	[Path]=.Bottom.Value_02
        //	[Path]=.Bottom.Status_02
        //	[Path]=.Bottom.Value_03
        //	[Path]=.Bottom.Status_03
        //	[Path]=.Bottom.Value_04
        //	[Path]=.Bottom.Status_04
        //	[Path]=.Bottom.Value_05
        //	[Path]=.Bottom.Status_05
        //	[Path]=.Bottom.Value_06
        //	[Path]=.Bottom.Status_06
        //	[Path]=.Bottom.Value_07
        //	[Path]=.Bottom.Status_07
        //	[Path]=.Bottom.Value_08
        //	[Path]=.Bottom.Status_08
        //	[Path]=.Bottom.Value_09
        //	[Path]=.Bottom.Status_09
        //	[Path]=.Bottom.Value_10
        //	[Path]=.Bottom.Status_10
        //	[Path]=.Bottom.ScoreOffset


        public enum StyleElements { OneSet, TwoSets, TwoSetsSum, ThreeSets, ThreeSetsSum, FourSetsSum, FiveSetsSum, SixSetsSum, SevenSetsSum, EightSetsSum, NineSetsSum, TenSetsSum, TenSetsSumSmall }

        public enum ValueStatusElements { Idle, Valid, Invalid }

        #region Properties     
   
        private const string sceneID = "project/gamepool/_modules/sets";

        #endregion


        #region Funktionen

        public Sets(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public Sets(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetStyle(StyleElements value) { this.SetDataItemValue(".Style", value); }

        public void SetTopName(string value) { this.SetDataItemValue(".Top.Name", value); }
        public void SetTopValue(
            int id,
            int value) {
            string name = string.Format(".Top.Value_{0}", id.ToString("00"));
            this.SetDataItemValue(name, value);
        }
        public void SetTopStatus(
            int id,
            ValueStatusElements status) {
            string name = string.Format(".Top.Status_{0}", id.ToString("00"));
            this.SetDataItemValue(name, status);
        }
        public void SetTopScoreOffset(int value) { this.SetDataItemValue(".Top.ScoreOffset", value); }

        public void SetBottomName(string value) { this.SetDataItemValue(".Bottom.Name", value); }
        public void SetBottomValue(
            int id,
            int value) {
            string name = string.Format(".Bottom.Value_{0}", id.ToString("00"));
            this.SetDataItemValue(name, value);
        }
        public void SetBottomStatus(
            int id,
            ValueStatusElements status) {
            string name = string.Format(".Bottom.Status_{0}", id.ToString("00"));
            this.SetDataItemValue(name, status);
        }
        public void SetBottomScoreOffset(int value) { this.SetDataItemValue(".Bottom.ScoreOffset", value); }

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
