namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Springball : Templates.Score.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Springball()
            : base("Springball") {
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
