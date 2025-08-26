namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class TonBildSchere : Templates.MovieQuestionsBuzzerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public TonBildSchere()
            : base("Ton-Bild-Schere") {
            this.TextInsertPositionX = 0;
            this.TextInsertPositionY = 0;
            this.CounterStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
            this.CounterPositionX = 0;
            this.CounterPositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FourDots;
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