namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class EimerBall : Templates.PenaltySoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public EimerBall()
            : base("Eimer-Ball") {
            this.PenaltyDotsCount = 5;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
