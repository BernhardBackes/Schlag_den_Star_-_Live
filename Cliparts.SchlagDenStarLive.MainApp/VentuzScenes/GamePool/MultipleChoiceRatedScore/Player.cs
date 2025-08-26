using Cliparts.VRemote4.HandlerSi.DataItem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MultipleChoiceRatedScore {

    public class Player : VRemote4.HandlerSi.Scene, INotifyPropertyChanged {

        //	[Path]=.LeftPlayer.Name
        //	[Path]=.RightPlayer.Name
        /*
        [Path]= .SelectedPlayer (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= SelectedPlayer (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= Left (the default value for this DataItem)
            [Elements]= Left,Right (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.FlipColor
        //	[Path]=.Game.Reset
        //	[Path]=.Game.In
        //	[Path]=.Game.Out
        //	[Path]=.Game.Lock
        //	[Path]=.Game.Unlock
        //	[Path]=.Game.Text
        //	[Path]=.Game.MaxValue
        //	[Path]=.Game.Answers.Answer_A.Text
        //	[Path]=.Game.Answers.Answer_A.Solution
        //	[Path]=.Game.Answers.Answer_A.Value
        //	[Path]=.Game.Answers.Answer_B.Text
        //	[Path]=.Game.Answers.Answer_B.Solution
        //	[Path]=.Game.Answers.Answer_B.Value
        //	[Path]=.Game.Answers.Answer_C.Text
        //	[Path]=.Game.Answers.Answer_C.Solution
        //	[Path]=.Game.Answers.Answer_C.Value
        //	[Path]=.Game.Answers.Answer_D.Text
        //	[Path]=.Game.Answers.Answer_D.Solution
        //	[Path]=.Game.Answers.Answer_D.Value
        //	[Path]=.Game.OKPressed

        public enum SelectedPlayerItems { Left, Right }

        #region Properties

        private const string sceneID = "project/gamepool/multiplechoiceratedscore/player";

        private int answerAInput = 0;
        public int AnswerAInput {
            get { return this.answerAInput; }
            private set {
                this.answerAInput = value;
                this.on_PropertyChanged();
            }
        }

        private int answerBInput = 0;
        public int AnswerBInput {
            get { return this.answerBInput; }
            private set {
                this.answerBInput = value;
                this.on_PropertyChanged();
            }
        }

        private int answerCInput = 0;
        public int AnswerCInput {
            get { return this.answerCInput; }
            private set {
                this.answerCInput = value;
                this.on_PropertyChanged();
            }
        }

        private int answerDInput = 0;
        public int AnswerDInput {
            get { return this.answerDInput; }
            private set {
                this.answerDInput = value;
                this.on_PropertyChanged();
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

        private void init() {
        }

        public void SetLeftPlayerName(string value) { this.SetDataItemValue(".LeftPlayer.Name", value); }
        public void SetRightPlayerName(string value) { this.SetDataItemValue(".RightPlayer.Name", value); }
        public void SetSelectedPlayer(SelectedPlayerItems value) { this.SetDataItemValue(".SelectedPlayer", value); }
        public void SetFlipColor(bool value) { this.SetDataItemValue(".FlipColor", value); }

        public void ResetGame() { this.SetDataItemTrigger(".Game.Reset"); }
        public void GameToIn() { this.SetDataItemTrigger(".Game.In"); }
        public void GameToOut() { this.SetDataItemTrigger(".Game.Out"); }
        public void LockInput() { this.SetDataItemTrigger(".Game.Lock"); }
        public void UnlockInput() { this.SetDataItemTrigger(".Game.Unlock"); }

        public void SetText(string value) { this.SetDataItemValue(".Game.Text", value); }
        public void SetMaximumValue(int value) { this.SetDataItemValue(".Game.MaxValue", value); }

        public void SetAnswerAText(string value) { this.SetDataItemValue(".Game.Answers.Answer_A.Text", value); }
        public void SetAnswerAIsSolution(bool value) { this.SetDataItemValue(".Game.Answers.Answer_A.Solution", value); }

        public void SetAnswerBText(string value) { this.SetDataItemValue(".Game.Answers.Answer_B.Text", value); }
        public void SetAnswerBIsSolution(bool value) { this.SetDataItemValue(".Game.Answers.Answer_B.Solution", value); }

        public void SetAnswerCText(string value) { this.SetDataItemValue(".Game.Answers.Answer_C.Text", value); }
        public void SetAnswerCIsSolution(bool value) { this.SetDataItemValue(".Game.Answers.Answer_C.Solution", value); }

        public void SetAnswerDText(string value) { this.SetDataItemValue(".Game.Answers.Answer_D.Text", value); }
        public void SetAnswerDIsSolution(bool value) { this.SetDataItemValue(".Game.Answers.Answer_D.Solution", value); }

        public override void Dispose() {
            base.Dispose();
        }

        public void Clear() {
            this.GameToOut();
        }

        protected override void dataItem_TriggerReceived(object sender, ValueArgs e) {
            if (e is ValueArgs) {
                if (e.Path == ".Game.OKPressed") this.on_OKPressed(this, new EventArgs());
                else if (e.Path == ".Game.Answers.Answer_A.ValueChanged") this.AnswerAInput = Convert.ToInt32(e.Value);
                else if (e.Path == ".Game.Answers.Answer_B.ValueChanged") this.AnswerBInput = Convert.ToInt32(e.Value);
                else if (e.Path == ".Game.Answers.Answer_C.ValueChanged") this.AnswerCInput = Convert.ToInt32(e.Value);
                else if (e.Path == ".Game.Answers.Answer_D.ValueChanged") this.AnswerDInput = Convert.ToInt32(e.Value);
            }
        }

        protected override void dataItem_ValueChanged(object sender, ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is ValueArgs) {
                if (e.Path == ".Game.Answers.Answer_A.Value") this.AnswerAInput = Convert.ToInt32(e.Value);
                else if (e.Path == ".Game.Answers.Answer_B.Value") this.AnswerBInput = Convert.ToInt32(e.Value);
                else if (e.Path == ".Game.Answers.Answer_C.Value") this.AnswerCInput = Convert.ToInt32(e.Value);
                else if (e.Path == ".Game.Answers.Answer_D.Value") this.AnswerDInput = Convert.ToInt32(e.Value);
            }
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler OKPressed;
        private void on_OKPressed(object sender, EventArgs e) { Helper.raiseEvent(sender, this.OKPressed, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }
}
