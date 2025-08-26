namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class RiechDuell : Templates.TextInsertScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public RiechDuell()
            : base("Riech-Duell") {
            this.TextInsertPositionX = 0;
            this.TextInsertPositionY = 0;
            this.TextInsertStyle = VentuzScenes.GamePool._Modules.TextInsert.Styles.OneRow;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FiveDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
