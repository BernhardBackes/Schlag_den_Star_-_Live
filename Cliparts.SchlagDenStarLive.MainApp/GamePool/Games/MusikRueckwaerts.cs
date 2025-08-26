namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class MusikRueckwaerts : Templates.AudioPictureBuzzerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public MusikRueckwaerts()
            : base("Musik rückwärts") {
            this.TimeoutPositionY = 30;
            this.TimeoutIsVisible = true;
            this.ScorePositionX = 0;
            this.ScorePositionY = 30;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FourDots;
            this.TaskCounterPositionX = 0;
            this.TaskCounterPositionY = 0;
            this.TaskCounterSize = 11;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
