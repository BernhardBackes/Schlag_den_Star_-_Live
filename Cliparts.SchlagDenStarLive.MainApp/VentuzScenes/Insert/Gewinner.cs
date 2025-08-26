using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.Insert {

    public class Gewinner : VRemote4.HandlerSi.Scene, INotifyPropertyChanged {

        #region Properties

        private const string sceneID = "project/insert/gewinner";

        #endregion


        #region Funktionen

        public Gewinner(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode = Modes.Static, 
            string scene = sceneID)
            : base(syncContext, port, scene, mode) {
        }

        public Gewinner(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe, 
            string scene = sceneID)
            : base(syncContext, pipe, scene) {
        }

        public void SetAuto(string value) { this.SetDataItemValue(".Auto", value); }
        public void SetName(string value) { this.SetDataItemValue(".Name", value); }
        public void Start() { this.SetDataItemTrigger(".Start"); }
        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
