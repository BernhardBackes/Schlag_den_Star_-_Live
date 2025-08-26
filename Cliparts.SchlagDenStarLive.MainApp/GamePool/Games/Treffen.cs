namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Treffen : Templates.TimeToBeatSpeedKick.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Treffen()
            : base("Treffen") {

            this.TimeToBeatPositionX = 0;
            this.TimeToBeatPositionY = 0;
            this.TimeToBeatStopTime = 59999;
            this.TimeToBeatStyle = VentuzScenes.GamePool._Modules.TimeToBeat.Styles.Stopwatch;
            this.ShowNextPanel = false;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
