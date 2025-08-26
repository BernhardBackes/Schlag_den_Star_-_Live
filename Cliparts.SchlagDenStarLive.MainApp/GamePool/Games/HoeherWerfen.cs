namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class HoeherWerfen : Templates.Score.Business {

        #region Properties
        #endregion


        #region Funktionen

        public HoeherWerfen()
            : base("Höher werfen") {
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
