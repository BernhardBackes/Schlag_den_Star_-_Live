using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TrueOrFalseMultiple {

    public class Preview : _PreviewBase {


        /*
        [Path]= .Source (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Source (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= Host (the default value for this DataItem)
            [Elements]= Insert,Host,Player (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.ShowSafeArea

        public enum Sources { Insert, Host, Player };

        #region Properties

        private const string sceneID = "project/gamepool/trueorfalsemultiple/preview";

        public Insert Insert;
        public Host Host;
        public Player Player;

        #endregion


        #region Funktionen

        public Preview(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
            this.Insert = new Insert(this.syncContext, this.addPort("InsertLayer"), Modes.Static);
            this.Host = new Host(this.syncContext, this.addPort("HostLayer"), Modes.Static);
            this.Player = new Player(this.syncContext, this.addPort("PlayerLayer"), Modes.Static);
        }

        public void SetSource(Sources value) { this.SetDataItemValue(".Source", value); }


        public override void Dispose() {
            base.Dispose();
            this.Insert.Dispose();
            this.Host.Dispose();
            this.Player.Dispose();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
