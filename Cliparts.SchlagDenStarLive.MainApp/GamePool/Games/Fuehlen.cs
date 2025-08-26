namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Fuehlen : Templates.TimerCounterToBeatTextInsert.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Fuehlen()
            : base("Fühlen") {
            this.Vinsert_Timer.AlarmTime1 = 10;
            this.Vinsert_Timer.AlarmTime2 = -1;
            this.Vinsert_Timer.PositionX = 0;
            this.Vinsert_Timer.PositionY = 0;
            this.Vinsert_Timer.StartTime = 3000;
            this.Vinsert_Timer.StopTime = 0;
            this.Vinsert_Timer.Style = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
            this.CounterPositionX = 0;
            this.CounterPositionY = 0;
            this.TextInsertPositionX = 0;
            this.TextInsertPositionY = 0;
            this.TextInsertStyle = VentuzScenes.GamePool._Modules.TextInsert.Styles.OneRow;
            //this.ScorePositionX = 0;
            //this.ScorePositionY = 0;
            //this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FiveDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
