using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.ListRefersToImageTimerScore {

    public class Stage : _Base {

        //	[Path]=.Reset
        //	[Path]=.ToOut
        //	[Path]=.ToIn
        //	[Path]=.SetIn
        //	[Path]=.Headline
        //	[Path]=.PictureFilename
        /*
        [Path]= .Style (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Style (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= Grid (the default value for this DataItem)
            [Elements]= Grid,List (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.Items.Count
        //	[Path]=.Items.Item_1.Text
        //	[Path]=.Items.Item_1.Idle
        //	[Path]=.Items.Item_2.Text
        //  ...
        //	[Path]=.Items.Item_18.Idle

        public enum HostStyles { Grid, List }

        #region Properties

        private const string sceneID = "project/gamepool/listreferstoimagetimerscore/stage";

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

        public void SetHeadline(string value) { this.SetDataItemValue(".Headline", value); }

        public void SetPictureFilename(string value) { this.SetDataItemValue(".PictureFilename", value); }

        public void SetStyle(HostStyles value) { this.SetDataItemValue(".Style", value); }

        public void SetItemsCount(int value) { this.SetDataItemValue(".Items.Count", value); }

        public void SetItemText(
            int id,
            string value) {
            string name = string.Format(".Items.Item_{0}.Text", id.ToString());
            this.SetDataItemValue(name, value);
        }

        public void SetItemIdle(
            int id,
            bool value) {
            string name = string.Format(".Items.Item_{0}.Idle", id.ToString());
            this.SetDataItemValue(name, value);
        }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }
        public void SetOut() { this.SetDataItemTrigger(".Reset"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public override void Dispose() {
            base.Dispose();
        }

        public override void Clear() {
            this.ToOut();
            base.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion


    }
}
