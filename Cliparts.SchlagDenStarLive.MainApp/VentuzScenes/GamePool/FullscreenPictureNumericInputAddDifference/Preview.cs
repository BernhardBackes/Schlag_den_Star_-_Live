using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.FullscreenPictureNumericInputAddDifference {

    public class Preview : _PreviewBase {

        /*
        [Path]= .Source (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Source (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= Content (the default value for this DataItem)
            [Elements]= Content,Host,Player (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        /*
        [Path]= .ShowSafeArea (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Value (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= ShowSafeArea (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Boolean (type of the current instance)
            [Default]= False (the default value for this DataItem)
            [PropertyType]= System.Boolean (the underlaying system type of this DataItem)
         */

        public enum Sources { Content, Host, Player };

        #region Properties

        private const string sceneID = "project/gamepool/fullscreenpicturenumericinputadddifference/preview";

        public Fullscreen Fullscreen;
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
            this.Fullscreen = new Fullscreen(this.syncContext, this.addPort("FullscreenLayer"), Modes.Static);
            this.Insert = new Insert(this.syncContext, this.addPort("InsertLayer"), Modes.Static);
            this.Host = new Host(this.syncContext, this.addPort("HostLayer"), Modes.Static);
            this.Player = new Player(this.syncContext, this.addPort("PlayerLayer"), Modes.Static);
        }

        public void SetSource(Sources value) { this.SetDataItemValue(".Source", value); }

        public override void Dispose() {
            base.Dispose();
            this.Fullscreen.Dispose();
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
