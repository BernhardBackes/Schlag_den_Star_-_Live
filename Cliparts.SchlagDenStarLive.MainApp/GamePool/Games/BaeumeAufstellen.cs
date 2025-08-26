namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class BaeumeAufstellen : Templates.Score.Business {

        #region Properties
        #endregion


        #region Funktionen

        public BaeumeAufstellen()
            : base("Bäume aufstellen") {
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
