using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.Fullscreen {

    public class Score : VRemote4.HandlerSi.Scene, INotifyPropertyChanged {

        //	[Path]=.FlipPosition
        //	[Path]=.Left.Name
        //	[Path]=.Left.Score
        //	[Path]=.Right.Name
        //	[Path]=.Right.Score

        #region Properties     
   
        private const string sceneID = "project/fullscreen/score";

        #endregion


        #region Funktionen

        public Score(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode = Modes.Static, string scene = sceneID)
            : base(syncContext, port, scene, mode) {
        }

        public Score(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe, string scene = sceneID)
            : base(syncContext, pipe, scene) {
        }

        public void SetLeftName(string value) { this.SetDataItemValue(".Left.Name", value); }
        public void SetLeftScore(int value) { this.SetDataItemValue(".Left.Score", value); }

        public void SetRightName(string value) { this.SetDataItemValue(".Right.Name", value); }
        public void SetRightScore(int value) { this.SetDataItemValue(".Right.Score", value); }

        public void SetFlipPosition(bool value) { this.SetDataItemValue(".FlipPosition", value); }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
