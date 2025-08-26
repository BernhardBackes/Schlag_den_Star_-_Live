namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class PusteRennen : Templates.FinishBuzzerRGBScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public PusteRennen()
            : base("Puste-Rennen") {
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
