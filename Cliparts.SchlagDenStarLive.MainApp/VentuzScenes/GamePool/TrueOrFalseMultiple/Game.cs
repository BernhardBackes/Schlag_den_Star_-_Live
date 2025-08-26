using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TrueOrFalseMultiple {

    public enum SelectionValues { NotAvailable, True, False }

    public class Game : _Modules._InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Text.Value
        //	[Path]=.Answers.Reset
        //	[Path]=.Answers.ToOut
        //	[Path]=.Answers.Answer1.SetIn
        //	[Path]=.Answers.Answer1.ToIn
        //	[Path]=.Answers.Answer1.Text.Value
        /*
        [Path]= .Answers.Answer1.Text.Color (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Color (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Color (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= Neutral (the default value for this DataItem)
            [Elements]= Neutral,Green,Red,Countdown (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.Answers.Answer1.SelectionLeft.Color
        //	[Path]=.Answers.Answer1.SelectionRight.Color
        //	[Path]=.Answers.Answer2.SetIn
        //	...
        //	[Path]=.Answers.Answer8.SelectionRight.Color
        //	[Path]=.CountDown.Start
        //	[Path]=.CountDown.Reset
        //	[Path]=.CountDown.ToOut
        //	[Path]=.CountDown.ToIn
        //	[Path]=.PlaySound.Wusch
        //	[Path]=.PlaySound.Plopp
        //	[Path]=.PlaySound.Pling

        public enum ColorValues { Neutral, Green, Red, Countdown }
        public enum SoundValues { Pling, Plopp, Wusch }

        public static int ItemsCount = 8;

        #region Properties

        private const string sceneID = "project/gamepool/trueorfalsemultiple/game";

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

        public void SetText(string value) { this.SetDataItemValue(".Text.Value", value); }

        public void ResetAnswers() { this.SetDataItemTrigger(".Answers.Reset"); }
        public void AnswersToOut() { this.SetDataItemTrigger(".Answers.ToOut"); }

        private string AnswerPrefix(int id) { return $".Answers.Answer{id}"; }
        public void SetAnswerIn(int id) { this.SetDataItemTrigger($"{this.AnswerPrefix(id)}.SetIn"); }
        public void AnswerToIn(int id) { this.SetDataItemTrigger($"{this.AnswerPrefix(id)}.ToIn"); }
        public void SetAnswerText(int id, string value) { this.SetDataItemValue($"{this.AnswerPrefix(id)}.Text.Value", value); }
        public void SetAnswerTextColor(int id, ColorValues value) { this.SetDataItemValue($"{this.AnswerPrefix(id)}.Text.Color", value); }
        public void SetLeftSelectionColor(int id, ColorValues value) { this.SetDataItemValue($"{this.AnswerPrefix(id)}.SelectionLeft.Color", value); }
        public void SetRightSelectionColor(int id, ColorValues value) { this.SetDataItemValue($"{this.AnswerPrefix(id)}.SelectionRight.Color", value); }

        public void StartCountDown(int duration) { this.SetDataItemTrigger(".CountDown.Start", duration); }
        public void ResetCountDown() { this.SetDataItemTrigger(".CountDown.Reset"); }
        public void CountDownToOut() { this.SetDataItemTrigger(".CountDown.ToOut"); }
        public void CountDownToIn() { this.SetDataItemTrigger(".CountDown.ToIn"); }

        public void PlaySound(SoundValues value) { this.SetDataItemTrigger($".PlaySound.{value}"); }

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
