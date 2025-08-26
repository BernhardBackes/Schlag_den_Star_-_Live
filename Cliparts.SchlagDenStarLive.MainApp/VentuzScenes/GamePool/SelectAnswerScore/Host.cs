using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SelectAnswerScore {

    public class Host : _Base {

        //	[Path]=.Reset
        //	[Path]=.ToOut
        //	[Path]=.ToIn
        //	[Path]=.SetIn
        //	[Path]=.Headline
        //	[Path]=.LeftText
        //	[Path]=.RightText
        /*
        [Path]= .Solution (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Solution (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= Left (the default value for this DataItem)
            [Elements]= Left,Right (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        /*
        [Path]= .Selection.LeftPlayer (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= LeftPlayer (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= NotAvailable (the default value for this DataItem)
            [Elements]= NotAvailable,Left,Right (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.Selection.RightPlayer

        #region Properties

        private const string sceneID = "project/gamepool/selectanswerscore/host";

        public enum SolutionStates { Left, Right }

        public enum PlayerSelections { NotAvailable, Left, Right }

        #endregion


        #region Funktionen

        public Host(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Host(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
        }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void SetHeadline(string value) { this.SetDataItemValue(".Headline", value); }
        public void SetLeftText(string value) { this.SetDataItemValue(".LeftText", value); }
        public void SetRightText(string value) { this.SetDataItemValue(".RightText", value); }
        public void SetSolution(SolutionStates value) { this.SetDataItemValue(".Solution", value); }

        public void SetLeftPlayerSelection(PlayerSelections value) { this.SetDataItemValue(".Selection.LeftPlayer", value); }
        public void SetRightPlayerSelection(PlayerSelections value) { this.SetDataItemValue(".Selection.RightPlayer", value); }

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
