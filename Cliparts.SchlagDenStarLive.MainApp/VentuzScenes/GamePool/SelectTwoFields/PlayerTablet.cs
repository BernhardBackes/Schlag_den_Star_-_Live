using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SelectTwoFields {

    public class PlayerTablet : Game
    {

        #region Properties

        private const string sceneID = "project/gamepool/selecttwofields/playertablet";

        #endregion


        #region Funktionen

        public PlayerTablet(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Business client,
            int pipeIndex)
            : base(syncContext, client, pipeIndex, sceneID)
        {
            this.init();
        }

        private void init() {
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
