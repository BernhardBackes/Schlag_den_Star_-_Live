namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Chippen : Templates.SetsScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Chippen()
            : base("Chippen") {
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
            this.SetsPositionX = 0;
            this.SetsPositionY = 0;
            this.SetsStyle = VentuzScenes.GamePool._Modules.Sets.StyleElements.FiveSetsSum;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
