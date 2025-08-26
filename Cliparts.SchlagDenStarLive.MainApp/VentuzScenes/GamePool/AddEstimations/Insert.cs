using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.AddEstimations {

    public class Insert : _Base {

        //	[Path]=.Content.Position.X
        //	[Path]=.Content.Position.Y
        //	[Path]=.Content.Reset
        //	[Path]=.Content.SetOut
        //	[Path]=.Content.ToOut
        //	[Path]=.Content.SetIn
        //	[Path]=.Content.ToIn
        //	[Path]=.Content.Questions.Text_1
        //	[Path]=.Content.Questions.Text_2
        //	[Path]=.Content.Questions.Text_3
        //	[Path]=.Content.Solutions.SetOut
        //	[Path]=.Content.Solutions.ToOut
        //	[Path]=.Content.Solutions.SetIn
        //	[Path]=.Content.Solutions.ToIn
        //	[Path]=.Content.Solutions.Amount_1
        //	[Path]=.Content.Solutions.Amount_2
        //	[Path]=.Content.Solutions.Amount_3
        //	[Path]=.Content.Solutions.Sum
        //	[Path]=.Content.LeftAnswers.Name
        //	[Path]=.Content.LeftAnswers.Amount_1
        //	[Path]=.Content.LeftAnswers.Amount_2
        //	[Path]=.Content.LeftAnswers.Amount_3
        //	[Path]=.Content.LeftAnswers.Sum
        //	[Path]=.Content.LeftAnswers.Offset.Amount
        //	[Path]=.Content.LeftAnswers.Offset.SetOut
        //	[Path]=.Content.LeftAnswers.Offset.ToOut
        //	[Path]=.Content.LeftAnswers.Offset.SetIn
        //	[Path]=.Content.LeftAnswers.Offset.ToIn
        //	[Path]=.Content.LeftAnswers.Border.SetOut
        //	[Path]=.Content.LeftAnswers.Border.ToOut
        //	[Path]=.Content.LeftAnswers.Border.SetIn
        //	[Path]=.Content.LeftAnswers.Border.ToIn
        //	[Path]=.Content.RightAnswers.Name
        //	[Path]=.Content.RightAnswers.Amount_1
        //	[Path]=.Content.RightAnswers.Amount_2
        //	[Path]=.Content.RightAnswers.Amount_3
        //	[Path]=.Content.RightAnswers.Sum
        //	[Path]=.Content.RightAnswers.Offset.Amount
        //	[Path]=.Content.RightAnswers.Offset.SetOut
        //	[Path]=.Content.RightAnswers.Offset.ToOut
        //	[Path]=.Content.RightAnswers.Offset.SetIn
        //	[Path]=.Content.RightAnswers.Offset.ToIn
        //	[Path]=.Content.RightAnswers.Border.SetOut
        //	[Path]=.Content.RightAnswers.Border.ToOut
        //	[Path]=.Content.RightAnswers.Border.SetIn
        //	[Path]=.Content.RightAnswers.Border.ToIn

        #region Properties

        private const string sceneID = "project/gamepool/addestimations/insert";

        public _Modules.Score Score;
        public _Modules.TextInsert TextInsert;


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
            this.TextInsert = new _Modules.TextInsert(syncContext, this.addPort("TextInsertLayer"));
        }

        public void SetContentPositionX(int value) { this.SetDataItemValue(".Content.Position.X", value); }
        public void SetContentPositionY(int value) { this.SetDataItemValue(".Content.Position.Y", value); }

        public void ResetContent() { this.SetDataItemTrigger(".Content.Reset"); }

        public void SetContentOut() { this.SetDataItemTrigger(".Content.SetOut"); }
        public void ContentToOut() { this.SetDataItemTrigger(".Content.ToOut"); }
        public void SetContentIn() { this.SetDataItemTrigger(".Content.SetIn"); }
        public void ContentToIn() { this.SetDataItemTrigger(".Content.ToIn"); }

        public void SetContentQuestionText(int id, string value) { this.SetDataItemValue(string.Format(".Content.Questions.Text_{0}", id.ToString()), value); }

        public void SetContentSolutionAmount(int id, string value) { this.SetDataItemValue(string.Format(".Content.Solutions.Amount_{0}", id.ToString()), value); }
        public void SetContentSolutionSum(string value) { this.SetDataItemValue(".Content.Solutions.Sum", value); }
        public void SetContentSolutionsOut() { this.SetDataItemTrigger(".Content.Solutions.SetOut"); }
        public void ContentSolutionsToOut() { this.SetDataItemTrigger(".Content.Solutions.ToOut"); }
        public void SetContentSolutionsIn() { this.SetDataItemTrigger(".Content.Solutions.SetIn"); }
        public void ContentSolutionsToIn() { this.SetDataItemTrigger(".Content.Solutions.ToIn"); }

        public void SetContentLeftAnswersName(string value) { this.SetDataItemValue(".Content.LeftAnswers.Name", value); }
        public void SetContentLeftAnswersAmount(int id, string value) { this.SetDataItemValue(string.Format(".Content.LeftAnswers.Amount_{0}", id.ToString()), value); }
        public void SetContentLeftAnswersSum(string value) { this.SetDataItemValue(".Content.LeftAnswers.Sum", value); }
        public void SetContentLeftAnswersOffsetAmount(string value) { this.SetDataItemValue(".Content.LeftAnswers.Offset.Amount", value); }
        public void SetContentLeftAnswersOffsetOut() { this.SetDataItemTrigger(".Content.LeftAnswers.Offset.SetOut"); }
        public void ContentLeftAnswersOffsetToOut() { this.SetDataItemTrigger(".Content.LeftAnswers.Offset.ToOut"); }
        public void SetContentLeftAnswersOffsetIn() { this.SetDataItemTrigger(".Content.LeftAnswers.Offset.SetIn"); }
        public void ContentLeftAnswersOffsetToIn() { this.SetDataItemTrigger(".Content.LeftAnswers.Offset.ToIn"); }
        public void SetContentLeftAnswersBorderOut() { this.SetDataItemTrigger(".Content.LeftAnswers.Border.SetOut"); }
        public void ContentLeftAnswersBorderToOut() { this.SetDataItemTrigger(".Content.LeftAnswers.Border.ToOut"); }
        public void SetContentLeftAnswersBorderIn() { this.SetDataItemTrigger(".Content.LeftAnswers.Border.SetIn"); }
        public void ContentLeftAnswersBorderToIn() { this.SetDataItemTrigger(".Content.LeftAnswers.Border.ToIn"); }

        public void SetContentRightAnswersName(string value) { this.SetDataItemValue(".Content.RightAnswers.Name", value); }
        public void SetContentRightAnswersAmount(int id, string value) { this.SetDataItemValue(string.Format(".Content.RightAnswers.Amount_{0}", id.ToString()), value); }
        public void SetContentRightAnswersSum(string value) { this.SetDataItemValue(".Content.RightAnswers.Sum", value); }
        public void SetContentRightAnswersOffsetAmount(string value) { this.SetDataItemValue(".Content.RightAnswers.Offset.Amount", value); }
        public void SetContentRightAnswersOffsetOut() { this.SetDataItemTrigger(".Content.RightAnswers.Offset.SetOut"); }
        public void ContentRightAnswersOffsetToOut() { this.SetDataItemTrigger(".Content.RightAnswers.Offset.ToOut"); }
        public void SetContentRightAnswersOffsetIn() { this.SetDataItemTrigger(".Content.RightAnswers.Offset.SetIn"); }
        public void ContentRightAnswersOffsetToIn() { this.SetDataItemTrigger(".Content.RightAnswers.Offset.ToIn"); }
        public void SetContentRightAnswersBorderOut() { this.SetDataItemTrigger(".Content.RightAnswers.Border.SetOut"); }
        public void ContentRightAnswersBorderToOut() { this.SetDataItemTrigger(".Content.RightAnswers.Border.ToOut"); }
        public void SetContentRightAnswersBorderIn() { this.SetDataItemTrigger(".Content.RightAnswers.Border.SetIn"); }
        public void ContentRightAnswersBorderToIn() { this.SetDataItemTrigger(".Content.RightAnswers.Border.ToIn"); }

        public override void Dispose() {
            base.Dispose();
            this.Score.Dispose();
            this.TextInsert.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.Score.Clear();
            this.TextInsert.Clear();
            this.ContentToOut();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
