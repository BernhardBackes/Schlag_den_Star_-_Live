using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TeamCorrelation
{

    public enum PositionValues { Desk, Tablet }

    public class Game : _Modules._InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Scaling
        //	[Path]=.Tasks.Task1.File
        //	[Path]=.Tasks.Task2.File
        //	[Path]=.Tasks.Task3.File
        //	[Path]=.Tasks.Task4.File
        /*
        [Path]= .TopRow.Position (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Position (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= Desk (the default value for this DataItem)
            [Elements]= Desk, Tablet (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.TopRow.TopTarget1.File
        //	[Path]=.TopRow.TopTarget2.File
        //	[Path]=.TopRow.TopTarget3.File
        //	[Path]=.TopRow.TopTarget4.File
        //	[Path]=.BottomRow.Position
        //	[Path]=.BottomRow.BottomTarget1.File
        //	[Path]=.BottomRow.BottomTarget2.File
        //	[Path]=.BottomRow.BottomTarget3.File
        //	[Path]=.BottomRow.BottomTarget4.File
        //	[Path]=.Items.Begin
        //	[Path]=.Items.Next
        //	[Path]=.Items.SetTop1
        //	[Path]=.Items.SetTop2
        //	[Path]=.Items.SetTop3
        //	[Path]=.Items.SetBottom1
        //	[Path]=.Items.SetBottom2
        //	[Path]=.Items.SetBottom3
        //	[Path]=.Items.SetAllIn
        //	[Path]=.Items.SetAllOut
        //	[Path]=.Items.ToTop1
        //	[Path]=.Items.ToAllOut

        #region Properties

        private const string sceneID = "project/gamepool/teamcorrelation/game";

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

        public void SetScaling(int value) { this.SetDataItemValue(".Scaling", value); }

        public void ResetItems() { this.SetDataItemTrigger(".Items.Begin"); }
        public void NextItem() { this.SetDataItemTrigger(".Items.Next"); }
        public void SetTopItem1() { this.SetDataItemTrigger(".Items.SetTop1"); }
        public void SetTopItem2() { this.SetDataItemTrigger(".Items.SetTop2"); }
        public void SetTopItem3() { this.SetDataItemTrigger(".Items.SetTop3"); }
        public void SetBottomItem1() { this.SetDataItemTrigger(".Items.SetBottom1"); }
        public void SetBottomItem2() { this.SetDataItemTrigger(".Items.SetBottom2"); }
        public void SetBottomItem3() { this.SetDataItemTrigger(".Items.SetBottom3"); }
        public void SetAllItemsIn() { this.SetDataItemTrigger(".Items.SetAllIn"); }
        public void SetAllItemsOut() { this.SetDataItemTrigger(".Items.SetAllOut"); }
        public void ToAllItemsOut() { this.SetDataItemTrigger(".Items.ToAllOut"); }
        public void ToTopItem1() { this.SetDataItemTrigger(".Items.ToTop1"); }
        public void ToToAllItemsOut() { this.SetDataItemTrigger(".Items.ToAllOut"); }

        public void SetTaskFile(
            int id,
            string value)
        {
            this.SetDataItemValue($".Tasks.Task{id.ToString()}.File", value);
        }

        public void SetTopRowPosition(PositionValues value) { this.SetDataItemValue(".TopRow.Position", value); }
        public void SetTopRowTargetFile(
            int id,
            string value)
        {
            this.SetDataItemValue($".TopRow.TopTarget{id.ToString()}.File", value);
        }

        public void SetBottomRowPosition(PositionValues value) { this.SetDataItemValue(".BottomRow.Position", value); }
        public void SetBottomRowTargetFile(
            int id,
            string value)
        {
            this.SetDataItemValue($".BottomRow.BottomTarget{id.ToString()}.File", value);
        }

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
