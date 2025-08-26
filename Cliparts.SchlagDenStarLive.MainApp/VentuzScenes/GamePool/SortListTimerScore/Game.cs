using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SortListTimerScore {

    public class Game : _Modules._InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.SetAllIn
        //	[Path]=.Target.ToIn
        //	[Path]=.Target.SetIn
        //	[Path]=.Target.ToOut
        //	[Path]=.Target.SetOut
        /*
        [Path]= .Target.TopText (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= TopText (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= Jahresanfang (the default value for this DataItem)
            [Elements]= frueh,gross,hoch,Jahresanfang,viel,weit (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //[Path]=.Target.Text_0_1.Text.Value
        //[Path]=.Target.Text_0_1.Scaling.Reset
        //[Path]=.Target.Text_0_1.Scaling.SetOut
        //[Path]=.Target.Text_0_1.Scaling.ToOut
        //[Path]=.Target.Text_0_1.Scaling.SetIn
        //[Path]=.Target.Text_0_1.Scaling.ToIn
        //[Path]=.Target.Indicator_1.ID
        //[Path]=.Target.Indicator_1.Animation.Reset
        //[Path]=.Target.Indicator_1.Animation.SetOut
        //[Path]=.Target.Indicator_1.Animation.ToOut
        //[Path]=.Target.Indicator_1.Animation.SetIn
        //[Path]=.Target.Indicator_1.Animation.ToIn
        //[Path]=.Target.Text_1.Text.Value
        //[Path]=.Target.Text_1.Text.BackColor
        //[Path]=.Target.Text_1.Scaling.Reset
        //[Path]=.Target.Text_1.Scaling.SetOut
        //[Path]=.Target.Text_1.Scaling.ToOut
        //[Path]=.Target.Text_1.Scaling.SetIn
        //[Path]=.Target.Text_1.Scaling.ToIn
        //[Path]=.Target.Text_1_2.Text.Value
        //...
        //[Path]=.Target.Text_9.Scaling.ToIn
        /*
        [Path]= .Target.BottomText (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= BottomText (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= Jahresende (the default value for this DataItem)
            [Elements]= Jahresende,klein,kurz,niedrig,spaet,wenig,weniger_viel (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //[Path]=.Source.Text_1.Text.Value
        //[Path]=.Source.Text_1.Scaling.Reset
        //[Path]=.Source.Text_1.Scaling.SetOut
        //[Path]=.Source.Text_1.Scaling.ToOut
        //[Path]=.Source.Text_1.Scaling.SetIn
        //[Path]=.Source.Text_1.Scaling.ToIn
        //[Path]=.Source.Text_2.Text.Value
        //...
        //[Path]=.Source.Text_8.Scaling.ToIn

        public enum TopTextElements { frueh, gross, hoch, Jahresanfang, viel, weit }
        public enum BottomTextElements { Jahresende, klein, kurz, niedrig, spaet, wenig, weniger_viel }
        public enum BackColorElements { Yellow, Orange, Red }

        #region Properties

        private const string sceneID = "project/gamepool/sortlisttimerscore/game";

        #endregion


        #region Funktionen

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID, Modes.Static) {
        }

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public override void Dispose() {
            base.Dispose();
        }

        public void SetAllIn() { this.SetDataItemTrigger(".SetAllIn"); }

        public void SetSourceText(int id, string value) { this.SetDataItemValue(string.Format(".Source.Text_{0}.Text.Value", id.ToString()), value); }
        public void ResetSourceScaling(int id) { this.SetDataItemTrigger(string.Format(".Source.Text_{0}.Scaling.Reset", id.ToString())); }
        public void SetSourceScalingOut(int id) { this.SetDataItemTrigger(string.Format(".Source.Text_{0}.Scaling.SetOut", id.ToString())); }
        public void SourceScalingToOut(int id) { this.SetDataItemTrigger(string.Format(".Source.Text_{0}.Scaling.ToOut", id.ToString())); }
        public void SetSourceScalingIn(int id) { this.SetDataItemTrigger(string.Format(".Source.Text_{0}.Scaling.SetIn", id.ToString())); }
        public void SourceScalingToIn(int id) { this.SetDataItemTrigger(string.Format(".Source.Text_{0}.Scaling.ToIn", id.ToString())); }

        public void SetTargetOut() { this.SetDataItemTrigger(".Target.SetOut"); }
        public void TargetToOut() { this.SetDataItemTrigger(".Target.ToOut"); }
        public void SetTargetIn() { this.SetDataItemTrigger(".Target.SetIn"); }
        public void TargetToIn() { this.SetDataItemTrigger(".Target.ToIn"); }

        public void SetTargetTopText(TopTextElements value) { this.SetDataItemValue(".Target.TopText", value); }
        public void SetTargetBottomText(BottomTextElements value) { this.SetDataItemValue(".Target.BottomText", value); }

        public void SetTargetInterspaceText(int id, string value) {
            int preID = id - 1;
            this.SetDataItemValue(string.Format(".Target.Text_{0}_{1}.Text.Value", preID.ToString(), id.ToString()), value);
        }
        public void SetTargetInterspaceBackColor(int id, BackColorElements value) {
            int preID = id - 1;
            this.SetDataItemValue(string.Format(".Target.Text_{0}_{1}.Text.BackColor", preID.ToString(), id.ToString()), value);
        }
        public void ResetTargetInterspaceScaling(int id) {
            int preID = id - 1;
            this.SetDataItemTrigger(string.Format(".Target.Text_{0}_{1}.Scaling.Reset", preID.ToString(), id.ToString())); 
        }
        public void SetTargetInterspaceScalingOut(int id) {
            int preID = id - 1;
            this.SetDataItemTrigger(string.Format(".Target.Text_{0}_{1}.Scaling.SetOut", preID.ToString(), id.ToString()));
        }
        public void TargetInterspaceScalingToOut(int id) {
            int preID = id - 1;
            this.SetDataItemTrigger(string.Format(".Target.Text_{0}_{1}.Scaling.ToOut", preID.ToString(), id.ToString()));
        }
        public void SetTargetInterspaceScalingIn(int id) {
            int preID = id - 1;
            this.SetDataItemTrigger(string.Format(".Target.Text_{0}_{1}.Scaling.SetIn", preID.ToString(), id.ToString()));
        }
        public void TargetInterspaceScalingToIn(int id) {
            int preID = id - 1;
            this.SetDataItemTrigger(string.Format(".Target.Text_{0}_{1}.Scaling.ToIn", preID.ToString(), id.ToString()));
        }

        public void SetTargetIndicatorID(int id, int value) { this.SetDataItemValue(string.Format(".Target.Indicator_{0}.ID", id.ToString()), value); }
        public void ResetTargetIndicator(int id) { this.SetDataItemTrigger(string.Format(".Target.Indicator_{0}.Animation.Reset", id.ToString())); }
        public void SetTargetIndicatorOut(int id) { this.SetDataItemTrigger(string.Format(".Target.Indicator_{0}.Animation.SetOut", id.ToString())); }
        public void TargetIndicatorToOut(int id) { this.SetDataItemTrigger(string.Format(".Target.Indicator_{0}.Animation.ToOut", id.ToString())); }
        public void SetTargetIndicatorIn(int id) { this.SetDataItemTrigger(string.Format(".Target.Indicator_{0}.Animation.SetIn", id.ToString())); }
        public void TargetIndicatorToIn(int id) { this.SetDataItemTrigger(string.Format(".Target.Indicator_{0}.Animation.ToIn", id.ToString())); }

        public void SetTargetText(int id, string value) { this.SetDataItemValue(string.Format(".Target.Text_{0}.Text.Value", id.ToString()), value); }
        public void SetTargetBackColor(int id, BackColorElements value) { this.SetDataItemValue(string.Format(".Target.Text_{0}.Text.BackColor", id.ToString()), value); }
        public void ResetTargetScaling(int id) { this.SetDataItemTrigger(string.Format(".Target.Text_{0}.Scaling.Reset", id.ToString())); }
        public void SetTargetScalingOut(int id) { this.SetDataItemTrigger(string.Format(".Target.Text_{0}.Scaling.SetOut", id.ToString())); }
        public void TargetScalingToOut(int id) { this.SetDataItemTrigger(string.Format(".Target.Text_{0}.Scaling.ToOut", id.ToString())); }
        public void SetTargetScalingIn(int id) { this.SetDataItemTrigger(string.Format(".Target.Text_{0}.Scaling.SetIn", id.ToString())); }
        public void TargetScalingToIn(int id) { this.SetDataItemTrigger(string.Format(".Target.Text_{0}.Scaling.ToIn", id.ToString())); }

        public void PlayJingle(string name) {
            string dataItemName = string.Format(".Jingles.{0}.Play", name);
            this.SetDataItemTrigger(dataItemName);
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler Completed;

        private void on_Completed(object sender, EventArgs e) { Helper.raiseEvent(sender, this.Completed, e); }

        #endregion

        #region Events.Incoming

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Text.Completed") this.on_Completed(this, new EventArgs());
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs &&
                e.Value != null) {
                //if (e.Path == ".Audio.Duration") { this.Duration = Convert.ToInt32(e.Value); }
                //else if (e.Path == ".Audio.Remaining") { this.Remaining = Convert.ToInt32(e.Value); }
            }
        }

        #endregion

    }
}
