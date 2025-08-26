using System.Threading;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MathematicalFormulaScore {

    public class Host : _Base {


        //	[Path]=.Reset
        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.Text
        //	[Path]=.Input.Value
        /*
        [Path]= .Input.Position (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Position (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= Left (the default value for this DataItem)
            [Elements]= Left,Right (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.Input.Correct
        //	[Path]=.Input.SetOut
        //	[Path]=.Input.SetIn

        public enum InputPositions { Left, Right };

        #region Properties

        private const string sceneID = "project/gamepool/mathematicalformulascore/host";

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

        public void SetText(string value) { this.SetDataItemValue(".Text", value); }
        public void SetInputValue(string value) { this.SetDataItemValue(".Input.Value", value); }
        public void SetInputPosition(InputPositions value) { this.SetDataItemValue(".Input.Position", value); }
        public void SetInputIsCorrect(bool value) { this.SetDataItemValue(".Input.Correct", value); }
        public void SetInputIn() { this.SetDataItemTrigger(".Input.SetIn"); }
        public void SetInputOut() { this.SetDataItemTrigger(".Input.SetOut"); }

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
