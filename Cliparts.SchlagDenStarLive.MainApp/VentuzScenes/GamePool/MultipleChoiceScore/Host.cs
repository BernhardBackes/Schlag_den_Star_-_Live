using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MultipleChoiceScore {

    public class Host : VRemote4.HandlerSi.Scene, INotifyPropertyChanged {

        //	[Path]=.Reset
        //	[Path]=.Out
        //	[Path]=.In
        //	[Path]=.Text
        //	[Path]=.Answers.Answer_A.Text
        //	[Path]=.Answers.Answer_A.IsSolution
        //	[Path]=.Answers.Answer_B.Text
        //	[Path]=.Answers.Answer_B.IsSolution
        //	[Path]=.Answers.Answer_C.Text
        //	[Path]=.Answers.Answer_C.IsSolution
        /*
        [Path]= .LeftPlayerSelection.Input (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Input (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= NA (the default value for this DataItem)
            [Elements]= NA,AnswerA,AnswerB,AnswerC (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.RightPlayerSelection.Input

        #region Properties

        public enum SelectionItems { NA, AnswerA, AnswerB, AnswerC }

        private const string sceneID = "project/gamepool/multiplechoicescore/host";

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
            VRemote4.HandlerSi.Client.Business client,
            int pipeIndex)
            : base(syncContext, client, pipeIndex, sceneID) {
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

        public void SetAnswerAText(string value) { this.SetDataItemValue(".Answers.Answer_A.Text", value); }
        public void SetAnswerBText(string value) { this.SetDataItemValue(".Answers.Answer_B.Text", value); }
        public void SetAnswerCText(string value) { this.SetDataItemValue(".Answers.Answer_C.Text", value); }

        public void SetSolution(Game.AnswerItems value) {
            switch (value) {
                case Game.AnswerItems.AnswerA:
                    this.SetDataItemValue(".Answers.Answer_A.IsSolution", true);
                    this.SetDataItemValue(".Answers.Answer_B.IsSolution", false);
                    this.SetDataItemValue(".Answers.Answer_C.IsSolution", false);
                    break;
                case Game.AnswerItems.AnswerB:
                    this.SetDataItemValue(".Answers.Answer_A.IsSolution", false);
                    this.SetDataItemValue(".Answers.Answer_B.IsSolution", true);
                    this.SetDataItemValue(".Answers.Answer_C.IsSolution", false);
                    break;
                case Game.AnswerItems.AnswerC:
                    this.SetDataItemValue(".Answers.Answer_A.IsSolution", false);
                    this.SetDataItemValue(".Answers.Answer_B.IsSolution", false);
                    this.SetDataItemValue(".Answers.Answer_C.IsSolution", true);
                    break;
            }
        }

        public void SetLeftPlayerInput(SelectionItems value) { this.SetDataItemValue(".LeftPlayerSelection.Input", value); }
        public void SetRightPlayerInput(SelectionItems value) { this.SetDataItemValue(".RightPlayerSelection.Input", value); }

        public override void Dispose() {
            base.Dispose();
        }

        public void Clear() {
            this.SetDataItemTrigger(".Clear");
            this.ToOut();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
