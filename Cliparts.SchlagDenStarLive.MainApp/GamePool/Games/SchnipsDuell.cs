namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class SchnipsDuell : Templates.Score.Business {

        #region Properties
        #endregion


        #region Funktionen

        public SchnipsDuell()
            : base("Schnips-Duell") {
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.SevenDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
