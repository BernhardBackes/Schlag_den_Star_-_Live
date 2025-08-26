using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class SportIcons : _InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Basketball.SetOut
        //	[Path]=.Basketball.ToOut
        //	[Path]=.Basketball.SetIn
        //	[Path]=.Basketball.ToIn
        //	[Path]=.Hockey.SetOut
        //	[Path]=.Hockey.ToOut
        //	[Path]=.Hockey.SetIn
        //	[Path]=.Hockey.ToIn
        //	[Path]=.Can.SetOut
        //	[Path]=.Can.ToOut
        //	[Path]=.Can.SetIn
        //	[Path]=.Can.ToIn
        //	[Path]=.Soccer.SetOut
        //	[Path]=.Soccer.ToOut
        //	[Path]=.Soccer.SetIn
        //	[Path]=.Soccer.ToIn

        public enum States { In, Out }

        #region Properties     
   
        private const string sceneID = "project/gamepool/_modules/sporticons";

        #endregion


        #region Funktionen

        public SportIcons(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public SportIcons(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetBasketball(States status) {
            if (status == States.In) this.SetDataItemTrigger(".Basketball.SetIn");
            else this.SetDataItemTrigger(".Basketball.SetOut");
        }
        public void BasketballTo(States status) {
            if (status == States.In) this.SetDataItemTrigger(".Basketball.ToIn");
            else this.SetDataItemTrigger(".Basketball.ToOut");
        }

        public void SetHockey(States status) {
            if (status == States.In) this.SetDataItemTrigger(".Hockey.SetIn");
            else this.SetDataItemTrigger(".Hockey.SetOut");
        }
        public void HockeyTo(States status) {
            if (status == States.In) this.SetDataItemTrigger(".Hockey.ToIn");
            else this.SetDataItemTrigger(".Hockey.ToOut");
        }

        public void SetCan(States status) {
            if (status == States.In) this.SetDataItemTrigger(".Can.SetIn");
            else this.SetDataItemTrigger(".Can.SetOut");
        }
        public void CanTo(States status) {
            if (status == States.In) this.SetDataItemTrigger(".Can.ToIn");
            else this.SetDataItemTrigger(".Can.ToOut");
        }

        public void SetSoccer(States status) {
            if (status == States.In) this.SetDataItemTrigger(".Soccer.SetIn");
            else this.SetDataItemTrigger(".Soccer.SetOut");
        }
        public void SoccerTo(States status) {
            if (status == States.In) this.SetDataItemTrigger(".Soccer.ToIn");
            else this.SetDataItemTrigger(".Soccer.ToOut");
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
