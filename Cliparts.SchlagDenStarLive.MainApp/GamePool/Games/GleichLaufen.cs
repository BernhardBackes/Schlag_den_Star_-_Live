namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class GleichLaufen : Templates.TimeOffsetAdditionScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public GleichLaufen()
            : base("Gleich-Laufen") {
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
