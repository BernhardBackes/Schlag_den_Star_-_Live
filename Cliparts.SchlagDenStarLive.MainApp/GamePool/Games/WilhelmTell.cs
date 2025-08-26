namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class WilhelmTell : Templates.CounterScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public WilhelmTell()
            : base("Wilhelm Tell") {
            this.CounterScoreStyle = VentuzScenes.GamePool._Modules.CounterScore.Styles.ThreeDots;
            this.WinnerMode = Templates._Base.CounterScore.WinnerModes.LowestCounter;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
