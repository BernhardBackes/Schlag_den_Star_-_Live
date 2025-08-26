namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class StimmtsDeluxe : Templates.TrueOrFalseMultiple.Business {

        #region Properties
        #endregion


        #region Funktionen

        public StimmtsDeluxe()
            : base("Stimmt's Deluxe") {
            this.GamePositionX = 0;
            this.GamePositionY = 0;
            this.CountDownDuration = 4;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FourDots;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.CounterPositionX = 0;
            this.CounterPositionY = 0;
            this.CounterStyle = VentuzScenes.GamePool._Modules.Score.Styles.CounterIsolatedTwoDigits;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
