namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Rohrhammer : Templates.CounterSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Rohrhammer()
            : base("Rohrhammer") {
            this.CounterPositionX = 0;
            this.CounterPositionY = 0;
            this.CounterStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FourDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
