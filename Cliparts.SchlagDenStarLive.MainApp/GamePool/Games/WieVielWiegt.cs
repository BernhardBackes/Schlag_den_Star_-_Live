
namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class WieVielWiegt : Templates.MovieNumericInputCloserToValue.Business {

        #region Properties
        #endregion


        #region Funktionen

        public WieVielWiegt()
            : base("Wie viel wiegt..?") {
            this.InputInsertPositionX = 0;
            this.InputInsertPositionY = 0;
            this.TextInsertPositionX = 0;
            this.TextInsertPositionY = 0;
            this.TextInsertStyle = VentuzScenes.GamePool._Modules.TextInsert.Styles.OneRow;
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
