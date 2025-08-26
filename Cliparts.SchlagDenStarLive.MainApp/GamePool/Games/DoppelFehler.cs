namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class DoppelFehler : Templates.SelectTwoFields.Business {

        #region Properties
        #endregion


        #region Funktionen

        public DoppelFehler()
            : base("Doppel-Fehler") {
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;

            this.TaskCounterPositionX = 0;
            this.TaskCounterPositionY = 0;
            this.TaskCounterSize = 7;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
