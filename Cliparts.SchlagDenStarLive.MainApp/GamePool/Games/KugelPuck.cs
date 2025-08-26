namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class KugelPuck : Templates.Score.Business {

        #region Properties
        #endregion


        #region Funktionen

        public KugelPuck()
            : base("Kugel-Puck") {
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
