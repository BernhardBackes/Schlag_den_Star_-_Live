using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class PenaltyScore : _InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Size
        //	[Path]=.Top.Name
        //	[Path]=.Top.Dot_01
        //	[Path]=.Top.Dot_02
        //	[Path]=.Top.Dot_03
        //	[Path]=.Top.Dot_04
        //	[Path]=.Top.Dot_05
        //	[Path]=.Top.Dot_06
        //	[Path]=.Top.Dot_07
        //	[Path]=.Top.Dot_08
        //	[Path]=.Top.Dot_09
        //	[Path]=.Top.Dot_10
        //	[Path]=.Top.Score
        //	[Path]=.Bottom.Name
        //	[Path]=.Bottom.Dot_01
        //	[Path]=.Bottom.Dot_02
        //	[Path]=.Bottom.Dot_03
        //	[Path]=.Bottom.Dot_04
        //	[Path]=.Bottom.Dot_05
        //	[Path]=.Bottom.Dot_06
        //	[Path]=.Bottom.Dot_07
        //	[Path]=.Bottom.Dot_08
        //	[Path]=.Bottom.Dot_09
        //	[Path]=.Bottom.Dot_10
        //	[Path]=.Bottom.Score

        public enum DotStates { Off, Green, Red }

        #region Properties     
   
        private const string sceneID = "project/gamepool/_modules/penaltyscore";

        #endregion


        #region Funktionen

        public PenaltyScore(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public PenaltyScore(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetSize(int value) { this.SetDataItemValue(".Size", value); }

        public void SetTopName(string value) { this.SetDataItemValue(".Top.Name", value); }
        public void SetTopDot(
            int id,
            DotStates status) {
            string name = string.Format(".Top.Dot_{0}", id.ToString("00"));
            this.SetDataItemValue(name, status);
        }
        public void SetTopScore(int value) { this.SetDataItemValue(".Top.Score", value); }

        public void SetBottomName(string value) { this.SetDataItemValue(".Bottom.Name", value); }
        public void SetBottomDot(
            int id,
            DotStates status) {
            string name = string.Format(".Bottom.Dot_{0}", id.ToString("00"));
            this.SetDataItemValue(name, status);
        }
        public void SetBottomScore(int value) { this.SetDataItemValue(".Bottom.Score", value); }

        public override void Dispose() {
            base.Dispose();
            this.Reset();
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
