using System;
using System.Threading;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MathematicalFormulaScore {

    public class Player : _Base {

        //	[Path]=.Reset
        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        /*
        [Path]= .Size (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Size (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= TwoOperations (the default value for this DataItem)
            [Elements]= TwoOperations,ThreeOperations (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.Formula.Number_01
        //	[Path]=.Formula.Number_02
        //	[Path]=.Formula.Number_03
        //	[Path]=.Formula.Number_04
        //	[Path]=.Formula.Solution
        //	[Path]=.Input.Value
        //	[Path]=.Input.Changed
        //	[Path]=.FullText.Value
        //	[Path]=.FullText.Changed
        //	[Path]=.Lock
        //	[Path]=.Unlock
        //	[Path]=.OKPressed

        #region Properties

        private const string sceneID = "project/gamepool/mathematicalformulascore/player";

        private string input = string.Empty;
        public string Input {
            get { return this.input; }
            protected set {
                if (this.input != value) {
                    if (string.IsNullOrEmpty(value)) this.input = string.Empty;
                    else this.input = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string fullText = string.Empty;
        public string FullText {
            get { return this.fullText; }
            protected set {
                if (this.fullText != value) {
                    if (string.IsNullOrEmpty(value)) this.fullText = string.Empty;
                    else this.fullText = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public Player(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Player(
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

        public void SetSize(Game.Sizes value) { this.SetDataItemValue(".Size", value); }
        public void SetNumber(int id, int value) { this.SetDataItemValue(string.Format(".Formula.Number_{0}", id.ToString("00")), value); }
        public void SetSolution(int value) { this.SetDataItemValue(".Formula.Solution", value); }

        public void LockTouch() { this.SetDataItemTrigger(".Lock"); }
        public void UnlockTouch() { this.SetDataItemTrigger(".Unlock"); }

        public override void Dispose() {
            base.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.ToOut();
        }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Input.Changed") this.syncDataItemValue(".Input.Value");
                else if (e.Path == ".FullText.Changed") this.syncDataItemValue(".FullText.Value");
                else if (e.Path == ".OKPressed") {
                    this.on_OKButtonPressed(this, new EventArgs());
                    this.syncDataItemValue(".Input.Value");
                    this.syncDataItemValue(".FullText.Value");
                }
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs &&
                e.Value != null) {
                if (e.Path == ".Input.Value") this.Input = e.Value.ToString();
                else if (e.Path == ".FullText.Value") this.FullText = e.Value.ToString();
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
