namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class HandstandSchalter : Templates.TripleBuzzerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public HandstandSchalter()
            : base("Handstand-Schalter") {
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
