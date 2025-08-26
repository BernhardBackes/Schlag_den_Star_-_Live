namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class BallonHuepfen : Templates.Score.Business {

        #region Properties
        #endregion


        #region Funktionen

        public BallonHuepfen()
            : base("Ballon-Hüpfen") {
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
