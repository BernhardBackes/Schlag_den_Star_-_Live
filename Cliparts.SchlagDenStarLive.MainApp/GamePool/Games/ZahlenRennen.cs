namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class ZahlenRennen : Templates.TwoTextInsertsBorder.Business {

        #region Properties
        #endregion


        #region Funktionen

        public ZahlenRennen()
            : base("Zahlen-Rennen") {
            this.TextInsertPositionX = 0;
            this.TextInsertPositionY = 0;
            this.TextInsertStyle = VentuzScenes.GamePool._Modules.TextInsert.Styles.Short;
            //this.BorderPositionX = 0;
            //this.BorderPositionY = 0;
            //this.BorderScaling = 100;
            //this.BorderStyle = VentuzScenes.GamePool._Modules.Border.Styles.Colored;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
