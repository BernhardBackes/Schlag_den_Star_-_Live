using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.Correlation {

    public class Insert : _Base {

        //	[Path]=.Reset
        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.Resolve
        //	[Path]=.Choice.Button_1.ToIn
        //	[Path]=.Choice.Button_1.ToOut
        //	[Path]=.Choice.Button_1.Text
        //  ...
        //	[Path]=.Choice.Button_9.Text
        //	[Path]=.Solution.Button_1.ToNeutral
        //	[Path]=.Solution.Button_1.ToSelected
        //	[Path]=.Solution.Button_1.ToTrue
        //	[Path]=.Solution.Button_1.ToFalse
        //	[Path]=.Solution.Button_1.ChoiceButtonIndex
        //	[Path]=.Solution.Button_1.TopText
        //	[Path]=.Solution.Button_1.SolutionText
        //	[Path]=.Solution.Button_1.SelectionText
        //  ...
        //	[Path]=.Solution.Button_8.SelectionText

        #region Properties

        private const string sceneID = "project/gamepool/correlation/insert";

        public _Modules.Score Score;
        public _Modules.Timer Timer;

        #endregion


        #region Funktionen

        public Insert(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Insert(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
            this.Score = new _Modules.Score(syncContext, this.addPort("ScoreLayer"));
            this.Timer = new _Modules.Timer(syncContext, this.addPort("TimerLayer"));
        }

        public override void Dispose() {
            base.Dispose();
            this.Score.Dispose();
            this.Timer.Dispose();
        }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }
        public void SetTrue() { this.SetDataItemTrigger(".SetTrue"); }

        public void Resolve() { this.SetDataItemTrigger(".Resolve"); }

        public void SetChoiceButtonText(
            int id,
            string text) {
            string dataItemName = string.Format(".Choice.Button_{0}.Text", id.ToString());
            this.SetDataItemValue(dataItemName, text);
        }
        public void ChoiceButtonIn(
            int id) {
            string dataItemName = string.Format(".Choice.Button_{0}.ToIn", id.ToString());
            this.SetDataItemTrigger(dataItemName);
        }
        public void ChoiceButtonOut(
            int id) {
            string dataItemName = string.Format(".Choice.Button_{0}.ToOut", id.ToString());
            this.SetDataItemTrigger(dataItemName);
        }

        public void SetSolutionButtonChoiceIndex(
            int id,
            int choiceButtonIndex) {
            string dataItemName = string.Format(".Solution.Button_{0}.ChoiceButtonIndex", id.ToString());
            this.SetDataItemValue(dataItemName, choiceButtonIndex);
        }
        public void SetSolutionButtonTopText(
            int id,
            string text) {
            string dataItemName = string.Format(".Solution.Button_{0}.TopText", id.ToString());
            this.SetDataItemValue(dataItemName, text);
        }
        public void SetSolutionButtonSolutionText(
            int id,
            string text) {
            string dataItemName = string.Format(".Solution.Button_{0}.SolutionText", id.ToString());
            this.SetDataItemValue(dataItemName, text);
        }
        public void SetSolutionButtonSelectionText(
            int id,
            string text) {
            string dataItemName = string.Format(".Solution.Button_{0}.SelectionText", id.ToString());
            this.SetDataItemValue(dataItemName, text);
        }
        public void SetSolutionButtonNeutral(
            int id) {
            string dataItemName = string.Format(".Solution.Button_{0}.ToNeutral", id.ToString());
            this.SetDataItemTrigger(dataItemName);
        }
        public void SelectSolutionButton(
            int id) {
            string dataItemName = string.Format(".Solution.Button_{0}.ToSelected", id.ToString());
            this.SetDataItemTrigger(dataItemName);
        }
        public void SetSolutionButtonTrue(
            int id) {
            string dataItemName = string.Format(".Solution.Button_{0}.ToTrue", id.ToString());
            this.SetDataItemTrigger(dataItemName);
        }
        public void SetSolutionButtonFalse(
            int id) {
            string dataItemName = string.Format(".Solution.Button_{0}.ToFalse", id.ToString());
            this.SetDataItemTrigger(dataItemName);
        }

        public void PlayJingle(string name) { 
            string dataItemName = string.Format(".Jingles.{0}.Play", name);
            this.SetDataItemTrigger(dataItemName);
        }

        public override void Clear() {
            base.Clear();
            this.Timer.Clear();
            this.Score.Clear();
            this.ToOut();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
