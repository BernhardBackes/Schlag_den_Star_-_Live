using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MorveMovieSelectScore {

    public class Player : _Base {

        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.TaskText
        //	[Path]=.Selection.EnableTouch
        //	[Path]=.Selection.DisableTouch
        //	[Path]=.Selection.Deselect
        //	[Path]=.Selection.SelectLeft
        //	[Path]=.Selection.SelectRight
        /*
        [Path]= .Selection.Value (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Value (the display label of the DataItem)
        [Mode]= R (read/write mode of the DataItem)
        [Name]= Value (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.String (type of the current instance)
            [Default]= NotSelected (the default value for this DataItem)
            [MaxLines]=  (the maximum number of lines for this String. A value of null does not define any value.)
            [MinLines]=  (the minimum number of lines for this String. A value of null does not define any value.)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
            [RegEx]=  (a regular expression used to validate the value of this String)
         */
        //	[Path]=.Selection.OnValueChanged

        public enum SelectionValues { NotSelected, Left, Right }

        #region Properties

        private const string sceneID = "project/gamepool/morvemovieselectscore/player";

        private SelectionValues? selection = null;
        public SelectionValues Selection {
            get {
                if (this.selection.HasValue) return this.selection.Value;
                else return SelectionValues.NotSelected;
            }
            private set {
                if (!this.selection.HasValue ||
                    this.selection != value) {
                    this.selection = value;
                    this.on_PropertyChanged();
                    if (value != SelectionValues.NotSelected) this.on_SelectionMade(this, value);
                }
            }
        }

        #endregion


        #region Funktionen

        public Player(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Player(
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

        public void EnableTouch() { this.SetDataItemTrigger(".Selection.EnableTouch"); }
        public void DisableTouch() { this.SetDataItemTrigger(".Selection.DisableTouch"); }
        public void Deselect() { this.SetDataItemTrigger(".Selection.Deselect"); }
        public void SelectLeft() { this.SetDataItemTrigger(".Selection.SelectLeft"); }
        public void SelectRight() { this.SetDataItemTrigger(".Selection.SelectRight"); }

        public void SetTaskText(string value) { this.SetDataItemValue(".TaskText", value); }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Selection.OnValueChanged") {
                    try { this.Selection = (SelectionValues)e.Value; }
                    catch (Exception) { }
                }
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs &&
                e.Value != null) {
                if (e.Path == ".Selection.Value") {
                    SelectionValues selection;
                    if (Enum.TryParse(e.Value.ToString(), out selection)) this.Selection = selection;
                }
            }
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

        public event EventHandler<SelectionValues> SelectionMade;
        private void on_SelectionMade(object sender, SelectionValues e) { Helper.raiseEvent(sender, this.SelectionMade, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }
}
