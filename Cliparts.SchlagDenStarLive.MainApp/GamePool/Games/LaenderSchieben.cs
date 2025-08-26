namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class LaenderSchieben : Templates.PlaceImageScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public LaenderSchieben()
            : base("Länder schieben") {
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;

            this.TaskCounterSize = 9;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
