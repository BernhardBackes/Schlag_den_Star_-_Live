namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class TischShuffleboard : Templates.CounterSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public TischShuffleboard()
            : base("Tisch-Shuffleboard") {
            this.CounterStyle = VentuzScenes.GamePool._Modules.Score.Styles.CounterLeft;
            this.CounterPositionX = 0;
            this.CounterPositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FiveDots;
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