using System;
using System.Threading;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.PlaceImageScore {

    public class Game : _Modules._InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        //	[Path]=.Name
        //	[Path]=.MapBlocked
        //	[Path]=.AudioEnabled
        //	[Path]=.Files.Solution
        //	[Path]=.Files.Red
        //	[Path]=.Files.Blue
        //	[Path]=.Solution.Reset
        //	[Path]=.Solution.ToIn
        //	[Path]=.Solution.SetIn
        //	[Path]=.Solution.X
        //	[Path]=.Solution.Y
        //	[Path]=.InputRed.Reset
        //	[Path]=.InputRed.ToIn
        //	[Path]=.InputRed.X
        //	[Path]=.InputRed.Y
        //	[Path]=.InputBlue.Reset
        //	[Path]=.InputBlue.ToIn
        //	[Path]=.InputBlue.X
        //	[Path]=.InputBlue.Y
        /*
        [Path]= .Selection.Color (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Color (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= Red (the default value for this DataItem)
            [Elements]= Orange,Red,Blue (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.Selection.Reset
        //	[Path]=.Selection.Enable
        //	[Path]=.Selection.Disable
        //	[Path]=.Selection.OKPressed
        //	[Path]=.Selection.X
        //	[Path]=.Selection.XChanged
        //	[Path]=.Selection.Y
        //	[Path]=.Selection.YChanged
        //	[Path]=.Result.LeftPlayer.Name
        //	[Path]=.Result.LeftPlayer.Distance
        //	[Path]=.Result.RightPlayer.Name
        //	[Path]=.Result.RightPlayer.Distance
        /*
        [Path]= .Visualisation (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Visualisation (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= Name (the default value for this DataItem)
            [Elements]= Name,Selection,Result (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */

        #region Properties

        private const string sceneID = "project/gamepool/placeimagescore/game";

        public enum ColorElements { Orange, Red, Blue };

        public enum VisualisationElements { Out, Name, Selection, Result };

        private double? imagePositionX = null;
        public double ImagePositionX {
            get {
                if (this.imagePositionX.HasValue) return this.imagePositionX.Value;
                else return 0;
            }
            private set {
                if (!this.imagePositionX.HasValue ||
                    this.imagePositionX != value) {
                    this.imagePositionX = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double? imagePositionY = null;
        public double ImagePositionY {
            get {
                if (this.imagePositionY.HasValue) return this.imagePositionY.Value;
                else return 0;
            }
            private set {
                if (!this.imagePositionY.HasValue ||
                    this.imagePositionY != value) {
                    this.imagePositionY = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            string sceneID,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
        }

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetName(string value) { this.SetDataItemValue(".Name", value); }
        public void SetMapBlocked(bool value) { this.SetDataItemValue(".MapBlocked", value); }
        public void SetAudioEnabled(bool value) { this.SetDataItemValue(".AudioEnabled", value); }
        public void SetSolutionFile(string value) { this.SetDataItemValue(".Files.Solution", value); }
        public void SetRedFile(string value) { this.SetDataItemValue(".Files.Red", value); }
        public void SetBlueFile(string value) { this.SetDataItemValue(".Files.Blue", value); }
        public void ResetSolution()  { this.SetDataItemTrigger(".Solution.ReSet"); }
        public void SolutionToIn()  { this.SetDataItemTrigger(".Solution.ToIn"); }
        public void SetSolutionIn()  { this.SetDataItemTrigger(".Solution.SetIn"); }
        public void SetSolutionX(double value) { this.SetDataItemValue(".Solution.X", value); }
        public void SetSolutionY(double value) { this.SetDataItemValue(".Solution.Y", value); }
        public void ResetInputRed() { this.SetDataItemTrigger(".InputRed.ReSet"); }
        public void InputRedToIn() { this.SetDataItemTrigger(".InputRed.ToIn"); }
        public void SetInputRedX(double value) { this.SetDataItemValue(".InputRed.X", value); }
        public void SetInputRedY(double value) { this.SetDataItemValue(".InputRed.Y", value); }
        public void ResetInputBlue() { this.SetDataItemTrigger(".InputBlue.ReSet"); }
        public void InputBlueToIn() { this.SetDataItemTrigger(".InputBlue.ToIn"); }
        public void SetInputBlueX(double value) { this.SetDataItemValue(".InputBlue.X", value); }
        public void SetInputBlueY(double value) { this.SetDataItemValue(".InputBlue.Y", value); }
        public void SetSelectionColor(ColorElements value) { this.SetDataItemValue(".Selection.Color", value); }
        public void ResetSelection()  { 
            this.SetDataItemTrigger(".Selection.Reset");
            this.imagePositionX = null;
            this.imagePositionY = null;
        }
        public void EnableSelection()  { 
            this.SetDataItemTrigger(".Selection.Enable");
            this.imagePositionX = null;
            this.imagePositionY = null;
        }
        public void DisableSelection()  { this.SetDataItemTrigger(".Selection.Disable"); }
        public void SetResultLeftPlayerName(string value) { this.SetDataItemValue(".Result.LeftPlayer.Name", value); }
        public void SetResultLeftPlayerDistance(double value) { this.SetDataItemValue(".Result.LeftPlayer.Distance", value); }
        public void SetResultRightPlayerName(string value) { this.SetDataItemValue(".Result.RightPlayer.Name", value); }
        public void SetResultRightPlayerDistance(double value) { this.SetDataItemValue(".Result.RightPlayer.Distance", value); }
        public void SetVisualisation(VisualisationElements value) { this.SetDataItemValue(".Visualisation", value); }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Selection.X") this.ImagePositionX = Convert.ToDouble(e.Value);
                else if (e.Path == ".Selection.XChanged") this.ImagePositionX = Convert.ToDouble(e.Value) / 100;
                else if (e.Path == ".Selection.Y") this.ImagePositionX = Convert.ToDouble(e.Value);
                else if (e.Path == ".Selection.YChanged") this.ImagePositionY = Convert.ToDouble(e.Value) / 100;
                else if (e.Path == ".Selection.OKPressed") this.on_OKButtonPressed(this, new EventArgs());
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs &&
                e.Value != null) {
                if (e.Path == ".Selection.X") this.ImagePositionX = Convert.ToDouble(e.Value);
                else if (e.Path == ".Selection.Y") this.ImagePositionY = Convert.ToDouble(e.Value);
            }
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler OKButtonPressed;
        private void on_OKButtonPressed(object sender, EventArgs e) { Helper.raiseEvent(sender, this.OKButtonPressed, e); }

        #endregion

        #region Events.Incoming
        #endregion
    }
}
