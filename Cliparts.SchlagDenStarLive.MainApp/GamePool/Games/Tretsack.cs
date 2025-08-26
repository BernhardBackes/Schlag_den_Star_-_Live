
namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Tretsack : Templates.SetsScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Tretsack()
            : base("Tretsack") {
            this.SetsStyle = VentuzScenes.GamePool._Modules.Sets.StyleElements.FiveSetsSum;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
