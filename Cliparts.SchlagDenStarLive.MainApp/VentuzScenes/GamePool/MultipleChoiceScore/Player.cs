using Cliparts.VRemote4.HandlerSi.DataItem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MultipleChoiceScore {

    public class Player : VRemote4.HandlerSi.Scene, INotifyPropertyChanged {


        //	[Path]=.Reset
        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.Text
        //	[Path]=.Answers.Answer_A.Text
        //	[Path]=.Answers.Answer_A.Pressed
        //	[Path]=.Answers.Answer_B.Text
        //	[Path]=.Answers.Answer_B.Pressed
        //	[Path]=.Answers.Answer_C.Text
        //	[Path]=.Answers.Answer_C.Pressed
        //	[Path]=.Answers.Reset
        //	[Path]=.Touch.Unlock
        //	[Path]=.Touch.Lock

        #region Properties

        private const string sceneID = "project/gamepool/multiplechoicescore/player";

        private Host.SelectionItems input = Host.SelectionItems.NA;
        public Host.SelectionItems Input {
            get { return this.input; }
            set {
                if (this.input != value) {
                    this.input = value;
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
        public void ToIn() {
            this.Input = Host.SelectionItems.NA;
            this.SetDataItemTrigger(".ToIn"); 
        }

        public void SetText(string value) { this.SetDataItemValue(".Text", value); }
        public void SetAnswerA(string value) { this.SetDataItemValue(".Answers.Answer_A.Text", value); }
        public void SetAnswerB(string value) { this.SetDataItemValue(".Answers.Answer_B.Text", value); }
        public void SetAnswerC(string value) { this.SetDataItemValue(".Answers.Answer_C.Text", value); }
        public void ResetAnswer() {
            this.Input = Host.SelectionItems.NA;
            this.SetDataItemTrigger(".Answers.Reset"); 
        }

        public void EnableTouch() {
            this.Input = Host.SelectionItems.NA;
            this.SetDataItemTrigger(".Touch.Unlock"); 
        }
        public void DisableTouch() { this.SetDataItemTrigger(".Touch.Lock"); }

        public override void Dispose() {
            base.Dispose();
        }

        public void Clear() {
            this.ToOut();
        }

        protected override void dataItem_TriggerReceived(object sender, ValueArgs e) {
            if (e is ValueArgs) {
                if (e.Path == ".Answers.Answer_A.Pressed") this.Input = Host.SelectionItems.AnswerA;
                else if (e.Path == ".Answers.Answer_B.Pressed") this.Input = Host.SelectionItems.AnswerB;
                else if (e.Path == ".Answers.Answer_C.Pressed") this.Input = Host.SelectionItems.AnswerC;
            }
        }

        protected override void dataItem_ValueChanged(object sender, ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
