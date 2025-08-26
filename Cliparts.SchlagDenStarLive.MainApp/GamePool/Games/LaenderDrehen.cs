namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class LaenderDrehen : Templates.RotateImageScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public LaenderDrehen()
            : base("Autos") {
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.BorderPositionX = 0;
            this.BorderPositionY = -100;
            this.BorderStyle = VentuzScenes.GamePool._Modules.Border.Styles.Counter;
            this.TextInsertPositionX = 0;
            this.TextInsertPositionY = 40;
            this.TextInsertStyle = VentuzScenes.GamePool._Modules.TextInsert.Styles.Short;
            this.TaskCounterPositionX = 0;
            this.TaskCounterPositionY = 0;
            this.TaskCounterSize = 13;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
