namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class MiniTennis : Templates.CounterSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public MiniTennis()
            : base("Mini-Tennis") {
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
