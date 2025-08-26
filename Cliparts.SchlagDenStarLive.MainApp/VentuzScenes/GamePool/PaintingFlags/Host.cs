using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.PaintingFlags
{

    public class Host : _Base
    {


        //	[Path]=.Reset
        //	[Path]=.ToOut
        //	[Path]=.ToIn
        //	[Path]=.SetIn
        //	[Path]=.Text
        //	[Path]=.Filename
        /*
        [Path]= .Orientation (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Orientation (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= Vertical (the default value for this DataItem)
            [Elements]= Horizontal,Vertical (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.BuzzeredPlayer
        //	[Path]=.LeftPlayer.FlagColor1
        //	[Path]=.LeftPlayer.FlagColor2
        //	[Path]=.LeftPlayer.FlagColor3
        //	[Path]=.RightPlayer.FlagColor1
        //	[Path]=.RightPlayer.FlagColor2
        //	[Path]=.RightPlayer.FlagColor3

        #region Properties

        private const string sceneID = "project/gamepool/paintingflags/host";

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

        public void SetText(string value) { this.SetDataItemValue(".Text", value); }

        public void SetFilename(string value) { this.SetDataItemValue(".Filename", value); }

        public void SetOrientation(OrientationStates value) { this.SetDataItemValue(".Orientation", value); }

        public void SetBuzzeredPlayer(Content.Gameboard.PlayerSelection value) { this.SetDataItemValue(".BuzzeredPlayer", value); }

        public void SetLeftPlayerFlagColor(int id, ColorStates value) { this.SetDataItemValue($".LeftPlayer.FlagColor{id}", Convert.ToInt32(value)); }

        public void SetRightPlayerFlagColor(int id, ColorStates value) { this.SetDataItemValue($".RightPlayer.FlagColor{id}", Convert.ToInt32(value)); }

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
