using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SelectDifferences {

    public class Player : _Base {

        //	[Path]=.Reset
        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.Filename
        //	[Path]=.Solution.Filename
        //	[Path]=.Solution.Fader.Reset
        //	[Path]=.Solution.Fader.Show
        //	[Path]=.Selection.SelectionCount
        //	[Path]=.Selection.ResetAll
        //	[Path]=.Selection.SelectAll
        //	[Path]=.Selection.LockTouch
        //	[Path]=.Selection.UnlockTouch
        //	[Path]=.Selection.OKPressed
        //	[Path]=.Selection.Frame01.ResetInvoke
        //	[Path]=.Selection.Frame01.SelectInvoke
        //	[Path]=.Selection.Frame01.IsIdle
        //	[Path]=.Selection.Frame01.SetMissingInvoke
        //	[Path]=.Selection.Frame02.ResetInvoke
        //	...
        //	[Path]=.Selection.Frame20.SetMissingInvoke

        #region Properties

        private const string sceneID = "project/gamepool/selectdifferences/player";

        public const int FramesCount = 20;

        private List<bool> frameIsIdleList = new List<bool>();
        public bool[] FrameIsIdleList { get { return this.frameIsIdleList.ToArray(); } }
        
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
            while (this.frameIsIdleList.Count < FramesCount) this.frameIsIdleList.Add(true);
        }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void SetFilename(string value) { this.SetDataItemValue(".Filename", value); }

        public void ResetSolution() { this.SetDataItemTrigger(".Solution.Fader.Reset"); }
        public void ShowSolution() { this.SetDataItemTrigger(".Solution.Fader.Show"); }
        public void SetSolutionFilename(string value) { this.SetDataItemValue(".Solution.Filename", value); }

        public void ResetAllSelections() { this.SetDataItemTrigger(".Selection.ResetAll"); }
        public void SetAllSelections() { this.SetDataItemTrigger(".Selection.SelectAll"); }
        public void ResetSelection(int id) { this.SetDataItemTrigger(string.Format(".Selection.Frame{0}.ResetInvoke", id.ToString("00"))); }
        public void SetSelection(int id) { this.SetDataItemTrigger(string.Format(".Selection.Frame{0}.SelectInvoke", id.ToString("00"))); }
        public void SetSelectionMissing(int id) { this.SetDataItemTrigger(string.Format(".Selection.Frame{0}.SetMissingInvoke", id.ToString("00"))); }

        public void LockTouch() { this.SetDataItemTrigger(".Selection.LockTouch"); }
        public void UnlockTouch() { this.SetDataItemTrigger(".Selection.UnlockTouch"); }

        private void setIsIdle(
            int index,
            bool value) {
            if (index >= 0 && index < this.frameIsIdleList.Count) this.frameIsIdleList[index] = value;
        }

        public override void Dispose() {
            base.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.ToOut();
        }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Selection.OKPressed") {
                    this.syncAllDataItems();
                    this.on_OKPressed(this, new EventArgs());
                }
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs &&
                e.Value != null) {
                if (e.Path.StartsWith(".Selection.Frame") &&
                    e.Path.EndsWith(".IsIdle")) {
                    string key = e.Path.Replace(".Selection.Frame", "");
                    key = key.Replace(".IsIdle", "");
                    int id;
                    bool isIdle;
                    if (int.TryParse(key, out id) && bool.TryParse(e.Value.ToString(), out isIdle)) this.setIsIdle(id - 1, isIdle);
                }
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
