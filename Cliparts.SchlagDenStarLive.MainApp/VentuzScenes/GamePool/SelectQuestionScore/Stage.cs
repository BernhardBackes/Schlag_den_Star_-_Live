using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SelectQuestionScore {

    public class Stage : _Base {


        //	[Path]=.Reset
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.Question_01.Text.Value
        //	[Path]=.Question_01.Reset.Invoke
        //	[Path]=.Question_01.Select.Invoke
        //	[Path]=.Question_01.Block.Invoke
        //	[Path]=.Question_02.Text.Value
        //	...
        //	[Path]=.Question_07.Block.Invoke

        #region Properties

        private const string sceneID = "project/gamepool/selectquestionscore/stage";

        #endregion


        #region Funktionen

        public Stage(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Stage(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
        }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

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
        public void SelectQuestion(
            int id) {
            string name = string.Format("{0}.Select.Invoke", this.questionPrefix(id));
            this.SetDataItemTrigger(name);
        }
        public void BlockQuestion(
            int id) {
            string name = string.Format("{0}.Block.Invoke", this.questionPrefix(id));
            this.SetDataItemTrigger(name);
        }

        private string questionPrefix(int id) { return string.Format(".Question_{0}", id.ToString("00")); }

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
