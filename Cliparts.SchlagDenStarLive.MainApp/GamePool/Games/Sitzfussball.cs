namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Sitzfussball : Templates.Score.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Sitzfussball()
            : base("Sitzfußball") {
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FiveDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
