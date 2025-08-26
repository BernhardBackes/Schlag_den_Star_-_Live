using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SelectAnswerScore {

    public class Game : _Modules._InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.LeftPlayer.Name
        /*
        [Path]= .LeftPlayer.Selection (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Selection (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= NotAvailable (the default value for this DataItem)
            [Elements]= NotAvailable,Left,Right (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.RightPlayer.Name
        //	[Path]=.RightPlayer.Selection
        //	[Path]=.Selection.SetOut
        //	[Path]=.Selection.ToOut
        //	[Path]=.Selection.SetIn
        //	[Path]=.Selection.ToIn
        //	[Path]=.Text.Left
        //	[Path]=.Text.Right
        /*
        [Path]= .Solution (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Solution (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= NotAvailable (the default value for this DataItem)
            [Elements]= NotAvailable,Left,Right (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */

        #region Properties

        private const string sceneID = "project/gamepool/selectanswerscore/game";

        public enum PlayerSelections { NotAvailable, Left, Right }

        public enum SolutionStates { NotAvailable, Left, Right }

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

        public void SetLeftPlayerName(string value) { this.SetDataItemValue(".LeftPlayer.Name", value); }
        public void SetLeftPlayerSelection(PlayerSelections value) { this.SetDataItemValue(".LeftPlayer.Selection", value); }

        public void SetRightPlayerName(string value) { this.SetDataItemValue(".RightPlayer.Name", value); }
        public void SetRightPlayerSelection(PlayerSelections value) { this.SetDataItemValue(".RightPlayer.Selection", value); }

        public void SetLeftText(string value) { this.SetDataItemValue(".Text.Left", value); }
        public void SetRightText(string value) { this.SetDataItemValue(".Text.Right", value); }

        public void SetSelectionOut() { this.SetDataItemTrigger(".Selection.SetOut"); }
        public void SelectionToOut() { this.SetDataItemTrigger(".Selection.ToOut"); }
        public void SetSelectionIn() { this.SetDataItemTrigger(".Selection.SetIn"); }
        public void SelectionToIn() { this.SetDataItemTrigger(".Selection.ToIn"); }

        public void SetSolution(SolutionStates value) { this.SetDataItemValue(".Solution", value); }

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
