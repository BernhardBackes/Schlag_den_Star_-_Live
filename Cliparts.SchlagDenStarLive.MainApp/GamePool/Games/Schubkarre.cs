namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Schubkarre : Templates.Score.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Schubkarre()
            : base("Schubkarre") {
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.TwoDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
