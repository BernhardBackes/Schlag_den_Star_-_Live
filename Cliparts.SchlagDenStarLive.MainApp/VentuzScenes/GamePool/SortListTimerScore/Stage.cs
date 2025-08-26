using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SortListTimerScore {

    public class Stage : _Base {

        //	[Path]=.Reset
        //	[Path]=.ToOut
        //	[Path]=.ToIn
        //	[Path]=.SetIn
        //	[Path]=.Tasktext
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
        //	[Path]=.Source.StageItem_01.Enum.Input
        //	[Path]=.Source.StageItem_01.State.Blocked
        //	[Path]=.Source.StageItem_01.Text.Value
        //	[Path]=.Source.StageItem_02.Enum.Input
        //	...
        //	[Path]=.Source.StageItem_08.Text.Value
        //	[Path]=.Target.StageItem_01.Enum.Input
        //	[Path]=.Target.StageItem_01.State.Blocked
        //	[Path]=.Target.StageItem_01.Text.Value
        //	[Path]=.Target.StageItem_02.Enum.Input
        //	...
        //	[Path]=.Target.StageItem_17.Text.Value

        public enum BackgroundElements { Number, Text, Wrong }

        #region Properties

        private const string sceneID = "project/gamepool/sortlisttimerscore/stage";

        #endregion


        #region Funktionen

        public Stage(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Stage(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
        }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }
        public void SetOut() { this.SetDataItemTrigger(".Reset"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void SetTaskText(string value) { this.SetDataItemValue(".TaskText", value); }

        public void SetTargetTopText(Game.TopTextElements value) { this.SetDataItemValue(".Target.TopText", value); }
        public void SetTargetBottomText(Game.BottomTextElements value) { this.SetDataItemValue(".Target.BottomText", value); }

        public void SetSourceText(int id, string value) { this.SetDataItemValue(string.Format(".Source.StageItem_{0}.Text.Value", id.ToString("00")), value); }
        public void SetSourceBackground(int id, BackgroundElements value) { this.SetDataItemValue(string.Format(".Source.StageItem_{0}.Enum.Input", id.ToString("00")), value); }
        public void SetSourceBlocked(int id, bool value) { this.SetDataItemValue(string.Format(".Source.StageItem_{0}.State.Blocked", id.ToString("00")), value); }

        public void SetTargetText(int id, string value) { this.SetDataItemValue(string.Format(".Target.StageItem_{0}.Text.Value", id.ToString("00")), value); }
        public void SetTargetBackground(int id, BackgroundElements value) { this.SetDataItemValue(string.Format(".Target.StageItem_{0}.Enum.Input", id.ToString("00")), value); }
        public void SetTargetBlocked(int id, bool value) { this.SetDataItemValue(string.Format(".Target.StageItem_{0}.State.Blocked", id.ToString("00")), value); }

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
