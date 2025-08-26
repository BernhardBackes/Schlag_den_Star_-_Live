namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Menschenkenntnis : Templates.TextInsertNumericSelectionScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Menschenkenntnis()
            : base("Menschenkenntnis") {
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Sport;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.TextInsertPositionX = 0;
            this.TextInsertPositionY = 0;
            this.TextInsertStyle = VentuzScenes.GamePool._Modules.TextInsert.Styles.TwoRows;
            this.CounterPositionX = 0;
            this.CounterPositionY = 50;
            this.CounterSize = VentuzScenes.GamePool._Modules.Counter.SizeElements.TwoDigitsLarge;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
