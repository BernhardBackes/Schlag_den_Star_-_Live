using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.Geography {

    public class Fullscreen : _Base {


        /*
        [Path]= .Map.Layout (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Layout (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= Africa (the default value for this DataItem)
            [Elements]= Africa,Asia,Europe,Northamerica,Southamerica (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.Reset
        //	[Path]=.Task.Reset
        //	[Path]=.Task.Show
        //	[Path]=.Task.Text
        //	[Path]=.YellowPin.Position.X
        //	[Path]=.YellowPin.Position.Y
        //	[Path]=.YellowPin.Reset
        //	[Path]=.YellowPin.Set
        //	[Path]=.YellowPin.Show
        //	[Path]=.BluePin.Position.X
        //	[Path]=.BluePin.Position.Y
        //	[Path]=.BluePin.Reset
        //	[Path]=.BluePin.Set
        //	[Path]=.BluePin.Show
        //	[Path]=.RedPin.Position.X
        //	[Path]=.RedPin.Position.Y
        //	[Path]=.RedPin.Reset
        //	[Path]=.RedPin.Set
        //	[Path]=.RedPin.Show
        //	[Path]=.Results.Reset
        //	[Path]=.Results.Show
        //	[Path]=.Results.Solution
        //	[Path]=.Results.Blue.Name
        //	[Path]=.Results.Blue.Distance
        //	[Path]=.Results.Blue.ResetDistance
        //	[Path]=.Results.Blue.ShowDistance
        //	[Path]=.Results.Red.Name
        //	[Path]=.Results.Red.Distance
        //	[Path]=.Results.Red.ResetDistance
        //	[Path]=.Results.Red.ShowDistance

        public enum MapLayoutElements { Africa, Asia, Europe, Northamerica, Southamerica, Germany }

        #region Properties

        private const string sceneID = "project/gamepool/geography/fullscreen";

        #endregion


        #region Funktionen

        public Fullscreen(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Fullscreen(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
        }



        public void Reset() { this.SetDataItemTrigger(".Reset"); }
        public void SetMapLayout(MapLayoutElements value) { this.SetDataItemValue(".Map.Layout", value); }
        public void SetTaskText(string value) { this.SetDataItemValue(".Task.Text", value); }
        public void ResetTask() { this.SetDataItemTrigger(".Task.Reset"); }
        public void ShowTask() { this.SetDataItemTrigger(".Task.Show"); }

        public void SetYellowPinPosition(
            double x,
            double y) {
            this.SetDataItemValue(".YellowPin.Position.X", x);
            this.SetDataItemValue(".YellowPin.Position.Y", y);
        }
        public void ResetYellowPin() { this.SetDataItemTrigger(".YellowPin.Reset"); }
        public void SetYellowPin() { this.SetDataItemTrigger(".YellowPin.Set"); }
        public void ShowYellowPin() { this.SetDataItemTrigger(".YellowPin.Show"); }

        public void SetBluePinPosition(
            double x,
            double y) {
            this.SetDataItemValue(".BluePin.Position.X", x);
            this.SetDataItemValue(".BluePin.Position.Y", y);
        }
        public void ResetBluePin() { this.SetDataItemTrigger(".BluePin.Reset"); }
        public void SetBluePin() { this.SetDataItemTrigger(".BluePin.Set"); }
        public void ShowBluePin() { this.SetDataItemTrigger(".BluePin.Show"); }

        public void SetRedPinPosition(
            double x,
            double y) {
            this.SetDataItemValue(".RedPin.Position.X", x);
            this.SetDataItemValue(".RedPin.Position.Y", y);
        }
        public void ResetRedPin() { this.SetDataItemTrigger(".RedPin.Reset"); }
        public void SetRedPin() { this.SetDataItemTrigger(".RedPin.Set"); }
        public void ShowRedPin() { this.SetDataItemTrigger(".RedPin.Show"); }

        public void SetResultsSolution(string value) { this.SetDataItemValue(".Results.Solution", value); }
        public void SetResultsBlueName(string value) { this.SetDataItemValue(".Results.Blue.Name", value); }
        public void SetResultsBlueDistance(int value) { this.SetDataItemValue(".Results.Blue.Distance", value); }
        public void SetResultsRedName(string value) { this.SetDataItemValue(".Results.Red.Name", value); }
        public void SetResultsRedDistance(int value) { this.SetDataItemValue(".Results.Red.Distance", value); }
        public void ResetResults() { this.SetDataItemTrigger(".Results.Reset"); }
        public void ShowResults() { this.SetDataItemTrigger(".Results.Show"); }
        public void ResetBlueDistance() { this.SetDataItemTrigger(".Results.Blue.ResetDistance"); }
        public void ShowBlueDistance() { this.SetDataItemTrigger(".Results.Blue.ShowDistance"); }
        public void ResetRedDistance() { this.SetDataItemTrigger(".Results.Red.ResetDistance"); }
        public void ShowRedDistance() { this.SetDataItemTrigger(".Results.Red.ShowDistance"); }

        public override void Dispose() {
            base.Dispose();
            this.Reset();
        }

        public override void Clear() {
            base.Clear();
            this.Reset();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
