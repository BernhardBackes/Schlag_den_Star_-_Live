
namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Flieger : Templates.SetsScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Flieger()
            : base("Flieger") {
            this.SetsPositionX = 0;
            this.SetsPositionY = 0;
            this.SetsStyle = VentuzScenes.GamePool._Modules.Sets.StyleElements.FiveSetsSum;
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
