namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class WeihnachtsbaumWerfen : Templates.DecimalAdditionScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public WeihnachtsbaumWerfen() : base("Weihnachtsbaum werfen") {
            this.DecimalAdditionPositionX = 0;
            this.DecimalAdditionPositionY = 0;
            this.DecimalAdditionStyle = VentuzScenes.GamePool._Modules.DecimalAddition.Styles.FourSets;
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
