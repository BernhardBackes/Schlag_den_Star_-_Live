using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SelectDifferences4x4 {

    //	[Path]=.Reset
    //	[Path]=.SetOut
    //	[Path]=.ToOut
    //	[Path]=.SetIn
    //	[Path]=.ToIn
    //	[Path]=.Filename
    //	[Path]=.Solution.Filename
    //	[Path]=.Solution.Fader.Reset
    //	[Path]=.Solution.Fader.Show
    //	[Path]=.Selection.ResetAll
    //	[Path]=.Selection.SelectAll
    //	[Path]=.Selection.Frame01.ResetInvoke
    //	[Path]=.Selection.Frame01.SelectInvoke
    //	[Path]=.Selection.Frame01.SetMissingInvoke
    //	[Path]=.Selection.Frame02.ResetInvoke
    //	...
    //	[Path]=.Selection.Frame16.SetMissingInvoke

    public class Fullscreen : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/selectdifferences4x4/fullscreen";

        #endregion


        #region Funktionen

        public Fullscreen(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Fullscreen(
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

        public void SetFilename(string value) { this.SetDataItemValue(".Filename", value); }

        public void ResetSolution() { this.SetDataItemTrigger(".Solution.Fader.Reset"); }
        public void ShowSolution() { this.SetDataItemTrigger(".Solution.Fader.Show"); }
        public void SetSolutionFilename(string value) { this.SetDataItemValue(".Solution.Filename", value); }

        public void ResetAllSelections() { this.SetDataItemTrigger(".Selection.ResetAll"); }
        public void SetAllSelections() { this.SetDataItemTrigger(".Selection.SelectAll"); }
        public void ResetSelection(int id) { this.SetDataItemTrigger(string.Format(".Selection.Frame{0}.ResetInvoke", id.ToString("00"))); }
        public void SetSelection(int id) { this.SetDataItemTrigger(string.Format(".Selection.Frame{0}.SelectInvoke", id.ToString("00"))); }
        public void SetSelectionMissing(int id) { this.SetDataItemTrigger(string.Format(".Selection.Frame{0}.SetMissingInvoke", id.ToString("00"))); }

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
