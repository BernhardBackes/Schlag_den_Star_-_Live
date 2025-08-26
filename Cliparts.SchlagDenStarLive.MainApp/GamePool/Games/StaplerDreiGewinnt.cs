namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class StaplerDreiGewinnt : Templates.BorderScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public StaplerDreiGewinnt()
            : base("Stapler-Drei-Gewinnt") {
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.TwoDots;
            this.BorderPositionX = 0;
            this.BorderPositionY = 0;
            this.BorderScaling = 100;
            this.BorderStyle = VentuzScenes.GamePool._Modules.Border.Styles.Names;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
