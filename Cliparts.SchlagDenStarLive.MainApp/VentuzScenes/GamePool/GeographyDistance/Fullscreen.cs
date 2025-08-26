using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.GeographyDistance
{

    public class Fullscreen : _Base {


        //	[Path]=.Reset
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
        //	[Path]=.Task.Reset
        //	[Path]=.Task.Show
        //	[Path]=.Task.Text
        //	[Path]=.Results.Solution
        //	[Path]=.Results.Reset
        //	[Path]=.Results.Show
        //	[Path]=.Results.SetIn
        //	[Path]=.Results.SolutionDistance
        //	[Path]=.Results.ResetSolutionDistance
        //	[Path]=.Results.ShowSolutionDistance
        //	[Path]=.Results.SetSolutionDistanceIn
        //	[Path]=.Results.ShowOffsets
        //	[Path]=.Results.ResetOffsets
        //	[Path]=.Results.SetOInffsetsIn
        //	[Path]=.Results.Blue.Name
        //	[Path]=.Results.Blue.Distance
        //	[Path]=.Results.Blue.ResetDistance
        //	[Path]=.Results.Blue.ShowDistance
        //	[Path]=.Results.Blue.SetBlueDistanceIn
        //	[Path]=.Results.Blue.Offset
        //	[Path]=.Results.Red.Name
        //	[Path]=.Results.Red.Distance
        //	[Path]=.Results.Red.ResetDistance
        //	[Path]=.Results.Red.ShowDistance
        //	[Path]=.Results.Red.SetRedDistanceIn
        //	[Path]=.Results.Red.Offset
        //	[Path]=.YellowPins.Position1.X
        //	[Path]=.YellowPins.Position1.Y
        //	[Path]=.YellowPins.Position2.X
        //	[Path]=.YellowPins.Position2.Y
        //	[Path]=.YellowPins.Reset
        //	[Path]=.YellowPins.Set
        //	[Path]=.YellowPins.Show
        //	[Path]=.BluePins.Position1.X
        //	[Path]=.BluePins.Position1.Y
        //	[Path]=.BluePins.Position2.X
        //	[Path]=.BluePins.Position2.Y
        //	[Path]=.BluePins.Reset
        //	[Path]=.BluePins.Set
        //	[Path]=.BluePins.Show
        //	[Path]=.RedPins.Position1.X
        //	[Path]=.RedPins.Position1.Y
        //	[Path]=.RedPins.Position2.X
        //	[Path]=.RedPins.Position2.Y
        //	[Path]=.RedPins.Reset
        //	[Path]=.RedPins.Set
        //	[Path]=.RedPins.Show

        public enum MapLayoutElements { Africa, Asia, Europe, Northamerica, Southamerica, Germany }

        #region Properties

        private const string sceneID = "project/gamepool/geographydistance/fullscreen";

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

        public void SetYellowPin1Position(
            double x,
            double y)
        {
            this.SetDataItemValue(".YellowPins.Position1.X", x);
            this.SetDataItemValue(".YellowPins.Position1.Y", y);
        }
        public void SetYellowPin2Position(
            double x,
            double y)
        {
            this.SetDataItemValue(".YellowPins.Position2.X", x);
            this.SetDataItemValue(".YellowPins.Position2.Y", y);
        }
        public void ResetYellowPins() { this.SetDataItemTrigger(".YellowPins.Reset"); }
        public void SetYellowPins() { this.SetDataItemTrigger(".YellowPins.Set"); }
        public void ShowYellowPins() { this.SetDataItemTrigger(".YellowPins.Show"); }

        public void SetBluePin1Position(
            double x,
            double y)
        {
            this.SetDataItemValue(".BluePins.Position1.X", x);
            this.SetDataItemValue(".BluePins.Position1.Y", y);
        }
        public void SetBluePin2Position(
            double x,
            double y)
        {
            this.SetDataItemValue(".BluePins.Position2.X", x);
            this.SetDataItemValue(".BluePins.Position2.Y", y);
        }
        public void ResetBluePins() { this.SetDataItemTrigger(".BluePins.Reset"); }
        public void SetBluePins() { this.SetDataItemTrigger(".BluePins.Set"); }
        public void ShowBluePins() { this.SetDataItemTrigger(".BluePins.Show"); }

        public void SetRedPin1Position(
            double x,
            double y)
        {
            this.SetDataItemValue(".RedPins.Position1.X", x);
            this.SetDataItemValue(".RedPins.Position1.Y", y);
        }
        public void SetRedPin2Position(
            double x,
            double y)
        {
            this.SetDataItemValue(".RedPins.Position2.X", x);
            this.SetDataItemValue(".RedPins.Position2.Y", y);
        }
        public void ResetRedPins() { this.SetDataItemTrigger(".RedPins.Reset"); }
        public void SetRedPins() { this.SetDataItemTrigger(".RedPins.Set"); }
        public void ShowRedPins() { this.SetDataItemTrigger(".RedPins.Show"); }

        public void SetResultsSolution(string value) { this.SetDataItemValue(".Results.Solution", value); }
        public void SetResultsSolutionDistance(int value) { this.SetDataItemValue(".Results.SolutionDistance", value); }
        public void SetResultsBlueName(string value) { this.SetDataItemValue(".Results.Blue.Name", value); }
        public void SetResultsBlueDistance(int value) { this.SetDataItemValue(".Results.Blue.Distance", value); }
        public void SetResultsBlueOffset(int value) { this.SetDataItemValue(".Results.Blue.Offset", value); }
        public void SetResultsRedName(string value) { this.SetDataItemValue(".Results.Red.Name", value); }
        public void SetResultsRedDistance(int value) { this.SetDataItemValue(".Results.Red.Distance", value); }
        public void SetResultsRedOffset(int value) { this.SetDataItemValue(".Results.Red.Offset", value); }
        public void ResetResults() { this.SetDataItemTrigger(".Results.Reset"); }
        public void ShowResults() { this.SetDataItemTrigger(".Results.Show"); }
        public void SetResultsIn() { this.SetDataItemTrigger(".Results.SetIn"); }
        public void ResetSolutionDistance() { this.SetDataItemTrigger(".Results.ResetSolutionDistance"); }
        public void ShowSolutionDistance() { this.SetDataItemTrigger(".Results.ShowSolutionDistance"); }
        public void SetSolutionDistanceIn() { this.SetDataItemTrigger(".Results.SetSolutionDistanceIn"); }
        public void ResetBlueDistance() { this.SetDataItemTrigger(".Results.Blue.ResetDistance"); }
        public void ShowBlueDistance() { this.SetDataItemTrigger(".Results.Blue.ShowDistance"); }
        public void SetBlueDistanceIn() { this.SetDataItemTrigger(".Results.Blue.SetBlueDistanceIn"); }
        public void ResetRedDistance() { this.SetDataItemTrigger(".Results.Red.ResetDistance"); }
        public void ShowRedDistance() { this.SetDataItemTrigger(".Results.Red.ShowDistance"); }
        public void SetRedDistanceIn() { this.SetDataItemTrigger(".Results.Red.SetRedDistanceIn"); }
        public void ResetOffsets() { this.SetDataItemTrigger(".Results.ResetOffsets"); }
        public void ShowOffsets() { this.SetDataItemTrigger(".Results.ShowOffsets"); }
        public void SetOInffsetsIn() { this.SetDataItemTrigger(".Results.SetOInffsetsIn"); }

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
