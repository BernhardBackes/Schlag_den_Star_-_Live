namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class FallenLassen : Templates.Score.Business {

        #region Properties
        #endregion


        #region Funktionen

        public FallenLassen()
            : base("Fallen lassen") {
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.CounterLeft;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
