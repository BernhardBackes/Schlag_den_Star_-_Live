namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class _Score : Templates.Score.Business {

        #region Properties
        #endregion


        #region Funktionen

        public _Score()
            : base("_Score") {
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
