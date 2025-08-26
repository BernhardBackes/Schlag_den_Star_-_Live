using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TrueOrFalseMultiple {

    public class Player : _Base {

        //	[Path]=.Reset
        //	[Path]=.ToOut
        //	[Path]=.ToIn
        //	[Path]=.SetIn
        //	[Path]=.Text.Value
        //	[Path]=.Answer1.Text.Value
        /*
        [Path]= .Answers.Answer1.Text.Color (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Color (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Color (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= Neutral (the default value for this DataItem)
            [Elements]= Neutral,Green,Red (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.Answer1.Selection.Reset
        //	[Path]=.Answer1.Selection.Lock
        //	[Path]=.Answer1.Selection.SetTrue
        //	[Path]=.Answer1.Selection.SetFalse
        //	[Path]=.Answer1.Selection.TrueFired
        //	[Path]=.Answer1.Selection.FalseFired
        //	[Path]=.Answer1.Blocked
        //	[Path]=.Answer2.Text.Value
        //	...
        //	[Path]=.Answer8.Blocked
        //	[Path]=.CountDown.Start
        //	[Path]=.CountDown.Reset

        public enum ColorValues { Neutral, Green, Red }

        #region Properties

        private const string sceneID = "project/gamepool/trueorfalsemultiple/player";

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
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
        }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void SetText(string value) { this.SetDataItemValue(".Text.Value", value); }

        private string AnswerPrefix(int id) { return $".Answer{id}"; }
        public void SetAnswerText(int id, string value) { this.SetDataItemValue($"{this.AnswerPrefix(id)}.Text.Value", value); }
        public void SetAnswerTextColor(int id, ColorValues value) { this.SetDataItemValue($"{this.AnswerPrefix(id)}.Text.Color", value); }
        public void ResetAnswer(int id) { this.SetDataItemTrigger($"{this.AnswerPrefix(id)}.Selection.Reset"); }
        public void LockAnswer(int id) { this.SetDataItemTrigger($"{this.AnswerPrefix(id)}.Selection.Lock"); }
        public void SetAnswerTrue(int id) { this.SetDataItemTrigger($"{this.AnswerPrefix(id)}.Selection.SetTrue"); }
        public void SetAnswerFalse(int id) { this.SetDataItemTrigger($"{this.AnswerPrefix(id)}.Selection.SetFalse"); }
        public void SetAnswerBlocked(int id, bool value) { this.SetDataItemValue($"{this.AnswerPrefix(id)}.Blocked", value); }

        public void StartCountDown() { this.SetDataItemTrigger(".CountDown.Start"); }
        public void ResetCountDown(int duration) { this.SetDataItemTrigger(".CountDown.Reset, duration"); }

        public override void Dispose() {
            base.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.ToOut();
        }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs)
            {
                if (e.Path.EndsWith("TrueFired")) this.TrueFired?.Invoke(sender, this.GetIDFromPath(e.Path));
                else if (e.Path.EndsWith("FalseFired")) this.FalseFired?.Invoke(sender, this.GetIDFromPath(e.Path));
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs)
            {
            }
        }

        private string GetIDFromPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) return string.Empty;
            else
            {
                string[] s = path.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                if (s.Length > 0) return s[0].Replace("Answer", "");
                else return string.Empty;
            }
        }


        #endregion


        #region Events.Outgoing

        public event EventHandler<string> TrueFired;

        public event EventHandler<string> FalseFired;

        #endregion

        #region Events.Incoming
        #endregion

    }
}
