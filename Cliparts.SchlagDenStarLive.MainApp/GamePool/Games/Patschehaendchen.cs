namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Patschehaendchen : Templates.CounterSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Patschehaendchen()
            : base("Patschehändchen") {
            this.CounterStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
            this.CounterPositionX = 0;
            this.CounterPositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.SevenCrosses;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
