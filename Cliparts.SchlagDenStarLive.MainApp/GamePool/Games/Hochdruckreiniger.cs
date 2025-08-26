namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Hochdruckreiniger : Templates.TimerForTwoSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Hochdruckreiniger()
            : base("Hochdruckreiniger") {
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.ShowFullscreenTimer = false;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.BuzzerMode = true;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
