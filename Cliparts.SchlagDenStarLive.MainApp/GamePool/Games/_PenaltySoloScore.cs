namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class _PenaltySoloScore : Templates.PenaltySoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public _PenaltySoloScore()
            : base("_PenaltySoloScore") {
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
