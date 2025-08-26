using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TrueOrFalseMultiple {

    public class Host : _Base {


        //	[Path]=.Reset
        //	[Path]=.ToOut
        //	[Path]=.ToIn
        //	[Path]=.SetIn
        //	[Path]=.Text.Value
        //	[Path]=.Counter.Left
        //	[Path]=.Counter.Right
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
        //	[Path]=.Answer1.Text.Color
        //	[Path]=.Answer1.Left.Color
        //	[Path]=.Answer1.Right.Color
        //	[Path]=.Answer1.Blocked
        //	[Path]=.Answer2.Text.Value
        //	...
        //	[Path]=.Answer8.Blocked

        public enum ColorValues { Neutral, Green, Red }

        #region Properties

        private const string sceneID = "project/gamepool/trueorfalsemultiple/host";

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
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
        }

        public void Reset() { 
            this.SetDataItemTrigger(".Reset"); 
        }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void SetText(string value) { this.SetDataItemValue(".Text.Value", value); }

        public void SetLeftCounter(int value) { this.SetDataItemValue(".Counter.Left", value); }
        public void SetRightCounter(int value) { this.SetDataItemValue(".Counter.Right", value); }

        private string AnswerPrefix(int id) { return $".Answer{id}"; }
        public void SetAnswerText(int id, string value) { this.SetDataItemValue($"{this.AnswerPrefix(id)}.Text.Value", value); }
        public void SetAnswerTextColor(int id, ColorValues value) { this.SetDataItemValue($"{this.AnswerPrefix(id)}.Text.Color", value); }
        public void SetLeftSelectionColor(int id, ColorValues value) { this.SetDataItemValue($"{this.AnswerPrefix(id)}.Left.Color", value); }
        public void SetRightSelectionColor(int id, ColorValues value) { this.SetDataItemValue($"{this.AnswerPrefix(id)}.Right.Color", value); }
        public void SetAnswerBlocked(int id, bool value) { this.SetDataItemValue($"{this.AnswerPrefix(id)}.Blocked", value); }

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
