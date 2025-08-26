namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Raeuberleiter : Templates.TargetCounterSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Raeuberleiter()
            : base("Räuberleiter") {
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
            this.CounterPositionX = 0;
            this.CounterPositionY = 0;
            this.CounterStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
            this.CounterTarget = 10;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
