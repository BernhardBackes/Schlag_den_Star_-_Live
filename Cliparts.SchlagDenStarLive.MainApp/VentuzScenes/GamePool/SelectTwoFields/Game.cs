using Cliparts.VRemote4.HandlerSi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SelectTwoFields
{

    public class Game : _Base {

        //	[Path]=.Position.X
        //	[Path]=.Position.Y
        //	[Path]=.Reset
        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.AllIDsOut
        //	[Path]=.Touch.EnabledOn
        //	[Path]=.Touch.EnabledOff
        //	[Path]=.Image.Visual.Uri
        /*
        [Path]= .Marker_A1.Marker.Input (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= MarkerInput (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Input (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= Out (the default value for this DataItem)
            [Elements]= Out,Yellow,Red,Red_Dashed,Blue,Blue_Dashed (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.Marker_A1.Touch.Pressed
        //	[Path]=.Marker_A1.Touch.EnabledOn
        //	[Path]=.Marker_A1.Touch.EnabledOff
        //	[Path]=.Marker_A2...
        //	[Path]=.Marker_A3...
        //	[Path]=.Marker_A4...
        //	[Path]=.Marker_A5...
        //	[Path]=.Marker_B1...
        //  ...
        //	[Path]=.Marker_E5...

        public enum FieldStatusElements { Out, Yellow, Red, Red_Dashed, Blue, Blue_Dashed }
        public enum FieldIDElements { 
            A1, A2, A3, A4, A5,
            B1, B2, B3, B4, B5,
            C1, C2, C3, C4, C5,
            D1, D2, D3, D4, D5,
            E1, E2, E3, E4, E5
        }

        #region Properties

        #endregion


        #region Funktionen

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            string sceneID,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe,
            string sceneID)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Business client,
            int pipeIndex,
            string sceneID)
            : base(syncContext, client, pipeIndex, sceneID)
        {
            this.init();
        }

        private void init() {
        }

        public void Reset() { 
            this.SetDataItemTrigger(".Reset");
            this.ResetAllFields();
        }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void SetAllIDsOut() { this.SetDataItemTrigger(".AllIDsOut"); }

        public void SetImageFilename(string value) { this.SetDataItemValue(".Image.Visual.Uri", value); }

        public void EnableTouch() { this.SetDataItemTrigger(".Touch.EnabledOn"); }
        public void DisableTouch() { this.SetDataItemTrigger(".Touch.EnabledOff"); }

        public void ResetAllFields()
        {
            this.DisableTouch();
            foreach (FieldIDElements item in Enum.GetValues(typeof(FieldIDElements))) this.SetFieldStatus(item, FieldStatusElements.Out);
        }

        private string fieldPrefix(FieldIDElements id)
        {
            return string.Format(".Marker_{0}.Marker", id.ToString());
        }
        public void SetFieldStatus(
            FieldIDElements id,
            FieldStatusElements value)
        {
            this.SetDataItemValue(string.Format("{0}.Input", this.fieldPrefix(id)), value);
        }
        public void EnableTouch(FieldIDElements id) { this.SetDataItemTrigger(string.Format("{0}.Touch.EnabledOn", this.fieldPrefix(id))); }
        public void DisableTouch(FieldIDElements id) { this.SetDataItemTrigger(string.Format("{0}.Touch.EnabledOff", this.fieldPrefix(id))); }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e)
        {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs &&
                e.Path.EndsWith(".Touch.Pressed"))
            {
                string path = e.Path.Replace(".Marker_", "").Replace(".Touch.Pressed", "");
                FieldIDElements id;
                if (Enum.TryParse(path, out id)) this.on_TouchPressed(sender, id);
            }
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler<FieldIDElements> TouchPressed;
        private void on_TouchPressed(object sender, FieldIDElements e) { Helper.raiseEvent(sender, TouchPressed, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }

}
