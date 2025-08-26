using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SixHintsOneSolution {

    public class Insert : _Base {

        //	[Path]=.Position.X
        //	[Path]=.Position.Y
        //	[Path]=.Items.SetOut
        //	[Path]=.Items.ToOut
        //	[Path]=.Items.SetIn
        //	[Path]=.Items.ToIn
        //	[Path]=.Items.Item_1.Text
        //	[Path]=.Items.Item_1.SetOut
        //	[Path]=.Items.Item_1.ToOut
        //	[Path]=.Items.Item_1.SetIn
        //	[Path]=.Items.Item_1.ToIn
        //	[Path]=.Items.Item_2.Text
        //	[Path]=.Items.Item_2.SetOut
        //	[Path]=.Items.Item_2.ToOut
        //	[Path]=.Items.Item_2.SetIn
        //	[Path]=.Items.Item_2.ToIn
        //	[Path]=.Items.Item_3.Text
        //	[Path]=.Items.Item_3.SetOut
        //	[Path]=.Items.Item_3.ToOut
        //	[Path]=.Items.Item_3.SetIn
        //	[Path]=.Items.Item_3.ToIn
        //	[Path]=.Items.Item_4.Text
        //	[Path]=.Items.Item_4.SetOut
        //	[Path]=.Items.Item_4.ToOut
        //	[Path]=.Items.Item_4.SetIn
        //	[Path]=.Items.Item_4.ToIn
        //	[Path]=.Items.Item_5.Text
        //	[Path]=.Items.Item_5.SetOut
        //	[Path]=.Items.Item_5.ToOut
        //	[Path]=.Items.Item_5.SetIn
        //	[Path]=.Items.Item_5.ToIn
        //	[Path]=.Items.Item_6.Text
        //	[Path]=.Items.Item_6.SetOut
        //	[Path]=.Items.Item_6.ToOut
        //	[Path]=.Items.Item_6.SetIn
        //	[Path]=.Items.Item_6.ToIn
        //	[Path]=.Solution.Filename
        //	[Path]=.Solution.SetOut
        //	[Path]=.Solution.ToOut
        //	[Path]=.Solution.SetIn
        //	[Path]=.Solution.ToIn

        #region Properties

        private const string sceneID = "project/gamepool/sixhintsonesolution/insert";

        public _Modules.Score Score;
        public _Modules.Timeout Timeout;

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
        }

        public void SetPositionX(int value) { this.SetDataItemValue(".Position.X", value); }
        public void SetPositionY(int value) { this.SetDataItemValue(".Position.Y", value); }

        public void SetAllItemsOut() { this.SetDataItemTrigger(".Items.SetOut"); }
        public void AllItemsToOut() { this.SetDataItemTrigger(".Items.ToOut"); }
        public void SetAllItemsIn() { this.SetDataItemTrigger(".Items.SetIn"); }
        public void AllItemsToIn() { this.SetDataItemTrigger(".Items.ToIn"); }

        public void SetItemText(int id, string value) { this.SetDataItemValue(string.Format(".Items.Item_{0}.Text", id.ToString()), value); }
        public void SetItemOut(int id) { this.SetDataItemTrigger(string.Format(".Items.Item_{0}.SetOut", id.ToString())); }
        public void ItemToOut(int id) { this.SetDataItemTrigger(string.Format(".Items.Item_{0}.ToOut", id.ToString())); }
        public void SetItemIn(int id) { this.SetDataItemTrigger(string.Format(".Items.Item_{0}.SetIn", id.ToString())); }
        public void ItemToIn(int id) { this.SetDataItemTrigger(string.Format(".Items.Item_{0}.ToIn", id.ToString())); }

        public void SetSolutionFilename(string value) { this.SetDataItemValue(".Solution.Filename", value); }
        public void SetSolutionOut() { this.SetDataItemTrigger(".Solution.SetOut"); }
        public void SolutionToOut() { this.SetDataItemTrigger(".Solution.ToOut"); }
        public void SetSolutionIn() { this.SetDataItemTrigger(".Solution.SetIn"); }
        public void SolutionToIn() { this.SetDataItemTrigger(".Solution.ToIn"); }

        public override void Dispose() {
            base.Dispose();
            this.Score.Dispose();
            this.Timeout.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.Score.Clear();
            this.Timeout.Stop();
            this.AllItemsToOut();
            this.SolutionToOut();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
