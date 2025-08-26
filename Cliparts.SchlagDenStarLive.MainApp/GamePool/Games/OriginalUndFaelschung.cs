namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class OriginalUndFaelschung : Templates.SelectPictureAorBTimerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public OriginalUndFaelschung()
            : base("Original und Fälschung") {
            this.BorderPositionX = 0;
            this.BorderPositionY = -100;
            this.BorderScaling = 100;
            this.BorderStyle = VentuzScenes.GamePool._Modules.Border.Styles.Counter;
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
