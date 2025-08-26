namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Bankball : Templates.Score.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Bankball()
            : base("Bankball") {
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
