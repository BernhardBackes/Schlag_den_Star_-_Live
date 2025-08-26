namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class BallonBagger : Templates.CounterSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public BallonBagger()
            : base("Ballon-Bagger") {
            this.CounterStyle = VentuzScenes.GamePool._Modules.Score.Styles.Ballons;
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