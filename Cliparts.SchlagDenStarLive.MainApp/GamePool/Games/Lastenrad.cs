namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Lastenrad : Templates.CounterSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Lastenrad()
            : base("Lastenrad") {
            this.CounterPositionX = 0;
            this.CounterPositionY = 0;
            this.CounterStyle = VentuzScenes.GamePool._Modules.Score.Styles.FourColoredDots;
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
