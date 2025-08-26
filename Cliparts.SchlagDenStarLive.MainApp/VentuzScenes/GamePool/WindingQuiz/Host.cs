using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.WindingQuiz {

    public class Host : _Base {

        //	[Path]=.Reset
        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.Solution
        //	[Path]=.Items.Count
        //	[Path]=.Items.StartID
        //	[Path]=.Items.HostText_1
        //	[Path]=.Items.HostText_2
        //	[Path]=.Items.HostText_3
        //	[Path]=.Items.HostText_4
        //	[Path]=.Items.HostText_5
        //	[Path]=.Items.PassNewValues
        //	[Path]=.Items.Next
        //	[Path]=.Items.Bypass.On
        //	[Path]=.Items.Bypass.Off

        #region Properties

        private const string sceneID = "project/gamepool/windingquiz/host";

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

        public void PassNewItemValues() { this.SetDataItemTrigger(".Items.PassNewValues"); }
        public void ShowNextItem() { this.SetDataItemTrigger(".Items.Next"); }
        public void SetItemValueBypass(bool on) {
            if (on) this.SetDataItemTrigger(".Items.Bypass.On");
            else this.SetDataItemTrigger(".Items.Bypass.Off");
        }

        public void SetSolution(string value) { this.SetDataItemValue(".Solution", value); }
        public void SetItemsCount(int value) { this.SetDataItemValue(".Items.Count", value); }
        public void SetItemsStartID(int value) { this.SetDataItemValue(".Items.StartID", value); }
        public void SetItemsHostText(
            int id,
            string value) {
            string name = string.Format(".Items.HostText_{0}", id.ToString());
            this.SetDataItemValue(name, value);
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

