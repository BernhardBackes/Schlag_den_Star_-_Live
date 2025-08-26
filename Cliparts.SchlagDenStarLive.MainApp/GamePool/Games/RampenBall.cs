namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class RampenBall : Templates.SetsScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public RampenBall()
            : base("Rampen-Ball") {

            this.SetsPositionX = 0;
            this.SetsPositionY = 0;
            this.SetsStyle = VentuzScenes.GamePool._Modules.Sets.StyleElements.FourSetsSum;

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
