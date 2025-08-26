using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MultipleChoiceRatedScore {

    public class Game : _Modules._InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Text
        //	[Path]=.Answers.Solution
        //	[Path]=.Answers.Reset
        //	[Path]=.Answers.ToIn
        //	[Path]=.Answers.SetIn
        //	[Path]=.Answers.SetInput
        //	[Path]=.Answers.ToInput
        //	[Path]=.Answers.SetSolution
        //	[Path]=.Answers.ToSolution
        //	[Path]=.Answers.Answer_A.Text
        //	[Path]=.Answers.Answer_A.InputLeft
        //	[Path]=.Answers.Answer_A.InputRight
        //	[Path]=.Answers.Answer_B.Text
        //	[Path]=.Answers.Answer_B.InputLeft
        //	[Path]=.Answers.Answer_B.InputRight
        //	[Path]=.Answers.Answer_C.Text
        //	[Path]=.Answers.Answer_C.InputLeft
        //	[Path]=.Answers.Answer_C.InputRight
        //	[Path]=.Answers.Answer_D.Text
        //	[Path]=.Answers.Answer_D.InputLeft
        //	[Path]=.Answers.Answer_D.InputRight

        public enum SolutionItems { AnswerA, AnswerB, AnswerC, AnswerD }

        #region Properties

        private const string sceneID = "project/gamepool/multiplechoiceratedscore/game";

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

        public void SetText(string value) { this.SetDataItemValue(".Text", value); }
        public void SetAnswersSolution(SolutionItems value) { this.SetDataItemValue(".Answers.Solution", value); }

        public void ResetAnswers() { this.SetDataItemTrigger(".Answers.Reset"); }
        public void AnswersToIn() { this.SetDataItemTrigger(".Answers.ToIn"); }
        public void SetAnswersIn() { this.SetDataItemTrigger(".Answers.SetIn"); }

        public void SetAnswerAText(string value) { this.SetDataItemValue(".Answers.Answer_A.Text", value); }
        public void SetAnswerAInputLeft(string value) { this.SetDataItemValue(".Answers.Answer_A.InputLeft", value); }
        public void SetAnswerAInputRight(string value) { this.SetDataItemValue(".Answers.Answer_A.InputRight", value); }

        public void SetAnswerBText(string value) { this.SetDataItemValue(".Answers.Answer_B.Text", value); }
        public void SetAnswerBInputLeft(string value) { this.SetDataItemValue(".Answers.Answer_B.InputLeft", value); }
        public void SetAnswerBInputRight(string value) { this.SetDataItemValue(".Answers.Answer_B.InputRight", value); }

        public void SetAnswerCText(string value) { this.SetDataItemValue(".Answers.Answer_C.Text", value); }
        public void SetAnswerCInputLeft(string value) { this.SetDataItemValue(".Answers.Answer_C.InputLeft", value); }
        public void SetAnswerCInputRight(string value) { this.SetDataItemValue(".Answers.Answer_C.InputRight", value); }

        public void SetAnswerDText(string value) { this.SetDataItemValue(".Answers.Answer_D.Text", value); }
        public void SetAnswerDInputLeft(string value) { this.SetDataItemValue(".Answers.Answer_D.InputLeft", value); }
        public void SetAnswerDInputRight(string value) { this.SetDataItemValue(".Answers.Answer_D.InputRight", value); }

        public void AnswersToInputIn() { this.SetDataItemTrigger(".Answers.ToInput"); }
        public void SetAnswersInputIn() { this.SetDataItemTrigger(".Answers.SetInput"); }

        public void AnswersToSolutionIn() { this.SetDataItemTrigger(".Answers.ToSolution"); }
        public void SetAnswersSolutionIn() { this.SetDataItemTrigger(".Answers.SetSolution"); }

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
