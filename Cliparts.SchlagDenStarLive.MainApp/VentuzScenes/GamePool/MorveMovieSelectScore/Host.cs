using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MorveMovieSelectScore {

    public class Host : _Base {


        //	[Path]=.Reset
        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.Filename
        //	[Path]=.BorderPosition
        //	[Path]=.LeftText
        //	[Path]=.RightText
        //	[Path]=.LeftSelection
        //	[Path]=.RightSelection
        /*
        [Path]= .RightSelection (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= RightSelection (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= NotSelected (the default value for this DataItem)
            [Elements]= NotSelected,LeftPlayer,RightPlayer,Both (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */

        public enum BorderPositions { Left, Right }

        public enum SelectionValues { NotSelected, LeftPlayer, RightPlayer, Both }

        #region Properties

        private const string sceneID = "project/gamepool/morvemovieselectscore/host";

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

        public void SetFilename(string value) { this.SetDataItemValue(".Filename", value); }
        public void SetBorderPosition(BorderPositions value) { this.SetDataItemValue(".BorderPosition", value); }
        public void SetHostText(string value) { this.SetDataItemValue(".HostText", value); }
        public void SetLeftText(string value) { this.SetDataItemValue(".LeftText", value); }
        public void SetRightText(string value) { this.SetDataItemValue(".RightText", value); }
        public void SetLeftSelection(SelectionValues value) { this.SetDataItemValue(".LeftSelection", value); }
        public void SetRightSelection(SelectionValues value) { this.SetDataItemValue(".RightSelection", value); }

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
