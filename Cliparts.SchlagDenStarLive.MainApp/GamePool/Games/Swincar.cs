namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Swincar : Templates.LAPTimeToBeatScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Swincar()
            : base("Swincar") {
            this.LAPsCount = 3;
            this.ShowFullscreenTimer = false;
            this.TimeToBeatPositionX = 0;
            this.TimeToBeatPositionY = 0;
            this.TimeToBeatStyle = VentuzScenes.GamePool._Modules.TimeToBeat.Styles.Stopwatch;
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
