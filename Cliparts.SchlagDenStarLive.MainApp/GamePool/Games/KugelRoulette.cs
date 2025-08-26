namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class KugelRoulette : Templates.Score.Business {

        #region Properties
        #endregion


        #region Funktionen

        public KugelRoulette()
            : base("Kugel-Roulette") {
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
