using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.Reversi {

    public class Tablet : VRemote4.HandlerSi.Scene, INotifyPropertyChanged {


        /*
        [Path]= .Pressed (full path of the DataItem)
        [Description]= Occurs if Invoke method was called (the description of the DataItem)
        [Label]= Fired (the display label of the DataItem)
        [Mode]= R (read/write mode of the DataItem)
        [Name]= Pressed (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Trigger (type of the current instance)
         */

        #region Properties

        private const string sceneID = "project/gamepool/reversi/tablet";

        #endregion


        #region Funktionen

        public Tablet(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Business client,
            int pipeIndex)
            : base(syncContext, client, pipeIndex, sceneID) {
            this.init();
        }

        private void init() { }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Pressed") { this.on_FieldPressed(this, Convert.ToInt32(e.Value)); }
            }
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler<int> FieldPressed;
        private void on_FieldPressed(object sender, int e) { Helper.raiseEvent(sender, this.FieldPressed, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }

}
