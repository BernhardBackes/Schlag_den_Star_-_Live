using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SelectQuestionScore {

    public enum SolutionStates { AnswerA, AnswerB }
    public enum SelectionStates { NotAvailable, AnswerA, AnswerB }

    public class Game : _Modules._InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Preview.Value
        //	[Path]=.Question_01.Text.Value
        //	[Path]=.Question_01.Reset.Invoke
        //	[Path]=.Question_01.ToOut.Invoke
        //	[Path]=.Question_01.SetIn.Invoke
        //	[Path]=.Question_01.ToIn.Invoke
        //	[Path]=.Question_01.SetSelected.Invoke
        //	[Path]=.Question_01.ToSelected.Invoke
        //	[Path]=.Question_01.SetGreen.Invoke
        //	[Path]=.Question_01.ToGreen.Invoke
        //	[Path]=.Question_01.SetRed.Invoke
        //	[Path]=.Question_01.ToRed.Invoke
        //	[Path]=.Question_01.ToNext.Invoke
        //	[Path]=.Question_02.Text.Value
        //	...
        //	[Path]=.Question_07.ToRed.Invoke
        //  [Path]=.Jingle.PlayTrue
        //  [Path]=.Jingle.PlayFalse

        #region Properties

        private const string sceneID = "project/gamepool/selectquestionscore/game";

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

        public void SetPreview(bool value) { this.SetDataItemValue(".Text.Value", value); }

        public void SetQuestionText(
            int id,
            string value) {
            string name = string.Format("{0}.Text.Value", this.questionPrefix(id));
            this.SetDataItemValue(name, value);
        }
        public void ResetQuestion(
            int id) {
            string name = string.Format("{0}.Reset.Invoke", this.questionPrefix(id));
            this.SetDataItemTrigger(name);
        }
        public void QuestionToOut(
            int id) {
            string name = string.Format("{0}.ToOut.Invoke", this.questionPrefix(id));
            this.SetDataItemTrigger(name);
        }
        public void SetQuestionIn(
            int id) {
            string name = string.Format("{0}.SetIn.Invoke", this.questionPrefix(id));
            this.SetDataItemTrigger(name);
        }
        public void QuestionToIn(
            int id) {
            string name = string.Format("{0}.ToIn.Invoke", this.questionPrefix(id));
            this.SetDataItemTrigger(name);
        }
        public void SetQuestionSelected(
            int id) {
            string name = string.Format("{0}.SetSelected.Invoke", this.questionPrefix(id));
            this.SetDataItemTrigger(name);
        }
        public void QuestionToSelected(
            int id) {
            string name = string.Format("{0}.ToSelected.Invoke", this.questionPrefix(id));
            this.SetDataItemTrigger(name);
        }
        public void SetQuestionGreen(
            int id) {
            string name = string.Format("{0}.SetGreen.Invoke", this.questionPrefix(id));
            this.SetDataItemTrigger(name);
        }
        public void QuestionToGreen(
            int id) {
            string name = string.Format("{0}.ToGreen.Invoke", this.questionPrefix(id));
            this.SetDataItemTrigger(name);
        }
        public void SetQuestionRed(
            int id) {
            string name = string.Format("{0}.SetRed.Invoke", this.questionPrefix(id));
            this.SetDataItemTrigger(name);
        }
        public void QuestionToRed(
            int id) {
            string name = string.Format("{0}.ToRed.Invoke", this.questionPrefix(id));
            this.SetDataItemTrigger(name);
        }
        public void QuestionToNext(
            int id,
            string value) {
            this.SetQuestionText(id, value);
            string name = string.Format("{0}.ToNext.Invoke", this.questionPrefix(id));
            this.SetDataItemTrigger(name);
        }

        private string questionPrefix(int id) { return string.Format(".Question_{0}", id.ToString("00")); }

        public void PlayJingleTrue() { this.SetDataItemTrigger(".Jingle.PlayTrue"); }
        public void PlayJingleFalse() { this.SetDataItemTrigger(".Jingle.PlayFalse"); }


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
