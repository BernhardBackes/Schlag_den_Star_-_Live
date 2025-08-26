
namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class DreiOderVier : Templates.MorveMovieScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public DreiOderVier()
            : base("3 oder 4?") {
            this.TimeoutPositionY = 0;
            this.TimeoutPositionY = 0;
            this.TimeoutIsVisible = false;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.SevenDots;
            this.BorderPositionX = 0;
            this.BorderPositionY = -100;
            this.BorderStyle = VentuzScenes.GamePool._Modules.Border.Styles.SevenDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
