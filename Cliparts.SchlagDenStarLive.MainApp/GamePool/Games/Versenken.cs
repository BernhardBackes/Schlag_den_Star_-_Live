namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Versenken : Templates.Score.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Versenken()
            : base("Versenken") {
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
