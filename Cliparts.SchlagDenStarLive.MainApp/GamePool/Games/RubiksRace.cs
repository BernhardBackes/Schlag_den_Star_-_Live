namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class RubiksRace : Templates.PictureScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public RubiksRace()
            : base("Rubik's Race") {
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
            this.ContentPositionX = 0;
            this.ContentPositionY = 0;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
