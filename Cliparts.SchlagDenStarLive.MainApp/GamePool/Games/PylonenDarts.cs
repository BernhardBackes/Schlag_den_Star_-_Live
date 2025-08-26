namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class PylonenDarts : Templates.SetsScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public PylonenDarts()
            : base("Pylonen-Darts") {
            this.SetsStyle = VentuzScenes.GamePool._Modules.Sets.StyleElements.SixSetsSum;
            this.SetsPositionX = 0;
            this.SetsPositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FourDots;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}