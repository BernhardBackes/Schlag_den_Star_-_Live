namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class KurbelautoArena : Templates.TimeToBeatSpeedKick.Business {

        #region Properties
        #endregion


        #region Funktionen

        public KurbelautoArena()
            : base("Kurbelauto-Arena") {

            this.TimeToBeatPositionX = 0;
            this.TimeToBeatPositionY = 0;
            this.TimeToBeatStopTime = 59999;
            this.TimeToBeatStyle = VentuzScenes.GamePool._Modules.TimeToBeat.Styles.Stopwatch;

        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
