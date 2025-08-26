using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SelectDifferences {

    public class Insert : _Base {


        //	[Path]=.Style
        //	[Path]=.LeftScore
        //	[Path]=.RightScore
        //	[Path]=.Reset
        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.Filename
        //	[Path]=.Solution.Filename
        //	[Path]=.Solution.Fader.Reset
        //	[Path]=.Solution.Fader.Show
        //	[Path]=.Buzzer.Reset
        //	[Path]=.Buzzer.SetLeft
        //	[Path]=.Buzzer.Left
        //	[Path]=.Buzzer.SetRight
        //	[Path]=.Buzzer.Right
        //	[Path]=.Selection.ResetAll
        //	[Path]=.Selection.SelectAll
        //	[Path]=.Selection.Frame01.ResetInvoke
        //	[Path]=.Selection.Frame01.SelectInvoke
        //	[Path]=.Selection.Frame01.SetMissingInvoke
        //	[Path]=.Selection.Frame02.ResetInvoke
        //	...
        //	[Path]=.Selection.Frame20.SetMissingInvoke


        public enum Style { FiveDots, SixDots }

        #region Properties

        private const string sceneID = "project/gamepool/selectdifferences/insert";

        public _Modules.Score Score;

        #endregion


        #region Funktionen

        public Insert(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Insert(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
            this.Score = new _Modules.Score(syncContext, this.addPort("ScoreLayer"));
        }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void SetStyle(Style value) { this.SetDataItemValue(".Style", value); }

        public void SetLeftScore(int value) { this.SetDataItemValue(".LeftScore", value); }
        public void SetRightScore(int value) { this.SetDataItemValue(".RightScore", value); }

        public void ResetBuzzer() { this.SetDataItemTrigger(".Buzzer.Reset"); }
        public void SetBuzzerLeft() { this.SetDataItemTrigger(".Buzzer.SetLeft"); }
        public void SetBuzzerRight() { this.SetDataItemTrigger(".Buzzer.SetRight"); }
        public void BuzzerLeft() { this.SetDataItemTrigger(".Buzzer.Left"); }
        public void BuzzerRight() { this.SetDataItemTrigger(".Buzzer.Right"); }

        public void SetFilename(string value) { this.SetDataItemValue(".Filename", value); }

        public void ResetSolution() { this.SetDataItemTrigger(".Solution.Fader.Reset"); }
        public void SetSolution() { this.SetDataItemTrigger(".Solution.Fader.Set"); }
        public void ShowSolution() { this.SetDataItemTrigger(".Solution.Fader.Show"); }
        public void SetSolutionFilename(string value) { this.SetDataItemValue(".Solution.Filename", value); }

        public void ResetAllSelections() { this.SetDataItemTrigger(".Selection.ResetAll"); }
        public void SetAllSelections() { this.SetDataItemTrigger(".Selection.SelectAll"); }
        public void ResetSelection(int id) { this.SetDataItemTrigger(string.Format(".Selection.Frame{0}.ResetInvoke", id.ToString("00"))); }
        public void SetSelection(int id) { this.SetDataItemTrigger(string.Format(".Selection.Frame{0}.SelectInvoke", id.ToString("00"))); }
        public void SetSelectionMissing(int id) { this.SetDataItemTrigger(string.Format(".Selection.Frame{0}.SetMissingInvoke", id.ToString("00"))); }

        public override void Dispose() {
            base.Dispose();
            this.Reset();
            this.Score.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.ToOut();
            this.Score.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
