using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SortItemsScore {

    ////	[Path]=.Reset
    ////	[Path]=.SetOut
    ////	[Path]=.ToOut
    ////	[Path]=.SetIn
    ////	[Path]=.ToIn
    ////	[Path]=.Position.X
    ////	[Path]=.Position.Y
    //	[Path]=.LeftPlayer.Score.Value
    //	[Path]=.LeftPlayer.Item_01.Text.Value
    /*
    [Path]= .LeftPlayer.Item_01.Color.Input (full path of the DataItem)
    [Description]=  (the description of the DataItem)
    [Label]= ColorInput (the display label of the DataItem)
    [Mode]= RW (read/write mode of the DataItem)
    [Name]= Input (the name of the DataItem)
    [UserData]=  (user-defined information of the DataItem)
    [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
        [Default]= Yellow (the default value for this DataItem)
        [Elements]= Yellow,Green (an array of strings containing the choice of enum values)
        [PropertyType]= System.String (the underlaying system type of this DataItem)
     */
    //	[Path]=.LeftPlayer.Item_02.Text.Value
    //	...
    //	[Path]=.LeftPlayer.Item_05.Color.Input
    //	[Path]=.RightPlayer.Score.Value
    //	[Path]=.RightPlayer.Item_01.Text.Value
    //	[Path]=.RightPlayer.Item_01.Color.Input
    //	[Path]=.RightPlayer.Item_02.Text.Value
    //	...
    //	[Path]=.RightPlayer.Item_05.Color.Input
    //	[Path]=.Solution.Item_01.ID.Value
    //	[Path]=.Solution.Item_01.Text.Value
    //	[Path]=.Solution.Item_01.Result.Value
    //	[Path]=.Solution.Item_01.Transition.ToInInvoke
    //	[Path]=.Solution.Item_01.Transition.ToOutInvoke
    //	[Path]=.Solution.Item_01.Transition.SetInInvoke
    //	[Path]=.Solution.Item_01.Transition.SetOutInvoke
    //	[Path]=.Solution.Item_02.ID.Value
    //	...
    //	[Path]=.Solution.Item_05.Transition.SetOutInvoke
    //	[Path]=.PlayJingle.In
    //	[Path]=.PlayJingle.Resolve

    public class Game : _Modules._InsertBase {

        public enum ItemColor { Yellow, Green }


        #region Properties

        private const string sceneID = "project/gamepool/sortitemsscore/game";

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

        public void SetLeftPlayerScore(int value) { this.SetDataItemValue(".LeftPlayer.Score.Value", value); }
        public void SetLeftPlayerItemText(
            int itemID,
            string value) {
            this.SetDataItemValue(string.Format(".LeftPlayer.Item_{0}.Text.Value", itemID.ToString("00")), value);
        }
        public void SetLeftPlayerItemColor(
            int itemID,
            ItemColor value) {
            this.SetDataItemValue(string.Format(".LeftPlayer.Item_{0}.Color.Input", itemID.ToString("00")), value);
        }

        public void SetRightPlayerScore(int value) { this.SetDataItemValue(".RightPlayer.Score.Value", value); }
        public void SetRightPlayerName(string value) { this.SetDataItemValue(".RightPlayer.Name", value); }
        public void SetLeftPlayerName(string value) { this.SetDataItemValue(".LeftPlayer.Name", value); }
        public void SetRightPlayerItemText(
            int itemID,
            string value) {
            this.SetDataItemValue(string.Format(".RightPlayer.Item_{0}.Text.Value", itemID.ToString("00")), value);
        }
        public void SetRightPlayerItemColor(
            int itemID,
            ItemColor value) {
            this.SetDataItemValue(string.Format(".RightPlayer.Item_{0}.Color.Input", itemID.ToString("00")), value);
        }

        public void SetSolutionItemID(
            int itemID,
            int value) {
            this.SetDataItemValue(string.Format(".Solution.Item_{0}.ID.Value", itemID.ToString("00")), value);
        }
        public void SetSolutionItemText(
            int itemID,
            string value) {
            this.SetDataItemValue(string.Format(".Solution.Item_{0}.Text.Value", itemID.ToString("00")), value);
        }
        public void SetSolutionItemResult(
            int itemID,
            string value) {
            this.SetDataItemValue(string.Format(".Solution.Item_{0}.Result.Value", itemID.ToString("00")), value);
        }
        public void SetSolutionDampDuration(
            int itemID,
            float value) {
            this.SetDataItemValue(string.Format(".Solution.Item_{0}.Damp.Duration", itemID.ToString("00")), value);
        }

        public void SetSolutionItemOut(
            int itemID) {
            this.SetDataItemTrigger(string.Format(".Solution.Item_{0}.Transition.SetOutInvoke", itemID.ToString("00")));
        }
        public void SolutionItemToOut(
            int itemID) {
            this.SetDataItemTrigger(string.Format(".Solution.Item_{0}.Transition.ToOutInvoke", itemID.ToString("00")));
        }
        public void SetSolutionItemIn(
            int itemID) {
            this.SetDataItemTrigger(string.Format(".Solution.Item_{0}.Transition.SetInInvoke", itemID.ToString("00")));
        }
        public void SolutionItemToIn(
            int itemID) {
            this.SetDataItemTrigger(string.Format(".Solution.Item_{0}.Transition.ToInInvoke", itemID.ToString("00")));
        }

        public void PlayJingleIn() { this.SetDataItemTrigger(".PlayJingle.In"); }
        public void PlayJingleResolve() { this.SetDataItemTrigger(".PlayJingle.Resolve"); }

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
