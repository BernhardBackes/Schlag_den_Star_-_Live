namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class StandHochsprung : Templates.Score.Business {

        #region Properties
        #endregion


        #region Funktionen

        public StandHochsprung()
            : base("Stand-Hochsprung") {
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
