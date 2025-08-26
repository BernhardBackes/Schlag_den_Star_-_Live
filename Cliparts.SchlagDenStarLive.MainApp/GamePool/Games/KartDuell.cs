namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class KartDuell : Templates.TimerForTwoLaps.Business {

        #region Properties
        #endregion


        #region Funktionen

        public KartDuell()
            : base("Kart-Duell") {
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStyle = VentuzScenes.GamePool.TimerForTwoLaps.Insert.Styles.FourDots;
            this.LAPsCount = 3;
            this.ShowFullscreenTimer = false;
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
