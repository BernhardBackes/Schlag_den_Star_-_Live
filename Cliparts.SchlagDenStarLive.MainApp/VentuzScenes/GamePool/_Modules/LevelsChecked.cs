using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class LevelsChecked : _InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Top.Name
        //	[Path]=.Top.Checked01
        //	[Path]=.Top.Checked02
        //	[Path]=.Top.Checked03
        //	[Path]=.Top.Checked04
        //	[Path]=.Top.Checked05
        //	[Path]=.Top.Checked06
        //	[Path]=.Top.Checked07
        //	[Path]=.Top.Checked08
        //	[Path]=.Top.Checked10
        //	[Path]=.Top.Checked09
        //	[Path]=.Bottom.Name
        //	[Path]=.Bottom.Checked01
        //	[Path]=.Bottom.Checked02
        //	[Path]=.Bottom.Checked03
        //	[Path]=.Bottom.Checked04
        //	[Path]=.Bottom.Checked05
        //	[Path]=.Bottom.Checked06
        //	[Path]=.Bottom.Checked07
        //	[Path]=.Bottom.Checked08
        //	[Path]=.Bottom.Checked09
        //	[Path]=.Bottom.Checked10

        #region Properties     

        private const string sceneID = "project/gamepool/_modules/levelschecked";

        #endregion


        #region Funktionen

        public LevelsChecked(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public LevelsChecked(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetTopName(string value) { this.SetDataItemValue(".Top.Name", value); }
        public void SetTopDot(
            int id,
            bool isChecked) {
            string name = string.Format(".Top.Checked{0}", id.ToString("00"));
            this.SetDataItemValue(name, isChecked);
        }

        public void SetBottomName(string value) { this.SetDataItemValue(".Bottom.Name", value); }
        public void SetBottomDot(
            int id,
            bool isChecked) {
            string name = string.Format(".Bottom.Checked{0}", id.ToString("00"));
            this.SetDataItemValue(name, isChecked);
        }

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
