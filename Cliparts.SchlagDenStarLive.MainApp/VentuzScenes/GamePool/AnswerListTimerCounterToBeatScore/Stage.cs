using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.AnswerListTimerCounterToBeatScore
{

    public class Stage : _Base {

        //	[Path]=.HostText
        //	[Path]=.Headline
        //	[Path]=.Reset
        //	[Path]=.ToOut
        //	[Path]=.ToIn
        //	[Path]=.SetIn
        //	[Path]=.PanelsCount
        //	[Path]=.ResetAllPanels
        //	[Path]=.SetAllPanelsIn
        //	[Path]=.SetAllPanelsYellow
        //	[Path]=.SetAllPanelsGrey
        //	[Path]=.Panel_01.Value.Text
        //	[Path]=.Panel_01.Event.SetGrey
        //	[Path]=.Panel_01.Event.ToGrey
        //	[Path]=.Panel_01.Event.ToYellow
        //	[Path]=.Panel_01.Event.SetYellow
        //	[Path]=.Panel_01.Event.ToIn
        //	[Path]=.Panel_01.Event.SetIn
        //	[Path]=.Panel_01.Event.ToOut
        //	[Path]=.Panel_01.Event.Reset
        //	[Path]=.Panel_02.Value.Text
        //  ...
        //	[Path]=.Panel_36.Event.Reset

        #region Properties

        private const string sceneID = "project/gamepool/answerlisttimercountertobeatscore/stage";

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

        public void SetHostText(string value) {
            this.SetDataItemValue(".HostText", value);
            this.SetDataItemValue(".Headline", string.Empty);
        }

        public void SetHeadline(string value) {
            this.SetDataItemValue(".HostText", string.Empty);
            this.SetDataItemValue(".Headline", value); 
        }

        public void SetPanelsCount(int value) { this.SetDataItemValue(".PanelsCount", value); }

        public void SetPanelText(
            int id,
            string value) {
            string name = string.Format(".Panel_{0}.Value.Text", id.ToString("00"));
            this.SetDataItemValue(name, value);
        }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }
        public void SetOut() { this.SetDataItemTrigger(".Reset"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void ResetAllPanels() { this.SetDataItemTrigger(".ResetAllPanels"); }
        public void SetAllPanelsIn() { this.SetDataItemTrigger(".SetAllPanelsIn"); }
        public void AllPanelsToIn() { this.SetDataItemTrigger(".AllPanelsToIn"); }
        public void SetAllPanelsYellow() { this.SetDataItemTrigger(".SetAllPanelsYellow"); }
        public void AllPanelsToYellow() { this.SetDataItemTrigger(".AllPanelsToYellow"); }
        public void SetAllPanelsGrey() { this.SetDataItemTrigger(".SetAllPanelsGrey"); }
        public void AllPanelsToGrey() { this.SetDataItemTrigger(".AllPanelsToGrey"); }

        public void ResetPanel(
            int id) {
            string name = string.Format(".Panel_{0}.Event.Reset", id.ToString("00"));
            this.SetDataItemTrigger(name);
        }
        public void PanelToOut(
            int id) {
            string name = string.Format(".Panel_{0}.Event.ToOut", id.ToString("00"));
            this.SetDataItemTrigger(name);
        }
        public void SetPanelIn(
            int id) {
            string name = string.Format(".Panel_{0}.Event.SetIn", id.ToString("00"));
            this.SetDataItemTrigger(name);
        }
        public void PanelToIn(
            int id) {
            string name = string.Format(".Panel_{0}.Event.ToIn", id.ToString("00"));
            this.SetDataItemTrigger(name);
        }
        public void SetPanelYellow(
            int id) {
            string name = string.Format(".Panel_{0}.Event.SetYellow", id.ToString("00"));
            this.SetDataItemTrigger(name);
        }
        public void PanelToYellow(
            int id) {
            string name = string.Format(".Panel_{0}.Event.ToYellow", id.ToString("00"));
            this.SetDataItemTrigger(name);
        }
        public void SetPanelGrey(
            int id) {
            string name = string.Format(".Panel_{0}.Event.SetGrey", id.ToString("00"));
            this.SetDataItemTrigger(name);
        }
        public void PanelToGrey(
            int id) {
            string name = string.Format(".Panel_{0}.Event.ToGrey", id.ToString("00"));
            this.SetDataItemTrigger(name);
        }

        public override void Dispose() {
            base.Dispose();
        }

        public override void Clear() {
            this.ToOut();
            base.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion


    }
}
