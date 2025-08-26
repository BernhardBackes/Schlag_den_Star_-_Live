using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TeamCorrelation
{

    public class Host : _Base
    {
        //	[Path]=.Reset
        //	[Path]=.ToOut
        //	[Path]=.ToIn
        //	[Path]=.SetIn
        //	[Path]=.Headline
        //	[Path]=.Tasks.Task1.Value
        //	[Path]=.Tasks.Task2.Value
        //	[Path]=.Tasks.Task3.Value
        //	[Path]=.Tasks.Task4.Value
        //	[Path]=.RightTeam.Inside.Name.Value
        //	[Path]=.RightTeam.Inside.Input1.Value
        //	[Path]=.RightTeam.Inside.Input2.Value
        //	[Path]=.RightTeam.Inside.Input3.Value
        //	[Path]=.RightTeam.Inside.Input4.Value
        //	[Path]=.RightTeam.Outside.Name.Value
        //	[Path]=.RightTeam.Outside.Input1.Value
        //	[Path]=.RightTeam.Outside.Input2.Value
        //	[Path]=.RightTeam.Outside.Input3.Value
        //	[Path]=.RightTeam.Outside.Input4.Value
        //	[Path]=.LeftTeam.Inside.Name.Value
        //	[Path]=.LeftTeam.Inside.Input1.Value
        //	[Path]=.LeftTeam.Inside.Input2.Value
        //	[Path]=.LeftTeam.Inside.Input3.Value
        //	[Path]=.LeftTeam.Inside.Input4.Value
        //	[Path]=.LeftTeam.Outside.Name.Value
        //	[Path]=.LeftTeam.Outside.Input1.Value
        //	[Path]=.LeftTeam.Outside.Input2.Value
        //	[Path]=.LeftTeam.Outside.Input3.Value
        //	[Path]=.LeftTeam.Outside.Input4.Value

        #region Properties

        private const string sceneID = "project/gamepool/teamcorrelation/host";

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

        public void SetTask(
            int id,
            string value)
        {
            this.SetDataItemValue($".Tasks.Task{id.ToString()}.Value", value);
        }

        public void SetLeftTeamInsideName(string value) { this.SetDataItemValue(".LeftTeam.Inside.Name.Value", value); }
        public void SetLeftTeamInsideInput(
            int id,
            string value)
        {
            this.SetDataItemValue($".LeftTeam.Inside.Input{id.ToString()}.Value", value);
        }

        public void SetLeftTeamOutsideName(string value) { this.SetDataItemValue(".LeftTeam.Outside.Name.Value", value); }
        public void SetLeftTeamOutsideInput(
            int id,
            string value)
        {
            this.SetDataItemValue($".LeftTeam.Outside.Input{id.ToString()}.Value", value);
        }

        public void SetRightTeamInsideName(string value) { this.SetDataItemValue(".RightTeam.Inside.Name.Value", value); }
        public void SetRightTeamInsideInput(
            int id,
            string value)
        {
            this.SetDataItemValue($".RightTeam.Inside.Input{id.ToString()}.Value", value);
        }

        public void SetRightTeamOutsideName(string value) { this.SetDataItemValue(".RightTeam.Outside.Name.Value", value); }
        public void SetRightTeamOutsideInput(
            int id,
            string value)
        {
            this.SetDataItemValue($".RightTeam.Outside.Input{id.ToString()}.Value", value);
        }

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
