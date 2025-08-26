namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Brettminton : Templates.CounterSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Brettminton()
            : base("Brettminton") {
            this.CounterStyle = VentuzScenes.GamePool._Modules.Score.Styles.Sport;
            this.CounterPositionX = 0;
            this.CounterPositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.TwoDots;
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