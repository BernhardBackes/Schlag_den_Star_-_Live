using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TextInsertTeamInputScore {

    public class Host : _Base {

        //	[Path]=.Reset
        //	[Path]=.ToOut
        //	[Path]=.ToIn
        //	[Path]=.SetIn
        //	[Path]=.Headline
        //	[Path]=.Input.LeftTeamTop
        //	[Path]=.Input.LeftTeamBottom
        //	[Path]=.Input.RightTeamTop  
        //	[Path]=.Input.RightTeamBottom  

        #region Properties

        private const string sceneID = "project/gamepool/textinsertteaminputscore/host";

        #endregion


        #region Funktionen

        public Host(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Host(
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

        public void SetHeadline(string value) { this.SetDataItemValue(".Headline", value); }
        public void SetLeftTeamInputTop(string value) { this.SetDataItemValue(".Input.LeftTeamTop", value); }
        public void SetLeftTeamInputBottom(string value) { this.SetDataItemValue(".Input.LeftTeamBottom", value); }
        public void SetRightTeamInputTop(string value) { this.SetDataItemValue(".Input.RightTeamTop", value); }
        public void SetRightTeamInputBottom(string value) { this.SetDataItemValue(".Input.RightTeamBottom", value); }

        public override void Dispose() {
            base.Dispose();
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
