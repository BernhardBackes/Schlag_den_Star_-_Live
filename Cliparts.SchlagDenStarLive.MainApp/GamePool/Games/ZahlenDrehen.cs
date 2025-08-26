namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class ZahlenDrehen : Templates.BorderScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public ZahlenDrehen() : base("Zahlen drehen") {
            this.BorderPositionX = -10;
            this.BorderPositionY = -164;
            this.BorderScaling = 80;
            this.BorderStyle = VentuzScenes.GamePool._Modules.Border.Styles.FiveDots;
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
