namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class DurchBall : Templates.PenaltySoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public DurchBall()
            : base("Durch-Ball") {
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
