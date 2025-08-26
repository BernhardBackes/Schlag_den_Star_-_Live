namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class FlaschenAufstellen : Templates.FinishBuzzerRGBScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public FlaschenAufstellen() : base("Flaschen aufstellen") {
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
