namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Hitster : Templates.JukeboxScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Hitster()
            : base("Hitster") {
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
