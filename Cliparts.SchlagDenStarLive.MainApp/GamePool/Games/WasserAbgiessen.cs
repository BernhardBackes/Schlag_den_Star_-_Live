namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {
    public class WasserAbgiessen : Templates.TextInsertSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public WasserAbgiessen()
            : base("Wasser abgiessen") {
            this.TextInsertPositionX = 0;
            this.TextInsertPositionY = 0;
            this.TextInsertStyle = VentuzScenes.GamePool._Modules.TextInsert.Styles.Short;
            this.ShowFullscreenTimer = true;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FiveDots;
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
