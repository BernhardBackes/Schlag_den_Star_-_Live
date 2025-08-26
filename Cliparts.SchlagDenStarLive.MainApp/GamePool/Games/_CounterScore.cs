namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class _CounterScore : Templates.CounterScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public _CounterScore()
            : base("_CounterScore") {
            this.CounterScorePositionX = 0;
            this.CounterScorePositionY = 0;
            this.CounterScoreStyle = VentuzScenes.GamePool._Modules.CounterScore.Styles.ThreeDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
