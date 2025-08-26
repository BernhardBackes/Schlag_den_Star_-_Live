using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MultipleChoiceScore {

    public class Game : _Modules._InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Text
        /*
        [Path]= .Answers.Solution (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Solution (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= AnswerA (the default value for this DataItem)
            [Elements]= AnswerA,AnswerB,AnswerC (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.Answers.Reset
        //	[Path]=.Answers.ToIn
        //	[Path]=.Answers.SetIn
        //	[Path]=.Answers.SetInput
        //	[Path]=.Answers.ToInput
        //	[Path]=.Answers.SetSolution
        //	[Path]=.Answers.ToSolution
        //	[Path]=.Answers.Answer_A.Text
        //	[Path]=.Answers.Answer_B.Text
        //	[Path]=.Answers.Answer_C.Text
        /*
        [Path]= .LeftPlayerSelection.Input (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Input (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= AnswerA (the default value for this DataItem)
            [Elements]= AnswerA,AnswerB,AnswerC (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.RightPlayerSelection.Input

        public enum AnswerItems { AnswerA, AnswerB, AnswerC }

        #region Properties

        private const string sceneID = "project/gamepool/multiplechoicescore/game";

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

        public void SetAnswerAText(string value) { this.SetDataItemValue(".Answers.Answer_A.Text", value); }
        public void SetAnswerBText(string value) { this.SetDataItemValue(".Answers.Answer_B.Text", value); }
        public void SetAnswerCText(string value) { this.SetDataItemValue(".Answers.Answer_C.Text", value); }
        public void ResetAnswers() { this.SetDataItemTrigger(".Answers.Reset"); }
        public void AnswersToIn() { this.SetDataItemTrigger(".Answers.ToIn"); }
        public void SetAnswersIn() { this.SetDataItemTrigger(".Answers.SetIn"); }


        public void SetLeftPlayerInput(Host.SelectionItems value) { this.SetDataItemValue(".LeftPlayerSelection.Input", value); }
        public void SetRightPlayerInput(Host.SelectionItems value) { this.SetDataItemValue(".RightPlayerSelection.Input", value); }
        public void AnswersToInputIn() { this.SetDataItemTrigger(".Answers.ToInput"); }
        public void SetAnswersInputIn() { this.SetDataItemTrigger(".Answers.SetInput"); }

        public void SetAnswersSolution(AnswerItems value) { this.SetDataItemValue(".Answers.Solution", value); }
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
