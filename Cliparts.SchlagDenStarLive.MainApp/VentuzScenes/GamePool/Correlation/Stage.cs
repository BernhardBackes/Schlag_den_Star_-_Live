using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.Correlation {

    public class Stage : _Base {

        //	[Path]=.Reset
        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.TaskText
        //	[Path]=.Solutions.Solution_01.TopText
        //	[Path]=.Solutions.Solution_01.BottomText
        //	[Path]=.Solutions.Solution_01.IsTrue
        //  ...
        //	[Path]=.Solutions.Solution_08.IsTrue
        //	[Path]=.Choices.Choice_01.Text
        //	[Path]=.Choices.Choice_01.Show
        //	[Path]=.Choices.Choice_01.Hide
        //  ...
        //	[Path]=.Choices.Choice_09.Hide

        #region Properties

        private const string sceneID = "project/gamepool/correlation/stage";

        #endregion

        #region Funktionen

        public Stage(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Stage(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
        }

        public override void Dispose() {
            base.Dispose();
        }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void SetTaskText(string text) { this.SetDataItemValue(".TaskText", text); }

        public void SetSolutionTopText(
            int id,
            string text) {
            string dataItemName = string.Format(".Solutions.Solution_{0}.TopText", id.ToString("00"));
            this.SetDataItemValue(dataItemName, text);
        }
        public void SetSolutionBottomText(
            int id,
            string text) {
            string dataItemName = string.Format(".Solutions.Solution_{0}.BottomText", id.ToString("00"));
            this.SetDataItemValue(dataItemName, text);
        }
        public void SetSolutionIsTrue(
            int id,
            bool isTrue) {
            string dataItemName = string.Format(".Solutions.Solution_{0}.IsTrue", id.ToString("00"));
            this.SetDataItemValue(dataItemName, isTrue);
        }

        public void SetChoiceText(
            int id,
            string text) {
            string dataItemName = string.Format(".Choices.Choice_{0}.Text", id.ToString("00"));
            this.SetDataItemValue(dataItemName, text);
        }
        public void ShowChoice(
            int id) {
            string dataItemName = string.Format(".Choices.Choice_{0}.Show", id.ToString("00"));
            this.SetDataItemTrigger(dataItemName);
        }
        public void HideChoice(
            int id) {
            string dataItemName = string.Format(".Choices.Choice_{0}.Hide", id.ToString("00"));
            this.SetDataItemTrigger(dataItemName);
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
