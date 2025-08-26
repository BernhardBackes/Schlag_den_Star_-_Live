namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class FahrradBowling : Templates.CounterSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public FahrradBowling()
            : base("Fahrrad-Bowling") {
            this.CounterStyle = VentuzScenes.GamePool._Modules.Score.Styles.FourCrosses;
            this.CounterPositionX = 0;
            this.CounterPositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
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