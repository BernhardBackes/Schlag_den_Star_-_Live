namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Abwerfen : Templates.PenaltySoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Abwerfen()
            : base("Abwerfen") {
            this.PenaltyPositionX = 0;
            this.PenaltyPositionY = 0;
            this.PenaltyDotsCount = 10;
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
