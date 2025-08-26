namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Bierdeckel : Templates.TaskCounterScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Bierdeckel()
            : base("Bierdeckel") {
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.SevenCrosses;
            this.TaskCounterPositionX = 0;
            this.TaskCounterPositionY = 0;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
