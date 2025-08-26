namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class MultiKorb : Templates.DoubleRGBIndicators.Business {

        #region Properties
        #endregion


        #region Funktionen

        public MultiKorb()
            : base("Multi-Korb") {
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.TwoDots;
            this.IndicatorsCount = 7;
            this.OffColor = System.Drawing.Color.White;
            this.LeftColor = System.Drawing.Color.Red;
            this.RightColor = System.Drawing.Color.Blue;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
