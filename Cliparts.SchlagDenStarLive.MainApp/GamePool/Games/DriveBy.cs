namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class DriveBy : Templates.TimerForTwoSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public DriveBy()
            : base("Drive-By") {
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FourDots;
            this.FinishMode = FinishModes.Crossing;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion


    }
}
