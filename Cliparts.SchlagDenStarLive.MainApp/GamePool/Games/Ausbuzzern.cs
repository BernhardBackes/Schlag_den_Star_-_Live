namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Ausbuzzern : Templates.SpeedBuzzerCounterSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Ausbuzzern()
            : base("Ausbuzzern") {

            this.CounterPositionX = 0;
            this.CounterPositionY = 0;
            this.CounterStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;

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
