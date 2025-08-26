using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.ListRefersToImageTimerScore {

    public class InsertList : _Modules._InsertBase {

        /*
        [Path]= .Style (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Style (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= Columns (the default value for this DataItem)
            [Elements]= Columns,Tree (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.Position.X
        //	[Path]=.Position.Y
        //	[Path]=.Position.XCorrection
        //	[Path]=.Position.YCorrection
        //	[Path]=.Reset
        //	[Path]=.ToOut
        //	[Path]=.ToIn
        //	[Path]=.SetIn
        //	[Path]=.Items.Count
        //	[Path]=.Items.Item_01.Text
        //	[Path]=.Items.Item_01.Idle
        //	[Path]=.Items.Item_02.Text
        //	...
        //	[Path]=.Items.Item_18.Idle
        //	[Path]=.Jingles.Select.Play
        //	[Path]=.Jingles.Wrong.Play

        public enum Styles { Columns, Tree }

        #region Properties

        private const string sceneID = "project/gamepool/listreferstoimagetimerscore/insertlist";

        #endregion


        #region Funktionen

        public InsertList(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public InsertList(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetXCorrection(int value) { this.SetDataItemValue(".Position.XCorrection", value); }
        public void SetYCorrection(int value) { this.SetDataItemValue(".Position.YCorrection", value); }

        public void SetStyle(Styles value) { this.SetDataItemValue(".Style", value); }

        public void SetItemsCount(int value) { this.SetDataItemValue(".Items.Count", value); }

        public void SetItemText(
            int id,
            string value) {
            string name = string.Format(".Items.Item_{0}.Text", id.ToString("00"));
            this.SetDataItemValue(name, value);
        }
        public void SetItemIdle(
            int id,
            bool value) {
            string name = string.Format(".Items.Item_{0}.Idle", id.ToString("00"));
            this.SetDataItemValue(name, value);
        }

        public void PlayJingle(string name) {
            string dataItemName = string.Format(".Jingles.{0}.Play", name);
            this.SetDataItemTrigger(dataItemName);
        }

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
