namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Muellabfuhr : Templates.TimeToBeatScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Muellabfuhr()
            : base("Müllabfuhr") {
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.TwoDots;
            this.TimeToBeatPositionX = 0;
            this.TimeToBeatPositionY = 0;
            this.TimeToBeatStyle = VentuzScenes.GamePool._Modules.TimeToBeat.Styles.Stopwatch;
            this.ShowFullscreenTimer = false;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
