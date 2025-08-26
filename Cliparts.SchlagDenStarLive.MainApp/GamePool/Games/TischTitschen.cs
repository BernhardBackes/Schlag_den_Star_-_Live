namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class TischTitschen : Templates.CounterSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public TischTitschen()
            : base("Tisch-Titschen") {
            this.CounterPositionX = 0;
            this.CounterPositionY = 0;
            this.CounterStyle = VentuzScenes.GamePool._Modules.Score.Styles.SevenColoredDots;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FiveDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
