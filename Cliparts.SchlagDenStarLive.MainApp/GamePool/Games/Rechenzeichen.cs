namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Rechenzeichen : Templates.MathematicalFormulaScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Rechenzeichen()
            : base("Rechenzeichen") {
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.SevenDots;
            this.TaskCounterPositionX = 0;
            this.TaskCounterPositionY = 0;
            this.TaskCounterSize = 9;
            this.GameInsertPositionX = 0;
            this.GameInsertPositionY = 0;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
