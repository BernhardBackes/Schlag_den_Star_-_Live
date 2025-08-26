
namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class NichtBeruehren : Templates.ContactDMXScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public NichtBeruehren()
            : base("Nicht Berühren!") {
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
