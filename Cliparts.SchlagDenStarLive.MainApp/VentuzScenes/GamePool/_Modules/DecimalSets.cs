using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class DecimalSets : _InsertBase {

        //	[Path]=.Style
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        //	[Path]=.Top.Name
        //	[Path]=.Top.Set01.Status
        //	[Path]=.Top.Set01.Value
        //	[Path]=.Top.Set01.SetBorder
        //	[Path]=.Top.Set02.Status
        //	[Path]=.Top.Set02.Value
        //	[Path]=.Top.Set02.SetBorder
        //	[Path]=.Top.Set03.Status
        //	[Path]=.Top.Set03.SetBorder
        //	[Path]=.Top.Set03.Value
        //	[Path]=.Top.Set04.Status
        //	[Path]=.Top.Set04.SetBorder
        //	[Path]=.Top.Set04.Value
        //	[Path]=.Top.Set05.Status
        //	[Path]=.Top.Set05.SetBorder
        //	[Path]=.Top.Set05.Value
        //	[Path]=.Top.Set06.Status
        //	[Path]=.Top.Set06.SetBorder
        //	[Path]=.Top.Set06.Value
        //	[Path]=.Top.Set07.Status
        //	[Path]=.Top.Set07.SetBorder
        //	[Path]=.Top.Set07.Value
        //	[Path]=.Top.Set08.Status
        //	[Path]=.Top.Set08.SetBorder
        //	[Path]=.Top.Set08.Value
        //	[Path]=.Bottom.Name
        //	[Path]=.Bottom.Set01.Status
        //	[Path]=.Bottom.Set01.Value
        //	[Path]=.Bottom.Set01.SetBorder
        //	[Path]=.Bottom.Set02.Status
        //	[Path]=.Bottom.Set02.Value
        //	[Path]=.Bottom.Set02.SetBorder
        //	[Path]=.Bottom.Set03.Status
        //	[Path]=.Bottom.Set03.Value
        //	[Path]=.Bottom.Set03.SetBorder
        //	[Path]=.Bottom.Set04.Status
        //	[Path]=.Bottom.Set04.Value
        //	[Path]=.Bottom.Set04.SetBorder
        //	[Path]=.Bottom.Set05.Status
        //	[Path]=.Bottom.Set05.Value
        //	[Path]=.Bottom.Set05.SetBorder
        //	[Path]=.Bottom.Set06.Status
        //	[Path]=.Bottom.Set06.Value
        //	[Path]=.Bottom.Set06.SetBorder
        //	[Path]=.Bottom.Set07.Status
        //	[Path]=.Bottom.Set07.Value
        //	[Path]=.Bottom.Set07.SetBorder
        //	[Path]=.Bottom.Set08.Status
        //	[Path]=.Bottom.Set08.Value
        //	[Path]=.Bottom.Set08.SetBorder

        public enum Styles { FiveSets, SixSets, SevenSets, EightSets }

        public enum ValueStates { Idle, Valid, Invalid }

        #region Properties     
   
        private const string sceneID = "project/gamepool/_modules/decimalsets";

        #endregion


        #region Funktionen

        public DecimalSets(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public DecimalSets(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetStyle(Styles value) { this.SetDataItemValue(".Style", value); }

        public void SetTopName(string value) { this.SetDataItemValue(".Top.Name", value); }
        public void SetTopValue(
            int id,
            Single value) {
            string name = string.Format(".Top.Set{0}.Value", id.ToString("00"));
            this.SetDataItemValue(name, value);
        }
        public void SetTopStatus(
            int id,
            ValueStates status) {
            string name = string.Format(".Top.Set{0}.Status", id.ToString("00"));
            this.SetDataItemValue(name, status);
        }
        public void SetTopBorder(
            int id,
            bool setIn) {
            string name = string.Format(".Top.Set{0}.SetBorder", id.ToString("00"));
            this.SetDataItemValue(name, setIn);
        }

        public void SetBottomName(string value) { this.SetDataItemValue(".Bottom.Name", value); }
        public void SetBottomValue(
            int id,
            Single value) {
            string name = string.Format(".Bottom.Set{0}.Value", id.ToString("00"));
            this.SetDataItemValue(name, value);
        }
        public void SetBottomStatus(
            int id,
            ValueStates status) {
            string name = string.Format(".Bottom.Set{0}.Status", id.ToString("00"));
            this.SetDataItemValue(name, status);
        }
        public void SetBottomBorder(
            int id,
            bool setIn) {
            string name = string.Format(".Bottom.Set{0}.SetBorder", id.ToString("00"));
            this.SetDataItemValue(name, setIn);
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
