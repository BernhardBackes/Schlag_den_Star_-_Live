using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.Select1PictureOf4 {

    //	[Path]=.Content.PictureFilename
    //	[Path]=.Content.SolutionFilename
    //	[Path]=.Content.Fader.Reset
    //	[Path]=.Content.Fader.SetOut
    //	[Path]=.Content.Fader.ToOut
    //	[Path]=.Content.Fader.SetIn
    //	[Path]=.Content.Fader.ToIn
    //	[Path]=.Content.Fader.SetSolutionOut
    //	[Path]=.Content.Fader.SolutionToIn
    //	[Path]=.Content.Border.Clear
    //	[Path]=.Content.Border.Set_1
    //	[Path]=.Content.Border.Select_1
    //	[Path]=.Content.Border.Set_2
    //	[Path]=.Content.Border.Select_2
    //	[Path]=.Content.Border.Set_3
    //	[Path]=.Content.Border.Select_3
    //	[Path]=.Content.Border.Set_4
    //	[Path]=.Content.Border.Select_4
    //	[Path]=.Content.Score.Left
    //	[Path]=.Content.Score.Right
    //	[Path]=.Content.Score.Style
    //	[Path]=.Content.Buzzer.Reset
    //	[Path]=.Content.Buzzer.Out
    //	[Path]=.Content.Buzzer.Left
    //	[Path]=.Content.Buzzer.Right
    //	[Path]=.Content.Credits.Text
    //	[Path]=.Content.Credits.Show

    public class Insert : _Base {
        
        public enum Styles { FourDots, FiveDots, SixDots, Counter }

        #region Properties

        private const string sceneID = "project/gamepool/select1pictureof4/insert";

        public _Modules.Score Score;
        public _Modules.Timeout Timeout;
        public _Modules.TaskCounter TaskCounter;

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
            this.Timeout = new _Modules.Timeout(syncContext, this.addPort("TimeoutLayer"));
            this.TaskCounter = new _Modules.TaskCounter(syncContext, this.addPort("TaskCounterLayer"));
        }

        public override void Dispose() {
            base.Dispose();
            this.Score.Dispose();
            this.Timeout.Dispose();
            this.TaskCounter.Dispose();
        }

        public void SetPictureFilename(string value) { this.SetDataItemValue(".Content.PictureFilename", value); }
        public void SetSolutionFilename(string value) { this.SetDataItemValue(".Content.SolutionFilename", value); }

        public void SetScoreStyle(Styles value) { this.SetDataItemValue(".Content.Score.Style", value); }
        public void SetLeftScore(int value) { 
            this.SetDataItemValue(".Content.Score.Left", value);
            this.Score.SetLeftTopScore(value);
        }
        public void SetRightScore(int value) {
            this.SetDataItemValue(".Content.Score.Right", value);
            this.Score.SetRightBottomScore(value);
        }

        public void SetCreditsText(string value) { this.SetDataItemValue(".Content.Credits.Text", value); }

        public void Reset() { this.SetDataItemTrigger(".Content.Fader.Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".Content.Fader.SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".Content.Fader.ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".Content.Fader.SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".Content.Fader.ToIn"); }
        public void SetSolutionOut() { this.SetDataItemTrigger(".Content.Fader.SetSolutionOut"); }
        public void SolutionToIn() { this.SetDataItemTrigger(".Content.Fader.SolutionToIn"); }

        public void Deselect() { this.SetDataItemTrigger(".Content.Border.Clear"); }
        public void SetBorder(int borderID) {
            if (borderID > 0 &&
                borderID <= 4) {
                string name = string.Format(".Content.Border.Set_{0}", borderID.ToString());
                this.SetDataItemTrigger(name);
            }
            else this.Deselect();
        }
        public void Select(int borderID) {
            if (borderID > 0 &&
                borderID <= 4) {
                string name = string.Format(".Content.Border.Select_{0}", borderID.ToString());
                this.SetDataItemTrigger(name);
            }
            else this.Deselect();
        }

        public void ResetBuzzer() { this.SetDataItemTrigger(".Content.Buzzer.Reset"); }
        public void SetBuzzerOut() { this.SetDataItemTrigger(".Content.Buzzer.Out"); }
        public void BuzzerLeft() { this.SetDataItemTrigger(".Content.Buzzer.Left"); }
        public void BuzzerRight() { this.SetDataItemTrigger(".Content.Buzzer.Right"); }

        public void ShowCredits() { this.SetDataItemTrigger(".Content.Credits.Show"); }

        public override void Clear() {
            base.Clear();
            this.Timeout.Stop();
            this.Score.Clear();
            this.TaskCounter.Clear();
            this.ToOut();
            this.Deselect();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
