namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class FragenDuell : Templates.SelectQuestionScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public FragenDuell()
            : base("Fragen-Duell") {
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.SevenDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
